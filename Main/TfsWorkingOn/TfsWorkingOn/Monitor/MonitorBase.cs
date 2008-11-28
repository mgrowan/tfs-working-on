using System;
using System.Diagnostics;
using System.Globalization;

namespace Rowan.TfsWorkingOn.Monitor
{
    public abstract class MonitorBase
    {
        public bool Enabled { get; set; }
        public bool Started { get; protected set; }

        public abstract void Start();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Stop")]
        public abstract void Stop();

        #region Events
        public event EventHandler<MonitorEventArgs> MonitorStartedEvent;
        public event EventHandler<MonitorEventArgs> MonitorStoppedEvent;
        public event EventHandler<MonitorEventArgs> MonitorTriggeredEvent;
        #endregion Events

        #region Raise Events

        protected virtual void OnMonitorStarted(MonitorEventArgs monitorEventArgs)
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Monitor {0} Started Event, Reason: {1}", monitorEventArgs.MonitorType, monitorEventArgs.Reason));
            var handler = MonitorStartedEvent;
            if (handler != null)
            {
                MonitorStartedEvent(this, monitorEventArgs);
            }
        }

        protected virtual void OnMonitorStopped(MonitorEventArgs monitorEventArgs)
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Monitor {0} Stopped Event, Reason: {1}", monitorEventArgs.MonitorType, monitorEventArgs.Reason));
            var handler = MonitorStoppedEvent;
            if (handler != null)
            {
                MonitorStoppedEvent(this, monitorEventArgs);
            }
        }

        protected virtual void OnMonitorTriggered(MonitorEventArgs monitorEventArgs)
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Monitor {0} Triggered Event, Reason: {1}", monitorEventArgs.MonitorType, monitorEventArgs.Reason));
            var handler = MonitorTriggeredEvent;
            if (handler != null)
            {
                MonitorTriggeredEvent(this, monitorEventArgs);
            }
        }
        #endregion Raise Events

    }

    public class MonitorEventArgs : EventArgs
    {
        /// <summary>
        /// Specifies the type of the monitor to allow casting
        /// </summary>
        public Type MonitorType { get; set; }
        /// <summary>
        /// Cause of the event being raised
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// Specifies information about the Monitor type
        /// </summary>
        public string Details { get; set; }
    }
}
