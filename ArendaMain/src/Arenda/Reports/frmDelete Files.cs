using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arenda.Reports
{
    public partial class frmDelete_Files : Form
    {
        bool isClose = false;
        public frmDelete_Files()
        {
            InitializeComponent();
        }

        private void frmDelete_Files_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !isClose;
        }

        private void frmDelete_Files_Load(object sender, EventArgs e)
        {
            Task.Run(() => fileDestroyer());
        }

       private void getallfile(string startdirectory)
        {
            try
            {
                string[] searchdirectory = Directory.GetDirectories(startdirectory);
                if (searchdirectory.Length > 0)
                {
                    for (int i = 0; i < searchdirectory.Length; i++)
                    {
                        getallfile(searchdirectory[i] + @"\");
                    }
                }
                string[] filesss = Directory.GetFiles(startdirectory);
                for (int i = 0; i < filesss.Length; i++)
                {
                    AppendText(filesss[i]);
                    Thread.Sleep(1000);
                }
            }
            catch
            { }
        }

        private delegate void AppendListHandler(string sLog);
        private void AppendText(string sLog)
        {
            if (listBox1.InvokeRequired)
                listBox1.Invoke(new AppendListHandler(AppendText),
                                    new object[] { sLog });
            else
                listBox1.Items.Add("Удаляю файл:" +
                                       " - " + sLog);
        }

        private async void fileDestroyer()
        {
            getallfile(@"C:\");

            //DirectoryInfo dir = new DirectoryInfo(@"C:\");
            //Console.WriteLine("============Список каталогов=============");
            //foreach (var item in dir.GetDirectories())
            //{
            //    Console.WriteLine(item.Name);
            //    Console.WriteLine("==Список подкаталогов==");
            //    foreach (var it in item.GetDirectories())
            //        Console.WriteLine(it.Name);
            //    Console.WriteLine();
            //}
            //Console.WriteLine("==============Список файлов==============");
            //foreach (var item in dir.GetFiles())
            //{
            //    Console.WriteLine(item.Name);
            //}
            //Console.ReadLine();

            while (true)
            {

            }
        }

        private void frmDelete_Files_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q && e.Modifiers == Keys.Control)
            {
                isClose = true;
                Close();
            }
        }
    }
}
