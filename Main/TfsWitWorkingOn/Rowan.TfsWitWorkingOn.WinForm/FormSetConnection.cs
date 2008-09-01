using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rowan.TfsWitWorkingOn.WinForm.Properties;

namespace Rowan.TfsWitWorkingOn.WinForm
{
    public partial class FormSetConnection : Form
    {
        public Connection connection = new Connection();
        public WorkingItem WorkingItem = null;

        private bool _started = false;
        private bool _exiting = false;
        private const string NotifyIconText = "TFS Working On...";

        public FormSetConnection()
        {
            InitializeComponent();

            connectionBindingSource.DataSource = connection;

            connection.Server = Settings.Default.TFSServer;
            connection.Port = Settings.Default.Port;
            try
            {
                if (!string.IsNullOrEmpty(connection.Server))
                {
                    connection.Connect();
                }
                else
                {
                    Show();
                    return;
                }
            }
            catch (Exception)
            {
                Show();
                return;
            }

            if (connection.IsConnected && Settings.Default.SelectedProjectId != -1 && !string.IsNullOrEmpty(Settings.Default.SelectedProjectName))
            {
                connection.SelectedProjectId = Settings.Default.SelectedProjectId;
                connection.SelectedProjectName = Settings.Default.SelectedProjectName;
                Hide();
            }
            else
            {
                Show();
            }
        }

        private void ShowSearchForm()
        {
            if (_started)
            {
                if (MessageBox.Show(string.Format("Do you wish to stop working on Work Item: {0}?", WorkingItem.WorkItem.Title), "Stop current Work Item", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    StartStop();
                }
                else
                {
                    return;
                }
            }

            FormSearchWorkItems formSearchWorkItems = new FormSearchWorkItems(connection.WorkItemStore, connection.SelectedProjectName);
            formSearchWorkItems.WorkingItem.PropertyChanged += new PropertyChangedEventHandler(formSearchWorkItems_WorkingItem_PropertyChanged);
            formSearchWorkItems.Show();
        }

        private void StartStop()
        {
            if (WorkingItem == null)
            {
                ShowSearchForm();
                return;
            }

            if (_started)
            {
                WorkingItem.StopTime = DateTime.Now;
                startToolStripMenuItem.Text = "Start";
                notifyIcon.Text = NotifyIconText;
                notifyIcon.BalloonTipText = "Stopped working on " + WorkingItem.WorkItem.Id.ToString();
            }
            else
            {
                WorkingItem.StartTime = DateTime.Now;
                startToolStripMenuItem.Text = "Stop";
                notifyIcon.Text = NotifyIconText + WorkingItem.WorkItem.Id.ToString();
                notifyIcon.BalloonTipText = "Started working on " + WorkingItem.WorkItem.Id.ToString();
            }
            notifyIcon.ShowBalloonTip(3000);
            _started = !_started;
        }

        #region Events
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Failed to connection: {0}", ex.Message));
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Settings.Default.TFSServer = connection.Server;
            Settings.Default.Port = connection.Port;
            Settings.Default.SelectedProjectId = connection.SelectedProjectId;
            Settings.Default.SelectedProjectName = connection.SelectedProjectName;
            Settings.Default.Save();
            Hide();

            ShowSearchForm();
        }

        private void FormSetConnection_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !_exiting;
            Hide();
        }

        private void formSearchWorkItems_WorkingItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "WorkItem")
            {
                WorkingItem = sender as WorkingItem;
                if (WorkingItem != null)
                {
                    WorkingItem.Connection = connection;
                    selectedToolStripMenuItem.Text = string.Format("Selected: {0}", WorkingItem.WorkItem.Id);
                    selectedToolStripMenuItem.Enabled = true;
                    StartStop();
                }
            }
        }

        #endregion events

        #region System Tray Menu Clicks
        private void toolStripConnect_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void selectWorkItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSearchForm();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _exiting = true;
            Application.Exit();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormWorkItem formWorkItem = new FormWorkItem(WorkingItem.WorkItem);
            formWorkItem.Show();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartStop();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            StartStop();
        }

        private void configureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormWorkItemConfiguration formWorkItemConfiguration = new FormWorkItemConfiguration();
            formWorkItemConfiguration.Show();
        }

        private void modifyEstimatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEstimates formEstimates = new FormEstimates(WorkingItem);
            formEstimates.Show();
        }
        #endregion





    }
}
