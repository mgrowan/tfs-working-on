﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.Win32;
using Rowan.TfsWorkingOn.Monitor;
using Rowan.TfsWorkingOn.WinForm.Properties;

namespace Rowan.TfsWorkingOn.WinForm
{
    public partial class FormSetConnection : Form
    {
        private Connection _connection = new Connection();
        private WorkingItem _workingItem;
        private Nag _nag = new Nag();

        private bool _exiting;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public FormSetConnection()
        {
            InitializeComponent();
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            SystemEvents.SessionEnding += new SessionEndingEventHandler(SystemEvents_SessionEnding);
            Settings.Default.PropertyChanged += new PropertyChangedEventHandler(Settings_PropertyChanged);

            connectionBindingSource.DataSource = _connection;
            comboBoxTfsServers.Items.AddRange(RegisteredServers.GetServerNames());

            _connection.Server = Settings.Default.TfsServer;

            _nag.MonitorTriggeredEvent += new EventHandler<MonitorEventArgs>(_nag_MonitorTriggeredEvent);
            components.Add(_nag);
            _nag.Start();
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
                ShowSearchForm();
                SetMenuQuery();
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
            formSearchWorkItems.WorkingItem.PropertyChanged += new PropertyChangedEventHandler(WorkingItem_PropertyChanged);
            formSearchWorkItems.Show();
        }

        private void StartStop()
        {
            if (_workingItem == null)
            {
                ShowSearchForm();
                return;
            }

            if (_workingItem.Started) // Stop
            {
                _nag.Start();
                startToolStripMenuItem.Text = Resources.Start;
                notifyIcon.BalloonTipText = string.Format(CultureInfo.CurrentCulture, Resources.StoppedWorkingOn,
                    _workingItem.WorkItem.Id.ToString(CultureInfo.CurrentCulture), GetStringWithEllipsis(_workingItem.WorkItem.Title, 50));
                notifyIcon.Icon = Resources.Stopwatch_Red;
            }
            else // Start
            {
                _nag.Stop();
                startToolStripMenuItem.Text = Resources.Stop;
                notifyIcon.Text = GetStringWithEllipsis(Resources.NotifyIconText + _workingItem.WorkItem.Id.ToString(CultureInfo.CurrentCulture) + "\n" + _workingItem.WorkItem.Title, 63);
                notifyIcon.BalloonTipText = string.Format(CultureInfo.CurrentCulture, Resources.StartedWorkingOn,
                    _workingItem.WorkItem.Id.ToString(CultureInfo.CurrentCulture), GetStringWithEllipsis(_workingItem.WorkItem.Title, 50));
                notifyIcon.Icon = Resources.Stopwatch_Green;
            }
            _workingItem.StartStop();
            notifyIcon.ShowBalloonTip(Settings.Default.BalloonTipTimeoutSeconds * 1000);
        }

        private void SetMenuQuery()
        {
            if (Settings.Default.SelectedQuery != Guid.Empty)
            {
                try
                {
                    StoredQuery query = _connection.WorkItemStore.GetStoredQuery(Settings.Default.SelectedQuery);
                    queryListToolStripMenuItem.Text = GetStringWithEllipsis(query.Name, 30);
                    queryListToolStripMenuItem.ToolTipText = query.Name;
                }
                catch (UnauthorizedAccessException)
                {
                    Settings.Default.SelectedQuery = Guid.Empty;
                }
            }
        }

        private static string GetStringWithEllipsis(string text, int length)
        {
            return text.Length > length ? string.Concat(text.Substring(0, length - 3), "...") : text;
        }

        private void SafeShutdown()
        {
            if (_workingItem != null && _workingItem.Started) _workingItem.StartStop();
            notifyIcon.Dispose();
        }

