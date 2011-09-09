using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace Rowan.TfsWorkingOn.UserHistory
{
    [Serializable]
    public class ProjectCollectionWorkedOn : UserWorkedOnItem
    {
        #region Fields

        private string _teamProjectCollectionAbsoluteUri = Settings.DefaultTeamProjectCollectionAbsoluteUri;

        #endregion

        #region Constants

        private const int MaxProjectCount = 5;

        public const string ProjectHistoryPropertyName = "ProjectHistory";
        public const string TeamProjectCollectionAbsoluteUriPropertyName = "TeamProjectCollectionAbsoluteUri";

        #endregion

        #region Constructors

        private ProjectCollectionWorkedOn()
        {
            ProjectHistory.ListChanged += new ListChangedEventHandler(ProjectHistory_ListChanged);
        }

        public ProjectCollectionWorkedOn(string collectionUri) : this()
        {
            this.TeamProjectCollectionAbsoluteUri = collectionUri;
            this.LastAccessed = DateTime.Now;
        }

        #endregion

        private void ProjectHistory_ListChanged(object sender, ListChangedEventArgs e)
        {
            OnPropertyChanged(sender, new PropertyChangedEventArgs(ProjectHistoryPropertyName));
        }

        #region Instance Properties

        [DefaultValue(Settings.DefaultTeamProjectCollectionAbsoluteUri)]
        public string TeamProjectCollectionAbsoluteUri
        {
            get { return _teamProjectCollectionAbsoluteUri; }
            set
            {
                if (_teamProjectCollectionAbsoluteUri == value) return;
                
                _teamProjectCollectionAbsoluteUri = value;
                OnPropertyChanged(new PropertyChangedEventArgs(TeamProjectCollectionAbsoluteUriPropertyName));
            }
        }

        [XmlIgnore]
        public ProjectWorkedOn LastProjectWorkedOn
        {
            get { return ProjectHistory.Count == 0 ? null : ProjectHistory.OrderByDescending(x => x.LastAccessed).First(); }
        }

        private BindingList<ProjectWorkedOn> _projectHistory;
        public BindingList<ProjectWorkedOn> ProjectHistory
        {
            get { return _projectHistory ?? (_projectHistory = new BindingList<ProjectWorkedOn>()); }
            set
            {
                _projectHistory = value;
            }
        }

        #endregion

        #region Instance Methods

        public void SetProjectLastWorkedOn(string projectName)
        {
            var project = (from pr in ProjectHistory
                          where pr.ProjectName == projectName
                          select pr).FirstOrDefault();

            if (project != null)
            {
                project.LastAccessed = DateTime.Now;
            }
            else
            {
                if (ProjectHistory.Count == MaxProjectCount)
                {
                    var oldestProject = (from pr in ProjectHistory
                                         orderby pr.LastAccessed ascending
                                         select pr).First();

                    ProjectHistory.Remove(oldestProject);
                }

                ProjectHistory.Add(new ProjectWorkedOn(projectName));
            }
        }

        #endregion
    }
}