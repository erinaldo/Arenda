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
using System.Diagnostics;
using System.Drawing.Printing;
using Nwuram.Framework.Logging;
using System.Security.AccessControl;
using System.Threading;

namespace Arenda
{
    public partial class frmDocument : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(),
          ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        private int ZoomValue = 10;
        public int id_Doc { private get; set; }
        //public int _id_type_dog { private get; set; }
        private DataTable dtScan, dtScan_old;
        private bool isView = false;
        public DateTime str { private get; set; }
        int doc_Type;
        DateTime date_doc;
        string name = "";
        NetworkShare net = new NetworkShare(true);
        public frmDocument(bool isView)
        {
            InitializeComponent();
            this.isView = isView;
            dgvScan.AutoGenerateColumns = false;

            btDel.Visible = btEditName.Visible = btAddFile.Visible = btScan.Visible = btSave.Visible = !isView;
        }

        private void frmDocument_Load(object sender, EventArgs e)
        {
            getData();
            dgvScan_CurrentCellChanged(null, null);
        }

        private void getData()
        {
            dtScan = _proc.getScan(id_Doc, -1);
            dtScan_old = dtScan.Copy();
            dgvScan.DataSource = dtScan;
        }

        private void btZoomOut_Click(object sender, EventArgs e)
        {
            ZoomValue -= 1;
            if (ZoomValue == 0)
                ZoomValue = 1;
            imagePanel1.Zoom = ZoomValue * 0.02f;
        }

        private void btZoomIn_Click(object sender, EventArgs e)
        {
            ZoomValue += 1;
            imagePanel1.Zoom = ZoomValue * 0.02f;
        }

