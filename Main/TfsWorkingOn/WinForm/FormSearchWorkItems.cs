using System;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Controls;
using Rowan.TfsWorkingOn.WinForm.Properties;

namespace Rowan.TfsWorkingOn.WinForm
{
    public partial class FormSearchWorkItems : Form
    {
        private PickWorkItemsControl pickWorkItemsControl;
        public WorkingItem WorkingItem { get; private set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public FormSearchWorkItems(WorkItemStore workItemStore, string projectName)
        {
            InitializeComponent();

            WorkingItem = new WorkingItem();

            pickWorkItemsControl = new PickWorkItemsControl(workItemStore, false);
            pickWorkItemsControl.Dock = DockStyle.Fill;
            pickWorkItemsControl.PortfolioDisplayName = projectName;
            pickWorkItemsControl.PickWorkItemsDoubleClicked += new PickWorkItemsDoubleClickedEventHandler(pickWorkItemsControl_PickWorkItemsListViewDoubleClicked);

            // Add context menu to view the work item when trying to pick from the query
            try
            {
                // Dirty hack - this will continue to work as long as the TFS control has not been updated by Microsoft.
                ((DataGridView)(pickWorkItemsControl.Controls[0].Controls[9].Controls[0])).MouseUp += new MouseEventHandler(FormSearchWorkItems_MouseUp);
            }
            catch (Exception)
            {
                // Let this go!!
                // The only implication is the context menu wont display
            }

            Controls.Add(pickWorkItemsControl);
            SetClientSizeCore(pickWorkItemsControl.PreferredSize.Width, pickWorkItemsControl.PreferredSize.Height);
            Text = string.Format(CultureInfo.CurrentCulture, Resources.SearchForWorkItemsIn, projectName);
        }

        void FormSearchWorkItems_MouseUp(object sender, MouseEventArgs e)
        {
            // Show context menu when right clicking items in the grid.
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                workitemMenuStrip.Show(Cursor.Position);
        }

        void pickWorkItemsControl_PickWorkItemsListViewDoubleClicked(object sender, EventArgs e)
        {
            PickWorkItem((pickWorkItemsControl.SelectedWorkItems()[0] as WorkItem));
            Close();
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PickWorkItem((pickWorkItemsControl.SelectedWorkItems()[0] as WorkItem));
            Close();
        }

        private void PickWorkItem(WorkItem workItem)
        {
            // TODO: Check if assigned to user
            try
            {
                WorkingItem.WorkItem = workItem;
                workItem.Open();

            }
            catch (ItemAlreadyUpdatedOnServerException)
            {
                workItem = Connection.GetConnection().WorkItemStore.GetWorkItem(workItem.Id);
                workItem.Open();
            }
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pickWorkItemsControl.SelectedWorkItems().Count > 0)
            {
                var workItem = (pickWorkItemsControl.SelectedWorkItems()[0] as WorkItem);
                FormWorkItem formWorkItem = new FormWorkItem(workItem);
                formWorkItem.ShowDialog(this);
            }
            else
            {
                MessageBox.Show(@"Please select a work item to view", @"No Work Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FormSearchWorkItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Escape)
                this.Close();
        }
    }
}
