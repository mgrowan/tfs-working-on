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
using Rowan.TfsWorkingOn.UserHistory;
using Rowan.TfsWorkingOn.WinForm.Properties;

namespace Rowan.TfsWorkingOn.WinForm
{
    public partial class FormNotificationTray : Form
    {
        #region Fields

        private WorkingItem _workingItem;
        private readonly Nag _nag = new Nag();

        private bool _exiting;

        private readonly ToolStripMenuItem _refreshQueryListItem;
        private readonly Image _stationaryRefreshGif;

        private static readonly TeamProjectPicker ProjectPicker = new TeamProjectPicker();
        private static FormSearchWorkItems _formSearchWorkItems;
        private static FormWorkItemConfiguration _formItemConfiguration;

        // initial load of the settings
        private readonly Settings _currentSettings = Settings.Default;

        #endregion

        #region Constructor
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public FormNotificationTray()
        {
            InitializeComponent();

            queryListToolStripMenuItem.DropDown.Closing += new ToolStripDropDownClosingEventHandler(queryListToolStripMenuItemDropDown_Closing);
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            SystemEvents.SessionEnding += new SessionEndingEventHandler(SystemEvents_SessionEnding);
            Connection.GetConnection().PropertyChanged += new PropertyChangedEventHandler(Connection_PropertyChanged);
            
            _nag.MonitorTriggeredEvent += new EventHandler<MonitorEventArgs>(_nag_MonitorTriggeredEvent);
            components.Add(_nag);
            _nag.Start();

            /* keep the refresh item separated from the refresh process so that it can remain within the menu upon refresh
            and not cause an apparent closure on items.Clear(). set the menu query dropdown to not auto close so that explicit calls can handle closure */
            this._refreshQueryListItem = new ToolStripMenuItem(Resources.RefreshQueryList, Resources.Refresh, refreshQueryToolStripItem_Click);

            this._stationaryRefreshGif = Resources.Refresh.GetThumbnailImage(Resources.Refresh.Width,
                                                                       Resources.Refresh.Height,
                                                                       null,
                                                                       (IntPtr)0);

            this._refreshQueryListItem.Image = this._stationaryRefreshGif;
            this.workItemsToolStripMenuItem.Image = this._stationaryRefreshGif;

            Hide(); // TODO Don't ever show this form -- CB: remove form inheritance and lose nice development/design gui?

            if (_currentSettings.LastProjectCollectionWorkedOn != null)
            {
                try
                {
                    Connection.GetConnection().TeamProjectCollectionAbsoluteUri = _currentSettings.LastProjectCollectionWorkedOn.TeamProjectCollectionAbsoluteUri;
                    Connection.GetConnection().SelectedProjectName = _currentSettings.LastProjectCollectionWorkedOn.LastProjectWorkedOn.ProjectName;
                    Connection.Connect();
                }
                catch (Exception)
                {
                    ShowTeamProjectPicker(); // TODO: check for errors occurring here when no settings 
                }
            }
            else
            {
                ShowTeamProjectPicker();
            }

            if (_currentSettings.LastProjectCollectionWorkedOn != null)
            {
                if (this._currentSettings.LastProjectCollectionWorkedOn.LastProjectWorkedOn.LastQueryWorkedOn.HasValue)
                {
                    this.SetMenuQuery();
                }
                else
                {
                    this.ShowSearchForm();
                }
            }

            SetUserOptionAccess();
        }
        #endregion

        #region Class Methods
        private static string GetStringWithEllipsis(string text, int length)
        {
            return text.Length > length ? string.Concat(text.Substring(0, length - 3), "...") : text;
        }

        private static void ShowTeamProjectPicker()
        {
            switch (ProjectPicker.ShowDialog())
            {
                case DialogResult.OK:
                    Connection.GetConnection().TeamProjectCollectionAbsoluteUri = ProjectPicker.SelectedTeamProjectCollection.Uri.AbsoluteUri;
                    Connection.GetConnection().SelectedProjectName = ProjectPicker.SelectedProjects[0].Name;
                    Connection.Connect();
                    break;
            }
        }
        #endregion

