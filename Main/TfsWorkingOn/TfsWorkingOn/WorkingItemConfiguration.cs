using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Rowan.TfsWorkingOn.Properties;
using Rowan.TfsWorkingOn.TfsWarehouse;

namespace Rowan.TfsWorkingOn
{
    public class WorkingItemConfiguration : INotifyPropertyChanged, IDisposable
    {
        #region Members
        private string _filename = string.Empty;
        #endregion

        #region Properties
        public const string ConnectionPropertyName = "Connection";
        private Connection _connection = new Connection();
        public Connection Connection
        {
            get { return _connection; }
            set
            {
                _connection = value;
                OnPropertyChanged(new PropertyChangedEventArgs(ConnectionPropertyName));
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
                if (_selectedWorkItemType != value)
                {
                    _selectedWorkItemType = value;
                    SelectedWorkItemTypeName = value.Name;
                    Load();
                    OnPropertyChanged(new PropertyChangedEventArgs(SelectedWorkItemTypePropertyName));
                }
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

        private ControllerService _warehouseController;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        [XmlIgnore]
        public ControllerService WarehouseController
        {
            get
            {
                if (_warehouseController == null)
                {
                    _warehouseController = new ControllerService();
                    _warehouseController.Url = string.Format("{0}warehouse/v1.0/warehousecontroller.asmx", TeamFoundationServerFactory.GetServer(Connection.Server).Uri.AbsoluteUri);
                    _warehouseController.UseDefaultCredentials = true;
                }
                return _warehouseController;
            }
        }

        #endregion

        public WorkingItemConfiguration()
        {
            this.Connection.PropertyChanged += new PropertyChangedEventHandler(Connection_PropertyChanged);
        }

        void Connection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Connection.SelectedProjectPropertyName) SelectedWorkItemType = Connection.SelectedProject.WorkItemTypes[0];
            OnPropertyChanged(e);
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

        #region Public Methods
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

            _filename = string.Format(CultureInfo.InvariantCulture, Resources.ConfigFileName, Connection.ServerInstanceId.ToString("N"), Connection.SelectedProject.Name, SelectedWorkItemType.Name);

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
            return string.IsNullOrEmpty(Connection.Server) || Connection.SelectedProject == null || SelectedWorkItemType == null;
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (_warehouseController != null))
            {
                _warehouseController.Dispose();
            }
        }
        #endregion
    }
}