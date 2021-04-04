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
using Microsoft.WindowsAPICodePack.Dialogs;
using Nwuram.Framework.Logging;

namespace DllLink1CForAgreements
{
    public partial class frmLoaderFile1C : Form
    {
        private string pathSign = "";
        private string pathSignTmpPDF = "";
        private string pathEndParse = "";
        private static Random getrandom = new Random();
        private List<FileData> lFileData = new List<FileData>();
        private NetworkShare net;

        private List<string> lStringError = new List<string>();
        private Nwuram.Framework.UI.Service.EnableControlsServiceInProg blockers = new Nwuram.Framework.UI.Service.EnableControlsServiceInProg();


        #region "Over"
        public frmLoaderFile1C()
        {
            InitializeComponent();
            if(Config.hCntMain==null)
                Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);            
            DataTable providers = (new OleDbEnumerator()).GetElements();
        }

        private void frmLoaderFile1C_Load(object sender, EventArgs e)
        {
            DataTable conf = Config.hCntMain.EditGetConf(ConnectionSettings.GetIdProgram(), "", "");
            EnumerableRowCollection<DataRow> rowCollect = conf.AsEnumerable().Where(r => r.Field<string>("id_value").Equals("psss"));
            if (rowCollect.Count() > 0)
            {
                net = new NetworkShare(true, false);
                net.ConnectToShare();
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

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            
            //dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {                
                if (!validateFileAndFolder(dialog.FileName)) return;

                btSave.Enabled = true;
                tbPath.Text = dialog.FileName;
            }

            //if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            //{
            //    if (!validateFileAndFolder(folderBrowserDialog1.SelectedPath)) return;

            //    btSave.Enabled = true;
            //    tbPath.Text = folderBrowserDialog1.SelectedPath;

            //    //Type folderBrowserType = folderBrowserDialog1.GetType();
            //    //FieldInfo fieldInfo = folderBrowserType.GetField("rootFolder", BindingFlags.NonPublic | BindingFlags.Instance);
            //    //fieldInfo.SetValue(folderBrowserDialog1, (Environment.SpecialFolder)18);

            //    //folderBrowserDialog1.ShowDialog();
            //}
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btSave_Click(object sender, EventArgs e)
        {
            blockers.SaveControlsEnabledState(this);
            blockers.SetControlsEnabled(this, false);
            progressBar1.Visible = true;
            var result = await Task<bool>.Factory.StartNew(() =>
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

                Logging.StartFirstLevel((int)LogEvents.Добавление_связи_договора_в_БД);

                if (listFileExcel.Count() > 0) Logging.Comment("Обработка файлов Excel");
                foreach (string filePath in listFileExcel)
                {
                    ParseExcelFile(filePath);
                }

                if (listFileWord.Count() > 0) Logging.Comment("Обработка файлов Word");
                foreach (string filePath in listFileWord)
                {
                    ParseWordFile(filePath);
                }

                Logging.StopFirstLevel();


                if (lStringError.Count > 0)
                {
                    Logging.StartFirstLevel((int)LogEvents.Нет_Подписей);
                    string msg = "У следующих арендодателей\nотсутствуют файлы подписи для\nсчетов 1С.\n";
                    Logging.Comment("Отсутствуют подписи для создания pdf файлов у арендодателей");
                    foreach (string s in lStringError)
                    {
                        Logging.Comment(s);
                        msg += $"- {s}\n";
                    }
                    Logging.StopFirstLevel();
                    msg += $"Их файлы не будут обработаны.";
                    MessageBox.Show(Config.centralText(msg + "\n"), "У арендодателей отсутствуют подписи", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                

                if (lFileData.Count > 0)
                {
                    if (DialogResult.Yes == MessageBox.Show(Config.centralText("В процессе загрузки счетов 1С найдены счета\nбез связи с договором. Установить связь\n вручную и обработать счета заново?\n"), "Обнаружены не связанные договора", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        lStringError.Clear();
                        if (DialogResult.OK == new frmLinkAgreementVs1C() { lFileData = lFileData.ToList() }.ShowDialog())
                        {
                            var ListToParse = lFileData.AsEnumerable<FileData>().Where(r => r.idAgreement != 0);

                            if (ListToParse.Count() > 0)
                            {
                                Logging.StartFirstLevel((int)LogEvents.Ручное_добавление_связи_договора_в_БД);
                                foreach (FileData fData in ListToParse)
                                {
                                    if (!AddSignAndConvertToPDF(fData.tFile, fData))
                                    {

                                    }
                                }
                                Logging.StopFirstLevel();

                                if (lStringError.Count > 0)
                                {
                                    Logging.StartFirstLevel((int)LogEvents.Нет_Подписей);
                                    Logging.Comment("Отсутствуют подписи для создания pdf файлов у арендодателей");
                                    string msg = "У следующих арендодателей\nотсутствуют файлы подписи для\nсчетов 1С.\n";
                                    foreach (string s in lStringError)
                                    {
                                        Logging.Comment(s);
                                        msg += $"- {s}\n";
                                    }
                                    Logging.StopFirstLevel();
                                    msg += $"Их файлы не будут обработаны.";
                                    MessageBox.Show(Config.centralText(msg + "\n"), "У арендодателей отсутствуют подписи", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                        }
                    }
                }
                Config.DoOnUIThread(() =>
                {
                    blockers.RestoreControlEnabledState(this);
                    progressBar1.Visible = false;
                }, this);
                MessageBox.Show(Config.centralText("Загрузка счетов 1С\nзавершена.\n"), "Загрузка счетов 1С", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            });
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

        #region "Excel"
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
                    //delta = -2;
                    delta = -3;
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

                if (dtexcel != null && dtexcel.Rows.Count >= 24 + delta)
                {
                    string s1 = dtexcel.Rows[10 + delta][1].ToString();//9
                    s1 = s1.Replace("\r\a", string.Empty);

                    string s2 = dtexcel.Rows[20 + delta][6].ToString();//19
                    s2 = s2.Replace("\r\a", string.Empty);

                    string s3 = dtexcel.Rows[23 + delta][3].ToString();//22
                    s3 = s3.Replace("\r\a", string.Empty);

                    int positonInsertSign = 0;
                    //for (int i = 23+ delta; i < dtexcel.Rows.Count; i++)
                    //{
                    //    if (dtexcel.Rows[i][0].ToString().ToLower().Contains("руководитель")
                    //        || dtexcel.Rows[i][1].ToString().ToLower().Contains("руководитель")
                    //        || dtexcel.Rows[i][2].ToString().ToLower().Contains("руководитель")
                    //        || dtexcel.Rows[i][3].ToString().ToLower().Contains("руководитель")
                    //        || dtexcel.Rows[i][4].ToString().ToLower().Contains("руководитель"))
                    //    {
                    //        positonInsertSign = i;
                    //        break;
                    //    }
                    //}

                    if (s1 != null && s2 != null && s3 != null && s1.Length != 0 && s2.Length != 0 && s3.Length != 0)
                    {
                        parserText(s1, s2, s3, filePath, typeFile.excel, positonInsertSign);
                    }
                    else
                    {
                        Console.WriteLine("Not Find");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{filePath}: {ex.Message}", "");
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
            object paramMissing = Type.Missing;
            var app = new Microsoft.Office.Interop.Excel.Application();
            app.Caption = System.Guid.NewGuid().ToString().ToUpper();            
            string ver = app.Version;
            var wb = app.Workbooks.Open(filePath);

            

            app.DisplayAlerts = false;
            wb.SaveAs(filePath, FileFormat: Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
            wb.Close(false, paramMissing, paramMissing);
            app.Quit();

            Config.EnsureProcessKilled(IntPtr.Zero, app.Caption);
            

            Marshal.ReleaseComObject(wb);
            Marshal.FinalReleaseComObject(wb);
            wb = null;

            Marshal.ReleaseComObject(app);
            Marshal.FinalReleaseComObject(app);
            app = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();

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

                int indexTable = -1;//1
                string s1 = null, s2 = null, s3 = null;

                foreach (Word.Table WordTable in doc.Tables)
                {
                    ////foreach (Word.Column columns in WordTable.Columns)
                    ////{
                    //try
                    //{
                    //    foreach (Word.Row row in WordTable.Rows)
                    //    {
                    //        try
                    //        {
                    //            foreach (Word.Cell cell in row.Cells)
                    //                try
                    //                {
                    //                    Console.WriteLine($"[{cell.RowIndex}:{cell.ColumnIndex}]:{cell.Range.Text}");

                    //                }
                    //                catch (Exception ex)
                    //                {

                    //                }
                    //        }
                    //        catch (Exception ex)
                    //        {

                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //}
                    ////}

                    if (indexTable == 1)
                    {
                        if (WordTable.Rows.Count > 10)
                        {
                            //s1 = WordTable.Cell(10, 2).Range.Text;
                            s1 = WordTable.Cell(1, 2).Range.Text;
                            s1 = s1.Replace("\r\a", string.Empty);
                            //Console.WriteLine(s1);
                        }

                        //if (WordTable.Rows.Count > 20)
                        if (WordTable.Rows.Count > 11)
                        {
                            //s2 = WordTable.Cell(20, 3).Range.Text;
                            s2 = WordTable.Cell(11, 3).Range.Text;
                            s2 = s2.Replace("\r\a", string.Empty);
                            //Console.WriteLine(s2);
                        }
                    }
                    else if (indexTable == 2)
                    {
                        if (WordTable.Rows.Count >= 2)
                        {
                            s3 = WordTable.Cell(2, 3).Range.Text;
                            s3 = s3.Replace("\r\a", string.Empty);
                            //Console.WriteLine(s3);
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
                    parserText(s1, s2, s3, FileName.ToString(), typeFile.word, 0);
                }
                else
                {
                    Console.WriteLine("Not Find");
                }
                //Console.WriteLine();
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
 
        private void parserText(string s1, string s2, string s3, string file, typeFile tFile,int positonInsertSign)
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
                int id_Landlord= (int)dtTmp.Rows[0]["id_Landlord"];
                string nameObject = (string)dtTmp.Rows[0]["nameObject"];

                FileData fData = new FileData();
                fData.setData(file, file, num, date, agreement, s3, id_agreement, isAdd, nameLandLord, tFile, id_Landlord, nameObject, positonInsertSign);
                if (!AddSignAndConvertToPDF(tFile, fData))
                {

                }
            }
            else
            {
                FileData fData = new FileData();
                fData.setData(file, file, num, date, agreement, s3, 0, true, "", tFile, 0, "", positonInsertSign);
                lFileData.Add(fData);
            }
        }

        private string GetImageAgreements(int id_agreement)
        {
            string link = $"{pathSign}\\{id_agreement}";
            if (!Directory.Exists(link)) return "";

            string[] files = Directory.GetFiles(link);
            if (files.Count() == 0) return "";

            getrandom = new Random((int)DateTime.Now.Ticks);
            int value = getrandom.Next(0, files.Length - 1);

            return files[value];
        }

        private bool AddSignAndConvertToPDF(typeFile tFile, FileData fData)
        {
            string FileName = fData.FileName;
            FileInfo newFile = new FileInfo(FileName);
            string FileNameToSaveAndSign = pathSignTmpPDF + "\\" + Path.GetFileNameWithoutExtension(FileName) + newFile.Extension;
            string FileAndParse = pathEndParse + "\\" + Path.GetFileNameWithoutExtension(FileName) + newFile.Extension;
            File.Copy(FileName, FileNameToSaveAndSign, true);
            string filePDF = pathSignTmpPDF + "\\" + Path.GetFileNameWithoutExtension(FileName) + ".pdf";
            newFile = new FileInfo(FileNameToSaveAndSign);

            Logging.Comment($"Начало обработки файла: { Path.GetFileNameWithoutExtension(filePDF)}");


            if (tFile == typeFile.word)
            {
                using (var document = DocX.Load(FileNameToSaveAndSign))
                {
                    string StrImage = GetImageAgreements(fData.id_Landlord);
                    if (StrImage.Length == 0)
                    {
                        if (File.Exists(FileNameToSaveAndSign))
                            File.Delete(FileNameToSaveAndSign);

                        string sError = $"{fData.nameLandLord}: Отсутствует файлы для подписи";

                        if (!lStringError.Contains(fData.nameLandLord.Trim()))
                            lStringError.Add(fData.nameLandLord + "/" + fData.nameObject);

                        return false;
                    }
                    Xceed.Document.NET.Image image = document.AddImage(StrImage);
                    Picture picture = image.CreatePicture();
                    //picture.Rotation = 10;
                    picture.SetPictureShape(BasicShapes.cube);
                    //picture.Height = 115;
                    //picture.Width = 931;

                    //Table table = document.Tables[3];                    
                    //table.Rows[7].Remove();
                    //table.Rows[8].Remove();
                    //table.Rows[7].MergeCells(0, table.Rows[7].Cells.Count);
                    //table.Rows[7].Cells[0].Paragraphs[0].AppendPicture(picture);

                    Table table = document.Tables[5];
                    //table.Rows[7].Remove();
                    table.Rows[10].Remove();
                    table.Rows[10].MergeCells(0, table.Rows[10].Cells.Count);
                    //table.Rows[10].Cells[0].Paragraphs[0].InsertText("test");
                    table.Rows[10].Cells[0].Paragraphs[0].AppendPicture(picture);

                    document.Save();
                }

                filePDF = cnvWordToPDF.ConvertData(FileNameToSaveAndSign);
            }
            else if (tFile == typeFile.excel)
            {
                if (newFile.Extension.Equals(".xls"))
                {
                    object paramMissing = Type.Missing;
                    string newFileName = newFile.DirectoryName + "\\" + Path.GetFileNameWithoutExtension(FileName) + ".xlsx";

                    var app = new Microsoft.Office.Interop.Excel.Application();
                    app.Caption = System.Guid.NewGuid().ToString().ToUpper();
                    var wb = app.Workbooks.Open(FileName);
                    wb.SaveAs(newFileName, FileFormat: Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
                    wb.Close(false, paramMissing, paramMissing);
                    app.Quit();
                    Config.EnsureProcessKilled(IntPtr.Zero, app.Caption);

                    Marshal.ReleaseComObject(wb);
                    Marshal.FinalReleaseComObject(wb);
                    wb = null;

                    Marshal.ReleaseComObject(app);
                    Marshal.FinalReleaseComObject(app);
                    app = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    File.Delete(FileNameToSaveAndSign);
                    FileNameToSaveAndSign = newFileName;
                    newFile = new FileInfo(FileNameToSaveAndSign);
                }

                if (newFile.Extension.Equals(".xlsx"))
                {

                    string StrImage = GetImageAgreements(fData.id_Landlord);
                    if (StrImage.Length == 0)
                    {
                        if (File.Exists(FileNameToSaveAndSign))
                            File.Delete(FileNameToSaveAndSign);

                        string sError = $"{fData.nameLandLord}: Отсутствует файлы для подписи";
                        if (!lStringError.Contains(fData.nameLandLord.Trim()))
                            //lStringError.Add(fData.nameLandLord);
                            lStringError.Add(fData.nameLandLord + "/" + fData.nameObject);
                        return false;
                    }


                    ExcelPackage epp = new ExcelPackage(newFile);
                    Bitmap image = new Bitmap(StrImage);
                    OfficeOpenXml.Drawing.ExcelPicture excelImage = null;
                    var worksheet = epp.Workbook.Worksheets[0];

                    int countRow = worksheet.Dimension.End.Row;
                    int countColumns = worksheet.Dimension.End.Column;
                    bool isStop = false;
                    for (int i = countRow; i > 0; i--)
                    {
                        for (int j = 1; j < countColumns; j++)
                        {
                            object value = worksheet.Cells[i, j].Value;
                            if (value != null)
                            {
                                //Console.WriteLine(value);
                                if (value.ToString().ToLower().Equals("Руководитель".ToLower()) || value.ToString().ToLower().Equals("Предприниматель".ToLower()))
                                {
                                    fData.positonInsertSign = i-1;
                                    isStop = true;
                                    break;
                                }
                            }
                        }
                        if (isStop) break;
                    }

                    excelImage = worksheet.Drawings.AddPicture("image", image);

                    // In .SetPosition, we are using 8th Column and 8th Row, with 0 Offset 

                    //var rowCnt = worksheet.Dimension.End.Row;
                    //var colCnt = worksheet.Dimension.End.Column;

                    //worksheet.SetValue(fData.positonInsertSign, 1, "test");
                    //excelImage.SetPosition(38, 0, 0, 0);
                    excelImage.SetPosition(fData.positonInsertSign, 0, 0, 0);

                    //set size of image, 100= width, 100= height
                    //excelImage.SetSize(931, 115);
                    epp.Save();
                    //epp.SaveAs()
                }
                filePDF = cnvXLSToPDF.ConvertData(FileNameToSaveAndSign);

            }

            File.Delete(FileNameToSaveAndSign);
            newFile = new FileInfo(filePDF);

            DataTable dtScan = Config.hCntMain.getScan(fData.idAgreement, -1);
            bool isoverwrite = false;
            int id_Scane = 0;
            if (dtScan != null && dtScan.Rows.Count > 0)
            {
                EnumerableRowCollection<DataRow> rowCollectScan = dtScan.AsEnumerable().Where(r => r.Field<string>("cName").Contains(Path.GetFileNameWithoutExtension(filePDF))).OrderBy(r => r.Field<int>("id"));
                if (rowCollectScan.Count() > 0)
                {
                    DialogResult dlResult = DialogResult.Cancel;
                    Config.DoOnUIThread(() =>
                    {
                        dlResult = new MyMessageBox.MyMessageBox($"В каталоге арендатора уже существует файл с сохраняемым именем \r\n \"{Path.GetFileNameWithoutExtension(filePDF)}\"", "Сохранение PDF файла счёта", MyMessageBox.MessageBoxButtons.YesNoCancel, new List<string>(new string[] { "Перезаписать", "Создать копию", "Отмена" })) { Owner = this }.ShowDialog();
                    }, this);
                    if (dlResult == DialogResult.Cancel)
                    {
                        if (File.Exists(filePDF))
                            File.Delete(filePDF);

                        Logging.Comment($"{Path.GetFileNameWithoutExtension(filePDF)}: файл счёта не сохранён. Операция прервана пользователем");

                        MessageBox.Show(Config.centralText("PDF файл счёта не сохранён.\nОперация прервана пользователем\n"), "Сохранение PDF файла счёта", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return false;
                    }

                    if (dlResult == DialogResult.Yes)
                    {
                        Logging.Comment($"{Path.GetFileNameWithoutExtension(filePDF)}: Перезапись");
                        isoverwrite = true;
                        //id_Scane = (int)rowCollectScan.First()["id"];
                    }
                    else if (dlResult == DialogResult.No)
                    {
                        
                        string filePDFTmp = filePDF.Replace(Path.GetFileNameWithoutExtension(filePDF), Path.GetFileNameWithoutExtension(filePDF) + $"({rowCollectScan.Count()})");
                        File.Move(filePDF, filePDFTmp);
                        Logging.Comment($"{Path.GetFileNameWithoutExtension(filePDF)}: Копирование: Новое наименование файла: {Path.GetFileNameWithoutExtension(filePDFTmp)}");
                        filePDF = filePDFTmp;
                        
                    }
                }
            }

            string ServerPath = $"{net.server}\\{fData.idAgreement}";
            //if (id_Scane == 0)
            //{
                DataTable dtResult = Config.hCntMain.setScan(fData.idAgreement, Path.GetFileNameWithoutExtension(filePDF), newFile.Extension, 11, fData.Date, ServerPath);
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    id_Scane = (int)dtResult.Rows[0]["id"];
                    net.CopyFile(fData.idAgreement.ToString(), filePDF, Path.GetFileNameWithoutExtension(filePDF) + newFile.Extension, isoverwrite);
                }
            //}
            //else
            //{
            //    net.CopyFile(fData.idAgreement.ToString(), filePDF, Path.GetFileNameWithoutExtension(filePDF) + newFile.Extension, isoverwrite);
            //}

            Config.hCntMain.SetAgreement1CForAgreement(fData.idAgreement, fData.Number, fData.Date, fData.Agreement, fData.TypePay, fData.isAdd, id_Scane, !isoverwrite);
            Logging.Comment($"Запись в БД:[idAgreement:{fData.idAgreement};Номер: {fData.Number};Дата: {fData.Date}; Agreement: {fData.Agreement}; Тип оплаты:{fData.TypePay}]");
            File.Delete(filePDF);

            try
            {
                File.Move(FileName, FileAndParse);
            }
            catch (IOException ex)
            {
                newFile = new FileInfo(FileName);
                string[] listFileInDir = Directory.GetFiles(pathEndParse + "\\", Path.GetFileNameWithoutExtension(FileName)+"*");
                if (listFileInDir.Count() > 0)
                    FileAndParse = pathEndParse + "\\" + Path.GetFileNameWithoutExtension(FileName) + $"({listFileInDir.Count()})" + newFile.Extension;

                File.Move(FileName, FileAndParse);
            }

            Logging.Comment($"Завершение обработки файла: { Path.GetFileNameWithoutExtension(filePDF)}");

            return true;
        }
     
    }
}