        private void Scan()
        {
            try
            {
                Nwuram.Framework.scan.scanImg fImg = new Nwuram.Framework.scan.scanImg();
                fImg.ShowDialog();
                this.TopMost = true;
                byte[] img_array = fImg.img_array;
                this.TopMost = false;
                if (img_array != null)
                {
                    /*frmNameFile frmNF = new frmNameFile();
                    if (DialogResult.OK == frmNF.ShowDialog())
                    {
                      string fileName = frmNF.getComment;*/
                    byte[] byteFile = img_array;
                    saveFileToDataBase(byteFile, name, ".jpg", doc_Type, date_doc);
                    //}
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при работе со сканером!");
            }
        }

        private void addFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = @"(All Image Files)|*.BMP;*.bmp;*.JPG;*.JPEG*.jpg;*.jpeg;*.PNG;*.png;*.GIF;*.gif;*.tif;*.tiff;*.ico;*.ICO" +
              "|(Microsoft Word)|*.doc;*.docx" +
              "|(Microsoft Excel)|*.xls;*.xlsx" +
              "|(Portable Document Format)|*.pdf";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                //string fileName = Path.GetFileNameWithoutExtension(fileDialog.FileName);
                byte[] byteFile = File.ReadAllBytes(fileDialog.FileName);
                string @Extension = Path.GetExtension(fileDialog.FileName);

                saveFileToDataBase(byteFile, name, @Extension, doc_Type, date_doc);
            }
        }

        private void btScan_Click(object sender, EventArgs e)
        {
            frmAddDoc frmAD = new frmAddDoc(id_Doc, str/*, _id_type_dog*/);
            if (frmAD.ShowDialog() == DialogResult.OK)
            {
                doc_Type = frmAD.id_DocType;
                date_doc = frmAD.date_Doc;
                name = frmAD.name;
                int n = 0;
                foreach (DataRow r in dtScan.Rows)
                {
                    if (r["cName"].ToString().StartsWith(name))
                    {
                        n++;
                    }
                }
                if (n != 0)
                    name += "_" + n.ToString();
                Scan();
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            bool changed = false;
            DataTable dtOldScan = _proc.getScan(id_Doc, -1);
            if (dtScan.Rows.Count == dtOldScan.Rows.Count)
            {
                if (dtOldScan.Rows.Count != 0)
                {
                    for (int i = 0; i < dtOldScan.Rows.Count; i++)
                    {
                        if (dtScan.Rows[i]["id"].ToString() != dtOldScan.Rows[i]["id"].ToString() ||
                          dtScan.Rows[i]["cName"].ToString() != dtOldScan.Rows[i]["cName"].ToString())
                        {
                            changed = true;
                            break;
                        }
                    }
                }
            }
            else
                changed = true;
            if (changed)
            {
                if (MessageBox.Show("На форме были внесены изменения. \nВыйти без сохранения?", "Запрос на выход",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                  foreach(DataRow r in dtScan.Rows)
                  {
                    DataRow[] tmp = dtScan_old.Select("id = " + r["id"].ToString());
                    if(tmp != null && tmp.Length > 0 && tmp[0]["cName"] != r["cName"]
                      && r["Path"] != DBNull.Value)
                      File.Move(r["Path"].ToString() + "\\" + r["cName"].ToString() +
                        r["Extension"].ToString(), r["Path"].ToString() + "\\"
                        + tmp[0]["cName"].ToString() + r["Extension"].ToString());
                  }
                  this.DialogResult = DialogResult.Cancel;
                }
            }
            else
                this.DialogResult = DialogResult.Cancel;
        }

        private void btView_Click(object sender, EventArgs e)
        {
            if (dtScan != null && dtScan.DefaultView.Count > 0 && dgvScan.CurrentRow != null && id_Doc != 0)
            {
                int indexRow = dgvScan.CurrentRow.Index;
                int id = int.Parse(dtScan.DefaultView[indexRow]["id"].ToString());
                string name = dtScan.DefaultView[indexRow]["cName"].ToString();
                string extension = dtScan.DefaultView[indexRow]["Extension"].ToString();
                if (!Directory.Exists(Application.StartupPath + "\\temp"))
                    Directory.CreateDirectory(Application.StartupPath + "\\temp");
                string filename = Application.StartupPath + "\\temp\\" + DateTime.Now.ToString().Replace(":","") + extension;
                net.CopyFromServer(id_Doc.ToString(), name, extension, filename);
                Thread.Sleep(500);
                Process.Start(filename);
                Logging.StartFirstLevel(1065);
                Logging.Comment("Произведена выгрузка внешнего файла у доп.документа договора");

                Logging.Comment("ID: " + id);
                //Logging.Comment("Тип документа ID: " + r["id_DocType"]);// + " ; Наименование: " + cbTypeDoc.Text);
                Logging.Comment("Наименование файла: " + @name + " ;Расширение: "
                  + extension);

                Logging.Comment("Данные договора, к которому добавляется внешний файл");
                Logging.Comment("Дата заключения договора: "
                  + oldDoc.ToShortDateString());
                Logging.Comment("Номер договора: " + num_doc);
                Logging.Comment("Арендатор ID: " + _old_id_ten + "; Наименование: "
                  + oldTen);
                Logging.Comment("Арендодатель ID: " + _old_id_lord + "; Наименование: "
                  + oldLord);

                Logging.Comment("Операцию выполнил: ID:"
                  + Nwuram.Framework.Settings.User.UserSettings.User.Id + " ; ФИО:"
                  + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
                return;
            }
                /*
                DataTable dtFile = _proc.getScan(id_Doc, id);
                if (dtFile != null && dtFile.Rows.Count > 0 && dtFile.Rows[0]["Scan"] != DBNull.Value)
                {
                    byte[] img = (byte[])dtFile.Rows[0]["Scan"];
                    string @Extension = (string)dtScan.DefaultView[indexRow]["Extension"];
                    string @name = dtScan.DefaultView[indexRow]["cName"].ToString();
                    try
                    {
                        using (var fs = new FileStream("tmpFile" + @Extension, FileMode.Create,
                          FileAccess.Write))
                        {
                            fs.Write(img, 0, img.Length);
                            Process.Start("tmpFile" + @Extension);

                            Logging.StartFirstLevel(1065);
                            Logging.Comment("Произведена выгрузка внешнего файла у доп.документа договора");

                            Logging.Comment("ID: " + id);
                            //Logging.Comment("Тип документа ID: " + r["id_DocType"]);// + " ; Наименование: " + cbTypeDoc.Text);
                            Logging.Comment("Наименование файла: " + @name + " ;Расширение: "
                              + @Extension);

                            Logging.Comment("Данные договора, к которому добавляется внешний файл");
                            Logging.Comment("Дата заключения договора: "
                              + oldDoc.ToShortDateString());
                            Logging.Comment("Номер договора: " + num_doc);
                            Logging.Comment("Арендатор ID: " + _old_id_ten + "; Наименование: "
                              + oldTen);
                            Logging.Comment("Арендодатель ID: " + _old_id_lord + "; Наименование: "
                              + oldLord);

                            Logging.Comment("Операцию выполнил: ID:"
                              + Nwuram.Framework.Settings.User.UserSettings.User.Id + " ; ФИО:"
                              + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                            Logging.StopFirstLevel();

                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception caught in process: {0}", ex);
                        return;
                    }
                }
                if (dtFile != null && dtFile.Rows.Count > 0 && dtFile.Rows[0]["Path"]
                  != DBNull.Value)
                {
                    try
                    {
                        string @Extension = (string)dtScan.DefaultView[indexRow]["Extension"];
                        string @name = dtScan.DefaultView[indexRow]["cName"].ToString();
                        Process.Start(dtFile.Rows[0]["Path"].ToString() + "\\" + @name
                          + @Extension);
                    }
                    catch { }
                }
            }
            else
              if (id_Doc == 0 && dtScan != null && dtScan.Rows.Count > 0
                && dgvScan.CurrentRow != null)
            {
                int indexRow = dgvScan.CurrentRow.Index;
                string @Extension = (string)dtScan.DefaultView[indexRow]["Extension"];
                string @name = dtScan.DefaultView[indexRow]["cName"].ToString();
                if (dtScan.DefaultView[indexRow]["Scan"] != DBNull.Value)
                {
                    byte[] img = (byte[])dtScan.DefaultView[indexRow]["Scan"];

                    try
                    {
                        using (var fs = new FileStream("tmpFile" + @Extension,
                          FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(img, 0, img.Length);
                            Process.Start("tmpFile" + @Extension);

                            Logging.StartFirstLevel(1065);
                            Logging.Comment("Произведена выгрузка внешнего файла у доп.документа договора");

                            //Logging.Comment("ID: " + id);
                            //Logging.Comment("Тип документа ID: " + r["id_DocType"]);// + " ; Наименование: " + cbTypeDoc.Text);
                            Logging.Comment("Наименование файла: " + @name + " ;Расширение: "
                              + @Extension);

                            Logging.Comment("Данные договора, к которому добавляется внешний файл");
                            Logging.Comment("Дата заключения договора: "
                              + oldDoc.ToShortDateString());
                            Logging.Comment("Номер договора: " + num_doc);
                            Logging.Comment("Арендатор ID: " + _old_id_ten + "; Наименование: "
                              + oldTen);
                            Logging.Comment("Арендодатель ID: " + _old_id_lord
                              + "; Наименование: " + oldLord);

                            Logging.Comment("Операцию выполнил: ID:"
                              + Nwuram.Framework.Settings.User.UserSettings.User.Id + " ; ФИО:"
                              + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                            Logging.StopFirstLevel();

                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception caught in process: {0}", ex);
                        return;
                    }
                }
                else if (dtScan.DefaultView[indexRow]["Path"] != DBNull.Value)
                {
                    Process.Start(dtScan.DefaultView[indexRow]["Path"].ToString()
                      + "\\" + @name + @Extension);
                }
            }*/
        }

        private void btAddFile_Click(object sender, EventArgs e)
        {
            frmAddDoc frmAD = new frmAddDoc(id_Doc, str/*, _id_type_dog*/);
            if (frmAD.ShowDialog() == DialogResult.OK)
            {
                doc_Type = frmAD.id_DocType;
                date_doc = frmAD.date_Doc;
                name = frmAD.name;
                int n = 0;
                foreach (DataRow r in dtScan.Rows)
                {
                    if (r["cName"].ToString().StartsWith(name))
                    {
                        n++;
                    }
                }
                if (n != 0)
                    name += "_" + n.ToString();
                addFile();
            }
        }

        List<int> lRename = new List<int>();
        private void btEditName_Click(object sender, EventArgs e)
        {
            int indexRow = dgvScan.CurrentRow.Index;
            frmNameFile frmNF = new frmNameFile();
            frmNF.getComment = dtScan.DefaultView[indexRow]["cName"].ToString();
            if (DialogResult.OK == frmNF.ShowDialog())
            {
                if (id_Doc != 0 && dtScan != null && dtScan.DefaultView.Count > 0 && dgvScan.CurrentRow != null)
                {
                    int ind = dgvScan.CurrentRow.Index;
                    int id = int.Parse(dtScan.DefaultView[ind]["id"].ToString());
                    if (dtScan.DefaultView[indexRow]["Path"] != DBNull.Value)
                    {
                      File.Move(dtScan.DefaultView[indexRow]["Path"].ToString() + "\\"
                        + dtScan.DefaultView[indexRow]["cName"].ToString() +
                        dtScan.DefaultView[indexRow]["Extension"].ToString(),
                        dtScan.DefaultView[indexRow]["Path"].ToString() + "\\"
                        + frmNF.getComment +
                        dtScan.DefaultView[indexRow]["Extension"].ToString());
                    }
                    lRename.Add(id);
                }
                string fileName = frmNF.getComment;
                dtScan.DefaultView[indexRow]["cName"] = fileName;
                dtScan.AcceptChanges();
            }
        }
       
        private void saveFileToDataBase(byte[] byteFile, string nameFile, string Extension, int id_dt, DateTime dd)
        {
            //kav меняем запись на серверную папку
            DataRow row = dtScan.Rows.Add();
            row["id"] = -1;
            row["cName"] = nameFile;
            row["Scan"] = byteFile;
            row["Extension"] = @Extension;
            row["id_DocType"] = id_dt;
            row["DateDocument"] = dd;

            if (!net.CopyBytes(byteFile, nameFile + Extension, id_Doc.ToString()))
                MessageBox.Show("Ошибка копирования файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            row["Path"] = net.server + "\\" + id_Doc.ToString();
            
         /* DataRow[] tmp = _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "", "").Select("id_value = \'psss\'");

          if (tmp.Length == 0)
          {
            row["Path"] = "\\\\192.168.5.31\\Scans\\" + id_Doc.ToString();
            _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "psss", "\\\\192.168.5.31\\Scans");
          }
          else
          {
            row["Path"] = tmp[0]["value"].ToString() + "\\" + id_Doc.ToString();
          }

          try
          {
            if (row["Path"] != null)
            {
              if (!Directory.Exists(row["Path"].ToString()))
                Directory.CreateDirectory(row["Path"].ToString());
              File.WriteAllBytes(row["Path"].ToString() + "\\" + row["cName"].ToString()
                + row["Extension"].ToString(), byteFile);
              row["Scan"] = null;
            }
          }
          catch { }
          */
            dgvScan_CurrentCellChanged(null, null);
        }

        private void dgvScan_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dtScan != null && dtScan.Rows.Count > 0 && dgvScan.CurrentRow != null)
            {
                int indexRow = dgvScan.CurrentRow.Index;

                if (dtScan.DefaultView[indexRow]["id"] == DBNull.Value) return;

                int id = int.Parse(dtScan.DefaultView[indexRow]["id"].ToString());
                byte[] img;
                if (dtScan.DefaultView[indexRow]["Scan"] != DBNull.Value)
                    img = (byte[])dtScan.DefaultView[indexRow]["Scan"];
                else
                {
                    string tmp = dtScan.DefaultView[indexRow]["Path"].ToString() + "\\" +
                      dtScan.DefaultView[indexRow]["cName"].ToString()
                      + dtScan.DefaultView[indexRow]["Extension"].ToString();
                    try
                    {
                        //img = File.ReadAllBytes(tmp);
                        img = net.GetFileBytes(id_Doc.ToString(), dtScan.DefaultView[indexRow]["cName"].ToString(), dtScan.DefaultView[indexRow]["Extension"].ToString());
                    }
                    catch
                    {
                        img = null;
                        imagePanel1.Image = null;
                        btDel.Enabled = false;
                        //btSaveDoc.Enabled = false;
                        btEditName.Enabled = false;
                        btZoomIn.Enabled = btZoomOut.Enabled = false;
                        //btView.Enabled = false;
                    }
                }
                string @Extension = (string)dtScan.DefaultView[indexRow]["Extension"];
                try
                {
                    MemoryStream ms = new MemoryStream(img);
                    Bitmap b = new Bitmap(ms);
                    imagePanel1.Image = (Bitmap)b;
                    ZoomValue = 10;
                    imagePanel1.Zoom = ZoomValue * 0.02f;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in process: {0}", ex);
                    imagePanel1.Image = null;
                    return;
                }

                btDel.Enabled = true;
                btSaveDoc.Enabled = true;
                btEditName.Enabled = true;
                btZoomIn.Enabled = btZoomOut.Enabled = true;
                //btView.Enabled = true;
            }
            else
            {
                imagePanel1.Image = null;
                btDel.Enabled = false;
                //btSaveDoc.Enabled = false;
                btEditName.Enabled = false;
                btZoomIn.Enabled = btZoomOut.Enabled = false;
                //btView.Enabled = false;
            }
        }

        private void tbNameImg_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void setFilter()
        {
            string filter = "";

            filter += (tbNameImg.Text.Length != 0 ?
              (filter.Length == 0 ? "" : " AND ") + "CONVERT(cName, 'System.String') LIKE '%" + tbNameImg.Text + "%'" : "");

            try
            {
                dtScan.DefaultView.RowFilter = filter;
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректное значение фильтра!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        List<int> lDelete = new List<int>();
        List<string> DelPath = new List<string>();
        private void btDel_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Удалить выбранную запись?", "Удаление записи", MessageBoxButtons.YesNo,
              MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                if (id_Doc != 0 && dtScan != null && dtScan.DefaultView.Count > 0 &&
                  dgvScan.CurrentRow != null)
                {
                    int ind = dgvScan.CurrentRow.Index;
                    int id = int.Parse(dtScan.DefaultView[ind]["id"].ToString());
                    lDelete.Add(id);
                  if(dtScan.DefaultView[ind]["Path"] != DBNull.Value && dtScan.DefaultView[ind]["Path"].ToString() != "")
                    DelPath.Add(dtScan.DefaultView[ind]["Path"].ToString()
                      + "\\" + dtScan.DefaultView[ind]["cName"].ToString()
                      + dtScan.DefaultView[ind]["Extension"].ToString());
                    if (lRename != null && lRename.Count != 0 && lRename.Contains(id))
                        lRename.Remove(id);
                }

                int indexRow = dgvScan.CurrentRow.Index;
                dgvScan.Rows.RemoveAt(indexRow);
                dtScan.AcceptChanges();
            }
        }

        /*private byte[] imgPring;
        private void btPrint_Click(object sender, EventArgs e)
        {
          if (dtScan != null && dtScan.Rows.Count > 0 &&
            dgvScan.CurrentRow != null)
          {
            int indexRow = dgvScan.CurrentRow.Index;
            int id = int.Parse(dtScan.DefaultView[indexRow]["id"].ToString());

            imgPring = (byte[])dtScan.DefaultView[indexRow]["Scan"];
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += PrintPage;
            pd.Print();
          }
        }

        private void PrintPage(object o, PrintPageEventArgs e)
        {
          MemoryStream ms = new MemoryStream(imgPring);
          Bitmap i = new Bitmap(ms);

          float newWidth = i.Width * 100 / i.HorizontalResolution;
          float newHeight = i.Height * 100 / i.VerticalResolution;
          float widthFactor = newWidth / e.MarginBounds.Width;
          float heightFactor = newHeight / e.MarginBounds.Height;

          if (widthFactor > 1 | heightFactor > 1)
          {
            if (widthFactor > heightFactor)
            {
              newWidth = newWidth / widthFactor;
              newHeight = newHeight / widthFactor;
            }
            else
            {
              newWidth = newWidth / heightFactor;
              newHeight = newHeight / heightFactor;
            }
          }
          if (newHeight > newWidth)
          {
            i.RotateFlip(RotateFlipType.Rotate90FlipNone);
          }
          e.Graphics.DrawImage(i, 0, 0, (int)newWidth, (int)newHeight);
        }*/

        private void frmDocument_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Directory.Exists(Application.StartupPath + "\\temp"))
            {
                string folder = Application.StartupPath + "\\temp";
                DirectoryInfo di = new DirectoryInfo(folder);
                foreach (FileInfo f in di.GetFiles())
                {
                    f.Delete();
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btSaveDoc_Click(object sender, EventArgs e)
        {
            if (id_Doc != 0 && dtScan != null && dtScan.DefaultView.Count > 0 && dgvScan.CurrentRow != null)
            {
                int indexRow = dgvScan.CurrentRow.Index;
                int id = int.Parse(dtScan.DefaultView[indexRow]["id"].ToString());
                string name = dtScan.DefaultView[indexRow]["cName"].ToString();
                string extension = dtScan.DefaultView[indexRow]["Extension"].ToString();
                byte[] file = new byte[0];
                DataTable dtFile = _proc.getScan(id_Doc, id);
                /*if (dtFile != null && dtFile.Rows.Count > 0 && dtFile.Rows[0]["Scan"] != DBNull.Value)
                    file = (byte[])dtFile.Rows[0]["Scan"];
                else if (dtFile != null && dtFile.Rows.Count > 0 && dtFile.Rows[0]["Path"] != DBNull.Value
                  && dtFile.Rows[0]["Path"].ToString() != "")
                  try
                  {

                    file = File.ReadAllBytes(dtFile.Rows[0]["Path"].ToString()
                      + "\\" + dtFile.Rows[0]["cName"].ToString()
                      + dtFile.Rows[0]["Extension"].ToString());
                  }
                  catch(Exception ex)
                  {
                    MessageBox.Show("Не удалось выгрузить документ " + dtFile.Rows[0]["Path"].ToString() + "\\"
                            + dtFile.Rows[0]["cName"].ToString() + dtFile.Rows[0]["Extension"].ToString() + "/nТекст ошибки: "
                            + ex.Message);
                  }
                  */





                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Filter = @"(All Image Files)|*.BMP;*.bmp;*.JPG;*.JPEG*.jpg;*.jpeg;*.PNG;*.png;*.GIF;*.gif;*.tif;*.tiff;*.ico;*.ICO" +
                  "|(Microsoft Word)|*.doc;*.docx" +
                  "|(Microsoft Excel)|*.xls;*.xlsx" +
                  "|(Portable Document Format)|*.pdf";
                fileDialog.FileName = name + extension;
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    //File.WriteAllBytes(fileDialog.FileName, file);
                    net.CopyFromServer(id_Doc.ToString(), name,extension , fileDialog.FileName);

                    Logging.StartFirstLevel(821);
                    Logging.Comment("Произведена выгрузка внешнего файла у доп.документа договора");

                    Logging.Comment("ID: " + id);                   
                    //Logging.Comment("Тип документа ID: " + r["id_DocType"]);// + " ; Наименование: " + cbTypeDoc.Text);
                    Logging.Comment("Наименование файла: " + name + " ;Расширение: " + extension);

                    Logging.Comment("Данные договора, к которому добавляется внешний файл");
                    Logging.Comment("Дата заключения договора: " + oldDoc.ToShortDateString());
                    Logging.Comment("Номер договора: " + num_doc);
                    Logging.Comment("Арендатор ID: " + _old_id_ten + "; Наименование: " + oldTen);
                    Logging.Comment("Арендодатель ID: " + _old_id_lord + "; Наименование: " + oldLord);
                    
                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                  + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();
                }
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (id_Doc != 0)
            {
                if (lDelete != null && lDelete.Count != 0)
                {
                    Logging.StartFirstLevel(1271);
                    Logging.Comment("Удаление внешенего файла у доп.документа договора");
                    foreach (int del in lDelete)
                    {
                        _proc.delScan(del);

                        EnumerableRowCollection<DataRow> rowCollect = dtScan_old.AsEnumerable().Where(rq => rq.Field<int>("id") == del);

                        foreach (DataRow r in rowCollect)
                        {
                            Logging.Comment("ID: " + del);
                            //Logging.Comment("№ документа: " + num);                            
                            Logging.Comment("Тип документа ID: " + r["id_DocType"]);// + " ; Наименование: " + cbTypeDoc.Text);
                            Logging.Comment("Наименование файла: " + r["cName"].ToString() + " ;Расширение: " + r["Extension"].ToString());
                            //Logging.Comment("Дата документа: " + dateadddoc.Value.ToShortDateString());
                        }
                    }

                    Logging.Comment("Данные договора, у которого удаляется внешний файл");
                    Logging.Comment("Дата заключения договора: " + oldDoc.ToShortDateString());
                    Logging.Comment("Номер договора: " + num_doc);
                    Logging.Comment("Арендатор ID: " + _old_id_ten + "; Наименование: " + oldTen);
                    Logging.Comment("Арендодатель ID: " + _old_id_lord + "; Наименование: " + oldLord);
                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                        + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();
                }

              if(DelPath != null && DelPath.Count != 0)
              {
                foreach (string p in DelPath)
                {
                  try
                  {
                    File.Delete(p);
                  }
                  catch (Exception ex)
                  {
                    MessageBox.Show("Не удалось удалить файл " + p.ToString()
                      + "\nТекст ошибки:" + ex.Message);
                  }
                }
              }

                if (lRename != null && lRename.Count != 0)
                {
                    Logging.StartFirstLevel(1272);
                    Logging.Comment("Произведено переименование прикрепленного файла");
                    foreach (int rename in lRename)
                    {
                        foreach (DataRow r in dtScan.Rows)
                            if (r["id"].ToString() == rename.ToString())
                            {
                                _proc.updateScanName(rename, r["cName"].ToString());

                                Logging.Comment("ID: " + rename);
                                //Logging.Comment("№ документа: " + num);                            
                                Logging.Comment("Тип документа ID: " + r["id_DocType"]);// + " ; Наименование: " + cbTypeDoc.Text);
                               

                                EnumerableRowCollection<DataRow> rowCollect = dtScan_old.AsEnumerable().Where(rq => rq.Field<int>("id") == rename);
                                if (rowCollect.Count() == 0)
                                {
                                    Logging.Comment("Наименование файла: " + r["cName"].ToString() + " ;Расширение: " + r["Extension"].ToString());
                                }
                                else
                                {
                                    foreach (DataRow rCol in rowCollect)
                                    {
                                        Logging.VariableChange("Наименование файла: " , r["cName"].ToString() , rCol["cName"].ToString());                                        
                                    }
                                }
                                break;
                            }
                    }

                    Logging.Comment("Данные договора, у которого удаляется внешний файл");
                    Logging.Comment("Дата заключения договора: " + oldDoc.ToShortDateString());
                    Logging.Comment("Номер договора: " + num_doc);
                    Logging.Comment("Арендатор ID: " + _old_id_ten + "; Наименование: " + oldTen);
                    Logging.Comment("Арендодатель ID: " + _old_id_lord + "; Наименование: " + oldLord);
                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                        + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();
                }

                Logging.StartFirstLevel(1270);
                Logging.Comment("Добавление внешнего файла к доп.документу договора");
                foreach (DataRow r in dtScan.Rows)
                {
                    if ((int)r["id"] == -1)
                    {
                      /*if (r["Path"] == null || r["Path"].ToString() == "")
                      {
                        r["Path"] = "\\\\192.168.5.31\\Scans";
                        _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "psss", r["Path"].ToString());
                      }*/

                        #region проверка в комменте
                        /*
                        if (!Directory.Exists(r["Path"].ToString()))
                        {
                          MessageBox.Show("   Введенный путь хранения\nотсканированных документов\n      недоступен для чтения.\n      Выберите другой путь.",
                            "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                          var fd = new FolderBrowser2 { };
                          if (fd.ShowDialog(this) == DialogResult.OK)
                          {
                            if (fd.DirectoryPath.Trim().Length == 0)
                            {
                              return;
                            }
                            r["Path"] = fd.DirectoryPath.Trim() + id_Doc.ToString();
                          }
                          else
                          {
                            return;
                          }
                        }
                        else
                        {
                          DirectoryInfo di = new DirectoryInfo(r["Path"].ToString());
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
                                  return;
                                }
                                r["Path"] = fd.DirectoryPath.Trim() + id_Doc.ToString();
                              }
                              else
                              {
                                return;
                              }
                            }
                          }
                        }*/
                        #endregion

                        _proc.setScan(id_Doc, r["cName"].ToString(), r["Extension"].ToString(), (int)r["id_DocType"],
                          (DateTime)r["DateDocument"], r["Path"].ToString());
                        if (r["Scan"] != DBNull.Value)
                        {
                          try
                          {
                                if (!net.CopyBytesWithServer((byte[])r["Scan"],r["Path"].ToString(), r["cName"].ToString() + r["Extension"].ToString()))
                                {
                                    MessageBox.Show("Не удалось сохранить файл " + r["Path"].ToString() + "\\"
                              + r["cName"].ToString() + r["Extension"].ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                

                           /* File.WriteAllBytes(r["Path"].ToString() + "\\"
                              + r["cName"].ToString() + r["Extension"].ToString(),
                              (byte[])r["Scan"]);*/
                          }
                          catch (Exception ex)
                          {
                            MessageBox.Show("Не удалось сохранить файл " + r["Path"].ToString() + "\\"
                              + r["cName"].ToString() + r["Extension"].ToString() + "\nТекст ошибки: "
                              + ex.Message);
                          }
                        }
                        //Logging.Comment("ID: " + id_DopDoc);
                        //Logging.Comment("№ документа: " + num);
                        
                        Logging.Comment("Тип документа ID: " + r["id_DocType"]);// + " ; Наименование: " + cbTypeDoc.Text);
                        Logging.Comment("Наименование файла: " + r["cName"].ToString() + " ;Расширение: " + r["Extension"].ToString());
                        //Logging.Comment("Дата документа: " + dateadddoc.Value.ToShortDateString());
                    }
                }
                Logging.Comment("Данные договора, к которому добавляется внешний файл");
                Logging.Comment("Дата заключения договора: " + oldDoc.ToShortDateString());
                Logging.Comment("Номер договора: " + num_doc);
                Logging.Comment("Арендатор ID: " + _old_id_ten + "; Наименование: " + oldTen);
                Logging.Comment("Арендодатель ID: " + _old_id_lord + "; Наименование: " + oldLord);

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
              + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }


        private string num_doc, oldTen, oldLord;
        private int _old_id_ten, _old_id_lord;
        private DateTime oldDoc;

        public void setDocData(string num_doc, string oldTen, string oldLord, int _old_id_ten, int _old_id_lord, DateTime oldDoc)
        {
            this.num_doc = num_doc;
            this.oldTen = oldTen;
            this.oldLord = oldLord;

            this._old_id_ten = _old_id_ten;
            this._old_id_lord = _old_id_lord;

            this.oldDoc = oldDoc;
        }

        private void tbNameImg_KeyPress(object sender, KeyPressEventArgs e)
        {
          if (e.KeyChar == '%')
            e.Handled = true;
        }
    }
}
