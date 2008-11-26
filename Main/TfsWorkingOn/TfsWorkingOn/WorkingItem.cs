﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Rowan.TfsWorkingOn.Monitor;
using Rowan.TfsWorkingOn.Properties;

namespace Rowan.TfsWorkingOn
{
    public class WorkingItem : INotifyPropertyChanged, IDisposable
    {
        public Connection Connection { get; set; }

        public const string WorkItemPropertyName = "WorkItem";
        private WorkItem _workItem;
        public WorkItem WorkItem
        {
            get { return _workItem; }
            set
            {
                _workItem = value;
                OnPropertyChanged(new PropertyChangedEventArgs(WorkItemPropertyName));
            }
        }

        public const string StartTimePropertyName = "StartTime";
        private DateTime _startTime;
        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                OnPropertyChanged(new PropertyChangedEventArgs(StartTimePropertyName));
            }
        }

        public const string StopTimePropertyName = "StopTime";
        private DateTime _stopTime;
        public DateTime StopTime
        {
            get { return _stopTime; }
            set
            {
                _stopTime = value;
                OnPropertyChanged(new PropertyChangedEventArgs(StopTimePropertyName));
            }
        }

        public const string EstimatesPropertyName = "Estimates";
        private Estimates _estimates;
        public Estimates Estimates
        {
            get
            {
                if (_estimates == null)
                {
                    _estimates = new Estimates();
                    string filePath = Estimates.GetFilePath(WorkItem.Store.TeamFoundationServer.Uri.Host, WorkItem.Id);
                    if (File.Exists(filePath))
                    {
                        _estimates.Load(filePath);
                    }
                    LoadWorkItemConfiguration();
                    UpdateWorkingOnEstimates();

                }
                return _estimates;
            }
            set
            {
                _estimates = value;
                OnPropertyChanged(new PropertyChangedEventArgs(EstimatesPropertyName));
            }
        }

        public const string StartedPropertyName = "Started";
        private bool _started;
        public bool Started
        {
            get { return _started; }
            set
            {
                _started = value;
                OnPropertyChanged(new PropertyChangedEventArgs(StartedPropertyName));
            }
        }

        public const string PausedPropertyName = "Paused";
        private bool _paused;
        public bool Paused
        {
            get { return _paused; }
            set
            {
                _paused = value;
                OnPropertyChanged(new PropertyChangedEventArgs(PausedPropertyName));
            }
        }

        private UserActivity _userActivity = new UserActivity();
        public UserActivity UserActivityMonitor { get { return _userActivity; } }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

        public void UpdateEstimates()
        {
            UpdateEstimates(string.Empty);
        }

        public void UpdateEstimates(string reason)
        {
            TimeSpan interval = StopTime.Subtract(StartTime);
            Estimates.ElapsedTime += interval.TotalHours;
            Estimates.RemainingTime -= interval.TotalHours;
            if (Estimates.RemainingTime < 0) Estimates.RemainingTime = 0d;

            WorkItem.History = Resources.WorkingOnUpdate;
            if (!string.IsNullOrEmpty(reason)) WorkItem.History += reason + "<br/>";
            WorkItem.History += string.Format(CultureInfo.CurrentCulture, Resources.WorkingOnUpdateInterval, interval.Hours, interval.Minutes);
            WorkItem.History += string.Format(CultureInfo.CurrentCulture, Resources.WorkingOnUpdateStatus, Estimates.RemainingTime, Estimates.ElapsedTime, Estimates.Duration);

            UpdateWorkItem();
            WorkItem.Save();
            Estimates.Save(Estimates.GetFilePath(WorkItem.Store.TeamFoundationServer.Uri.Host, WorkItem.Id));
        }

        private WorkingItemConfiguration LoadWorkItemConfiguration()
        {
            WorkingItemConfiguration workingItemConfiguration = new WorkingItemConfiguration();
            workingItemConfiguration.Server = Connection.Server;
            workingItemConfiguration.SelectedProject = WorkItem.Project;
            workingItemConfiguration.SelectedWorkItemType = WorkItem.Type;
            workingItemConfiguration.Load();
            return workingItemConfiguration;
        }

        public void UpdateWorkItem()
        {
            WorkingItemConfiguration workingItemConfiguration = LoadWorkItemConfiguration();
            WorkItem.Open();
            if (!string.IsNullOrEmpty(workingItemConfiguration.DurationField))
            {
                WorkItem.Fields[workingItemConfiguration.DurationField].Value = _estimates.Duration;
            }
            if (!string.IsNullOrEmpty(workingItemConfiguration.ElapsedField))
            {
                WorkItem.Fields[workingItemConfiguration.ElapsedField].Value = _estimates.ElapsedTime;
            }
            if (!string.IsNullOrEmpty(workingItemConfiguration.RemainingField))
            {
                WorkItem.Fields[workingItemConfiguration.RemainingField].Value = _estimates.RemainingTime;
            }
        }

        private void UpdateWorkingOnEstimates()
        {
            WorkingItemConfiguration workingItemConfiguration = LoadWorkItemConfiguration();
            if (!string.IsNullOrEmpty(workingItemConfiguration.DurationField))
            {
                if (WorkItem.Fields[workingItemConfiguration.DurationField].Value != null)
                    _estimates.Duration = (double)WorkItem.Fields[workingItemConfiguration.DurationField].Value;
            }
            if (!string.IsNullOrEmpty(workingItemConfiguration.ElapsedField))
            {
                if (WorkItem.Fields[workingItemConfiguration.ElapsedField].Value != null)
                    _estimates.ElapsedTime = (double)WorkItem.Fields[workingItemConfiguration.ElapsedField].Value;
            }
            if (!string.IsNullOrEmpty(workingItemConfiguration.RemainingField))
            {
                if (WorkItem.Fields[workingItemConfiguration.RemainingField].Value != null)
                    _estimates.RemainingTime = (double)WorkItem.Fields[workingItemConfiguration.RemainingField].Value;
            }
        }

        public void StartStop()
        {
            if (Started) // Stop
            {
                StopTime = DateTime.Now;
                UserActivityMonitor.Stop();
                UpdateEstimates();
            }
            else // Start
            {
                StartTime = DateTime.Now;
                UserActivityMonitor.Start();
            }
            Started = !Started;
        }

        public void Pause(string reason)
        {
            Paused = true;
            StopTime = DateTime.Now;
            UpdateEstimates(reason);
        }

        public void Resume()
        {
            Paused = false;
            StartTime = DateTime.Now;
        }

        #region IDisposable Members
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (_userActivity != null))
            {
                _userActivity.Dispose();
            }
        }
        #endregion
    }
}