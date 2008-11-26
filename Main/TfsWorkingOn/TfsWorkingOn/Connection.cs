using System;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Rowan.TfsWorkingOn.Properties;

namespace Rowan.TfsWorkingOn
{
    public class Connection : INotifyPropertyChanged
    {
        #region Members
        private TeamFoundationServer _tfsServer;
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

        private WorkItemStore _workItemStore;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public WorkItemStore WorkItemStore
        {
            get
            {
                if (_workItemStore == null)
                {
                    try
                    {
                        Connect();
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                return _workItemStore;
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

        public int SelectedProjectId { get; set; }
        public string SelectedProjectName { get; set; }
        public const string SelectedProjectPropertyName = "SelectedProject";
        private Project _selectedProject;
        [XmlIgnore]
        public Project SelectedProject
        {
            get { return _selectedProject == null ? _workItemStore.Projects[SelectedProjectName] : _selectedProject; }
            set
            {
                _selectedProject = value;
                SelectedProjectName = value.Name;
                SelectedProjectId = value.Id;
                OnPropertyChanged(new PropertyChangedEventArgs(SelectedProjectPropertyName));
            }
        }

        public string AuthenticatedUserName
        {
            get { return _tfsServer.AuthenticatedUserName; }
        }
        #endregion Properties

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
                _tfsServer = TeamFoundationServerFactory.GetServer(Server, new UICredentialsProvider());
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
        #endregion Public Methods
        
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
    }
}
