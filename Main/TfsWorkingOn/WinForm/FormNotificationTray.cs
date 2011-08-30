using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.Win32;
using Rowan.TfsWorkingOn.Monitor;
using Rowan.TfsWorkingOn.WinForm.Properties;

namespace Rowan.TfsWorkingOn.WinForm
{
    public partial class FormNotificationTray : Form
    {
        #region Fields

        private WorkingItem _workingItem;
        private Nag _nag = new Nag();

        private bool _exiting;

        private ToolStripMenuItem refreshQueryListItem;
        private Image stationaryRefreshGif;

        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public FormNotificationTray()
        {
            InitializeComponent();
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            SystemEvents.SessionEnding += new SessionEndingEventHandler(SystemEvents_SessionEnding);
            Settings.Default.PropertyChanged += new PropertyChangedEventHandler(Settings_PropertyChanged);

            _nag.MonitorTriggeredEvent += new EventHandler<MonitorEventArgs>(_nag_MonitorTriggeredEvent);
            components.Add(_nag);
            _nag.Start();
            try
            {
                if (Settings.Default.TeamProjectCollectionAbsoluteUri != null)
                {
                    Connection.Connect();
                }
                else
                {
                    ShowTeamProjectPicker();
                    return;
                }
            }
            catch (Exception)
            {
                ShowTeamProjectPicker();
                return;
            }

            if (!string.IsNullOrEmpty(Settings.Default.SelectedProjectName))
            {
                Connection.GetConnection().SelectedProjectName = Settings.Default.SelectedProjectName;
                Hide(); // TODO Don't ever show this form
                ShowSearchForm();
                SetMenuQuery();
            }
            else
            {
                ShowTeamProjectPicker();
            }

            /* keep the refresh item separated from the refresh process so that it can remain within the menu upon refresh
            and not cause an apparent closure on items.Clear(). set the menu query dropdown to not auto close so that explicit calls can handle closure */
            refreshQueryListItem = new ToolStripMenuItem(Resources.RefreshQueryList, Resources.Refresh, refreshQueryToolStripItem_Click);

            stationaryRefreshGif = Resources.Refresh.GetThumbnailImage(Resources.Refresh.Width,
                                                             Resources.Refresh.Height, null,
                                                             (IntPtr)0);

            refreshQueryListItem.Image = stationaryRefreshGif;
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

            if (!Connection.GetConnection().IsConnected)
            {
                ShowTeamProjectPicker();
                return;
            }

            FormSearchWorkItems formSearchWorkItems = new FormSearchWorkItems(Connection.GetConnection().WorkItemStore, Connection.GetConnection().SelectedProjectName);
            formSearchWorkItems.WorkingItem.PropertyChanged += new PropertyChangedEventHandler(WorkingItem_PropertyChanged);
            formSearchWorkItems.Show();
        }

