namespace Rowan.TfsWitWorkingOn.WinForm
{
    partial class FormWorkItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWorkItem));
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlWICtl = new System.Windows.Forms.Panel();
            this.witControl = new Microsoft.TeamFoundation.WorkItemTracking.Controls.WorkItemFormControl();
            this.pnlButtons.SuspendLayout();
            this.pnlWICtl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 560);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(801, 48);
            this.pnlButtons.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(723, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(642, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlWICtl
            // 
            this.pnlWICtl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlWICtl.Controls.Add(this.witControl);
            this.pnlWICtl.Location = new System.Drawing.Point(0, 0);
            this.pnlWICtl.Name = "pnlWICtl";
            this.pnlWICtl.Size = new System.Drawing.Size(801, 559);
            this.pnlWICtl.TabIndex = 5;
            // 
            // witControl
            // 
            this.witControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.witControl.FormDefinition = null;
            this.witControl.Item = null;
            this.witControl.LayoutTargetName = "";
            this.witControl.Location = new System.Drawing.Point(0, 0);
            this.witControl.Name = "witControl";
            this.witControl.ReadOnly = false;
            this.witControl.ServiceProvider = null;
            this.witControl.Size = new System.Drawing.Size(801, 559);
            this.witControl.TabIndex = 0;
            this.witControl.UserActionRequiredBackColor = System.Drawing.SystemColors.Info;
            this.witControl.UserActionRequiredForeColor = System.Drawing.SystemColors.InfoText;
            // 
            // FormWorkItem
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(801, 608);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlWICtl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormWorkItem";
            this.Text = "Work Item";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormWorkItem_FormClosing);
            this.pnlButtons.ResumeLayout(false);
            this.pnlWICtl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlButtons;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Panel pnlWICtl;
        private Microsoft.TeamFoundation.WorkItemTracking.Controls.WorkItemFormControl witControl;

    }
}