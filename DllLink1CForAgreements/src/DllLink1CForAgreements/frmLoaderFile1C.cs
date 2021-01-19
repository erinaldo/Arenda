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
using OfficeOpenXml;
using Xceed.Words.NET;
using Xceed.Document.NET;

namespace DllLink1CForAgreements
{
    public partial class frmLoaderFile1C : Form
    {
        private string pathSign = "";
        private string pathSignTmpPDF = "";
        private string pathEndParse = "";
        private static readonly Random getrandom = new Random();
        private List<FileData> lFileData = new List<FileData>();
        private NetworkShare net;

        private List<string> lStringError = new List<string>();

       

        #region "Over"
        public frmLoaderFile1C()
        {
            InitializeComponent();
            if(Config.hCntMain==null)
                Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
            //DataTable providers = (new OleDbEnumerator()).GetElements();
        }

        private void frmLoaderFile1C_Load(object sender, EventArgs e)
        {
            DataTable conf = Config.hCntMain.EditGetConf(ConnectionSettings.GetIdProgram(), "", "");
            EnumerableRowCollection<DataRow> rowCollect = conf.AsEnumerable().Where(r => r.Field<string>("id_value").Equals("psss"));
            if (rowCollect.Count() > 0)
            {
                pathSign = rowCollect.First()["value"].ToString() + "\\sign";
                if (!Directory.Exists(pathSign))
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
            lStringError.Clear();
            net = new NetworkShare(true, false);
            pathSignTmpPDF = tbPath.Text + "\\tmpPdfSign\\";
            pathEndParse = tbPath.Text + "\\EndParse\\";

            if (!Directory.Exists(pathSignTmpPDF)) Directory.CreateDirectory(pathSignTmpPDF);
            if (!Directory.Exists(pathEndParse)) Directory.CreateDirectory(pathEndParse);


            lFileData.Clear();
            var listFileExcel = Directory.GetFiles(tbPath.Text).Where(s => s.EndsWith(".xls") || s.EndsWith(".xlsx"));
            var listFileWord = Directory.GetFiles(tbPath.Text).Where(s => s.EndsWith(".doc") || s.EndsWith(".docx"));
            var listFilePDF = Directory.GetFiles(tbPath.Text).Where(s => s.EndsWith(".pdf"));

            foreach (string filePath in listFileExcel)
            {
                ParseExcelFile(filePath);                
            }

            foreach (string filePath in listFileWord)
            {                
                ParseWordFile(filePath);
            }

            if (lStringError.Count > 0)
            {
                string msg = "У следующих арендодателей\nотсутствуют файлы подписи для\nсчетов 1С.\n";
                foreach (string s in lStringError)
                {
                    msg += $"- {s}\n";
                }
                msg += $"Их файлы не будут обработаны.";
                MessageBox.Show(Config.centralText(msg + "\n"), "У арендодателей отсутствуют подписи", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (lFileData.Count > 0)
            {
                if (DialogResult.Yes == MessageBox.Show(Config.centralText("В процессе загрузки счетов 1С найдены счета\nбез связи с договором. Установить связь\n вручную и обработать счета заново?\n"),"Обнаружены не связанные договора",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2))
                {

                    if (DialogResult.OK == new frmLinkAgreementVs1C() { lFileData = lFileData.ToList() }.ShowDialog())
                    {
                        var ListToParse = lFileData.AsEnumerable<FileData>().Where(r => r.idAgreement != 0);

                        if (ListToParse.Count() > 0)
                        {
                            foreach (FileData fData in ListToParse)
                            {
                                if (!AddSignAndConvertToPDF(fData.tFile, fData))
                                {

                                }
                            }

                            if (lStringError.Count > 0)
                            {
                                string msg = "У следующих арендодателей\nотсутствуют файлы подписи для\nсчетов 1С.\n";
                                foreach (string s in lStringError)
                                {
                                    msg += $"- {s}\n";
                                }
                                msg += $"Их файлы не будут обработаны.";
                                MessageBox.Show(Config.centralText(msg + "\n"), "У арендодателей отсутствуют подписи", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                    }
                }
            }

            MessageBox.Show(Config.centralText("Загрузка счетов 1С\nзавершена.\n"), "Загрузка счетов 1С", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        #endregion

        #region "excel"
        private void ParseExcelFile(string filePath)
        {
            FileInfo newFile = new FileInfo(filePath);
            if (newFile.Extension.Equals(".xlsx"))
            {
                reSaveFile(filePath);
            }
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
                }
                else
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=1\"";
                conn = new OleDbConnection(strConn);
                conn.Open();
                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
               
                DataRow schemaRow = schemaTable.Rows[0];
                string sheet = schemaRow["TABLE_NAME"].ToString();
                if (!sheet.EndsWith("_"))
                {
                    string query = "SELECT  * FROM [" + sheet + "]";
                    OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                    dtexcel.Locale = CultureInfo.CurrentCulture;
                    daexcel.Fill(dtexcel);
                }

                if (conn != null)
                    conn.Close();

                if (dtexcel != null && dtexcel.Rows.Count >= 23 + delta)
                {
                    string s1 = dtexcel.Rows[9 + delta][1].ToString();
                    s1 = s1.Replace("\r\a", string.Empty);

                    string s2 = dtexcel.Rows[19 + delta][6].ToString();
                    s2 = s2.Replace("\r\a", string.Empty);

                    string s3 = dtexcel.Rows[22 + delta][3].ToString();
                    s3 = s3.Replace("\r\a", string.Empty);


                    if (s1 != null && s2 != null && s3 != null)
                    {
                        parserText(s1, s2, s3, filePath, typeFile.excel);                                                
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{filePath}: {ex.Message}");
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        private void reSaveFile(string filePath)
        {
            var app = new Microsoft.Office.Interop.Excel.Application();
            var wb = app.Workbooks.Open(filePath);
            app.DisplayAlerts = false;
            wb.SaveAs(filePath, FileFormat: Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
            wb.Close();
            app.Quit();
        }
        #endregion

        #region "Word"

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

                if (doc != null)
                {
                    doc.Close(ref SaveChanges);
                    doc = null;
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


                if (s1 != null && s2 != null && s3 != null)
                {
                    parserText(s1, s2, s3, FileName.ToString(),typeFile.word);                 
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

        #endregion
 
        private void parserText(string s1, string s2, string s3, string file, typeFile tFile)
        {
            //string num = s1.Substring(s1.IndexOf("№") + 1, s1.IndexOf("от") - s1.IndexOf("№") - 1).Trim();
            string num = s1.Substring(s1.IndexOf("№") + 1).Trim();
            num = num.Substring(0, num.IndexOf("от")).Trim();
            string sDate = s1.Substring(s1.IndexOf("от")).Replace("от", string.Empty).Replace("г.", string.Empty).Trim();
            DateTime date = DateTime.Parse(sDate);

            string agreement = s2.Replace("Д", string.Empty).Replace("№", string.Empty).Trim();
            if (agreement.IndexOf(" ") != -1)
                agreement = agreement.Substring(0, agreement.IndexOf(" ")).Trim();

            DataTable dtTmp = Config.hCntMain.FindAgreement1CForAgreement(agreement);

            if (dtTmp != null && dtTmp.Rows.Count > 0)
            {
                int id_agreement = (int)dtTmp.Rows[0]["id"];
                bool isAdd = (bool)dtTmp.Rows[0]["isAdd"];
                string nameLandLord = (string)dtTmp.Rows[0]["nameLandLord"];

                FileData fData = new FileData();
                fData.setData(file, file, num, date, agreement, s3, id_agreement, isAdd, nameLandLord, tFile);
                if (!AddSignAndConvertToPDF(tFile, fData))
                {

                }
            }
            else
            {
                FileData fData = new FileData();
                fData.setData(file, file, num, date, agreement, s3, 0, true, "", tFile);
                lFileData.Add(fData);
            }
        }

        private string GetImageAgreements(int id_agreement)
        {
            string link = $"{pathSign}\\{id_agreement}";
            if (!Directory.Exists(link)) return "";

            string[] files = Directory.GetFiles(link);
            
            int value = getrandom.Next(0, files.Length - 1);

            return files[value];
        }

        private bool  AddSignAndConvertToPDF(typeFile tFile, FileData fData)
        {
            string FileName = fData.FileName;
            FileInfo newFile = new FileInfo(FileName);
            string FileNameToSaveAndSign = pathSignTmpPDF + "\\" + Path.GetFileNameWithoutExtension(FileName) + newFile.Extension;
            string FileAndParse = pathEndParse + "\\" + Path.GetFileNameWithoutExtension(FileName) + newFile.Extension;
            File.Copy(FileName, FileNameToSaveAndSign, true);
            string filePDF = "";
            newFile = new FileInfo(FileNameToSaveAndSign);


            if (tFile == typeFile.word)
            {
                using (DocX document = DocX.Load(FileNameToSaveAndSign))
                {
                    string StrImage = GetImageAgreements(fData.idAgreement);
                    if (StrImage.Length == 0)
                    {
                        if(File.Exists(FileNameToSaveAndSign))
                            File.Delete(FileNameToSaveAndSign);

                        string sError = $"{fData.nameLandLord}: Отсутствует файлы для подписи";

                        if (!lStringError.Contains(fData.nameLandLord.Trim()))
                            lStringError.Add(fData.nameLandLord);

                        return false;
                    }
                    Xceed.Document.NET.Image image = document.AddImage(StrImage);
                    Picture picture = image.CreatePicture();
                    //picture.Rotation = 10;
                    picture.SetPictureShape(BasicShapes.cube);

                    Table table = document.Tables[3];
                    table.Rows[7].Remove();
                    table.Rows[8].Remove();
                    table.Rows[7].MergeCells(0, table.Rows[7].Cells.Count);
                    table.Rows[7].Cells[0].Paragraphs[0].AppendPicture(picture);
                    document.Save();
                }

                filePDF = cnvWordToPDF.ConvertData(FileNameToSaveAndSign);
            }
            else if (tFile == typeFile.excel)
            {
                if (newFile.Extension.Equals(".xls"))
                {
                    string newFileName = newFile.DirectoryName + "\\" + Path.GetFileNameWithoutExtension(FileName) + ".xlsx";

                    var app = new Microsoft.Office.Interop.Excel.Application();
                    var wb = app.Workbooks.Open(FileName);
                    wb.SaveAs(newFileName, FileFormat: Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
                    wb.Close();
                    app.Quit();
                    File.Delete(FileNameToSaveAndSign);
                    FileNameToSaveAndSign = newFileName;
                    newFile = new FileInfo(FileNameToSaveAndSign);
                }

                if (newFile.Extension.Equals(".xlsx"))
                {
                    ExcelPackage epp = new ExcelPackage(newFile);
                    string StrImage = GetImageAgreements(fData.idAgreement);
                    if (StrImage.Length == 0)
                    {
                        if (File.Exists(FileNameToSaveAndSign))
                            File.Delete(FileNameToSaveAndSign);

                        string sError = $"{fData.nameLandLord}: Отсутствует файлы для подписи";
                        if (!lStringError.Contains(fData.nameLandLord.Trim()))
                            lStringError.Add(fData.nameLandLord);
                        return false;
                    }

                    Bitmap image = new Bitmap(StrImage);
                    OfficeOpenXml.Drawing.ExcelPicture excelImage = null;
                    var worksheet = epp.Workbook.Worksheets[0];

                    excelImage = worksheet.Drawings.AddPicture("image", image);

                    // In .SetPosition, we are using 8th Column and 8th Row, with 0 Offset 
                    //worksheet.SetValue(34, 0, "");
                    excelImage.SetPosition(34, 0, 0, 0);

                    //set size of image, 100= width, 100= height
                    //excelImage.SetSize(100, 100);
                    epp.Save();
                    //epp.SaveAs()
                }
                filePDF =  cnvXLSToPDF.ConvertData(FileNameToSaveAndSign);

            }




            File.Delete(FileNameToSaveAndSign);            
            newFile = new FileInfo(filePDF);
            net.CopyFile(fData.idAgreement.ToString(), filePDF, Path.GetFileNameWithoutExtension(filePDF) + newFile.Extension);

            string ServerPath = $"{net.server}\\{fData.idAgreement}";
            int id_Scane = 0;
            DataTable dtResult = Config.hCntMain.setScan(fData.idAgreement, Path.GetFileNameWithoutExtension(filePDF), newFile.Extension, 11, fData.Date, ServerPath);
            if (dtResult != null && dtResult.Rows.Count > 0)
                id_Scane = (int)dtResult.Rows[0]["id"];

            Config.hCntMain.SetAgreement1CForAgreement(fData.idAgreement, fData.Number, fData.Date, fData.Agreement, fData.TypePay, fData.isAdd, id_Scane);            
            File.Delete(filePDF);
            File.Move(FileName, FileAndParse);

            return true;
        }
     
    }
}
