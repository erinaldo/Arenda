using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using System.Text.RegularExpressions;
using System.Threading;
using Nwuram.Framework.ToExcel;
using Nwuram.Framework.Logging;
using System.IO;
using System.Security.AccessControl;

namespace Arenda
{
    public partial class AddEditTenant : Form
    {
        int id; string type; string cName; string name; string otc; string fam; string famR; int sex; string wphone; string hphone; string mphone;
        string _adress; string pa; string okpo; string kpp; string inn; string WiS; string dateReg;
        string regNum; string numCert; string serCer; string WPON; string numAcc; string serAcc; int nds; string _remark; string _email;
            DataTable Ten; int id_obj; string path;
            DataTable cpTen;
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        Procedures bgwProc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        int bankevich;
        bool cheakName = false;
        string rezhim;
        bool tenant;
        bool shown = false;
        string _PredPost, _tbnumbas, _tbAdrTrade, _cbBasment, _textBox1, _tbBank, _tbAc, _tbBIK, _tbLastname, _tbName, _tbSecondname, _tbDepartment, _tbPassport, _tbIssued, _tbAddress, _tbOrgnip, _tbDateIssue, _tbOGRN;
        DateTime _dtbase;
        TabPage tb;
        bool _sex;

        DataTable DocById, _Bank, dtSettings;
        DateTime SRZA, CurDate;
        DataTable basList;

        bool cp = true;

        string orgnip = "", ogrn = "";
        
        public AddEditTenant(bool prz)
        {
            rezhim = "add"; tenant = prz;

            InitializeComponent();
            FillCb();
            radioButton2.Checked = true;
           // if (prz==false)
                tabControl1.TabPages.Remove(tabPage3);
            if (prz == false)
            { 
                this.Text = "Добавление арендодателя";                
               // checkBox1.Enabled = false; 
                tbAdrTrade.Enabled = true;
                tabControl1.TabPages.Remove(tbpAddInfo);
                //lEmail.Visible = tbEmail.Visible = false;
                groupBox9.Visible = false;
                groupBox10.Visible = false;
            }
            else 
            { 
                this.Text = "Добавление арендатора";                
               // checkBox1.Enabled = true;
                checkBox1.Checked = true;
                tbAdrTrade.Enabled = false;
                groupBox8.Visible = false;
                label44.Visible = cmbObject.Visible = label28.Visible = tbOGRN.Visible = false;
                label45.Visible = tbScanD.Visible = button5.Visible = false;
            }
            PredPostRefresh();
            PredPost.SelectedValue = -1;
            GetTab();            
        }

        public AddEditTenant(int _id, bool _tenant, bool prosmotr)
        {
            id = _id;
            tenant = _tenant;
            rezhim = prosmotr ? "view" : "edit";
            InitializeComponent();

            groupBox10.Visible = _tenant;

            FillCb();
            GetData();

            if (!tenant)
            {
                tabControl1.TabPages.Remove(tabPage3);
                tabControl1.TabPages.Remove(tbpAddInfo);
            }

            if (rezhim == "view")
            {
                this.Text = "Просмотр";
                this.Text += tenant ? " арендатора" : " арендодателя";
                ViewSettings();
            }
            else
            {
                this.Text = "Редактирование";
                this.Text += tenant ? " арендатора" : " арендодателя";
            }
            GetTab();            

        }

        /// <summary>
        /// Запоминаем значения после открытия формы 
        /// для дальнейшего сравнения при попытке выхода из формы
        /// </summary>

        private int id_type = 0;
        private int id_predPost = 0;
        private int id_basment = 0;
        private int _chbShowInReport = 0;
        private int _bankevich = 0;
        private string _txtAddress = "";
        private void GetDefaultValues()
        {
            //tabPage1 Основная информация            
            type = cbTypeOrg.Text;
            id_type = (int)cbTypeOrg.SelectedIndex;
            cName = orgName.Text;
            id_predPost = PredPost.SelectedValue == null ? 0 : (int)PredPost.SelectedValue;
            _PredPost = PredPost.Text;
            fam = Fam.Text;
            famR = FamR.Text;
            name = pName.Text;
            otc = pOtcest.Text;
            sex = radioButton2.Checked ? 1 : 0;
            wphone = telrab.Text;
            hphone = telhome.Text;
            mphone = telsot.Text;
            _tbAdrTrade = tbAdrTrade.Text;
            _adress = adress.Text;
            nds = checkBox1.Checked ? 1 : 0;
            _remark = remark.Text;
            _email = tbEmail.Text;
            path = tbScanD.Text;
            _chbShowInReport = chbShowInReport.Checked ? 1 : 0;
            if (!tenant)
            {
                if (cmbObject.SelectedValue != null)
                    id_obj = (int)cmbObject.SelectedValue;
            }
            //tabPage2 Реквизиты
            _textBox1 = textBox1.Text;
            _bankevich = bankevich;
            _tbBank = tbBank.Text;
            _tbAc = tbAc.Text;
            _tbBIK = tbBIK.Text;
            pa = tbRk.Text;
            okpo = tborpo.Text;
            kpp = tbKpp.Text;
            inn = tbInn.Text;
            _cbBasment = (cbBasment.SelectedValue != null && (int)cbBasment.SelectedValue != 0) ? cbBasment.Text : "";
            id_basment = cbBasment.SelectedValue == null ? 0 : (int)cbBasment.SelectedValue;
            _tbnumbas = tbnumbas.Text;
            _dtbase = dtbase.Value.Date;
            WiS = tbKem.Text;
            dateReg = maskedtbdatareg.Text;
            regNum = tbNreg.Text;
            numCert = tbNsvid.Text;
            serCer = tbSer.Text;
            WPON = tbPny.Text;
            numAcc = tbNchet.Text;
            serAcc = tbSerP.Text;
            _tbOGRN = tbOGRN.Text;
            //Дополнительно
            _tbLastname = txtLastName.Text;
            _tbName = txtName.Text;
            _tbSecondname = txtSecondName.Text;
            _tbDepartment = txtDepartment.Text;
            _tbDateIssue = dtpDateIssue.Value.Date.ToShortDateString();
            _tbPassport = txtPassport.Text;
            _tbIssued = txtIssued.Text;
            _txtAddress = txtAddress.Text;
            _sex = rbW.Checked;
            _tbOrgnip = txtORGNIP.Text;
        }

