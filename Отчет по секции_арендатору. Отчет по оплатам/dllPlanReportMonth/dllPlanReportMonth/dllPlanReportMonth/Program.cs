using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nwuram.Framework.Project;
using Nwuram.Framework.Settings.Connection;

namespace dllPlanReportMonth
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Project.FillSettings(args))
            {
                Application.Run(new frmPrint());
                Project.clearBufferFiles();
            }
        }
    }
}
