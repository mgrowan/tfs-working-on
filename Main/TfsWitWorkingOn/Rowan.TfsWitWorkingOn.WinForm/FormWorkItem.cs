using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Controls;
using System.Diagnostics;
using System.Collections;
using System.IO;

namespace Rowan.TfsWitWorkingOn.WinForm
{
    public partial class FormWorkItem : Form
    {
        private bool _isCanceled;

        public FormWorkItem()
        {
            InitializeComponent();
        }

        public FormWorkItem(WorkItem workItem) : this()
        {
            if (workItem != null)
            {
                Text = string.Format("Work Item {0}: {1}", workItem.Id, workItem.Title);
                witControl.Item = workItem;
            }
        }

        private bool Save()
        {
            ArrayList badFields = witControl.Item.Validate();
            if (!witControl.Item.IsValid())
            {
                using (StringWriter sw = new StringWriter())
                {
                    sw.WriteLine("The following fields are invalid: ");
                    foreach (Field f in badFields)
                    {
                        if (!f.IsValid)
                        {
                            sw.WriteLine(f.Name);
                        }
                    }

                    MessageBox.Show(sw.ToString(), "Invalid fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            if (witControl.Item.IsDirty) witControl.Item.Save();
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (witControl.Item.IsDirty || !witControl.Item.IsValid())
            {
                witControl.Item.Reset();
            }

            _isCanceled = true;
            Close();
        }

        private void FormWorkItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (witControl.Item.IsDirty && !_isCanceled)
            {
                switch (MessageBox.Show("There are outstanding changes. Would you like to save your changes?", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        e.Cancel = !Save();
                        break;
                    case DialogResult.No:
                        if (!witControl.Item.IsValid())
                        {
                            witControl.Item.Reset();
                        }
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }
            }
        }
    }
}
