using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.TeamFoundation.Controls;

namespace Rowan.TfsWitWorkingOn.WinForm
{
    public partial class FormSearchWorkItems : Form
    {
        private PickWorkItemsControl pickWorkItemsControl = null;
        public WorkingItem WorkingItem { get; private set; }

        public FormSearchWorkItems(WorkItemStore workItemStore, string projectName)
        {
            InitializeComponent();

            WorkingItem = new WorkingItem();
                        
            pickWorkItemsControl = new PickWorkItemsControl(workItemStore, false);
            pickWorkItemsControl.Dock = DockStyle.Fill;
            pickWorkItemsControl.PortfolioDisplayName = projectName;
            pickWorkItemsControl.SelectListViewLabel = "Double click the work item you are working on...";
            pickWorkItemsControl.PickWorkItemsListViewDoubleClicked += new PickWorkItemsListViewDoubleClickedEventHandler(pickWorkItemsControl_PickWorkItemsListViewDoubleClicked);
            
            Controls.Add(pickWorkItemsControl);
            SetClientSizeCore(pickWorkItemsControl.PreferredSize.Width, pickWorkItemsControl.PreferredSize.Height);
            Text = string.Format("Search for Work Items in {0}", projectName);
        }

        void pickWorkItemsControl_PickWorkItemsListViewDoubleClicked(object sender, EventArgs e)
        {
            // TODO: Check if assigned to user
            WorkingItem.WorkItem = (pickWorkItemsControl.SelectedWorkItems()[0] as WorkItem);
            Close();
        }
    }
}
