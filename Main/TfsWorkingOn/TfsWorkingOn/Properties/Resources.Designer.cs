﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rowan.TfsWorkingOn.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Rowan.TfsWorkingOn.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} - {1} - {2}.WorkingItemConfig.
        /// </summary>
        internal static string ConfigFileName {
            get {
                return ResourceManager.GetString("ConfigFileName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to TFS Working On.
        /// </summary>
        internal static string DefaultDirectory {
            get {
                return ResourceManager.GetString("DefaultDirectory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} - {1}.WorkingOn.
        /// </summary>
        internal static string FileName {
            get {
                return ResourceManager.GetString("FileName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Server.
        /// </summary>
        internal static string Server {
            get {
                return ResourceManager.GetString("Server", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Server must be specified before connecting..
        /// </summary>
        internal static string ServerRequired {
            get {
                return ResourceManager.GetString("ServerRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user activity monitor monitors the idle time on the computer. When the computer has been idle for a set period of time the user activity monitor is triggered..
        /// </summary>
        internal static string UserActivityMonitorDetails {
            get {
                return ResourceManager.GetString("UserActivityMonitorDetails", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user activity monitor was manually started..
        /// </summary>
        internal static string UserActivityMonitorStartedEventReason {
            get {
                return ResourceManager.GetString("UserActivityMonitorStartedEventReason", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user activity monitor was manually stopped..
        /// </summary>
        internal static string UserActivityMonitorStoppedEventReason {
            get {
                return ResourceManager.GetString("UserActivityMonitorStoppedEventReason", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user has resumed activity..
        /// </summary>
        internal static string UserActivityMonitorTriggeredEventActiveReason {
            get {
                return ResourceManager.GetString("UserActivityMonitorTriggeredEventActiveReason", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user has been inactive for over {0} seconds..
        /// </summary>
        internal static string UserActivityMonitorTriggeredEventIdleReason {
            get {
                return ResourceManager.GetString("UserActivityMonitorTriggeredEventIdleReason", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Working On Update:&lt;br/&gt;.
        /// </summary>
        internal static string WorkingOnUpdate {
            get {
                return ResourceManager.GetString("WorkingOnUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Interval: {0:D} hours {1:D} minutes&lt;br/&gt;.
        /// </summary>
        internal static string WorkingOnUpdateInterval {
            get {
                return ResourceManager.GetString("WorkingOnUpdateInterval", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Remaining: {0,-4:F} Elapsed: {1,-4:F} Duration: {2,-4:F}.
        /// </summary>
        internal static string WorkingOnUpdateStatus {
            get {
                return ResourceManager.GetString("WorkingOnUpdateStatus", resourceCulture);
            }
        }
    }
}