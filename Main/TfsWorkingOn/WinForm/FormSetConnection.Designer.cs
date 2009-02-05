namespace Rowan.TfsWorkingOn.WinForm
{
    partial class FormSetConnection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_workingItem")]
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetConnection));
            this.labelProject = new System.Windows.Forms.Label();
            this.labelServer = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.comboBoxProjects = new System.Windows.Forms.ComboBox();
            this.connectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.projectsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonOK = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.selectWorkItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queryListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyEstimatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxTfsServers = new System.Windows.Forms.ComboBox();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.connectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectsBindingSource)).BeginInit();
            this.notifyMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelProject
            // 
            this.labelProject.AutoSize = true;
            this.labelProject.Location = new System.Drawing.Point(7, 43);
            this.labelProject.Name = "labelProject";
            this.labelProject.Size = new System.Drawing.Size(43, 13);
            this.labelProject.TabIndex = 8;
            this.labelProject.Text = "Project:";
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Location = new System.Drawing.Point(9, 17);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(41, 13);
            this.labelServer.TabIndex = 5;
            this.labelServer.Text = "Server:";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConnect.Location = new System.Drawing.Point(298, 12);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 7;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // comboBoxProjects
            // 
            this.comboBoxProjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxProjects.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.connectionBindingSource, "SelectedProject", true));
            this.comboBoxProjects.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.connectionBindingSource, "IsConnected", true));
            this.comboBoxProjects.DataSource = this.projectsBindingSource;
            this.comboBoxProjects.DisplayMember = "Name";
            this.comboBoxProjects.FormattingEnabled = true;
            this.comboBoxProjects.Location = new System.Drawing.Point(63, 40);
            this.comboBoxProjects.Name = "comboBoxProjects";
            this.comboBoxProjects.Size = new System.Drawing.Size(229, 21);
            this.comboBoxProjects.TabIndex = 9;
            this.comboBoxProjects.ValueMember = "Id";
            // 
            // connectionBindingSource
            // 
            this.connectionBindingSource.DataSource = typeof(Rowan.TfsWorkingOn.Connection);
            // 
            // projectsBindingSource
            // 
            this.projectsBindingSource.DataMember = "Projects";
            this.projectsBindingSource.DataSource = this.connectionBindingSource;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.connectionBindingSource, "IsConnected", true));
            this.buttonOK.Location = new System.Drawing.Point(298, 40);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 10;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipTitle = "TFS Working On";
            this.notifyIcon.ContextMenuStrip = this.notifyMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "TFS Working On";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // notifyMenu
            // 
            this.notifyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.toolStripMenuItem2,
            this.selectWorkItemToolStripMenuItem,
            this.queryListToolStripMenuItem,
            this.selectedToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripConnect,
            this.configureToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem});
            this.notifyMenu.Name = "notifyMenu";
            this.notifyMenu.Size = new System.Drawing.Size(173, 198);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.startToolStripMenuItem.Text = "&Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(169, 6);
            // 
            // selectWorkItemToolStripMenuItem
            // 
            this.selectWorkItemToolStripMenuItem.Name = "selectWorkItemToolStripMenuItem";
            this.selectWorkItemToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.selectWorkItemToolStripMenuItem.Text = "Select &Work Item...";
            this.selectWorkItemToolStripMenuItem.Click += new System.EventHandler(this.selectWorkItemToolStripMenuItem_Click);
            // 
            // queryListToolStripMenuItem
            // 
            this.queryListToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.workItemsToolStripMenuItem});
            this.queryListToolStripMenuItem.Name = "queryListToolStripMenuItem";
            this.queryListToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.queryListToolStripMenuItem.Text = "Query List";
            this.queryListToolStripMenuItem.DropDownOpening += new System.EventHandler(this.queryListToolStripMenuItem_DropDownOpening);
            // 
            // workItemsToolStripMenuItem
            // 
            this.workItemsToolStripMenuItem.Name = "workItemsToolStripMenuItem";
            this.workItemsToolStripMenuItem.Size = new System.Drawing.Size(392, 22);
            this.workItemsToolStripMenuItem.Text = "Set Menu Query in Configuration for quick work item access";
            // 
            // selectedToolStripMenuItem
            // 
            this.selectedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem,
            this.modifyEstimatesToolStripMenuItem,
            this.toolStripMenuItem4,
            this.cancelToolStripMenuItem});
            this.selectedToolStripMenuItem.Enabled = false;
            this.selectedToolStripMenuItem.Name = "selectedToolStripMenuItem";
            this.selectedToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.selectedToolStripMenuItem.Text = "S&elected: ";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.viewToolStripMenuItem.Text = "&View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // modifyEstimatesToolStripMenuItem
            // 
            this.modifyEstimatesToolStripMenuItem.Name = "modifyEstimatesToolStripMenuItem";
            this.modifyEstimatesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.modifyEstimatesToolStripMenuItem.Text = "&Estimates";
            this.modifyEstimatesToolStripMenuItem.Click += new System.EventHandler(this.modifyEstimatesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(169, 6);
            // 
            // toolStripConnect
            // 
            this.toolStripConnect.Name = "toolStripConnect";
            this.toolStripConnect.Size = new System.Drawing.Size(172, 22);
            this.toolStripConnect.Text = "&Connect...";
            this.toolStripConnect.Click += new System.EventHandler(this.toolStripConnect_Click);
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.configureToolStripMenuItem.Text = "Co&nfigure...";
            this.configureToolStripMenuItem.Click += new System.EventHandler(this.configureToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(169, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // comboBoxTfsServers
            // 
            this.comboBoxTfsServers.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.connectionBindingSource, "Server", true));
            this.comboBoxTfsServers.FormattingEnabled = true;
            this.comboBoxTfsServers.Location = new System.Drawing.Point(63, 14);
            this.comboBoxTfsServers.Name = "comboBoxTfsServers";
            this.comboBoxTfsServers.Size = new System.Drawing.Size(229, 21);
            this.comboBoxTfsServers.TabIndex = 11;
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(149, 6);
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cancelToolStripMenuItem.Text = "&Cancel";
            this.cancelToolStripMenuItem.Click += new System.EventHandler(this.cancelToolStripMenuItem_Click);
            // 
            // FormSetConnection
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 75);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxTfsServers);
            this.Controls.Add(this.labelProject);
            this.Controls.Add(this.labelServer);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.comboBoxProjects);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSetConnection";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Set Connection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSetConnection_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.connectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectsBindingSource)).EndInit();
            this.notifyMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelProject;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.ComboBox comboBoxProjects;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.BindingSource connectionBindingSource;
        private System.Windows.Forms.BindingSource projectsBindingSource;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyMenu;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem selectWorkItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripConnect;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyEstimatesToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxTfsServers;
        private System.Windows.Forms.ToolStripMenuItem queryListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem workItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;

    }
}