using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Client;
using Rowan.TfsWorkingOn.TfsWarehouse;
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
            toolTipHelp.SetToolTip(pictureBoxHelpPromptOnResume, Resources.HelpPromptOnResume);
            toolTipHelp.SetToolTip(pictureBoxHelpNag, Resources.HelpNag);

            labelVersion.Text = Assembly.GetExecutingAssembly().GetCustomAttributes(true).OfType<AssemblyInformationalVersionAttribute>().FirstOrDefault().InformationalVersion;

            workingItemConfiguration.WarehouseController.GetWarehouseStatusCompleted += new GetWarehouseStatusCompletedEventHandler(WarehouseController_GetWarehouseStatusCompleted);
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

        private void buttonRefreshWarehouseStatus_Click(object sender, EventArgs e)
        {
            workingItemConfiguration.WarehouseController.GetWarehouseStatusAsync();
        }

        private void WarehouseController_GetWarehouseStatusCompleted(object sender, GetWarehouseStatusCompletedEventArgs e)
        {
            textBoxWarehouseStatus.Text = e.Result.ToString();
        }

        #region Async Data Warehouse Processing
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void buttonUpdateWarehouse_Click(object sender, EventArgs e)
        {
            progressBarWarehouse.MarqueeAnimationSpeed = 1;
            buttonUpdateWarehouse.Enabled = false;
            try
            {
                _idle = false;
                backgroundWorker.RunWorkerAsync();
                workingItemConfiguration.WarehouseController.RunAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonUpdateWarehouse.Enabled = true;
                progressBarWarehouse.MarqueeAnimationSpeed = 0;
            }
        }

        bool _idle;
        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (!_idle)
            {
                Thread.Sleep(1000);
                backgroundWorker.ReportProgress(1);
            }
            backgroundWorker.ReportProgress(100);
        }

        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            WarehouseStatus ws = workingItemConfiguration.WarehouseController.GetWarehouseStatus();
            textBoxWarehouseStatus.Text = ws.ToString();
            _idle = (ws == WarehouseStatus.Idle);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            textBoxWarehouseStatus.Text = workingItemConfiguration.WarehouseController.GetWarehouseStatus().ToString();
            buttonUpdateWarehouse.Enabled = true;
            progressBarWarehouse.MarqueeAnimationSpeed = 0;
        }
        #endregion Async Data Warehouse Processing
    }
}
