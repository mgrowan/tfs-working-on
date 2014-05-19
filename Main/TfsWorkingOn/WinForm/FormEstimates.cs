using System;
using System.Windows.Forms;

namespace Rowan.TfsWorkingOn.WinForm
{
    public partial class FormEstimates : Form
    {
        private WorkingItem _workingItem;

        public FormEstimates(WorkingItem workingItem)
        {
            InitializeComponent();

            _workingItem = workingItem;
            estimatesBindingSource.DataSource = workingItem.Estimates;
            workingItem.UpdateWorkingOnEstimates();
        }

        private void buttonViewWorkItem_Click(object sender, EventArgs e)
        {
            var formWorkItem = new FormWorkItem(_workingItem.WorkItem);
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