        #region Events
        private static readonly object _userActivityMutex = new object();
        private static bool _userActivityTriggeredCurrentlyProcessing;
        // Pause Resume on User Activity Monitor
        private void _userActivity_MonitorTriggeredEvent(object sender, MonitorEventArgs e)
        {
            UserActivity userActivity = sender as UserActivity;
            if (userActivity == null) return;
            if (_userActivityTriggeredCurrentlyProcessing) return;

            lock (_userActivityMutex)
            {
                if (_userActivityTriggeredCurrentlyProcessing) return;
                _userActivityTriggeredCurrentlyProcessing = true;

                if (userActivity.UserActiveState == UserActivityState.Inactive)
                {
                    _workingItem.Pause(e.Reason);
                    notifyIcon.Text = GetStringWithEllipsis(string.Format(CultureInfo.CurrentCulture, "{0} {1}{2}\n{3}", Resources.Paused, Resources.NotifyIconText, _workingItem.WorkItem.Id, _workingItem.WorkItem.Title), 63);
                    notifyIcon.BalloonTipText = string.Format(CultureInfo.CurrentCulture, Resources.PausedWorkingOn, _workingItem.WorkItem.Id, GetStringWithEllipsis(_workingItem.WorkItem.Title, 50));
                    notifyIcon.ShowBalloonTip(Settings.Default.BalloonTipTimeoutSeconds * 1000);
                }
                else if (userActivity.UserActiveState == UserActivityState.Active)
                {
                    bool record = false;
                    if (Settings.Default.PromptOnResume)
                    {
                        DialogResult dialogResult = MessageBox.Show(string.Format(CultureInfo.CurrentCulture, Resources.PromptOnResumeText, _workingItem.WorkItem.Id, _workingItem.WorkItem.Title),
                            Resources.PromptOnResumeCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                        switch (dialogResult)
                        {
                            case DialogResult.Cancel:
                                StartStop();
                                return;
                            case DialogResult.Yes:
                                record = true;
                                break;
                        }
                    }
                    if (_workingItem.Paused) _workingItem.Resume(record);
                    notifyIcon.Text = GetStringWithEllipsis(string.Format(CultureInfo.CurrentCulture, "{0} {1}{2}\n{3}", Resources.Resumed, Resources.NotifyIconText, _workingItem.WorkItem.Id, _workingItem.WorkItem.Title), 63);
                    notifyIcon.BalloonTipText = string.Format(CultureInfo.CurrentCulture, Resources.ResumedWorkingOn, _workingItem.WorkItem.Id, GetStringWithEllipsis(_workingItem.WorkItem.Title, 50));
                }
                notifyIcon.ShowBalloonTip(Settings.Default.BalloonTipTimeoutSeconds * 1000);
            }
            _userActivityTriggeredCurrentlyProcessing = false;
        }

        void _nag_MonitorTriggeredEvent(object sender, MonitorEventArgs e)
        {
            notifyIcon.BalloonTipText = Resources.NagMessage;
            notifyIcon.ShowBalloonTip(Settings.Default.BalloonTipTimeoutSeconds * 1000);
        }

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

        private void WorkingItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == WorkingItem.WorkItemPropertyName)
            {
                _workingItem = sender as WorkingItem;
                if (_workingItem != null)
                {
                    _workingItem.Connection = _connection;
                    _workingItem.UserActivityMonitor.MonitorTriggeredEvent += new EventHandler<MonitorEventArgs>(_userActivity_MonitorTriggeredEvent);
                    selectedToolStripMenuItem.Text = string.Format(CultureInfo.CurrentCulture, Resources.Selected, _workingItem.WorkItem.Id);
                    selectedToolStripMenuItem.ToolTipText = _workingItem.WorkItem.Title;
                    selectedToolStripMenuItem.Enabled = true;
                    StartStop();
                }
            }
        }

        void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Settings.SelectedQueryPropertyName && Settings.Default.SelectedQuery != Guid.Empty)
            {
                SetMenuQuery();

                foreach (ToolStripItem tsi in queryListToolStripMenuItem.DropDownItems)
                {
                    tsi.Click -= ToolStripWorkItem_Click;
                }
                queryListToolStripMenuItem.DropDownItems.Clear();
                queryListToolStripMenuItem.DropDownItems.Add(workItemsToolStripMenuItem);
            }
        }

        void Application_ApplicationExit(object sender, EventArgs e)
        {
            SafeShutdown();
        }

        void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            SafeShutdown();
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

        private void queryListToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (queryListToolStripMenuItem.DropDownItems.Count > 1 || Settings.Default.SelectedQuery == Guid.Empty) return;
            queryListToolStripMenuItem.DropDownItems.Clear();

            Dictionary<string, string> parameters = new Dictionary<string, string>(2);
            parameters.Add("me", TeamFoundationServerFactory.GetServer(_connection.Server).AuthenticatedUserDisplayName);
            parameters.Add("project", Settings.Default.SelectedProjectName);
            WorkItemCollection workItems = _connection.WorkItemStore.Query(_connection.WorkItemStore.GetStoredQuery(Settings.Default.SelectedQuery).QueryText, parameters);
            List<ToolStripItem> workItemToolStripItems = new List<ToolStripItem>();
            foreach (WorkItem workItem in workItems)
            {
                ToolStripItem wi = new ToolStripMenuItem();
                wi.Tag = workItem.Id;
                wi.ToolTipText = string.Format(CultureInfo.CurrentCulture, "{0}: {1}", workItem.Id, workItem.Title);
                wi.Text = GetStringWithEllipsis(wi.ToolTipText, 60);
                wi.Click += ToolStripWorkItem_Click;
                workItemToolStripItems.Add(wi);
            }
            queryListToolStripMenuItem.DropDownItems.AddRange(workItemToolStripItems.ToArray());
        }

        void ToolStripWorkItem_Click(object sender, EventArgs e)
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

            _workingItem = new WorkingItem();
            _workingItem.PropertyChanged += new PropertyChangedEventHandler(WorkingItem_PropertyChanged);
            _workingItem.WorkItem = _connection.WorkItemStore.GetWorkItem((int)(sender as ToolStripItem).Tag);
            _workingItem.WorkItem.Open();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _exiting = true;
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

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_workingItem.Started)
            {
                _nag.Start();
                startToolStripMenuItem.Text = Resources.Start;
                notifyIcon.BalloonTipText = string.Format(CultureInfo.CurrentCulture, Resources.CancelledWorkingOn,
                    _workingItem.WorkItem.Id.ToString(CultureInfo.CurrentCulture), GetStringWithEllipsis(_workingItem.WorkItem.Title, 50));
                notifyIcon.Icon = Resources.Stopwatch_Red;
                _workingItem.Cancel();
            }
        }
        #endregion
    }
}
