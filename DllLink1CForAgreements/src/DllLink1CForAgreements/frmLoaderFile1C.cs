using Nwuram.Framework.Settings.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Data.OleDb;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using System.Threading;

namespace DllLink1CForAgreements
{
    public partial class frmLoaderFile1C : Form
    {
        private List<FileData> lFileData = new List<FileData>();

        public frmLoaderFile1C()
        {
            InitializeComponent();



            DataTable providers = (new OleDbEnumerator()).GetElements();
        }

        private void frmLoaderFile1C_Load(object sender, EventArgs e)
        {
            DataTable conf = Config.hCntMain.EditGetConf(ConnectionSettings.GetIdProgram(), "", "");
            EnumerableRowCollection<DataRow> rowCollect = conf.AsEnumerable().Where(r => r.Field<string>("id_value").Equals("psss"));
            if (rowCollect.Count() > 0)
            {
                string path = rowCollect.First()["value"].ToString() + "\\sign";
                if (!Directory.Exists(path))
                {
                    MessageBox.Show(Config.centralText("Программа не обнаружила\nкаталога с подписями\nарендодателей.\nЗагрузка счетов 1С невозможна.\n"), "Загрузка счетов", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }
            }
        }

        private void btSelectPath_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                if (!validateFileAndFolder(folderBrowserDialog1.SelectedPath)) return;

                btSave.Enabled = true;
                tbPath.Text = folderBrowserDialog1.SelectedPath;

                //Type folderBrowserType = folderBrowserDialog1.GetType();
                //FieldInfo fieldInfo = folderBrowserType.GetField("rootFolder", BindingFlags.NonPublic | BindingFlags.Instance);
                //fieldInfo.SetValue(folderBrowserDialog1, (Environment.SpecialFolder)18);

                //folderBrowserDialog1.ShowDialog();
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void btSave_Click(object sender, EventArgs e)
        {
            lFileData.Clear();
            var listFileExcel = Directory.GetFiles(tbPath.Text).Where(s => s.EndsWith(".xls") || s.EndsWith(".xlsx"));
            var listFileWord = Directory.GetFiles(tbPath.Text).Where(s => s.EndsWith(".doc") || s.EndsWith(".docx"));
            var listFilePDF = Directory.GetFiles(tbPath.Text).Where(s => s.EndsWith(".pdf"));

            foreach (string filePath in listFileExcel)
            {
                ParseExcelFile(filePath);
                //ParseExcelFileNew(filePath);           
            }

            foreach (string filePath in listFileWord)
            {                
                ParseWordFile(filePath);
            }
        }

        private void ParseExcelFile(string filePath)
        {
            OleDbConnection conn = null;
            try
            {
                DataTable dtexcel = new DataTable();
                bool hasHeaders = false;
                string HDR = hasHeaders ? "Yes" : "No";
                string strConn;
                int delta = 0;
                if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                {
                    HDR = "Yes";
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=1\"";
                    delta = -2;
                    //strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;";
                    //strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;";
                }
                else
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=1\"";
                conn = new OleDbConnection(strConn);
                conn.Open();
                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                //Looping Total Sheet of Xl File
                /*foreach (DataRow schemaRow in schemaTable.Rows)
                {
                }*/
                //Looping a first Sheet of Xl File
                DataRow schemaRow = schemaTable.Rows[0];
                string sheet = schemaRow["TABLE_NAME"].ToString();
                if (!sheet.EndsWith("_"))
                {
                    string query = "SELECT  * FROM [" + sheet + "]";
                    OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                    dtexcel.Locale = CultureInfo.CurrentCulture;
                    daexcel.Fill(dtexcel);
                }

                if (dtexcel != null && dtexcel.Rows.Count >= 23 + delta)
                {
                    string s1 = dtexcel.Rows[9 + delta][1].ToString();
                    s1 = s1.Replace("\r\a", string.Empty);
                    Console.WriteLine(s1);

                    string s2 = dtexcel.Rows[19 + delta][6].ToString();
                    s2 = s2.Replace("\r\a", string.Empty);
                    Console.WriteLine(s2);

                    string s3 = dtexcel.Rows[22 + delta][3].ToString();
                    s3 = s3.Replace("\r\a", string.Empty);
                    Console.WriteLine(s3);
                    Console.WriteLine();


                    if (s1 != null && s2 != null && s3 != null)
                    {
                        parserText(s1, s2, s3, filePath);
                        cnvXLSToPDF.ConvertData(filePath);
                    }
                }
            }
            catch
            { }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        private void insertImage(string filePath,int id_agreement)
        { 
        
        }

        private void ParseWordFile(object FileName)
        {            
            object rOnly = true;
            object SaveChanges = false;
            object MissingObj = System.Reflection.Missing.Value;

            Word.Application app = new Word.Application();
            Word.Document doc = null;
            Word.Range range = null;
            try
            {
                doc = app.Documents.Open(ref FileName, ref MissingObj, ref rOnly, ref MissingObj,
                ref MissingObj, ref MissingObj, ref MissingObj, ref MissingObj,
                ref MissingObj, ref MissingObj, ref MissingObj, ref MissingObj,
                ref MissingObj, ref MissingObj, ref MissingObj, ref MissingObj);

                int indexTable = 1;
                string s1 = null, s2 = null, s3 = null;

                foreach (Word.Table WordTable in doc.Tables)
                {
                    if (indexTable == 1)
                    {
                        if (WordTable.Rows.Count > 10)
                        {
                            s1 = WordTable.Cell(10, 2).Range.Text;
                            s1 = s1.Replace("\r\a", string.Empty);
                            Console.WriteLine(s1);
                        }

                        if (WordTable.Rows.Count > 20)
                        {
                            s2 = WordTable.Cell(20, 3).Range.Text;
                            s2 = s2.Replace("\r\a", string.Empty);
                            Console.WriteLine(s2);
                        }
                    }
                    else if (indexTable == 2)
                    {
                        if (WordTable.Rows.Count >= 2)
                        {
                            s3 = WordTable.Cell(2, 3).Range.Text;
                            s3 = s3.Replace("\r\a", string.Empty);
                            Console.WriteLine(3);
                        }
                    }
                    indexTable++;                   
                }
                if (s1 != null && s2 != null && s3 != null)
                {
                    parserText(s1, s2, s3, FileName.ToString());
                    cnvWordToPDF.ConvertData(FileName.ToString());

                }
                Console.WriteLine();               
            }
            catch (Exception ex)
            {
                /* Обработка исключений */
            }
            finally
            {
                /* Очистка неуправляемых ресурсов */
                if (doc != null)
                {
                    doc.Close(ref SaveChanges);
                }
                if (range != null)
                {
                    Marshal.ReleaseComObject(range);
                    range = null;
                }
                if (app != null)
                {
                    app.Quit();
                    Marshal.ReleaseComObject(app);
                    app = null;
                }
            }
        }

        private void parserText(string s1, string s2, string s3, string file)
        {
            //string num = s1.Substring(s1.IndexOf("№") + 1, s1.IndexOf("от") - s1.IndexOf("№") - 1).Trim();
            string num = s1.Substring(s1.IndexOf("№") + 1).Trim();
            num = num.Substring(0, num.IndexOf("от")).Trim();
            string sDate = s1.Substring(s1.IndexOf("от")).Replace("от",string.Empty).Replace("г.", string.Empty).Trim();
            DateTime date = DateTime.Parse(sDate);

            string agreement = s2.Replace("Д",string.Empty).Replace("№", string.Empty).Trim();
            if (agreement.IndexOf(" ") != -1)
                agreement = agreement.Substring(0, agreement.IndexOf(" ")).Trim();
            
            FileData fData = new FileData();
            fData.setData(file, file, num, date, agreement, s3);
            lFileData.Add(fData);
        }

        private void tbPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Control)
            {
                if (Directory.Exists(Clipboard.GetText()))
                {

                    if (!validateFileAndFolder(Clipboard.GetText())) return;


                    tbPath.Text = Clipboard.GetText();
                    btSave.Enabled = true;
                }
            }
        }

        private bool validateFileAndFolder(string path)
        {
            try
            {
                if (!File.Exists(path + "\\text.txt"))
                    System.IO.File.WriteAllText(path + "\\text.txt", "текст");
                Thread.Sleep(100);
                if (File.Exists(path + "\\text.txt"))
                    File.Delete(path + "\\text.txt");
            }
            catch
            {
                MessageBox.Show(Config.centralText("Программа не может получить доступ в указанный\nкаталог для чтения и записи файлов.\nОперация загрузки счетов 1С прервана.\nОбратитесь в ОЭЭС.\n"), "Проверка наличия файлов", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            var listFile = Directory.GetFiles(path).Where(s => s.EndsWith(".xls") || s.EndsWith(".xlsx") || s.EndsWith(".doc") || s.EndsWith(".docx"));
            if (listFile.Count() == 0)
            {
                MessageBox.Show(Config.centralText("В указаном каталоге отсутствуют файлы\n с допустимыми расширениями счетов 1С.\nОперация загрузки счетов 1С прервана.\nОбратитесь в ОЭЭС.\n"), "Проверка наличия файлов", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }


     
    }
}
