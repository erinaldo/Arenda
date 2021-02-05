using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using System.IO;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Nwuram.Framework.Logging;

namespace ArendaFileExport
{
    public partial class Form1 : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(),
          ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(),
          ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        bool stop;
        public Form1()
        {
            InitializeComponent();
        }

        public static void DoOnUIThread(MethodInvoker d, Form _this)
        {
            if (_this.InvokeRequired) { _this.Invoke(d); } else { d(); }
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            newUnload();
            return;

            button1.Enabled = false;
            button2.Enabled = true;
            label1.Text = "Получение списка документов...";
            label1.Refresh();
            this.Cursor = Cursors.WaitCursor;
            string Path = "";
            try
            {
                Path = _proc.EditGetConf(ConnectionSettings.GetIdProgram(),
                  "", "").Select("id_value = \'psss\'")[0]["value"].ToString();
            }
            catch
            { }
            if (Path == null || Path == "")
            {
                Path = "\\\\192.168.5.31\\Scans";
                _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "psss", Path);
            }
            if (!Directory.Exists(Path))
            {
                MessageBox.Show("   Введенный путь хранения\nотсканированных документов\n      недоступен для чтения.\n      Выберите другой путь.",
                  "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                var fd = new FolderBrowser2 { };
                if (fd.ShowDialog(this) == DialogResult.OK)
                {
                    if (fd.DirectoryPath.Trim().Length == 0)
                    {
                        this.Cursor = Cursors.Arrow;
                        label1.Text = "Не выбран путь сохранения. Выгрузка остановлена.";
                        label1.Refresh();
                        button1.Enabled = stop = true;
                        button2.Enabled = false;
                        return;
                    }
                    Path = fd.DirectoryPath.Trim();
                }
                else
                {
                    this.Cursor = Cursors.Arrow;
                    label1.Text = "Не выбран путь сохранения. Выгрузка остановлена.";
                    label1.Refresh();
                    button1.Enabled = stop = true;
                    button2.Enabled = false;
                    return;
                }
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(Path);
                DirectorySecurity ds = di.GetAccessControl();
                var rules = ds.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
                foreach (FileSystemAccessRule rule in rules)
                {
                    if (rule.FileSystemRights == FileSystemRights.Read && rule.AccessControlType == AccessControlType.Deny)
                    {
                        MessageBox.Show("   Введенный путь хранения\nотсканированных документов\n      недоступен для чтения.\n      Выберите другой путь.",
                          "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        var fd = new FolderBrowser2 { };
                        if (fd.ShowDialog(this) == DialogResult.OK)
                        {
                            if (fd.DirectoryPath.Trim().Length == 0)
                            {
                                this.Cursor = Cursors.Arrow;
                                label1.Text = "Не выбран путь сохранения. Выгрузка остановлена.";
                                label1.Refresh();
                                button1.Enabled = stop = true;
                                button2.Enabled = false;
                                return;
                            }
                            Path = fd.DirectoryPath.Trim();
                        }
                        else
                        {
                            this.Cursor = Cursors.Arrow;
                            label1.Text = "Не выбран путь сохранения. Выгрузка остановлена.";
                            label1.Refresh();
                            button1.Enabled = stop = true;
                            button2.Enabled = false;
                            return;
                        }
                    }
                }
            }

            DataTable dtdoc = _proc.GetDocumens();

            //if (dtdoc == null || dtdoc.Rows.Count == 0)
            //{
            //    MessageBox.Show("Нет данных для выгрузки", "Выгрузка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    label1.Text = "";
            //    label1.Refresh();
            //    label2.Text = "";
            //    label2.Refresh();
            //    return;
            //}


            int i = 0;
            DataTable dtTmp;
            if (dtdoc != null && dtdoc.Rows.Count != 0)
            {
                foreach (DataRow r in dtdoc.Rows)
                {
                    if (!stop)
                    {
                        dtTmp = _proc.GetDocumentsBody((int)r["id"]);

                        if (dtTmp == null || dtTmp.Rows.Count == 0 || dtTmp.Rows[0]["Scan"] == DBNull.Value) continue;

                        if (!Directory.Exists(Path + "\\" + r["id_Doc"].ToString()))
                            Directory.CreateDirectory(Path + "\\" + r["id_Doc"].ToString());
                        try
                        {
                           

                            label1.Text = "Запись файла " + r["cName"].ToString()
                              + r["Extension"].ToString();
                            label1.Refresh();
                            File.WriteAllBytes(Path + "\\" + r["id_Doc"].ToString() + "\\"
                              + r["cName"].ToString() + r["Extension"].ToString(),
                              (byte[])dtTmp.Rows[0]["Scan"]);
                            _proc.SetDocument((int)r["id"], Path + "\\" + r["id_Doc"].ToString());
                            i++;
                            label2.Text = "Файлов записано:" + i.ToString() + ". Файлов осталось:"
                              + (dtdoc.Rows.Count - i).ToString();
                            label2.Refresh();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Не удалось сохранить файл " + Path + "\\"
                              + r["cName"].ToString() + r["Extension"].ToString() + "/nТекст ошибки: "
                              + ex.Message);
                        }
                    }
                    else
                    {
                        button1.Enabled = stop = true;
                        button2.Enabled = false;
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Невыгруженные документы не найдены.");
            }
            this.Cursor = Cursors.Arrow;
            MessageBox.Show("Выгрузка окончена!");
            label1.Text = "Выгрузка завершена.";
            label1.Refresh();
            label2.Text = "";
            label2.Refresh();
            button1.Enabled = stop = true;
            button2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //button1.Enabled =
                stop = true;
            //button2.Enabled = false;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DoOnUIThread(() =>
            {
                label1.Text = "Получение списка документов...";
                label1.Refresh();
            }, this);
            DataTable dtdoc = _proc.GetDocumens();


            int i = 0;
            DataTable dtTmp;
            if (dtdoc != null && dtdoc.Rows.Count != 0)
            {
                foreach (DataRow r in dtdoc.Rows)
                {
                    if (!stop)
                    {
                        dtTmp = _proc.GetDocumentsBody((int)r["id"]);
                        System.Threading.Thread.Sleep(2000);
                        if (dtTmp == null || dtTmp.Rows.Count == 0 || dtTmp.Rows[0]["Scan"] == DBNull.Value) continue;

                        if (!Directory.Exists(Path + "\\" + r["id_Doc"].ToString()))
                            Directory.CreateDirectory(Path + "\\" + r["id_Doc"].ToString());
                        try
                        {


                            //label1.Text = "Запись файла " + r["cName"].ToString()
                            //  + r["Extension"].ToString();
                            //label1.Refresh();


                            File.WriteAllBytes(Path + "\\" + r["id_Doc"].ToString() + "\\"
                              + r["cName"].ToString() + r["Extension"].ToString(),
                              (byte[])dtTmp.Rows[0]["Scan"]);
                            _proc.SetDocument((int)r["id"], Path + "\\" + r["id_Doc"].ToString());
                            
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show("Не удалось сохранить файл " + Path + "\\"
                            //  + r["cName"].ToString() + r["Extension"].ToString() + "/nТекст ошибки: "
                            //  + ex.Message);
                        }
                    }
                    else
                    {
                        Logging.Comment("Принудительная остановка выгрузки файлов");
                        //button1.Enabled = stop = true;
                        //button2.Enabled = false;
                        return;
                    }

                    i++;

                    DoOnUIThread(() =>
                    {
                        label2.Text = "Файлов записано:" + i.ToString() + ". Файлов осталось:"
                      + (dtdoc.Rows.Count - i).ToString();
                        label2.Refresh();
                    }, this);
                }
                Logging.Comment($"Количество выгруженных файлов:{i}");
            }
            else
            {
                MessageBox.Show("Невыгруженные документы не найдены.");
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            Logging.Comment("Заверешение выгрузки файлов");
            Logging.StopFirstLevel();
            this.Cursor = Cursors.Arrow;
            MessageBox.Show("Выгрузка окончена!");
            label1.Text = "Выгрузка завершена.";
            label1.Refresh();
            label2.Text = "";
            label2.Refresh();
            button1.Enabled = stop = true;
            button2.Enabled = false;
        }


        string Path = "";
        private void newUnload()
        {
            Logging.StartFirstLevel(821);
            Logging.Comment("Начало выгрузки файлов");


            button1.Enabled = false;
            button2.Enabled = true;
            label1.Text = "Получение пути сохранения...";
            label1.Refresh();
            this.Cursor = Cursors.WaitCursor;
            try
            {
                Path = _proc.EditGetConf(ConnectionSettings.GetIdProgram(),
                  "", "").Select("id_value = \'psss\'")[0]["value"].ToString();
            }
            catch
            { }
            if (Path == null || Path == "")
            {
                Path = "\\\\192.168.5.31\\Scans";
                _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "psss", Path);
            }
            if (!Directory.Exists(Path))
            {
                MessageBox.Show("   Введенный путь хранения\nотсканированных документов\n      недоступен для чтения.\n      Выберите другой путь.",
                  "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                var fd = new FolderBrowser2 { };
                if (fd.ShowDialog(this) == DialogResult.OK)
                {
                    if (fd.DirectoryPath.Trim().Length == 0)
                    {
                        this.Cursor = Cursors.Arrow;
                        label1.Text = "Не выбран путь сохранения. Выгрузка остановлена.";
                        label1.Refresh();
                        button1.Enabled = stop = true;
                        button2.Enabled = false;
                        return;
                    }
                    Path = fd.DirectoryPath.Trim();
                }
                else
                {
                    this.Cursor = Cursors.Arrow;
                    label1.Text = "Не выбран путь сохранения. Выгрузка остановлена.";
                    label1.Refresh();
                    button1.Enabled = stop = true;
                    button2.Enabled = false;
                    return;
                }
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(Path);
                DirectorySecurity ds = di.GetAccessControl();
                var rules = ds.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
                foreach (FileSystemAccessRule rule in rules)
                {
                    if (rule.FileSystemRights == FileSystemRights.Read && rule.AccessControlType == AccessControlType.Deny)
                    {
                        MessageBox.Show("   Введенный путь хранения\nотсканированных документов\n      недоступен для чтения.\n      Выберите другой путь.",
                          "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        var fd = new FolderBrowser2 { };
                        if (fd.ShowDialog(this) == DialogResult.OK)
                        {
                            if (fd.DirectoryPath.Trim().Length == 0)
                            {
                                this.Cursor = Cursors.Arrow;
                                label1.Text = "Не выбран путь сохранения. Выгрузка остановлена.";
                                label1.Refresh();
                                button1.Enabled = stop = true;
                                button2.Enabled = false;
                                return;
                            }
                            Path = fd.DirectoryPath.Trim();
                        }
                        else
                        {
                            this.Cursor = Cursors.Arrow;
                            label1.Text = "Не выбран путь сохранения. Выгрузка остановлена.";
                            label1.Refresh();
                            button1.Enabled = stop = true;
                            button2.Enabled = false;
                            return;
                        }
                    }
                }
            }

            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }

        }
    }
}
