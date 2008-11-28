using System;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Client;
using Rowan.TfsWorkingOn.WinForm.Properties;

namespace Rowan.TfsWorkingOn.WinForm
{
    public partial class FormWorkItemConfiguration : Form
    {
        private WorkingItemConfiguration workingItemConfiguration = new WorkingItemConfiguration();

        public FormWorkItemConfiguration()
        {
            InitializeComponent();
        }

        private void FormWorkItemConfiguration_Load(object sender, EventArgs e)
        {
            workingItemConfigurationBindingSource.DataSource = workingItemConfiguration;
            settingsBindingSource.DataSource = Settings.Default;

            comboBoxTfsServers.Items.AddRange(RegisteredServers.GetServerNames());
            workingItemConfiguration.Connection.Server = Settings.Default.TfsServer;
            
            toolTipHelp.SetToolTip(pictureBoxHelpUserActivity, Resources.HelpActivityMonitor);
            toolTipHelp.SetToolTip(pictureBoxHelpNag, Resources.HelpNag);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                workingItemConfiguration.Connection.Connect();
                Settings.Default.TfsServer = workingItemConfiguration.Connection.Server;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ConnectionFailedTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            workingItemConfiguration.Save();
            Settings.Default.Save();
        }

        private void buttonSetDirectory_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                Settings.Default.ConfigurationsPath = folderBrowserDialog.SelectedPath;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabelAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabelAbout.Text);
        }

        private void tabControlConfiguration_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tabPageMappings && string.IsNullOrEmpty(Settings.Default.ConfigurationsPath))
            {
                MessageBox.Show(Resources.MappingPath, Resources.MappingPathTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabControlConfiguration.SelectedTab = tabPageOptions;
            }
        }
    }
}
