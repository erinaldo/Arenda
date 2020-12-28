using System;
using System.Windows.Forms;
using Nwuram.Framework.Project;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;

namespace DllLink1CForAgreements
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length != 0)
                if (Project.FillSettings(args))
                {
                    Logging.Init(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
                    Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

                    Application.Run(new frmSelectAgreementsTo1C());

                    Project.clearBufferFiles();
                }
        }
    }
}
