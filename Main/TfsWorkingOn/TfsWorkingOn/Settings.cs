using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Rowan.TfsWorkingOn.UserHistory;

namespace Rowan.TfsWorkingOn
{
    [Serializable]
    public class Settings : INotifyPropertyChanged
    {
        #region Constants

        public const string MonitorUserActivityPropertyName = "MonitorUserActivity";
        public const string NagEnabledPropertyName = "NagEnabled";
        public const string NagIntervalMinutesPropertyName = "NagIntervalMinutes";
        public const string PromptOnResumePropertyName = "PromptOnResume";
        public const string UserActivityIdleTimeoutMinutesPropertyName = "UserActivityIdleTimeoutMinutes";
        public const string ProjectCollectionHistoryPropertyName = "ProjectCollectionHistory";
        
        private const int MaxCollectionCount = 5;

        public const int DefaultBalloonTipTimeoutSeconds = 5;
        public const bool DefaultMonitorUserActivity = true;

        public const bool DefaultNagEnabled = true;
        public const int DefaultNagIntervalMinutes = 5;
        public const bool DefaultPromptOnResume = false;

        public const string DefaultSelectedProjectName = "";
        public const string DefaultTeamProjectCollectionAbsoluteUri = null;
        public const int DefaultUserActivityIdleTimeoutMinutes = 10;

        #endregion

        #region Fields

        private int _balloonTipTimeoutSeconds = DefaultBalloonTipTimeoutSeconds;
        private bool _monitorUserActivity = DefaultMonitorUserActivity;
        private bool _nagEnabled = DefaultNagEnabled;
        private int _nagIntervalMinutes = DefaultNagIntervalMinutes;
        private bool _promptOnResume;
        private int _userActivityIdleTimeoutMinutes = DefaultUserActivityIdleTimeoutMinutes;

        #endregion

        #region Constructor

        public Settings()
        {
            ProjectCollectionHistory.ListChanged += new ListChangedEventHandler(ProjectCollectionHistory_ListChanged);
        }

        #endregion

