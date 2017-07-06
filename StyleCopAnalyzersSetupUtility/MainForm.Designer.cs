namespace StyleCopAnalyzersSetupUtility {
    partial class StyleCopExcluderForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.CSProjFilePathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.SingleProjectRadioButton = new System.Windows.Forms.RadioButton();
            this.ProjectsInFolderRadioButton = new System.Windows.Forms.RadioButton();
            this.SingleProjectPanel = new System.Windows.Forms.Panel();
            this.ProjectsFolderPanel = new System.Windows.Forms.Panel();
            this.FolderTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.OpenFolderButton = new System.Windows.Forms.Button();
            this.ExcludeFromStyleCopCheckBox = new System.Windows.Forms.CheckBox();
            this.SingleProjectPanel.SuspendLayout();
            this.ProjectsFolderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CSProjFilePathTextBox
            // 
            this.CSProjFilePathTextBox.Location = new System.Drawing.Point(79, 3);
            this.CSProjFilePathTextBox.Name = "CSProjFilePathTextBox";
            this.CSProjFilePathTextBox.Size = new System.Drawing.Size(327, 20);
            this.CSProjFilePathTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "CSProj";
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Location = new System.Drawing.Point(412, 3);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(31, 20);
            this.OpenFileButton.TabIndex = 2;
            this.OpenFileButton.Text = "...";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Location = new System.Drawing.Point(79, 119);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(75, 23);
            this.ExecuteButton.TabIndex = 3;
            this.ExecuteButton.Text = "Execute";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            // 
            // SingleProjectRadioButton
            // 
            this.SingleProjectRadioButton.AutoSize = true;
            this.SingleProjectRadioButton.Location = new System.Drawing.Point(200, 12);
            this.SingleProjectRadioButton.Name = "SingleProjectRadioButton";
            this.SingleProjectRadioButton.Size = new System.Drawing.Size(89, 17);
            this.SingleProjectRadioButton.TabIndex = 4;
            this.SingleProjectRadioButton.Text = "Single project";
            this.SingleProjectRadioButton.UseVisualStyleBackColor = true;
            // 
            // ProjectsInFolderRadioButton
            // 
            this.ProjectsInFolderRadioButton.AutoSize = true;
            this.ProjectsInFolderRadioButton.Checked = true;
            this.ProjectsInFolderRadioButton.Location = new System.Drawing.Point(81, 12);
            this.ProjectsInFolderRadioButton.Name = "ProjectsInFolderRadioButton";
            this.ProjectsInFolderRadioButton.Size = new System.Drawing.Size(103, 17);
            this.ProjectsInFolderRadioButton.TabIndex = 5;
            this.ProjectsInFolderRadioButton.TabStop = true;
            this.ProjectsInFolderRadioButton.Text = "Projects in folder";
            this.ProjectsInFolderRadioButton.UseVisualStyleBackColor = true;
            // 
            // SingleProjectPanel
            // 
            this.SingleProjectPanel.Controls.Add(this.CSProjFilePathTextBox);
            this.SingleProjectPanel.Controls.Add(this.label1);
            this.SingleProjectPanel.Controls.Add(this.OpenFileButton);
            this.SingleProjectPanel.Location = new System.Drawing.Point(0, 30);
            this.SingleProjectPanel.Name = "SingleProjectPanel";
            this.SingleProjectPanel.Size = new System.Drawing.Size(470, 28);
            this.SingleProjectPanel.TabIndex = 7;
            this.SingleProjectPanel.Visible = false;
            // 
            // ProjectsFolderPanel
            // 
            this.ProjectsFolderPanel.Controls.Add(this.FolderTextBox);
            this.ProjectsFolderPanel.Controls.Add(this.label2);
            this.ProjectsFolderPanel.Controls.Add(this.OpenFolderButton);
            this.ProjectsFolderPanel.Location = new System.Drawing.Point(0, 30);
            this.ProjectsFolderPanel.Name = "ProjectsFolderPanel";
            this.ProjectsFolderPanel.Size = new System.Drawing.Size(470, 28);
            this.ProjectsFolderPanel.TabIndex = 8;
            // 
            // FolderTextBox
            // 
            this.FolderTextBox.Location = new System.Drawing.Point(79, 3);
            this.FolderTextBox.Name = "FolderTextBox";
            this.FolderTextBox.Size = new System.Drawing.Size(327, 20);
            this.FolderTextBox.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Folder";
            // 
            // OpenFolderButton
            // 
            this.OpenFolderButton.Location = new System.Drawing.Point(412, 3);
            this.OpenFolderButton.Name = "OpenFolderButton";
            this.OpenFolderButton.Size = new System.Drawing.Size(31, 20);
            this.OpenFolderButton.TabIndex = 2;
            this.OpenFolderButton.Text = "...";
            this.OpenFolderButton.UseVisualStyleBackColor = true;
            // 
            // ExcludeFromStyleCopCheckBox
            // 
            this.ExcludeFromStyleCopCheckBox.AutoSize = true;
            this.ExcludeFromStyleCopCheckBox.Location = new System.Drawing.Point(79, 69);
            this.ExcludeFromStyleCopCheckBox.Name = "ExcludeFromStyleCopCheckBox";
            this.ExcludeFromStyleCopCheckBox.Size = new System.Drawing.Size(315, 17);
            this.ExcludeFromStyleCopCheckBox.TabIndex = 9;
            this.ExcludeFromStyleCopCheckBox.Text = "Exclude all existent C# files from StyleCop Analyzers vigilance";
            this.ExcludeFromStyleCopCheckBox.UseVisualStyleBackColor = true;
            // 
            // StyleCopExcluderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 163);
            this.Controls.Add(this.ExcludeFromStyleCopCheckBox);
            this.Controls.Add(this.ProjectsFolderPanel);
            this.Controls.Add(this.SingleProjectPanel);
            this.Controls.Add(this.ProjectsInFolderRadioButton);
            this.Controls.Add(this.SingleProjectRadioButton);
            this.Controls.Add(this.ExecuteButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StyleCopExcluderForm";
            this.Text = "StyleCop - CSProj Files excluder";
            this.SingleProjectPanel.ResumeLayout(false);
            this.SingleProjectPanel.PerformLayout();
            this.ProjectsFolderPanel.ResumeLayout(false);
            this.ProjectsFolderPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CSProjFilePathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.Button ExecuteButton;
        private System.Windows.Forms.RadioButton SingleProjectRadioButton;
        private System.Windows.Forms.RadioButton ProjectsInFolderRadioButton;
        private System.Windows.Forms.Panel SingleProjectPanel;
        private System.Windows.Forms.Panel ProjectsFolderPanel;
        private System.Windows.Forms.TextBox FolderTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OpenFolderButton;
        private System.Windows.Forms.CheckBox ExcludeFromStyleCopCheckBox;
    }
}

