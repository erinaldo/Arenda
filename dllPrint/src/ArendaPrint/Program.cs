using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Project;
using Nwuram.Framework.Logging;

namespace ArendaPrint
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

            Project.FillSettings(args);
            Logging.Init(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
            Logging.StartFirstLevel(1);
            Logging.Comment("Вход в программу");
            Logging.StopFirstLevel();
            //Application.Run(new frmPrint(1002,"4", 1));
            //Application.Run(new frmPrint(1028, "98p", 1));
            //Application.Run(new frmPrint(1042, "33", 3));
            //Application.Run(new frmPrint(1051, "dd444412341d", 2));
            Application.Run(new frmPrint(2165, "2508", 1));
            Logging.StartFirstLevel(2);
            Logging.Comment("Выход из программы");
            Logging.StopFirstLevel();

            Project.clearBufferFiles();
        }
    }
}