        private static TeamProjectPicker _teamProjectPicker = new TeamProjectPicker();
        private void ShowTeamProjectPicker()
        {
            switch (_teamProjectPicker.ShowDialog())
            {
                case DialogResult.OK:
                    Settings.Default.TeamProjectCollectionAbsoluteUri = _teamProjectPicker.SelectedTeamProjectCollection.Uri.AbsoluteUri;
                    Settings.Default.SelectedProjectName = _teamProjectPicker.SelectedProjects[0].Name;
                    Settings.Default.Save();
                    Connection.Connect();
                    ShowSearchForm();
                    break;
                case DialogResult.Cancel:
                    if (!Connection.GetConnection().IsConnected) SafeShutdown();
                    break;
                default:
                    ShowTeamProjectPicker(); // TODO Allow an Application Exit
                    break;
            }
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
            if (Settings.Default.SelectedQuery.HasValue)
            {
                try
                {
                    QueryDefinition query = Connection.GetConnection().WorkItemStore.GetQueryDefinition(Settings.Default.SelectedQuery.Value);
                    queryListToolStripMenuItem.Text = GetStringWithEllipsis(query.Name, 30);
                    queryListToolStripMenuItem.ToolTipText = query.Name;
                }
                catch (UnauthorizedAccessException)
                {
                    Settings.Default.SelectedQuery = null;
                }
                catch (ArgumentException) { } // remove with WI #13244 
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

        /// <summary>
        /// Refreshes the query list.
        /// </summary>
        private void RefreshQueryList()
        {
            // disregard repeated clicks, currently no reason to implement cancel
            if (refreshingWorkItemsWorker.IsBusy) return;

            foreach (var itm in GetMenuQueryToolStripItems())
            {
                itm.Enabled = false;
            }

            refreshQueryListItem.Image = Resources.Refresh;

            refreshingWorkItemsWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Cleans the menu query drop down.
        /// </summary>
        private void CleanMenuQueryDropDown()
        {
            if (queryListToolStripMenuItem.DropDownItems.Contains(workItemsToolStripMenuItem))
            {
                queryListToolStripMenuItem.DropDownItems.Clear();
                queryListToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { refreshQueryListItem, new ToolStripSeparator() });
            }
            else
            {
                foreach (var itm in GetMenuQueryToolStripItems())
                {
                    itm.Click -= ToolStripWorkItem_Click;
                    queryListToolStripMenuItem.DropDownItems.Remove(itm);
                }
            }
        }

        /// <summary>
        /// Creates the new menu query items.
        /// </summary>
        /// <returns>All of the new menu query items.</returns>
        private IEnumerable<ToolStripMenuItem> CreateNewMenuQueryItems()
        {
            List<ToolStripMenuItem> workItemToolStripItems = new List<ToolStripMenuItem>();

            Dictionary<string, string> parameters = new Dictionary<string, string>(2);
            parameters.Add("me", Connection.GetConnection().TfsTeamProjectCollection.AuthorizedIdentity.DisplayName);
            parameters.Add("project", Settings.Default.SelectedProjectName);
            Query query = new Query(Connection.GetConnection().WorkItemStore, Connection.GetConnection().WorkItemStore.GetQueryDefinition(Settings.Default.SelectedQuery.Value).QueryText, parameters);

            if (query.IsLinkQuery)
            {
                WorkItemLinkInfo[] workItemLinkInfos = query.RunLinkQuery();
                Stack<int> parents = new Stack<int>();
                int previous = 0;
                foreach (WorkItemLinkInfo wiLinkInfo in workItemLinkInfos)
                {
                    // Build indented query result tree
                    if (parents.Count == 0) parents.Push(wiLinkInfo.SourceId);
                    if (parents.Peek() != wiLinkInfo.SourceId && previous == wiLinkInfo.SourceId) parents.Push(wiLinkInfo.SourceId);
                    else
                    {
                        while (wiLinkInfo.SourceId != parents.Peek()) parents.Pop();
                    }
                    previous = wiLinkInfo.TargetId;

                    WorkItem wi = Connection.GetConnection().WorkItemStore.GetWorkItem(wiLinkInfo.TargetId);
                    workItemToolStripItems.Add(CreateNewMenuQueryItem(wi, parents.Count));
                }
            }
            else
            {
                foreach (WorkItem workItem in query.RunQuery())
                {
                    workItemToolStripItems.Add(CreateNewMenuQueryItem(workItem));
                }
            }

            return workItemToolStripItems;
        }

        /// <summary>
        /// Creates the new menu query item.
        /// </summary>
        /// <param name="workItem">The work item.</param>
        /// <param name="indentLevel">The indent level.</param>
        /// <returns>A new menu query item.</returns>
        private ToolStripMenuItem CreateNewMenuQueryItem(WorkItem workItem, int indentLevel = 0)
        {
            ToolStripMenuItem wi = new ToolStripMenuItem
                                       {
                                           Tag = workItem.Id,
                                           Name = Resources.QueryItemName + workItem.Id
                                       };

            string text = string.Format(CultureInfo.CurrentCulture, "{0," + indentLevel * 4 + "}: {1}", workItem.Id, workItem.Title);
            wi.Text = GetStringWithEllipsis(text, 60);
            // Only show tooltip if string is truncated on the Text field with an ellipsis. To help minimize occurrence of #13457.
            if (text != wi.Text) wi.ToolTipText = string.Format(CultureInfo.CurrentCulture, "{0}: {1}", workItem.Id, workItem.Title);
            wi.Click += ToolStripWorkItem_Click;
            return wi;
        }

        /// <summary>
        /// Gets the menu query tool strip items.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ToolStripMenuItem> GetMenuQueryToolStripItems()
        {
            return queryListToolStripMenuItem.DropDown.Items.OfType<ToolStripMenuItem>().Where(x => x.Name.StartsWith(Resources.QueryItemName)).ToList();
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
                    _workingItem.Connection = Connection.GetConnection();
                    _workingItem.UserActivityMonitor.MonitorTriggeredEvent += new EventHandler<MonitorEventArgs>(_userActivity_MonitorTriggeredEvent);

                    selectedToolStripMenuItem.Text = string.Format(CultureInfo.CurrentCulture, Resources.Selected, _workingItem.WorkItem.Id);
                    selectedToolStripMenuItem.ToolTipText = _workingItem.WorkItem.Title;
                    selectedToolStripMenuItem.Enabled = true;
                    StartStop();

                    AskForEstimates();
                }
            }
        }

