using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rowan.TfsWitWorkingOn.WinForm.Properties;

namespace Rowan.TfsWitWorkingOn.WinForm
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
            TfsWitWorkingOn.Properties.Settings.Default.ConfigurationsPath = Settings.Default.ConfigurationsPath;
            workingItemConfiguration.Server = Settings.Default.TFSServer;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                workingItemConfiguration.Connect();
                Settings.Default.TFSServer = workingItemConfiguration.Server;
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
