using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StyleCopAnalyzersSetupUtility {
    public partial class StyleCopExcluderForm : Form {
        private const string CSharpProjectsNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";

        public StyleCopExcluderForm() {
            InitializeComponent();

            ExecuteButton.Enabled = false;
            CSProjFilePathTextBox.TextChanged += ExclusionsTextBoxs_TextChanged;
            FolderTextBox.TextChanged += ExclusionsTextBoxs_TextChanged;
            ProjectsInFolderRadioButton.CheckedChanged += ProjectsInFolderRadioButton_CheckedChanged;
            OpenFileButton.Click += OpenFileButton_Click;
            OpenFolderButton.Click += OpenFolderButton_Click;
            ExecuteButton.Click += ExecuteButton_Click;
            ExcludeFromStyleCopCheckBox.CheckedChanged += OptionsCheckBoxs_CheckedChanged;
        }

        private void OptionsCheckBoxs_CheckedChanged(object sender, EventArgs e) {
            RefreshExecuteButtonState();
        }

        private void ProjectsInFolderRadioButton_CheckedChanged(object sender, EventArgs e) {
            SingleProjectPanel.Visible = !ProjectsInFolderRadioButton.Checked;
            ProjectsFolderPanel.Visible = ProjectsInFolderRadioButton.Checked;
            RefreshExecuteButtonState();
        }

        private void ExecuteButton_Click(object sender, EventArgs e) {
            Execute();
        }

        private void ExclusionsTextBoxs_TextChanged(object sender, EventArgs e) {
            RefreshExecuteButtonState();
        }

        private void OpenFileButton_Click(object sender, EventArgs e) {
            OpenFile();
        }

        private void OpenFolderButton_Click(object sender, EventArgs e) {
            OpenFolder();
        }

        private void RefreshExecuteButtonState() {
            bool enabled = false;

            if (SingleProjectRadioButton.Checked) {
                enabled = !string.IsNullOrWhiteSpace(CSProjFilePathTextBox.Text) && File.Exists(CSProjFilePathTextBox.Text);
            } else {
                enabled = !string.IsNullOrWhiteSpace(FolderTextBox.Text) && Directory.Exists(FolderTextBox.Text);
            }

            enabled = enabled && ExcludeFromStyleCopCheckBox.Checked;

            ExecuteButton.Enabled = enabled;
        }

        private void Execute() {
            if (SingleProjectRadioButton.Checked && ProcessCSProj(CSProjFilePathTextBox.Text)) {
                MessageBox.Show("Operation succeeded!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else if (ProjectsInFolderRadioButton.Checked) {
                int succeededProjects;
                int failedProjects;
                var result = ProcessProjectsInFolder(FolderTextBox.Text, out succeededProjects, out failedProjects);
                if (result) {
                    MessageBox.Show($"Operation succeeded! {succeededProjects} projects successfully processed!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else {
                    MessageBox.Show($"Operation failed! {succeededProjects} projects successfully processed! {failedProjects} projects failed!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void OpenFile() {
            var dialog = new OpenFileDialog();
            dialog.Filter = "C# Project file | *.csproj";
            if (dialog.ShowDialog() == DialogResult.OK) {
                CSProjFilePathTextBox.Text = dialog.FileName;
            }
        }

        private void OpenFolder() {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK) {
                FolderTextBox.Text = dialog.SelectedPath;
            }
        }

        private bool ProcessProjectsInFolder(string folderPath, out int succeededProjects, out int failedProjects) {
            succeededProjects = 0;
            failedProjects = 0;

            var files = Directory.GetFiles(folderPath, "*.csproj", SearchOption.AllDirectories);
            foreach (var item in files) {
                if (ProcessCSProj(item)) {
                    succeededProjects++;
                } else {
                    failedProjects++;
                }
            }

            return succeededProjects >= 0 && failedProjects == 0;
        }

        private bool ProcessCSProj(string csprojFile) {
            if (string.IsNullOrWhiteSpace(csprojFile) || !File.Exists(csprojFile)) {
                MessageBox.Show("Invalid csproj location");
                return false;
            }

            try {
                var projectDocument = new XmlDocument();
                using (var fileStream = File.Open(csprojFile, FileMode.Open, FileAccess.ReadWrite)) {
                    projectDocument.Load(fileStream);

                    XmlNamespaceManager ns = new XmlNamespaceManager(projectDocument.NameTable);
                    ns.AddNamespace("x", CSharpProjectsNamespace);

                    var root = projectDocument.DocumentElement;

                    var json = @"{
  // ACTION REQUIRED: This file was automatically added to your project, but it
  // will not take effect until additional steps are taken to enable it. See the
  // following page for additional information:
  //
  // https://github.com/brunocunhasilva/StyleCopAnalyzers/blob/master/documentation/EnableConfiguration.md
  

  ""settings"": {
    ""documentationRules"": {
                        ""companyName"": ""OutSystems""
    },
    ""excludedFiles"": [],
    ""excludedFileFilters"": [ ""\\.generated\\.cs$"", ""\\.designer\\.cs$"" ]
    }
}";
                    var loadSettings = new JsonLoadSettings();
                    loadSettings.CommentHandling = CommentHandling.Load;
                    loadSettings.LineInfoHandling = LineInfoHandling.Load;
                    var stylecopSettings = JObject.Parse(json, loadSettings);

                    if (ExcludeFromStyleCopCheckBox.Checked) {
                        ExcludeFilesFromStyleCop(projectDocument, stylecopSettings, GetCommonsThirdPartyPath(csprojFile), ns);
                    }

                    // write JSON directly to a file
                    using (StreamWriter file = File.CreateText(Directory.GetParent(csprojFile).FullName + @"\stylecop.json"))
                    using (JsonTextWriter writer = new JsonTextWriter(file)) {
                        stylecopSettings.WriteTo(writer);
                    }
                }
                projectDocument.Save(csprojFile);

                return true;
            } catch (Exception ex) {
                MessageBox.Show($"Exception has occurred{Environment.NewLine}{ex.Message}{Environment.NewLine}StackTrace: {ex.StackTrace}");
                return false;
            }
        }

        private void ExcludeFilesFromStyleCop(XmlDocument projectDocument, JObject stylecopSettings, string commonFilesPath, XmlNamespaceManager ns) {
            var projectNode = projectDocument.DocumentElement;
            var compileNodes = projectNode.SelectNodes("*[local-name()='ItemGroup']/*[local-name()='Compile']", ns);
            
            var excludedFilesJObj = stylecopSettings["settings"]["excludedFiles"] as JArray;

            var csprojAlreadySetup = false;
            foreach (XmlNode compileNode in compileNodes) {
                csprojAlreadySetup = csprojAlreadySetup ? csprojAlreadySetup : compileNode.SelectNodes("*[local-name()='AdditionalFiles'][@Include='stylecop.json']", ns).Count > 0;
                
                var includeAttr = compileNode.Attributes.GetNamedItem("Include");
                if (includeAttr?.Value?.EndsWith(".cs", StringComparison.InvariantCultureIgnoreCase) ?? false) {
                    excludedFilesJObj.Add(includeAttr.Value);
                }
            }

            if (projectNode != null && !csprojAlreadySetup) {
                var itemGroupNode = projectDocument.CreateElement("ItemGroup", CSharpProjectsNamespace);

                var compileNode = compileNodes[0];
                // add the StyleCop.json settings file to the csproj
                var settingsNode = CreateNewProjectDocumentNode(projectDocument, itemGroupNode, "AdditionalFiles", "Include", "stylecop.json");

                // add the GlobalSuppressions file link
                var globalSuppressionsNode = CreateNewProjectDocumentNode(projectDocument, itemGroupNode, "Compile", "Include", Path.Combine(commonFilesPath, "GlobalSuppressions.cs"));

                var linkNode = projectDocument.CreateElement("Link", CSharpProjectsNamespace);
                linkNode.InnerText = "GlobalSuppressions.cs";
                globalSuppressionsNode.AppendChild(linkNode);

                // setup the analyzers references
                CreateNewProjectDocumentNode(projectDocument, itemGroupNode, "Analyzer", "Include", Path.Combine(commonFilesPath, "Newtonsoft.Json.dll"));
                CreateNewProjectDocumentNode(projectDocument, itemGroupNode, "Analyzer", "Include", Path.Combine(commonFilesPath, "StyleCop.Analyzers.CodeFixes.dll"));
                CreateNewProjectDocumentNode(projectDocument, itemGroupNode, "Analyzer", "Include", Path.Combine(commonFilesPath, "StyleCop.Analyzers.dll"));

                // setup json Schema for the settings file
                var projectExtensionsNode = projectNode.SelectSingleNode("*[local-name()='ProjectExtensions']", ns);
                if (projectExtensionsNode == null) {
                    projectExtensionsNode = CreateNewProjectDocumentNode(projectDocument, projectNode, "ProjectExtensions", string.Empty, string.Empty);
                }
                var visualStudioNode = projectExtensionsNode.SelectSingleNode("*[local-name()='VisualStudio']", ns);
                if (visualStudioNode == null) {
                    visualStudioNode = CreateNewProjectDocumentNode(projectDocument, projectExtensionsNode, "VisualStudio", string.Empty, string.Empty);
                }

                CreateNewProjectDocumentNode(
                    projectDocument, 
                    visualStudioNode, 
                    "UserProperties", 
                    "stylecop_1json__JSONSchema", 
                    "https://raw.githubusercontent.com/brunocunhasilva/StyleCopAnalyzers/master/StyleCop.Analyzers/StyleCop.Analyzers/Settings/stylecop.schema.json");

                projectNode.AppendChild(itemGroupNode);
            }
        }

        private XmlElement CreateNewProjectDocumentNode(XmlDocument projectDocument, XmlNode parentNode, string nodeName, string attributeName, string attributeValue) {
            var createdNode = projectDocument.CreateElement(nodeName, CSharpProjectsNamespace);
            if (!string.IsNullOrEmpty(attributeName)) {
                var includeAttr = projectDocument.CreateAttribute(attributeName);
                includeAttr.Value = attributeValue;
                createdNode.Attributes.Append(includeAttr);
            }
            
            parentNode.AppendChild(createdNode);

            return createdNode;
        }

        private string GetCommonsThirdPartyPath(string csprojFile) {
            var info = new FileInfo(csprojFile);
            var directory = info.Directory.ToString();
            if (!directory.EndsWith("\\")) { directory += "\\"; }

            for (var level = 0; level < 20; level++) {
                var baseRelativePath = Enumerable.Repeat("..\\", level).Aggregate(new StringBuilder(), (sb, s) => sb.Append(s)).ToString();
                if (Directory.Exists(
                    Path.Combine(directory + baseRelativePath,
                    "Commons\\ThirdParty\\StyleCopAnalyzers\\"))) {
                    return Path.Combine(baseRelativePath,
                    "Commons\\ThirdParty\\StyleCopAnalyzers\\");
                }
            }

            return string.Empty;
        }
    }
}
