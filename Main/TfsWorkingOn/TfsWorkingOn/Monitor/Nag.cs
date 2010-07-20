using System;
using System.Globalization;
using System.Threading;
using Rowan.TfsWorkingOn.Properties;
using System.ComponentModel;

namespace Rowan.TfsWorkingOn.Monitor
{
    public class Nag : MonitorBase, IDisposable, IComponent
    {
        private Timer _nagIntervalTimer;

        #region Contructors
        /// <summary>
        /// Constructs a new timer to track the idle/activity status of the user.
        /// Defaults to 60-second idle threshold in a disabled state
        /// </summary>
        public Nag() : this(Settings.Default.NagIntervalMinutes * 60000) { }

        /// <summary>
        /// Constructs a new timer to track the idle/activity status of the user.
        /// Defaults to a disabled state.
        /// </summary>
        /// <param name="interval">Number of milliseconds of inactivity until user is considered idle</param>
        public Nag(int interval)
        {
            Interval = interval;

            // Create underlying timer
            _nagIntervalTimer = new Timer(new TimerCallback(RaiseNag), null, Timeout.Infinite, Timeout.Infinite);
            Enabled = Settings.Default.NagEnabled;
            Started = false;
        }
        #endregion Constructors

        /// <summary>
        /// Gets or sets the amount of time between nags, measured in milliseconds.
        /// </summary>
        public long Interval { get; set; }

        /// <summary>
        /// Stops Nagging
        /// </summary>
        public override void Stop()
        {
            if (Started)
            {
                Started = false;
                _nagIntervalTimer.Change(Timeout.Infinite, Timeout.Infinite);
                OnMonitorStopped(new MonitorEventArgs { MonitorType = GetType(), Reason = Resources.NagMonitorStoppedEventReason, Details = Resources.NagMonitorDetails });
            }
        }

        /// <summary>
        /// Starts Nagging
        /// </summary>
        public override void Start()
        {
            if (Enabled && !Started)
            {
                Started = true;
                _nagIntervalTimer.Change(0, Interval);
                OnMonitorStarted(new MonitorEventArgs { MonitorType = GetType(), Reason = Resources.NagMonitorStartedEventReason, Details = Resources.NagMonitorDetails });
            }
        }

        /// <summary>
        /// Raises Nag at each interval
        /// </summary>
        /// <param name="userState">Ignored - required for ThreadStart delegate</param>
        private void RaiseNag(object userState)
        {
            OnMonitorTriggered(new MonitorEventArgs { MonitorType = GetType(), Reason = Resources.NagTriggeredEventReason, Details = Resources.NagMonitorDetails });
        }


        #region IDisposable Members
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (_nagIntervalTimer != null))
            {
                _nagIntervalTimer.Dispose();
                Disposed(this, EventArgs.Empty);
            }
        }
        #endregion

        #region IComponent Members

        public event EventHandler Disposed;

        public ISite Site { get; set; }

        #endregion
    }
}
