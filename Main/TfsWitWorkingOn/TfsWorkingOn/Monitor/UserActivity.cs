using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Rowan.TfsWorkingOn.Properties;
using System.Globalization;

namespace Rowan.TfsWorkingOn.Monitor
{
    /// <summary>
    /// The user activity monitor monitors the idle time on the computer. When the computer has been idle for a set period of time the user activity monitor is triggered.
    /// </summary>
    public class UserActivity : MonitorBase, IDisposable
    {
        #region Private Members
        private LASTINPUTINFO _lastInput;
        private Timer _activityCheckerTimer;
        private Stopwatch _activityStopWatch = new Stopwatch();
        #endregion

        #region Properties
        /// <summary>
        /// The time the timer was created or last reset
        /// </summary>
        public DateTime LastResetTime { get; private set; }

        /// <summary>
        /// Gets or sets the amount of inactivity until user is considered idle, measured in milliseconds.
        /// </summary>
        public long IdleThreshold { get; set; }

        /// <summary>
        /// Gets the total elapsed time that the user has been active, in milliseconds.
        /// </summary>
        public long ActiveTimeMilliseconds
        {
            get
            {
                return _activityStopWatch.ElapsedMilliseconds;
            }
        }

        /// <summary>
        /// Gets the total elapsed time that the user has been active.
        /// </summary>
        public TimeSpan ActiveTime
        {
            get
            {
                return _activityStopWatch.Elapsed;
            }
        }

        /// <summary>
        /// Gets the current state of the user
        /// </summary>
        public UserActivityState UserActiveState { get; private set; }
        #endregion Properties

        #region Contructors
        /// <summary>
        /// Constructs a new timer to track the idle/activity status of the user.
        /// Defaults to 60-second idle threshold in a disabled state
        /// </summary>
        public UserActivity() : this(Settings.Default.UserActivityIdleTimeoutSeconds * 1000) {}

        /// <summary>
        /// Constructs a new timer to track the idle/activity status of the user.
        /// Defaults to a disabled state.
        /// </summary>
        /// <param name="idleThreshold">Number of milliseconds of inactivity until user is considered idle</param>
        public UserActivity(long idleThreshold)
        {
            IdleThreshold = idleThreshold;

            // Initialize system call
            _lastInput = new LASTINPUTINFO();
            _lastInput.cbSize = (uint)Marshal.SizeOf(_lastInput);

            // Create underlying timer
            _activityCheckerTimer = new Timer(new TimerCallback(GetLastInput), null, Timeout.Infinite, Timeout.Infinite);
            LastResetTime = DateTime.Now;
            Enabled = Settings.Default.MonitorUserActivity;
            Started = false;
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Resets the elapsed activity time to zero and continues measuring.
        /// </summary>
        public void Reset()
        {
            _activityStopWatch.Reset();
            UserActiveState = UserActivityState.Unknown;
            LastResetTime = DateTime.Now;

            if (Enabled && !Started)
            {
                _activityStopWatch.Start();
            }
        }

        /// <summary>
        /// Stops the activity timer
        /// </summary>
        public override void Stop()
        {
            if (Started) 
            {
                Started = false;
                _activityCheckerTimer.Change(Timeout.Infinite, Timeout.Infinite);
                UserActiveState = UserActivityState.Unknown;
                _activityStopWatch.Stop();
                OnMonitorStopped(new MonitorEventArgs { MonitorType = GetType(), Reason = Resources.UserActivityMonitorStoppedEventReason, Details = Resources.UserActivityMonitorDetails });
            }
        }

        /// <summary>
        /// Starts the activity timer
        /// </summary>
        public override void Start()
        {
            if (Enabled && !Started)
            {
                Started = true;
                _activityCheckerTimer.Change(0, 1000);
                _activityStopWatch.Start();
                UserActiveState = UserActivityState.Active;
                OnMonitorStarted(new MonitorEventArgs { MonitorType = GetType(), Reason = Resources.UserActivityMonitorStartedEventReason, Details = Resources.UserActivityMonitorDetails });
            }
        }

        /// <summary>
        /// Checks to see the last activity timestamp and compares is to the idle threshold.
        /// Will trigger appropriate events if state changes.
        /// </summary>
        /// <param name="userState">Ignored - required for ThreadStart delegate</param>
        private void GetLastInput(object userState)
        {
            SafeNativeMethods.GetLastInputInfo(ref _lastInput);

            if ((Environment.TickCount - _lastInput.dwTime) > IdleThreshold)
            {
                if (UserActiveState == UserActivityState.Active)
                {
                    UserActiveState = UserActivityState.Inactive;
                    _activityStopWatch.Stop();
                    OnMonitorTriggered(new MonitorEventArgs { MonitorType = GetType(), Reason = string.Format(CultureInfo.CurrentCulture, Resources.UserActivityMonitorTriggeredEventIdleReason, IdleThreshold / 1000), Details = Resources.UserActivityMonitorDetails });
                }
            }
            else if (UserActiveState == UserActivityState.Inactive)
            {
                UserActiveState = UserActivityState.Active;
                _activityStopWatch.Start();
                OnMonitorTriggered(new MonitorEventArgs { MonitorType = GetType(), Reason = Resources.UserActivityMonitorTriggeredEventActiveReason, Details = Resources.UserActivityMonitorDetails });
            }
        }
        #endregion Methods

        #region IDisposable Members
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (_activityCheckerTimer != null))
            {
                _activityCheckerTimer.Dispose();
            }
        }
        #endregion
    }

    public enum UserActivityState
    {
        Unknown = -1,
        Inactive = 0,
        Active = 1
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LASTINPUTINFO
    {
        public uint cbSize;
        public uint dwTime;
    }

    internal static class SafeNativeMethods
    {
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("User32.dll")]
        internal static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
    }
}