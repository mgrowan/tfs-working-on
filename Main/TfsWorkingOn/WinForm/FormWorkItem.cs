﻿using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Rowan.TfsWorkingOn.WinForm.Properties;

namespace Rowan.TfsWorkingOn.WinForm
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
                Text = string.Format(CultureInfo.CurrentCulture, Resources.WorkItemTitle, workItem.Id, workItem.Title);
                witControl.Item = workItem;
            }
        }

        private bool Save()
        {
            ArrayList badFields = witControl.Item.Validate();
            if (!witControl.Item.IsValid())
            {
                using (StringWriter sw = new StringWriter(CultureInfo.CurrentCulture))
                {
                    sw.WriteLine(Resources.FollowingFieldsInvalid);
                    foreach (Field f in badFields)
                    {
                        if (!f.IsValid)
                        {
                            sw.WriteLine(f.Name);
                        }
                    }

                    MessageBox.Show(sw.ToString(), Resources.InvalidFields, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                switch (MessageBox.Show(Resources.OutstandingChanges, Resources.SaveChanges, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
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