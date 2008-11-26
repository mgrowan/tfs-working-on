using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Rowan.TfsWorkingOn.Properties;

namespace Rowan.TfsWorkingOn
{
    public class WorkingItemConfiguration : INotifyPropertyChanged
    {
        #region Members
        private TeamFoundationServer _tfsServer;
        private WorkItemStore _workItemStore;
        private string _filename = string.Empty;
        #endregion

        #region Properties
        public const string ServerPropertyName = "Server";
        private string _server;
        public string Server
        {
            get { return _server; }
            set
            {
                _server = value;
                OnPropertyChanged(new PropertyChangedEventArgs(ServerPropertyName));
            }
        }

        public const string PortPropertyName = "Port";
        private int _port = 8080;
        public int Port
        {
            get { return _port; }
            set
            {
                _port = value;
                OnPropertyChanged(new PropertyChangedEventArgs(PortPropertyName));
            }
        }

        public const string IsConnectedPropertyName = "IsConnected";
        private bool _isConnected;
        [XmlIgnore]
        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                OnPropertyChanged(new PropertyChangedEventArgs(IsConnectedPropertyName));
            }
        }

        public const string ProjectsPropertyName = "Projects";
        private ProjectCollection _projects;
        [XmlIgnore]
        public ProjectCollection Projects
        {
            get { return _projects; }
            private set
            {
                _projects = value;
                OnPropertyChanged(new PropertyChangedEventArgs(ProjectsPropertyName));
                SelectedProject = value[0];
            }
        }

        public const string SelectedProjectPropertyName = "SelectedProject";
        public int SelectedProjectId { get; set; }
        public string SelectedProjectName { get; set; }
        private Project _selectedProject;
        [XmlIgnore]     
        public Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                SelectedProjectName = value.Name;
                SelectedProjectId = value.Id;
                OnPropertyChanged(new PropertyChangedEventArgs(SelectedProjectPropertyName));
            }
        }

        public string SelectedWorkItemTypeName { get; set; }
        public const string SelectedWorkItemTypePropertyName = "SelectedWorkItemType";
        private WorkItemType _selectedWorkItemType;
        [XmlIgnore]
        public WorkItemType SelectedWorkItemType
        {
            get { return _selectedWorkItemType; }
            set
            {
                _selectedWorkItemType = value;
                SelectedWorkItemTypeName = value.Name;
                Load();
                OnPropertyChanged(new PropertyChangedEventArgs(SelectedWorkItemTypePropertyName));
            }
        }

        /// <summary>
        /// Specify Duration, Remaining, Elapsed Time Fields
        /// Non mapped fields are tracked by the WorkingItem and input into the work item history
        /// </summary>
        public const string DurationFieldPropertyName = "DurationField";
        private string _durationField = string.Empty;
        public string DurationField
        {
            get { return _durationField; }
            set
            {
                _durationField = value;
                OnPropertyChanged(new PropertyChangedEventArgs(DurationFieldPropertyName));
            }
        }

        public const string RemainingFieldPropertyName = "RemainingField";
        private string _remainingField = string.Empty;
        public string RemainingField
        {
            get { return _remainingField; }
            set
            {
                _remainingField = value;
                OnPropertyChanged(new PropertyChangedEventArgs(RemainingFieldPropertyName));
            }
        }

        public const string ElapsedFieldPropertyName = "ElapsedField";
        private string _elapsedField = string.Empty;
        public string ElapsedField
        {
            get { return _elapsedField; }
            set
            {
                _elapsedField = value;
                OnPropertyChanged(new PropertyChangedEventArgs(ElapsedFieldPropertyName));
            }
        }
        
        #endregion

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

        #region Public Methods
        /// <summary>
        /// Connects to Server and populates to Project list
        /// </summary>
        public void Connect()
        {
            #region Parameter Check
            if (string.IsNullOrEmpty(Server))
            {
                throw new ArgumentNullException(Resources.Server, Resources.ServerRequired);
            }
            #endregion

            try
            {
                _tfsServer = TeamFoundationServerFactory.GetServer(string.Format(CultureInfo.InvariantCulture, "http://{0}:{1}", Server, Port.ToString(CultureInfo.InvariantCulture)), new UICredentialsProvider());
                _tfsServer.EnsureAuthenticated();
                
                _workItemStore = _tfsServer.GetService(typeof(WorkItemStore)) as WorkItemStore;
                Projects = _workItemStore.Projects;

                IsConnected = true;
            }
            catch (Exception)
            {
                IsConnected = false;
                if (Projects != null) Projects = null;
                throw;
            }
        }

        public void Disconnect()
        {
            Server = string.Empty;
            IsConnected = false;
        }
        
        public void Save()
        {
            if (HasNoFileContents()) return;

            using (FileStream fs = new FileStream(Path.Combine(Settings.Default.ConfigurationsPath, _filename), FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializerFactory().CreateSerializer(this.GetType());
                xs.Serialize(fs, this);
            }
        }
        
        public void Load()
        {
            if (HasRequiredPropertiesMissing()) return;

            _filename = string.Format(CultureInfo.InvariantCulture, Resources.ConfigFileName, Server, SelectedProject.Name, SelectedWorkItemType.Name);
            
            string filePath = Path.Combine(Settings.Default.ConfigurationsPath, _filename);
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    XmlSerializer xs = new XmlSerializerFactory().CreateSerializer(this.GetType());
                    WorkingItemConfiguration wic = xs.Deserialize(fs) as WorkingItemConfiguration;
                    if (wic != null)
                    {
                        this.DurationField = wic.DurationField;
                        this.RemainingField = wic.RemainingField;
                        this.ElapsedField = wic.ElapsedField;
                    }
                }
            }
            else
            {
                this.DurationField = this.RemainingField = this.ElapsedField = string.Empty;
            }
        }
        #endregion

        #region Private Methods
        private bool HasNoFileContents()
        {
            return string.IsNullOrEmpty(DurationField) && string.IsNullOrEmpty(RemainingField) && string.IsNullOrEmpty(ElapsedField);
        }
        private bool HasRequiredPropertiesMissing()
        {
            return string.IsNullOrEmpty(Server) || SelectedProject == null || SelectedWorkItemType == null;
        }
        #endregion

    }
}