        #region Instance Methods
        private void ShowSearchForm()
        {
            if (_workingItem != null && _workingItem.Started)
            {
                if (MessageBox.Show(string.Format(CultureInfo.CurrentCulture, Resources.StopWorkingOnWorkItem, _workingItem.WorkItem.Title), Resources.StopCurrentWorkItem, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    StartStop();
                }
                else
                {
                    return;
                }
            }

            if (_formSearchWorkItems != null)
            {
                _formSearchWorkItems.Focus();
                return;
            }

            _formSearchWorkItems = new FormSearchWorkItems(Connection.GetConnection().WorkItemStore, Connection.GetConnection().SelectedProjectName);
            _formSearchWorkItems.WorkingItem.PropertyChanged += new PropertyChangedEventHandler(WorkingItem_PropertyChanged);
            _formSearchWorkItems.ShowDialog();

            _formSearchWorkItems = null;
        }

        private void SetCurrentWorkItem(int itemId)
        {
            _workingItem = new WorkingItem();
            _workingItem.PropertyChanged += new PropertyChangedEventHandler(WorkingItem_PropertyChanged);
            _workingItem.WorkItem = Connection.GetConnection().WorkItemStore.GetWorkItem(itemId);
            _workingItem.WorkItem.Open();
        }

        private void StartStop()
        {
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

            notifyIcon.ShowBalloonTip(_currentSettings.BalloonTipTimeoutSeconds * 1000);
        }

        /// <summary>
        /// Sets the menu query.
        /// </summary>
        private void SetMenuQuery()
        {           
            try
            {
                QueryDefinition query =
                    Connection.GetConnection().WorkItemStore.GetQueryDefinition(this._currentSettings.LastProjectCollectionWorkedOn.LastProjectWorkedOn.LastQueryWorkedOn.Value);
                queryListToolStripMenuItem.Text = GetStringWithEllipsis(query.Name, 30);
                queryListToolStripMenuItem.ToolTipText = query.Name;

                this.RefreshQueryList();
            }
            catch (UnauthorizedAccessException)
            {
                this._currentSettings.LastProjectCollectionWorkedOn.LastProjectWorkedOn.LastQueryWorkedOn = null;
            }
            catch (ArgumentException)
            {
            } // remove with WI #13244 
        }

        /// <summary>
        /// Performs a safe shutdown.
        /// </summary>
        private void SafeShutdown()
        {
            if (_workingItem != null && _workingItem.Started) _workingItem.StartStop();
            _currentSettings.Save();
            notifyIcon.Dispose();
        }

        /// <summary>
        /// Refreshes the query list.
        /// </summary>
        private void RefreshQueryList()
        {
            if (refreshingWorkItemsWorker.IsBusy)
            {
                return;
            }

            foreach (var itm in GetMenuQueryToolStripItems())
            {
                itm.Enabled = false;
            }

            this._refreshQueryListItem.Image = Resources.Refresh;

            refreshingWorkItemsWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Cleans the menu query drop down.
        /// </summary>
        private void CleanMenuQueryDropDown()
        {
            if (!_currentSettings.LastProjectCollectionWorkedOn.LastProjectWorkedOn.LastQueryWorkedOn.HasValue)
            {
                SetMenuQueryToDefault();
            }
            else if (queryListToolStripMenuItem.DropDownItems.Contains(workItemsToolStripMenuItem))
            {
                queryListToolStripMenuItem.DropDownItems.Clear();
                queryListToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this._refreshQueryListItem, new ToolStripSeparator() });
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
        /// Sets the menu query to default.
        /// </summary>
        private void SetMenuQueryToDefault()
        {
            queryListToolStripMenuItem.DropDownItems.Clear();
            queryListToolStripMenuItem.DropDownItems.Add(workItemsToolStripMenuItem);
            queryListToolStripMenuItem.Text = Resources.QueryListDefaultText;
            queryListToolStripMenuItem.ToolTipText = string.Empty;
        }

        /// <summary>
        /// Sets the menu selected item to default.
        /// </summary>
        private void SetMenuSelectedItemToDefault()
        {
            selectedToolStripMenuItem.Text = string.Format(Resources.Selected, string.Empty);
            selectedToolStripMenuItem.ToolTipText = string.Empty;
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
            parameters.Add("project", _currentSettings.LastProjectCollectionWorkedOn.LastProjectWorkedOn.ProjectName);
            Query query = new Query(Connection.GetConnection().WorkItemStore, Connection.GetConnection().WorkItemStore.GetQueryDefinition(this._currentSettings.LastProjectCollectionWorkedOn.LastProjectWorkedOn.LastQueryWorkedOn.Value).QueryText, parameters);

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
                workItemToolStripItems.AddRange(from WorkItem workItem in query.RunQuery()
                                                select CreateNewMenuQueryItem(workItem));
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

        /// <summary>
        /// Sets the user option access.
        /// </summary>
        private void SetUserOptionAccess()
        {
            // TODO: decision to either restrict access (disable as below) or get connection

            bool hasProject = !string.IsNullOrEmpty(Connection.GetConnection().SelectedProjectName);

            selectWorkItemToolStripMenuItem.Enabled = hasProject;
            queryListToolStripMenuItem.Enabled = hasProject;
            configureToolStripMenuItem.Enabled = hasProject;

            bool hasWorkItem = _workingItem != null;

            selectedToolStripMenuItem.Enabled = hasWorkItem;
            startToolStripMenuItem.Enabled = hasWorkItem;
        }

        private void AskForEstimates()
        {
            if (_workingItem.Estimates.Duration != 0 || _workingItem.Estimates.ElapsedTime != 0) return;

            // If no estimates, then ask user add some
            if (MessageBox.Show(Resources.ThereAreNoEstimatesForTheWorkItem, Resources.AddEstimates, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FormEstimates formEstimates = new FormEstimates(_workingItem);
                formEstimates.ShowDialog(this);
            }
        }
        #endregion

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
                    notifyIcon.ShowBalloonTip(_currentSettings.BalloonTipTimeoutSeconds * 1000);
                }
                else if (userActivity.UserActiveState == UserActivityState.Active)
                {
                    bool record = false;
                    if (_currentSettings.PromptOnResume)
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
                notifyIcon.ShowBalloonTip(_currentSettings.BalloonTipTimeoutSeconds * 1000);
            }
            _userActivityTriggeredCurrentlyProcessing = false;
        }

        private void _nag_MonitorTriggeredEvent(object sender, MonitorEventArgs e)
        {
            notifyIcon.BalloonTipText = Resources.NagMessage;
            notifyIcon.ShowBalloonTip(_currentSettings.BalloonTipTimeoutSeconds * 1000);
        }

        private void FormSetConnection_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !_exiting;
            Hide();
        }

        private void WorkingItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != WorkingItem.WorkItemPropertyName) return;

