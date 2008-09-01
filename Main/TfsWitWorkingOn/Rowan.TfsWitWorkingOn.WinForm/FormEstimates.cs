using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rowan.TfsWitWorkingOn.WinForm
{
    public partial class FormEstimates : Form
    {
        private WorkingItem _workingItem = null;

        public FormEstimates(WorkingItem workingItem)
        {
            InitializeComponent();

            _workingItem = workingItem;
            estimatesBindingSource.DataSource = workingItem.Estimates;
        }

        private void buttonViewWorkItem_Click(object sender, EventArgs e)
        {
            FormWorkItem formWorkItem = new FormWorkItem(_workingItem.WorkItem);
            formWorkItem.Show();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _workingItem.UpdateWorkItem();
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
