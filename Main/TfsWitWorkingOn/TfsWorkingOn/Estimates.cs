using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using Rowan.TfsWorkingOn.Properties;

namespace Rowan.TfsWorkingOn
{
    public class Estimates : INotifyPropertyChanged
    {
        public const string DurationPropertyName = "Duration";
        private double _duration;
        public double Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                OnPropertyChanged(new PropertyChangedEventArgs(DurationPropertyName));

                if (RemainingTime == 0)
                {
                    RemainingTime = _duration - ElapsedTime;
                }
            }
        }

        public const string RemainingTimePropertyName = "RemainingTime";
        private double _remainingTime;
        public double RemainingTime
        {
            get { return _remainingTime; }
            set
            {
                _remainingTime = value;
                OnPropertyChanged(new PropertyChangedEventArgs(RemainingTimePropertyName));
            }
        }

        public const string ElapsedTimePropertyName = "ElapsedTime";
        private double _elapsedTime;
        public double ElapsedTime
        {
            get { return _elapsedTime; }
            set
            {
                _elapsedTime = value;
                OnPropertyChanged(new PropertyChangedEventArgs(ElapsedTimePropertyName));
            }
        }

        internal void Load(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                XmlSerializer xs = new XmlSerializerFactory().CreateSerializer(this.GetType());
                Estimates estimates = xs.Deserialize(fs) as Estimates;
                if (estimates != null)
                {
                    this.Duration = estimates.Duration;
                    this.RemainingTime = estimates.RemainingTime;
                    this.ElapsedTime = estimates.ElapsedTime;
                }
            }
        }

        internal void Save(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializerFactory().CreateSerializer(this.GetType());
                xs.Serialize(fs, this);
            }
        }

        internal static string GetFilePath(string server, int workItemId)
        {
            string filename = string.Format(CultureInfo.InvariantCulture, Resources.FileName, server, workItemId);
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Resources.DefaultDirectory);
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
            filePath = Path.Combine(filePath, filename);
            return filePath;
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
    }
}
