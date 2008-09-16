using System;
using System.Windows.Forms;
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
            TfsWorkingOn.Properties.Settings.Default.ConfigurationsPath = Settings.Default.ConfigurationsPath;
            workingItemConfiguration.Server = Settings.Default.TfsServer;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                workingItemConfiguration.Connect();
                Settings.Default.TfsServer = workingItemConfiguration.Server;
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ConnectionFailedTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void buttonDuration_Click(object sender, EventArgs e)
        {
            workingItemConfiguration.DurationField = listBoxFields.SelectedValue.ToString();
        }

        private void buttonRemaining_Click(object sender, EventArgs e)
        {
            workingItemConfiguration.RemainingField = listBoxFields.SelectedValue.ToString();
        }

        private void buttonElapsed_Click(object sender, EventArgs e)
        {
            workingItemConfiguration.ElapsedField = listBoxFields.SelectedValue.ToString();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            workingItemConfiguration.Save();
        }

        private void buttonSetDirectory_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                Settings.Default.ConfigurationsPath = folderBrowserDialog.SelectedPath;
                Settings.Default.Save();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