        private void GetData()
        {
            Ten = _proc.getLT(id);

            tbAdrTrade.Text = Ten.Rows[0]["Address_trade_premises"].ToString(); //_proc.getLT(id).Rows[0]["Address_trade_premises"].ToString();

            try
            {
                type = Ten.Rows[0]["type_abb"].ToString();
                type = cbTypeOrg.Text = cbTypeOrg.Items[cbTypeOrg.FindString(type)].ToString();
            }
            catch (Exception)
            {
                cbTypeOrg.Items.Add(type);
                cbTypeOrg.Text = cbTypeOrg.Items[cbTypeOrg.FindString(type)].ToString();
            }

            orgName.Text = Ten.Rows[0]["cName"].ToString(); //_proc.getLT(id).Rows[0][2].ToString();
            Fam.Text = Ten.Rows[0]["Contact_Lastname"].ToString();//_proc.getLT(id).Rows[0][5].ToString();
            FamR.Text = Ten.Rows[0]["Contact_Lastname_Par"].ToString(); //_proc.getLT(id).Rows[0]["Contact_Lastname_Par"].ToString();
            pName.Text = Ten.Rows[0]["Contact_Firstname"].ToString();//_proc.getLT(id).Rows[0][3].ToString();
            pOtcest.Text = Ten.Rows[0]["Contact_Middlename"].ToString();//_proc.getLT(id).Rows[0][4].ToString();

            radioButton1.Checked = !bool.Parse(Ten.Rows[0]["Sex"].ToString());
            radioButton2.Checked = bool.Parse(Ten.Rows[0]["Sex"].ToString());
            chbShowInReport.Checked = bool.Parse(Ten.Rows[0]["outReport"].ToString());

            telrab.Text = Ten.Rows[0]["Work_phone"].ToString();
            telhome.Text = Ten.Rows[0]["Home_phone"].ToString();
            telsot.Text = Ten.Rows[0]["Mobile_phone"].ToString();
            adress.Text = Ten.Rows[0]["Address"].ToString();
            tbFactAdress.Text = Ten.Rows[0]["FactAdress"].ToString();
            remark.Text = Ten.Rows[0]["Remark"].ToString();
            tbEmail.Text = Ten.Rows[0]["email"].ToString();

            checkBox1.Checked = bool.Parse(Ten.Rows[0]["Vat_Nds"].ToString());

            tbRk.Text = Ten.Rows[0]["PaymentAccount"].ToString();
            tborpo.Text = Ten.Rows[0]["OKPO"].ToString();
            tbKpp.Text = Ten.Rows[0]["KPP"].ToString();
            tbInn.Text = Ten.Rows[0]["INN"].ToString();

            tbKem.Text = Ten.Rows[0]["Who_is_Registered"].ToString();
            maskedtbdatareg.Text = Ten.Rows[0]["DateRegistration"].ToString();
            tbNreg.Text = Ten.Rows[0]["RegistrationNumber"].ToString();
            tbNsvid.Text = Ten.Rows[0]["Number_of_Certificate"].ToString();
            tbSer.Text = Ten.Rows[0]["Series_od_Certificate"].ToString();
            tbPny.Text = Ten.Rows[0]["Who_put_on_Account"].ToString();
            tbNchet.Text = Ten.Rows[0]["Number_Accounting"].ToString();
            tbSerP.Text = Ten.Rows[0]["Series_of_Accounting"].ToString();
            tbnumbas.Text = Ten.Rows[0]["Number_basement"].ToString();
            try
            {
                dtbase.Value = Convert.ToDateTime(Ten.Rows[0]["Date_basement"].ToString());
            }
            catch (Exception) { }

            try
            {
                cbBasment.SelectedValue = int.Parse(Ten.Rows[0]["id_Basement"].ToString());
            }
            catch (Exception) { }
            //checkBox1.Enabled = tenant;
            tbAdrTrade.Enabled = !tenant;
            groupBox8.Visible = !tenant;
            label44.Visible = cmbObject.Visible = label28.Visible = tbOGRN.Visible = !tenant;
            label45.Visible = tbScanD.Visible = button5.Visible = !tenant;
            //lEmail.Visible = tbEmail.Visible = tenant;
            groupBox9.Visible = tenant;

            bankevich = Convert.ToInt32(Ten.Rows[0]["id_Bank"].ToString());

            try
            {
                _Bank = _proc.getBank();
                DataRow[] Temp;

                Temp = _Bank.Select("id =" + bankevich + " ");

                tbBank.Text = Temp[0].ItemArray[1].ToString();
                tbAc.Text = Temp[0].ItemArray[2].ToString();
                tbBIK.Text = Temp[0].ItemArray[3].ToString();
            }
            catch //(Exception ex)
            {
                //MessageBox.Show("Использован удаленный банк."); 
            }

            ini(id);

            PredPostRefresh();
            PredPost.SelectedValue = int.Parse(Ten.Rows[0]["id_Posts"].ToString());
            if (!tenant)
            {
                if (Ten.Rows[0]["id_ObjectLease"] != null)
                    cmbObject.SelectedValue = int.Parse(Ten.Rows[0]["id_ObjectLease"].ToString());

                if (cmbObject.SelectedValue == null)
                {
                    DataTable dtObj = _proc.GetObjects();
                    dtObj.DefaultView.RowFilter = "isActive = 1 OR id = " + Ten.Rows[0]["id_ObjectLease"].ToString();
                    dtObj.DefaultView.Sort = "cName";
                    cmbObject.DataSource = dtObj;
                    cmbObject.SelectedValue = int.Parse(Ten.Rows[0]["id_ObjectLease"].ToString());
                }

                tbScanD.Text = Ten.Rows[0]["Path"].ToString();
            }
            if (/*tenant &&*/ id != 0)
            {
                FillAddInfo(id);
                FillParentChildTenant();
            }
        }

      private void FillParentChildTenant()
      {
        if (tenant)
        {
          if (rezhim == "view")
            cpTen = _proc.GetParentChildTenant(id, 0);
          else
            cpTen = _proc.GetParentChildTenant(id, 1);
          dgCon.AutoGenerateColumns = false;
          cpTen.Columns.Add("num");
          if (cpTen != null && cpTen.Rows.Count > 0)
          {
            if(cpTen.Select("id_child = " + id.ToString()).Length > 0)
              btAddCon.Enabled = btDelCon.Enabled = cp = false;
            foreach (DataRow r in cpTen.Rows)
            {
              r["num"] = /*cpTen.Rows.Count - */cpTen.Rows.IndexOf(r) + 1;
            }
            cpTen.DefaultView.Sort = "num desc";
            dgCon.DataSource = cpTen;
          }
        }
      }

        private void FillAddInfo(int id)
        {
            DataTable dt = _proc.GetTenantAddInfo(id);

            if (dt != null && dt.Rows.Count > 0)
            {
              if (tenant)
              {
                txtLastName.Text = dt.Rows[0]["last_name"].ToString();
                txtName.Text = dt.Rows[0]["name"].ToString();
                txtSecondName.Text = dt.Rows[0]["second_name"].ToString();
                txtDepartment.Text = dt.Rows[0]["department"].ToString();
                dtpDateIssue.Value = Convert.ToDateTime(dt.Rows[0]["date_issue"]);
                txtPassport.Text = dt.Rows[0]["passport"].ToString();
                txtIssued.Text = dt.Rows[0]["issued"].ToString();
                txtAddress.Text = dt.Rows[0]["address"].ToString();
                rbM.Checked = !Convert.ToBoolean(dt.Rows[0]["sex"]);
                rbW.Checked = Convert.ToBoolean(dt.Rows[0]["sex"]);
                txtORGNIP.Text = orgnip = dt.Rows[0]["orgnip"].ToString();

                if (cbBasment.Text == "Свидетельства")
                {
                  tbnumbas.Text = orgnip;
                  tbnumbas.Enabled = false;
                }
              }
              else
                tbOGRN.Text = ogrn = dt.Rows[0]["orgnip"].ToString();
            }

            dtpDateIssue.MaxDate = DateTime.Today;
        }

        private void ViewSettings()
        {
            for (int i = 0; i < tabPage1.Controls.Count; i++)
            {
                tabPage1.Controls[i].Enabled = false;
            }

            for (int i = 0; i < tabPage2.Controls.Count; i++)
            {
                tabPage2.Controls[i].Enabled = false;
            }

            for (int i = 0; i < tbpAddInfo.Controls.Count; i++)
            {
                tbpAddInfo.Controls[i].Enabled = false;
            }

            //for (int i = 0; i < tabControl1.Controls.Count; i++)
            //{
            //    tabControl1.Controls[i].Enabled = false;
            //}
            btExit.Enabled = true;
            
            btnView.Visible = true;
            btnView.Enabled = true;
            btnView.Location = button3.Location;

            btAdd.Visible = false;
            button3.Visible = false;
            button2.Visible = false;
            button4.Visible = false;

            btAddCon.Visible = false;
            btDelCon.Visible = false;
            groupBox9.Enabled = true;
            dgCon.Enabled = true;

            btAddBank.Visible = btDelBank.Visible = btEditBank.Visible = false;
        }

        private void FillCb()
        {
            DataTable dtCbToo = _proc.FillCbToo();

            for (int i = 0; i <= dtCbToo.Rows.Count - 1; i++)
                cbTypeOrg.Items.Add(dtCbToo.Rows[i][0].ToString());

            basList = _proc.FillCbBas();

            DataRow all = basList.NewRow();
            all["cName"] = "";
            all["id"] = 0;
            basList.Rows.InsertAt(all, 0);

            cbBasment.DataSource = basList;
            cbBasment.DisplayMember = "cName";
            cbBasment.ValueMember = "id";
            if (rezhim=="add")
                cbBasment.SelectedValue = -1;
            else
                cbBasment.SelectedIndex = 0;
            if (!tenant)
            {
              DataTable dtObj = _proc.GetObjects();
              dtObj.DefaultView.RowFilter = "isActive = 1";
              dtObj.DefaultView.Sort = "cName";
              cmbObject.DataSource = dtObj;
              if (rezhim == "add")
                cmbObject.SelectedValue = -1;
            }
            tbnumbas.Enabled = false;
            dtbase.Enabled = false;
        }

        private void ini(int id)
        {
            dtSettings = new DataTable();
            dtSettings = _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "", "");

            int SRZA_days = 30;
            if (dtSettings != null)
            {
                for (int i = 0; dtSettings.Rows.Count > i; i++)
                {
                    if (dtSettings.DefaultView[i]["id_value"].ToString() == "SRZA")
                    {
                        SRZA_days = int.Parse(dtSettings.DefaultView[i]["value"].ToString());
                    }
                }
            }

            CurDate = _proc.getdate();
            SRZA = CurDate.AddDays(SRZA_days);

            DocById = _proc.GetDocById(id);
            dbs.DataSource = DocById;
            dgdoc.DataSource = dbs;


        }