        private void AskForEstimates()
        {
            // If no estimates, then ask user add some
            if (_workingItem.Estimates.Duration == 0 && _workingItem.Estimates.ElapsedTime == 0)
            {
                if (MessageBox.Show(Resources.ThereAreNoEstimatesForTheWorkItem, Resources.AddEstimates, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    FormEstimates formEstimates = new FormEstimates(_workingItem);
                    formEstimates.ShowDialog(this);
                }
            }
        }

        void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Settings.SelectedQueryPropertyName && Settings.Default.SelectedQuery != Guid.Empty)
            {
                SetMenuQuery();

                RefreshQueryList();
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

        private void queryListToolStripMenuItemDropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            e.Cancel = e.CloseReason == ToolStripDropDownCloseReason.ItemClicked;
        }

        private void notifyMenu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            e.Cancel = e.CloseReason == ToolStripDropDownCloseReason.AppClicked;
        }

        private void refreshingWorkItemsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var currentWorkItems = new List<ToolStripMenuItem>();

            currentWorkItems.AddRange(CreateNewMenuQueryItems());

            e.Result = currentWorkItems;
        }

        private void refreshingWorkItemsWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CleanMenuQueryDropDown();

            if (e.Error != null)
            {
                if (e.Error is UnauthorizedAccessException)
                {
                    // The message is informative, using this in the message box.
                    MessageBox.Show(e.Error.Message, Resources.ErrorLoadingQuery, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (e.Error is ArgumentException)
                {
                    return;
                }

                throw e.Error; // remove with WI #13244 
            }

            var items = (List<ToolStripMenuItem>)e.Result;
            queryListToolStripMenuItem.DropDownItems.AddRange(items.ToArray());

            refreshQueryListItem.Image = stationaryRefreshGif;
        }

        #endregion events

        #region System Tray Menu Clicks
        private void toolStripConnect_Click(object sender, EventArgs e)
        {
            ShowTeamProjectPicker();
        }

        private void selectWorkItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSearchForm();
        }

        private void workItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            configureToolStripMenuItem_Click(sender, e);
        }

        private void queryListToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (queryListToolStripMenuItem.DropDownItems.Count > 1 || !Settings.Default.SelectedQuery.HasValue)
            {
                return;
            }
            
            RefreshQueryList();
        }

        private void refreshQueryToolStripItem_Click(object sender, EventArgs e)
        {
            if (!Settings.Default.SelectedQuery.HasValue) 
            {
                configureToolStripMenuItem_Click(sender, e); // Open Configuration
            }
            else
            {
                RefreshQueryList();
            }
        }

        private void ToolStripWorkItem_Click(object sender, EventArgs e)
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

            if (!Connection.GetConnection().IsConnected)
            {
                ShowTeamProjectPicker();
                return;
            }

            // explicitly close the context menus on refresh button click
            queryListToolStripMenuItem.DropDown.Close();
            notifyMenu.Close();

            _workingItem = new WorkingItem();
            _workingItem.PropertyChanged += new PropertyChangedEventHandler(WorkingItem_PropertyChanged);
            _workingItem.WorkItem = Connection.GetConnection().WorkItemStore.GetWorkItem((int)(sender as ToolStripItem).Tag);
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