            _workingItem = sender as WorkingItem;

            bool hasWorkItem = _workingItem != null;

            this.selectedToolStripMenuItem.Enabled = hasWorkItem;
            this.startToolStripMenuItem.Enabled = hasWorkItem;

            if (_workingItem == null)
            {
                return;
            }

            _workingItem.Connection = Connection.GetConnection();
            _workingItem.UserActivityMonitor.MonitorTriggeredEvent += new EventHandler<MonitorEventArgs>(_userActivity_MonitorTriggeredEvent);

            selectedToolStripMenuItem.Text = string.Format(CultureInfo.CurrentCulture, Resources.Selected, _workingItem.WorkItem.Id);
            selectedToolStripMenuItem.ToolTipText = _workingItem.WorkItem.Title;
            StartStop();

            AskForEstimates();
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            this.SafeShutdown();
        }

        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
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

                if (e.Error is ArgumentException) // remove with WI #13244 
                {
                    return;
                }

                throw e.Error;
            }

            var items = (List<ToolStripMenuItem>)e.Result;
            queryListToolStripMenuItem.DropDownItems.AddRange(items.ToArray());

            _refreshQueryListItem.Image = _stationaryRefreshGif;
        }

        private void ProjectWorkedOn_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != ProjectWorkedOn.LastQueryWorkedOnFieldName)
                return;

            if (_currentSettings.LastProjectCollectionWorkedOn.LastProjectWorkedOn.LastQueryWorkedOn != null)
            {
                this.SetMenuQuery();
            }
            else
            {
                this.CleanMenuQueryDropDown();
            }
        }

        private void Connection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this._workingItem = null;
            this.SetMenuSelectedItemToDefault();

            switch (e.PropertyName)
            {
                case Connection.TeamProjectCollectionAbsoluteUriPropertyName:
                    _currentSettings.SetCollectionLastWorkedOn(Connection.GetConnection().TeamProjectCollectionAbsoluteUri);
                    break;
                case Connection.SelectedProjectNamePropertyName:
                    _currentSettings.LastProjectCollectionWorkedOn.SetProjectLastWorkedOn(Connection.GetConnection().SelectedProjectName);
                    _currentSettings.LastProjectCollectionWorkedOn.LastProjectWorkedOn.PropertyChanged += new PropertyChangedEventHandler(ProjectWorkedOn_PropertyChanged);
                    break;
            }

            SetUserOptionAccess();
        }
        #endregion events

        #region System Tray Menu Clicks
        private void toolStripConnect_Click(object sender, EventArgs e)
        {
            string initialProjectName = Connection.GetConnection().SelectedProjectName;

            ShowTeamProjectPicker();

            if (Connection.GetConnection().SelectedProjectName == initialProjectName) return;
            
            CleanMenuQueryDropDown();

            if (_currentSettings.LastProjectCollectionWorkedOn.LastProjectWorkedOn.LastQueryWorkedOn.HasValue)
            {
                this.SetMenuQuery();
            }
            else
            {
                ShowSearchForm();
            }
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
            if (queryListToolStripMenuItem.DropDownItems.Contains(workItemsToolStripMenuItem)
                || queryListToolStripMenuItem.DropDownItems.Count > 2)
            {
                return;
            }
            
            RefreshQueryList();
        }

        private void refreshQueryToolStripItem_Click(object sender, EventArgs e)
        {
            if (!this._currentSettings.LastProjectCollectionWorkedOn.LastProjectWorkedOn.LastQueryWorkedOn.HasValue) 
            {
                this.configureToolStripMenuItem_Click(sender, e); // Open Configuration
            }
            else
            {
                this.RefreshQueryList();
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

            // explicitly close the context menus on refresh button click
            queryListToolStripMenuItem.DropDown.Close();
            notifyMenu.Close();

            var tsItem = sender as ToolStripItem;

            if (tsItem != null)
            {
                SetCurrentWorkItem((int)tsItem.Tag);
            }
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
            if (_formItemConfiguration != null)
            {
                _formItemConfiguration.Focus();
                return;
            }

            _formItemConfiguration = new FormWorkItemConfiguration();
            _formItemConfiguration.ShowDialog(); // TODO: have the config setup for a given project
            _formItemConfiguration = null;
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
