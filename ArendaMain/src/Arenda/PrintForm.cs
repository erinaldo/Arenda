using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using System.Threading;
using Nwuram.Framework.ToExcel;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using System.Diagnostics;
using Nwuram.Framework.Logging;

namespace Arenda
{
    public partial class PrintForm : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        Procedures bgwProc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        
        int id, idtd;
        string Path = "", number = "", Dir= "", Temp = "";
        string actp, actr, sogr;
        DataTable conf;
        bool Rekl = false;
        string DateAddDoc = "";
        //DataTable print;

        public PrintForm(int _id, string _number, int _idtd)
        {
            InitializeComponent();
            id = _id;
            number = _number;
            idtd = _idtd;
        }

        private void PrintForm_Load(object sender, EventArgs e)
        {
            CheckBoxEnabled();
            DataTable dt = _proc.GetLD(id);

            DateAddDoc = "от ";
            if (decimal.Parse(dt.Rows[0]["Reklama"].ToString()) > 0)
            {
                Rekl = true;
            }
        }

        private void CheckBoxEnabled()
        {
            chbAct.Enabled = cboAct.Enabled = false;                
            chbRastor.Enabled = cboRastor.Enabled = false;
            chbActVozvr.Enabled = cboActVozvr.Enabled =  false;
            chbDopSoglasheniye.Enabled = cboDopSoglasheniye.Enabled = false;
            chbActKomm.Enabled = cmbActKomm.Enabled = false;
            chbActReklama.Enabled = cmbActReklama.Enabled = false;

            DataTable dt = new DataTable();
            dt = _proc.GetAdditionDocs(id);

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
                    if (dt.DefaultView[i]["Rus_Name"].ToString() == chbActReklama.Text.Trim())
                    {
                        chbActReklama.Enabled = true;
                        cmbActReklama.Enabled = true;
                        FillCBO(5, cmbActReklama);
                    }
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
                        FillCBO(7, cboAct);
                    }
                }
            }
        }

        private void FillCBO(int type, ComboBox cbo)
        {
            DataTable dt = new DataTable();
            dt = _proc.GetDopDocuments(id, type);

            cbo.DataSource = dt;
            cbo.DisplayMember = "text";
            cbo.ValueMember = "id";

            cbo.SelectedIndex = 0;
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetSettings()
        {
            conf = new DataTable();
            conf = _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "", "");

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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt = _proc.GetLD(id);

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
                if (number=="")
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
                                            string cmbName = (cCmb as ComboBox).Name.Replace("cbo", "").Replace("cmb","");
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
                        bgwToExcel.RunWorkerAsync();
                    }
                    else
                    {
                        MessageBox.Show("Не выбраны документы для печати", "Внимание", MessageBoxButtons.OK);
                    }                    
                }                
            }            
        }

        private void bgwToExcel_DoWork(object sender, DoWorkEventArgs e)
        {
            bgwToExcel.WorkerSupportsCancellation = false;
            var args = (object[])e.Argument;

            if (Report.ExcelAvailable)
            {
                string DopDoc_NumAndDate = "";

                if (chbDogovor.Checked && idtd == 1)
                {
                    CheckFileAndPrint("Договор № " + number, "dogovor_arenda");
                }
                if (chbDogovor.Checked && idtd == 2)
                {
                  /*cmbDogovorReklama.Invoke((MethodInvoker)delegate
                  {
                    DopDoc_NumAndDate = cmbDogovorReklama.Text;
                  });*/
                  CheckFileAndPrint("Договор на рекламу " + number, "dogovor_reklama");
                }
                if (chbDogovor.Checked && idtd == 3)
                {
                  CheckFileAndPrint("Договор аренды земельного участка №" + number, "dogovor_zem");
                }
                if (chbRastor.Checked == true && idtd != 3)
                {
                    cboRastor.Invoke((MethodInvoker)delegate
                    {
                        DopDoc_NumAndDate = cboRastor.Text;
                    });
                    CheckFileAndPrint("Соглашение о расторжении " + DopDoc_NumAndDate + " договора № " + number, "sogl_rastor");
                }
                if (chbRastor.Checked == true && idtd == 3)
                {
                  cboRastor.Invoke((MethodInvoker)delegate
                  {
                    DopDoc_NumAndDate = cboRastor.Text;
                  });
                  CheckFileAndPrint("Соглашение о расторжении " + DopDoc_NumAndDate + " договора № " + number, "sogl_rastor_zem");
                }
                if (chbDopSoglasheniye.Checked == true)
                {
                    cboDopSoglasheniye.Invoke((MethodInvoker)delegate
                    {
                        DopDoc_NumAndDate = cboDopSoglasheniye.Text;
                    });
                    CheckFileAndPrint("Дополнительное соглашение " + DopDoc_NumAndDate + " к дог № " + number, "ds");
                }
                if (chbActReklama.Checked == true)
                {
                    cmbActReklama.Invoke((MethodInvoker)delegate
                    {
                        DopDoc_NumAndDate = cmbActReklama.Text;
                    });
                    CheckFileAndPrint("Акт приёма-передачи реклама " + DopDoc_NumAndDate + " к дог № " + number, "act_priema_reklama");
                }
                if (chbActVozvr.Checked == true && idtd != 3)
                {
                    cboActVozvr.Invoke((MethodInvoker)delegate
                    {
                        DopDoc_NumAndDate = cboActVozvr.Text;
                    });
                    CheckFileAndPrint("Акт приёма-передачи возврат " + DopDoc_NumAndDate + " к дог № " + number, "act_priema_vozvrat");
                }
                if (chbActVozvr.Checked == true && idtd == 3)
                {
                  cboActVozvr.Invoke((MethodInvoker)delegate
                  {
                    DopDoc_NumAndDate = cboActVozvr.Text;
                  });
                  CheckFileAndPrint("Акт приёма-передачи возврат " + DopDoc_NumAndDate + " к дог № " + number, "act_vozvr_zem");
                }
                if (chbAct.Checked == true && idtd != 3)
                {
                    cboAct.Invoke((MethodInvoker)delegate
                    {
                        DopDoc_NumAndDate = cboAct.Text;
                    });
                    CheckFileAndPrint("Акт приёма-передачи" + DopDoc_NumAndDate + " к дог № " + number, "act_priema_peredachi");
                }
                if (chbAct.Checked == true && idtd == 3)
                {
                  cboAct.Invoke((MethodInvoker)delegate
                  {
                    DopDoc_NumAndDate = cboAct.Text;
                  });
                  CheckFileAndPrint("Акт приёма-передачи" + DopDoc_NumAndDate + " к дог № " + number, "act_priema_zem");
                }
                if (chbActKomm.Checked == true)
                {
                    cmbActKomm.Invoke((MethodInvoker)delegate
                    {
                        DopDoc_NumAndDate = cmbActKomm.Text;
                    });
                    CheckFileAndPrint("Акт приёма-передачи коммуникаций " + DopDoc_NumAndDate + " к дог № " + number, "act_komm");
                }
            }
            else
            {
                MessageBox.Show("Печать невозможна. Обратитесь в ОЭЭС для установки Excel.","Ошибка");
            }

        }

        private void bgwToExcel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Выгрузка документов \n       завершена.", "Сообщение", MessageBoxButtons.OK);
            prbExcel.Visible = false;
            this.Enabled = true;
            this.Close();
        }

        private void CrystalActReportShow(string type)
        {
            if (type == "act_priema")
            {
                int id_actpriema = 0;

                cboAct.Invoke((MethodInvoker)delegate
                {
                    id_actpriema = int.Parse(cboAct.SelectedValue.ToString());
                });

                DataTable dtPrintResult = new DataTable();
                DataTable dtEquipment = new DataTable();

                dtPrintResult = bgwProc.GetPrintDataAct(id, id_actpriema);
                dtEquipment = bgwProc.GetPrintDataActEquipment(id);

                frmReport frmActRep = new frmReport(dtPrintResult, dtEquipment, number, 1);
                frmActRep.WindowState = FormWindowState.Maximized;
                frmActRep.ShowDialog();
            }

            if (type == "act_vozvr")
            {
                int id_vozvr = 0;

                cboActVozvr.Invoke((MethodInvoker)delegate
                {
                    id_vozvr = int.Parse(cboActVozvr.SelectedValue.ToString());
                });

                DataTable dtPrintResult = new DataTable();
                dtPrintResult = bgwProc.GetPrintDataActVozvr(id, id_vozvr);

                DataTable dtEquipment = new DataTable();
                dtEquipment = bgwProc.GetPrintDataActEquipment(id);

                frmReport frmActVozvRep = new frmReport(dtPrintResult, dtEquipment, number, 2);
                frmActVozvRep.WindowState = FormWindowState.Maximized;
                frmActVozvRep.ShowDialog();
            }
        }

        private bool CheckFileAndPrint(string FileName, string DocType)
        {            
            //если файла в папке с договором нет
            
            //if ((!File.Exists(Dir + FileName + ".xls")) && (!File.Exists(Dir + FileName + ".xlsx")))
            //if (!File.Exists(Dir + FileName + ".xls"))
            if (!File.Exists(Dir + FileName + ".doc"))
            {
                return Print(DocType, FileName);
            }
            else
            {
                DialogResult d = MessageBox.Show("Файл с названием " + FileName + " уже \nсуществует. Заменить существующий файл \nновым?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (d == DialogResult.Yes)
                {
                    //удаление существующего файла
                    //if (File.Exists(Dir + FileName + ".xls"))
                    //{
                    //    File.Delete(Dir + FileName + ".xls");
                    //}
                  
                    //перезапись файла
                    return Print(DocType, FileName);
                }
                else
                {
                    //открываем существующий файл
                    //Report temp = new Report();
                    //temp.OpenFile(Dir + FileName);
                    Process.Start(Dir + FileName + ".doc");
                    return true;
                }
            }
        }

        private bool Print(string template, string FileName)
        {

            try
            {
                #region "act_vozvr_rekl"
                if (template == "act_vozvr_rekl")
                {
                    int id_vozvr = 0;

                    cboActVozvr.Invoke((MethodInvoker)delegate
                    {
                        id_vozvr = int.Parse(cboActVozvr.SelectedValue.ToString());
                    });

                    DataTable dtPrintResult = new DataTable();
                    dtPrintResult = bgwProc.GetPrintDataActVozvr(id, id_vozvr);

                    DataTable dtEquipment = new DataTable();
                    dtEquipment = bgwProc.GetPrintDataActEquipment(id);

                    if (dtPrintResult != null)
                    {
                        if (dtPrintResult.Rows.Count != 0)
                        {
                            print_act_vozvr_rekl(dtPrintResult, FileName);
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

                    DataTable dtPrintResult = new DataTable();
                    dtPrintResult = bgwProc.GetPrintDataSoglRastor(id, id_rastorg);

                    if (dtPrintResult != null)
                    {
                        if (dtPrintResult.Rows.Count != 0)
                        {
                            print_sogl_rastor(dtPrintResult, FileName);
                        }
                    }
                }
                #endregion

                #region "dogovor"
                if (template == "dogovor")
                {
                    DataTable dtPrintResult = new DataTable();

                    //договор рекламы
                    if (Rekl)
                    {
                        dtPrintResult = bgwProc.GetPrintDataRekl(id);

                        if (dtPrintResult != null)
                        {
                            if (dtPrintResult.Rows.Count != 0)
                            {
                                print_dogovor_rekl(dtPrintResult, FileName);
                            }
                        }
                    }
                    //договор аренды
                    else
                    {
                        dtPrintResult = bgwProc.GetPrintData(id);

                        if (dtPrintResult != null)
                        {
                            if (dtPrintResult.Rows.Count != 0)
                            {
                                print_dogovor(dtPrintResult, FileName);
                            }
                        }
                    }
                }
                #endregion

                #region "ds"
                if (template == "ds")
                {
                    int id_dopsogl = 0;

                    cboDopSoglasheniye.Invoke((MethodInvoker)delegate
                    {
                        id_dopsogl = int.Parse(cboDopSoglasheniye.SelectedValue.ToString());
                    });

                    DataTable dtPrintResult = new DataTable();
                    dtPrintResult = bgwProc.GetPrintDataDopSogl(id, id_dopsogl);

                    if (dtPrintResult != null)
                    {
                        if (dtPrintResult.Rows.Count != 0)
                        {
                            print_dop_sogl(dtPrintResult, FileName);
                        }
                    }
                }
                #endregion

                #region "act_priema_reklama"
                if (template == "act_priema_reklama")
                {
                    int id_rekl = 0;

                    cmbActReklama.Invoke((MethodInvoker)delegate
                    {
                        id_rekl = int.Parse(cmbActReklama.SelectedValue.ToString());
                    });

                    DataTable dtPrintResult = new DataTable();
                    dtPrintResult = bgwProc.GetPrintDataActReklama(id, id_rekl);

                    DataTable dtDevices = new DataTable();
                    dtDevices = bgwProc.GetDevicesForPrint(id);

                    if (dtPrintResult != null)
                    {
                        if (dtPrintResult.Rows.Count != 0)
                        {
                            print_act_priema_reklama(dtPrintResult, dtDevices, FileName);
                        }
                    }
                }
                #endregion

                #region "act_priema_vozvrat"
                if (template == "act_priema_vozvrat")
                {
                    int id_vozvr = 0;

                    cboActVozvr.Invoke((MethodInvoker)delegate
                    {
                        id_vozvr = int.Parse(cboActVozvr.SelectedValue.ToString());
                    });

                    DataTable dtPrintResult = new DataTable();
                    dtPrintResult = bgwProc.GetPrintDataActVozvrat(id, id_vozvr);

                    DataTable dtDevices = new DataTable();
                    dtDevices = bgwProc.GetDevicesForPrint(id);

                    DataTable dtEquipment = new DataTable();
                    dtEquipment = bgwProc.GetEquipmentForPrint(id);

                    if (dtPrintResult != null)
                    {
                        if (dtPrintResult.Rows.Count != 0)
                        {
                            print_act_priema_vozvrat(dtPrintResult, dtDevices, dtEquipment, FileName);
                        }
                    }
                }
                #endregion

                #region "act_priema_peredachi"
                if (template == "act_priema_peredachi")
                {
                    int id_priema = 0;

                    cboAct.Invoke((MethodInvoker)delegate
                    {
                        id_priema = int.Parse(cboAct.SelectedValue.ToString());
                    });

                    DataTable dtPrintResult = new DataTable();
                    dtPrintResult = bgwProc.GetPrintDataActPriemPeredacha(id, id_priema);

                    DataTable dtDevices = new DataTable();
                    dtDevices = bgwProc.GetDevicesForPrint(id);

                    DataTable dtEquipment = new DataTable();
                    dtEquipment = bgwProc.GetEquipmentForPrint(id);

                    if (dtPrintResult != null)
                    {
                        if (dtPrintResult.Rows.Count != 0)
                        {
                            print_act_priema_peredachi(dtPrintResult, dtDevices, dtEquipment, FileName);
                        }
                    }
                }
                #endregion

                #region "dogovor_reklama"
                if (template == "dogovor_reklama")
                {
                    DataTable dtPrintResult = new DataTable();
                    dtPrintResult = bgwProc.GetPrintDataDogovorReklama(id);

                    if (dtPrintResult != null)
                    {
                        if (dtPrintResult.Rows.Count != 0)
                        {
                            print_dogovor_reklama(dtPrintResult, FileName);
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

                    DataTable dtPrintResult = new DataTable();
                    dtPrintResult = bgwProc.GetPrintDataActKomm(id, id_act_komm);

                    DataTable dtDevices = new DataTable();
                    dtDevices = bgwProc.GetDevicesForPrint(id);

                    if (dtPrintResult != null)
                    {
                        if (dtPrintResult.Rows.Count != 0)
                        {
                            print_act_komm(dtPrintResult, dtDevices, FileName);
                        }
                    }
                }
                #endregion

                #region "dogovor_arenda"
                if (template == "dogovor_arenda")
                {
                    DataTable dtPrintResult = new DataTable();
                    dtPrintResult = bgwProc.GetPrintDataDogovorArenda(id);

                    if (dtPrintResult != null)
                    {
                        if (dtPrintResult.Rows.Count != 0)
                        {
                            print_dogovor_arenda(dtPrintResult, FileName);
                        }
                    }
                }
                #endregion

                #region "dogovor_zem"
                if (template == "dogovor_zem")
                {
                  DataTable dtPrintResult = new DataTable();
                  dtPrintResult = bgwProc.GetPrintDataZem(id);

                  if (dtPrintResult != null && dtPrintResult.Rows.Count > 0)
                  {
                    print_zem(dtPrintResult, FileName);
                  }
                }
                #endregion

                #region "act_priema_zem"
                if (template == "act_priema_zem")
                {
                  DataTable dtPrintResult = new DataTable();
                  dtPrintResult = bgwProc.GetPrintDataZem(id);

                  if (dtPrintResult != null && dtPrintResult.Rows.Count > 0)
                  {
                    print_act_priem_zem(dtPrintResult, FileName);
                  }
                }
                #endregion

                #region "act_vozvr_zem"
                if (template == "act_vozvr_zem")
                {
                  DataTable dtPrintResult = new DataTable();
                  dtPrintResult = bgwProc.GetPrintDataZem(id);

                  if (dtPrintResult != null && dtPrintResult.Rows.Count > 0)
                  {
                    print_act_vozvr_zem(dtPrintResult, FileName);
                  }
                }
                #endregion

                #region "sogl_rastor_zem"
                if (template == "sogl_rastor_zem")
                {
                  DataTable dtPrintResult = new DataTable();
                  dtPrintResult = bgwProc.GetPrintDataZem(id);

                  if (dtPrintResult != null && dtPrintResult.Rows.Count > 0)
                  {
                    print_sogl_rastor_zem(dtPrintResult, FileName);
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

        private void chbAct_Click(object sender, EventArgs e)
        {
            if (idtd == 1)
            {
                bool isAppz = false;

                isAppz = _proc.CanPrintAPPZAct(id);

                if (!isAppz)
                {
                    MessageBox.Show("У секции с признаком наличия \nоборудования АППЗ отсуствует \nсвязь с оборудованием. \nПечать акта невозможна.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    chbAct.Checked = false;
                }            
            }            
        }

        private void chbActVozvr_Click(object sender, EventArgs e)
        {
            if (idtd == 1)
            {
                bool isAppz = false;

                isAppz = _proc.CanPrintAPPZAct(id);

                if (!isAppz)
                {
                    MessageBox.Show("У секции с признаком наличия \nоборудования АППЗ отсуствует \nсвязь с оборудованием. \nПечать акта невозможна.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    chbActVozvr.Checked = false;
                } 
            }            
        }



        private void print_dogovor(DataTable dtPrintResult, string FileName)
        {
          string fileName = "";
          if (Temp == "")
            fileName = Application.StartupPath + "\\Templates\\doc\\dogovor.doc";
          else
            fileName = Temp + "\\dogovor.doc";

          if (!File.Exists(fileName))
            fileName = Application.StartupPath + "\\Templates\\doc\\dogovor.doc";

            Nwuram.Framework.ToWord.HandmadeReport report = new Nwuram.Framework.ToWord.HandmadeReport(fileName);
            //report.CurrentTable = report.GetTable(1);
            //report.CutCurrentTable();

            string number = dtPrintResult.DefaultView[0]["num"].ToString();
          
            report.Replace("{num}", number);
            //если пришло пусто, то в Excel передается " - " иначе значение из процедуры
            report.Replace("{adress}", (dtPrintResult.DefaultView[0]["adress"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["adress"].ToString()));
            report.Replace("{date_con}", (dtPrintResult.DefaultView[0]["date_con"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_con"].ToString()));
            report.Replace("{arendodatel_str}", (dtPrintResult.DefaultView[0]["arendodatel_str"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["arendodatel_str"].ToString()));
            report.Replace("{post_arendodatel}", (dtPrintResult.DefaultView[0]["post_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["post_arendodatel"].ToString()));
            report.Replace("{FIO_arendodatel_Par}", (dtPrintResult.DefaultView[0]["FIO_arendodatel_Par"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["FIO_arendodatel_Par"].ToString()));

            string Landlord_basement = ",";

            if (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length != 0)
                Landlord_basement = ", действующего на основании "
                    + (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_basement"].ToString());

            if (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length != 0)
                Landlord_basement += (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length == 0 ? "" : " № " + dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString());
            if (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length != 0)
                Landlord_basement += " от " + (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString());


            if (Landlord_basement != ",")
                Landlord_basement += ",";
            report.Replace("{osnovanie_arendodatel}", Landlord_basement);


            report.Replace("{arendator_str}", (dtPrintResult.DefaultView[0]["arendator_str"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["arendator_str"].ToString()));
            report.Replace("{post_arendator}", (dtPrintResult.DefaultView[0]["post_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["post_arendator"].ToString()));
            report.Replace("{FIO_arendator_Par}", (dtPrintResult.DefaultView[0]["FIO_arendator_Par"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["FIO_arendator_Par"].ToString()));


            string Tenant_basement = ",";

            if (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length != 0)
                Tenant_basement = ", действующего на основании "
                    + (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_basement"].ToString());

            if (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length != 0)
                Tenant_basement += (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length == 0 ? "" : " № " + dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString());
            if (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length != 0)
                Tenant_basement += " от " + (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString());


            if (Tenant_basement != ",")
                Tenant_basement += ",";
            report.Replace("{osnovanie_arendator}", Tenant_basement);

            report.Replace("{buildings}", (dtPrintResult.DefaultView[0]["buildings"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["buildings"].ToString()));
            report.Replace("{floors}", (dtPrintResult.DefaultView[0]["floors"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["floors"].ToString()));
            report.Replace("{section}", (dtPrintResult.DefaultView[0]["section"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["section"].ToString()));

            string S_arend = " - ";
            if (dtPrintResult.DefaultView[0]["S_arend"].ToString().Trim().Length != 0)
                S_arend = (dtPrintResult.DefaultView[0]["S_arend"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["S_arend"].ToString()) + " кв.м.";
            if (dtPrintResult.DefaultView[0]["S_trading_hall"].ToString().Trim().Length != 0)
            {
                if (dtPrintResult.DefaultView[0]["S_trading_hall"].ToString() != "0.00")
                {
                    S_arend += " (в т.ч. торговый зал " + dtPrintResult.DefaultView[0]["S_trading_hall"].ToString() + " кв.м.)";
                }
            }
            report.Replace("{S_arend}", S_arend);


            report.Replace("{type_of_premises}", (dtPrintResult.DefaultView[0]["type_of_premises"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["type_of_premises"].ToString()));
            report.Replace("{NDS}", (dtPrintResult.DefaultView[0]["NDS"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["NDS"].ToString()));

            report.Replace("{Summa}", (dtPrintResult.DefaultView[0]["Summa"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Summa"].ToString()));
            report.Replace("{Summa_str}", (dtPrintResult.DefaultView[0]["Summa_str"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Summa_str"].ToString()));

            report.Replace("{date_start}", (dtPrintResult.DefaultView[0]["date_start"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_start"].ToString()));
            report.Replace("{date_end}", (dtPrintResult.DefaultView[0]["date_end"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_end"].ToString()));


            report.Replace("{arendodatel}", (dtPrintResult.DefaultView[0]["arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["arendodatel"].ToString()));
            report.Replace("{adress_arendodatel}", (dtPrintResult.DefaultView[0]["adress_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["adress_arendodatel"].ToString()));
            report.Replace("{inn_arendodatel}", (dtPrintResult.DefaultView[0]["inn_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["inn_arendodatel"].ToString()));
            report.Replace("{PaymentAccount_arendodatel}", (dtPrintResult.DefaultView[0]["PaymentAccount_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["PaymentAccount_arendodatel"].ToString()));
            report.Replace("{bank_arendodatel}", (dtPrintResult.DefaultView[0]["bank_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["bank_arendodatel"].ToString()));
            report.Replace("{bik_arendodatel}", (dtPrintResult.DefaultView[0]["bik_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["bik_arendodatel"].ToString()));
            report.Replace("{CorrespondentAccount_arendodatel}", (dtPrintResult.DefaultView[0]["CorrespondentAccount_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["CorrespondentAccount_arendodatel"].ToString()));
            report.Replace("{kpp_arendodatel}", (dtPrintResult.DefaultView[0]["kpp_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["kpp_arendodatel"].ToString()));
            report.Replace("{FIO_arendodatel}", (dtPrintResult.DefaultView[0]["FIO_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["FIO_arendodatel"].ToString()));
            report.Replace("{arendator}", (dtPrintResult.DefaultView[0]["arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["arendator"].ToString()));
            report.Replace("{adress_arendator}", (dtPrintResult.DefaultView[0]["adress_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["adress_arendator"].ToString()));
            report.Replace("{inn_arendator}", (dtPrintResult.DefaultView[0]["inn_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["inn_arendator"].ToString()));
            report.Replace("{PaymentAccount_arendator}", (dtPrintResult.DefaultView[0]["PaymentAccount_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["PaymentAccount_arendator"].ToString()));
            report.Replace("{bank_arendator}", (dtPrintResult.DefaultView[0]["bank_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["bank_arendator"].ToString()));
            report.Replace("{bik_arendator}", (dtPrintResult.DefaultView[0]["bik_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["bik_arendator"].ToString()));
            report.Replace("{CorrespondentAccount_arendator}", (dtPrintResult.DefaultView[0]["CorrespondentAccount_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["CorrespondentAccount_arendator"].ToString()));
            report.Replace("{kpp_arendator}", (dtPrintResult.DefaultView[0]["kpp_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["kpp_arendator"].ToString()));
            report.Replace("{FIO_arendator}", (dtPrintResult.DefaultView[0]["FIO_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["FIO_arendator"].ToString()));

            report.Replace("{dop_str}", (dtPrintResult.DefaultView[0]["dop_str"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["dop_str"].ToString()));


            //report.Replace("{key_word}", "АДВДАВДАЛДВАВАВДЛВДЛАВ");

            //report.SaveAs(getFileNameForReport(fileName.ToString()));
            report.SaveAs(Dir + FileName + ".doc");
            //report.Show();
            report.Close(false);
            Process.Start(Dir + FileName + ".doc");
        }

        private void print_dogovor_rekl(DataTable dtPrintResult, string FileName)
        {
          string fileName = "";
          if (Temp == "")
            fileName = Application.StartupPath + "\\Templates\\doc\\dogovor_rekl.doc";
          else
            fileName = Temp + "\\dogovor_rekl.doc";

          if (!File.Exists(fileName))
            fileName = Application.StartupPath + "\\Templates\\doc\\dogovor_rekl.doc";

            Nwuram.Framework.ToWord.HandmadeReport report = new Nwuram.Framework.ToWord.HandmadeReport(fileName);
            //report.CurrentTable = report.GetTable(1);
            //report.CutCurrentTable();

            string number = dtPrintResult.DefaultView[0]["num"].ToString();
            
            report.Replace("{num}", number);
            report.Replace("{date_con}", (dtPrintResult.DefaultView[0]["date_con"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_con"].ToString()));

            report.Replace("{arendodatel_str}", (dtPrintResult.DefaultView[0]["arendodatel_str"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["arendodatel_str"].ToString()));
            report.Replace("{post_arendodatel}", (dtPrintResult.DefaultView[0]["post_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["post_arendodatel"].ToString()));
            report.Replace("{FIO_arendodatel_Par}", (dtPrintResult.DefaultView[0]["FIO_arendodatel_Par"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["FIO_arendodatel_Par"].ToString()));


            string Landlord_basement = ",";

            if (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length != 0)
                Landlord_basement = ", действующего на основании "
                    + (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_basement"].ToString());

            if (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length != 0)
                Landlord_basement += (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length == 0 ? "" : " № " + dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString());
            if (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length != 0)
                Landlord_basement += " от " + (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString());


            if (Landlord_basement != ",")
                Landlord_basement += ",";
            report.Replace("{osnovanie_arendodatel}", Landlord_basement);


            report.Replace("{arendator_str}", (dtPrintResult.DefaultView[0]["arendator_str"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["arendator_str"].ToString()));
            report.Replace("{post_arendator}", (dtPrintResult.DefaultView[0]["post_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["post_arendator"].ToString()));
            report.Replace("{FIO_arendator_Par}", (dtPrintResult.DefaultView[0]["FIO_arendator_Par"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["FIO_arendator_Par"].ToString()));


            string Tenant_basement = ",";

            if (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length != 0)
                Tenant_basement = ", действующего на основании "
                    + (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_basement"].ToString());

            if (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length != 0)
                Tenant_basement += (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length == 0 ? "" : " № " + dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString());
            if (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length != 0)
                Tenant_basement += " от " + (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString());


            if (Tenant_basement != ",")
                Tenant_basement += ",";
            report.Replace("{osnovanie_arendator}", Tenant_basement);

            report.Replace("{adress}", (dtPrintResult.DefaultView[0]["adress"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["adress"].ToString()));

            report.Replace("{reklsquare}", (dtPrintResult.DefaultView[0]["reklsquare"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["reklsquare"].ToString()));
            report.Replace("{reklLength}", (dtPrintResult.DefaultView[0]["reklLength"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["reklLength"].ToString()));
            report.Replace("{reklWidth}", (dtPrintResult.DefaultView[0]["reklWidth"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["reklWidth"].ToString()));
            report.Replace("{reklNumber}", (dtPrintResult.DefaultView[0]["reklNumber"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["reklNumber"].ToString()));

            report.Replace("{date_start}", (dtPrintResult.DefaultView[0]["date_start"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_start"].ToString()));
            report.Replace("{date_end}", (dtPrintResult.DefaultView[0]["date_end"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_end"].ToString()));

            report.Replace("{Summa_Rek}", (dtPrintResult.DefaultView[0]["Summa_Rek"].ToString()));
            report.Replace("{Summa_str_Rek}", (dtPrintResult.DefaultView[0]["Summa_str_Rek"].ToString()));

            report.Replace("{paydate}", (dtPrintResult.DefaultView[0]["paydate"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["paydate"].ToString()));


            //!!!!!!!!!!!!!!!!
            //----------------
            //!!!!!!!!!!!!!!!!



            string S_arend = " - ";
            if (dtPrintResult.DefaultView[0]["S_arend"].ToString().Trim().Length != 0)
                S_arend = (dtPrintResult.DefaultView[0]["S_arend"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["S_arend"].ToString()) + " кв.м.";
            if (dtPrintResult.DefaultView[0]["S_trading_hall"].ToString().Trim().Length != 0)
            {
                if (dtPrintResult.DefaultView[0]["S_trading_hall"].ToString() != "0.00")
                {
                    S_arend += " (в т.ч. торговый зал " + dtPrintResult.DefaultView[0]["S_trading_hall"].ToString() + " кв.м.)";
                }
            }

            report.Replace("{arendodatel}", (dtPrintResult.DefaultView[0]["arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["arendodatel"].ToString()));
            report.Replace("{adress_arendodatel}", (dtPrintResult.DefaultView[0]["adress_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["adress_arendodatel"].ToString()));
            report.Replace("{inn_arendodatel}", (dtPrintResult.DefaultView[0]["inn_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["inn_arendodatel"].ToString()));
            report.Replace("{PaymentAccount_arendodatel}", (dtPrintResult.DefaultView[0]["PaymentAccount_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["PaymentAccount_arendodatel"].ToString()));
            report.Replace("{bank_arendodatel}", (dtPrintResult.DefaultView[0]["bank_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["bank_arendodatel"].ToString()));
            report.Replace("{bik_arendodatel}", (dtPrintResult.DefaultView[0]["bik_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["bik_arendodatel"].ToString()));
            report.Replace("{CorrespondentAccount_arendodatel}", (dtPrintResult.DefaultView[0]["CorrespondentAccount_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["CorrespondentAccount_arendodatel"].ToString()));
            report.Replace("{kpp_arendodatel}", (dtPrintResult.DefaultView[0]["kpp_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["kpp_arendodatel"].ToString()));
            report.Replace("{FIO_arendodatel}", (dtPrintResult.DefaultView[0]["FIO_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["FIO_arendodatel"].ToString()));
            report.Replace("{arendator}", (dtPrintResult.DefaultView[0]["arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["arendator"].ToString()));
            report.Replace("{adress_arendator}", (dtPrintResult.DefaultView[0]["adress_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["adress_arendator"].ToString()));
            report.Replace("{inn_arendator}", (dtPrintResult.DefaultView[0]["inn_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["inn_arendator"].ToString()));
            report.Replace("{PaymentAccount_arendator}", (dtPrintResult.DefaultView[0]["PaymentAccount_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["PaymentAccount_arendator"].ToString()));
            report.Replace("{bank_arendator}", (dtPrintResult.DefaultView[0]["bank_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["bank_arendator"].ToString()));
            report.Replace("{bik_arendator}", (dtPrintResult.DefaultView[0]["bik_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["bik_arendator"].ToString()));
            report.Replace("{CorrespondentAccount_arendator}", (dtPrintResult.DefaultView[0]["CorrespondentAccount_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["CorrespondentAccount_arendator"].ToString()));
            report.Replace("{kpp_arendator}", (dtPrintResult.DefaultView[0]["kpp_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["kpp_arendator"].ToString()));
            report.Replace("{FIO_arendator}", (dtPrintResult.DefaultView[0]["FIO_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["FIO_arendator"].ToString()));


            
            //report.SaveAs(getFileNameForReport(fileName.ToString()));
            report.SaveAs(Dir + FileName + ".doc");
            //report.Show();
            report.Close(false);
            Process.Start(Dir + FileName + ".doc");
        }

        private void print_zem(DataTable dtPrintResult, string FileName)
        {
          string fileName = "";
          if (Temp == "")
            fileName = Application.StartupPath + "\\Templates\\doc\\dogovor_zem.doc";
          else
            fileName = Temp + "\\dogovor_zem.doc";

          if (!File.Exists(fileName))
            fileName = Application.StartupPath + "\\Templates\\doc\\dogovor_zem.doc";

          Nwuram.Framework.ToWord.HandmadeReport report =
            new Nwuram.Framework.ToWord.HandmadeReport(fileName);

          string number = dtPrintResult.DefaultView[0]["num"].ToString();

          report.Replace("{num}", number);

          report.Replace("{date_con}",
            dtPrintResult.DefaultView[0]["date_con"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["date_con"].ToString());

          report.Replace("{arendodatel_str}",
            dtPrintResult.DefaultView[0]["arendodatel_str"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["arendodatel_str"].ToString());
          report.Replace("{post_arendodatel}",
            dtPrintResult.DefaultView[0]["post_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["post_arendodatel"].ToString());
          report.Replace("{FIO_arendodatel_Par}",
            dtPrintResult.DefaultView[0]["FIO_arendodatel_Par"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["FIO_arendodatel_Par"].ToString());

          string Landlord_basement = ",";

          if (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length != 0)
            Landlord_basement = ", действующего на основании "
                + (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length == 0 ?
                " - " : dtPrintResult.DefaultView[0]["Landlord_basement"].ToString());

          if (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length != 0)
            Landlord_basement += (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length == 0 ?
              "" : " № " + dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString());
          if (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length != 0)
            Landlord_basement += " от " + (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length == 0 ?
              " - " : dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString());

          if (Landlord_basement != ",")
            Landlord_basement += ",";
          report.Replace("{osnovanie_arendodatel}", Landlord_basement);

          report.Replace("{arendator_str}",
            dtPrintResult.DefaultView[0]["arendator_str"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["arendator_str"].ToString());
          report.Replace("{post_arendator}",
            dtPrintResult.DefaultView[0]["post_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["post_arendator"].ToString());
          report.Replace("{FIO_arendator_Par}",
            dtPrintResult.DefaultView[0]["FIO_arendator_Par"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["FIO_arendator_Par"].ToString());

          string Tenant_basement = ",";

          if (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length != 0)
            Tenant_basement = ", действующего на основании "
                + (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length == 0 ?
                " - " : dtPrintResult.DefaultView[0]["Tenant_basement"].ToString());

          if (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length != 0)
            Tenant_basement += (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length == 0 ?
              "" : " № " + dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString());
          if (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length != 0)
            Tenant_basement += " от " + (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length == 0 ?
              " - " : dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString());

          if (Tenant_basement != ",")
            Tenant_basement += ",";
          report.Replace("{osnovanie_arendator}", Tenant_basement);

          report.Replace("{S_arend}",
            dtPrintResult.DefaultView[0]["S_arend"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["S_arend"].ToString());
          report.Replace("{address}",
            dtPrintResult.DefaultView[0]["address"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["address"].ToString());
          report.Replace("{Summa}",
            dtPrintResult.DefaultView[0]["Summa"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["Summa"].ToString());
          report.Replace("{Summa_str}",
            dtPrintResult.DefaultView[0]["Summa_str"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["Summa_str"].ToString());
          report.Replace("{kad_number}",
            dtPrintResult.DefaultView[0]["KadNum"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["KadNum"].ToString());

          report.Replace("{arendodatel}",
            dtPrintResult.DefaultView[0]["arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["arendodatel"].ToString());
          report.Replace("{address_arendodatel}",
            dtPrintResult.DefaultView[0]["address_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["address_arendodatel"].ToString());
          report.Replace("{inn_arendodatel}",
            dtPrintResult.DefaultView[0]["inn_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["inn_arendodatel"].ToString());
          report.Replace("{payment_account_arendodatel}",
            dtPrintResult.DefaultView[0]["payment_account_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["payment_account_arendodatel"].ToString());
          report.Replace("{bank_arendodatel}",
            dtPrintResult.DefaultView[0]["bank_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["bank_arendodatel"].ToString());
          report.Replace("{bik_arendodatel}",
            dtPrintResult.DefaultView[0]["bik_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["bik_arendodatel"].ToString());
          report.Replace("{correspondent_account_arendodatel}",
            dtPrintResult.DefaultView[0]["correspondent_account_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["correspondent_account_arendodatel"].ToString());
          report.Replace("{kpp_arendodatel}",
            dtPrintResult.DefaultView[0]["kpp_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["kpp_arendodatel"].ToString());
          report.Replace("{ogrn_arendodatel}",
            dtPrintResult.DefaultView[0]["ogrn_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["ogrn_arendodatel"].ToString());
          report.Replace("{FIO_arendodatel}",
            dtPrintResult.DefaultView[0]["FIO_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["FIO_arendodatel"].ToString());

          report.Replace("{arendator}",
            dtPrintResult.DefaultView[0]["arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["arendator"].ToString());
          report.Replace("{address_arendator}",
            dtPrintResult.DefaultView[0]["address_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["address_arendator"].ToString());
          report.Replace("{inn_arendator}",
            dtPrintResult.DefaultView[0]["inn_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["inn_arendator"].ToString());
          report.Replace("{payment_account_arendator}",
            dtPrintResult.DefaultView[0]["payment_account_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["payment_account_arendator"].ToString());
          report.Replace("{bank_arendator}",
            dtPrintResult.DefaultView[0]["bank_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["bank_arendator"].ToString());
          report.Replace("{bik_arendator}",
            dtPrintResult.DefaultView[0]["bik_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["bik_arendator"].ToString());
          report.Replace("{correspondent_account_arendator}",
            dtPrintResult.DefaultView[0]["correspondent_account_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["correspondent_account_arendator"].ToString());
          report.Replace("{kpp_arendator}",
            dtPrintResult.DefaultView[0]["kpp_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["kpp_arendator"].ToString());
          report.Replace("{ogrn_arendator}",
            dtPrintResult.DefaultView[0]["ogrn_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["ogrn_arendator"].ToString());
          report.Replace("{FIO_arendator}",
            dtPrintResult.DefaultView[0]["FIO_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["FIO_arendator"].ToString());

          string Phones_arendodatel = "";
          if (dtPrintResult.DefaultView[0]["work_phone_arendodatel"].ToString().Replace(" ", "").Length > 0)
            Phones_arendodatel += "раб. тел.: " + dtPrintResult.DefaultView[0]["work_phone_arendodatel"].ToString().Replace(" ", "");
          if (dtPrintResult.DefaultView[0]["home_phone_arendodatel"].ToString().Replace(" ", "").Length > 0)
          {
            if (Phones_arendodatel != "")
              Phones_arendodatel += "\r\n";
            Phones_arendodatel += "дом. тел.: " + dtPrintResult.DefaultView[0]["home_phone_arendodatel"].ToString().Replace(" ", "");
          }
          if (dtPrintResult.DefaultView[0]["mobile_phone_arendodatel"].ToString().Replace(" ", "").Length > 0)
          {
            if (Phones_arendodatel != "")
              Phones_arendodatel += "\r\n";
            Phones_arendodatel += "моб. тел.: " + dtPrintResult.DefaultView[0]["mobile_phone_arendodatel"].ToString().Replace(" ", "");
          }
          if (dtPrintResult.DefaultView[0]["email_arendodatel"].ToString().Replace(" ", "").Length > 0)
          {
            if (Phones_arendodatel != "")
              Phones_arendodatel += "\r\n";
            Phones_arendodatel += "mail: " + dtPrintResult.DefaultView[0]["email_arendodatel"].ToString().Replace(" ", "");
          }
          if (Phones_arendodatel == "")
            Phones_arendodatel += "тел./mail:";
          report.Replace("{phones_arendodatel}", Phones_arendodatel);

          string Phones_arendator = "";
          if (dtPrintResult.DefaultView[0]["work_phone_arendator"].ToString().Replace(" ", "").Length > 0)
            Phones_arendator += "раб. тел.: " + dtPrintResult.DefaultView[0]["work_phone_arendator"].ToString().Replace(" ", "");
          if (dtPrintResult.DefaultView[0]["home_phone_arendator"].ToString().Replace(" ", "").Length > 0)
          {
            if (Phones_arendator != "")
              Phones_arendator += "\r\n";
            Phones_arendator += "дом. тел.: " + dtPrintResult.DefaultView[0]["home_phone_arendator"].ToString().Replace(" ", "");
          }
          if (dtPrintResult.DefaultView[0]["mobile_phone_arendator"].ToString().Replace(" ", "").Length > 0)
          {
            if (Phones_arendator != "")
              Phones_arendator += "\r\n";
            Phones_arendator += "моб. тел.: " + dtPrintResult.DefaultView[0]["mobile_phone_arendator"].ToString().Replace(" ", "");
          }
          if (dtPrintResult.DefaultView[0]["email_arendator"].ToString().Replace(" ", "").Length > 0)
          {
            if (Phones_arendator != "")
              Phones_arendator += "\r\n";
            Phones_arendator += "mail: " + dtPrintResult.DefaultView[0]["email_arendator"].ToString().Replace(" ", "");
          }
          if (Phones_arendator == "")
            Phones_arendator += "тел./mail:";
          report.Replace("{phones_arendator}", Phones_arendator);

          report.SaveAs(Dir + FileName + ".doc");
          //report.Show();
          report.Close(false);
          Process.Start(Dir + FileName + ".doc");
        }

      private void print_act_priem_zem(DataTable dtPrintResult, string FileName)
      {
        string fileName = "";
        if (Temp == "")
          fileName = Application.StartupPath + "\\Templates\\doc\\act_priema_zem.doc";
        else
          fileName = Temp + "\\act_priema_zem.doc";

        if (!File.Exists(fileName))
          fileName = Application.StartupPath + "\\Templates\\doc\\act_priema_zem.doc";

        Nwuram.Framework.ToWord.HandmadeReport report =
          new Nwuram.Framework.ToWord.HandmadeReport(fileName);

        string number = dtPrintResult.DefaultView[0]["num"].ToString();

        report.Replace("{num}", number);
        report.Replace("{date_this}", actp);

        report.Replace("{date_con}",
          dtPrintResult.DefaultView[0]["date_con"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["date_con"].ToString());

        report.Replace("{arendodatel_str}",
          dtPrintResult.DefaultView[0]["arendodatel_str"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["arendodatel_str"].ToString());
        report.Replace("{post_arendodatel}",
          dtPrintResult.DefaultView[0]["post_arendodatel"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["post_arendodatel"].ToString());
        report.Replace("{FIO_arendodatel_Par}",
          dtPrintResult.DefaultView[0]["FIO_arendodatel_Par"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["FIO_arendodatel_Par"].ToString());

        string Landlord_basement = ",";

        if (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length != 0)
          Landlord_basement = ", действующего на основании "
            + (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["Landlord_basement"].ToString());

        if (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length != 0)
          Landlord_basement += (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length == 0 ?
            "" : " № " + dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString());
        if (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length != 0)
          Landlord_basement += " от " + (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString());

        if (Landlord_basement != ",")
          Landlord_basement += ",";
        report.Replace("{osnovanie_arendodatel}", Landlord_basement);

        report.Replace("{arendator_str}",
          dtPrintResult.DefaultView[0]["arendator_str"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["arendator_str"].ToString());
        report.Replace("{post_arendator}",
          dtPrintResult.DefaultView[0]["post_arendator"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["post_arendator"].ToString());
        report.Replace("{FIO_arendator_Par}",
          dtPrintResult.DefaultView[0]["FIO_arendator_Par"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["FIO_arendator_Par"].ToString());

        string Tenant_basement = ",";

        if (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length != 0)
          Tenant_basement = ", действующего на основании "
            + (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["Tenant_basement"].ToString());

        if (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length != 0)
          Tenant_basement += (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length == 0 ?
            "" : " № " + dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString());
        if (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length != 0)
          Tenant_basement += " от " + (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString());

        if (Tenant_basement != ",")
          Tenant_basement += ",";
        report.Replace("{osnovanie_arendator}", Tenant_basement);

        report.Replace("{S_arend}",
          dtPrintResult.DefaultView[0]["S_arend"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["S_arend"].ToString());
        report.Replace("{address}",
          dtPrintResult.DefaultView[0]["address"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["address"].ToString());
        report.Replace("{Summa}",
          dtPrintResult.DefaultView[0]["Summa"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["Summa"].ToString());
        report.Replace("{Summa_str}",
          dtPrintResult.DefaultView[0]["Summa_str"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["Summa_str"].ToString());
        report.Replace("{kad_number}",
          dtPrintResult.DefaultView[0]["KadNum"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["KadNum"].ToString());

        report.Replace("{arendodatel}",
          dtPrintResult.DefaultView[0]["arendodatel"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["arendodatel"].ToString());
        report.Replace("{address_arendodatel}",
          dtPrintResult.DefaultView[0]["address_arendodatel"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["address_arendodatel"].ToString());
        report.Replace("{inn_arendodatel}",
          dtPrintResult.DefaultView[0]["inn_arendodatel"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["inn_arendodatel"].ToString());
        report.Replace("{payment_account_arendodatel}",
          dtPrintResult.DefaultView[0]["payment_account_arendodatel"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["payment_account_arendodatel"].ToString());
        report.Replace("{bank_arendodatel}",
          dtPrintResult.DefaultView[0]["bank_arendodatel"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["bank_arendodatel"].ToString());
        report.Replace("{bik_arendodatel}",
          dtPrintResult.DefaultView[0]["bik_arendodatel"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["bik_arendodatel"].ToString());
        report.Replace("{correspondent_account_arendodatel}",
          dtPrintResult.DefaultView[0]["correspondent_account_arendodatel"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["correspondent_account_arendodatel"].ToString());
        report.Replace("{kpp_arendodatel}",
          dtPrintResult.DefaultView[0]["kpp_arendodatel"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["kpp_arendodatel"].ToString());
        report.Replace("{ogrn_arendodatel}",
          dtPrintResult.DefaultView[0]["ogrn_arendodatel"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["ogrn_arendodatel"].ToString());
        report.Replace("{FIO_arendodatel}",
          dtPrintResult.DefaultView[0]["FIO_arendodatel"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["FIO_arendodatel"].ToString());

        report.Replace("{arendator}",
          dtPrintResult.DefaultView[0]["arendator"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["arendator"].ToString());
        report.Replace("{address_arendator}",
          dtPrintResult.DefaultView[0]["address_arendator"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["address_arendator"].ToString());
        report.Replace("{inn_arendator}",
          dtPrintResult.DefaultView[0]["inn_arendator"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["inn_arendator"].ToString());
        report.Replace("{payment_account_arendator}",
          dtPrintResult.DefaultView[0]["payment_account_arendator"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["payment_account_arendator"].ToString());
        report.Replace("{bank_arendator}",
          dtPrintResult.DefaultView[0]["bank_arendator"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["bank_arendator"].ToString());
        report.Replace("{bik_arendator}",
          dtPrintResult.DefaultView[0]["bik_arendator"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["bik_arendator"].ToString());
        report.Replace("{correspondent_account_arendator}",
          dtPrintResult.DefaultView[0]["correspondent_account_arendator"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["correspondent_account_arendator"].ToString());
        report.Replace("{kpp_arendator}",
          dtPrintResult.DefaultView[0]["kpp_arendator"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["kpp_arendator"].ToString());
        report.Replace("{ogrn_arendator}",
          dtPrintResult.DefaultView[0]["ogrn_arendator"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["ogrn_arendator"].ToString());
        report.Replace("{FIO_arendator}",
          dtPrintResult.DefaultView[0]["FIO_arendator"].ToString().Trim().Length == 0 ?
          " - " : dtPrintResult.DefaultView[0]["FIO_arendator"].ToString());

        string Phones_arendodatel = "";
        if (dtPrintResult.DefaultView[0]["work_phone_arendodatel"].ToString().Replace(" ", "").Length > 0)
          Phones_arendodatel += "раб. тел.: " + dtPrintResult.DefaultView[0]["work_phone_arendodatel"].ToString().Replace(" ", "");
        if (dtPrintResult.DefaultView[0]["home_phone_arendodatel"].ToString().Replace(" ", "").Length > 0)
        {
          if (Phones_arendodatel != "")
            Phones_arendodatel += "\r\n";
          Phones_arendodatel += "дом. тел.: " + dtPrintResult.DefaultView[0]["home_phone_arendodatel"].ToString().Replace(" ", "");
        }
        if (dtPrintResult.DefaultView[0]["mobile_phone_arendodatel"].ToString().Replace(" ", "").Length > 0)
        {
          if (Phones_arendodatel != "")
            Phones_arendodatel += "\r\n";
          Phones_arendodatel += "моб. тел.: " + dtPrintResult.DefaultView[0]["mobile_phone_arendodatel"].ToString().Replace(" ", "");
        }
        if (dtPrintResult.DefaultView[0]["email_arendodatel"].ToString().Replace(" ", "").Length > 0)
        {
          if (Phones_arendodatel != "")
            Phones_arendodatel += "\r\n";
          Phones_arendodatel += "mail: " + dtPrintResult.DefaultView[0]["email_arendodatel"].ToString().Replace(" ", "");
        }
        if (Phones_arendodatel == "")
          Phones_arendodatel += "тел./mail:";
        report.Replace("{phones_arendodatel}", Phones_arendodatel);

        string Phones_arendator = "";
        if (dtPrintResult.DefaultView[0]["work_phone_arendator"].ToString().Replace(" ", "").Length > 0)
          Phones_arendator += "раб. тел.: " + dtPrintResult.DefaultView[0]["work_phone_arendator"].ToString().Replace(" ", "");
        if (dtPrintResult.DefaultView[0]["home_phone_arendator"].ToString().Replace(" ", "").Length > 0)
        {
          if (Phones_arendator != "")
            Phones_arendator += "\r\n";
          Phones_arendator += "дом. тел.: " + dtPrintResult.DefaultView[0]["home_phone_arendator"].ToString().Replace(" ", "");
        }
        if (dtPrintResult.DefaultView[0]["mobile_phone_arendator"].ToString().Replace(" ", "").Length > 0)
        {
          if (Phones_arendator != "")
            Phones_arendator += "\r\n";
          Phones_arendator += "моб. тел.: " + dtPrintResult.DefaultView[0]["mobile_phone_arendator"].ToString().Replace(" ", "");
        }
        if (dtPrintResult.DefaultView[0]["email_arendator"].ToString().Replace(" ", "").Length > 0)
        {
          if (Phones_arendator != "")
            Phones_arendator += "\r\n";
          Phones_arendator += "mail: " + dtPrintResult.DefaultView[0]["email_arendator"].ToString().Replace(" ", "");
        }
        if (Phones_arendator == "")
          Phones_arendator += "тел./mail:";
        report.Replace("{phones_arendator}", Phones_arendator);

        report.SaveAs(Dir + FileName + ".doc");
        //report.Show();
        report.Close(false);
        Process.Start(Dir + FileName + ".doc");
      }

      private void print_act_vozvr_zem(DataTable dtPrintResult, string FileName)
      {
        string fileName = "";
        if (Temp == "")
          fileName = Application.StartupPath + "\\Templates\\doc\\act_vozvr_zem.doc";
        else
          fileName = Temp + "\\act_vozvr_zem.doc";

        if (!File.Exists(fileName))
          fileName = Application.StartupPath + "\\Templates\\doc\\act_vozvr_zem.doc";

          Nwuram.Framework.ToWord.HandmadeReport report =
            new Nwuram.Framework.ToWord.HandmadeReport(fileName);

          string number = dtPrintResult.DefaultView[0]["num"].ToString();

          report.Replace("{num}", number);
          report.Replace("{date_this}", actr);

          report.Replace("{date_con}",
            dtPrintResult.DefaultView[0]["date_con"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["date_con"].ToString());

          report.Replace("{arendodatel_str}",
            dtPrintResult.DefaultView[0]["arendodatel_str"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["arendodatel_str"].ToString());
          report.Replace("{post_arendodatel}",
            dtPrintResult.DefaultView[0]["post_arendodatel"].ToString().Trim().Length == 0 ?
            "" : dtPrintResult.DefaultView[0]["post_arendodatel"].ToString());
          report.Replace("{FIO_arendodatel_Par}",
            dtPrintResult.DefaultView[0]["FIO_arendodatel_Par"].ToString().Trim().Length == 0 ?
            "" : dtPrintResult.DefaultView[0]["FIO_arendodatel_Par"].ToString());

          string Landlord_basement = ",";

          if (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length != 0)
            Landlord_basement = ", действующего на основании "
                + (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length == 0 ?
                " - " : dtPrintResult.DefaultView[0]["Landlord_basement"].ToString());

          if (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length != 0)
            Landlord_basement += (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length == 0 ?
              "" : " № " + dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString());
          if (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length != 0)
            Landlord_basement += " от " + (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length == 0 ?
              " - " : dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString());

          if (Landlord_basement != ",")
            Landlord_basement += ",";
          report.Replace("{osnovanie_arendodatel}", Landlord_basement);

          report.Replace("{arendator_str}",
            dtPrintResult.DefaultView[0]["arendator_str"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["arendator_str"].ToString());
          report.Replace("{post_arendator}",
            dtPrintResult.DefaultView[0]["post_arendator"].ToString().Trim().Length == 0 ?
            "" : dtPrintResult.DefaultView[0]["post_arendator"].ToString());
          report.Replace("{FIO_arendator_Par}",
            dtPrintResult.DefaultView[0]["FIO_arendator_Par"].ToString().Trim().Length == 0 ?
            "" : dtPrintResult.DefaultView[0]["FIO_arendator_Par"].ToString());

          string Tenant_basement = ",";

          if (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length != 0)
            Tenant_basement = ", действующего на основании "
                + (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length == 0 ?
                " - " : dtPrintResult.DefaultView[0]["Tenant_basement"].ToString());

          if (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length != 0)
            Tenant_basement += (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length == 0 ?
              "" : " № " + dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString());
          if (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length != 0)
            Tenant_basement += " от " + (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length == 0 ?
              " - " : dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString());

          if (Tenant_basement != ",")
            Tenant_basement += ",";
          report.Replace("{osnovanie_arendator}", Tenant_basement);

          report.Replace("{S_arend}",
            dtPrintResult.DefaultView[0]["S_arend"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["S_arend"].ToString());
          report.Replace("{address}",
            dtPrintResult.DefaultView[0]["address"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["address"].ToString());
          report.Replace("{Summa}",
            dtPrintResult.DefaultView[0]["Summa"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["Summa"].ToString());
          report.Replace("{Summa_str}",
            dtPrintResult.DefaultView[0]["Summa_str"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["Summa_str"].ToString());
          report.Replace("{kad_number}",
            dtPrintResult.DefaultView[0]["KadNum"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["KadNum"].ToString());

          report.Replace("{arendodatel}",
            dtPrintResult.DefaultView[0]["arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["arendodatel"].ToString());
          report.Replace("{address_arendodatel}",
            dtPrintResult.DefaultView[0]["address_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["address_arendodatel"].ToString());
          report.Replace("{inn_arendodatel}",
            dtPrintResult.DefaultView[0]["inn_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["inn_arendodatel"].ToString());
          report.Replace("{payment_account_arendodatel}",
            dtPrintResult.DefaultView[0]["payment_account_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["payment_account_arendodatel"].ToString());
          report.Replace("{bank_arendodatel}",
            dtPrintResult.DefaultView[0]["bank_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["bank_arendodatel"].ToString());
          report.Replace("{bik_arendodatel}",
            dtPrintResult.DefaultView[0]["bik_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["bik_arendodatel"].ToString());
          report.Replace("{correspondent_account_arendodatel}",
            dtPrintResult.DefaultView[0]["correspondent_account_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["correspondent_account_arendodatel"].ToString());
          report.Replace("{kpp_arendodatel}",
            dtPrintResult.DefaultView[0]["kpp_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["kpp_arendodatel"].ToString());
          report.Replace("{ogrn_arendodatel}",
            dtPrintResult.DefaultView[0]["ogrn_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["ogrn_arendodatel"].ToString());
          report.Replace("{FIO_arendodatel}",
            dtPrintResult.DefaultView[0]["FIO_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["FIO_arendodatel"].ToString());

          report.Replace("{arendator}",
            dtPrintResult.DefaultView[0]["arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["arendator"].ToString());
          report.Replace("{address_arendator}",
            dtPrintResult.DefaultView[0]["address_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["address_arendator"].ToString());
          report.Replace("{inn_arendator}",
            dtPrintResult.DefaultView[0]["inn_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["inn_arendator"].ToString());
          report.Replace("{payment_account_arendator}",
            dtPrintResult.DefaultView[0]["payment_account_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["payment_account_arendator"].ToString());
          report.Replace("{bank_arendator}",
            dtPrintResult.DefaultView[0]["bank_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["bank_arendator"].ToString());
          report.Replace("{bik_arendator}",
            dtPrintResult.DefaultView[0]["bik_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["bik_arendator"].ToString());
          report.Replace("{correspondent_account_arendator}",
            dtPrintResult.DefaultView[0]["correspondent_account_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["correspondent_account_arendator"].ToString());
          report.Replace("{kpp_arendator}",
            dtPrintResult.DefaultView[0]["kpp_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["kpp_arendator"].ToString());
          report.Replace("{ogrn_arendator}",
            dtPrintResult.DefaultView[0]["ogrn_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["ogrn_arendator"].ToString());
          report.Replace("{FIO_arendator}",
            dtPrintResult.DefaultView[0]["FIO_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["FIO_arendator"].ToString());

          string Phones_arendodatel = "";
          if (dtPrintResult.DefaultView[0]["work_phone_arendodatel"].ToString().Replace(" ", "").Length > 0)
            Phones_arendodatel += "раб. тел.: " + dtPrintResult.DefaultView[0]["work_phone_arendodatel"].ToString().Replace(" ", "");
          if (dtPrintResult.DefaultView[0]["home_phone_arendodatel"].ToString().Replace(" ", "").Length > 0)
          {
            if (Phones_arendodatel != "")
              Phones_arendodatel += "\r\n";
            Phones_arendodatel += "дом. тел.: " + dtPrintResult.DefaultView[0]["home_phone_arendodatel"].ToString().Replace(" ", "");
          }
          if (dtPrintResult.DefaultView[0]["mobile_phone_arendodatel"].ToString().Replace(" ", "").Length > 0)
          {
            if (Phones_arendodatel != "")
              Phones_arendodatel += "\r\n";
            Phones_arendodatel += "моб. тел.: " + dtPrintResult.DefaultView[0]["mobile_phone_arendodatel"].ToString().Replace(" ", "");
          }
          if (dtPrintResult.DefaultView[0]["email_arendodatel"].ToString().Replace(" ", "").Length > 0)
          {
            if (Phones_arendodatel != "")
              Phones_arendodatel += "\r\n";
            Phones_arendodatel += "mail: " + dtPrintResult.DefaultView[0]["email_arendodatel"].ToString().Replace(" ", "");
          }
          if (Phones_arendodatel == "")
            Phones_arendodatel += "тел./mail:";
          report.Replace("{phones_arendodatel}", Phones_arendodatel);

          string Phones_arendator = "";
          if (dtPrintResult.DefaultView[0]["work_phone_arendator"].ToString().Replace(" ", "").Length > 0)
            Phones_arendator += "раб. тел.: " + dtPrintResult.DefaultView[0]["work_phone_arendator"].ToString().Replace(" ", "");
          if (dtPrintResult.DefaultView[0]["home_phone_arendator"].ToString().Replace(" ", "").Length > 0)
          {
            if (Phones_arendator != "")
              Phones_arendator += "\r\n";
            Phones_arendator += "дом. тел.: " + dtPrintResult.DefaultView[0]["home_phone_arendator"].ToString().Replace(" ", "");
          }
          if (dtPrintResult.DefaultView[0]["mobile_phone_arendator"].ToString().Replace(" ", "").Length > 0)
          {
            if (Phones_arendator != "")
              Phones_arendator += "\r\n";
            Phones_arendator += "моб. тел.: " + dtPrintResult.DefaultView[0]["mobile_phone_arendator"].ToString().Replace(" ", "");
          }
          if (dtPrintResult.DefaultView[0]["email_arendator"].ToString().Replace(" ", "").Length > 0)
          {
            if (Phones_arendator != "")
              Phones_arendator += "\r\n";
            Phones_arendator += "mail: " + dtPrintResult.DefaultView[0]["email_arendator"].ToString().Replace(" ", "");
          }
          if (Phones_arendator == "")
            Phones_arendator += "тел./mail:";
          report.Replace("{phones_arendator}", Phones_arendator);

          report.SaveAs(Dir + FileName + ".doc");
          //report.Show();
          report.Close(false);
          Process.Start(Dir + FileName + ".doc");
      }

      private void print_sogl_rastor_zem(DataTable dtPrintResult, string FileName)
      {
        string fileName = "";
        if (Temp == "")
          fileName = Application.StartupPath + "\\Templates\\doc\\sogl_rastor_zem.doc";
        else
          fileName = Temp + "\\sogl_rastor_zem.doc";

        if (!File.Exists(fileName))
          fileName = Application.StartupPath + "\\Templates\\doc\\sogl_rastor_zem.doc";

          Nwuram.Framework.ToWord.HandmadeReport report =
            new Nwuram.Framework.ToWord.HandmadeReport(fileName);

          string number = dtPrintResult.DefaultView[0]["num"].ToString();

          report.Replace("{num}", number);
          report.Replace("{date_this}", sogr);

          report.Replace("{date_con}",
            dtPrintResult.DefaultView[0]["date_con"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["date_con"].ToString());

          report.Replace("{arendodatel_str}",
            dtPrintResult.DefaultView[0]["arendodatel_str"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["arendodatel_str"].ToString());
          report.Replace("{post_arendodatel}",
            dtPrintResult.DefaultView[0]["post_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["post_arendodatel"].ToString());
          report.Replace("{FIO_arendodatel_Par}",
            dtPrintResult.DefaultView[0]["FIO_arendodatel_Par"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["FIO_arendodatel_Par"].ToString());

          string Landlord_basement = ",";

          if (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length != 0)
            Landlord_basement = ", действующего на основании "
                + (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length == 0 ?
                " - " : dtPrintResult.DefaultView[0]["Landlord_basement"].ToString());

          if (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length != 0)
            Landlord_basement += (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length == 0 ?
              "" : " № " + dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString());
          if (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length != 0)
            Landlord_basement += " от " + (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length == 0 ?
              " - " : dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString());

          if (Landlord_basement != ",")
            Landlord_basement += ",";
          report.Replace("{osnovanie_arendodatel}", Landlord_basement);

          report.Replace("{arendator_str}",
            dtPrintResult.DefaultView[0]["arendator_str"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["arendator_str"].ToString());
          report.Replace("{post_arendator}",
            dtPrintResult.DefaultView[0]["post_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["post_arendator"].ToString());
          report.Replace("{FIO_arendator_Par}",
            dtPrintResult.DefaultView[0]["FIO_arendator_Par"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["FIO_arendator_Par"].ToString());

          string Tenant_basement = ",";

          if (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length != 0)
            Tenant_basement = ", действующего на основании "
                + (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length == 0 ?
                " - " : dtPrintResult.DefaultView[0]["Tenant_basement"].ToString());

          if (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length != 0)
            Tenant_basement += (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length == 0 ?
              "" : " № " + dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString());
          if (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length != 0)
            Tenant_basement += " от " + (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length == 0 ?
              " - " : dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString());

          if (Tenant_basement != ",")
            Tenant_basement += ",";
          report.Replace("{osnovanie_arendator}", Tenant_basement);

          report.Replace("{S_arend}",
            dtPrintResult.DefaultView[0]["S_arend"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["S_arend"].ToString());
          report.Replace("{address}",
            dtPrintResult.DefaultView[0]["address"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["address"].ToString());
          report.Replace("{Summa}",
            dtPrintResult.DefaultView[0]["Summa"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["Summa"].ToString());
          report.Replace("{Summa_str}",
            dtPrintResult.DefaultView[0]["Summa_str"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["Summa_str"].ToString());
          report.Replace("{kad_number}",
            dtPrintResult.DefaultView[0]["KadNum"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["KadNum"].ToString());

          report.Replace("{arendodatel}",
            dtPrintResult.DefaultView[0]["arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["arendodatel"].ToString());
          report.Replace("{address_arendodatel}",
            dtPrintResult.DefaultView[0]["address_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["address_arendodatel"].ToString());
          report.Replace("{inn_arendodatel}",
            dtPrintResult.DefaultView[0]["inn_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["inn_arendodatel"].ToString());
          report.Replace("{payment_account_arendodatel}",
            dtPrintResult.DefaultView[0]["payment_account_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["payment_account_arendodatel"].ToString());
          report.Replace("{bank_arendodatel}",
            dtPrintResult.DefaultView[0]["bank_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["bank_arendodatel"].ToString());
          report.Replace("{bik_arendodatel}",
            dtPrintResult.DefaultView[0]["bik_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["bik_arendodatel"].ToString());
          report.Replace("{correspondent_account_arendodatel}",
            dtPrintResult.DefaultView[0]["correspondent_account_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["correspondent_account_arendodatel"].ToString());
          report.Replace("{kpp_arendodatel}",
            dtPrintResult.DefaultView[0]["kpp_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["kpp_arendodatel"].ToString());
          report.Replace("{ogrn_arendodatel}",
            dtPrintResult.DefaultView[0]["ogrn_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["ogrn_arendodatel"].ToString());
          report.Replace("{FIO_arendodatel}",
            dtPrintResult.DefaultView[0]["FIO_arendodatel"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["FIO_arendodatel"].ToString());

          report.Replace("{arendator}",
            dtPrintResult.DefaultView[0]["arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["arendator"].ToString());
          report.Replace("{address_arendator}",
            dtPrintResult.DefaultView[0]["address_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["address_arendator"].ToString());
          report.Replace("{inn_arendator}",
            dtPrintResult.DefaultView[0]["inn_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["inn_arendator"].ToString());
          report.Replace("{payment_account_arendator}",
            dtPrintResult.DefaultView[0]["payment_account_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["payment_account_arendator"].ToString());
          report.Replace("{bank_arendator}",
            dtPrintResult.DefaultView[0]["bank_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["bank_arendator"].ToString());
          report.Replace("{bik_arendator}",
            dtPrintResult.DefaultView[0]["bik_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["bik_arendator"].ToString());
          report.Replace("{correspondent_account_arendator}",
            dtPrintResult.DefaultView[0]["correspondent_account_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["correspondent_account_arendator"].ToString());
          report.Replace("{kpp_arendator}",
            dtPrintResult.DefaultView[0]["kpp_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["kpp_arendator"].ToString());
          report.Replace("{ogrn_arendator}",
            dtPrintResult.DefaultView[0]["ogrn_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["ogrn_arendator"].ToString());
          report.Replace("{FIO_arendator}",
            dtPrintResult.DefaultView[0]["FIO_arendator"].ToString().Trim().Length == 0 ?
            " - " : dtPrintResult.DefaultView[0]["FIO_arendator"].ToString());

          string Phones_arendodatel = "";
          if (dtPrintResult.DefaultView[0]["work_phone_arendodatel"].ToString().Replace(" ", "").Length > 0)
            Phones_arendodatel += "раб. тел.: " + dtPrintResult.DefaultView[0]["work_phone_arendodatel"].ToString().Replace(" ", "");
          if (dtPrintResult.DefaultView[0]["home_phone_arendodatel"].ToString().Replace(" ", "").Length > 0)
          {
            if (Phones_arendodatel != "")
              Phones_arendodatel += "\r\n";
            Phones_arendodatel += "дом. тел.: " + dtPrintResult.DefaultView[0]["home_phone_arendodatel"].ToString().Replace(" ", "");
          }
          if (dtPrintResult.DefaultView[0]["mobile_phone_arendodatel"].ToString().Replace(" ", "").Length > 0)
          {
            if (Phones_arendodatel != "")
              Phones_arendodatel += "\r\n";
            Phones_arendodatel += "моб. тел.: " + dtPrintResult.DefaultView[0]["mobile_phone_arendodatel"].ToString().Replace(" ", "");
          }
          if (dtPrintResult.DefaultView[0]["email_arendodatel"].ToString().Replace(" ", "").Length > 0)
          {
            if (Phones_arendodatel != "")
              Phones_arendodatel += "\r\n";
            Phones_arendodatel += "mail: " + dtPrintResult.DefaultView[0]["email_arendodatel"].ToString().Replace(" ", "");
          }
          if (Phones_arendodatel == "")
            Phones_arendodatel += "тел./mail:";
          report.Replace("{phones_arendodatel}", Phones_arendodatel);

          string Phones_arendator = "";
          if (dtPrintResult.DefaultView[0]["work_phone_arendator"].ToString().Replace(" ", "").Length > 0)
            Phones_arendator += "раб. тел.: " + dtPrintResult.DefaultView[0]["work_phone_arendator"].ToString().Replace(" ", "");
          if (dtPrintResult.DefaultView[0]["home_phone_arendator"].ToString().Replace(" ", "").Length > 0)
          {
            if (Phones_arendator != "")
              Phones_arendator += "\r\n";
            Phones_arendator += "дом. тел.: " + dtPrintResult.DefaultView[0]["home_phone_arendator"].ToString().Replace(" ", "");
          }
          if (dtPrintResult.DefaultView[0]["mobile_phone_arendator"].ToString().Replace(" ", "").Length > 0)
          {
            if (Phones_arendator != "")
              Phones_arendator += "\r\n";
            Phones_arendator += "моб. тел.: " + dtPrintResult.DefaultView[0]["mobile_phone_arendator"].ToString().Replace(" ", "");
          }
          if (dtPrintResult.DefaultView[0]["email_arendator"].ToString().Replace(" ", "").Length > 0)
          {
            if (Phones_arendator != "")
              Phones_arendator += "\r\n";
            Phones_arendator += "mail: " + dtPrintResult.DefaultView[0]["email_arendator"].ToString().Replace(" ", "");
          }
          if (Phones_arendator == "")
            Phones_arendator += "тел./mail:";
          report.Replace("{phones_arendator}", Phones_arendator);

          report.SaveAs(Dir + FileName + ".doc");
          //report.Show();
          report.Close(false);
          Process.Start(Dir + FileName + ".doc");
      }

      // Закоменчено, так как не используется
      /*
        private void print_garant(DataTable dtPrintResult, string FileName)
      {
        string fileName = "";
        if (Temp == "")
          fileName = Application.StartupPath + "\\Templates\\doc\\garant.doc";
        else
          fileName = Temp + "\\garant.doc";

        if (!File.Exists(fileName))
          fileName = Application.StartupPath + "\\Templates\\doc\\garant.doc";

            Nwuram.Framework.ToWord.HandmadeReport report = new Nwuram.Framework.ToWord.HandmadeReport(fileName);
            //report.CurrentTable = report.GetTable(1);
            //report.CutCurrentTable();

            string number = dtPrintResult.DefaultView[0]["num"].ToString();

            report.Replace("{num}", number);

            report.Replace("{Tenant_type}", (dtPrintResult.DefaultView[0]["Tenant_type"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_type"].ToString()));
            report.Replace("{Tenant_name}", (dtPrintResult.DefaultView[0]["Tenant_name"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_name"].ToString()));
            report.Replace("{Landlord_post_Dative}", (dtPrintResult.DefaultView[0]["Landlord_post_Dative"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_post_Dative"].ToString()));
            report.Replace("{Landlord_type}", (dtPrintResult.DefaultView[0]["Landlord_type"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_type"].ToString()));
            report.Replace("{Landlord_name}", (dtPrintResult.DefaultView[0]["Landlord_name"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_name"].ToString()));
            report.Replace("{Landlord_FIO}", (dtPrintResult.DefaultView[0]["Landlord_FIO"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_FIO"].ToString()));
            report.Replace("{Tenant_INN}", (dtPrintResult.DefaultView[0]["Tenant_INN"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_INN"].ToString()));
            report.Replace("{Tenant_ks}", (dtPrintResult.DefaultView[0]["Tenant_ks"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_ks"].ToString()));
            report.Replace("{Tenant_bank}", (dtPrintResult.DefaultView[0]["Tenant_bank"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_bank"].ToString()));
            report.Replace("{Tenant_bank_BIC}", (dtPrintResult.DefaultView[0]["Tenant_bank_BIC"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_bank_BIC"].ToString()));
            report.Replace("{Tenant_FIO2}", (dtPrintResult.DefaultView[0]["Tenant_FIO2"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_FIO2"].ToString()));
          
           // report.SaveAs(getFileNameForReport(fileName.ToString()));
            report.SaveAs(Dir + FileName + ".doc");
            report.Show();
        }
      */

        private void print_act_vozvr_rekl(DataTable dtPrintResult, string FileName)
        {
          string fileName = "";
          if (Temp == "")
            fileName = Application.StartupPath + "\\Templates\\doc\\act_vozvr_rekl.doc";
          else
            fileName = Temp + "\\act_vozvr_rekl.doc";

          if (!File.Exists(fileName))
            fileName = Application.StartupPath + "\\Templates\\doc\\act_vozvr_rekl.doc";

            Nwuram.Framework.ToWord.HandmadeReport report = new Nwuram.Framework.ToWord.HandmadeReport(fileName);
           
            string number = dtPrintResult.DefaultView[0]["num"].ToString();

           // report.Replace("{num}", number);

            report.Replace("{object_address}", (dtPrintResult.DefaultView[0]["object_address"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["object_address"].ToString()));
            report.Replace("{date_doc}", (dtPrintResult.DefaultView[0]["date_doc"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_doc"].ToString()));

            report.Replace("{Landlord_type_full}", (dtPrintResult.DefaultView[0]["Landlord_type_full"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_type_full"].ToString()));
            report.Replace("{Tenant_type_full}", (dtPrintResult.DefaultView[0]["Tenant_type_full"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_type_full"].ToString()));

            report.Replace("{Landlord_post}", (dtPrintResult.DefaultView[0]["Landlord_post"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_post"].ToString()));
            report.Replace("{Landlord_FIO}", (dtPrintResult.DefaultView[0]["Landlord_FIO"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_FIO"].ToString()));

            string Landlord_basement = ",";

            if (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length != 0)
                Landlord_basement = ", действующего на основании "
                    + (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_basement"].ToString());

            if (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length != 0)
                Landlord_basement += (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length == 0 ? "" : " № " + dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString());
            if (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length != 0)
                Landlord_basement += " от " + (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString());

            if (Landlord_basement != ",")
                Landlord_basement += ",";
            report.Replace("{Landlord_basement}", Landlord_basement);

            report.Replace("{Tenant_post}", (dtPrintResult.DefaultView[0]["Tenant_post"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_post"].ToString()));
            report.Replace("{Tenant_FIO}", (dtPrintResult.DefaultView[0]["Tenant_FIO"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_FIO"].ToString()));

            string Tenant_basement = ",";

            if (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length != 0)
                Tenant_basement = ", действующего на основании "
                    + (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_basement"].ToString());

            if (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length != 0)
                Tenant_basement += (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length == 0 ? "" : " № " + dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString());
            if (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length != 0)
                Tenant_basement += " от " + (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString());

            if (Tenant_basement != ",")
                Tenant_basement += ",";
            report.Replace("{Tenant_basement}", Tenant_basement);


            report.Replace("{num}", (dtPrintResult.DefaultView[0]["num"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["num"].ToString()));
            report.Replace("{date_con}", (dtPrintResult.DefaultView[0]["date_con"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_con"].ToString()));

            report.Replace("{Landlord_type}", (dtPrintResult.DefaultView[0]["Landlord_type"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_type"].ToString()));
            report.Replace("{Landlord_name}", (dtPrintResult.DefaultView[0]["Landlord_name"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_name"].ToString()));

            report.Replace("{Tenant_type}", (dtPrintResult.DefaultView[0]["Tenant_type"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_type"].ToString()));
            report.Replace("{Tenant_name}", (dtPrintResult.DefaultView[0]["Tenant_name"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_name"].ToString()));

            report.Replace("{Landlord_FIO2}", (dtPrintResult.DefaultView[0]["Landlord_FIO2"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_FIO2"].ToString()));
            report.Replace("{Tenant_FIO2}", (dtPrintResult.DefaultView[0]["Tenant_FIO2"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_FIO2"].ToString()));

            report.Replace("{reklsquare}", (dtPrintResult.DefaultView[0]["reklsquare"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["reklsquare"].ToString()));
            report.Replace("{reklLength}", (dtPrintResult.DefaultView[0]["reklLength"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["reklLength"].ToString()));
            report.Replace("{reklWidth}", (dtPrintResult.DefaultView[0]["reklWidth"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["reklWidth"].ToString()));
            report.Replace("{reklNumber}", (dtPrintResult.DefaultView[0]["reklNumber"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["reklNumber"].ToString()));
        
            //report.SaveAs(getFileNameForReport(fileName.ToString()));
            report.SaveAs(Dir + FileName + ".doc");
            //report.Show();
            report.Close(false);
            Process.Start(Dir + FileName + ".doc");
        }

      // Закоменчено, так как не используется
      /*
        private void print_dopsoglsquare(DataTable dtPrintResult, string FileName)
        {
          string fileName = "";
          if (Temp == "")
            fileName = Application.StartupPath + "\\Templates\\doc\\dopsoglsquare.doc";
          else
            fileName = Temp + "\\dopsoglsquare.doc";

          if (!File.Exists(fileName))
            fileName = Application.StartupPath + "\\Templates\\doc\\dopsoglsquare.doc";

            Nwuram.Framework.ToWord.HandmadeReport report = new Nwuram.Framework.ToWord.HandmadeReport(fileName);
            //report.CurrentTable = report.GetTable(1);
            //report.CutCurrentTable();

            string number = dtPrintResult.DefaultView[0]["num"].ToString();

            //report.Replace("{num}", number);

            //если пришло пусто, то в Excel передается " - " иначе значение из процедуры
            report.Replace("{object_address}", (dtPrintResult.DefaultView[0]["object_address"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["object_address"].ToString()));
            report.Replace("{num}", (dtPrintResult.DefaultView[0]["num"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["num"].ToString()));
            report.Replace("{date_con}", (dtPrintResult.DefaultView[0]["date_con"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_con"].ToString()));
            report.Replace("{date_doc}", (dtPrintResult.DefaultView[0]["date_doc"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_doc"].ToString()));
            report.Replace("{date_doc_renew}", (dtPrintResult.DefaultView[0]["date_doc_renew"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_doc_renew"].ToString()));

            report.Replace("{Landlord_type}", (dtPrintResult.DefaultView[0]["Landlord_type"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_type"].ToString()));
            report.Replace("{Landlord_name}", (dtPrintResult.DefaultView[0]["Landlord_name"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_name"].ToString()));
            report.Replace("{Landlord_post}", (dtPrintResult.DefaultView[0]["Landlord_post"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_post"].ToString()));
            report.Replace("{Landlord_FIO}", (dtPrintResult.DefaultView[0]["Landlord_FIO"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_FIO"].ToString()));


            string Landlord_basement = ",";

            if (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length != 0)
                Landlord_basement = ", действующего на основании "
                    + (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_basement"].ToString());

            if (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length != 0)
                Landlord_basement += (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length == 0 ? "" : " № " + dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString());
            if (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length != 0)
                Landlord_basement += " от " + (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString());

            if (Landlord_basement != ",")
                Landlord_basement += ",";
            report.Replace("{Landlord_basement}", Landlord_basement);


            report.Replace("{Landlord_adress}", (dtPrintResult.DefaultView[0]["Landlord_adress"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_adress"].ToString()));
            report.Replace("{Landlord_INN}", (dtPrintResult.DefaultView[0]["Landlord_INN"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_INN"].ToString()));
            report.Replace("{Landlord_rs}", (dtPrintResult.DefaultView[0]["Landlord_rs"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_rs"].ToString()));
            report.Replace("{Landlord_bank}", (dtPrintResult.DefaultView[0]["Landlord_bank"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_bank"].ToString()));
            report.Replace("{Landlord_bank_BIC}", (dtPrintResult.DefaultView[0]["Landlord_bank_BIC"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_bank_BIC"].ToString()));
            report.Replace("{Landlord_ks}", (dtPrintResult.DefaultView[0]["Landlord_ks"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_ks"].ToString()));
            report.Replace("{Landlord_KPP}", (dtPrintResult.DefaultView[0]["Landlord_KPP"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_KPP"].ToString()));
            report.Replace("{Landlord_FIO2}", (dtPrintResult.DefaultView[0]["Landlord_FIO2"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_FIO2"].ToString()));
            report.Replace("{Tenant_type}", (dtPrintResult.DefaultView[0]["Tenant_type"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_type"].ToString()));
            report.Replace("{Tenant_name}", (dtPrintResult.DefaultView[0]["Tenant_name"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_name"].ToString()));
            report.Replace("{Tenant_post}", (dtPrintResult.DefaultView[0]["Tenant_post"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_post"].ToString()));
            report.Replace("{Tenant_FIO}", (dtPrintResult.DefaultView[0]["Tenant_FIO"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_FIO"].ToString()));


            string Tenant_basement = ",";

            if (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length != 0)
                Tenant_basement = ", действующего на основании "
                    + (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_basement"].ToString());

            if (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length != 0)
                Tenant_basement += (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length == 0 ? "" : " № " + dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString());
            if (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length != 0)
                Tenant_basement += " от " + (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString());

            if (Tenant_basement != ",")
                Tenant_basement += ",";
            report.Replace("{Tenant_basement}", Tenant_basement);


            report.Replace("{Tenant_adress}", (dtPrintResult.DefaultView[0]["Tenant_adress"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_adress"].ToString()));
            report.Replace("{Tenant_INN}", (dtPrintResult.DefaultView[0]["Tenant_INN"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_INN"].ToString()));
            report.Replace("{Tenant_rs}", (dtPrintResult.DefaultView[0]["Tenant_rs"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_rs"].ToString()));
            report.Replace("{Tenant_bank}", (dtPrintResult.DefaultView[0]["Tenant_bank"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_bank"].ToString()));
            report.Replace("{Tenant_bank_BIC}", (dtPrintResult.DefaultView[0]["Tenant_bank_BIC"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_bank_BIC"].ToString()));
            report.Replace("{Tenant_ks}", (dtPrintResult.DefaultView[0]["Tenant_ks"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_ks"].ToString()));
            report.Replace("{Tenant_KPP}", (dtPrintResult.DefaultView[0]["Tenant_KPP"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_KPP"].ToString()));
            report.Replace("{Tenant_FIO2}", (dtPrintResult.DefaultView[0]["Tenant_FIO2"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_FIO2"].ToString()));

            report.Replace("{NDS}", (dtPrintResult.DefaultView[0]["NDS"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["NDS"].ToString()));
            report.Replace("{Total_Area_new}", (dtPrintResult.DefaultView[0]["Total_Area_new"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Total_Area_new"].ToString()));
            report.Replace("{Summa_new}", (dtPrintResult.DefaultView[0]["Summa_new"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Summa_new"].ToString()));
            report.Replace("{Summa_str_new}", (dtPrintResult.DefaultView[0]["Summa_str_new"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Summa_str_new"].ToString()));


        
            //report.SaveAs(getFileNameForReport(fileName.ToString()));
            report.SaveAs(Dir + FileName + ".doc");
            report.Show();
        }

        private void print_rastorg(DataTable dtPrintResult, string FileName)
        {
          string fileName = "";
          if (Temp == "")
            fileName = Application.StartupPath + "\\Templates\\doc\\rastorg.doc";
          else
            fileName = Temp + "\\rastorg.doc";

          if (!File.Exists(fileName))
            fileName = Application.StartupPath + "\\Templates\\doc\\rastorg.doc";

            Nwuram.Framework.ToWord.HandmadeReport report = new Nwuram.Framework.ToWord.HandmadeReport(fileName);            

            string number = dtPrintResult.DefaultView[0]["num"].ToString();

            //если пришло пусто, то в Excel передается " - " иначе значение из процедуры
            report.Replace("{object_address}", (dtPrintResult.DefaultView[0]["object_address"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["object_address"].ToString()));
            report.Replace("{num}", (dtPrintResult.DefaultView[0]["num"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["num"].ToString()));
            report.Replace("{date_con}", (dtPrintResult.DefaultView[0]["date_con"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_con"].ToString()));
            report.Replace("{date_doc}", (dtPrintResult.DefaultView[0]["date_doc"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_doc"].ToString()));
            report.Replace("{date_doc_renew}", (dtPrintResult.DefaultView[0]["date_doc_renew"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_doc_renew"].ToString()));
            report.Replace("{Landlord_type}", (dtPrintResult.DefaultView[0]["Landlord_type"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_type"].ToString()));
            report.Replace("{Landlord_name}", (dtPrintResult.DefaultView[0]["Landlord_name"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_name"].ToString()));
            report.Replace("{Landlord_post}", (dtPrintResult.DefaultView[0]["Landlord_post"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_post"].ToString()));
            report.Replace("{Landlord_FIO}", (dtPrintResult.DefaultView[0]["Landlord_FIO"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_FIO"].ToString()));


            string Landlord_basement = ",";

            if (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length != 0)
                Landlord_basement = ", действующего на основании "
                    + (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_basement"].ToString());

            if (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length != 0)
                Landlord_basement += (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length == 0 ? "" : " № " + dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString());
            if (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length != 0)
                Landlord_basement += " от " + (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString());

            if (Landlord_basement != ",")
                Landlord_basement += ",";
            report.Replace("{Landlord_basement}", Landlord_basement);


            report.Replace("{Landlord_adress}", (dtPrintResult.DefaultView[0]["Landlord_adress"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_adress"].ToString()));
            report.Replace("{Landlord_INN}", (dtPrintResult.DefaultView[0]["Landlord_INN"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_INN"].ToString()));
            report.Replace("{Landlord_rs}", (dtPrintResult.DefaultView[0]["Landlord_rs"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_rs"].ToString()));
            report.Replace("{Landlord_bank}", (dtPrintResult.DefaultView[0]["Landlord_bank"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_bank"].ToString()));
            report.Replace("{Landlord_bank_BIC}", (dtPrintResult.DefaultView[0]["Landlord_bank_BIC"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_bank_BIC"].ToString()));
            report.Replace("{Landlord_ks}", (dtPrintResult.DefaultView[0]["Landlord_ks"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_ks"].ToString()));
            report.Replace("{Landlord_KPP}", (dtPrintResult.DefaultView[0]["Landlord_KPP"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_KPP"].ToString()));
            report.Replace("{Landlord_FIO2}", (dtPrintResult.DefaultView[0]["Landlord_FIO2"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_FIO2"].ToString()));
            report.Replace("{Tenant_type}", (dtPrintResult.DefaultView[0]["Tenant_type"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_type"].ToString()));
            report.Replace("{Tenant_name}", (dtPrintResult.DefaultView[0]["Tenant_name"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_name"].ToString()));
            report.Replace("{Tenant_post}", (dtPrintResult.DefaultView[0]["Tenant_post"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_post"].ToString()));
            report.Replace("{Tenant_FIO}", (dtPrintResult.DefaultView[0]["Tenant_FIO"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_FIO"].ToString()));


            string Tenant_basement = ",";

            if (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length != 0)
                Tenant_basement = ", действующего на основании "
                    + (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_basement"].ToString());

            if (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length != 0)
                Tenant_basement += (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length == 0 ? "" : " № " + dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString());
            if (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length != 0)
                Tenant_basement += " от " + (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString());

            if (Tenant_basement != ",")
                Tenant_basement += ",";
            report.Replace("{Tenant_basement}", Tenant_basement);


            report.Replace("{Tenant_adress}", (dtPrintResult.DefaultView[0]["Tenant_adress"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_adress"].ToString()));
            report.Replace("{Tenant_INN}", (dtPrintResult.DefaultView[0]["Tenant_INN"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_INN"].ToString()));
            report.Replace("{Tenant_rs}", (dtPrintResult.DefaultView[0]["Tenant_rs"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_rs"].ToString()));
            report.Replace("{Tenant_bank}", (dtPrintResult.DefaultView[0]["Tenant_bank"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_bank"].ToString()));
            report.Replace("{Tenant_bank_BIC}", (dtPrintResult.DefaultView[0]["Tenant_bank_BIC"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_bank_BIC"].ToString()));
            report.Replace("{Tenant_ks}", (dtPrintResult.DefaultView[0]["Tenant_ks"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_ks"].ToString()));
            report.Replace("{Tenant_KPP}", (dtPrintResult.DefaultView[0]["Tenant_KPP"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_KPP"].ToString()));
            report.Replace("{Tenant_FIO2}", (dtPrintResult.DefaultView[0]["Tenant_FIO2"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_FIO2"].ToString()));
  

            //report.SaveAs(getFileNameForReport(fileName.ToString()));
            report.SaveAs(Dir + FileName + ".doc");
            report.Show();
        }

        private void print_act_priema_rekl(DataTable dtPrintResult, string FileName)
        {
          string fileName = "";
          if (Temp == "")
            fileName = Application.StartupPath + "\\Templates\\doc\\act_priema_rekl.doc";
          else
            fileName = Temp + "\\act_priema_rekl.doc";

          if (!File.Exists(fileName))
            fileName = Application.StartupPath + "\\Templates\\doc\\act_priema_rekl.doc";

            Nwuram.Framework.ToWord.HandmadeReport report = new Nwuram.Framework.ToWord.HandmadeReport(fileName);

            string number = dtPrintResult.DefaultView[0]["num"].ToString();

            report.Replace("{adress}", (dtPrintResult.DefaultView[0]["adress"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["adress"].ToString()));
            report.Replace("{date_act}", (dtPrintResult.DefaultView[0]["date_act"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_act"].ToString()));


            report.Replace("{arendodatel_str}", (dtPrintResult.DefaultView[0]["arendodatel_str"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["arendodatel_str"].ToString()));
            report.Replace("{post_arendodatel}", (dtPrintResult.DefaultView[0]["post_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["post_arendodatel"].ToString()));
            report.Replace("{FIO_arendodatel_Par}", (dtPrintResult.DefaultView[0]["FIO_arendodatel_Par"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["FIO_arendodatel_Par"].ToString()));


            string Landlord_basement = ",";

            if (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length != 0)
                Landlord_basement = ", действующего на основании "
                    + (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_basement"].ToString());

            if (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length != 0)
                Landlord_basement += (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length == 0 ? "" : " № " + dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString());
            if (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length != 0)
                Landlord_basement += " от " + (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString());

            if (Landlord_basement != ",")
                Landlord_basement += ",";
            report.Replace("{osnovanie_arendodatel}", Landlord_basement);


            report.Replace("{arendator_str}", (dtPrintResult.DefaultView[0]["arendator_str"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["arendator_str"].ToString()));
            report.Replace("{post_arendator}", (dtPrintResult.DefaultView[0]["post_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["post_arendator"].ToString()));
            report.Replace("{FIO_arendator_Par}", (dtPrintResult.DefaultView[0]["FIO_arendator_Par"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["FIO_arendator_Par"].ToString()));


            string Tenant_basement = ",";

            if (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length != 0)
                Tenant_basement = ", действующего на основании "
                    + (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_basement"].ToString());

            if (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length != 0)
                Tenant_basement += (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length == 0 ? "" : " № " + dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString());
            if (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length != 0)
                Tenant_basement += " от " + (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString());

            if (Tenant_basement != ",")
                Tenant_basement += ",";
            report.Replace("{osnovanie_arendator}", Tenant_basement);

            report.Replace("{num}", number);
            report.Replace("{date_con}", (dtPrintResult.DefaultView[0]["date_con"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_con"].ToString()));

            report.Replace("{reklsquare}", (dtPrintResult.DefaultView[0]["reklsquare"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["reklsquare"].ToString()));
            report.Replace("{reklLength}", (dtPrintResult.DefaultView[0]["reklLength"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["reklLength"].ToString()));
            report.Replace("{reklWidth}", (dtPrintResult.DefaultView[0]["reklWidth"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["reklWidth"].ToString()));
            report.Replace("{reklNumber}", (dtPrintResult.DefaultView[0]["reklNumber"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["reklNumber"].ToString()));

            report.Replace("{FIO_arendodatel}", (dtPrintResult.DefaultView[0]["FIO_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["FIO_arendodatel"].ToString()));
            report.Replace("{FIO_arendator}", (dtPrintResult.DefaultView[0]["FIO_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["FIO_arendator"].ToString()));

            
            //report.SaveAs(getFileNameForReport(fileName.ToString()));
            report.SaveAs(Dir + FileName + ".doc");
            report.Show();
        }

        private void print_ds(DataTable dtPrintResult, string FileName)
        {
          string fileName = "";
          if (Temp == "")
            fileName = Application.StartupPath + "\\Templates\\doc\\ds.doc";
          else
            fileName = Temp + "\\ds.doc";

          if (!File.Exists(fileName))
            fileName = Application.StartupPath + "\\Templates\\doc\\ds.doc";

            Nwuram.Framework.ToWord.HandmadeReport report = new Nwuram.Framework.ToWord.HandmadeReport(fileName);

            string number = dtPrintResult.DefaultView[0]["num"].ToString();

            //если пришло пусто, то в Excel передается " - " иначе значение из процедуры
            report.Replace("{object_address}", (dtPrintResult.DefaultView[0]["object_address"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["object_address"].ToString()));
            report.Replace("{num}", (dtPrintResult.DefaultView[0]["num"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["num"].ToString()));
            report.Replace("{date_con}", (dtPrintResult.DefaultView[0]["date_con"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_con"].ToString()));
            report.Replace("{date_doc}", (dtPrintResult.DefaultView[0]["date_doc"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_doc"].ToString()));

            //report.Replace("{date_doc_renew}", (dtPrintResult.DefaultView[0]["date_doc_renew"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_doc_renew"].ToString()));
            report.Replace("{Landlord_type}", (dtPrintResult.DefaultView[0]["Landlord_type"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_type"].ToString()));
            report.Replace("{Landlord_name}", (dtPrintResult.DefaultView[0]["Landlord_name"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_name"].ToString()));
            report.Replace("{Landlord_post}", (dtPrintResult.DefaultView[0]["Landlord_post"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_post"].ToString()));
            report.Replace("{Landlord_FIO}", (dtPrintResult.DefaultView[0]["Landlord_FIO"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_FIO"].ToString()));


            string Landlord_basement = ",";

            if (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length != 0)
                Landlord_basement = ", действующего на основании "
                    + (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_basement"].ToString());

            if (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length != 0)
                Landlord_basement += (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length == 0 ? "" : " № " + dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString());
            if (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length != 0)
                Landlord_basement += " от " + (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString());

            if (Landlord_basement != ",")
                Landlord_basement += ",";
            report.Replace("{Landlord_basement}", Landlord_basement);


            report.Replace("{Landlord_adress}", (dtPrintResult.DefaultView[0]["Landlord_adress"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_adress"].ToString()));
            report.Replace("{Landlord_INN}", (dtPrintResult.DefaultView[0]["Landlord_INN"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_INN"].ToString()));
            report.Replace("{Landlord_rs}", (dtPrintResult.DefaultView[0]["Landlord_rs"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_rs"].ToString()));
            report.Replace("{Landlord_bank}", (dtPrintResult.DefaultView[0]["Landlord_bank"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_bank"].ToString()));
            report.Replace("{Landlord_bank_BIC}", (dtPrintResult.DefaultView[0]["Landlord_bank_BIC"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_bank_BIC"].ToString()));
            report.Replace("{Landlord_ks}", (dtPrintResult.DefaultView[0]["Landlord_ks"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_ks"].ToString()));
            report.Replace("{Landlord_KPP}", (dtPrintResult.DefaultView[0]["Landlord_KPP"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_KPP"].ToString()));
            report.Replace("{Landlord_FIO2}", (dtPrintResult.DefaultView[0]["Landlord_FIO2"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_FIO2"].ToString()));
            report.Replace("{Tenant_type}", (dtPrintResult.DefaultView[0]["Tenant_type"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_type"].ToString()));
            report.Replace("{Tenant_name}", (dtPrintResult.DefaultView[0]["Tenant_name"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_name"].ToString()));
            report.Replace("{Tenant_post}", (dtPrintResult.DefaultView[0]["Tenant_post"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_post"].ToString()));
            report.Replace("{Tenant_FIO}", (dtPrintResult.DefaultView[0]["Tenant_FIO"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_FIO"].ToString()));


            string Tenant_basement = ",";

            if (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length != 0)
                Tenant_basement = ", действующего на основании "
                    + (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_basement"].ToString());

            if (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length != 0)
                Tenant_basement += (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length == 0 ? "" : " № " + dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString());
            if (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length != 0)
                Tenant_basement += " от " + (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString());

            if (Tenant_basement != ",")
                Tenant_basement += ",";
            report.Replace("{Tenant_basement}", Tenant_basement);

            report.Replace("{Tenant_adress}", (dtPrintResult.DefaultView[0]["Tenant_adress"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_adress"].ToString()));
            report.Replace("{Tenant_INN}", (dtPrintResult.DefaultView[0]["Tenant_INN"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_INN"].ToString()));
            report.Replace("{Tenant_rs}", (dtPrintResult.DefaultView[0]["Tenant_rs"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_rs"].ToString()));
            report.Replace("{Tenant_bank}", (dtPrintResult.DefaultView[0]["Tenant_bank"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_bank"].ToString()));
            report.Replace("{Tenant_bank_BIC}", (dtPrintResult.DefaultView[0]["Tenant_bank_BIC"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_bank_BIC"].ToString()));
            report.Replace("{Tenant_ks}", (dtPrintResult.DefaultView[0]["Tenant_ks"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_ks"].ToString()));
            report.Replace("{Tenant_KPP}", (dtPrintResult.DefaultView[0]["Tenant_KPP"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_KPP"].ToString()));
            report.Replace("{Tenant_FIO2}", (dtPrintResult.DefaultView[0]["Tenant_FIO2"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_FIO2"].ToString()));

            //report.SaveAs(getFileNameForReport(fileName.ToString()));
            report.SaveAs(Dir + FileName + ".doc");
            report.Show();
        }

        private void print_inf_letter(DataTable dtPrintResult, string FileName)
        {
          string fileName = "";
          if (Temp == "")
            fileName = Application.StartupPath + "\\Templates\\doc\\inf_letter.doc";
          else
            fileName = Temp + "\\inf_letter.doc";

          if (!File.Exists(fileName))
            fileName = Application.StartupPath + "\\Templates\\doc\\inf_letter.doc";

            Nwuram.Framework.ToWord.HandmadeReport report = new Nwuram.Framework.ToWord.HandmadeReport(fileName);

            DataRow row = dtPrintResult.Rows[0];
            report.Replace("{type}", row["type"].ToString());
            report.Replace("{name}", row["name"].ToString());
            report.Replace("{address}", row["address"].ToString());
            report.Replace("{dd}", row["dd"].ToString());
            report.Replace("{mm}", row["mm"].ToString());

            report.SaveAs(Dir + FileName + ".doc");
            report.Show();
        }
      */

      private void print_act_priema_reklama(DataTable dtPrintResult, DataTable dtDevices, string FileName)
      {
          string fileName = "";
          if (Temp == "")
            fileName = Application.StartupPath + "\\Templates\\doc\\act_priema_reklama.doc";
          else
            fileName = Temp + "\\act_priema_reklama.doc";

          if (!File.Exists(fileName))
            fileName = Application.StartupPath + "\\Templates\\doc\\act_priema_reklama.doc";

              Nwuram.Framework.ToWord.HandmadeReport report = new Nwuram.Framework.ToWord.HandmadeReport(fileName);

              DataRow row = dtPrintResult.Rows[0];
              report.Replace("{num_dog}", row["num_dog"].ToString(), false);
              report.Replace("{date_dog}", row["date_dog"].ToString(), false);
              report.Replace("{arendodatel}", row["arendodatel"].ToString(), false);
              report.Replace("{arendator}", row["arendator"].ToString(), false);
              report.Replace("{date_akt}", row["date_akt"].ToString(), false);
              report.Replace("{predstavitel}", row["predstavitel"].ToString(), false);
              report.Replace("{osnovanie_arendodatel}", row["osnovanie_arendodatel"].ToString(), false);
              report.Replace("{osnovanie_arendator}", row["osnovanie_arendator"].ToString(), false);
              report.Replace("{inn}", row["inn"].ToString(), false);
              report.Replace("{okonchanie_1}", row["okonchanie_1"].ToString(), false);
              report.Replace("{okonchanie_2}", row["okonchanie_2"].ToString(), false);
              report.Replace("{okonchanie_a}", row["okonchanie_a"].ToString(), false);
              report.Replace("{size_v}", row["size_v"].ToString(), false);
              report.Replace("{area}", row["area"].ToString(), false);
              report.Replace("{size_object}", row["size_object"].ToString(), false);

              report.ExportDataToTable(2, dtDevices, true);

              report.SaveAs(Dir + FileName + ".doc");
              //report.Show();
              report.Close(false);
              Process.Start(Dir + FileName + ".doc");
          }
      
      private void print_act_priema_vozvrat(DataTable dtPrintResult, DataTable dtDevices, DataTable dtEquipment, string FileName)
      {
        string fileName = "";
        if (Temp == "")
          fileName = Application.StartupPath + "\\Templates\\doc\\act_priema_vozvrat.doc";
        else
          fileName = Temp + "\\act_priema_vozvrat.doc";

        if (!File.Exists(fileName))
          fileName = Application.StartupPath + "\\Templates\\doc\\act_priema_vozvrat.doc";

              Nwuram.Framework.ToWord.HandmadeReport report = new Nwuram.Framework.ToWord.HandmadeReport(fileName);

              DataRow row = dtPrintResult.Rows[0];
              report.Replace("{num_dog}", row["num_dog"].ToString(), false);
              report.Replace("{date_dog}", row["date_dog"].ToString(), false);
              report.Replace("{arendodatel}", row["arendodatel"].ToString(), false);
              report.Replace("{arendator}", row["arendator"].ToString(), false);
              report.Replace("{arendator_sokr}", row["arendator_sokr"].ToString(), false);
              report.Replace("{date_akt}", row["date_akt"].ToString(), false);
              report.Replace("{predstavitel}", row["predstavitel"].ToString(), false);
              report.Replace("{arendator_predst}", row["arendator_predst"].ToString(), false);
              report.Replace("{osnovanie_arendodatel}", row["osnovanie_arendodatel"].ToString(), false);
              report.Replace("{osnovanie_arendator}", row["osnovanie_arendator"].ToString(), false);
              report.Replace("{inn}", row["inn"].ToString(), false);
              report.Replace("{okonchanie_1}", row["okonchanie_1"].ToString(), false);
              report.Replace("{okonchanie_2}", row["okonchanie_2"].ToString(), false);
              report.Replace("{okonchanie_a}", row["okonchanie_a"].ToString(), false);
              report.Replace("{address}", row["address"].ToString(), false);
              report.Replace("{area}", row["area"].ToString(), false);
              report.Replace("{section}", row["section"].ToString(), false);
              report.Replace("{floor}", row["floor"].ToString(), false);

              report.ExportDataToTable(2, dtDevices, true);
              report.ExportDataToTable(3, dtEquipment, false);

              report.SaveAs(Dir + FileName + ".doc");
              //report.Show();
              report.Close(false);
              Process.Start(Dir + FileName + ".doc");
          }

          private void print_act_priema_peredachi(DataTable dtPrintResult, DataTable dtDevices, DataTable dtEquipment, string FileName)
          {
            string fileName = "";
            if (Temp == "")
              fileName = Application.StartupPath + "\\Templates\\doc\\act_priema_peredachi.doc";
            else
              fileName = Temp + "\\act_priema_peredachi.doc";

            if (!File.Exists(fileName))
              fileName = Application.StartupPath + "\\Templates\\doc\\act_priema_peredachi.doc";

              Nwuram.Framework.ToWord.HandmadeReport report = new Nwuram.Framework.ToWord.HandmadeReport(fileName);

              DataRow row = dtPrintResult.Rows[0];
              report.Replace("{num_dog}", row["num_dog"].ToString(), false);
              report.Replace("{date_dog}", row["date_dog"].ToString(), false);
              report.Replace("{arendodatel}", row["arendodatel"].ToString(), false);
              report.Replace("{arendator}", row["arendator"].ToString(), false);
              report.Replace("{date_akt}", row["date_akt"].ToString(), false);
              report.Replace("{predstavitel}", row["predstavitel"].ToString(), false);
              report.Replace("{arendator_predst}", row["arendator_predst"].ToString(), false);
              report.Replace("{osnovanie_arendodatel}", row["osnovanie_arendodatel"].ToString(), false);
              report.Replace("{osnovanie_arendator}", row["osnovanie_arendator"].ToString(), false);
              report.Replace("{area}", row["area"].ToString(), false);
              report.Replace("{section}", row["section"].ToString(), false);
              report.Replace("{floor}", row["floor"].ToString(), false);
              report.Replace("{okonchanie_1}", row["okonchanie_1"].ToString(), false);
              report.Replace("{okonchanie_2}", row["okonchanie_2"].ToString(), false);
              //report.Replace("{count_lamps}", row["count_lamps"].ToString(), false);

              report.ExportDataToTable(2, dtDevices, true);
              report.ExportDataToTable(3, dtEquipment, false);

              report.SaveAs(Dir + FileName + ".doc");
              //report.Show();
              report.Close(false);
              Process.Start(Dir + FileName + ".doc");
          }

          private void print_dogovor_reklama(DataTable dtPrintResult, string FileName)
          {
            string fileName = "";
            if (Temp == "")
              fileName = Application.StartupPath + "\\Templates\\doc\\dogovor_reklama.doc";
            else
              fileName = Temp + "\\dogovor_reklama.doc";

            if (!File.Exists(fileName))
              fileName = Application.StartupPath + "\\Templates\\doc\\dogovor_reklama.doc";

              Nwuram.Framework.ToWord.HandmadeReport report = new Nwuram.Framework.ToWord.HandmadeReport(fileName);

              DataRow row = dtPrintResult.Rows[0];
              report.Replace("{num_dog}", row["num_dog"].ToString(), false);
              report.Replace("{date_dog}", row["date_dog"].ToString(), false);
              report.Replace("{arendodatel}", row["arendodatel"].ToString(), false);
              report.Replace("{arendator}", row["arendator"].ToString(), false);
              report.Replace("{predstavitel}", row["predstavitel"].ToString(), false);
              report.Replace("{osnovanie_arendodatel}", row["osnovanie_arendodatel"].ToString(), false);
              report.Replace("{osnovanie_arendator}", row["osnovanie_arendator"].ToString(), false);
              report.Replace("{okonchanie_1}", row["okonchanie_1"].ToString(), false);
              report.Replace("{okonchanie_2}", row["okonchanie_2"].ToString(), false);
              report.Replace("{okonchanie_a}", row["okonchanie_a"].ToString(), false);
              report.Replace("{area}", row["area"].ToString(), false);
              report.Replace("{size}", row["size"].ToString(), false);
              report.Replace("{size_inf}", row["size_inf"].ToString(), false);
              report.Replace("{date_start}", row["date_start"].ToString(), false);
              report.Replace("{date_end}", row["date_end"].ToString(), false);
              report.Replace("{sum_arenda}", row["sum_arenda"].ToString(), false);
              report.Replace("{inn}", row["inn"].ToString(), false);

              report.Replace("{name_1}", row["name_1"].ToString(), false);
              report.Replace("{name_2}", row["name_2"].ToString(), false);
              report.Replace("{address_1}", row["address_1"].ToString(), false);
              report.Replace("{address_2}", row["address_2"].ToString(), false);
              report.Replace("{ogrn_1}", row["ogrn_1"].ToString(), false);
              report.Replace("{inn_kpp_1}", row["inn_kpp_1"].ToString(), false);
              report.Replace("{inn_2}", row["inn_2"].ToString(), false);
              report.Replace("{orgnip_2}", row["orgnip_2"].ToString(), false);
              report.Replace("{rs_1}", row["rs_1"].ToString(), false);
              report.Replace("{rs_2}", row["rs_2"].ToString(), false);
              report.Replace("{bank_1}", row["bank_1"].ToString(), false);
              report.Replace("{bank_2}", row["bank_2"].ToString(), false);
              report.Replace("{bik_1}", row["bik_1"].ToString(), false);
              report.Replace("{bik_2}", row["bik_2"].ToString(), false);
              report.Replace("{ks_1}", row["ks_1"].ToString(), false);
              report.Replace("{ks_2}", row["ks_2"].ToString(), false);
              report.Replace("{kpp_1}", row["kpp_1"].ToString(), false);
              report.Replace("{predst_1}", row["predst_1"].ToString(), false);
              report.Replace("{predst_2}", row["predst_2"].ToString(), false);

              report.SaveAs(Dir + FileName + ".doc");
              //report.Show();
              report.Close(false);
              Process.Start(Dir + FileName + ".doc");
          }

          private void print_dop_sogl(DataTable dtPrintResult, string FileName)
          {
            string fileName = "";
            if (Temp == "")
              fileName = Application.StartupPath + "\\Templates\\doc\\dop_sogl.doc";
            else
              fileName = Temp + "\\dop_sogl.doc";

            if (!File.Exists(fileName))
              fileName = Application.StartupPath + "\\Templates\\doc\\dop_sogl.doc";

              Nwuram.Framework.ToWord.HandmadeReport report = new Nwuram.Framework.ToWord.HandmadeReport(fileName);

              DataRow row = dtPrintResult.Rows[0];
              report.Replace("{num_dog}", row["num_dog"].ToString(), false);
              report.Replace("{date_dog}", row["date_dog"].ToString(), false);
              report.Replace("{date_dop}", row["date_dop"].ToString(), false);
              report.Replace("{arendodatel_sokr}", row["name_1"].ToString(), false);
              report.Replace("{arendator}", row["arendator"].ToString(), false);
              report.Replace("{predstavitel}", row["predstavitel"].ToString(), false);
              report.Replace("{predst_arend}", row["predst_arend"].ToString(), false);
              report.Replace("{osnovanie_arendodatel}", row["osnovanie_arendodatel"].ToString(), false);
              report.Replace("{osnovanie_arendator}", row["osnovanie_arendator"].ToString(), false);
              report.Replace("{okonchanie_1}", row["okonchanie_1"].ToString(), false);
              report.Replace("{okonchanie_a}", row["okonchanie_a"].ToString(), false);
             

              report.Replace("{name_1}", row["name_1"].ToString(), false);
              report.Replace("{name_2}", row["name_2"].ToString(), false);
              report.Replace("{address_1}", row["address_1"].ToString(), false);
              report.Replace("{address_2}", row["address_2"].ToString(), false);
             
              report.Replace("{inn_1}", row["inn_1"].ToString(), false);
              report.Replace("{inn_2}", row["inn_2"].ToString(), false);
              report.Replace("{orgnip}", row["orgnip"].ToString(), false);
              report.Replace("{rs_1}", row["rs_1"].ToString(), false);
              report.Replace("{rs_2}", row["rs_2"].ToString(), false);

              report.Replace("{bank}", row["bank"].ToString(), false);
              report.Replace("{passport}", row["passport"].ToString(), false);
              
              report.Replace("{bik_1}", row["bik_1"].ToString(), false);
              report.Replace("{bik_2}", row["bik_2"].ToString(), false);
              report.Replace("{ks_1}", row["ks_1"].ToString(), false);
              report.Replace("{ks_2}", row["ks_2"].ToString(), false);

              report.Replace("{tel_mail_1}", row["tel_mail_1"].ToString(), false);
              report.Replace("{tel_mail_2}", row["tel_mail_2"].ToString(), false);

              report.Replace("{kpp_1}", row["kpp_1"].ToString(), false);
              report.Replace("{predst_1}", row["predst_1"].ToString(), false);
              report.Replace("{predst_2}", row["predst_2"].ToString(), false);

              report.SaveAs(Dir + FileName + ".doc");
              //report.Show();
              report.Close(false);
              Process.Start(Dir + FileName + ".doc");
          }

          private void print_sogl_rastor(DataTable dtPrintResult, string FileName)
          {
            string fileName = "";
            if (Temp == "")
              fileName = Application.StartupPath + "\\Templates\\doc\\sogl_rastor.doc";
            else
              fileName = Temp + "\\sogl_rastor.doc";

            if (!File.Exists(fileName))
              fileName = Application.StartupPath + "\\Templates\\doc\\sogl_rastor.doc";

              Nwuram.Framework.ToWord.HandmadeReport report = new Nwuram.Framework.ToWord.HandmadeReport(fileName);

              DataRow row = dtPrintResult.Rows[0];
              report.Replace("{num_dog}", row["num_dog"].ToString(), false);
              report.Replace("{date_dog}", row["date_dog"].ToString(), false);
              report.Replace("{date_sogl}", row["date_sogl"].ToString(), false);
              report.Replace("{arendodatel}", row["arendodatel"].ToString(), false);
              report.Replace("{arendator}", row["arendator"].ToString(), false);
              report.Replace("{predstavitel}", row["predstavitel"].ToString(), false);
              report.Replace("{arendator_predst}", row["arendator_predst"].ToString(), false);
              report.Replace("{osnovanie_arendodatel}", row["osnovanie_arendodatel"].ToString(), false);
              report.Replace("{osnovanie_arendator}", row["osnovanie_arendator"].ToString(), false);
              report.Replace("{okonchanie_1}", row["okonchanie_1"].ToString(), false);
              report.Replace("{okonchanie_2}", row["okonchanie_2"].ToString(), false);
              report.Replace("{okonchanie_a}", row["okonchanie_a"].ToString(), false);
              report.Replace("{floor}", row["floor"].ToString(), false);
              report.Replace("{section}", row["section"].ToString(), false);
              report.Replace("{area}", row["area"].ToString(), false);


              report.Replace("{name_1}", row["name_1"].ToString(), false);
              report.Replace("{name_2}", row["name_2"].ToString(), false);
              report.Replace("{address_1}", row["address_1"].ToString(), false);
              report.Replace("{address_2}", row["address_2"].ToString(), false);

              report.Replace("{inn_1}", row["inn_1"].ToString(), false);
              report.Replace("{inn_2}", row["inn_2"].ToString(), false);
              
              report.Replace("{rs_1}", row["rs_1"].ToString(), false);
              

              report.Replace("{bank_1}", row["bank_1"].ToString(), false);

              report.Replace("{bik_1}", row["bik_1"].ToString(), false);
              
              report.Replace("{ks_1}", row["ks_1"].ToString(), false);
              

              report.Replace("{tel_mail_1}", row["tel_mail_1"].ToString(), false);
              

              report.Replace("{kpp_1}", row["kpp_1"].ToString(), false);
              report.Replace("{predst_1}", row["predst_1"].ToString(), false);
              report.Replace("{predst_2}", row["predst_2"].ToString(), false);

              report.SaveAs(Dir + FileName + ".doc");
              //report.Show();
              report.Close(false);
              Process.Start(Dir + FileName + ".doc");
          }

          private void print_act_komm(DataTable dtPrintResult, DataTable dtDevices, string FileName)
          {
            string fileName = "";
            if (Temp == "")
              fileName = Application.StartupPath + "\\Templates\\doc\\act_komm.doc";
            else
              fileName = Temp + "\\act_komm.doc";

            if (!File.Exists(fileName))
              fileName = Application.StartupPath + "\\Templates\\doc\\act_komm.doc";

              Nwuram.Framework.ToWord.HandmadeReport report = new Nwuram.Framework.ToWord.HandmadeReport(fileName);

              DataRow row = dtPrintResult.Rows[0];
              report.Replace("{num_dog}", row["num_dog"].ToString(), false);
              report.Replace("{date_dog}", row["date_dog"].ToString(), false);
              report.Replace("{date_akt}", row["date_akt"].ToString(), false);
              report.Replace("{arendodatel}", row["arendodatel"].ToString(), false);
              report.Replace("{arendator}", row["arendator"].ToString(), false);
              report.Replace("{predstavitel}", row["predstavitel"].ToString(), false);
              report.Replace("{arendator_predst}", row["arendator_predst"].ToString(), false);
             
              report.Replace("{section}", row["section"].ToString(), false);

              report.Replace("{okonchanie_1}", row["okonchanie_1"].ToString(), false);
              report.Replace("{okonchanie_2}", row["okonchanie_2"].ToString(), false);

              report.ExportDataToTable(1, dtDevices, true);

              report.SaveAs(Dir + FileName + ".doc");
              //report.Show();
              report.Close(false);
              Process.Start(Dir + FileName + ".doc");
          }

          private void print_dogovor_arenda(DataTable dtPrintResult, string FileName)
          {
            string fileName = "";
            if (Temp == "")
              fileName = Application.StartupPath + "\\Templates\\doc\\dogovor.doc";
            else
              fileName = Temp + "\\dogovor.doc";

            if (!File.Exists(fileName))
              fileName = Application.StartupPath + "\\Templates\\doc\\dogovor.doc";
            
              Nwuram.Framework.ToWord.HandmadeReport report = new Nwuram.Framework.ToWord.HandmadeReport(fileName);

              DataRow row = dtPrintResult.Rows[0];
              //report.Replace("{adress1}", row["adress1"].ToString(), false);
              report.Replace("{num_dog}", row["num_dog"].ToString(), false);
              report.Replace("{date_dog}", row["date_dog"].ToString(), false);
              report.Replace("{arendodatel}", row["arendodatel"].ToString(), false);
              report.Replace("{arendator}", row["arendator"].ToString(), false);
              report.Replace("{predstavitel}", row["predstavitel"].ToString(), false);
              report.Replace("{arendator_predst}", row["arendator_predst"].ToString(), false);
              report.Replace("{osnovanie_arendodatel}", row["osnovanie_arendodatel"].ToString(), false);
              report.Replace("{osnovanie_arendator}", row["osnovanie_arendator"].ToString(), false);
              report.Replace("{okonchanie_1}", row["okonchanie_1"].ToString(), false);
              report.Replace("{okonchanie_2}", row["okonchanie_2"].ToString(), false);
              report.Replace("{okonchanie_a}", row["okonchanie_a"].ToString(), false);
              report.Replace("{inn}", row["inn"].ToString(), false);
              report.Replace("{floor}", row["floor"].ToString(), false);
              report.Replace("{section}", row["section"].ToString(), false);
              report.Replace("{area_all}", row["area_all"].ToString(), false);
              report.Replace("{area_sale}", row["area_sale"].ToString(), false);
              report.Replace("{dest}", row["dest"].ToString(), false);
              report.Replace("{sum_arenda}", row["sum_arenda"].ToString(), false);
              //report.Replace("{sum_metr}", row["sum_metr"].ToString(), false);
              report.Replace("{dop_str}", row["dop_str"].ToString(), false);
              report.Replace("{date_start}", row["date_start"].ToString(),false);
              report.Replace("{date_end}", row["date_end"].ToString(), false);
              report.Replace("{name_1}", row["name_1"].ToString(), false);
              report.Replace("{name_2}", row["name_2"].ToString(), false);
              report.Replace("{address_1}", row["address_1"].ToString(), false);
              report.Replace("{address_2}", row["address_2"].ToString(), false);
              report.Replace("{inn_1}", row["inn_1"].ToString(), false);
              report.Replace("{inn_2}", row["inn_2"].ToString(), false);
              report.Replace("{rs_1}", row["rs_1"].ToString(), false);
              report.Replace("{rs_2}", row["rs_2"].ToString(), false);

              report.Replace("{bank}", row["bank"].ToString(), false);
              report.Replace("{passport}", row["passport"].ToString(), false);

              report.Replace("{bik_1}", row["bik_1"].ToString(), false);
              report.Replace("{bik_2}", row["bik_2"].ToString(), false);

              report.Replace("{ks_1}", row["ks_1"].ToString(), false);
              report.Replace("{ks_2}", row["ks_2"].ToString(), false);

              report.Replace("{tel_mail_1}", row["tel_mail_1"].ToString(), false);
              report.Replace("{tel_mail_2}", row["tel_mail_2"].ToString(), false);

              report.Replace("{kpp_1}", row["kpp_1"].ToString(), false);
              report.Replace("{orgnip}", row["orgnip"].ToString(), false);
              report.Replace("{predst_1}", row["predst_1"].ToString(), false);
              report.Replace("{predst_2}", row["predst_2"].ToString(), false);

              report.SaveAs(Dir + FileName + ".doc");
              //report.Show();
              report.Close(false);
              Process.Start(Dir + FileName + ".doc");
          }

        //private DateTime dateDoc;
        private string dateDoc, numDoc, nameArend, position, dateStartDoc, dateEndDoc, idArend;
        //private int idArend;

        public void setData(string dateDoc, string numDoc, string nameArend, string position, string dateStartDoc, string dateEndDoc, string idArend)
        {
            this.dateDoc = dateDoc;
            this.numDoc = numDoc;
            this.nameArend=  nameArend;
            this.position=  position;
            this.dateStartDoc =  dateStartDoc;
            this.dateEndDoc =  dateEndDoc;
            this.idArend = idArend;
        }
    }
}
