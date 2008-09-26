using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Client;
using Rowan.TfsWorkingOn.Monitor;
using Rowan.TfsWorkingOn.WinForm.Properties;

namespace Rowan.TfsWorkingOn.WinForm
{
    public partial class FormSetConnection : Form
    {
        private Connection _connection = new Connection();
        private WorkingItem _workingItem;

        private bool _exiting;
        private static string NotifyIconText = Resources.NotifyIconText;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public FormSetConnection()
        {
            InitializeComponent();

            connectionBindingSource.DataSource = _connection;
            comboBoxTfsServers.Items.AddRange(RegisteredServers.GetServerNames());

            _connection.Server = Settings.Default.TfsServer;
            try
            {
                if (!string.IsNullOrEmpty(_connection.Server))
                {
                    _connection.Connect();
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

            if (_connection.IsConnected && Settings.Default.SelectedProjectId != -1 && !string.IsNullOrEmpty(Settings.Default.SelectedProjectName))
            {
                _connection.SelectedProjectId = Settings.Default.SelectedProjectId;
                _connection.SelectedProjectName = Settings.Default.SelectedProjectName;
                Hide();
            }
            else
            {
                Show();
            }
        }

        private void ShowSearchForm()
        {
            if (_workingItem != null && _workingItem.Started)
            {
                if (MessageBox.Show(string.Format(CultureInfo.CurrentCulture, Resources.StopWorkingOnWorkItem, _workingItem.WorkItem.Title), Resources.StopCurrentWorkItem, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    StartStop();
                }
                else
                {
                    return;
                }
            }

            if (!_connection.IsConnected)
            {
                Show();
                return;
            }
            
            FormSearchWorkItems formSearchWorkItems = new FormSearchWorkItems(_connection.WorkItemStore, _connection.SelectedProjectName);
            formSearchWorkItems.WorkingItem.PropertyChanged += new PropertyChangedEventHandler(formSearchWorkItems_WorkingItem_PropertyChanged);
            formSearchWorkItems.Show();
        }

        private void StartStop()
        {
            if (_workingItem == null)
            {
                ShowSearchForm();
                return;
            }

            if (_workingItem.Started)
            {
                startToolStripMenuItem.Text = Resources.Start;
                notifyIcon.Text = NotifyIconText;
                notifyIcon.BalloonTipText = string.Format(CultureInfo.CurrentCulture, Resources.StoppedWorkingOn, _workingItem.WorkItem.Id.ToString(CultureInfo.CurrentCulture));
            }
            else
            {
                startToolStripMenuItem.Text = Resources.Stop;
                notifyIcon.Text = NotifyIconText + _workingItem.WorkItem.Id.ToString(CultureInfo.CurrentCulture);
                notifyIcon.BalloonTipText = string.Format(CultureInfo.CurrentCulture, Resources.StartedWorkingOn, _workingItem.WorkItem.Id.ToString(CultureInfo.CurrentCulture));
            }
            _workingItem.StartStop();
            notifyIcon.ShowBalloonTip(Settings.Default.BalloonTipTimeoutSeconds * 1000);
        }

        // Pause Resume on User Activity Monitor
        private void _userActivity_MonitorTriggeredEvent(object sender, MonitorEventArgs e)
        {
            UserActivity userActivity = sender as UserActivity;
            if (userActivity == null) return;

            if (userActivity.UserActiveState == UserActivityState.Inactive)
            {
                _workingItem.Pause(e.Reason);
                notifyIcon.Text = string.Format(CultureInfo.CurrentCulture, "{0} {1}{2}", Resources.Paused, NotifyIconText, _workingItem.WorkItem.Id);
                notifyIcon.BalloonTipText = string.Format(CultureInfo.CurrentCulture, Resources.PausedWorkingOn, _workingItem.WorkItem.Id);
                notifyIcon.ShowBalloonTip(Settings.Default.BalloonTipTimeoutSeconds * 1000);
            }
            else if (userActivity.UserActiveState == UserActivityState.Active)
            {
                _workingItem.Resume();
                notifyIcon.Text = string.Format(CultureInfo.CurrentCulture, "{0} {1}{2}", Resources.Resumed, NotifyIconText, _workingItem.WorkItem.Id);
                notifyIcon.BalloonTipText = string.Format(CultureInfo.CurrentCulture, Resources.ResumedWorkingOn, _workingItem.WorkItem.Id);
            }
            notifyIcon.ShowBalloonTip(Settings.Default.BalloonTipTimeoutSeconds * 1000);
        }

        #region Events
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                _connection.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, Resources.FailedToConnect, ex.Message), Resources.ConnectionFailedTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Settings.Default.TfsServer = _connection.Server;
            Settings.Default.SelectedProjectId = _connection.SelectedProjectId;
            Settings.Default.SelectedProjectName = _connection.SelectedProjectName;
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
            if (e.PropertyName == WorkingItem.WorkItemPropertyName)
            {
                _workingItem = sender as WorkingItem;
                if (_workingItem != null)
                {
                    _workingItem.Connection = _connection;
                    _workingItem.UserActivityMonitor.MonitorTriggeredEvent += new EventHandler<MonitorEventArgs>(_userActivity_MonitorTriggeredEvent);
                    selectedToolStripMenuItem.Text = string.Format(CultureInfo.CurrentCulture, Resources.Selected, _workingItem.WorkItem.Id);
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
            notifyIcon.Dispose();
            Application.Exit();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormWorkItem formWorkItem = new FormWorkItem(_workingItem.WorkItem);
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
            FormEstimates formEstimates = new FormEstimates(_workingItem);
            formEstimates.Show();
        }
        #endregion
    }
}
