using System;
using System.Windows.Forms;
using Nwuram.Framework.Project;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;

namespace dllArendaDictonary
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
            //Application.Run(new frmMain());
            if (args.Length != 0)
                if (Project.FillSettings(args))
                {
                    Logging.Init(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
                    Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

                    Logging.StartFirstLevel(1);
                    Logging.Comment("Вход в программу");
                    Logging.StopFirstLevel();

                    Application.Run(new dllArendaDictonary.jDiscount.frmList());
                    //Application.Run(new ArchiveDocAddDoc.frmAddDoc(){Text = "Добавление документа"});
                    //Application.Run(new FeedBack.frmList());
                    //Application.Run(new WebSites.frmList());
                    //Application.Run(new Report.frmReport());

                    //Application.Run(new Form1());

                    Logging.StartFirstLevel(2);
                    Logging.Comment("Выход из программы");
                    Logging.StopFirstLevel();

                    Project.clearBufferFiles();
                }
        }
    }
}