        private void PredPostRefresh()
        {
            DataTable dtPosts = _proc.GetArendaPosts();
            if (dtPosts != null)
            {
                dtPosts.Rows.Cast<DataRow>().Where(r => bool.Parse(r["isActive"].ToString()) == false).ToList().ForEach(r => r.Delete());
                PredPost.DataSource = dtPosts;
                PredPost.ValueMember = "id";
                PredPost.DisplayMember = "cName";
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            //if (tabControl1.SelectedTab == tbpAddInfo)
            //{
            //    SaveAddInfo();
            //}
            //else
            //{

            if (!ValidateBanks()) return;

            if (tenant)
            {
                if (cpTen != null && cpTen.Rows.Count > 0)
                {
                    DataTable dtp = _proc.CheckParentChildTenant(Convert.ToInt32(cpTen.Rows[0]["id"].ToString()),
                      1);
                    if (dtp != null && dtp.Rows.Count > 0)
                    {
                        MessageBox.Show("Арендатор " + dtp.Rows[0]["CurrentTenant"].ToString() + " уже имеет связь в\nкачестве ребенка с арендатором " + dtp.Rows[0]["ConTenant"].ToString() + ".\n                         Добавление арендатора для связи\n                                   невозможно.",
                          "Добавление связи арендаторов", MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
                        return;
                    }
                    DataTable dtc = _proc.CheckParentChildTenant(Convert.ToInt32(cpTen.Rows[0]["id"].ToString()),
                      0);
                    if (dtc != null && dtc.Rows.Count > 0)
                    {
                        MessageBox.Show("Арендатор " + dtc.Rows[0]["CurrentTenant"].ToString() + " уже имеет связь в\nкачестве родителя с арендатором " + dtc.Rows[0]["ConTenant"].ToString() + ".\n                         Добавление арендатора для связи\n                                   невозможно.",
                          "Добавление связи арендаторов", MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (tbScanD.Text.Trim().Length > 0 && !tenant)
            {
                if (!Directory.Exists(tbScanD.Text))
                {
                    MessageBox.Show("Введенный путь к шаблонам\n  документов арендодателя\n    недоступен для чтения.\n  Сохранение невозможно.",
                      "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbScanD.Focus();
                    return;
                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(tbScanD.Text);
                    DirectorySecurity ds = di.GetAccessControl();
                    var rules = ds.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
                    foreach (FileSystemAccessRule rule in rules)
                    {
                        if (rule.FileSystemRights == FileSystemRights.Read && rule.AccessControlType == AccessControlType.Deny)
                        {
                            MessageBox.Show("Введенный путь к шаблонам\n  документов арендодателя\n    недоступен для чтения.\n  Сохранение невозможно.",
                              "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tbScanD.Focus();
                            return;
                        }
                    }
                }
            }

            if (tbOGRN.Text.Length > 0 && tbOGRN.Text.Length != 13 && tbOGRN.Text.Length != 15)
            {
                MessageBox.Show("Длина ОГРН должна быть 13 или 15 символов!",
                        "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtORGNIP.Text.Length > 0 && txtORGNIP.Text.Length != 13 && txtORGNIP.Text.Length != 15)
            {
                MessageBox.Show("Длина ОГРНИП должна быть 13 или 15 символов!",
                    "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Zа-яА-Я][\w\.-]*[a-zA-Z0-9а-яА-Я]@[a-zA-Z0-9а-яА-Я][\w\.-]*[a-zA-Z0-9а-яА-Я]\.[a-zA-Zа-яА-Я][a-zA-Zа-яА-Я\.]*[a-zA-Zа-яА-Я]$");

            if (tbEmail.Text.Length > 0)
            {
                if (!rEMail.IsMatch(tbEmail.Text))
                {
                    MessageBox.Show("Введенный адрес электронной почты\n\r          не соответствует шаблону.\n\r           Сохранение невозможно.",
                      "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //tbEmail.SelectAll();
                    return;
                }
            }

            string msg = "";

            msg += (cbTypeOrg.Text.Trim().Length == 0 ? "Тип\n" : "")
                    + (orgName.Text.Trim().Length == 0 ? "Название\n" : "")
                    + (adress.Text.Trim().Length == 0 ? "Адрес\n" : "")
                    + (tbInn.Text.Trim().Length == 0 ? "ИНН\n" : "")
                //+ (PredPost.Text.Trim().Length == 0 ? "Должность представителя\n" : "")
                + (!tenant && cmbObject.SelectedIndex == -1 ? "Объект\n" : "")
                    + (PredPost.SelectedIndex == -1 ? "Должность представителя\n" : "")
                    + (FamR.Text.Trim().Length == 0 ? "Фамилия представителя в род. п." : "");

            if (msg.Trim().Length != 0)
            {
                MessageBox.Show("Для сохранения необходимо заполнить следующие поля:\n" + msg, "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if ((tbInn.Text.Trim().Length == 10)
                    || (tbInn.Text.Trim().Length == 12))
                {
                    add();
                }
                else
                {
                    MessageBox.Show("ИНН должен быть 10 или 12 символов.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            //}
        }

        private void SaveAddInfo(int new_id)
        {
            _proc.SaveTenantAddInfo(new_id, txtLastName.Text, txtName.Text, txtSecondName.Text, txtDepartment.Text, dtpDateIssue.Value.Date, txtPassport.Text, txtIssued.Text, txtAddress.Text, rbW.Checked, tenant ? txtORGNIP.Text : tbOGRN.Text);
            //MessageBox.Show("Данные сохранены!");
            //this.DialogResult = DialogResult.OK;
        }

        private void SaveCon(int new_id)
        {
          if (id != 0 & (cpTen == null || cpTen.Rows.Count == 0))
            _proc.SetParentChildTenant(id, id);
          else if(cpTen != null && cpTen.Rows.Count > 0)
            foreach(DataRow r in cpTen.Rows)
            {
              if (r["id_parent"] == DBNull.Value || int.Parse(r["id_parent"].ToString()) == 0)
                r["id_parent"] = new_id;
              _proc.SetParentChildTenant(int.Parse(r["id_parent"].ToString()), int.Parse(r["id"].ToString()));
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab == this.tabPage3)
            { DialogResult = DialogResult.Cancel; } 
            else 
            if (BeforeSave())
            { DialogResult = DialogResult.Cancel; }
            else if (rezhim != "view")
            {
                if (MessageBox.Show("Есть несохраненные данные.\nВыйти без сохранения?", "Выход", MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                { DialogResult = DialogResult.Cancel; }
            }
            else
            { DialogResult = DialogResult.Cancel; }
        }

        private void telrab_KeyPress(object sender, KeyPressEventArgs e)
        {
            Lockchar(e);
        }

        private static void Lockchar(KeyPressEventArgs e)
        {
            Regex pat = new Regex(@"[\b]|[0-9]|[-()]");
            bool b = pat.IsMatch(e.KeyChar.ToString());
            if (b == false)
            {
                e.Handled = true;
            }
        }

        private static void LockcharNum(KeyPressEventArgs e)
        {//тут
            Regex pat = new Regex(@"[0-9]");
            bool b = pat.IsMatch(e.KeyChar.ToString());
            if (b == false && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void telhome_KeyPress(object sender, KeyPressEventArgs e)
        {
            Lockchar(e);
        }

        private void telsot_KeyPress(object sender, KeyPressEventArgs e)
        {
            Lockchar(e);
        }

        private void Fam_KeyPress(object sender, KeyPressEventArgs e)
        {
            lockNumb(e);
        }

        private static void lockNumb(KeyPressEventArgs e)
        {
            Regex pat = new Regex(@"[\b]|[а-яА-Я]|[\s]");
            bool b = pat.IsMatch(e.KeyChar.ToString());
            if (b == false)
            {
                e.Handled = true;
            }
        }

        private void FamR_KeyPress(object sender, KeyPressEventArgs e)
        {
            lockNumb(e);
        }

        private void pName_KeyPress(object sender, KeyPressEventArgs e)
        {
            lockNumb(e);
        }

        private void pOtcest_KeyPress(object sender, KeyPressEventArgs e)
        {
            lockNumb(e);
        }

        private void cbTypeOrg_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = cbTypeOrg.Text + " " + orgName.Text; 
        }

        private void orgName_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = cbTypeOrg.Text + " " + orgName.Text; 
        }

        private void PredPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            cheakName = true;
        }

        private void CheakName()
        {
            if (cheakName)
            {
                string mess = "";

                if (PredPost.SelectedIndex != -1)
                {
                    if (Fam.Text == "") { mess += "Фамилия, "; }                    
                    if (pName.Text == "") { mess += "Имя, "; }
                    if (pOtcest.Text == "") { mess += "Отчество, "; }

                    if (mess != "")
                    {

                        if (MessageBox.Show("Не заполнены данные о представителе: " + mess + " \nПродолжить сохранение?", "Внимание", MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        { AddRecord(); }
                        else { }
                    }
                    else { AddRecord(); }

                }
                else
                {
                    MessageBox.Show("Укажите должность представителя. \nСохранить данную запись невозможно.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else { AddRecord(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var b = new Banks(1);
          var b = new frmBanks(1);
            b.ShowDialog();
            bankevich = dataBank.id;
            tbBank.Text = dataBank.cName; 
            tbAc.Text =  dataBank.cA ;
            tbBIK.Text=  dataBank.BIK;
        }


        private void add()
        {

            if (cName == orgName.Text && inn == tbInn.Text && type == cbTypeOrg.Text)
            {
                CheakName();
            }
            else  
                if (_proc.CheakLT(orgName.Text, tbInn.Text , cbTypeOrg.Text , tbAdrTrade.Text).Rows.Count == 0)
            {
                CheakName();
            }
            else { MessageBox.Show("Организация с таким Наименованием и ИНН уже существует"); }

        }

        private void AddRecord()
        {
            int? id_basement;
            DateTime? dreg;
            DateTime? Date_basement;
            string numberBase;

            if (maskedtbdatareg.MaskCompleted)
                dreg = Convert.ToDateTime(maskedtbdatareg.Text);
            else dreg = null;


            if ((cbBasment.SelectedValue != null)
                && (cbBasment.SelectedIndex != 0))
            {
                id_basement = int.Parse(cbBasment.SelectedValue.ToString());
            }
            else
            {
                id_basement = null;
            }

            if (tbnumbas.Enabled == false)
            {
                numberBase = null;
            }
            else
            {
                if (tbnumbas.Text == "")
                    numberBase = null;
                else
                    numberBase = tbnumbas.Text;
            }

            if (dtbase.Enabled == false)
            {
                Date_basement = null;
            }
            else
            {
                Date_basement = Convert.ToDateTime(dtbase.Value);
            }



            int new_id = _proc.addedintLT(id,
                             cbTypeOrg.Text,
                             orgName.Text,
                             pName.Text,
                             pOtcest.Text,
                             Fam.Text,
                             FamR.Text,
                             radioButton1.Checked ? 0 : 1,
                             telrab.Text,
                             telhome.Text,
                             telsot.Text,
                             adress.Text,
                             0,//bankevich,
                             "",//tbRk.Text,
                             tborpo.Text,
                             tbKpp.Text,
                             tbInn.Text,
                             id_basement,
                             tbKem.Text,
                             dreg,
                             tbNreg.Text,
                             tbNsvid.Text,
                             tbSer.Text,
                             tbPny.Text,
                             tbNchet.Text,
                             tbSerP.Text,
                             checkBox1.Checked,
                             tenant ? 1 : 0,
                             remark.Text,
                             rezhim == "add" ? 1 : 0,
                             numberBase,
                             Date_basement,
                             (PredPost.SelectedValue == null ? 0 : int.Parse(PredPost.SelectedValue.ToString())),
                             tbAdrTrade.Text,
                             chbShowInReport.Checked,
                             (cmbObject.SelectedValue == null ? 0 : int.Parse(cmbObject.SelectedValue.ToString())),
                             tbScanD.Text,
                             tbEmail.Text,
                             tbFactAdress.Text.Trim(),
                             tenant);


            SaveBanks(new_id);

            if (id == 0)
            {
                Logging.StartFirstLevel(1398);

                Logging.Comment("ID: " + new_id);
           
                #region "Данные вкладки 'Основная информация'"
                Logging.Comment("Данные вкладки 'Основная информация'");
                //Logging.Comment("Тип огранизации ID: " + cbTypeOrg.SelectedValue + " ; Наименование: " + cbTypeOrg.Text);
                Logging.Comment("Тип огранизации Наименование: " + cbTypeOrg.Text);
                Logging.Comment("Название организации: " + orgName.Text);
                Logging.Comment("Должности представителя ID: " + PredPost.SelectedValue + " ; Наименование: " + PredPost.Text);
                Logging.Comment("Фамилия представителя: " + Fam.Text);
                Logging.Comment("Фамилия представителя в родительском падеже: " + FamR.Text);
                Logging.Comment("Имя представителя: " + pName.Text);
                Logging.Comment("Отчество  представителя: " + pOtcest.Text);
                Logging.Comment("Пол представителя: " + (radioButton1.Checked ? "Ж" : "М"));

                Logging.Comment("Номер рабочего телефона: " + telrab.Text);
                Logging.Comment("Номер домашнего телефона: " + telhome.Text);
                Logging.Comment("Номер сотового телефона: " + telsot.Text);

                Logging.Comment("Адрес сдаваемого помещения: " + tbAdrTrade.Text);
                Logging.Comment("Адрес: " + adress.Text);

                Logging.Comment("Выводить арендатора в отчет по оплатам: " + (chbShowInReport.Checked ? "Да" : "Нет"));
                Logging.Comment("НДС: " + (checkBox1.Checked ? "Да" : "Нет"));

                Logging.Comment("Примечание: " + remark.Text);
                #endregion

                #region "Данные вкладки 'Реквизиты'"
                Logging.Comment("Данные вкладки 'Реквизиты'");
                Logging.Comment("Банк ID: " + bankevich + " ; Наименование: " + tbBank.Text);
                Logging.Comment("К/С банка: " + tbAc.Text);
                Logging.Comment("БИК  банка: " + tbBIK.Text);
                Logging.Comment("Р/С банка: " + tbRk.Text);
                Logging.Comment("ОКПО банка: " + tborpo.Text);
                Logging.Comment("КПП банка: " + tbKpp.Text);
                Logging.Comment("ИНН организации: " + tbInn.Text);
              if(!tenant)
                Logging.Comment("ОРГН: " + tbOGRN.Text);

                if (id_basement != null)
                {
                    Logging.Comment("Основания документа заключения договора ID: " + cbBasment.SelectedValue + " ; Наименование: " + cbBasment.Text);
                    if (tbnumbas.Enabled)
                        Logging.Comment("№: " + tbnumbas.Text);
                    if (dtbase.Enabled)
                        Logging.Comment("От: " + dtbase.Value.ToShortDateString());                                    
                }


                Logging.Comment("Регистрация «Кем»: " + tbKem.Text);
                Logging.Comment("Дата регистрации: " + maskedtbdatareg.Text);
                Logging.Comment("№ регистрации: " + tbNreg.Text);
                Logging.Comment("№ свидительства: " + tbNsvid.Text);
                Logging.Comment("Серия регистрации: " + tbSer.Text);
                Logging.Comment("Постановка на учет «Кем»: " + tbPny.Text);
                Logging.Comment("№ учета: " + tbNchet.Text);
                Logging.Comment("Серия постановки на учет: " + tbSerP.Text);

                #endregion

                #region "Данные вкладки 'Дополнительно'"
                if (tenant)
                {
                    Logging.Comment("Паспортные данные");

                    Logging.Comment("Фамилия: " + txtLastName.Text);
                    Logging.Comment("Имя: " + txtName.Text);
                    Logging.Comment("Отчетство: " + txtSecondName.Text);
                    Logging.Comment("Код подразделения: " + txtDepartment.Text);
                    Logging.Comment("Дата выдачи: " + dtpDateIssue.Value.ToShortDateString());
                    Logging.Comment("Серия и номер паспорта: " + txtPassport.Text);
                    Logging.Comment("Выдан: " + txtIssued.Text);
                    Logging.Comment("Адрес: " + txtAddress.Text);
                    Logging.Comment("«Пол сотрудника: " + (rbM.Checked ? "М" : "Ж"));
                    Logging.Comment("ОРГНИП: " + txtORGNIP.Text);
                }
                #endregion

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }
            else
            {
                Logging.StartFirstLevel(1399);

                Logging.Comment("ID: " + new_id);

                #region "Данные вкладки 'Основная информация'"
                Logging.Comment("Данные вкладки 'Основная информация'");
                //Logging.VariableChange("Тип огранизации ID: ", cbTypeOrg.SelectedValue, id_type);
                Logging.VariableChange("Тип огранизации Наименование: ", cbTypeOrg.Text, type);

                Logging.VariableChange("Название организации: ", orgName.Text, cName);
                Logging.VariableChange("Должности представителя ID: " , PredPost.SelectedValue, id_predPost);
                Logging.VariableChange("Должности представителя Наименование: " , PredPost.Text, _PredPost);
                Logging.VariableChange("Фамилия представителя: " , Fam.Text, fam);
                Logging.VariableChange("Фамилия представителя в родительском падеже: ", FamR.Text, famR);
                Logging.VariableChange("Имя представителя: " , pName.Text, name);
                Logging.VariableChange("Отчество  представителя: " , pOtcest.Text, otc);
                Logging.VariableChange("Пол представителя: ", (radioButton1.Checked ? "Ж" : "М"), (sex == 0 ? "Ж" : "М"));

                Logging.VariableChange("Номер рабочего телефона: ", telrab.Text, wphone);
                Logging.VariableChange("Номер домашнего телефона: ", telhome.Text, hphone);
                Logging.VariableChange("Номер сотового телефона: ", telsot.Text, mphone);

                Logging.VariableChange("Адрес сдаваемого помещения: " , tbAdrTrade.Text, _tbAdrTrade);
                Logging.VariableChange("Адрес: " , adress.Text, _adress);

                Logging.VariableChange("Выводить арендатора в отчет по оплатам: ", (chbShowInReport.Checked ? "Да" : "Нет"), (_chbShowInReport == 1 ? "Да" : "Нет"));
                Logging.VariableChange("НДС: " , (checkBox1.Checked ? "Да" : "Нет"), (nds == 1 ? "Да" : "Нет"));

                Logging.VariableChange("Примечание: ", remark.Text, _remark);
                #endregion

                #region "Данные вкладки 'Реквизиты'"
                Logging.Comment("Данные вкладки 'Реквизиты'");
                Logging.VariableChange("Банк ID: ", bankevich, _bankevich);
                Logging.VariableChange("Банк Наименование: ", tbBank.Text, _tbBank);
                Logging.VariableChange("К/С банка: " , tbAc.Text, _tbAc);
                Logging.VariableChange("БИК  банка: " , tbBIK.Text, _tbBIK);
                Logging.VariableChange("Р/С банка: " , tbRk.Text, pa);
                Logging.VariableChange("ОКПО банка: " , tborpo.Text, okpo);
                Logging.VariableChange("КПП банка: " , tbKpp.Text, kpp);
                Logging.VariableChange("ИНН организации: " , tbInn.Text, inn);

                if (id_basement != null)
                {
                    Logging.VariableChange("Основания документа заключения договора ID: " , cbBasment.SelectedValue, id_basment);
                    Logging.VariableChange("Основания документа заключения договора Наименование: " , cbBasment.Text, _cbBasment);
                    if (tbnumbas.Enabled)
                        Logging.VariableChange("№: " , tbnumbas.Text, _tbnumbas);
                    if (dtbase.Enabled)
                        Logging.VariableChange("От: " , dtbase.Value.ToShortDateString(), _dtbase);
                }


                Logging.VariableChange("Регистрация «Кем»: " , tbKem.Text, WiS);
                Logging.VariableChange("Дата регистрации: " , maskedtbdatareg.Text, dateReg);
                Logging.VariableChange("№ регистрации: " , tbNreg.Text, regNum);
                Logging.VariableChange("№ свидительства: " , tbNsvid.Text, numCert);
                Logging.VariableChange("Серия регистрации: " , tbSer.Text, serCer);
                Logging.VariableChange("Постановка на учет «Кем»: " , tbPny.Text, WPON);
                Logging.VariableChange("№ учета: " , tbNchet.Text, numAcc);
                Logging.VariableChange("Серия постановки на учет: " , tbSerP.Text, serAcc);

                #endregion

                #region "Данные вкладки 'Дополнительно'"
                if (tenant)
                {
                    Logging.Comment("Паспортные данные");

                    Logging.VariableChange("Фамилия: ", txtLastName.Text, _tbLastname);
                    Logging.VariableChange("Имя: ", txtName.Text, _tbName);
                    Logging.VariableChange("Отчетство: ", txtSecondName.Text, _tbSecondname);
                    Logging.VariableChange("Код подразделения: ", txtDepartment.Text, _tbDepartment);
                    Logging.VariableChange("Дата выдачи: ", dtpDateIssue.Value.ToShortDateString(), _tbDateIssue);
                    Logging.VariableChange("Серия и номер паспорта: ", txtPassport.Text, _tbPassport);
                    Logging.VariableChange("Выдан: ", txtIssued.Text, _tbIssued);
                    Logging.VariableChange("Адрес: ", txtAddress.Text, _txtAddress);
                    Logging.VariableChange("«Пол сотрудника: ", (rbM.Checked ? "М" : "Ж"), (!_sex ? "М" : "Ж"));
                    Logging.VariableChange("ОРГНИП: ", txtORGNIP.Text, _tbOrgnip);
                }
                #endregion

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }

            SaveAddInfo(new_id);

            if (tenant)
              SaveCon(new_id);

            MessageBox.Show("Внесены изменения");
            DialogResult = DialogResult.Cancel;
        }

        private bool BeforeSave()
        {
            int rb2 = radioButton2.Checked ? 1 : 0;
            int chb1 = checkBox1.Checked ? 1 : 0;
            string cbb = (cbBasment.SelectedValue == null || (int)cbBasment.SelectedValue == 0) ? "" : cbBasment.Text;
            if (            //tabPage1 Основная информация            
                (type != cbTypeOrg.Text)
                || (cName != orgName.Text)
                || (_PredPost != PredPost.Text)
                || (fam != Fam.Text)
                || (famR != FamR.Text)
                || (name != pName.Text)
                || (otc != pOtcest.Text)
                || (sex != rb2)
                || (wphone != telrab.Text)
                || (hphone != telhome.Text)
                || (mphone != telsot.Text)
                || (_tbAdrTrade != tbAdrTrade.Text)
                || (_adress != adress.Text)
                || (nds != chb1)
                || (_remark != remark.Text)
                || (!tenant && cmbObject.SelectedValue == null || !tenant && id_obj != int.Parse(cmbObject.SelectedValue.ToString()))
                || (!tenant && tbScanD.Text != path)
                || (_email != tbEmail.Text)
                //tabPage2 Реквизиты
                || (_textBox1 != textBox1.Text)
                || (_tbBank != tbBank.Text)
                || (_tbAc != tbAc.Text)
                || (_tbBIK != tbBIK.Text)            
                || (pa != tbRk.Text)
                || (okpo != tborpo.Text)
                || (kpp != tbKpp.Text)
                || (inn != tbInn.Text)
                || (_tbOGRN != tbOGRN.Text)
                || (_cbBasment != cbb)
                || (_tbnumbas != tbnumbas.Text)
                || (_dtbase != dtbase.Value.Date)
                || (WiS != tbKem.Text)
                || (dateReg != maskedtbdatareg.Text)
                || (regNum != tbNreg.Text)
                || (numCert != tbNsvid.Text)
                || (serCer != tbSer.Text)
                || (WPON != tbPny.Text)
                || (numAcc != tbNchet.Text)
                || (serAcc != tbSerP.Text)  
                // Дополнительно
                || (_tbLastname != txtLastName.Text)
                || (_tbName != txtName.Text)
                || (_tbSecondname != txtSecondName.Text)
                || (_tbDepartment != txtDepartment.Text)
                || (_tbDateIssue != dtpDateIssue.Value.Date.ToShortDateString())
                || (_tbPassport != txtPassport.Text)
                || (_tbIssued != txtIssued.Text)
                || (_sex != rbW.Checked)
                || (_tbOrgnip != txtORGNIP.Text)
                )
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            GetTab();
        }
        private void GetTab()
        {
            tb = new TabPage();
            tb = this.tabControl1.SelectedTab;
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (this.tabControl1.SelectedTab == this.tabPage3)
            {
                if (dgdoc.RowCount == 0)
                {
                    button2.Enabled = false;
                    button4.Enabled = false;
                    btPrint.Enabled = false;
                    btnView.Enabled = false;
                }
                else
                {
                    button2.Enabled = true;
                    button4.Enabled = true;
                    btPrint.Enabled = true;
                    btnView.Enabled = true;
                }

                if (rezhim != "view")
                {
                    if (BeforeSave())
                    {
                    }
                    else
                    {
                        if (MessageBox.Show("На вкладке есть несохраненные данные. Выйти из вкладки без сохранения?", "Выход из вкладки", MessageBoxButtons.YesNo,
                                                       MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            e.Cancel = true;
                            SetDefaultSettings();
                        }

                        {
                            this.tabControl1.SelectedTab = tb;
                        }

                    }
                }
            }
        }

        private void SetDefaultSettings()
        {
            //tabPage1 Основная информация            
            cbTypeOrg.Text = type;
            orgName.Text = cName;
            PredPost.Text = _PredPost;
            Fam.Text = fam;
            FamR.Text = famR;
            pName.Text = name;
            pOtcest.Text = otc;
            radioButton2.Checked = (sex==1 ? true : false);
            telrab.Text = wphone;
            telhome.Text = hphone;
            telsot.Text = mphone;
            tbAdrTrade.Text = _tbAdrTrade;
            adress.Text = _adress;
            checkBox1.Checked = (nds==1 ? true : false);
            remark.Text = _remark;
            if (!tenant)
            {
              cmbObject.SelectedValue = id_obj;
              tbScanD.Text = path;
            }
            //tabPage2 Реквизиты
            textBox1.Text = _textBox1;
            tbBank.Text = _tbBank;
            tbAc.Text = _tbAc;
            tbBIK.Text = _tbBIK;
            tbRk.Text = pa;
            tborpo.Text = okpo;
            tbKpp.Text = kpp;
            tbInn.Text = inn;
            cbBasment.Text = _cbBasment;
            tbnumbas.Text = _tbnumbas;
            dtbase.Value = _dtbase.Date;
            tbKem.Text = WiS;
            maskedtbdatareg.Text = dateReg;
            tbNreg.Text = regNum;
            tbNsvid.Text = numCert;
            tbSer.Text = serCer;
            tbPny.Text = WPON;
            tbNchet.Text = numAcc;
            tbSerP.Text = serAcc;
        }

        private void maskedtbdatareg_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (Convert.ToDateTime(maskedtbdatareg.Text) < Convert.ToDateTime("1 / 1 / 1753"))
                {
                    MessageBox.Show("Должно находиться в пределах от 1/1/1753 12:00:00 AM и 12/31/9999 11:59:59 PM.");
                    e.Cancel = true;

                }
            }
            catch (Exception) { MessageBox.Show("Проверьте правильность ввода даты\nДД/ММ/ГГГГ","Внимание"); }
        }

        private void tborpo_KeyPress(object sender, KeyPressEventArgs e)
        {
            LockcharNum(e);
        }

        private void tbKpp_KeyPress(object sender, KeyPressEventArgs e)
        {
            LockcharNum(e);
        }

        private void tbInn_KeyPress(object sender, KeyPressEventArgs e)
        {
            LockcharNum(e);
        }

        private void tbNreg_KeyPress(object sender, KeyPressEventArgs e)
        {
            Lockchar(e);
        }

        private void tbNsvid_KeyPress(object sender, KeyPressEventArgs e)
        {
            Lockchar(e);
        }

        private void tbSer_KeyPress(object sender, KeyPressEventArgs e)
        {
            Lockchar(e);
        }

        private void tbNchet_KeyPress(object sender, KeyPressEventArgs e)
        {
            Lockchar(e);
        }

        private void tbSerP_KeyPress(object sender, KeyPressEventArgs e)
        {
            Lockchar(e);
        }
      
      private void button2_Click(object sender, EventArgs e)
      {
        int _id = Convert.ToInt32(dgdoc.SelectedRows[0].Cells["iddoc"].Value);

        DataTable dt = _proc.GetLD(_id);
        if (dt.Rows.Count == 0)
        {
          MessageBox.Show("Редактирование договора невозможно. Договор удален.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
          var editDoc = new AddeditDoc(_id, false,false);
          editDoc.ShowDialog();
        }
        ini(id);
      }

        private void button3_Click(object sender, EventArgs e)
        {
            var doc = new AddeditDoc(id, cbTypeOrg.Text +" "+ orgName.Text);
            doc.ShowDialog();
            ini(id);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgdoc.CurrentCell != null)
            {
                delDoc();
                ini(id);
            }
        }
      
      private void delDoc()
      {
        if (MessageBox.Show("Удалить запись? \n" + dgdoc.SelectedRows[0].Cells[1].Value.ToString() + " " + dgdoc.SelectedRows[0].Cells[2].Value.ToString() + " ", "Внимание", MessageBoxButtons.YesNo,
          MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
        {
          int _id = Convert.ToInt32(dgdoc.SelectedRows[0].Cells["iddoc"].Value);

          DataTable dt = _proc.GetLD(_id);
          if (dt.Rows.Count == 0)
          {
            MessageBox.Show("Удаление договора невозможно. Договор уже удален.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }

          DataTable dtPayments = new DataTable();
          dtPayments = _proc.GetPayments(_id);

          if (dtPayments.Rows.Count > 0)
          {
            MessageBox.Show("По договору есть оплаты. Удаление документа невозможно.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }

          Logging.StartFirstLevel(533);
          Logging.Comment("ID: " + _id);
          Logging.Comment("Дата документа: " + dgdoc.SelectedRows[0].Cells["date"].Value.ToString());
          Logging.Comment("Арендатель ID: " + dgdoc.SelectedRows[0].Cells["id_lanlord"].Value.ToString()+ " ; Наименование: "+ dgdoc.SelectedRows[0].Cells["aren"].Value.ToString());
          Logging.Comment("№ договора: " + dgdoc.SelectedRows[0].Cells["number"].Value.ToString());
          Logging.Comment("Начало: " + dgdoc.SelectedRows[0].Cells["begin"].Value.ToString());
          Logging.Comment("Конец: " + dgdoc.SelectedRows[0].Cells["end"].Value.ToString());
          Logging.Comment("Место: " + dgdoc.SelectedRows[0].Cells["place"].Value.ToString());

          Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
            + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
          Logging.StopFirstLevel();

          _proc.DelDTL(_id, "listdoc");
        }
      }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            FilterDataView();
        }

        private void FilterDataView()
        {
            try
            {
                string Fstring, Fstring1, Fstring2;
                if (tbLord.Text == "")
                { Fstring = "*"; }
                else Fstring = tbLord.Text;
                if (tbNumber.Text == "")
                { Fstring1 = "*"; }
                else Fstring1 = tbNumber.Text;
                if (tbPlace.Text == "")
                { Fstring2 = "*"; }
                else Fstring2 = tbPlace.Text;
                DataView view = new DataView();
                DataTable dt = DocById;
                view = dt.DefaultView;
                StringBuilder sb = new StringBuilder();
                sb.Append("id_lord like '%" + Fstring + "%'");
                sb.Append(" and № like '%" + Fstring1 + "%'");
                sb.Append(" and Место like '%" + Fstring2 + "%'");
                view.RowFilter = sb.ToString();
            }
            catch (Exception) { }

        }

        private void tbRk_KeyPress(object sender, KeyPressEventArgs e)
        {
            LockcharNum(e);
        }

        private void tbnumbas_KeyPress(object sender, KeyPressEventArgs e)
        {
            Lockchar(e);
        }

        private void dgdoc_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dgdoc.RowCount == 0)
            {
                button2.Enabled = false;
                button4.Enabled = false;
                btPrint.Enabled = false;
                btnView.Enabled = false;
            }
            else 
            {
                if (rezhim != "view")
                {
                    button2.Enabled = true;
                    button4.Enabled = true;
                }
                btPrint.Enabled = true;
                btnView.Enabled = true;
            }
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            /*PrintForm f = new PrintForm(int.Parse(dgdoc.CurrentRow.Cells["iddoc"].Value.ToString()),
                    dgdoc.CurrentRow.Cells["number"].Value.ToString().Trim(),
                    int.Parse(dgdoc.CurrentRow.Cells["cId_Type"].Value.ToString()));*/
            ArendaPrint.frmPrint f = new ArendaPrint.frmPrint(int.Parse(dgdoc.CurrentRow.Cells["iddoc"].Value.ToString()),
                    dgdoc.CurrentRow.Cells["number"].Value.ToString().Trim(),
                    int.Parse(dgdoc.CurrentRow.Cells["cId_Type"].Value.ToString()));
            
            f.setData(dgdoc.SelectedRows[0].Cells["date"].Value.ToString(), dgdoc.SelectedRows[0].Cells["number"].Value.ToString(), dgdoc.SelectedRows[0].Cells["aren"].Value.ToString(),
                dgdoc.SelectedRows[0].Cells["place"].Value.ToString(), dgdoc.SelectedRows[0].Cells["begin"].Value.ToString(), dgdoc.SelectedRows[0].Cells["end"].Value.ToString(), dgdoc.SelectedRows[0].Cells["id_lanlord"].Value.ToString());

            f.ShowDialog();            
        }

        private void cbBasment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if ((basList.Rows[cbBasment.SelectedIndex]["needDate"].ToString() == "False")
                    || (cbBasment.SelectedIndex==0))
                {
                    tbnumbas.Enabled = false;
                    dtbase.Enabled = false;
                }
                else
                {
                    tbnumbas.Enabled = true;
                    dtbase.Enabled = true;
                }

                if (cbBasment.Text == "Свидетельства")
                {
                    tbnumbas.Text = orgnip;
                    tbnumbas.Enabled = false;
                }
            }
            catch (Exception) { }
        }

        private void AddEditTenant_Shown(object sender, EventArgs e)
        {            
            if (!shown)
            GetDefaultValues();
            shown = true;
        }

        private void btAddBank_Click(object sender, EventArgs e)
        {
            new Bank.frmAddBank() { Owner = this, isEdit = false, Text = "Добавить банк" }.ShowDialog();
        }

        private void btEditBank_Click(object sender, EventArgs e)
        {
            if (dgvBank.CurrentRow != null && dgvBank.CurrentRow.Index != -1 && dtBanks != null && dtBanks.DefaultView.Count != 0)
            {
                new Bank.frmAddBank() { Owner = this, isEdit = true, Text = "Редактировать банк" }.ShowDialog();
            }
        }

        private void dgdoc_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (DateTime.Parse(DocById.DefaultView[e.RowIndex]["Конец"].ToString()) < CurDate)
            {
                dgdoc.Rows[e.RowIndex].DefaultCellStyle.BackColor = picConEnded.BackColor;
            }
            else
            {
                if (DateTime.Parse(DocById.DefaultView[e.RowIndex]["Конец"].ToString()) <= SRZA)
                {
                    dgdoc.Rows[e.RowIndex].DefaultCellStyle.BackColor = picConEnding.BackColor;
                }
                else
                {
                    dgdoc.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void AddEditTenant_Load(object sender, EventArgs e)
        {
            chbShowInReport.Text = "Выводить "
                + (tenant ? "арендатора" : "арендодателя")
                + " в отчет по оплатам";

            GetBanks();

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = _proc.GetLD(int.Parse(dgdoc.CurrentRow.Cells["iddoc"].Value.ToString()));
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Открытие документа невозможно. Документ удален.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var editDoc = new AddeditDoc(int.Parse(dgdoc.CurrentRow.Cells["iddoc"].Value.ToString()), true,false);
                editDoc.ShowDialog();
            }
        }

        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == ' '))
            {
                e.Handled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
          var fd = new FolderBrowser2 { };//new FolderBrowserDialog { };
          if (fd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
          {
            if (fd.DirectoryPath.Trim().Length == 0)
            {
              return;
            }
            tbScanD.Text = fd.DirectoryPath.Trim();
          }
        }

        private void btDelBank_Click(object sender, EventArgs e)
        {
            if (dgvBank.CurrentRow != null && dgvBank.CurrentRow.Index != -1 && dtBanks != null && dtBanks.DefaultView.Count != 0)
            {
                DataRowView row = dtBanks.DefaultView[dgvBank.CurrentRow.Index];
                if (!(bool)row["isActive"])
                {
                    if (DialogResult.Yes == MessageBox.Show("Сделать выбранную запись действующей?", "Восстановление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        DataTable dtResult = _proc.AddLandlordTenantBank((int)row["id"], (int)row["id_Bank"], (string)row["PaymentAccount"], id, true, false);
                        if (dtResult != null && dtResult.Rows.Count > 0 && (int)dtResult.Rows[0]["id"] > 0)
                        {
                            row["isActive"] = true;
                            dtBanks.AcceptChanges();
                        }
                    }
                }
                else
                {
                    if (DialogResult.Yes == MessageBox.Show("Удалить выбранную запись?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        if ((int)row["id"] < 0)
                        {
                            row.Delete();
                            dtBanks.AcceptChanges();
                            return;
                        }


                        DataTable dtResult = _proc.AddLandlordTenantBank((int)row["id"], (int)row["id_Bank"], (string)row["PaymentAccount"], id, true, true);

                        if (dtResult == null || dtResult.Rows.Count == 0) return;

                        if ((int)dtResult.Rows[0]["id"] == -1)
                        {
                            if (DialogResult.Yes == MessageBox.Show(TempData.centralText("Выбранная для удаления запись используется в программе.\nСделать запись недействующей?\n"), "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                            {
                                dtResult = _proc.AddLandlordTenantBank((int)row["id"], (int)row["id_Bank"], (string)row["PaymentAccount"], id, false, false);
                                if (dtResult != null && dtResult.Rows.Count > 0 && (int)dtResult.Rows[0]["id"] > 0)
                                {
                                    row["isActive"] = false;
                                    dtBanks.AcceptChanges();
                                }
                            }
                        }
                        else
                        {
                            row.Delete();
                            dtBanks.AcceptChanges();
                        }
                    }
                }
            }
        }

        private void tbNumber_TextChanged(object sender, EventArgs e)
        {
          FilterDataView();
        }

        private void tbPlace_TextChanged(object sender, EventArgs e)
        {
          FilterDataView();
        }

        private void dgvBank_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1 && dtBanks != null && dtBanks.DefaultView.Count != 0)
            {
                Color rColor = Color.White;
                if (!(bool)dtBanks.DefaultView[e.RowIndex]["isActive"])
                    rColor = panel1.BackColor;
                dgvBank.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                dgvBank.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
                dgvBank.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;

            }
        }

        private void dgvBank_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            //Рисуем рамку для выделеной строки
            if (dgv.Rows[e.RowIndex].Selected)
            {
                int width = dgv.Width;
                Rectangle r = dgv.GetRowDisplayRectangle(e.RowIndex, false);
                Rectangle rect = new Rectangle(r.X, r.Y, width - 1, r.Height - 1);

                ControlPaint.DrawBorder(e.Graphics, rect,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid);
            }
        }

        private void dgvBank_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBank.CurrentRow == null || dgvBank.CurrentRow.Index == -1 || dtBanks == null || dtBanks.DefaultView.Count == 0 || dgvBank.CurrentRow.Index >= dtBanks.DefaultView.Count)
            {
                btDelBank.Enabled = false;
                btEditBank.Enabled = false;
                return;
            }

            btDelBank.Enabled = true;
            btEditBank.Enabled = (bool)dtBanks.DefaultView[dgvBank.CurrentRow.Index]["isActive"];
        }

        private void GetBanks()
        {
            dgvBank.AutoGenerateColumns = false;
            dtBanks = _proc.GetLandlordTenantBank(id);
            setFilter();
            dgvBank.DataSource = dtBanks;
        }

        private void setFilter()
        {
            if (dtBanks == null || dtBanks.Rows.Count == 0)
            {
                btEditBank.Enabled = btDelBank.Enabled = false;
                return;
            }

            try
            {
                string filter = "";

                if (tbNumber.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"cName like '%{tbNumber.Text.Trim()}%'";

                if (!cbBankNotActive.Checked)
                    filter += (filter.Length == 0 ? "" : " and ") + $"isActive = 1";

                dtBanks.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtBanks.DefaultView.RowFilter = "id = -9999999999999";
            }
            finally
            {
                btEditBank.Enabled = btDelBank.Enabled =
               dtBanks.DefaultView.Count != 0;
                dgvBank_SelectionChanged(null, null);
            }
        }

        private void cbBankNotActive_Click(object sender, EventArgs e)
        {
            setFilter();
        }

        private void btAddCon_Click(object sender, EventArgs e)
        {
          var lookten = new Looktenant(1, id);
          lookten.ShowDialog();
          if ((dataTen.aren != "") && (dataTen.id != 0))
          {
            if (cpTen == null)
            {
              cpTen = new DataTable();
              cpTen.Columns.Add("id");
              cpTen.Columns.Add("id_parent");
              cpTen.Columns.Add("id_child");
              cpTen.Columns.Add("cName");
              cpTen.Columns.Add("num");
            }
            if(cpTen.Rows.Count == 0)
            {
              DataRow r = cpTen.Rows.Add();
              r["id"] = dataTen.id;
              r["id_parent"] = id;
              r["cName"] = dataTen.aren;
              r["num"] = 1;
            }
            else
            {
              //DataRow r = cpTen.Rows.Add();
              cpTen.Rows[0]["id"] = dataTen.id;
              cpTen.Rows[0]["id_parent"] = id;
              cpTen.Rows[0]["cName"] = dataTen.aren;
              cpTen.Rows[0]["num"] = 1;
            }
            dgCon.AutoGenerateColumns = false;
            dgCon.DataSource = cpTen;
          }
        }

        private void btDelCon_Click(object sender, EventArgs e)
        {
          if (MessageBox.Show("Удалить выбранную запись?\n", "Удаление записи", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
          {
            int idchild = 0;
            int idparent = 0;
            string find = "num = "
              + dgCon.SelectedRows[0].Cells[0].Value.ToString() + " and cName = \'"
              + dgCon.SelectedRows[0].Cells[1].Value.ToString() + "\'";
            DataRow[] r = cpTen.Select(find);
            if (r[0]["id_child"] != DBNull.Value)
            {
              idchild = int.Parse(r[0]["id_child"].ToString());
              string f = "id_child = " + r[0]["id"].ToString();
              DataRow[] tr = cpTen.Select(f);
              if (tr != null & tr.Length > 0)
                tr[0]["id_child"] = 0;
            }
            if (r[0]["id_parent"] != DBNull.Value)
            {
              idparent = int.Parse(r[0]["id_parent"].ToString());
              string f = "id_parent = " + r[0]["id"].ToString();
              DataRow[] tr = cpTen.Select(f);
              if (tr != null & tr.Length > 0)
                tr[0]["id_parent"] = 0;
            }
            cpTen.Rows.Remove(r[0]);
            while (idchild != 0)
            {
              string tmpfind = "id_parent = " + idchild.ToString();
              DataRow[] tmpr = cpTen.Select(tmpfind);
              if (tmpr.Length > 0 && tmpr[0]["id_child"] != DBNull.Value)
                idchild = (int)tmpr[0]["id_child"];
              else
                idchild = 0;
              if(tmpr.Length > 0)
                cpTen.Rows.Remove(tmpr[0]);
            }
            while (idparent != 0)
            {
              string tmpfind = "id_child = " + idparent.ToString();
              DataRow[] tmpr = cpTen.Select(tmpfind);
              if (tmpr.Length > 0 && tmpr[0]["id_parent"] != DBNull.Value)
                idparent = int.Parse(tmpr[0]["id_parent"].ToString());
              else
                idparent = 0;
              if(tmpr.Length > 0)
                cpTen.Rows.Remove(tmpr[0]);
            }
          }
        }

        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {/*
          System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");

          if (tbEmail.Text.Length > 0)
          {
            if (!rEMail.IsMatch(tbEmail.Text))
            {
              MessageBox.Show("Некорректный Email!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

              tbEmail.SelectAll();

              e.Cancel = true;
            }
          }*/
        }

        private void dgCon_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
          if (rezhim == "view" && dgCon != null && dgCon.SelectedRows != null
            && dgCon.SelectedRows.Count > 0)
          {
            string find = "num = "
              + dgCon.SelectedRows[0].Cells[0].Value.ToString() + " and cName = \'"
              + dgCon.SelectedRows[0].Cells[1].Value.ToString() + "\'";
            DataRow[] r = cpTen.Select(find);
            id = (int)r[0]["id"];
            GetData();
            ViewSettings();
          }
        }

        private void dgCon_SelectionChanged(object sender, EventArgs e)
        {
          if (dgCon != null && dgCon.SelectedRows != null && dgCon.SelectedRows.Count > 0)
          {
            string find = "num = "
              + dgCon.SelectedRows[0].Cells[0].Value.ToString() + " and cName = \'"
              + dgCon.SelectedRows[0].Cells[1].Value.ToString() + "\'";
            DataRow[] r = cpTen.Select(find);
            if (r.Length > 0
              && ((r[0]["id_parent"] != DBNull.Value && int.Parse(r[0]["id_parent"].ToString()) == id)
              || (r[0]["id_child"] != DBNull.Value && int.Parse(r[0]["id_child"].ToString()) == id)) && cp)
              btDelCon.Enabled = true;
          }
        }

        string tmptxt;

        private void tbScanD_KeyPress(object sender, KeyPressEventArgs e)
        {
          tmptxt = tbScanD.Text;
          if (e.KeyChar == '\b' || (!char.IsControl(e.KeyChar) && e.KeyChar != 'v'))
            e.Handled = true;
        }

        private void tbScanD_TextChanged(object sender, EventArgs e)
        {
          if(tbScanD.Text == tmptxt + "v")
            tbScanD.Text = tmptxt;
        }

        private void dgdoc_Paint(object sender, PaintEventArgs e)
        {
          int X = dgdoc.Location.X + dgdoc.Columns["cObj"].Width
            + dgdoc.Columns["cType"].Width
            + dgdoc.Columns["date"].Width;
          tbLord.Location = new Point(X, tbLord.Location.Y);
          tbLord.Width = dgdoc.Columns["aren"].Width;
          X += tbLord.Width;
          tbNumber.Location = new Point(X, tbNumber.Location.Y);
          tbNumber.Width = dgdoc.Columns["number"].Width;
          X += tbNumber.Width + dgdoc.Columns["begin"].Width + dgdoc.Columns["end"].Width;
          tbPlace.Location = new Point(X, tbPlace.Location.Y);
          tbPlace.Width = dgdoc.Columns["place"].Width;
        }

        private void tbLord_KeyPress(object sender, KeyPressEventArgs e)
        {
          if (e.KeyChar == '%')
            e.Handled = true;
        }

        private void tbNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
          if (e.KeyChar == '%')
            e.Handled = true;
        }

        private void tbPlace_KeyPress(object sender, KeyPressEventArgs e)
        {
          if (e.KeyChar == '%')
            e.Handled = true;
        }

        private void tbScanD_KeyDown(object sender, KeyEventArgs e)
        {
          if (e.KeyData == Keys.Delete)
            e.Handled = true;
        }

        private void tbOGRN_KeyPress(object sender, KeyPressEventArgs e)
        {
          Regex pat = new Regex(@"[\b]|[0-9]");
          bool b = pat.IsMatch(e.KeyChar.ToString());
          if (b == false)
          {
            e.Handled = true;
          }
        }


        private DataTable dtBanks;

        public bool validateBankRow(int id,int idBank, string RS,bool withTable=false)
        {

            DataTable dtResult = _proc.ValidateLandlordTenantBank(id, idBank, RS);

            if (dtResult == null) return false;
            if(dtResult.Rows.Count>0) return false;

            if (withTable)
            {
                EnumerableRowCollection<DataRow> rowCollect = dtBanks.AsEnumerable().Where(r => r.Field<int>("id_Bank") == idBank && r.Field<string>("PaymentAccount").Equals(RS) && r.Field<int>("id") != id);
                if (rowCollect.Count() > 0) return false;
            }


            return true;
        }

        public void addRowBank(int idBank, string name, string BIK, string KS, string RS)
        {
            object tmpMin = dtBanks.Compute("MIN(id)", "");
            if (tmpMin == null || tmpMin == DBNull.Value) tmpMin = -1;
            else if ((int)tmpMin > 0) tmpMin = -1; else tmpMin = (int)tmpMin - 1;


            DataRow row = dtBanks.NewRow();
            row["id"] = (int)tmpMin;
            row["id_Bank"] = idBank;
            row["PaymentAccount"] = RS;
            row["cName"] = name;
            row["BIC"] = BIK;
            row["CorrespondentAccount"] = KS;
            row["isActive"] = true;

            dtBanks.Rows.Add(row);
        }

        public void updateRowBank(int idBank, string name, string BIK, string KS, string RS)
        {
            DataRowView row = dtBanks.DefaultView[dgvBank.CurrentRow.Index];
            
            row["id_Bank"] = idBank;
            row["PaymentAccount"] = RS;
            row["cName"] = name;
            row["BIC"] = BIK;
            row["CorrespondentAccount"] = KS;

            dtBanks.AcceptChanges();
        }

        public DataRowView GetBankRow()
        {
            return dtBanks.DefaultView[dgvBank.CurrentRow.Index];
        }

        private bool ValidateBanks()
        {           
            if (dtBanks.Rows.Count == 0)
            {
                MessageBox.Show(TempData.centralText("Нет банков, необходимы банки\n"), "Проверка на дубли", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (tenant) {
                if (dtBanks.Rows.Count >1)
                {
                    MessageBox.Show(TempData.centralText("Банк должен быть только 1!\n"), "Проверка на дубли", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            foreach (DataRow row in dtBanks.Rows)
            {
                if (!validateBankRow((int)row["id"], (int)row["id_Bank"], (string)row["PaymentAccount"]))
                {
                    MessageBox.Show(TempData.centralText("В справочнике уже присутствует\nзапись с введёнными реквизитами.\n"), "Проверка на дубли", MessageBoxButtons.OK, MessageBoxIcon.Warning);         
                    return false;
                }
            }

            return true;
        }

        private void SaveBanks(int id_LandLord)
        {
            foreach (DataRow row in dtBanks.Rows)
            {
                _proc.AddLandlordTenantBank((int)row["id"], (int)row["id_Bank"], (string)row["PaymentAccount"], id_LandLord, (bool)row["isActive"], false);
            }
        }
    }
}
