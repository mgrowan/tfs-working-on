using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Rowan.TfsWitWorkingOn
{
    public class Connection : INotifyPropertyChanged
    {
        #region Members
        private TeamFoundationServer _tfsServer = null;
        #endregion

        #region Properties
        private string _server;
        public string Server
        {
            get { return _server; }
            set
            {
                _server = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Server"));
            }
        }

        private int _port = 8080;
        public int Port
        {
            get { return _port; }
            set
            {
                _port = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Port"));
            }
        }

        private bool _isConnected = false;
        [XmlIgnore]
        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsConnected"));
            }
        }

        private WorkItemStore _workItemStore = null;
        public WorkItemStore WorkItemStore
        {
            get
            {
                if (_workItemStore == null) Connect();
                return _workItemStore;
            }
        }


        private ProjectCollection _projects;
        [XmlIgnore]
        public ProjectCollection Projects
        {
            get { return _projects; }
            set
            {
                _projects = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Projects"));
                SelectedProject = value[0];
            }
        }

        public int SelectedProjectId { get; set; }
        public string SelectedProjectName { get; set; }
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
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedProject"));
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
                throw new ArgumentNullException("Server", "Server must be set before calling this method");
            }
            #endregion

            try
            {
                _tfsServer = TeamFoundationServerFactory.GetServer(string.Format("http://{0}:{1}", Server, Port.ToString()), new UICredentialsProvider());
                _tfsServer.EnsureAuthenticated();
                // TODO: Check Connected
                _workItemStore = _tfsServer.GetService(typeof(WorkItemStore)) as WorkItemStore;
                Projects = _workItemStore.Projects;

                IsConnected = true;
            }
            catch (Exception)
            {
                IsConnected = false;
                Projects = null;
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
