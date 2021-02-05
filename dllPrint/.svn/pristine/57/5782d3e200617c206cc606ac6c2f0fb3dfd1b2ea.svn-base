using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.ToExcel;

namespace ArendaPrint
{
    public partial class frmPrint : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        /// <summary>
        /// Инициализация формы печати
        /// </summary>
        /// <param name="_id">id_agreement</param>
        /// <param name="_number">номер договора</param>
        /// <param name="_idtd">тип контракта: 1 - Аренда площади, 2 - Аренда рекламного места, 3 - Аренда земельного участка (s_TypeContract)</param>
        public frmPrint(int _id, string _number, int _idtd)
        {
            InitializeComponent();
            id = _id;
            number = _number;
            idtd = _idtd;
        }
        int id, idtd;
        string Path = "", number = "", Dir = "", Temp = "";
        string actp, actr, sogr;

        

        private void frmPrint_Load(object sender, EventArgs e)
        {
            CheckBoxEnabled();
            Task<DataTable> task = _proc.GetLD(id);
            task.Wait();
            DataTable dt = task.Result;

            DateAddDoc = "от ";
            if (decimal.Parse(dt.Rows[0]["Reklama"].ToString()) > 0)
            {
                Rekl = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Task<DataTable> task = _proc.GetLD(id);
            task.Wait();

            DataTable dt = task.Result;

            if (decimal.Parse(dt.Rows[0]["Reklama"].ToString()) > 0)
            {
                Rekl = true;
            }

            Temp = dt.Rows[0]["Path"].ToString();

            GetSettings();
            if (Path == "")
            {
                MessageBox.Show("В настройках не задан путь для выгрузки.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (number == "")
                {
                    MessageBox.Show("Невозможно создать папку с пустым наименованием\n Проверьте номер договора.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    number = number.Replace("\\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("|", "");

                    Dir = Path + "\\" + number + "\\";
                    if (!Directory.Exists(Dir))
                    {
                        //создаем каталог
                        try
                        {
                            Directory.CreateDirectory(Dir);
                            //var Direct = new DirectoryInfo(Dir);
                            //Direct.Attributes &= ~FileAttributes.ReadOnly;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    //если все чек-боксы сняты errorChecked=true
                    bool errorChecked = true;
                    foreach (Control con in groupBox1.Controls.Cast<Control>().OrderBy(c => c.TabIndex))
                    {
                        CheckBox chb1 = con as CheckBox;
                        if (chb1 != null)
                        {
                            if (chb1.Checked == true)
                            {
                                errorChecked = false;
                            }
                        }
                    }

                    //печатаем если выбран хотя бы один чек-бокс
                    if (!errorChecked)
                    {
                        Logging.StartFirstLevel(79);
                        Logging.Comment("Выгрузка отчета по договору");

                        foreach (Control cnt in groupBox1.Controls)
                        {
                            if (cnt is CheckBox)
                            {
                                if ((cnt as CheckBox).Checked)
                                {
                                    string fTitle = (cnt as CheckBox).Name.Replace("chb", "");
                                    string strToLog = (cnt as CheckBox).Text;
                                    foreach (Control cCmb in groupBox1.Controls)
                                    {
                                        if (cCmb is ComboBox)
                                        {
                                            string cmbName = (cCmb as ComboBox).Name.Replace("cbo", "").Replace("cmb", "");
                                            if (fTitle.ToLower().Equals(cmbName.ToLower()))
                                            {
                                                strToLog += " " + (cCmb as ComboBox).Text;
                                                break;
                                            }
                                        }
                                    }

                                    Logging.Comment(strToLog);
                                }
                            }
                        }

                        Logging.Comment("Данные арендатора, для  которого печатается отчет");
                        Logging.Comment("Дата документа: " + dateDoc);
                        Logging.Comment("Арендатель Наименование: " + nameArend);
                        Logging.Comment("Арендодатель Наименование: " + idArend);
                        Logging.Comment("№ договора: " + numDoc);
                        Logging.Comment("Начало: " + dateStartDoc);
                        Logging.Comment("Конец: " + dateEndDoc);
                        Logging.Comment("Место: " + position);

                        Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                        + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                        Logging.StopFirstLevel();

                        //выгружаем данные
                        prbExcel.Visible = true;
                        this.Enabled = false;
                        actp = cboAct.Text;
                        actr = cboActVozvr.Text;
                        sogr = cboRastor.Text;
                        Task.Run(() => getData());
                    }
                    else
                    {
                        MessageBox.Show("Не выбраны документы для печати", "Внимание", MessageBoxButtons.OK);
                    }
                }
            }
        }


        private void getData()
        {
            if (Report.ExcelAvailable)
            {
                string DopDoc_NumAndDate = "";

                string dogovor = "Договор " + (idtd == 1 ? "№ " : (idtd == 2 ? "на рекламу " : "аренды земельного участка №"));
                //договор
                if (chbDogovor.Checked)
                {
                    CheckFileAndPrint(dogovor + number, "dogovor_arenda");
                }         
                // расторжение
                if (chbRastor.Checked)
                {
                    cboRastor.Invoke((MethodInvoker)delegate
                    {
                        DopDoc_NumAndDate = cboRastor.Text;
                    });
                    CheckFileAndPrint("Соглашение о расторжении " + DopDoc_NumAndDate + " договора № " + number, "sogl_rastor");
                }

                //возврат
                if (chbActVozvr.Checked)
                {
                    cboActVozvr.Invoke((MethodInvoker)delegate
                    {
                        DopDoc_NumAndDate = cboActVozvr.Text;
                    });
                    CheckFileAndPrint("Акт приёма-передачи возврат " + DopDoc_NumAndDate + " к дог № " + number, "priem_peredacha_vozvrat");
                }
                
                //прием-передача
                if (chbAct.Checked)
                {
                    cboAct.Invoke((MethodInvoker)delegate
                    {
                        DopDoc_NumAndDate = cboAct.Text;
                    });
                    CheckFileAndPrint("Акт приёма-передачи" + DopDoc_NumAndDate + " к дог № " + number, "priem_peredacha");
                }
                #region ??
                //коммуникации
                if (chbActKomm.Checked == true)
                {
                    cmbActKomm.Invoke((MethodInvoker)delegate
                    {
                        DopDoc_NumAndDate = cmbActKomm.Text;
                    });
                    CheckFileAndPrint("Акт приёма-передачи коммуникаций " + DopDoc_NumAndDate + " к дог № " + number, "act_komm");
                }
                // доп соглашение - не реализовано
                if (chbDopSoglasheniye.Checked)
                {
                    cboDopSoglasheniye.Invoke((MethodInvoker)delegate
                    {
                        DopDoc_NumAndDate = cboDopSoglasheniye.Text;
                    });
                    CheckFileAndPrint("Дополнительное соглашение " + DopDoc_NumAndDate + " к дог № " + number, "ds");
                }
                #endregion
                prbExcel.Invoke((MethodInvoker)delegate
               {
                   prbExcel.Visible = false;
                   this.Enabled = true;
               });
            }
            else
            {
                MessageBox.Show("Печать невозможна. Обратитесь в ОЭЭС для установки Excel.", "Ошибка");
            }
        }

        private bool CheckFileAndPrint(string FileName, string DocType)
        {
            //если файла в папке с договором нет         
            if (!File.Exists(Dir + FileName + ".doc"))
            {
                return Print(DocType, FileName);
            }
            else
            {
                DialogResult d = MessageBox.Show("Файл с названием " + FileName + " уже \nсуществует. Заменить существующий файл \nновым?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (d == DialogResult.Yes)
                {
                    //перезапись файла
                    return Print(DocType, FileName);
                }
                else
                {
                    //открываем существующий файл                 
                    Process.Start(Dir + FileName + ".doc");
                    return true;
                }
            }
        }



        DataTable conf;
        bool Rekl = false;
        string DateAddDoc = "";

        private void CheckBoxEnabled()
        {
            chbAct.Enabled = cboAct.Enabled = false;
            chbRastor.Enabled = cboRastor.Enabled = false;
            chbActVozvr.Enabled = cboActVozvr.Enabled = false;
            chbDopSoglasheniye.Enabled = cboDopSoglasheniye.Enabled = false;
            chbActKomm.Enabled = cmbActKomm.Enabled = false;
         //   chbActReklama.Enabled = cmbActReklama.Enabled = false;

            DataTable dt = new DataTable();
            Task<DataTable> task = _proc.GetAdditionDocs(id);
            task.Wait();
            dt = task.Result;
          
            if (dt != null)
            {
                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    /*if (dt.DefaultView[i]["Rus_Name"].ToString() == "Договор на рекламу")
                    {
                        cmbDogovorReklama.Visible = true;
                        FillCBO(1, cmbDogovorReklama);
                    }*/
                    if (dt.DefaultView[i]["Rus_Name"].ToString() == chbDopSoglasheniye.Text.Trim())
                    {
                        chbDopSoglasheniye.Enabled = true;
                        cboDopSoglasheniye.Enabled = true;
                        FillCBO(2, cboDopSoglasheniye);
                    }
                    if (dt.DefaultView[i]["Rus_Name"].ToString() == chbRastor.Text.Trim())
                    {
                        chbRastor.Enabled = true;
                        cboRastor.Enabled = true;
                        FillCBO(3, cboRastor);
                    }
                    if (dt.DefaultView[i]["Rus_Name"].ToString() == chbActKomm.Text.Trim())
                    {
                        chbActKomm.Enabled = true;
                        cmbActKomm.Enabled = true;
                        FillCBO(4, cmbActKomm);
                    }
                   /* if (dt.DefaultView[i]["Rus_Name"].ToString() == chbActReklama.Text.Trim())
                    {
                        chbActReklama.Enabled = true;
                        cmbActReklama.Enabled = true;
                        FillCBO(5, cmbActReklama);
                    }*/
                    if (dt.DefaultView[i]["Rus_Name"].ToString() == chbActVozvr.Text.Trim())
                    {
                        chbActVozvr.Enabled = true;
                        cboActVozvr.Enabled = true;
                        FillCBO(6, cboActVozvr);
                    }
                    if (dt.DefaultView[i]["Rus_Name"].ToString() == chbAct.Text.Trim())
                    {
                        chbAct.Enabled = true;
                        cboAct.Enabled = true;
                        FillCBO(2, cboAct);
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FillCBO(int type, ComboBox cbo)
        {

            Task<DataTable> task = _proc.GetDopDocuments(id, type);
            task.Wait();
            DataTable dt = task.Result;

            cbo.DataSource = dt;
            cbo.DisplayMember = "text";
            cbo.ValueMember = "id";

            cbo.SelectedIndex = 0;
        }

        private void GetSettings()
        {
            Task<DataTable> task = _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "", "");
            task.Wait();
            conf = task.Result;
            if (conf != null)
            {
                for (int i = 0; conf.Rows.Count > i; i++)
                {
                    if (conf.DefaultView[i]["id_value"].ToString() == "PotD")
                    {
                        Path = conf.DefaultView[i]["value"].ToString();
                    }
                }
            }
        }

        private bool Print(string template, string FileName)
        {

            try
            {
                #region "act_vozvr_rekl"
                if (template == "priem_peredacha_vozvrat")
                {
                    int id_vozvr = 0;

                    cboActVozvr.Invoke((MethodInvoker)delegate
                    {
                        id_vozvr = int.Parse(cboActVozvr.SelectedValue.ToString());
                    });
                    Task<DataTable> task = _proc.getPrintData(id, id_vozvr);
                    task.Wait();

                    DataTable dtPrintResult = task.Result;

                    /*task = _proc.GetPrintDataActEquipment(id);
                    task.Wait();
                    DataTable dtEquipment = task.Result;
                    */
                    if (dtPrintResult != null)
                    {
                        if (dtPrintResult.Rows.Count != 0)
                        {
                            print_template(dtPrintResult, FileName, template);
                        }
                    }
                }
                #endregion
                
                #region "sogl_rastor"
                if (template == "sogl_rastor")
                {
                    int id_rastorg = 0;

                    cboRastor.Invoke((MethodInvoker)delegate
                    {
                        id_rastorg = int.Parse(cboRastor.SelectedValue.ToString());
                    });

                    Task<DataTable> task = _proc.getPrintData(id, id_rastorg);
                    task.Wait();
                    DataTable dtPrintResult = task.Result;

                    if (dtPrintResult != null)
                    {
                        if (dtPrintResult.Rows.Count != 0)
                        {
                            print_template(dtPrintResult, FileName, template);
                        }
                    }
                }
                #endregion

                #region "dogovor_arenda"
                if (template == "dogovor_arenda")
                {
                    Task<DataTable> task;
                    DataTable dtPrintResult;                 
                    task = _proc.getPrintData(id,0);
                    task.Wait();
                    dtPrintResult = task.Result;
                    if (dtPrintResult != null)
                    {
                        if (dtPrintResult.Rows.Count != 0)
                        {
                            print_template(dtPrintResult, FileName, template);
                        }
                    }                                                    
                }
                #endregion
                #region "ds" доп соглашение
                if (template == "ds")
                {
                    int id_dopsogl = 0;

                    cboDopSoglasheniye.Invoke((MethodInvoker)delegate
                    {
                        id_dopsogl = int.Parse(cboDopSoglasheniye.SelectedValue.ToString());
                    });
                    Task<DataTable> task = _proc.GetPrintDataDopSogl(id, id_dopsogl);
                    task.Wait();
                    DataTable dtPrintResult = task.Result;

                    if (dtPrintResult != null)
                    {
                        if (dtPrintResult.Rows.Count != 0)
                        {
                            print_template(dtPrintResult, FileName, template);
                        }
                    }
                }
                #endregion
            
                   
                #region "act_priema_vozvrat"
                if (template == "priem_peredacha_vozvrat")
                {
                    int id_vozvr = 0;
                    cboActVozvr.Invoke((MethodInvoker)delegate
                    {
                        id_vozvr = int.Parse(cboActVozvr.SelectedValue.ToString());
                    });
                    Task<DataTable> task = _proc.getPrintData(id, id_vozvr);
                    task.Wait();
                    DataTable dtPrintResult = task.Result;              
                    /*task = _proc.GetDevicesForPrint(id);
                    task.Wait();
                    DataTable dtDevices = task.Result;

                    task =_proc.GetEquipmentForPrint(id);
                    task.Wait();
                    DataTable dtEquipment = task.Result;
                    */
                    if (dtPrintResult != null)
                    {
                        if (dtPrintResult.Rows.Count != 0)
                        {
                            print_template(dtPrintResult, FileName, template);
                          //  print_act_priema_vozvrat(dtPrintResult, dtDevices, dtEquipment, FileName);
                        }
                    }
                }
                #endregion

                #region "act_priema_peredachi"
                if (template == "priem_peredacha")
                {
                    int id_priema = 0;

                    cboAct.Invoke((MethodInvoker)delegate
                    {
                        id_priema = int.Parse(cboAct.SelectedValue.ToString());
                    });

                    Task<DataTable> task = _proc.getPrintData(id, id_priema);
                    task.Wait();
                    DataTable dtPrintResult = task.Result;

                 /*   task = _proc.GetDevicesForPrint(id);
                    task.Wait();
                    DataTable dtDevices = task.Result;

                    task = _proc.GetEquipmentForPrint(id);
                    task.Wait();
                    DataTable dtEquipment = task.Result;
                    */
                    if (dtPrintResult != null)
                    {
                        if (dtPrintResult.Rows.Count != 0)
                        {
                            print_template(dtPrintResult, FileName, template);
                         //   print_act_priema_peredachi(dtPrintResult, dtDevices, dtEquipment, FileName);
                        }
                    }
                }
                #endregion            

                #region "act_komm"
                if (template == "act_komm")
                {
                    int id_act_komm = 0;

                    cmbActKomm.Invoke((MethodInvoker)delegate
                    {
                        id_act_komm = int.Parse(cmbActKomm.SelectedValue.ToString());
                    });
                    Task<DataTable> task = _proc.GetPrintDataActKomm(id, id_act_komm);
                    task.Wait();

                    DataTable dtPrintResult = task.Result;

                    task = _proc.GetDevicesForPrint(id);
                    task.Wait();
                    DataTable dtDevices = task.Result;

                    if (dtPrintResult != null)
                    {
                        if (dtPrintResult.Rows.Count != 0)
                        {
                           // print_act_komm(dtPrintResult, dtDevices, FileName);
                        }
                    }
                }
                #endregion

                return true;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ошибка в процессе выгрузки данных: " + exc.Message);
                return false;
            }
        }

        private void print_template(DataTable dtPrintResult, string FileName, string template)
        {
            DataRow row = dtPrintResult.Rows[0];
            ReportArenda rep = new ReportArenda(Temp, Application.StartupPath, Dir, FileName, template, idtd);
            rep.Report(row);
        }

        private string dateDoc = "", numDoc = "", nameArend = "", position = "", dateStartDoc = "", dateEndDoc = "", idArend = "";
        //private int idArend;

        /// <summary>
        /// Пихаем данные для логирования, как написано также и добавляем :)
        /// </summary>
        /// <param name="dateDoc"></param>
        /// <param name="numDoc"></param>
        /// <param name="nameArend"></param>
        /// <param name="position"></param>
        /// <param name="dateStartDoc"></param>
        /// <param name="dateEndDoc"></param>
        /// <param name="idArend"></param>
        public void setData(string dateDoc, string numDoc, string nameArend, string position, string dateStartDoc, string dateEndDoc, string idArend)
        {
            this.dateDoc = dateDoc;
            this.numDoc = numDoc;
            this.nameArend = nameArend;
            this.position = position;
            this.dateStartDoc = dateStartDoc;
            this.dateEndDoc = dateEndDoc;
            this.idArend = idArend;
        }
    }
}
