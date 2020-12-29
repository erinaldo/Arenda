using System;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Project;
using System.Collections.Generic;

namespace Arenda
{ 
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        
        [STAThread]
        static void Main(string[] args)
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0)
            {
                Project.FillSettings(args);
                
                Logging.Init(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
                Logging.StartFirstLevel(1);
                Logging.Comment("Вход в программу");
                Logging.StopFirstLevel();
                
                //new Reports.frmDelete_Files().ShowDialog();

                if (!new List<string>() { "СОА", "РКВ", "МНД", "ПР", "КНТ", "СБ6", "Д" }.Contains(Nwuram.Framework.Settings.User.UserSettings.User.StatusCode)) { return; }

                Application.Run(new mForm(args[1], args[8], args[6]));

                Logging.StartFirstLevel(2);
                Logging.Comment("Выход из программы");
                Logging.StopFirstLevel();

                Project.clearBufferFiles();
            }
            
        }
    }

    static class dataBank
    {
        public static int id { get; set; }
        public static string cName { get; set; }
        public static string cA { get; set; }
        public static string BIK { get; set; }
        
    
    }

    static class dataTen
    {
        public static int id { get; set; }
        public static string aren { get; set; }
        public static string fam { get; set; }
        public static string name { get; set; }
        public static string midname { get; set; }
        public static long inn { get; set; }
        public static int id_Obj { get; set; }

        public static string CadastralNumber { get; set; }

        public static void ClearDataTen()
        {
            id = 0;
            aren = "";
            fam = "";
            name = "";
            midname = "";
            inn = 0;
            CadastralNumber = "";
        }

    }

    static class tGo
    {
        public static bool value { get; set; }
       

    }


}