        private void ProjectCollectionHistory_ListChanged(object sender, ListChangedEventArgs e)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(ProjectCollectionHistoryPropertyName));
        }

        #region Settings Management
        private static Settings _defaultInstance;
        [XmlIgnore]
        public static Settings Default
        {
            get
            {
                Load();
                return _defaultInstance;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static void Load()
        {
            if (_defaultInstance == null && File.Exists(SettingsFilePath))
            {
                try
                {
                    using (FileStream fs = new FileStream(SettingsFilePath, FileMode.OpenOrCreate))
                    {
                        XmlSerializer xs = new XmlSerializerFactory().CreateSerializer(typeof(Settings));
                        if (xs != null) _defaultInstance = xs.Deserialize(fs) as Settings;
                    }
                }
                catch (Exception) { /* Swallowed, not found or load error, just use defaults */ }
            }

            if (_defaultInstance == null)
            {
                _defaultInstance = new Settings();
            }
            _defaultInstance.PropertyChanged += new PropertyChangedEventHandler(_defaultInstance_PropertyChanged);
        }

        static void _defaultInstance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != IsDirtyPropertyName) _defaultInstance.IsDirty = true;
        }

        public void Save()
        {
            if (!Directory.GetParent(SettingsFilePath).Exists) Directory.GetParent(SettingsFilePath).Create();
            using (FileStream fs = new FileStream(SettingsFilePath, FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializerFactory().CreateSerializer(this.GetType());
                if (xs != null) xs.Serialize(fs, this);
            }
            IsDirty = false;
        }

        public static void Reload()
        {
            _defaultInstance = null;
            Load();
            _defaultInstance.IsDirty = false;
        }

        private static string _settingsFilePath;
        private static string SettingsFilePath
        {
            get { return _settingsFilePath ?? (_settingsFilePath = SettingsFolderPath + "Settings.xml"); }
        }

        public static string SettingsFolderPath
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + typeof(Settings).Assembly.GetName().Name + @"\"; }
        }

        public const string IsDirtyPropertyName = "IsDirty";
        private bool _isDirty;
        [XmlIgnore]
        public bool IsDirty
        {
            get { return _isDirty; }
            private set
            {
                if (_isDirty == value) return;
                _isDirty = value;
                OnPropertyChanged(new PropertyChangedEventArgs(IsDirtyPropertyName));
            }
        }
        #endregion Settings Management

        #region Settings

        private static readonly string DefaultConfigurationsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TfsWorkingOn");
        public const string ConfigurationsPathPropertyName = "ConfigurationsPath";
        private string _configurationsPath = DefaultConfigurationsPath;
        [DefaultValue("%APPDATA%\\TfsWorkingOn")]
        public string ConfigurationsPath
        {
            get { return _configurationsPath; }
            set
            {
                _configurationsPath = value;
                OnPropertyChanged(new PropertyChangedEventArgs(ConfigurationsPathPropertyName));
            }
        }

        [DefaultValue(DefaultUserActivityIdleTimeoutMinutes)]
        public int UserActivityIdleTimeoutMinutes
        {
            get { return _userActivityIdleTimeoutMinutes; }
            set
            {
                _userActivityIdleTimeoutMinutes = value;
                OnPropertyChanged(new PropertyChangedEventArgs(UserActivityIdleTimeoutMinutesPropertyName));
            }
        }



        [DefaultValue(DefaultMonitorUserActivity)]
        public bool MonitorUserActivity
        {
            get { return _monitorUserActivity; }
            set
            {
                _monitorUserActivity = value;
                if (!value)
                {
                    PromptOnResume = value;
                }

                OnPropertyChanged(new PropertyChangedEventArgs(MonitorUserActivityPropertyName));
            }
        }

        [DefaultValue(DefaultPromptOnResume)]
        public bool PromptOnResume
        {
            get { return _promptOnResume; }
            set
            {
                _promptOnResume = value;
                OnPropertyChanged(new PropertyChangedEventArgs(PromptOnResumePropertyName));
            }
        }


        [DefaultValue(DefaultBalloonTipTimeoutSeconds)]
        public int BalloonTipTimeoutSeconds
        {
            get { return _balloonTipTimeoutSeconds; }
            set { _balloonTipTimeoutSeconds = value; }
        }
        [DefaultValue(DefaultNagIntervalMinutes)]
        public int NagIntervalMinutes
        {
            get { return _nagIntervalMinutes; }
            set
            {
                _nagIntervalMinutes = value;
                OnPropertyChanged(new PropertyChangedEventArgs(NagIntervalMinutesPropertyName));
            }
        }

        [DefaultValue(DefaultNagEnabled)]
        public bool NagEnabled
        {
            get { return _nagEnabled; }
            set
            {
                _nagEnabled = value;
                OnPropertyChanged(new PropertyChangedEventArgs(NagEnabledPropertyName));
            }
        }

        #endregion

        #region History
        
        [XmlIgnore]
        public ProjectCollectionWorkedOn LastProjectCollectionWorkedOn
        {
            get { return ProjectCollectionHistory.Count == 0 ? null : ProjectCollectionHistory.OrderByDescending(x => x.LastAccessed).First(); }
        }

        private BindingList<ProjectCollectionWorkedOn> _projectCollectionHistory;

        public BindingList<ProjectCollectionWorkedOn> ProjectCollectionHistory
        {
            get { return _projectCollectionHistory ?? (_projectCollectionHistory = new BindingList<ProjectCollectionWorkedOn>()); }
            set
            {
                    _projectCollectionHistory = value;
            }
        }

        #region Instance Methods

        public void SetCollectionLastWorkedOn(string uri)
        {
            var project = (from pr in ProjectCollectionHistory
                           where pr.TeamProjectCollectionAbsoluteUri == uri
                           select pr).FirstOrDefault();

            if (project != null)
            {
                project.LastAccessed = DateTime.Now;
            }
            else
            {
                if (ProjectCollectionHistory.Count == MaxCollectionCount)
                {
                    var oldestProject = (from pr in ProjectCollectionHistory
                                         orderby pr.LastAccessed ascending
                                         select pr).First();

                    ProjectCollectionHistory.Remove(oldestProject);
                }

                ProjectCollectionHistory.Add(new ProjectCollectionWorkedOn(uri));
            }
        }

        #endregion

        #endregion Settings

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
