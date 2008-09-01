using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.IO;
using System.Xml.Serialization;

namespace Rowan.TfsWitWorkingOn
{
    public class WorkingItem : INotifyPropertyChanged
    {
        public Connection Connection { get; set; }

        private WorkItem _workItem;
        public WorkItem WorkItem
        {
            get { return _workItem; }
            set
            {
                _workItem = value;
                OnPropertyChanged(new PropertyChangedEventArgs("WorkItem"));
            }
        }

        private DateTime _startTime;
        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                OnPropertyChanged(new PropertyChangedEventArgs("StartTime"));
            }
        }

        private DateTime _stopTime;
        public DateTime StopTime
        {
            get { return _stopTime; }
            set
            {
                _stopTime = value;
                UpdateEstimates();
                OnPropertyChanged(new PropertyChangedEventArgs("StopTime"));
            }
        }

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
                    WorkingItemConfiguration workingItemConfiguration = LoadWorkItemConfiguration();
                    UpdateWorkingOnEstimates();

                }
                return _estimates;
            }
            set
            {
                _estimates = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Estimates"));
            }
        }

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
            TimeSpan interval = StopTime.Subtract(StartTime);
            Estimates.ElapsedTime += interval.TotalHours;
            Estimates.RemainingTime -= interval.TotalHours;
            if (Estimates.RemainingTime < 0) Estimates.RemainingTime = 0d;

            WorkItem.History = "Working On Update:<br/>";
            WorkItem.History += string.Format("Interval: {0:D} hours {1:D} minutes<br/>", interval.Hours, interval.Minutes);
            WorkItem.History += string.Format("Remaining: {0,-4:F} Elapsed: {1,-4:F} Duration: {2,-4:F}", Estimates.RemainingTime, Estimates.ElapsedTime, Estimates.Duration);

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
    }
}