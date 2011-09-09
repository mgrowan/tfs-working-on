using System;
using System.ComponentModel;

namespace Rowan.TfsWorkingOn.UserHistory
{
    public class UserWorkedOnItem : INotifyPropertyChanged
    {
        #region Constants

        public const string QueryLastAccessedPropertyName = "LastAccessed";

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            OnPropertyChanged(this, e);
        }

        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        #endregion

        private DateTime _lastAccessed;

        public DateTime LastAccessed
        {
            get { return _lastAccessed; }
            set
            {
                if (_lastAccessed == value) return;

                _lastAccessed = value;
                OnPropertyChanged(new PropertyChangedEventArgs(QueryLastAccessedPropertyName));
            }
        }
    }
}
