using System;
using System.Windows.Forms;

namespace Rowan.TfsWorkingOn.WinForm
{
    public partial class FormEstimates : Form
    {
        private WorkingItem _workingItem = null;

        public FormEstimates(WorkingItem workingItem)
        {
            InitializeComponent();

            _workingItem = workingItem;
            workingItem.UpdateWorkingOnEstimates();
            estimatesBindingSource.DataSource = workingItem.Estimates;
        }

        private void buttonViewWorkItem_Click(object sender, EventArgs e)
        {
            FormWorkItem formWorkItem = new FormWorkItem(_workingItem.WorkItem);
            formWorkItem.Show();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _workingItem.UpdateWorkItem(true);
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
