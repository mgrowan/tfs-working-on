namespace Rowan.TfsWorkingOn.WinForm
{
    partial class FormEstimates
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEstimates));
            this.buttonViewWorkItem = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelDuration = new System.Windows.Forms.Label();
            this.labelElaspedTime = new System.Windows.Forms.Label();
            this.labelRemainingTime = new System.Windows.Forms.Label();
            this.textBoxDuration = new System.Windows.Forms.TextBox();
            this.estimatesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.textBoxElapsedTime = new System.Windows.Forms.TextBox();
            this.textBoxRemainingTime = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.estimatesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonViewWorkItem
            // 
            this.buttonViewWorkItem.Location = new System.Drawing.Point(12, 84);
            this.buttonViewWorkItem.Name = "buttonViewWorkItem";
            this.buttonViewWorkItem.Size = new System.Drawing.Size(75, 23);
            this.buttonViewWorkItem.TabIndex = 0;
            this.buttonViewWorkItem.Text = "View";
            this.buttonViewWorkItem.UseVisualStyleBackColor = true;
            this.buttonViewWorkItem.Click += new System.EventHandler(this.buttonViewWorkItem_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(107, 84);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(188, 84);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelDuration
            // 
            this.labelDuration.AutoSize = true;
            this.labelDuration.Location = new System.Drawing.Point(9, 9);
            this.labelDuration.Name = "labelDuration";
            this.labelDuration.Size = new System.Drawing.Size(53, 13);
            this.labelDuration.TabIndex = 3;
            this.labelDuration.Text = "Duration: ";
            // 
            // labelElaspedTime
            // 
            this.labelElaspedTime.AutoSize = true;
            this.labelElaspedTime.Location = new System.Drawing.Point(9, 61);
            this.labelElaspedTime.Name = "labelElaspedTime";
            this.labelElaspedTime.Size = new System.Drawing.Size(77, 13);
            this.labelElaspedTime.TabIndex = 4;
            this.labelElaspedTime.Text = "Elapsed Time: ";
            // 
            // labelRemainingTime
            // 
            this.labelRemainingTime.AutoSize = true;
            this.labelRemainingTime.Location = new System.Drawing.Point(9, 35);
            this.labelRemainingTime.Name = "labelRemainingTime";
            this.labelRemainingTime.Size = new System.Drawing.Size(89, 13);
            this.labelRemainingTime.TabIndex = 5;
            this.labelRemainingTime.Text = "Remaining Time: ";
            // 
            // textBoxDuration
            // 
            this.textBoxDuration.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.estimatesBindingSource, "ElapsedTime", true));
            this.textBoxDuration.Location = new System.Drawing.Point(107, 58);
            this.textBoxDuration.Name = "textBoxDuration";
            this.textBoxDuration.Size = new System.Drawing.Size(156, 20);
            this.textBoxDuration.TabIndex = 9;
            // 
            // estimatesBindingSource
            // 
            this.estimatesBindingSource.DataSource = typeof(Rowan.TfsWorkingOn.Estimates);
            // 
            // textBoxElapsedTime
            // 
            this.textBoxElapsedTime.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.estimatesBindingSource, "Duration", true));
            this.textBoxElapsedTime.Location = new System.Drawing.Point(107, 6);
            this.textBoxElapsedTime.Name = "textBoxElapsedTime";
            this.textBoxElapsedTime.Size = new System.Drawing.Size(156, 20);
            this.textBoxElapsedTime.TabIndex = 10;
            // 
            // textBoxRemainingTime
            // 
            this.textBoxRemainingTime.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.estimatesBindingSource, "RemainingTime", true));
            this.textBoxRemainingTime.Location = new System.Drawing.Point(107, 32);
            this.textBoxRemainingTime.Name = "textBoxRemainingTime";
            this.textBoxRemainingTime.Size = new System.Drawing.Size(156, 20);
            this.textBoxRemainingTime.TabIndex = 11;
            // 
            // FormEstimates
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(271, 113);
            this.Controls.Add(this.textBoxRemainingTime);
            this.Controls.Add(this.textBoxElapsedTime);
            this.Controls.Add(this.textBoxDuration);
            this.Controls.Add(this.labelRemainingTime);
            this.Controls.Add(this.labelElaspedTime);
            this.Controls.Add(this.labelDuration);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonViewWorkItem);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormEstimates";
            this.Text = "Estimates";
            ((System.ComponentModel.ISupportInitialize)(this.estimatesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonViewWorkItem;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelDuration;
        private System.Windows.Forms.Label labelElaspedTime;
        private System.Windows.Forms.Label labelRemainingTime;
        private System.Windows.Forms.TextBox textBoxDuration;
        private System.Windows.Forms.TextBox textBoxElapsedTime;
        private System.Windows.Forms.TextBox textBoxRemainingTime;
        private System.Windows.Forms.BindingSource estimatesBindingSource;
    }
}