using System;
using System.Windows.Forms;
using Rowan.TfsWorkingOn.WinForm.Properties;
using Microsoft.TeamFoundation.Client;

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
            //connectionBindingSource.DataSource = workingItemConfiguration.Connection;
            workingItemConfigurationBindingSource.DataSource = workingItemConfiguration;

            // TODO: Update with new settings manager
            TfsWorkingOn.Properties.Settings.Default.ConfigurationsPath = Settings.Default.ConfigurationsPath;

            comboBoxTfsServers.Items.AddRange(RegisteredServers.GetServerNames());
            workingItemConfiguration.Connection.Server = Settings.Default.TfsServer;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                workingItemConfiguration.Connection.Connect();

                // TODO: Update with new Settings manager
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
            // TODO: Also Save settings
            Settings.Default.Save();
        }

        private void buttonSetDirectory_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                // TODO: New Settings Manager
                Settings.Default.ConfigurationsPath = folderBrowserDialog.SelectedPath;
                Settings.Default.Save();
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
    }
}
