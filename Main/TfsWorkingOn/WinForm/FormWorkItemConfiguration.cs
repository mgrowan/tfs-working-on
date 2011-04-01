using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Rowan.TfsWorkingOn.TfsWarehouse;
using Rowan.TfsWorkingOn.WinForm.Properties;

namespace Rowan.TfsWorkingOn.WinForm
{
    public partial class FormWorkItemConfiguration : Form
    {
        private WorkingItemConfiguration _workingItemConfiguration = new WorkingItemConfiguration();
        private dynamic _queryPickerControl;
        private Type _queryPickerControlType;

        public FormWorkItemConfiguration()
        {
            InitializeComponent();
        }

        private void FormWorkItemConfiguration_Load(object sender, EventArgs e)
        {
            workingItemConfigurationBindingSource.DataSource = _workingItemConfiguration;
            comboBoxWorkItemType.DataSource = _workingItemConfiguration.WorkItemTypes.ToList();
            comboBoxWorkItemType.DisplayMember = "Name";
            comboBoxWorkItemType.ValueMember = "Name";
            settingsBindingSource.DataSource = Settings.Default;

            _queryPickerControl = Activator.CreateInstance("Microsoft.TeamFoundation.Common.Library, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "Microsoft.TeamFoundation.Controls.QueryPickerControl").Unwrap();
            tabPageOptions.Controls.Add(_queryPickerControl);
            _queryPickerControl.Location = new System.Drawing.Point(94, 115);
            _queryPickerControl.AutoSize = false;
            _queryPickerControl.Size = new System.Drawing.Size(240, 21);
            _queryPickerControl.TabIndex = 21;
            // Can't use QueryPickerControl Methods or Properties with dynamic, since they are from an "internal" type. Only the "public" base type is exposed.
            // http://www.heartysoft.com/post/2010/05/26/anonymous-types-c-sharp-4-dynamic.aspx
            _queryPickerControlType = ((object)_queryPickerControl).GetType();
            //_queryPickerControl.Initialize(Connection.GetConnection().SelectedProject, null, 0);            
            _queryPickerControlType.GetMethod("Initialize", new Type[] { typeof(Project), typeof(QueryItem), Type.GetType("Microsoft.TeamFoundation.Controls.QueryPickerType, Microsoft.TeamFoundation.Common.Library, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a") })
                .Invoke(_queryPickerControl, new object[] { Connection.GetConnection().SelectedProject, null, 0 });
            //_queryPickerControl.SelectedItemId = Settings.Default.SelectedQuery;
            try
            {
                _queryPickerControlType.GetProperty("SelectedItemId").SetValue(_queryPickerControl, Settings.Default.SelectedQuery, null);
            }
            catch (Exception)
            {
                Settings.Default.SelectedQuery = null;
            }

            //_queryPickerControl.SelectedQueryItemChanged += new EventHandler(QueryPickerControl_OnSelectedQueryItemChanged);
            Type eventHandlerType = Type.GetType("Microsoft.TeamFoundation.Controls.SelectedQueryItemChangedEventHandler, Microsoft.TeamFoundation.Common.Library, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
            EventInfo eventInfo = _queryPickerControlType.GetEvent("SelectedQueryItemChanged", BindingFlags.Instance | BindingFlags.Public);
            eventInfo.AddEventHandler(_queryPickerControl, Create(eventInfo, QueryPickerControl_OnSelectedQueryItemChanged));

            toolTipHelp.SetToolTip(pictureBoxHelpUserActivity, Resources.HelpActivityMonitor);
            toolTipHelp.SetToolTip(pictureBoxHelpPromptOnResume, Resources.HelpPromptOnResume);
            toolTipHelp.SetToolTip(pictureBoxHelpNag, Resources.HelpNag);
            toolTipHelp.SetToolTip(pictureBoxHelpMenuQuery, Resources.HelpMenuQuery);

            labelVersion.Text = Assembly.GetExecutingAssembly().GetCustomAttributes(true).OfType<AssemblyInformationalVersionAttribute>().FirstOrDefault().InformationalVersion;

            backgroundWorker.WorkerSupportsCancellation = true;
            _workingItemConfiguration.WarehouseController.GetWarehouseStatusCompleted += new GetWarehouseStatusCompletedEventHandler(WarehouseController_GetWarehouseStatusCompleted);
            _workingItemConfiguration.WarehouseController.RunCompleted += new RunCompletedEventHandler(WarehouseController_RunCompleted);
        }

        // http://stackoverflow.com/questions/45779/c-dynamic-event-subscription
        private static Delegate Create(EventInfo evt, Action d)
        {
            var handlerType = evt.EventHandlerType;
            var eventParams = handlerType.GetMethod("Invoke").GetParameters();
            //lambda: (object x0, EventArgs x1) => d()      
            var parameters = eventParams.Select(p => Expression.Parameter(p.ParameterType, "x"));
            // - assumes void method with no arguments but can be        
            //   changed to accomodate any supplied method      
            var body = Expression.Call(Expression.Constant(d), d.GetType().GetMethod("Invoke"));
            var lambda = Expression.Lambda(body, parameters.ToArray());
            return Delegate.CreateDelegate(handlerType, lambda.Compile(), "Invoke", false);
        }

        protected void QueryPickerControl_OnSelectedQueryItemChanged()
        {
            Settings.Default.SelectedQuery = _queryPickerControlType.GetProperty("SelectedItemId").GetValue(_queryPickerControl, null);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            buttonSave_Click(sender, e);
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _workingItemConfiguration.Save();
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
            if (Settings.Default.IsDirty || _workingItemConfiguration.IsDirty)
            {
                DialogResult result = MessageBox.Show(Resources.OutstandingChanges, Resources.SaveChanges, MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Yes:
                        buttonSave_Click(sender, e);
                        break;
                    case DialogResult.No:
                        _workingItemConfiguration.Load();
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

        #region Data Warehouse Processing

        private void GetWarehouseStatus()
        {
            _workingItemConfiguration.WarehouseController.GetWarehouseStatusAsync();
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
                _workingItemConfiguration.WarehouseController.RunAsync();
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
