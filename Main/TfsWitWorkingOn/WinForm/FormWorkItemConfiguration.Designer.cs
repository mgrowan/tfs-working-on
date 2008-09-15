namespace Rowan.TfsWorkingOn.WinForm
{
    partial class FormWorkItemConfiguration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWorkItemConfiguration));
            this.textBoxTfsServer = new System.Windows.Forms.TextBox();
            this.workingItemConfigurationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxProjects = new System.Windows.Forms.ComboBox();
            this.projectsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonConnect = new System.Windows.Forms.Button();
            this.comboBoxWorkItemType = new System.Windows.Forms.ComboBox();
            this.workItemTypesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.labelServer = new System.Windows.Forms.Label();
            this.labelProject = new System.Windows.Forms.Label();
            this.labelWorkItemType = new System.Windows.Forms.Label();
            this.listBoxFields = new System.Windows.Forms.ListBox();
            this.fieldDefinitionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.textBoxDurationField = new System.Windows.Forms.TextBox();
            this.textBoxRemainingField = new System.Windows.Forms.TextBox();
            this.textBoxElapsedField = new System.Windows.Forms.TextBox();
            this.buttonDuration = new System.Windows.Forms.Button();
            this.buttonRemaining = new System.Windows.Forms.Button();
            this.buttonElapsed = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonSetDirectory = new System.Windows.Forms.Button();
            this.textBoxCurrentDirectory = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.workingItemConfigurationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.workItemTypesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fieldDefinitionsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxTfsServer
            // 
            this.textBoxTfsServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTfsServer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.workingItemConfigurationBindingSource, "Server", true));
            this.textBoxTfsServer.Location = new System.Drawing.Point(111, 12);
            this.textBoxTfsServer.Name = "textBoxTfsServer";
            this.textBoxTfsServer.Size = new System.Drawing.Size(369, 20);
            this.textBoxTfsServer.TabIndex = 1;
            // 
            // workingItemConfigurationBindingSource
            // 
            this.workingItemConfigurationBindingSource.DataSource = typeof(Rowan.TfsWorkingOn.WorkingItemConfiguration);
            // 
            // comboBoxProjects
            // 
            this.comboBoxProjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxProjects.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.workingItemConfigurationBindingSource, "SelectedProject", true));
            this.comboBoxProjects.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.workingItemConfigurationBindingSource, "IsConnected", true));
            this.comboBoxProjects.DataSource = this.projectsBindingSource;
            this.comboBoxProjects.DisplayMember = "Name";
            this.comboBoxProjects.FormattingEnabled = true;
            this.comboBoxProjects.Location = new System.Drawing.Point(111, 38);
            this.comboBoxProjects.Name = "comboBoxProjects";
            this.comboBoxProjects.Size = new System.Drawing.Size(369, 21);
            this.comboBoxProjects.TabIndex = 4;
            this.comboBoxProjects.ValueMember = "Id";
            // 
            // projectsBindingSource
            // 
            this.projectsBindingSource.DataMember = "Projects";
            this.projectsBindingSource.DataSource = this.workingItemConfigurationBindingSource;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConnect.Location = new System.Drawing.Point(486, 10);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 2;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // comboBoxWorkItemType
            // 
            this.comboBoxWorkItemType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxWorkItemType.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.workingItemConfigurationBindingSource, "IsConnected", true));
            this.comboBoxWorkItemType.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.workingItemConfigurationBindingSource, "SelectedWorkItemType", true));
            this.comboBoxWorkItemType.DataSource = this.workItemTypesBindingSource;
            this.comboBoxWorkItemType.DisplayMember = "Name";
            this.comboBoxWorkItemType.FormattingEnabled = true;
            this.comboBoxWorkItemType.Location = new System.Drawing.Point(111, 66);
            this.comboBoxWorkItemType.Name = "comboBoxWorkItemType";
            this.comboBoxWorkItemType.Size = new System.Drawing.Size(369, 21);
            this.comboBoxWorkItemType.TabIndex = 6;
            this.comboBoxWorkItemType.ValueMember = "Name";
            // 
            // workItemTypesBindingSource
            // 
            this.workItemTypesBindingSource.DataMember = "WorkItemTypes";
            this.workItemTypesBindingSource.DataSource = this.projectsBindingSource;
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Location = new System.Drawing.Point(57, 15);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(41, 13);
            this.labelServer.TabIndex = 0;
            this.labelServer.Text = "Server:";
            // 
            // labelProject
            // 
            this.labelProject.AutoSize = true;
            this.labelProject.Location = new System.Drawing.Point(55, 41);
            this.labelProject.Name = "labelProject";
            this.labelProject.Size = new System.Drawing.Size(43, 13);
            this.labelProject.TabIndex = 3;
            this.labelProject.Text = "Project:";
            // 
            // labelWorkItemType
            // 
            this.labelWorkItemType.AutoSize = true;
            this.labelWorkItemType.Location = new System.Drawing.Point(12, 69);
            this.labelWorkItemType.Name = "labelWorkItemType";
            this.labelWorkItemType.Size = new System.Drawing.Size(86, 13);
            this.labelWorkItemType.TabIndex = 5;
            this.labelWorkItemType.Text = "Work Item Type:";
            // 
            // listBoxFields
            // 
            this.listBoxFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxFields.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.workingItemConfigurationBindingSource, "IsConnected", true));
            this.listBoxFields.DataSource = this.fieldDefinitionsBindingSource;
            this.listBoxFields.DisplayMember = "ReferenceName";
            this.listBoxFields.FormattingEnabled = true;
            this.listBoxFields.Location = new System.Drawing.Point(111, 94);
            this.listBoxFields.Name = "listBoxFields";
            this.listBoxFields.Size = new System.Drawing.Size(369, 173);
            this.listBoxFields.TabIndex = 7;
            this.listBoxFields.ValueMember = "ReferenceName";
            // 
            // fieldDefinitionsBindingSource
            // 
            this.fieldDefinitionsBindingSource.DataMember = "FieldDefinitions";
            this.fieldDefinitionsBindingSource.DataSource = this.workItemTypesBindingSource;
            // 
            // textBoxDurationField
            // 
            this.textBoxDurationField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDurationField.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.workingItemConfigurationBindingSource, "DurationField", true));
            this.textBoxDurationField.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.workingItemConfigurationBindingSource, "IsConnected", true));
            this.textBoxDurationField.Location = new System.Drawing.Point(567, 94);
            this.textBoxDurationField.Name = "textBoxDurationField";
            this.textBoxDurationField.Size = new System.Drawing.Size(196, 20);
            this.textBoxDurationField.TabIndex = 9;
            // 
            // textBoxRemainingField
            // 
            this.textBoxRemainingField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRemainingField.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.workingItemConfigurationBindingSource, "RemainingField", true));
            this.textBoxRemainingField.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.workingItemConfigurationBindingSource, "IsConnected", true));
            this.textBoxRemainingField.Location = new System.Drawing.Point(567, 124);
            this.textBoxRemainingField.Name = "textBoxRemainingField";
            this.textBoxRemainingField.Size = new System.Drawing.Size(196, 20);
            this.textBoxRemainingField.TabIndex = 11;
            // 
            // textBoxElapsedField
            // 
            this.textBoxElapsedField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxElapsedField.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.workingItemConfigurationBindingSource, "ElapsedField", true));
            this.textBoxElapsedField.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.workingItemConfigurationBindingSource, "IsConnected", true));
            this.textBoxElapsedField.Location = new System.Drawing.Point(567, 154);
            this.textBoxElapsedField.Name = "textBoxElapsedField";
            this.textBoxElapsedField.Size = new System.Drawing.Size(196, 20);
            this.textBoxElapsedField.TabIndex = 13;
            // 
            // buttonDuration
            // 
            this.buttonDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDuration.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.workingItemConfigurationBindingSource, "IsConnected", true));
            this.buttonDuration.Location = new System.Drawing.Point(486, 94);
            this.buttonDuration.Name = "buttonDuration";
            this.buttonDuration.Size = new System.Drawing.Size(75, 23);
            this.buttonDuration.TabIndex = 8;
            this.buttonDuration.Text = "Duration >";
            this.buttonDuration.UseVisualStyleBackColor = true;
            this.buttonDuration.Click += new System.EventHandler(this.buttonDuration_Click);
            // 
            // buttonRemaining
            // 
            this.buttonRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemaining.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.workingItemConfigurationBindingSource, "IsConnected", true));
            this.buttonRemaining.Location = new System.Drawing.Point(486, 123);
            this.buttonRemaining.Name = "buttonRemaining";
            this.buttonRemaining.Size = new System.Drawing.Size(75, 23);
            this.buttonRemaining.TabIndex = 10;
            this.buttonRemaining.Text = "Remaining >";
            this.buttonRemaining.UseVisualStyleBackColor = true;
            this.buttonRemaining.Click += new System.EventHandler(this.buttonRemaining_Click);
            // 
            // buttonElapsed
            // 
            this.buttonElapsed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonElapsed.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.workingItemConfigurationBindingSource, "IsConnected", true));
            this.buttonElapsed.Location = new System.Drawing.Point(486, 152);
            this.buttonElapsed.Name = "buttonElapsed";
            this.buttonElapsed.Size = new System.Drawing.Size(75, 23);
            this.buttonElapsed.TabIndex = 12;
            this.buttonElapsed.Text = "Elapsed >";
            this.buttonElapsed.UseVisualStyleBackColor = true;
            this.buttonElapsed.Click += new System.EventHandler(this.buttonElapsed_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.workingItemConfigurationBindingSource, "IsConnected", true));
            this.buttonSave.Location = new System.Drawing.Point(605, 237);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 14;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(686, 237);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(77, 23);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Close";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.SelectedPath = global::Rowan.TfsWorkingOn.WinForm.Properties.Settings.Default.ConfigurationsPath;
            // 
            // buttonSetDirectory
            // 
            this.buttonSetDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetDirectory.Location = new System.Drawing.Point(486, 181);
            this.buttonSetDirectory.Name = "buttonSetDirectory";
            this.buttonSetDirectory.Size = new System.Drawing.Size(130, 23);
            this.buttonSetDirectory.TabIndex = 16;
            this.buttonSetDirectory.Text = "Set Configuration Path...";
            this.buttonSetDirectory.UseVisualStyleBackColor = true;
            this.buttonSetDirectory.Click += new System.EventHandler(this.buttonSetDirectory_Click);
            // 
            // textBoxCurrentDirectory
            // 
            this.textBoxCurrentDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCurrentDirectory.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Rowan.TfsWorkingOn.WinForm.Properties.Settings.Default, "ConfigurationsPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxCurrentDirectory.Location = new System.Drawing.Point(486, 207);
            this.textBoxCurrentDirectory.Name = "textBoxCurrentDirectory";
            this.textBoxCurrentDirectory.Size = new System.Drawing.Size(277, 20);
            this.textBoxCurrentDirectory.TabIndex = 17;
            this.textBoxCurrentDirectory.Text = global::Rowan.TfsWorkingOn.WinForm.Properties.Settings.Default.ConfigurationsPath;
            // 
            // FormWorkItemConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(775, 275);
            this.Controls.Add(this.textBoxCurrentDirectory);
            this.Controls.Add(this.buttonSetDirectory);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonElapsed);
            this.Controls.Add(this.buttonRemaining);
            this.Controls.Add(this.buttonDuration);
            this.Controls.Add(this.textBoxElapsedField);
            this.Controls.Add(this.textBoxRemainingField);
            this.Controls.Add(this.textBoxDurationField);
            this.Controls.Add(this.listBoxFields);
            this.Controls.Add(this.labelWorkItemType);
            this.Controls.Add(this.labelProject);
            this.Controls.Add(this.labelServer);
            this.Controls.Add(this.comboBoxWorkItemType);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.comboBoxProjects);
            this.Controls.Add(this.textBoxTfsServer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormWorkItemConfiguration";
            this.Text = "Team Foundation Server Working On... Configuration";
            this.Load += new System.EventHandler(this.FormWorkItemConfiguration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.workingItemConfigurationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.workItemTypesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fieldDefinitionsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxTfsServer;
        private System.Windows.Forms.ComboBox comboBoxProjects;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.ComboBox comboBoxWorkItemType;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.Label labelProject;
        private System.Windows.Forms.Label labelWorkItemType;
        private System.Windows.Forms.BindingSource workingItemConfigurationBindingSource;
        private System.Windows.Forms.BindingSource projectsBindingSource;
        private System.Windows.Forms.ListBox listBoxFields;
        private System.Windows.Forms.TextBox textBoxDurationField;
        private System.Windows.Forms.TextBox textBoxRemainingField;
        private System.Windows.Forms.TextBox textBoxElapsedField;
        private System.Windows.Forms.BindingSource workItemTypesBindingSource;
        private System.Windows.Forms.BindingSource fieldDefinitionsBindingSource;
        private System.Windows.Forms.Button buttonDuration;
        private System.Windows.Forms.Button buttonRemaining;
        private System.Windows.Forms.Button buttonElapsed;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button buttonSetDirectory;
        private System.Windows.Forms.TextBox textBoxCurrentDirectory;
    }
}

