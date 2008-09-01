using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Rowan.TfsWitWorkingOn.WinForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            FormSetConnection formSetConnection = new FormSetConnection();
            Application.Run();
        }
    }
}
