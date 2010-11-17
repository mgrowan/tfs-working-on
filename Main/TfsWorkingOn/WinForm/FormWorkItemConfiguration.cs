using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Client;
using Rowan.TfsWorkingOn.TfsWarehouse;
using Rowan.TfsWorkingOn.WinForm.Properties;
using System.Web.Services.Protocols;

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
            comboBoxMenuQuery.DataSource = workingItemConfiguration.Connection.WorkItemStore.Projects[Settings.Default.SelectedProjectName].StoredQueries;
            comboBoxMenuQuery.SelectedValue = Settings.Default.SelectedQuery;

            toolTipHelp.SetToolTip(pictureBoxHelpUserActivity, Resources.HelpActivityMonitor);
            toolTipHelp.SetToolTip(pictureBoxHelpPromptOnResume, Resources.HelpPromptOnResume);
            toolTipHelp.SetToolTip(pictureBoxHelpNag, Resources.HelpNag);
            toolTipHelp.SetToolTip(pictureBoxHelpMenuQuery, Resources.HelpMenuQuery);

            labelVersion.Text = Assembly.GetExecutingAssembly().GetCustomAttributes(true).OfType<AssemblyInformationalVersionAttribute>().FirstOrDefault().InformationalVersion;

            backgroundWorker.WorkerSupportsCancellation = true;
            workingItemConfiguration.WarehouseController.GetWarehouseStatusCompleted += new GetWarehouseStatusCompletedEventHandler(WarehouseController_GetWarehouseStatusCompleted);
            workingItemConfiguration.WarehouseController.RunCompleted += new RunCompletedEventHandler(WarehouseController_RunCompleted);
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

        private void buttonOK_Click(object sender, EventArgs e)
        {
            buttonSave_Click(sender, e);
            Close();
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

        private void FormWorkItemConfiguration_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Settings.Default.IsDirty || workingItemConfiguration.IsDirty)
            {
                DialogResult result = MessageBox.Show(Resources.OutstandingChanges, Resources.SaveChanges, MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Yes:
                        buttonSave_Click(sender, e);
                        break;
                    case DialogResult.No:
                        workingItemConfiguration.Load();
                        Settings.Reload();
                        break;
                    default:
                        e.Cancel = true;
                        break;
                }
            }
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

        private void GetWarehouseStatus()
        {
            workingItemConfiguration.WarehouseController.GetWarehouseStatusAsync();
        }

        private void buttonRefreshWarehouseStatus_Click(object sender, EventArgs e)
        {
            GetWarehouseStatus();
        }

        private void WarehouseController_GetWarehouseStatusCompleted(object sender, GetWarehouseStatusCompletedEventArgs e)
        {
            if (e.Error != null &&
                e.Error is SoapException &&
                e.Error.Message.Contains("Attempted to perform an unauthorized operation"))
            {

                MessageBox.Show("You do not have the correct permissions to view the warehouse status.",
                    "Permission Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            textBoxWarehouseStatus.Text = e.Result.ToString();
            _idle = (e.Result == WarehouseStatus.Idle);

        }

        #region Async Data Warehouse Processing

        /// <summary>
        /// Handles the async 'Warehouse Update' web service call back.
        /// If there has been an error, it needs to be trapped and displayed to the user.
        /// </summary>
        private void WarehouseController_RunCompleted(object sender, RunCompletedEventArgs e)
        {
            _idle = true;
            backgroundWorker.CancelAsync();

            if (e.Error != null &&
                e.Error is SoapException &&
                e.Error.Message.Contains("Attempted to perform an unauthorized operation"))
            {

                MessageBox.Show("You do not have the correct permissions to view the warehouse status.",
                    "Permission Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void buttonUpdateWarehouse_Click(object sender, EventArgs e)
        {
            progressBarWarehouse.MarqueeAnimationSpeed = 1;
            buttonUpdateWarehouse.Enabled = false;
            try
            {
                _idle = false;
                workingItemConfiguration.WarehouseController.RunAsync();
                backgroundWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonUpdateWarehouse.Enabled = true;
                progressBarWarehouse.MarqueeAnimationSpeed = 0;
            }
        }

        private bool _idle;
        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (!_idle)
            {
                Thread.Sleep(1000);

                if (backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                backgroundWorker.ReportProgress(1);
            }
            backgroundWorker.ReportProgress(100);
        }

        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            GetWarehouseStatus();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled != true)
                GetWarehouseStatus();

            buttonUpdateWarehouse.Enabled = true;
            progressBarWarehouse.MarqueeAnimationSpeed = 0;
        }
        #endregion Async Data Warehouse Processing
    }
}
