using System;
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

        private Uri TeamProjectCollectionUri
        {
            get
            {
                return new Uri(TeamProjectCollectionAbsoluteUri);
            }
        }

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
                if (_selectedProjectName == value) return;
                _selectedProjectName = value;
                OnPropertyChanged(new PropertyChangedEventArgs(SelectedProjectNamePropertyName));
            }
        }

        private Project _selectedProject = null;
        public Project SelectedProject
        {
            get 
            {
                return _selectedProject ?? (_selectedProject = WorkItemStore.Projects.OfType<Project>().FirstOrDefault(p => p.Name == SelectedProjectName));
            }
        }
        #endregion Properties

        #region Public Methods
        /// <summary>
        /// Connects to Server and populates to Project list
        /// </summary>
        public static void Connect()
        {
            if (_connection.TeamProjectCollectionAbsoluteUri == null) throw new ArgumentNullException(Resources.Server, Resources.ServerRequired);

            string collectionUri = _connection.TeamProjectCollectionAbsoluteUri;
            string projectName = _connection.SelectedProjectName;
            PropertyChangedEventHandler _propertyChanged = _connection.PropertyChanged;

            // Discard previous connection and reconnect
            _connection = null;
            GetConnection();

            _connection._tfsTeamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(collectionUri), new UICredentialsProvider());
            _connection._tfsTeamProjectCollection.EnsureAuthenticated();
            _connection._workItemStore = _connection.TfsTeamProjectCollection.GetService<WorkItemStore>();

            _connection.PropertyChanged = _propertyChanged;
            _connection.TeamProjectCollectionAbsoluteUri = collectionUri;
            _connection.SelectedProjectName = projectName;
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
