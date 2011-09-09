using System;
using System.ComponentModel;

namespace Rowan.TfsWorkingOn.UserHistory
{
    [Serializable]
    public class ProjectWorkedOn : UserWorkedOnItem
    {
        #region Field

        #endregion

        #region Constants

        public const string LastQueryWorkedOnFieldName = "LastQueryWorkedOn";

        #endregion

        #region Constructors

        private ProjectWorkedOn()
        {
        }

        public ProjectWorkedOn(string projectName) : this()
        {
            this.ProjectName = projectName;
            this.LastAccessed = DateTime.Now;
        }

        #endregion

        #region Instance Properties

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>
        /// The name of the project.
        /// </value>
        [DefaultValue(Settings.DefaultSelectedProjectName)]
        public string ProjectName { get; set; }

        private Guid? lastQueryWorkedOn;

        /// <summary>
        /// Gets or sets the last query worked on.
        /// </summary>
        /// <value>
        /// The last query worked on.
        /// </value>
        public Guid? LastQueryWorkedOn
        {
            get { return lastQueryWorkedOn; }
            set
            {
                if (lastQueryWorkedOn != value)
                {
                    lastQueryWorkedOn = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(LastQueryWorkedOnFieldName));
                }
            }
        }

        #endregion
    }
}