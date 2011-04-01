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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "workingItemConfiguration")]
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
            this.buttonSave = new System.Windows.Forms.Button();
            this.workingItemConfigurationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.settingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonCancel = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonSetDirectory = new System.Windows.Forms.Button();
            this.textBoxCurrentDirectory = new System.Windows.Forms.TextBox();
            this.tabControlConfiguration = new System.Windows.Forms.TabControl();
            this.tabPageOptions = new System.Windows.Forms.TabPage();
            this.pictureBoxHelpMenuQuery = new System.Windows.Forms.PictureBox();
            this.labelMenuQuery = new System.Windows.Forms.Label();
            this.pictureBoxHelpPromptOnResume = new System.Windows.Forms.PictureBox();
            this.checkBoxPromptOnResume = new System.Windows.Forms.CheckBox();
            this.pictureBoxHelpNag = new System.Windows.Forms.PictureBox();
            this.pictureBoxHelpUserActivity = new System.Windows.Forms.PictureBox();
            this.checkBoxEnableNag = new System.Windows.Forms.CheckBox();
            this.checkBoxMonitorUserActivity = new System.Windows.Forms.CheckBox();
            this.labelNagInterval = new System.Windows.Forms.Label();
            this.numericUpDownNagInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAutoTimeOut = new System.Windows.Forms.NumericUpDown();
            this.labelAutoTimeOut = new System.Windows.Forms.Label();
            this.labelConfigurationPath = new System.Windows.Forms.Label();
            this.tabPageMappings = new System.Windows.Forms.TabPage();
            this.labelElapsed = new System.Windows.Forms.Label();
            this.labelRemaining = new System.Windows.Forms.Label();
            this.labelDuration = new System.Windows.Forms.Label();
            this.comboBoxElapsed = new System.Windows.Forms.ComboBox();
            this.fieldDefinitionsElapsedBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.selectedWorkItemTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxRemaining = new System.Windows.Forms.ComboBox();
            this.fieldDefinitionsRemainingBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxDuration = new System.Windows.Forms.ComboBox();
            this.fieldDefinitionsDurationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.labelWorkItemType = new System.Windows.Forms.Label();
            this.comboBoxWorkItemType = new System.Windows.Forms.ComboBox();
            this.tabPageUpdateWarehouse = new System.Windows.Forms.TabPage();
            this.labelWarehouseStatus = new System.Windows.Forms.Label();
            this.labelUpdateDataWarehouse = new System.Windows.Forms.Label();
            this.textBoxWarehouseStatus = new System.Windows.Forms.TextBox();
            this.progressBarWarehouse = new System.Windows.Forms.ProgressBar();
            this.buttonRefreshWarehouseStatus = new System.Windows.Forms.Button();
            this.buttonUpdateWarehouse = new System.Windows.Forms.Button();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.labelVersion = new System.Windows.Forms.Label();
            this.pictureBoxAbout = new System.Windows.Forms.PictureBox();
            this.linkLabelAbout = new System.Windows.Forms.LinkLabel();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.toolTipHelp = new System.Windows.Forms.ToolTip(this.components);
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.buttonOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.workingItemConfigurationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingsBindingSource)).BeginInit();
            this.tabControlConfiguration.SuspendLayout();
            this.tabPageOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelpMenuQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelpPromptOnResume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelpNag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelpUserActivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNagInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoTimeOut)).BeginInit();
            this.tabPageMappings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fieldDefinitionsElapsedBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedWorkItemTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fieldDefinitionsRemainingBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fieldDefinitionsDurationBindingSource)).BeginInit();
            this.tabPageUpdateWarehouse.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAbout)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.workingItemConfigurationBindingSource, "IsDirty", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.buttonSave.Location = new System.Drawing.Point(357, 212);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 14;
            this.buttonSave.Text = "Apply";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // workingItemConfigurationBindingSource
            // 
            this.workingItemConfigurationBindingSource.DataSource = typeof(Rowan.TfsWorkingOn.WorkingItemConfiguration);
            // 
            // settingsBindingSource
            // 
            this.settingsBindingSource.DataSource = typeof(Rowan.TfsWorkingOn.Settings);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(274, 212);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(77, 23);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSetDirectory
            // 
            this.buttonSetDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetDirectory.Location = new System.Drawing.Point(404, 4);
            this.buttonSetDirectory.Name = "buttonSetDirectory";
            this.buttonSetDirectory.Size = new System.Drawing.Size(23, 23);
            this.buttonSetDirectory.TabIndex = 16;
            this.buttonSetDirectory.Text = "...";
            this.buttonSetDirectory.UseVisualStyleBackColor = true;
            this.buttonSetDirectory.Click += new System.EventHandler(this.buttonSetDirectory_Click);
            // 
            // textBoxCurrentDirectory
            // 
            this.textBoxCurrentDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCurrentDirectory.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.settingsBindingSource, "ConfigurationsPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxCurrentDirectory.Location = new System.Drawing.Point(94, 6);
            this.textBoxCurrentDirectory.Name = "textBoxCurrentDirectory";
            this.textBoxCurrentDirectory.Size = new System.Drawing.Size(304, 20);
            this.textBoxCurrentDirectory.TabIndex = 17;
            // 
            // tabControlConfiguration
            // 
            this.tabControlConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlConfiguration.Controls.Add(this.tabPageOptions);
            this.tabControlConfiguration.Controls.Add(this.tabPageMappings);
            this.tabControlConfiguration.Controls.Add(this.tabPageUpdateWarehouse);
            this.tabControlConfiguration.Controls.Add(this.tabPageAbout);
            this.tabControlConfiguration.Location = new System.Drawing.Point(1, 2);
            this.tabControlConfiguration.Name = "tabControlConfiguration";
            this.tabControlConfiguration.SelectedIndex = 0;
            this.tabControlConfiguration.Size = new System.Drawing.Size(442, 201);
            this.tabControlConfiguration.TabIndex = 19;
            this.tabControlConfiguration.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControlConfiguration_Selecting);
            // 
            // tabPageOptions
            // 
            this.tabPageOptions.Controls.Add(this.pictureBoxHelpMenuQuery);
            this.tabPageOptions.Controls.Add(this.labelMenuQuery);
            this.tabPageOptions.Controls.Add(this.pictureBoxHelpPromptOnResume);
            this.tabPageOptions.Controls.Add(this.checkBoxPromptOnResume);
            this.tabPageOptions.Controls.Add(this.pictureBoxHelpNag);
            this.tabPageOptions.Controls.Add(this.pictureBoxHelpUserActivity);
            this.tabPageOptions.Controls.Add(this.checkBoxEnableNag);
            this.tabPageOptions.Controls.Add(this.checkBoxMonitorUserActivity);
            this.tabPageOptions.Controls.Add(this.labelNagInterval);
            this.tabPageOptions.Controls.Add(this.numericUpDownNagInterval);
            this.tabPageOptions.Controls.Add(this.numericUpDownAutoTimeOut);
            this.tabPageOptions.Controls.Add(this.labelAutoTimeOut);
            this.tabPageOptions.Controls.Add(this.labelConfigurationPath);
            this.tabPageOptions.Controls.Add(this.buttonSetDirectory);
            this.tabPageOptions.Controls.Add(this.textBoxCurrentDirectory);
            this.tabPageOptions.Location = new System.Drawing.Point(4, 22);
            this.tabPageOptions.Name = "tabPageOptions";
            this.tabPageOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOptions.Size = new System.Drawing.Size(434, 175);
            this.tabPageOptions.TabIndex = 1;
            this.tabPageOptions.Text = "Options";
            this.tabPageOptions.UseVisualStyleBackColor = true;
            // 
            // pictureBoxHelpMenuQuery
            // 
            this.pictureBoxHelpMenuQuery.Cursor = System.Windows.Forms.Cursors.Help;
            this.pictureBoxHelpMenuQuery.Image = global::Rowan.TfsWorkingOn.WinForm.Properties.Resources.helpImage;
            this.pictureBoxHelpMenuQuery.Location = new System.Drawing.Point(340, 114);
            this.pictureBoxHelpMenuQuery.Name = "pictureBoxHelpMenuQuery";
            this.pictureBoxHelpMenuQuery.Size = new System.Drawing.Size(22, 22);
            this.pictureBoxHelpMenuQuery.TabIndex = 34;
            this.pictureBoxHelpMenuQuery.TabStop = false;
            // 
            // labelMenuQuery
            // 
            this.labelMenuQuery.AutoSize = true;
            this.labelMenuQuery.Location = new System.Drawing.Point(20, 118);
            this.labelMenuQuery.Name = "labelMenuQuery";
            this.labelMenuQuery.Size = new System.Drawing.Size(68, 13);
            this.labelMenuQuery.TabIndex = 20;
            this.labelMenuQuery.Text = "Menu Query:";
            // 
            // pictureBoxHelpPromptOnResume
            // 
            this.pictureBoxHelpPromptOnResume.Cursor = System.Windows.Forms.Cursors.Help;
            this.pictureBoxHelpPromptOnResume.Image = global::Rowan.TfsWorkingOn.WinForm.Properties.Resources.helpImage;
            this.pictureBoxHelpPromptOnResume.Location = new System.Drawing.Point(340, 60);
            this.pictureBoxHelpPromptOnResume.Name = "pictureBoxHelpPromptOnResume";
            this.pictureBoxHelpPromptOnResume.Size = new System.Drawing.Size(22, 22);
            this.pictureBoxHelpPromptOnResume.TabIndex = 33;
            this.pictureBoxHelpPromptOnResume.TabStop = false;
            // 
            // checkBoxPromptOnResume
            // 
            this.checkBoxPromptOnResume.AutoSize = true;
            this.checkBoxPromptOnResume.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.settingsBindingSource, "PromptOnResume", true));
            this.checkBoxPromptOnResume.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.settingsBindingSource, "MonitorUserActivity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxPromptOnResume.Location = new System.Drawing.Point(211, 61);
            this.checkBoxPromptOnResume.Name = "checkBoxPromptOnResume";
            this.checkBoxPromptOnResume.Size = new System.Drawing.Size(111, 17);
            this.checkBoxPromptOnResume.TabIndex = 32;
            this.checkBoxPromptOnResume.Text = "Prompt on resume";
            this.checkBoxPromptOnResume.UseVisualStyleBackColor = true;
            // 
            // pictureBoxHelpNag
            // 
            this.pictureBoxHelpNag.Cursor = System.Windows.Forms.Cursors.Help;
            this.pictureBoxHelpNag.Image = global::Rowan.TfsWorkingOn.WinForm.Properties.Resources.helpImage;
            this.pictureBoxHelpNag.Location = new System.Drawing.Point(340, 87);
            this.pictureBoxHelpNag.Name = "pictureBoxHelpNag";
            this.pictureBoxHelpNag.Size = new System.Drawing.Size(22, 22);
            this.pictureBoxHelpNag.TabIndex = 31;
            this.pictureBoxHelpNag.TabStop = false;
            // 
            // pictureBoxHelpUserActivity
            // 
            this.pictureBoxHelpUserActivity.Cursor = System.Windows.Forms.Cursors.Help;
            this.pictureBoxHelpUserActivity.Image = global::Rowan.TfsWorkingOn.WinForm.Properties.Resources.helpImage;
            this.pictureBoxHelpUserActivity.Location = new System.Drawing.Point(340, 33);
            this.pictureBoxHelpUserActivity.Name = "pictureBoxHelpUserActivity";
            this.pictureBoxHelpUserActivity.Size = new System.Drawing.Size(22, 22);
            this.pictureBoxHelpUserActivity.TabIndex = 30;
            this.pictureBoxHelpUserActivity.TabStop = false;
            // 
            // checkBoxEnableNag
            // 
            this.checkBoxEnableNag.AutoSize = true;
            this.checkBoxEnableNag.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.settingsBindingSource, "NagEnabled", true));
            this.checkBoxEnableNag.Location = new System.Drawing.Point(211, 90);
            this.checkBoxEnableNag.Name = "checkBoxEnableNag";
            this.checkBoxEnableNag.Size = new System.Drawing.Size(82, 17);
            this.checkBoxEnableNag.TabIndex = 29;
            this.checkBoxEnableNag.Text = "Enable Nag";
            this.checkBoxEnableNag.UseVisualStyleBackColor = true;
            // 
            // checkBoxMonitorUserActivity
            // 
            this.checkBoxMonitorUserActivity.AutoSize = true;
            this.checkBoxMonitorUserActivity.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.settingsBindingSource, "MonitorUserActivity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxMonitorUserActivity.Location = new System.Drawing.Point(211, 34);
            this.checkBoxMonitorUserActivity.Name = "checkBoxMonitorUserActivity";
            this.checkBoxMonitorUserActivity.Size = new System.Drawing.Size(123, 17);
            this.checkBoxMonitorUserActivity.TabIndex = 28;
            this.checkBoxMonitorUserActivity.Text = "Monitor User Activity";
            this.checkBoxMonitorUserActivity.UseVisualStyleBackColor = true;
            // 
            // labelNagInterval
            // 
            this.labelNagInterval.AutoSize = true;
            this.labelNagInterval.Location = new System.Drawing.Point(20, 91);
            this.labelNagInterval.Name = "labelNagInterval";
            this.labelNagInterval.Size = new System.Drawing.Size(68, 13);
            this.labelNagInterval.TabIndex = 27;
            this.labelNagInterval.Text = "Nag Interval:";
            // 
            // numericUpDownNagInterval
            // 
            this.numericUpDownNagInterval.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.settingsBindingSource, "NagIntervalMinutes", true));
            this.numericUpDownNagInterval.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.settingsBindingSource, "NagEnabled", true));
            this.numericUpDownNagInterval.Location = new System.Drawing.Point(94, 89);
            this.numericUpDownNagInterval.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numericUpDownNagInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNagInterval.Name = "numericUpDownNagInterval";
            this.numericUpDownNagInterval.Size = new System.Drawing.Size(111, 20);
            this.numericUpDownNagInterval.TabIndex = 26;
            this.numericUpDownNagInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownAutoTimeOut
            // 
            this.numericUpDownAutoTimeOut.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.settingsBindingSource, "UserActivityIdleTimeoutMinutes", true));
            this.numericUpDownAutoTimeOut.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.settingsBindingSource, "MonitorUserActivity", true));
            this.numericUpDownAutoTimeOut.Location = new System.Drawing.Point(94, 33);
            this.numericUpDownAutoTimeOut.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numericUpDownAutoTimeOut.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAutoTimeOut.Name = "numericUpDownAutoTimeOut";
            this.numericUpDownAutoTimeOut.Size = new System.Drawing.Size(111, 20);
            this.numericUpDownAutoTimeOut.TabIndex = 25;
            this.numericUpDownAutoTimeOut.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelAutoTimeOut
            // 
            this.labelAutoTimeOut.AutoSize = true;
            this.labelAutoTimeOut.Location = new System.Drawing.Point(10, 35);
            this.labelAutoTimeOut.Name = "labelAutoTimeOut";
            this.labelAutoTimeOut.Size = new System.Drawing.Size(78, 13);
            this.labelAutoTimeOut.TabIndex = 24;
            this.labelAutoTimeOut.Text = "Auto Time Out:";
            // 
            // labelConfigurationPath
            // 
            this.labelConfigurationPath.AutoSize = true;
            this.labelConfigurationPath.Location = new System.Drawing.Point(12, 9);
            this.labelConfigurationPath.Name = "labelConfigurationPath";
            this.labelConfigurationPath.Size = new System.Drawing.Size(76, 13);
            this.labelConfigurationPath.TabIndex = 23;
            this.labelConfigurationPath.Text = "Mapping Path:";
            // 
            // tabPageMappings
            // 
            this.tabPageMappings.AutoScroll = true;
            this.tabPageMappings.Controls.Add(this.labelElapsed);
            this.tabPageMappings.Controls.Add(this.labelRemaining);
            this.tabPageMappings.Controls.Add(this.labelDuration);
            this.tabPageMappings.Controls.Add(this.comboBoxElapsed);
            this.tabPageMappings.Controls.Add(this.comboBoxRemaining);
            this.tabPageMappings.Controls.Add(this.comboBoxDuration);
            this.tabPageMappings.Controls.Add(this.labelWorkItemType);
            this.tabPageMappings.Controls.Add(this.comboBoxWorkItemType);
            this.tabPageMappings.Location = new System.Drawing.Point(4, 22);
            this.tabPageMappings.Name = "tabPageMappings";
            this.tabPageMappings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMappings.Size = new System.Drawing.Size(434, 175);
            this.tabPageMappings.TabIndex = 0;
            this.tabPageMappings.Text = "Mappings";
            this.tabPageMappings.UseVisualStyleBackColor = true;
            // 
            // labelElapsed
            // 
            this.labelElapsed.AutoSize = true;
            this.labelElapsed.Location = new System.Drawing.Point(41, 147);
            this.labelElapsed.Name = "labelElapsed";
            this.labelElapsed.Size = new System.Drawing.Size(48, 13);
            this.labelElapsed.TabIndex = 30;
            this.labelElapsed.Text = "Elapsed:";
            // 
            // labelRemaining
            // 
            this.labelRemaining.AutoSize = true;
            this.labelRemaining.Location = new System.Drawing.Point(29, 119);
            this.labelRemaining.Name = "labelRemaining";
            this.labelRemaining.Size = new System.Drawing.Size(60, 13);
            this.labelRemaining.TabIndex = 29;
            this.labelRemaining.Text = "Remaining:";
            // 
            // labelDuration
            // 
            this.labelDuration.AutoSize = true;
            this.labelDuration.Location = new System.Drawing.Point(39, 91);
            this.labelDuration.Name = "labelDuration";
            this.labelDuration.Size = new System.Drawing.Size(50, 13);
            this.labelDuration.TabIndex = 28;
            this.labelDuration.Text = "Duration:";
            // 
            // comboBoxElapsed
            // 
            this.comboBoxElapsed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxElapsed.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxElapsed.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxElapsed.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.workingItemConfigurationBindingSource, "ElapsedField", true));
            this.comboBoxElapsed.DataSource = this.fieldDefinitionsElapsedBindingSource;
            this.comboBoxElapsed.DisplayMember = "ReferenceName";
            this.comboBoxElapsed.FormattingEnabled = true;
            this.comboBoxElapsed.Location = new System.Drawing.Point(95, 144);
            this.comboBoxElapsed.Name = "comboBoxElapsed";
            this.comboBoxElapsed.Size = new System.Drawing.Size(249, 21);
            this.comboBoxElapsed.TabIndex = 27;
            this.comboBoxElapsed.ValueMember = "ReferenceName";
            // 
            // fieldDefinitionsElapsedBindingSource
            // 
            this.fieldDefinitionsElapsedBindingSource.DataMember = "FieldDefinitions";
            this.fieldDefinitionsElapsedBindingSource.DataSource = this.selectedWorkItemTypeBindingSource;
            this.fieldDefinitionsElapsedBindingSource.Sort = "ReferenceName";
            // 
            // selectedWorkItemTypeBindingSource
            // 
            this.selectedWorkItemTypeBindingSource.DataMember = "SelectedWorkItemType";
            this.selectedWorkItemTypeBindingSource.DataSource = this.workingItemConfigurationBindingSource;
            // 
            // comboBoxRemaining
            // 
            this.comboBoxRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxRemaining.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxRemaining.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxRemaining.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.workingItemConfigurationBindingSource, "RemainingField", true));
            this.comboBoxRemaining.DataSource = this.fieldDefinitionsRemainingBindingSource;
            this.comboBoxRemaining.DisplayMember = "ReferenceName";
            this.comboBoxRemaining.FormattingEnabled = true;
            this.comboBoxRemaining.Location = new System.Drawing.Point(95, 116);
            this.comboBoxRemaining.Name = "comboBoxRemaining";
            this.comboBoxRemaining.Size = new System.Drawing.Size(249, 21);
            this.comboBoxRemaining.TabIndex = 26;
            this.comboBoxRemaining.ValueMember = "ReferenceName";
            // 
            // fieldDefinitionsRemainingBindingSource
            // 
            this.fieldDefinitionsRemainingBindingSource.DataMember = "FieldDefinitions";
            this.fieldDefinitionsRemainingBindingSource.DataSource = this.selectedWorkItemTypeBindingSource;
            this.fieldDefinitionsRemainingBindingSource.Sort = "ReferenceName";
            // 
            // comboBoxDuration
            // 
            this.comboBoxDuration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDuration.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDuration.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDuration.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.workingItemConfigurationBindingSource, "DurationField", true));
            this.comboBoxDuration.DataSource = this.fieldDefinitionsDurationBindingSource;
            this.comboBoxDuration.DisplayMember = "ReferenceName";
            this.comboBoxDuration.FormattingEnabled = true;
            this.comboBoxDuration.Location = new System.Drawing.Point(95, 88);
            this.comboBoxDuration.Name = "comboBoxDuration";
            this.comboBoxDuration.Size = new System.Drawing.Size(249, 21);
            this.comboBoxDuration.TabIndex = 25;
            this.comboBoxDuration.ValueMember = "ReferenceName";
            // 
            // fieldDefinitionsDurationBindingSource
            // 
            this.fieldDefinitionsDurationBindingSource.DataMember = "FieldDefinitions";
            this.fieldDefinitionsDurationBindingSource.DataSource = this.selectedWorkItemTypeBindingSource;
            this.fieldDefinitionsDurationBindingSource.Sort = "ReferenceName";
            // 
            // labelWorkItemType
            // 
            this.labelWorkItemType.AutoSize = true;
            this.labelWorkItemType.Location = new System.Drawing.Point(3, 63);
            this.labelWorkItemType.Name = "labelWorkItemType";
            this.labelWorkItemType.Size = new System.Drawing.Size(86, 13);
            this.labelWorkItemType.TabIndex = 22;
            this.labelWorkItemType.Text = "Work Item Type:";
            // 
            // comboBoxWorkItemType
            // 
            this.comboBoxWorkItemType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxWorkItemType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxWorkItemType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxWorkItemType.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.workingItemConfigurationBindingSource, "SelectedWorkItemType", true));
            this.comboBoxWorkItemType.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.workingItemConfigurationBindingSource, "SelectedWorkItemTypeName", true));
            this.comboBoxWorkItemType.FormattingEnabled = true;
            this.comboBoxWorkItemType.Location = new System.Drawing.Point(95, 60);
            this.comboBoxWorkItemType.Name = "comboBoxWorkItemType";
            this.comboBoxWorkItemType.Size = new System.Drawing.Size(249, 21);
            this.comboBoxWorkItemType.TabIndex = 23;
            // 
            // tabPageUpdateWarehouse
            // 
            this.tabPageUpdateWarehouse.Controls.Add(this.labelWarehouseStatus);
            this.tabPageUpdateWarehouse.Controls.Add(this.labelUpdateDataWarehouse);
            this.tabPageUpdateWarehouse.Controls.Add(this.textBoxWarehouseStatus);
            this.tabPageUpdateWarehouse.Controls.Add(this.progressBarWarehouse);
            this.tabPageUpdateWarehouse.Controls.Add(this.buttonRefreshWarehouseStatus);
            this.tabPageUpdateWarehouse.Controls.Add(this.buttonUpdateWarehouse);
            this.tabPageUpdateWarehouse.Location = new System.Drawing.Point(4, 22);
            this.tabPageUpdateWarehouse.Name = "tabPageUpdateWarehouse";
            this.tabPageUpdateWarehouse.Size = new System.Drawing.Size(434, 175);
            this.tabPageUpdateWarehouse.TabIndex = 2;
            this.tabPageUpdateWarehouse.Text = "Update Warehouse";
            this.tabPageUpdateWarehouse.UseVisualStyleBackColor = true;
            // 
            // labelWarehouseStatus
            // 
            this.labelWarehouseStatus.AutoSize = true;
            this.labelWarehouseStatus.Location = new System.Drawing.Point(8, 46);
            this.labelWarehouseStatus.Name = "labelWarehouseStatus";
            this.labelWarehouseStatus.Size = new System.Drawing.Size(98, 13);
            this.labelWarehouseStatus.TabIndex = 5;
            this.labelWarehouseStatus.Text = "Warehouse Status:";
            // 
            // labelUpdateDataWarehouse
            // 
            this.labelUpdateDataWarehouse.AutoSize = true;
            this.labelUpdateDataWarehouse.Location = new System.Drawing.Point(8, 89);
            this.labelUpdateDataWarehouse.Name = "labelUpdateDataWarehouse";
            this.labelUpdateDataWarehouse.Size = new System.Drawing.Size(129, 13);
            this.labelUpdateDataWarehouse.TabIndex = 4;
            this.labelUpdateDataWarehouse.Text = "Update Data Warehouse:";
            // 
            // textBoxWarehouseStatus
            // 
            this.textBoxWarehouseStatus.Location = new System.Drawing.Point(11, 62);
            this.textBoxWarehouseStatus.Name = "textBoxWarehouseStatus";
            this.textBoxWarehouseStatus.ReadOnly = true;
            this.textBoxWarehouseStatus.Size = new System.Drawing.Size(334, 20);
            this.textBoxWarehouseStatus.TabIndex = 3;
            // 
            // progressBarWarehouse
            // 
            this.progressBarWarehouse.Location = new System.Drawing.Point(11, 105);
            this.progressBarWarehouse.MarqueeAnimationSpeed = 0;
            this.progressBarWarehouse.Name = "progressBarWarehouse";
            this.progressBarWarehouse.Size = new System.Drawing.Size(334, 23);
            this.progressBarWarehouse.Step = 1;
            this.progressBarWarehouse.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarWarehouse.TabIndex = 2;
            // 
            // buttonRefreshWarehouseStatus
            // 
            this.buttonRefreshWarehouseStatus.Location = new System.Drawing.Point(351, 60);
            this.buttonRefreshWarehouseStatus.Name = "buttonRefreshWarehouseStatus";
            this.buttonRefreshWarehouseStatus.Size = new System.Drawing.Size(75, 23);
            this.buttonRefreshWarehouseStatus.TabIndex = 0;
            this.buttonRefreshWarehouseStatus.Text = "Refresh";
            this.buttonRefreshWarehouseStatus.UseVisualStyleBackColor = true;
            this.buttonRefreshWarehouseStatus.Click += new System.EventHandler(this.buttonRefreshWarehouseStatus_Click);
            // 
            // buttonUpdateWarehouse
            // 
            this.buttonUpdateWarehouse.Location = new System.Drawing.Point(351, 105);
            this.buttonUpdateWarehouse.Name = "buttonUpdateWarehouse";
            this.buttonUpdateWarehouse.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdateWarehouse.TabIndex = 1;
            this.buttonUpdateWarehouse.Text = "Update";
            this.buttonUpdateWarehouse.UseVisualStyleBackColor = true;
            this.buttonUpdateWarehouse.Click += new System.EventHandler(this.buttonUpdateWarehouse_Click);
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.Controls.Add(this.labelVersion);
            this.tabPageAbout.Controls.Add(this.pictureBoxAbout);
            this.tabPageAbout.Controls.Add(this.linkLabelAbout);
            this.tabPageAbout.Controls.Add(this.labelCopyright);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAbout.Size = new System.Drawing.Size(434, 175);
            this.tabPageAbout.TabIndex = 3;
            this.tabPageAbout.Text = "About";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(387, 157);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(0, 13);
            this.labelVersion.TabIndex = 3;
            // 
            // pictureBoxAbout
            // 
            this.pictureBoxAbout.Image = global::Rowan.TfsWorkingOn.WinForm.Properties.Resources.tfsworkingon;
            this.pictureBoxAbout.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxAbout.Name = "pictureBoxAbout";
            this.pictureBoxAbout.Size = new System.Drawing.Size(438, 137);
            this.pictureBoxAbout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxAbout.TabIndex = 2;
            this.pictureBoxAbout.TabStop = false;
            // 
            // linkLabelAbout
            // 
            this.linkLabelAbout.AutoSize = true;
            this.linkLabelAbout.Location = new System.Drawing.Point(114, 157);
            this.linkLabelAbout.Name = "linkLabelAbout";
            this.linkLabelAbout.Size = new System.Drawing.Size(160, 13);
            this.linkLabelAbout.TabIndex = 1;
            this.linkLabelAbout.TabStop = true;
            this.linkLabelAbout.Text = "http://mgrowan.wordpress.com/";
            this.linkLabelAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAbout_LinkClicked);
            // 
            // labelCopyright
            // 
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.Location = new System.Drawing.Point(120, 140);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(171, 13);
            this.labelCopyright.TabIndex = 0;
            this.labelCopyright.Text = "Copyright © 2011 Matthew Rowan";
            // 
            // toolTipHelp
            // 
            this.toolTipHelp.AutoPopDelay = 20000;
            this.toolTipHelp.InitialDelay = 300;
            this.toolTipHelp.IsBalloon = true;
            this.toolTipHelp.ReshowDelay = 100;
            this.toolTipHelp.Tag = "";
            this.toolTipHelp.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipHelp.ToolTipTitle = "Configuration Help";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(193, 212);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 20;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // FormWorkItemConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(444, 250);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControlConfiguration);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWorkItemConfiguration";
            this.Text = "TFS Working On... Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormWorkItemConfiguration_FormClosing);
            this.Load += new System.EventHandler(this.FormWorkItemConfiguration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.workingItemConfigurationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingsBindingSource)).EndInit();
            this.tabControlConfiguration.ResumeLayout(false);
            this.tabPageOptions.ResumeLayout(false);
            this.tabPageOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelpMenuQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelpPromptOnResume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelpNag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelpUserActivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNagInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoTimeOut)).EndInit();
            this.tabPageMappings.ResumeLayout(false);
            this.tabPageMappings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fieldDefinitionsElapsedBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedWorkItemTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fieldDefinitionsRemainingBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fieldDefinitionsDurationBindingSource)).EndInit();
            this.tabPageUpdateWarehouse.ResumeLayout(false);
            this.tabPageUpdateWarehouse.PerformLayout();
            this.tabPageAbout.ResumeLayout(false);
            this.tabPageAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAbout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button buttonSetDirectory;
        private System.Windows.Forms.TextBox textBoxCurrentDirectory;
        private System.Windows.Forms.TabControl tabControlConfiguration;
        private System.Windows.Forms.TabPage tabPageMappings;
        private System.Windows.Forms.TabPage tabPageOptions;
        private System.Windows.Forms.Label labelWorkItemType;
        private System.Windows.Forms.ComboBox comboBoxWorkItemType;
        private System.Windows.Forms.Label labelElapsed;
        private System.Windows.Forms.Label labelRemaining;
        private System.Windows.Forms.Label labelDuration;
        private System.Windows.Forms.ComboBox comboBoxElapsed;
        private System.Windows.Forms.ComboBox comboBoxRemaining;
        private System.Windows.Forms.ComboBox comboBoxDuration;
        private System.Windows.Forms.TabPage tabPageUpdateWarehouse;
        private System.Windows.Forms.Label labelConfigurationPath;
        private System.Windows.Forms.TabPage tabPageAbout;
        private System.Windows.Forms.LinkLabel linkLabelAbout;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.PictureBox pictureBoxAbout;
        private System.Windows.Forms.Label labelNagInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownNagInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownAutoTimeOut;
        private System.Windows.Forms.Label labelAutoTimeOut;
        private System.Windows.Forms.BindingSource workingItemConfigurationBindingSource;
        private System.Windows.Forms.BindingSource selectedWorkItemTypeBindingSource;
        private System.Windows.Forms.BindingSource fieldDefinitionsDurationBindingSource;
        private System.Windows.Forms.BindingSource fieldDefinitionsRemainingBindingSource;
        private System.Windows.Forms.BindingSource fieldDefinitionsElapsedBindingSource;
        private System.Windows.Forms.BindingSource settingsBindingSource;
        private System.Windows.Forms.CheckBox checkBoxMonitorUserActivity;
        private System.Windows.Forms.CheckBox checkBoxEnableNag;
        private System.Windows.Forms.PictureBox pictureBoxHelpUserActivity;
        private System.Windows.Forms.ToolTip toolTipHelp;
        private System.Windows.Forms.PictureBox pictureBoxHelpNag;
        private System.Windows.Forms.PictureBox pictureBoxHelpPromptOnResume;
        private System.Windows.Forms.CheckBox checkBoxPromptOnResume;
        private System.Windows.Forms.Label labelUpdateDataWarehouse;
        private System.Windows.Forms.TextBox textBoxWarehouseStatus;
        private System.Windows.Forms.ProgressBar progressBarWarehouse;
        private System.Windows.Forms.Button buttonRefreshWarehouseStatus;
        private System.Windows.Forms.Button buttonUpdateWarehouse;
        private System.Windows.Forms.Label labelWarehouseStatus;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.PictureBox pictureBoxHelpMenuQuery;
        private System.Windows.Forms.Label labelMenuQuery;
        private System.Windows.Forms.Button buttonOK;
    }
}

