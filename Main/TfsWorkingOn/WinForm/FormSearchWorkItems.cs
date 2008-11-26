using System;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Controls;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Rowan.TfsWorkingOn.WinForm.Properties;

namespace Rowan.TfsWorkingOn.WinForm
{
    public partial class FormSearchWorkItems : Form
    {
        private PickWorkItemsControl pickWorkItemsControl;
        public WorkingItem WorkingItem { get; private set; }

        public FormSearchWorkItems(WorkItemStore workItemStore, string projectName)
        {
            InitializeComponent();

            WorkingItem = new WorkingItem();
                        
            pickWorkItemsControl = new PickWorkItemsControl(workItemStore, false);
            pickWorkItemsControl.Dock = DockStyle.Fill;
            pickWorkItemsControl.PortfolioDisplayName = projectName;
            pickWorkItemsControl.SelectListViewLabel = Resources.DoubleClickToSelect;
            pickWorkItemsControl.PickWorkItemsListViewDoubleClicked += new PickWorkItemsListViewDoubleClickedEventHandler(pickWorkItemsControl_PickWorkItemsListViewDoubleClicked);
            
            Controls.Add(pickWorkItemsControl);
            SetClientSizeCore(pickWorkItemsControl.PreferredSize.Width, pickWorkItemsControl.PreferredSize.Height);
            Text = string.Format(CultureInfo.CurrentCulture, Resources.SearchForWorkItemsIn, projectName);
        }

        void pickWorkItemsControl_PickWorkItemsListViewDoubleClicked(object sender, EventArgs e)
        {
            // TODO: Check if assigned to user
            WorkingItem.WorkItem = (pickWorkItemsControl.SelectedWorkItems()[0] as WorkItem);
            Close();
        }
    }
}
