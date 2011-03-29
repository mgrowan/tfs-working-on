﻿using System;
using System.ComponentModel;
using System.Linq;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Rowan.TfsWorkingOn.Properties;

namespace Rowan.TfsWorkingOn
{
    public class Connection : INotifyPropertyChanged
    {
        #region Constructors
        private Connection() { }

        private static Connection _connection = null;
        public static Connection GetConnection()
        {
            if (_connection == null) _connection = new Connection();
            return _connection;
        }
        #endregion

        #region Properties
        private TfsTeamProjectCollection _tfsTeamProjectCollection;
        public TfsTeamProjectCollection TfsTeamProjectCollection
        {
            get
            {
                if (_tfsTeamProjectCollection == null) Connect();
                return _tfsTeamProjectCollection;
            }
        }

        public const string TeamProjectCollectionAbsoluteUriPropertyName = "TeamProjectCollectionAbsoluteUri";
        private string _teamProjectCollectionAbsoluteUri;
        public string TeamProjectCollectionAbsoluteUri
        {
            get { return _teamProjectCollectionAbsoluteUri; }
            set
            {
                if (value != _teamProjectCollectionAbsoluteUri)
                {
                    _teamProjectCollectionAbsoluteUri = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(TeamProjectCollectionAbsoluteUriPropertyName));
                }
            }
        }

        public Uri TeamProjectCollectionUri { get { return new Uri(TeamProjectCollectionAbsoluteUri); } }

        public bool IsConnected { get { return TfsTeamProjectCollection != null; } }

        private WorkItemStore _workItemStore;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public WorkItemStore WorkItemStore
        {
            get
            {
                if (_tfsTeamProjectCollection == null) Connect();
                return _workItemStore;
            }
        }

        private string _selectedProjectName = string.Empty;
        public const string SelectedProjectNamePropertyName = "SelectedProjectName";
        public string SelectedProjectName
        {
            get { return _selectedProjectName; }
            set
            {
                if (_selectedProjectName != value)
                {
                    _selectedProjectName = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(SelectedProjectNamePropertyName));
                }
            }
        }

        private Project _selectedProject = null;
        public Project SelectedProject
        {
            get
            {
                if (_selectedProject == null)
                {
                    _selectedProject = WorkItemStore.Projects.OfType<Project>().FirstOrDefault(p => p.Name == SelectedProjectName);
                }
                return _selectedProject;
            }
        }
        #endregion Properties

        #region Public Methods
        /// <summary>
        /// Connects to Server and populates to Project list
        /// </summary>
        public static void Connect()
        {
            if (Settings.Default.TeamProjectCollectionAbsoluteUri == null) throw new ArgumentNullException(Resources.Server, Resources.ServerRequired);
            // Discard previous connection and reconnect
            _connection = null;
            GetConnection().TeamProjectCollectionAbsoluteUri = Settings.Default.TeamProjectCollectionAbsoluteUri;
            _connection._selectedProjectName = Settings.Default.SelectedProjectName;
            try
            {
                _connection._tfsTeamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(_connection.TeamProjectCollectionUri, new UICredentialsProvider());
                _connection._tfsTeamProjectCollection.EnsureAuthenticated();
                _connection._workItemStore = _connection.TfsTeamProjectCollection.GetService(typeof(WorkItemStore)) as WorkItemStore;
            }
            catch (Exception)
            {
                throw;
            }
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
