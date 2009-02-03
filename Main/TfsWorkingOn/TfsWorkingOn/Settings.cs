using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace Rowan.TfsWorkingOn
{
    [Serializable]
    public class Settings : INotifyPropertyChanged
    {
        #region Settings Management
        private static Settings _defaultInstance;
        [XmlIgnore, System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static Settings Default
        {
            get
            {
                if (_defaultInstance == null && File.Exists(SettingsFilePath))
                {
                    try
                    {
                        using (FileStream fs = new FileStream(SettingsFilePath, FileMode.OpenOrCreate))
                        {
                            XmlSerializer xs = new XmlSerializerFactory().CreateSerializer(typeof(Settings));
                            _defaultInstance = xs.Deserialize(fs) as Settings;
                        }
                    }
                    catch (Exception) { /* Swallowed, not found or load error, just use defaults */ }
                }

                if (_defaultInstance == null)
                {
                    _defaultInstance = new Settings();
                }
                return _defaultInstance;
            }
        }

        public void Save()
        {
            using (FileStream fs = new FileStream(SettingsFilePath, FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializerFactory().CreateSerializer(this.GetType());
                xs.Serialize(fs, this);
            }
        }

        private static string _settingsFilePath;
        private static string SettingsFilePath
        {
            get
            {
                if (_settingsFilePath == null)
                {
                    _settingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + typeof(Settings).Assembly.GetName().Name + " Settings.xml";
                }
                return _settingsFilePath;
            }
        }
        #endregion Settings Management

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

        #region Settings

        private const string DefaultConfigurationsPath = "";
        public const string ConfigurationsPathPropertyName = "ConfigurationsPath";
        private string _configurationsPath = DefaultConfigurationsPath;
        [DefaultValue(DefaultConfigurationsPath)]
        public string ConfigurationsPath
        {
            get { return _configurationsPath; }
            set
            {
                _configurationsPath = value;
                OnPropertyChanged(new PropertyChangedEventArgs(ConfigurationsPathPropertyName));
            }
        }

        private const string DefaultTfsServer = "";
        private string _tfsServer = DefaultConfigurationsPath;
        [DefaultValue(DefaultTfsServer)]
        public string TfsServer
        {
            get { return _tfsServer; }
            set { _tfsServer = value; }
        }

        private const int DefaultSelectedProjectId = -1;
        private int _selectedProjectId = DefaultSelectedProjectId;
        [DefaultValue(DefaultSelectedProjectId)]
        public int SelectedProjectId
        {
            get { return _selectedProjectId; }
            set { _selectedProjectId = value; }
        }

        private const string DefaultSelectedProjectName = "";
        private string _selectedProjectName = DefaultSelectedProjectName;
        [DefaultValue(DefaultSelectedProjectName)]
        public string SelectedProjectName
        {
            get { return _selectedProjectName; }
            set { _selectedProjectName = value; }
        }

        private const int DefaultUserActivityIdleTimeoutMinutes = 10;
        public const string UserActivityIdleTimeoutMinutesPropertyName = "UserActivityIdleTimeoutMinutes";
        private int _userActivityIdleTimeoutMinutes = DefaultUserActivityIdleTimeoutMinutes;
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

        private const bool DefaultMonitorUserActivity = true;
        public const string MonitorUserActivityPropertyName = "MonitorUserActivity";
        private bool _monitorUserActivity = DefaultMonitorUserActivity;
        [DefaultValue(DefaultMonitorUserActivity)]
        public bool MonitorUserActivity
        {
            get { return _monitorUserActivity; }
            set
            {
                _monitorUserActivity = value;
                if (!value) PromptOnResume = value;
                OnPropertyChanged(new PropertyChangedEventArgs(MonitorUserActivityPropertyName));
            }
        }

        private const bool DefaultPromptOnResume = false;
        public const string PromptOnResumePropertyName = "PromptOnResume";
        private bool _promptOnResume;
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

        private const int DefaultBalloonTipTimeoutSeconds = 5;
        private int _balloonTipTimeoutSeconds = DefaultBalloonTipTimeoutSeconds;
        [DefaultValue(DefaultBalloonTipTimeoutSeconds)]
        public int BalloonTipTimeoutSeconds
        {
            get { return _balloonTipTimeoutSeconds; }
            set { _balloonTipTimeoutSeconds = value; }
        }

        private const int DefaultNagIntervalMinutes = 5;
        public const string NagIntervalMinutesPropertyName = "NagIntervalMinutes";
        private int _nagIntervalMinutes = DefaultNagIntervalMinutes;
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

        private const bool DefaultNagEnabled = true;
        public const string NagEnabledPropertyName = "NagEnabled";
        private bool _nagEnabled = DefaultNagEnabled;
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

        #endregion Settings
    }
}
