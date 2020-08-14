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
using Nwuram.Framework.Settings.User;

namespace Arenda
{
    public partial class AddeditDoc : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        string lr, zdan, floo, sec, tp, num, s, st, phone, cbm, ar, rekl, reklNum,
          reklLen, reklWid, _tbFailCommen, _telrab, _telhome, _telsot, cadNum,
          typeDog, obj;
        string errortext = "";
        string errorload = "";
        int _id = 0, id_ten, id_lord, pay;
        int id_build, id_floor, id_sec, id_type, _old_id_ten, _old_id_lord, _id_zdan,
          _id_floo, _id_sec, _id_tp, _id_td, _idObj,_id_ReklamPlace;
        string rezhim, remark, oldTen, oldLord;
        bool floorFill;
        bool click, reklamm, nal;
        DataTable TypeDoc, dtTP;
        DateTime strdate, stdate, oldDoc;
        DataTable dtSections;
        DataTable dtFloor;
        DataTable dtZdan;
        DataTable Rec;
        DataTable conf;
        DataTable dtDiscount;
        string defaultVal = "0.00";
        string format = "{0:### ### ##0.00}";
        string defaultValInt = "0";
        string formatInt = "{0:### ### ##0}";
        bool IsView;

        private bool isConfirmed;

        //public bool isCopyDoc { set; private get; }
        private bool isCopyDoc;

        public AddeditDoc()
        {
            InitializeComponent();
            dgAddDoc.AutoGenerateColumns = false;
            init_SavePayment();
            init_TypeActivitis();
            AddLoad();
        }

        public AddeditDoc(int id, string p)
        {
            InitializeComponent();
            dgAddDoc.AutoGenerateColumns = false;
            init_SavePayment();
            init_TypeActivitis();
            AddLoad();

            id_ten = id;
            tbTen.Text = p;
            button1.Enabled = false;
            //btAdd.Enabled = false;
            this.Text += " для " + p;
        }

        public AddeditDoc(int id, bool prosmotr, bool isConfirmed,bool isCopyDoc = false)
        {
            IsView = prosmotr;
            this.isConfirmed = isConfirmed;
            this.isCopyDoc = isCopyDoc;
            InitializeComponent();
            dgAddDoc.AutoGenerateColumns = false;
            init_SavePayment();
            init_TypeActivitis();
            if (!prosmotr)
            {
                rezhim = "edit";
                this.Text = "Редактирование документа";
                cmbTypeDog.Enabled = false;
                //if (isConfirmed) rezhim = "view";
            }
            else
            {
                rezhim = "view";
                this.Text = "Просмотр документа";
            }
            EditLoad(id);
            getDiscount();


            if (isCopyDoc)
            {
                _id = 0;
                rezhim = "add";
                this.Text = "Копирование документа";
                DopSoglButtons();
                doc.Value = DateTime.Now;
                startdate.Value = DateTime.Now;
                stopdate.Value = DateTime.Now.AddMonths(GetSRKA());
                tbnumd.Text = "";

                if (dtDiscount != null)
                    foreach (DataRow row in dtDiscount.Rows)
                        row["id"] = 0;
            }

            
        }


        private void init_SavePayment()
        {
            DataTable dtSavePayment = _proc.getSavePayment();
            cmbSavePayment.DataSource = dtSavePayment.AsEnumerable().Where(r => new List<int> { 2, 3 }.Contains(r.Field<int>("id"))).CopyToDataTable();
            cmbSavePayment.DisplayMember = "cName";
            cmbSavePayment.ValueMember = "id";
            cmbSavePayment.SelectedIndex = -1;
        }


        private void init_TypeActivitis()
        {
            DataTable dtSavePayment = _proc.getTypeActivities(false);
            cmbTypeActivities.DataSource = dtSavePayment;
            cmbTypeActivities.DisplayMember = "cName";
            cmbTypeActivities.ValueMember = "id";
            cmbTypeActivities.SelectedIndex = -1;
        }


        private void TypeToVisibleElement()
        {
            lReclamaPlace.Visible = cmbReclamaPlace.Visible = lbReklPrice.Visible = txtReklamma.Visible =
                // lblReklNumber.Visible= tbReklNumber.Visible =
                   lblReklSize.Visible = tbReklSize1.Visible
                  = tbReklSize2.Visible = (int)cmbTypeDog.SelectedValue == 2;

            label5.Visible = cbZdan.Visible = (int)cmbTypeDog.SelectedValue != 3;
            lLandPlot.Visible = cbLandPlot.Visible = (int)cmbTypeDog.SelectedValue == 3;

            //label7.Visible = cbSec.Visible = 
            lblS.Visible = tbS.Visible = lbPrice.Visible = tbcbm.Visible = (int)cmbTypeDog.SelectedValue != 2;

            label7.Visible = cbSec.Visible = lTp.Visible = cbTp.Visible = cbFloor.Visible = lFloor.Visible = lblSt.Visible = tbSt.Visible = lblPhone.Visible = tbphone.Visible
              = (int)cmbTypeDog.SelectedValue == 1;
        }

        private void AddLoad()
        {
            Rec = _proc.GetLD(0);
            cbZdan.Enabled = false;
            Fillcb();

            TypeToVisibleElement();
            //lReclamaPlace.Visible = cmbReclamaPlace.Visible = lbReklPrice.Visible = txtReklamma.Visible = lblReklNumber.Visible
            //  = tbReklNumber.Visible = lblReklSize.Visible = tbReklSize1.Visible
            //  = tbReklSize2.Visible = (int)cmbTypeDog.SelectedValue == 2;

            //label7.Visible = cbSec.Visible = lbPrice.Visible = tbcbm.Visible = (int)cmbTypeDog.SelectedValue != 2;

            //lTp.Visible = cbTp.Visible =  cbFloor.Visible = lFloor.Visible = lblSt.Visible = tbSt.Visible = lblPhone.Visible = tbphone.Visible
            //  = (int)cmbTypeDog.SelectedValue == 1;

            rezhim = "add";
            _id = 0;
            radioButton1.Checked = true;
            tbphone.Text = numTextBox.CheckAndChange(tbcbm.Text, 2, 0, 9999999999, false, defaultVal, format);
            tbcbm.Text = numTextBox.CheckAndChange(tbcbm.Text, 2, 0, 9999999999, false, defaultVal, format);
            stopdate.Value = DateTime.Now.AddMonths(GetSRKA());
            this.Text = "Добавление документа";
            DopSoglButtons();

            num = tbnumd.Text;
            strdate = startdate.Value;
            stdate = stopdate.Value;
            cadNum = tbKadNum.Text;
            typeDog = cmbTypeDog.Text;
            zdan = cbZdan.Text;
            floo = cbFloor.Text;
            sec = cbSec.Text;
            tp = cbTp.Text;
            s = tbS.Text;
            st = tbSt.Text;
            phone = tbphone.Text;
            cbm = tbcbm.Text;
            rekl = txtReklamma.Text;
            reklamm = (cmbTypeDog.SelectedValue != null && (int)cmbTypeDog.SelectedValue == 2);
            reklNum = tbReklNumber.Text;
            reklLen = tbReklSize1.Text;
            reklWid = tbReklSize2.Text;
            _tbFailCommen = tbFailComment.Text;
            _old_id_ten = id_ten;
            _old_id_lord = id_lord;
            _id_td = cmbTypeDog.SelectedValue == null ? 0 : (int)cmbTypeDog.SelectedValue;
            _id_zdan = cbZdan.SelectedValue == null ? 0 : (int)cbZdan.SelectedValue;
            _id_floo = cbFloor.SelectedValue == null ? 0 : (int)cbFloor.SelectedValue;
            _id_sec = cbSec.SelectedValue == null ? 0 : (int)cbSec.SelectedValue;
            _id_tp = cbTp.SelectedValue == null ? 0 : (int)cbTp.SelectedValue;
            _telrab = telrab.Text;
            _telhome = telhome.Text;
            _telsot = telsot.Text;

            FormatSumms();
            tbDopS.Visible = false;
            lblDopS.Visible = false;

        }

        private void EditLoad(int id)
        {
            Rec = _proc.GetLD(id);
            _id = id;
            Fillcb();

            num = tbnumd.Text = Rec.Rows[0][3].ToString();
            oldDoc = doc.Value = Convert.ToDateTime(Rec.Rows[0][4].ToString());
            strdate = startdate.Value = Convert.ToDateTime(Rec.Rows[0][5].ToString());
            stdate = stopdate.Value = Convert.ToDateTime(Rec.Rows[0][6].ToString());

            id_ten = Convert.ToInt32(Rec.Rows[0][1].ToString());
            id_lord = Convert.ToInt32(Rec.Rows[0][2].ToString());

            if (Rec.Rows[0]["id_ObjectLease"] != DBNull.Value)
            {
                _idObj = int.Parse(Rec.Rows[0]["id_ObjectLease"].ToString());
                obj = tbObj.Text = Rec.Rows[0]["Obj"].ToString();
            }
            else
                cbZdan.Enabled = false;

            try
            {
                DataTable Ten = _proc.getLT(id_ten);
                telrab.Text = Ten.Rows[0]["Work_phone"].ToString();
                telhome.Text = Ten.Rows[0]["Home_phone"].ToString();
                telsot.Text = Ten.Rows[0]["Mobile_phone"].ToString();
            }
            catch { };

            tbLord.Text = lr = Rec.Rows[0]["Landlord_name"].ToString();
            tbTen.Text = Rec.Rows[0]["Tenant_name"].ToString();

            errorload = "";
            //new
            if (Rec.Rows[0]["id_SavePayment"] != DBNull.Value)
                cmbSavePayment.SelectedValue = Rec.Rows[0]["id_SavePayment"];

            if (Rec.Rows[0]["RentalVacation"] != DBNull.Value)
                tbRentalVacation.Text = Rec.Rows[0]["RentalVacation"].ToString();
            
            if(Rec.Rows[0]["id_TypeActivities"]!=DBNull.Value)
                cmbTypeActivities.SelectedValue = Rec.Rows[0]["id_TypeActivities"];

            //

            DataTable Land_Tenant = new DataTable();
            Land_Tenant = _proc.GetTetant(0);
            if (Land_Tenant.Select("id = " + id_ten).Count() != 0)
            {
                if (Land_Tenant.Select("id = " + id_ten)[0]["isActive"].ToString() == "False")
                {
                    ErrorMessageLoad("\n - неактивный арендатор");
                }
            }
            else
            {
                ErrorMessageLoad("\n - арендатор, не найденный в справочнике");
                tbTen.Text = "";
            }

            Land_Tenant = new DataTable();
            Land_Tenant = _proc.GetLandLord(0);
            if (Land_Tenant.Select("id = " + id_lord).Count() != 0)
            {
                if (Land_Tenant.Select("id = " + id_lord)[0]["isActive"].ToString() == "False")
                {
                    ErrorMessageLoad("\n - неактивный арендодатель");
                }
            }
            else
            {
                ErrorMessageLoad("\n - арендодатель, не найденный в справочнике");
                tbLord.Text = "";
            }

            FillCbZdan(false);

            FillCbFloor(false);

            FillCbSec(false);

            FillCbTP(false);


            


            phone = tbphone.Text = numTextBox.CheckAndChange(Rec.Rows[0]["Phone"].ToString(),
              2, 0, 9999999999, false, defaultVal, format);

            st = tbSt.Text = numTextBox.CheckAndChange(Rec.Rows[0]["Area_of_Trading_Hall"].ToString().Trim(),
              2, 0, 9999999999, false, defaultVal, format);

            ar = tbAr.Text = numTextBox.CheckAndChange(Rec.Rows[0]["Total_Sum"].ToString(),
              2, 0, 9999999999, false, defaultVal, format);

            cbm = tbcbm.Text = numTextBox.CheckAndChange(Rec.Rows[0]["Cost_of_Meter"].ToString(),
              2, 0, 9999999999, false, defaultVal, format);

            rekl = txtReklamma.Text = numTextBox.CheckAndChange(Rec.Rows[0]["Reklama"].ToString(),
              2, 0, 9999999999, false, defaultVal, format);

            reklNum = tbReklNumber.Text = numTextBox.CheckAndChange(Rec.Rows[0]["ReklNumber"].ToString(),
              0, 0, 999999999, false, defaultValInt, formatInt);

            reklLen = tbReklSize1.Text = numTextBox.CheckAndChange(Rec.Rows[0]["ReklLength"].ToString(),
              2, 0, 9999999999, false, defaultVal, format);

            reklWid = tbReklSize2.Text = numTextBox.CheckAndChange(Rec.Rows[0]["ReklWidth"].ToString(),
              2, 0, 9999999999, false, defaultVal, format);

            if ((int)Rec.Rows[0]["id_TypeContract"] == 0)
            {
                if (rezhim != "view")
                    MessageBox.Show("Не выбран тип договора.\n", "Внимание", MessageBoxButtons.OK);
                cmbTypeDog.SelectedIndex = -1;
            }
            else
            {
                if (cmbTypeDog.Items.Count != 0)
                {
                    cmbTypeDog.SelectedValue = int.Parse(Rec.Rows[0]["id_TypeContract"].ToString());
                    if (cmbTypeDog.SelectedValue == null)
                    {
                        //MessageBox.Show("Выбран неактивный тип договора!", "Внимание", MessageBoxButtons.OK);
                        ErrorMessageLoad("\n - неактивный тип договора");
                        DataTable dtTypes = _proc.GetContractTypes();
                        dtTypes.DefaultView.RowFilter = "isActive = 1 OR id = "
                          + Rec.Rows[0]["id_TypeContract"].ToString();
                        dtTypes.DefaultView.Sort = "id";
                        cmbTypeDog.DataSource = dtTypes;
                        cmbTypeDog.SelectedValue = int.Parse(Rec.Rows[0]["id_TypeContract"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Нет ни одного типа договора!\nОткрытие формы невозможно!",
                      "Ошибка", MessageBoxButtons.OK);
                    this.Close();
                }
            }

            #region "Формировнаие рекламного места или земельного участка"
            if ((int)Rec.Rows[0]["id_TypeContract"] == 2)
            {
                GetDataReklamPlaceInfo(id_build);
                cmbReclamaPlace.SelectedValue = (int)Rec.Rows[0]["id_Section"];
                cmbReclamaPlace_SelectionChangeCommitted(null, null);
            }
            else if ((int)Rec.Rows[0]["id_TypeContract"] == 3)
            {
                GetLandPlot();
                if (Rec.Rows[0]["id_Section"] != DBNull.Value)
                    cbLandPlot.SelectedValue = (int)Rec.Rows[0]["id_Section"];
                cbLandPlot_SelectionChangeCommitted(null, null);
            }
            #endregion

            typeDog = cmbTypeDog.Text;
            cadNum = tbKadNum.Text = Rec.Rows[0]["CadastralNumber"] == null ? ""
              : Rec.Rows[0]["CadastralNumber"].ToString();

            //договор рекламы
            if ((cmbTypeDog.SelectedValue != null && (int)cmbTypeDog.SelectedValue == 2))
            {
                s = tbS.Text = numTextBox.CheckAndChange(Rec.Rows[0]["ReklArea"].ToString().Trim(),
                  2, 0, 9999999999, false, defaultVal, format);
            }
            else
            {
                s = tbS.Text = numTextBox.CheckAndChange(Rec.Rows[0]["Total_Area"].ToString().Trim(), 2, 0, 9999999999, false, defaultVal, format);
            }

            //если у договора есть оплаты, запретить менять признак типа договора (договор аренды / договор рекламы)
            DataTable dtPayments = new DataTable();
            dtPayments = _proc.GetPayments(_id);
            if (dtPayments.Rows.Count > 0)
            {
                cmbTypeDog.Enabled = false;
            }

            if (Rec.Rows[0][16].ToString() == "True")
            {
                radioButton1.Checked = true;
                pay = 1;
            }
            else
            {
                radioButton2.Checked = true;
                pay = 0;
            }
            tbremark.Text = Rec.Rows[0]["Remark"].ToString();
            tbFailComment.Text = Rec.Rows[0]["failComment"] == DBNull.Value ? ""
              : Rec.Rows[0]["failComment"].ToString();
            ini(id);

            if (errorload != "")
            {
                MessageBox.Show(errorload, "Внимание!");
            }

            if (rezhim == "view" || (isConfirmed && !isCopyDoc))
            {
                for (int i = 0; i < this.tabPage1.Controls.Count; i++)
                {
                    //if (this.Controls[i] is TabControl) continue;

                    this.tabPage1.Controls[i].Enabled = false;
                }

                for (int i = 0; i < this.tabPage2.Controls.Count; i++)
                {
                    //if (this.Controls[i] is TabControl) continue;

                    this.tabPage2.Controls[i].Enabled = false;
                }

                button4.Visible = false;
                button3.Visible = false;
                btAdd.Visible = false;
                btExit.Enabled = true;
                btAddDoc.Enabled = true;
                groupBox5.Enabled = true;                
                button3.Visible = button4.Visible = isConfirmed;
            }

            FormatSumms();
            oldTen = tbTen.Text;
            oldLord = tbLord.Text;
            s = tbS.Text;
            st = tbSt.Text;
            ar = tbAr.Text;
            cbm = tbcbm.Text;
            reklamm = (cmbTypeDog.SelectedValue != null && (int)cmbTypeDog.SelectedValue == 2);
            nal = radioButton1.Checked;
            rekl = txtReklamma.Text;
            tbDopS.Visible = false;
            lblDopS.Visible = false;
            remark = tbremark.Text;
            reklNum = tbReklNumber.Text;
            reklLen = tbReklSize1.Text;
            reklWid = tbReklSize2.Text;
            _tbFailCommen = tbFailComment.Text;
            _old_id_ten = id_ten;
            _old_id_lord = id_lord;
            _id_td = cmbTypeDog.SelectedValue == null ? 0 : (int)cmbTypeDog.SelectedValue;
            _id_zdan = _id_td != 3 ? (int)cbZdan.SelectedValue : -1;
            _id_floo = _id_td == 1 ? (int)cbFloor.SelectedValue : -1;
            _id_sec = _id_td == 1 ? (int)cbSec.SelectedValue : -1;
            _id_tp = _id_td == 1 ? (int)cbTp.SelectedValue : -1;

            _telrab = telrab.Text;
            _telhome = telhome.Text;
            _telsot = telsot.Text;
            cadNum = tbKadNum.Text;
            
            TypeToVisibleElement();

            //lReclamaPlace.Visible = cmbReclamaPlace.Visible = lbReklPrice.Visible = txtReklamma.Visible = lblReklNumber.Visible
            //  = tbReklNumber.Visible = lblReklSize.Visible = tbReklSize1.Visible
            //  = tbReklSize2.Visible = cmbTypeDog.SelectedValue != null
            //  && (int)cmbTypeDog.SelectedValue == 2;

            //label7.Visible = cbSec.Visible = lbPrice.Visible = tbcbm.Visible = cmbTypeDog.SelectedValue != null
            //  && (int)cmbTypeDog.SelectedValue != 2;

            //lTp.Visible = cbTp.Visible = cbFloor.Visible = lFloor.Visible = lblSt.Visible = tbSt.Visible = lblPhone.Visible = tbphone.Visible
            //  = cmbTypeDog.SelectedValue != null && (int)cmbTypeDog.SelectedValue == 1;
        }

        private void ErrorMessageLoad(string err)
        {
            if (errorload == "")
            {
                errorload += "В записи используется:";
            }
            errorload += err;
        }

        private void DopSoglButtons()
        {
            if (rezhim == "view")
            {
                button4.Enabled = false;
                button3.Enabled = false;
            }
            else
            {
                if (rezhim == "add")
                {
                    button4.Enabled = false;
                    btAddDoc.Visible = false;
                }
                else
                {
                    button4.Enabled = true;
                }

                if (dgAddDoc.Rows.Count == 0)
                {
                    button3.Enabled = false;
                }
                else
                {
                    button3.Enabled = true;
                }
            }
        }

        private void FillZd(int idZd, string name, bool unactive)
        {
            dtZdan = _proc.FillCbZdFl(0);

            if (unactive)
            {
                DataRow dr = dtZdan.NewRow();
                dr["cName"] = name;
                dr["id"] = idZd;
                dr["isActive"] = 1;
                dtZdan.Rows.InsertAt(dr, 0);
                dtZdan.AcceptChanges();
            }

            dtZdan.DefaultView.RowFilter = "isActive = 1";

            cbZdan.DataSource = dtZdan;
            cbZdan.DisplayMember = "cName";
            cbZdan.ValueMember = "id";

            if (idZd == -1) //unactive
                cbZdan.SelectedIndex = -1;
            else
                cbZdan.SelectedValue = idZd;
        }

        private void FillCbTP(bool onClick)
        {
            //if (Rec.Rows.Count != 0 && (int)Rec.Rows[0]["id_TypeContract"] != 1) return;

            //Rec = _proc.GetLD(_id);
            tp = Rec.Rows.Count != 0 && (int)Rec.Rows[0]["id_TypeContract"] ==1 ? Rec.Rows[0]["tofp"].ToString() : "";
            id_type = Rec.Rows.Count != 0 && (int)Rec.Rows[0]["id_TypeContract"] == 1 ? int.Parse(Rec.Rows[0]["id_Type_of_Premises"].ToString()) : -1;

            dtTP = new DataTable();
            dtTP = _proc.FillCbSecTp(0);
            cbTp.DataSource = dtTP;
            cbTp.DisplayMember = "cName";
            cbTp.ValueMember = "id";

            for (int i = 0; i <= dtTP.Rows.Count - 1; i++)
            {
                Label l = new Label();
                l.Font = cbTp.Font;
                l.AutoSize = true;
                l.Text = dtTP.Rows[i]["cName"].ToString();
                l.Visible = false;
                this.Controls.Add(l);
                if (l.Width > cbTp.DropDownWidth)
                    cbTp.DropDownWidth = l.Width;
                this.Controls.Remove(l);
            }

            bool TPIsUnactive;

            if (rezhim != "add")
            {
                if (dtTP.Select("id = " + id_type).Count() == 0)
                {
                    if (!onClick && Rec != null && Rec.Rows.Count > 0 && (int)Rec.Rows[0]["id_TypeContract"] == 1)
                    {
                        ErrorMessageLoad("\n - неактивный тип помещений");
                    }
                    TPIsUnactive = true;
                }
                else
                {
                    TPIsUnactive = false;
                }
            }
            else
            {
                TPIsUnactive = false;
            }
            FillTP(id_type, tp, TPIsUnactive);
        }

        private void FillTP(int idTP, string name, bool unactive)
        {
            dtTP = _proc.FillCbSecTp(0);

            if (unactive)
            {
                DataRow dr = dtTP.NewRow();
                dr["cName"] = name;
                dr["id"] = idTP;
                dtTP.Rows.InsertAt(dr, 0);
                dtTP.AcceptChanges();
            }

            cbTp.DataSource = dtTP;
            cbTp.DisplayMember = "cName";
            cbTp.ValueMember = "id";

            if (idTP == -1) //unactive
                cbTp.SelectedIndex = -1;
            else
                cbTp.SelectedValue = idTP;
        }

        private void cbTp_Click(object sender, EventArgs e)
        {
            click = true;
            int selectedTP = cbTp.SelectedValue != null ? int.Parse(cbTp.SelectedValue.ToString()) : -1;

            FillCbTP(true);

            if (dtTP.Select("id = " + selectedTP).Count() == 0)
            {
                click = false;
                cbTp.SelectedIndex = -1;
            }
            else
            {
                cbTp.SelectedValue = selectedTP;
            }
            click = false;
        }

        private void FillFloor(int idFl, string name, bool unactive)
        {
            dtFloor = _proc.FillCbZdFl(1);

            if (unactive)
            {
                DataRow dr = dtFloor.NewRow();
                dr["cName"] = name;
                dr["id"] = idFl;
                dr["isActive"] = 1;
                dtFloor.Rows.InsertAt(dr, 0);
                dtFloor.AcceptChanges();
            }

            dtFloor.DefaultView.RowFilter = "isActive = 1";

            cbFloor.DataSource = dtFloor;
            cbFloor.DisplayMember = "cName";
            cbFloor.ValueMember = "id";

            if (idFl == -1) //unactive
                cbFloor.SelectedIndex = -1;
            else
                cbFloor.SelectedValue = idFl;
        }

        private void FillSec(int idS, string name, bool unactive)
        {
            dtSections = _proc.FillCbSecTp(1,
                                        (cbZdan.SelectedValue != null ? int.Parse(cbZdan.SelectedValue.ToString()) : 0),
                                        (cbFloor.SelectedValue != null ? int.Parse(cbFloor.SelectedValue.ToString()) : 0),
                                        tbObj.Text != "" ? _idObj : 0);

            if (unactive)
            {
                DataRow dr = dtSections.NewRow();
                dr["cName"] = name;
                dr["id"] = idS;
                dtSections.Rows.InsertAt(dr, 0);
                dtSections.AcceptChanges();
            }

            cbSec.DataSource = dtSections;
            cbSec.DisplayMember = "cName";
            cbSec.ValueMember = "id";

            if (idS == -1) //unactive
                cbSec.SelectedIndex = -1;
            else
                cbSec.SelectedValue = idS;
        }

        private void ini(int id)
        {
            TypeDoc = _proc.GetTD(id);
            bds.DataSource = TypeDoc;
            dgAddDoc.DataSource = bds;
            DopSoglButtons();
        }

        private int GetSRKA()
        {
            int rez = 0;
            conf = new DataTable();
            conf = _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "", "");
            if (conf != null)
            {
                for (int i = 0; conf.Rows.Count > i; i++)
                {
                    if (conf.DefaultView[i]["id_value"].ToString() == "SRKA")
                    {
                        try
                        {
                            rez = int.Parse(conf.DefaultView[i]["value"].ToString());
                        }
                        catch //(Exception ex)
                        {
                            //MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
            return rez;
        }

        private void Fillcb()
        {
            FillCbZdan(false);
            cbZdan.SelectedIndex = -1;

            FillCbFloor(false);
            cbFloor.SelectedIndex = -1;

            FillCbSec(false);
            cbSec.SelectedIndex = -1;

            FillCbTP(false);
            cbTp.SelectedIndex = -1;

            FillCmbTypeDog();
            cmbTypeDog.SelectedIndex = 0;
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            if (rezhim != "view")
            {
                if (num != tbnumd.Text || strdate.Date != startdate.Value.Date
                  || stdate.Date != stopdate.Value.Date
                  || ((int)cmbTypeDog.SelectedValue != 3 && (zdan != cbZdan.Text
                  || floo != cbFloor.Text || sec != cbSec.Text || tp != cbTp.Text))
                  || s != tbS.Text || st != tbSt.Text || phone != tbphone.Text
                  || cbm != tbcbm.Text || rekl != txtReklamma.Text
                  || reklamm != (cmbTypeDog.SelectedValue != null
                  && (int)cmbTypeDog.SelectedValue == 2)
                  || reklNum != tbReklNumber.Text || reklLen != tbReklSize1.Text
                  || reklWid != tbReklSize2.Text || cadNum != tbKadNum.Text
                  || typeDog != cmbTypeDog.Text)
                {
                    if (MessageBox.Show("Есть несохраненные данные. Выйти без сохранения?", "Внимание", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        this.Close();
                    }
                    else { }
                }
                else
                    this.Close();
            }
            else
                this.Close();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            CalculateArenda();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var lookten = new Looktenant(1, 0);
            lookten.ShowDialog();
            if ((dataTen.aren != "") && (dataTen.id != 0))
            {
                tbTen.Text = dataTen.aren;
                id_ten = dataTen.id;
                try
                {
                    DataTable Ten = _proc.getLT(id_ten);
                    telrab.Text = Ten.Rows[0]["Work_phone"].ToString();
                    telhome.Text = Ten.Rows[0]["Home_phone"].ToString();
                    telsot.Text = Ten.Rows[0]["Mobile_phone"].ToString();
                }
                catch { };
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var lookten = new Looktenant(0, 0);
            lookten.ShowDialog();
            if ((dataTen.aren != "") && (dataTen.id != 0))
            {
                tbLord.Text = dataTen.aren.Substring(0, dataTen.aren.IndexOf('/'));
                id_lord = dataTen.id;
                if (dataTen.id_Obj != null)
                {
                    obj = tbObj.Text = dataTen.aren.Substring(dataTen.aren.IndexOf('/') + 1);
                    _idObj = dataTen.id_Obj;
                    tbKadNum.Text = dataTen.CadastralNumber;
                    if (cbZdan.SelectedValue != null)
                        GetDataReklamPlaceInfo((int)cbZdan.SelectedValue);

                    GetLandPlot();
                }
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (cmbTypeDog.SelectedValue == null)
            {
                MessageBox.Show("Не выбран тип договора.\nСохранение невозможно.",
                  "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbTypeActivities.SelectedValue == null)
            {
                MessageBox.Show(TempData.centralText($"Не выбран:\n{label6.Text.Replace(":","")}.\nСохранение невозможно.\n"),
                  "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbTypeActivities.Focus();
                return;
            }

            /*if ((int)cmbTypeDog.SelectedValue == 3 && tbKadNum.Text.Length == 0)
            {
              MessageBox.Show("Не введен кадастровый номер.\nСохранение невозможно.",
                "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
              return;
            }*/

            DataTable dt = _proc.CheckDogNum(_id, tbnumd.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                MessageBox.Show(" В списке договоров уже\n   присутствует договор\n       с таким номером.\nСохранение невозможно.",
                  "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (AllDataFilled())
            {
                try
                {                                                                                                                                                                                                                                                                                 //(cbBasment.SelectedValue == null ? 0 : int.Parse(cbBasment.SelectedValue.ToString()))
                    pay = (radioButton1.Checked) ? 1 : 0;

                    if (SquareChanged() && (int)cmbTypeDog.SelectedValue != 3)
                    {
                        if (MessageBox.Show("Внести изменения в справочник секций?", "Внимание", MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            _proc.AddEditSec("",
                                             "",
                                             "",
                                             3,
                                             int.Parse(cbSec.SelectedValue.ToString()),
                                             1,
                                             0,
                                             0,
                                             "",
                                             (tbS.Text != ""
                                                    ? decimal.Parse(numTextBox.ConvertToCompPunctuation(tbS.Text))
                                                    : 0),
                                             (tbSt.Text != ""
                                                    ? decimal.Parse(numTextBox.ConvertToCompPunctuation(tbSt.Text))
                                                    : 0));

                            MessageBox.Show("Изменения в справочник секций внесены", "Внимание");
                        }
                    }

                    int RentalVacation = int.Parse(tbRentalVacation.Text);
                    int? id_SavePayment = null;
                    if (cmbSavePayment.SelectedIndex != -1)
                        id_SavePayment = (int)cmbSavePayment.SelectedValue;


                    DataTable dtTmp =  _proc.getInfoUsedSection(_id, startdate.Value.Date, stopdate.Value.Date,
                        (int)cmbTypeDog.SelectedValue == 3 ? (int)cbLandPlot.SelectedValue
                                : (int)cmbTypeDog.SelectedValue == 1 ? (int)cbSec.SelectedValue
                                : (int)cmbReclamaPlace.SelectedValue, (int)cmbTypeDog.SelectedValue);

                    if (dtTmp != null && dtTmp.Rows.Count > 0)
                    {
                        if ((int)dtTmp.Rows[0]["id"] == 1)
                        {
                            MessageBox.Show($"Данная секция\\Рекламное место\\Земельный участок занят!", "Инфрмирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    DataTable SaveResult = new DataTable();

                    SaveResult = _proc.AddEditLD(_id,
                            id_ten,
                            id_lord,
                            tbnumd.Text,
                            Convert.ToDateTime(doc.Text),
                            Convert.ToDateTime(startdate.Text),
                            Convert.ToDateTime(stopdate.Text),
                            (int)cmbTypeDog.SelectedValue == 3 ? null : (int?)cbZdan.SelectedValue,
                            (int)cmbTypeDog.SelectedValue == 3 ? null : (int?)cbFloor.SelectedValue,
                            (int)cmbTypeDog.SelectedValue == 3 ? (int)cbLandPlot.SelectedValue
                                : (int)cmbTypeDog.SelectedValue == 1? (int)cbSec.SelectedValue
                                : (int)cmbReclamaPlace.SelectedValue,

                            (int)cmbTypeDog.SelectedValue == 3 ? null : (int?)cbTp.SelectedValue,
                            //если реклама, передаем 0
                            (int)cmbTypeDog.SelectedValue == 2 ? 0 : Convert.ToDecimal(numTextBox.ConvertToCompPunctuation(tbS.Text)),
                            (int)cmbTypeDog.SelectedValue == 2 ? 0 : Convert.ToDecimal(numTextBox.ConvertToCompPunctuation(tbSt.Text)),
                            (int)cmbTypeDog.SelectedValue == 2 ? 0 : Convert.ToDecimal(numTextBox.ConvertToCompPunctuation(tbcbm.Text)),
                            (int)cmbTypeDog.SelectedValue == 2 ? 0 : Convert.ToDecimal(numTextBox.ConvertToCompPunctuation(tbphone.Text)),
                            (int)cmbTypeDog.SelectedValue == 2 ? Convert.ToDecimal(numTextBox.ConvertToCompPunctuation(txtReklamma.Text)) : Convert.ToDecimal(numTextBox.ConvertToCompPunctuation(tbAr.Text)),
                            pay,
                            tbremark.Text,
                            //если не реклама, передаем 0
                            (int)cmbTypeDog.SelectedValue != 2 ? 0 : Convert.ToDecimal(numTextBox.ConvertToCompPunctuation(txtReklamma.Text)),
                            (int)cmbTypeDog.SelectedValue != 2 ? 0 : Convert.ToDecimal(numTextBox.ConvertToCompPunctuation(tbReklSize1.Text)),
                            (int)cmbTypeDog.SelectedValue != 2 ? 0 : Convert.ToDecimal(numTextBox.ConvertToCompPunctuation(tbReklSize2.Text)),
                            (int)cmbTypeDog.SelectedValue != 2 ? 0 : Convert.ToDecimal(numTextBox.ConvertToCompPunctuation(tbS.Text)),
                            0,//(int)cmbTypeDog.SelectedValue != 2 ? 0 : Convert.ToInt32(numTextBox.ConvertToCompPunctuation(tbReklNumber.Text)),
                            tbFailComment.Text,
                            (int)cmbTypeDog.SelectedValue,
                            tbKadNum.Text,
                            _idObj,
                            RentalVacation,
                            id_SavePayment,
                            (int)cmbTypeActivities.SelectedValue
                            );

                    if ((SaveResult != null) && (SaveResult.Rows.Count > 0))
                    {
                        _id = int.Parse(SaveResult.Rows[0]["id_Agreement"].ToString());
                    }
                    else
                    {
                        MessageBox.Show("Ошибка сохранения");
                        return;
                    }

                    if (rezhim != "add")
                    {
                        Logging.StartFirstLevel(532);

                        string operation = "Редактирование договора";

                        Logging.Comment(operation);
                        Logging.Comment("");
                        Logging.Comment("id договора = " + _id.ToString());

                        Logging.VariableChange("Арендатор ID: ", id_ten, _old_id_ten);
                        Logging.VariableChange("Арендатор Наименование: ", tbTen.Text, oldTen);

                        Logging.VariableChange("Арендодатель ID: ", id_lord, _old_id_lord);
                        Logging.VariableChange("Арендодатель Наименование: ", tbLord.Text, oldLord);

                        Logging.VariableChange("Номер договора: ", tbnumd.Text, num);
                        Logging.VariableChange("Дата заключения договора: ", doc.Value.ToShortDateString(), oldDoc.ToShortDateString());
                        Logging.VariableChange("Срок аренды: ",
                            "с " + startdate.Value.ToShortDateString() + " по " + stopdate.Value.ToShortDateString(),
                            "с " + strdate.ToShortDateString() + " по " + stdate.ToShortDateString());

                        if ((int)cmbTypeDog.SelectedValue != 3)
                        {
                            Logging.VariableChange("Здание ID: ", cbZdan.SelectedValue, _id_zdan);
                            Logging.VariableChange("Здание: ", cbZdan.Text, zdan);

                            if (cbFloor.SelectedValue != null)
                            {
                                Logging.VariableChange("Этаж ID: ", cbFloor.SelectedValue, _id_floo);
                                Logging.VariableChange("Этаж Наименование: ", cbFloor.Text, floo);
                            }

                            if (cbSec.SelectedValue != null)
                            {
                                Logging.VariableChange("Номер секции ID: ", cbSec.SelectedValue, _id_sec);
                                Logging.VariableChange("Номер секции Наименование: ", cbSec.Text, sec);
                            }

                            if (cbTp.SelectedValue != null)
                            {
                                Logging.VariableChange("Тип помещения ID: ", cbTp.SelectedValue, _id_tp);
                                Logging.VariableChange("Тип помещения Наименование: ", cbTp.Text, tp);
                            }
                        }



                        Logging.VariableChange("Общ.площадь: ", tbS.Text, s);
                        Logging.VariableChange("Аренда: ", tbAr.Text, ar);

                        Logging.VariableChange("Тип договора: ",
                            cmbTypeDog.SelectedText, typeDog);

                        Logging.VariableChange("Номер рабочего телефона: ", telrab.Text, _telrab);
                        Logging.VariableChange("Номер домашнего телефона: ", telhome.Text, _telhome);
                        Logging.VariableChange("Номер сотового телефона: ", telsot.Text, _telsot);

                        if (cmbTypeDog.Enabled)
                        {
                            if ((int)cmbTypeDog.SelectedValue == 2)
                            {
                                Logging.VariableChange("Номер места: ", tbReklNumber.Text, reklNum);
                                Logging.VariableChange("Сумма оплаты: ", txtReklamma.Text, rekl);
                                Logging.VariableChange("Размер места (ширина): ", tbReklSize1.Text, reklLen);
                                Logging.VariableChange("Размер места (высота): ", tbReklSize2.Text, reklWid);
                            }
                            else
                            {
                                Logging.VariableChange("Телефон: ", tbphone.Text, phone);
                                Logging.VariableChange("Торг. зал: ", tbSt.Text, st);
                            }
                        }
                        else
                        {
                            if ((int)cmbTypeDog.SelectedValue == 2)
                            {
                                Logging.VariableChange("Номер места: ", tbReklNumber.Text, reklNum);
                                Logging.VariableChange("Сумма оплаты: ", txtReklamma.Text, rekl);
                                Logging.VariableChange("Размер места (ширина): ", tbReklSize1.Text, reklLen);
                                Logging.VariableChange("Размер места (высота): ", tbReklSize2.Text, reklWid);
                            }
                            else
                            {
                                Logging.VariableChange("Телефон: ", tbphone.Text, phone);
                                Logging.VariableChange("Общ.площадь: ", tbS.Text, s);
                                Logging.VariableChange("Торг. зал: ", tbSt.Text, st);
                            }
                        }

                        Logging.VariableChange("Стоимость 1 кв.м.: ", tbcbm.Text, cbm);
                        //ar,cbm,rekl 
                        Logging.VariableChange("Тип оплаты: ",
                            (radioButton1.Checked ? "Нал" : "Безнал"),
                            (nal ? "Нал" : "Безнал"));
                        Logging.VariableChange("Замечание/примечание: ", tbFailComment.Text, _tbFailCommen);
                        Logging.VariableChange("Примечание: ", tbremark.Text, remark);
                        Logging.Comment("");
                        Logging.Comment("Завершение операции \"" + operation + "\"");
                        Logging.StopFirstLevel();
                    }
                    else
                    {
                        Logging.StartFirstLevel(531);

                        string operation = "Добавление договора";

                        Logging.Comment(operation);
                        Logging.Comment("");
                        Logging.Comment("id договора = " + _id.ToString());
                        Logging.Comment("Арендатор ID: " + id_ten + "; Наименование: " + tbTen.Text);
                        Logging.Comment("Арендодатель ID: " + id_lord + "; Наименование: " + tbLord.Text);
                        Logging.Comment("Номер договора: " + tbnumd.Text);
                        Logging.Comment("Дата заключения договора: " + doc.Value.ToShortDateString());
                        Logging.Comment("Срок аренды: с " + startdate.Value.ToShortDateString() + " по " + stopdate.Value.ToShortDateString());
                        if ((int)cmbTypeDog.SelectedValue != 3)
                        {
                            Logging.Comment("Здание ID: " + cbFloor.SelectedValue + "; Наименование: " + cbZdan.Text);
                            Logging.Comment("Этаж ID: " + cbFloor.SelectedValue + "; Наименование: " + cbFloor.Text);
                            
                            if ((int)cmbTypeDog.SelectedValue == 1)
                                Logging.Comment("Номер секции ID: " + cbSec.SelectedValue + "; Наименование: " + cbSec.Text);
                            else if ((int)cmbTypeDog.SelectedValue == 2)
                                Logging.Comment("Рекламное место ID: " + cmbReclamaPlace.SelectedValue + "; Наименование: " + cmbReclamaPlace.Text);

                            Logging.Comment("Тип помещения ID: " + cbTp.SelectedValue + "; Наименование: " + cbTp.Text);
                        }
                        Logging.Comment("Аренда: " + tbAr.Text);
                        Logging.Comment("Тип договора: "
                            + cmbTypeDog.SelectedText);


                        Logging.Comment("Номер рабочего телефона: " + telrab.Text);
                        Logging.Comment("Номер домашнего телефона: " + telhome.Text);
                        Logging.Comment("Номер сотового телефона: " + telsot.Text);

                        if ((int)cmbTypeDog.SelectedValue == 2)
                        {
                            Logging.Comment("Номер места: " + tbReklNumber.Text);
                            Logging.Comment("Сумма оплаты: " + txtReklamma.Text);
                            Logging.Comment("Размер места (ширина): " + tbReklSize1.Text);
                            Logging.Comment("Размер места (высота): " + tbReklSize2.Text);
                        }
                        else
                        {
                            Logging.Comment("Телефон: " + tbphone.Text);
                            Logging.Comment("Общ.площадь: " + tbS.Text);
                            Logging.Comment("Торг. зал: " + tbSt.Text);
                        }
                        Logging.Comment("Стоимость 1 кв.м.: " + tbcbm.Text);
                        Logging.Comment("Тип оплаты: " + (radioButton1.Checked ? "Нал" : "Безнал"));
                        Logging.Comment("Замечание/примечание: " + tbFailComment.Text);
                        Logging.Comment("Примечание: " + tbremark.Text);
                        Logging.Comment("");
                        Logging.Comment("Завершение операции \"" + operation + "\"");
                        Logging.StopFirstLevel();
                    }

                    MessageBox.Show("Договор сохранен");
                    oldTen = tbTen.Text;
                    oldLord = tbLord.Text;
                    num = tbnumd.Text;
                    strdate = startdate.Value;
                    stdate = stopdate.Value;
                    zdan = cbZdan.Text;
                    floo = cbFloor.Text;
                    sec = cbSec.Text;
                    tp = cbTp.Text;
                    s = tbS.Text;
                    st = tbSt.Text;
                    ar = tbAr.Text;
                    reklamm = (int)cmbTypeDog.SelectedValue == 2;
                    nal = radioButton1.Checked;
                    remark = tbremark.Text;
                    typeDog = cmbTypeDog.Text;
                    cadNum = tbKadNum.Text;

                    _tbFailCommen = tbFailComment.Text;
                    _old_id_ten = id_ten;
                    _old_id_lord = id_lord;
                    _id_td = (int)cmbTypeDog.SelectedValue;
                    _id_zdan = _id_td == 3 ? -1 : (int)cbZdan.SelectedValue;
                    _id_floo = _id_td != 1 ? -1 : (int)cbFloor.SelectedValue;
                    _id_sec = _id_td == 3 ? (int)cbLandPlot.SelectedValue : _id_td == 1 ? (int)cbSec.SelectedValue : (int)cmbReclamaPlace.SelectedValue;
                    _id_tp = _id_td != 1 ? -1 : (int)cbTp.SelectedValue;
                    _telrab = telrab.Text;
                    _telhome = telhome.Text;
                    _telsot = telsot.Text;

                    //если включена реклама, передаем 0
                    if ((int)cmbTypeDog.SelectedValue == 2)
                    {
                        phone = tbphone.Text = numTextBox.CheckAndChange(defaultVal, 2, 0, 9999999999, false, defaultVal, format);
                        cbm = tbcbm.Text = numTextBox.CheckAndChange(defaultVal, 2, 0, 9999999999, false, defaultVal, format);
                        reklNum = tbReklNumber.Text;
                        reklLen = tbReklSize1.Text;
                        reklWid = tbReklSize2.Text;
                    }
                    else
                    {
                        phone = tbphone.Text;
                        cbm = tbcbm.Text;
                        reklNum = tbReklNumber.Text = numTextBox.CheckAndChange(defaultValInt, 0, 0, 999999999, false, defaultValInt, formatInt);
                        reklLen = tbReklSize1.Text = numTextBox.CheckAndChange(defaultVal, 2, 0, 9999999999, false, defaultVal, format);
                        reklWid = tbReklSize2.Text = numTextBox.CheckAndChange(defaultVal, 2, 0, 9999999999, false, defaultVal, format);
                    }
                    rekl = txtReklamma.Text;



                    if (rezhim == "add")
                    {
                        rezhim = "edit";
                        button4.Enabled = true;
                        this.Text = "Редактирование документа";
                        btAddDoc.Visible = true;
                        btAddDiscount.Visible = btDelDiscount.Visible = rezhim.Equals("edit") &&  new List<string> { "СОА", "РКВ", "МНД"}.Contains(TempData.Rezhim);
                        cmbTypeDog.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    //MessageBox.Show("Есть незаполненные поля");
                }
            }
        }

        private void FormatSumms()
        {
            tbS.Text = numTextBox.CheckAndChange(tbS.Text, 2, 0, 9999999999, false, defaultVal, format);
            tbReklSize1.Text = numTextBox.CheckAndChange(tbReklSize1.Text, 2, 0, 9999999999, false, defaultVal, format);
            tbReklSize2.Text = numTextBox.CheckAndChange(tbReklSize2.Text, 2, 0, 9999999999, false, defaultVal, format);
            tbSt.Text = numTextBox.CheckAndChange(tbSt.Text, 2, 0, 9999999999, false, defaultVal, format);
            tbcbm.Text = numTextBox.CheckAndChange(tbcbm.Text, 2, 0, 9999999999, false, defaultVal, format);
            tbphone.Text = numTextBox.CheckAndChange(tbphone.Text, 2, 0, 9999999999, false, defaultVal, format);
            tbReklNumber.Text = numTextBox.CheckAndChange(tbReklNumber.Text, 0, 0, 999999999, false, defaultValInt, formatInt);
            tbAr.Text = numTextBox.CheckAndChange(tbAr.Text, 2, 0, 9999999999, false, defaultVal, format);
            txtReklamma.Text = numTextBox.CheckAndChange(txtReklamma.Text, 2, 0, 9999999999, false, defaultVal, format);
            s = numTextBox.CheckAndChange(s, 2, 0, 9999999999, false, defaultVal, format);
            st = numTextBox.CheckAndChange(st, 2, 0, 9999999999, false, defaultVal, format);
            phone = numTextBox.CheckAndChange(phone, 2, 0, 9999999999, false, defaultVal, format);
            cbm = numTextBox.CheckAndChange(cbm, 2, 0, 9999999999, false, defaultVal, format);
            rekl = numTextBox.CheckAndChange(rekl, 2, 0, 9999999999, false, defaultVal, format);
            reklNum = numTextBox.CheckAndChange(reklNum, 0, 0, 999999999, false, defaultValInt, formatInt);
            reklLen = numTextBox.CheckAndChange(reklLen, 2, 0, 9999999999, false, defaultVal, format);
            reklWid = numTextBox.CheckAndChange(reklWid, 2, 0, 9999999999, false, defaultVal, format);
        }

        private void ErrorMessage(string err)
        {
            if (errortext == "")
            {
                errortext += "Сохранение невозможно, т.к. ";
            }
            errortext += err;
        }

        private void DontLetSaveUnactive()
        {
            errortext = "";

            //if (cbZdan.SelectedValue != null)
            //{
            //    if (int.Parse(cbZdan.SelectedValue.ToString()) == -1)
            //    {
            //        ErrorMessage("\n - выбрано неактивное здание");
            //    }
            //}

            DataTable Land_Tenant = new DataTable();
            Land_Tenant = _proc.GetTetant(0);
            if (Land_Tenant.Select("id = " + id_ten).Count() != 0)
            {
                if (Land_Tenant.Select("id = " + id_ten)[0]["isActive"].ToString() == "False")
                {
                    ErrorMessage("\n - выбран неактивный арендатор");
                }
            }
            else
            {
                ErrorMessage("\n  - арендатор не найден в справочнике арендаторов");
            }

            Land_Tenant = new DataTable();
            Land_Tenant = _proc.GetLandLord(0);
            if (Land_Tenant.Select("id = " + id_lord).Count() != 0)
            {
                if (Land_Tenant.Select("id = " + id_lord)[0]["isActive"].ToString() == "False")
                {
                    ErrorMessage("\n - выбран неактивный арендодатель");
                }
            }
            else
            {
                ErrorMessage("\n  - арендодатель не найден в справочнике арендодателей");
            }


            //if (cbZdan.SelectedValue != null)
            //{
            //    if (int.Parse(cbZdan.SelectedValue.ToString()) == -1)
            //    {
            //        ErrorMessage("\n - выбрано неактивное здание");
            //    }
            //}

            //if (cbFloor.SelectedValue != null)
            //{
            //    if (int.Parse(cbFloor.SelectedValue.ToString()) == -1)
            //    {
            //        ErrorMessage("\n - выбран неактивный этаж");
            //    }
            //}

            //if (cbSec.SelectedValue != null)
            //{
            //    if (int.Parse(cbSec.SelectedValue.ToString()) == -1)
            //    {
            //        ErrorMessage("\n - выбрана неактивная секция");
            //    }
            //}

            if (errortext != "")
            {
                MessageBox.Show(errortext, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool AllDataFilled()
        {
            //раскомментить если нужно будет запретить сохранение неактивных полей
            //DontLetSaveUnactive();
            //if (errortext != "")
            //{
            //    MessageBox.Show(errortext, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}

            bool err = false;
            string errmes = "";

            #region "Шапка"
            if (cmbTypeDog.SelectedValue == null)
            {
                err = true;
                errmes += "\n- Тип договора";
            }

            if (id_ten == 0)
            {
                err = true;
                errmes += "\n- Арендатор";
            }

            if (id_lord == 0)
            {
                err = true;
                errmes += "\n- Арендодатель";
            }

            if (tbTen.Text.Trim() == "")
            {
                err = true;
                if (!errmes.Contains("Арендатор"))
                    errmes += "\n- Арендатор";
            }

            if (tbLord.Text.Trim() == "")
            {
                err = true;
                if (!errmes.Contains("Арендодатель"))
                    errmes += "\n- Арендодатель";
            }

            if (tbnumd.Text == "")
            {
                err = true;
                errmes += "\n- Номер договора";
            }

            if (doc == null)
            {
                err = true;
                errmes += "\n- Дата заключения";
            }

            if (startdate == null)
            {
                err = true;
                errmes += "\n- Начало аренды";
            }

            if (stopdate == null)
            {
                err = true;
                errmes += "\n- Конец аренды";
            }
            #endregion


            if ((int)cmbTypeDog.SelectedValue == 1)
            {
                if (cbZdan.SelectedValue == null)
                {
                    err = true;
                    errmes += "\n- Здание";
                }
                if (cbFloor.SelectedValue == null)
                {
                    err = true;
                    errmes += "\n- Этаж";
                }
                if (cbTp.SelectedValue == null)
                {
                    err = true;
                    errmes += "\n- Тип помещения";
                }
                if (cbSec.SelectedValue == null)
                {
                    err = true;
                    errmes += "\n- Секция";
                }
            }
            else if ((int)cmbTypeDog.SelectedValue == 2)
            {
                if (cbZdan.SelectedValue == null)
                {
                    err = true;
                    errmes += "\n- Здание";
                }

                if (cmbReclamaPlace.SelectedValue == null)
                {
                    err = true;
                    errmes += "\n- Рекламное место";
                }
            }
            else if ((int)cmbTypeDog.SelectedValue == 3)
            {
                if (cbLandPlot.SelectedValue == null)
                {
                    err = true;
                    errmes += "\n- Земальный участок";
                }
            }


            if ((int)cmbTypeDog.SelectedValue != 2)
                if (decimal.Parse(numTextBox.ConvertToCompPunctuation(tbS.Text)) == decimal.Parse(numTextBox.ConvertToCompPunctuation(defaultVal)))
                {
                    err = true;
                    errmes += "\n- Общая площадь";
                }

            if ((int)cmbTypeDog.SelectedValue == 3 && tbKadNum.Text.Length == 0)
            {
                err = true;
                errmes += "\n- Кадастровый номер";
            }

            if (cmbTypeDog.SelectedValue != null && (int)cmbTypeDog.SelectedValue == 2)
            {
                if (decimal.Parse(numTextBox.ConvertToCompPunctuation(tbReklSize1.Text))
                  == decimal.Parse(numTextBox.ConvertToCompPunctuation(defaultVal)))
                {
                    err = true;
                    errmes += "\n- Размер места";
                }

                if (decimal.Parse(numTextBox.ConvertToCompPunctuation(tbReklSize2.Text))
                  == decimal.Parse(numTextBox.ConvertToCompPunctuation(defaultVal)))
                {
                    err = true;
                    errmes += "\n- Размер места";
                }

                if (decimal.Parse(numTextBox.ConvertToCompPunctuation(txtReklamma.Text))
                  == decimal.Parse(numTextBox.ConvertToCompPunctuation(defaultVal)))
                {
                    err = true;
                    errmes += "\n- Стоимость рекламы";
                }

                //if (int.Parse(numTextBox.ConvertToCompPunctuation(tbReklNumber.Text))
                //  == int.Parse(defaultValInt))
                //{
                //    err = true;
                //    errmes += "\n- Номер места";
                //}
            }

            if ((decimal.Parse(numTextBox.ConvertToCompPunctuation(tbcbm.Text)) == 0)
              && (!(cmbTypeDog.SelectedValue != null
              && (int)cmbTypeDog.SelectedValue == 2)))
            {
                err = true;
                errmes += "\n- Стоимость квадратного метра";
            }

            if (tbFailComment.Text.Trim().Length == 0)
            {
                err = true;
                errmes += "\n- Замечание/Примечание";
            }

            if (err)
            {
                MessageBox.Show("Есть незаполненные поля:" + errmes, "Сохранение",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (startdate.Value > stopdate.Value)
            {
                MessageBox.Show("Дата окончания меньше Даты начала", "Ошибка");
                return false;
            }

            if (cmbTypeDog.SelectedValue != null && (int)cmbTypeDog.SelectedValue == 2)
            {
                //проверяем оплаты
                if (foundPayments())
                {
                    if (rekl != txtReklamma.Text)
                    {
                        MessageBox.Show("По договору имеются оплаты. Изменение суммы договора невозможно",
                          "Ошибка");
                        return false;
                    }

                    if (strdate != startdate.Value)
                    {
                        MessageBox.Show("По договору имеются оплаты. Изменение даты начала договора невозможно",
                          "Ошибка");
                        return false;
                    }
                }
            }
            else
            {
                //проверяем оплаты
                if (foundPayments())
                {
                    if (ar != tbAr.Text)
                    {
                        MessageBox.Show("По договору имеются оплаты. Изменение суммы договора невозможно",
                          "Ошибка");
                        return false;
                    }

                    if (strdate != startdate.Value)
                    {
                        MessageBox.Show("По договору имеются оплаты. Изменение даты начала договора невозможно",
                          "Ошибка");
                        return false;
                    }
                }
            }

            if (foundPaymentsLastMonth())
            {
                if (stdate != stopdate.Value)
                {
                    MessageBox.Show("По договору есть оплаты за месяц, \nвыбранный в календаре \"Срок аренды по\". Сохранение невозможно!",
                      "Ошибка");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ////Процедура проверки наличия оплат по договору в последний месяц
        /// </summary>
        /// <returns></returns>
        private bool foundPaymentsLastMonth()
        {
            bool res = false;

            DataTable dt = new DataTable();
            dt = _proc.GetPaymentsLastMonth(_id, stopdate.Value);

            if (dt.Rows.Count > 0)
            {
                res = true;
            }

            return res;
        }

        /// <summary>
        ////Процедура проверки наличия оплат по договору
        /// </summary>
        /// <returns></returns>
        private bool foundPayments()
        {
            bool res = false;

            DataTable dt = new DataTable();
            dt = _proc.GetPayments(_id);

            if (dt.Rows.Count > 0)
            {
                res = true;
            }

            return res;
        }

        private bool SquareChanged()
        {
            string sForCheck = tbS.Text;
            string stForCheck = tbSt.Text;

            //если включен чек-бокс рекламы то нам без разницы менялась площадь или нет
            //и тем более изменять площадь в справочнике не надо
            if (cmbTypeDog.SelectedValue != null && (int)cmbTypeDog.SelectedValue == 2)
            {
                return false;
            }

            if ((dtSections != null) && (dtSections.Rows.Count != 0) && (cbSec.SelectedIndex != -1))
            {
                DataTable dtSecInfo = new DataTable();
                dtSecInfo = _proc.GetSec(0);

                sForCheck = dtSecInfo.Select("id = " + cbSec.SelectedValue)[0]["Total_Area"].ToString();
                stForCheck = dtSecInfo.Select("id = " + cbSec.SelectedValue)[0]["Area_of_Trading_Hall"].ToString();

                sForCheck = numTextBox.CheckAndChange(s, 2, 0, 9999999999, false, defaultVal, format);
                stForCheck = numTextBox.CheckAndChange(st, 2, 0, 9999999999, false, defaultVal, format);

                //s = dtSecInfo.Select("id = " + cbSec.SelectedValue)[0]["Total_Area"].ToString();
                //st = dtSecInfo.Select("id = " + cbSec.SelectedValue)[0]["Area_of_Trading_Hall"].ToString();
                //s = numTextBox.CheckAndChange(s, 2, 0, 9999999999, false, defaultVal, format);
                //st = numTextBox.CheckAndChange(st, 2, 0, 9999999999, false, defaultVal, format);
            }
            else
            {
                return true;
            }

            if ((sForCheck != tbS.Text) || (stForCheck != tbSt.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void dgAddDoc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (_id == 0)
            {
                strdate = startdate.Value;
                stdate = stopdate.Value;
                _id = int.Parse(_proc.GetLastRec().Rows[0]["id"].ToString());
                var adddoc = new AdditionalDoc(_id, strdate, stdate, (int)cmbTypeDog.SelectedValue);
                adddoc.setDocData(num, oldTen, oldLord, _old_id_ten, _old_id_lord, oldDoc);
                adddoc.ShowDialog();
                ini(_id);
            }
            else
            {
                AdditionalDoc adddoc = new AdditionalDoc(_id, strdate, stdate, (int)cmbTypeDog.SelectedValue);
                adddoc.setDocData(num, oldTen, oldLord, _old_id_ten, _old_id_lord, oldDoc);
                adddoc.ShowDialog();
                ini(_id);
            }
            DopSVisible();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id_TypeDocF = int.Parse(dgAddDoc.SelectedRows[0].Cells["id_TypeDoc"].Value.ToString());
            int id_DopDoc = int.Parse(dgAddDoc.SelectedRows[0].Cells["tdid"].Value.ToString());

            //Соглашение о продлении договора
            if (id_TypeDocF == 1)
            {
                if (_proc.CheckBeforeDelDopDocs(id_DopDoc) > 0)
                {
                    MessageBox.Show("За месяц вступления в силу соглашения о \nпродлении договора имеются оплаты по договору. \nУдаление доп.соглашения невозможно!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DopSVisible();
                    return;
                }
            }

            //Соглашение об изменении площади
            if (id_TypeDocF == 5)
            {
                if (_proc.CheckBeforeDelDopDocs(id_DopDoc) > 0)
                {
                    MessageBox.Show("За месяц вступления в силу соглашения об \nизменении площади имеются оплаты по договору. \nУдаление доп.соглашения невозможно!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DopSVisible();
                    return;
                }
            }

            if (MessageBox.Show("Удалить доп.документ?", "Внимание", MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Logging.StartFirstLevel(1401);
                Logging.Comment("ID: " + id_DopDoc);
                Logging.Comment("№ документа: " + dgAddDoc.SelectedRows[0].Cells["number"].Value.ToString());
                Logging.Comment("Тип документа ID: " + id_TypeDocF + " ; Наименование: " + dgAddDoc.SelectedRows[0].Cells["td"].Value.ToString());
                Logging.Comment("Дата документа: " + dgAddDoc.SelectedRows[0].Cells["datetd"].Value.ToString());

                Logging.Comment("Данные арендатора, у которого удаляется доп.документ");
                Logging.Comment("Дата заключения договора: " + oldDoc.ToShortDateString());
                Logging.Comment("Номер договора: " + num);
                Logging.Comment("Арендатор ID: " + _old_id_ten + "; Наименование: " + oldTen);
                Logging.Comment("Арендодатель ID: " + _old_id_lord + "; Наименование: " + oldLord);


                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();

                _proc.DelTD(id_DopDoc);
                ini(_id);
            }
            DopSVisible();
        }

        private void tbRentalVacation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                e.Handled = true;
        }

        private void tbRentalVacation_Validating(object sender, CancelEventArgs e)
        {
            if ((sender as TextBox).Text.Trim().Length == 0) (sender as TextBox).Text = "0";
            else
                (sender as TextBox).Text = int.Parse((sender as TextBox).Text).ToString("0");
        }

        private void cbZdan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (lbReklPrice.Visible)
            {
                //Console.WriteLine(cbZdan.SelectedValue);
                GetDataReklamPlaceInfo((int)cbZdan.SelectedValue);
            }
        }

        private void GetDataReklamPlaceInfo(int id_Build)
        {
            //_idObj
            if ((int)cmbTypeDog.SelectedValue == 2)
            {
                DataTable dtReklamPLace = _proc.getReclamaPlace(_idObj, id_Build);
                cmbReclamaPlace.DataSource = dtReklamPLace;
                cmbReclamaPlace.DisplayMember = "NumberPlace";
                cmbReclamaPlace.ValueMember = "id";
                cmbReclamaPlace.SelectedIndex = -1;

                tbReklSize1.Text = "0.00";
                tbReklSize2.Text = "0.00";
                //tbKadNum.Text = "";
            }
        }

        private void GetLandPlot()
        {
            if ((int)cmbTypeDog.SelectedValue == 3)
            {
                DataTable dtLandPlot = _proc.getLandPlot(_idObj);
                cbLandPlot.DataSource = dtLandPlot;
                cbLandPlot.DisplayMember = "NumberPlot";
                cbLandPlot.ValueMember = "id";
                cbLandPlot.SelectedIndex = -1;
            }
        }

        private void cbLandPlot_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbLandPlot.SelectedValue != null)
            {
                EnumerableRowCollection<DataRow> rowCollect = (cbLandPlot.DataSource as DataTable).AsEnumerable()
                    .Where(r => r.Field<int>("id") == (int)cbLandPlot.SelectedValue);

                if (rowCollect.Count() > 0)
                {
                    tbS.Text = rowCollect.First()["AreaPlot"].ToString();
                }
                else
                {
                    tbS.Text = "0.00";
                    //tbKadNum.Text = "";
                }
            }
        }

        private void btAddDiscount_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == new Payments.frmAddDiscount() { id_Agreements = _id, id_TypeDog =  (int)cmbTypeDog.SelectedValue}.ShowDialog())
            {
                getDiscount();
            }
        }

        private void cmbReclamaPlace_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbReclamaPlace.SelectedValue != null)
            {
                EnumerableRowCollection<DataRow> rowCollect = (cmbReclamaPlace.DataSource as DataTable).AsEnumerable()
                    .Where(r => r.Field<int>("id") == (int)cmbReclamaPlace.SelectedValue);

                if (rowCollect.Count() > 0)
                {
                    tbReklSize1.Text = rowCollect.First()["Length"].ToString();
                    tbReklSize2.Text = rowCollect.First()["Width"].ToString();
                    //tbKadNum.Text = rowCollect.First()[""].ToString();

                }
                else
                {
                    tbReklSize1.Text = "0.00";
                    tbReklSize2.Text = "0.00";
                    //tbKadNum.Text = "";
                }
            }
        }

        private void btDelDiscount_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtDiscount != null && dtDiscount.DefaultView.Count != 0)
            {
                int id_discount = (int)dtDiscount.DefaultView[dgvData.CurrentRow.Index]["id"];


                DataTable dtResult = _proc.setTDiscount(id_discount, _id, DateTime.Now, null, 0, 0, 0, 0, true);

                if (dtResult == null)
                {
                    MessageBox.Show(TempData.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int result = (int)dtResult.Rows[0]["id"];

                if (result == -1)
                {
                    MessageBox.Show(TempData.centralText("Запись уже удалена другим пользователем\n"), "Удаление записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getDiscount();
                    return;
                }

                if (DialogResult.Yes == MessageBox.Show("Удалить выбранную запись?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    dtResult = _proc.setTDiscount(id_discount, _id, DateTime.Now, null, 0, 0, 0, 1, true);
                    if (dtResult == null)
                    {
                        MessageBox.Show(TempData.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    getDiscount();
                    return;
                }

                
            }
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow == null || dgvData.CurrentRow.Index == -1 || dtDiscount == null || dtDiscount.DefaultView.Count == 0 || dgvData.CurrentRow.Index >= dtDiscount.DefaultView.Count)
            {
                btDelDiscount.Enabled = false;                
                return;
            }

            btAccept.Enabled = btunAccept.Enabled = btDelDiscount.Enabled = (int)dtDiscount.DefaultView[dgvData.CurrentRow.Index]["id_StatusDiscount"] == 1;
        }

        private void dgvData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void dgvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1 && dtDiscount != null && dtDiscount.DefaultView.Count != 0)
            {
                Color rColor = Color.White;
                //if (!(bool)dtData.DefaultView[e.RowIndex]["isActive"])
                //    rColor = panel1.BackColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;

            }
        }

        private void подтвердитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id_discount = (int)dtDiscount.DefaultView[dgvData.CurrentRow.Index]["id"];
            DateTime dStart = (DateTime)dtDiscount.DefaultView[dgvData.CurrentRow.Index]["DateStart"];
            DateTime? dEnd = null;
            if (dtDiscount.DefaultView[dgvData.CurrentRow.Index]["DateEnd"] != DBNull.Value) dEnd = (DateTime)dtDiscount.DefaultView[dgvData.CurrentRow.Index]["DateEnd"];
            int id_TypeDiscount = (int)dtDiscount.DefaultView[dgvData.CurrentRow.Index]["id_TypeDiscount"];
            decimal discount = (decimal)dtDiscount.DefaultView[dgvData.CurrentRow.Index]["Discount"];
            int id_Status = 2;

            DataTable dtResult = _proc.setTDiscount(id_discount, _id, dStart, dEnd, id_TypeDiscount, id_Status, discount);

            if (dtResult == null)
            {
                MessageBox.Show(TempData.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int result = (int)dtResult.Rows[0]["id"];
            getDiscount();
        }

        private void отклонитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id_discount = (int)dtDiscount.DefaultView[dgvData.CurrentRow.Index]["id"];
            DateTime dStart = (DateTime)dtDiscount.DefaultView[dgvData.CurrentRow.Index]["DateStart"];
            DateTime? dEnd = null;
            if (dtDiscount.DefaultView[dgvData.CurrentRow.Index]["DateEnd"] != DBNull.Value) dEnd = (DateTime)dtDiscount.DefaultView[dgvData.CurrentRow.Index]["DateEnd"];
            int id_TypeDiscount = (int)dtDiscount.DefaultView[dgvData.CurrentRow.Index]["id_TypeDiscount"];
            decimal discount = (decimal)dtDiscount.DefaultView[dgvData.CurrentRow.Index]["Discount"];
            int id_Status = 3;

            DataTable dtResult = _proc.setTDiscount(id_discount, _id, dStart, dEnd, id_TypeDiscount, id_Status, discount);

            if (dtResult == null)
            {
                MessageBox.Show(TempData.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int result = (int)dtResult.Rows[0]["id"];
            getDiscount();
        }

        private void dgvData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1 && new List<string> { "Д" }.Contains(UserSettings.User.StatusCode))
            {
                dgvData.CurrentCell = dgvData[e.ColumnIndex, e.RowIndex];
                dgvData_SelectionChanged(null, null);
                cmsDiscount.Show(MousePosition);
            }
        }

        private void cmsDiscount_Opening(object sender, CancelEventArgs e)
        {
            if ((int)dtDiscount.DefaultView[dgvData.CurrentRow.Index]["id_StatusDiscount"] != 1)
                e.Cancel = true;
        }

        private void btAccept_Click(object sender, EventArgs e)
        {
            подтвердитьToolStripMenuItem_Click(null, null);
        }

        private void btunAccept_Click(object sender, EventArgs e)
        {
            отклонитьToolStripMenuItem_Click(null, null);
        }

        private void tbRentalVacation_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbSavePayment_DropDown(object sender, EventArgs e)
        {
            var senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;

            int vertScrollBarWidth = (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                    ? SystemInformation.VerticalScrollBarWidth : 0;

            //var itemsList = senderComboBox.Items.Cast<object>().Select(item => item.ToString());
            //foreach (string s in itemsList)
            //{
            //    int newWidth = (int)g.MeasureString(s, font).Width + vertScrollBarWidth;

            //    if (width < newWidth)
            //    {
            //        width = newWidth;
            //    }
            //}

            DataTable dtList = (DataTable)senderComboBox.DataSource;


            foreach (DataRow r in dtList.Rows)
            {
                string s = (string)r["cName"];

                int newWidth = (int)g.MeasureString(s, font).Width + vertScrollBarWidth;

                if (width < newWidth)
                {
                    width = newWidth;
                }
            }

            senderComboBox.DropDownWidth = width;
        }

        private void tbS_KeyPress(object sender, KeyPressEventArgs e)
        {
            numTextBox.KeyPress(tbS, e, false, false);
        }

        private void tbSt_KeyPress(object sender, KeyPressEventArgs e)
        {
            numTextBox.KeyPress(tbSt, e, false, false);
        }

        private void tbReklSize1_KeyPress(object sender, KeyPressEventArgs e)
        {
            numTextBox.KeyPress(tbReklSize1, e, false, false);
        }

        private void tbReklSize2_KeyPress(object sender, KeyPressEventArgs e)
        {
            numTextBox.KeyPress(tbReklSize2, e, false, false);
        }

        private void tbAr_KeyPress(object sender, KeyPressEventArgs e)
        {
            numTextBox.KeyPress(tbAr, e, false, false);
        }

        private void tbphone_KeyPress(object sender, KeyPressEventArgs e)
        {
            numTextBox.KeyPress(tbphone, e, false, false);
        }

        private void tbReklNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            numTextBox.KeyPress(tbReklNumber, e, true, false);
        }

        private void tbcbm_KeyPress(object sender, KeyPressEventArgs e)
        {
            numTextBox.KeyPress(tbcbm, e, false, false);
        }

        private void AddeditDoc_Load(object sender, EventArgs e)
        {
            btAddDiscount.Visible = btDelDiscount.Visible = rezhim.Equals("edit") && new List<string> { "СОА", "РКВ", "МНД" }.Contains(TempData.Rezhim);
            btAccept.Visible = btunAccept.Visible = rezhim.Equals("view") && new List<string> { "Д" }.Contains(TempData.Rezhim);

            groupBox7.Enabled = true;
            dgvData.Enabled = true;
        }

        private void cbZdan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!click)
            {
                try
                {
                    if (cbZdan.SelectedIndex == -1)
                    {
                        cbFloor.Enabled = false;
                    }
                    else
                    {
                        cbFloor.Enabled = true;
                        cbFloor.SelectedIndex = -1;
                        cbSec.Enabled = false;
                        cbSec.SelectedIndex = -1;
                    }
                }

                catch (Exception) { };
            }
        }

        private void cbFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!click)
            {
                try
                {
                    if (cbFloor.SelectedIndex == -1)
                    {
                        cbSec.Enabled = false;
                    }
                    else
                    {
                        cbSec.Enabled = true;
                    }
                    floorFill = true;
                    FillCbSec(false);
                    floorFill = false;
                }
                catch (Exception) { };
            }
        }

        private void cbSecRefresh()
        {
            dtSections = _proc.FillCbSecTp(
                                        1,
                                        (cbZdan.SelectedValue != null ? int.Parse(cbZdan.SelectedValue.ToString()) : 0),
                                        (cbFloor.SelectedValue != null ? int.Parse(cbFloor.SelectedValue.ToString()) : 0),
                                        tbObj.Text != "" ? _idObj : 0);
            cbSec.DataSource = dtSections;
            cbSec.DisplayMember = "cName";
            cbSec.ValueMember = "id";
            cbSec.SelectedIndex = -1;

        }

        private void cbFloorRefresh()
        {
            dtFloor = _proc.FillCbZdFl(1, (cbZdan.SelectedValue != null ? int.Parse(cbZdan.SelectedValue.ToString()) : 0));
            dtFloor.DefaultView.RowFilter = "isActive = 1";
            cbFloor.DataSource = dtFloor;
            cbFloor.DisplayMember = "cName";
            cbFloor.ValueMember = "id";
            cbFloor.SelectedIndex = -1;
            cbSec.SelectedIndex = -1;
        }

        private void cbSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!click)
            {
                try
                {
                    if ((dtSections != null) &&
                        (dtSections.Rows.Count != 0) &&
                        (cbSec.SelectedIndex != -1))
                    {
                        s = tbS.Text = numTextBox.CheckAndChange(dtSections.Rows[cbSec.SelectedIndex]["Total_Area"].ToString().Trim(), 2, 0, 9999999999, false, defaultVal, format);
                        st = tbSt.Text = numTextBox.CheckAndChange(dtSections.Rows[cbSec.SelectedIndex]["Area_of_Trading_Hall"].ToString().Trim(), 2, 0, 9999999999, false, defaultVal, format);
                    }
                    else
                    {
                        s = tbS.Text = numTextBox.CheckAndChange(tbS.Text, 2, 0, 9999999999, false, defaultVal, format);
                        st = tbSt.Text = numTextBox.CheckAndChange(tbSt.Text, 2, 0, 9999999999, false, defaultVal, format);
                    }
                }
                catch
                {
                }
            }
        }

        private void cbTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_type = Rec.Rows.Count != 0 && (int)Rec.Rows[0]["id_TypeContract"] == 1 ? int.Parse(Rec.Rows[0]["id_Type_of_Premises"].ToString()) : -1;
        }

        private void tbnumd_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Regex pat = new Regex(@"[\b]|[0-9]|[\s]");
            //bool b = pat.IsMatch(e.KeyChar.ToString());
            //if (b == false)
            //{
            //    e.Handled = true;
            //}
        }

        private void пиуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dgAddDoc.SelectedRows[0].Cells["td"].Value.ToString());
        }

        private void tbS_TextChanged(object sender, EventArgs e)
        {
            CalculateArenda();
        }

        private void CalculateArenda()
        {
            var asdasd = (tbS.Text.Trim().Length != 0 ? decimal.Parse(numTextBox.ConvertToCompPunctuation(tbS.Text)) : 0)
                *
                (tbcbm.Text.Trim().Length != 0 ? decimal.Parse(numTextBox.ConvertToCompPunctuation(tbcbm.Text)) : 0)
                +
                (tbphone.Text.Trim().Length != 0 ? decimal.Parse(numTextBox.ConvertToCompPunctuation(tbphone.Text)) : 0);
            tbAr.Text = numTextBox.CheckAndChange(asdasd.ToString(), 2, 0, 9999999999, false, defaultVal, format);
        }

        private void tbphone_TextChanged(object sender, EventArgs e)
        {
            CalculateArenda();
        }

        private void tbS_Leave(object sender, EventArgs e)
        {
            tbS.Text = numTextBox.CheckAndChange(tbS.Text, 2, 0, 9999999999, false, defaultVal, format);
            FormatSumms();
        }

        private void tbReklSize1_Leave(object sender, EventArgs e)
        {
            tbReklSize1.Text = numTextBox.CheckAndChange(tbReklSize1.Text, 2, 0, 9999999999, false, defaultVal, format);
            FormatSumms();
        }

        private void tbReklSize2_Leave(object sender, EventArgs e)
        {
            tbReklSize2.Text = numTextBox.CheckAndChange(tbReklSize2.Text, 2, 0, 9999999999, false, defaultVal, format);
            FormatSumms();
        }

        private void tbSt_Leave(object sender, EventArgs e)
        {
            tbSt.Text = numTextBox.CheckAndChange(tbSt.Text, 2, 0, 9999999999, false, defaultVal, format);
            FormatSumms();
        }

        private void tbcbm_Leave(object sender, EventArgs e)
        {
            tbcbm.Text = numTextBox.CheckAndChange(tbcbm.Text, 2, 0, 9999999999, false, defaultVal, format);
            FormatSumms();
        }

        private void tbphone_Leave(object sender, EventArgs e)
        {
            tbphone.Text = numTextBox.CheckAndChange(tbphone.Text, 2, 0, 9999999999, false, defaultVal, format);
            FormatSumms();
        }

        private void tbReklNumber_Leave(object sender, EventArgs e)
        {
            tbReklNumber.Text = numTextBox.CheckAndChange(tbReklNumber.Text, 0, 0, 999999999, false, defaultValInt, formatInt);
            FormatSumms();
        }

        private void cbZdan_Click(object sender, EventArgs e)
        {
            click = true;
            int selectedZdan = cbZdan.SelectedValue != null ? int.Parse(cbZdan.SelectedValue.ToString()) : -1;

            FillCbZdan(true);

            if (dtZdan.Select("id = " + selectedZdan).Count() == 0)
            {
                click = false;
                cbZdan.SelectedIndex = -1;
            }
            else
            {
                cbZdan.SelectedValue = selectedZdan;
            }
            click = false;
        }

        private void FillCbZdan(bool onClick)
        {
            //if (Rec.Rows.Count != 0 && (int)Rec.Rows[0]["id_TypeContract"] == 3) return;
            //Rec = _proc.GetLD(_id);
            zdan = Rec.Rows.Count != 0 && (int)Rec.Rows[0]["id_TypeContract"] != 3 ? Rec.Rows[0]["build"].ToString() : "";
            id_build = Rec.Rows.Count != 0 && (int)Rec.Rows[0]["id_TypeContract"] != 3 ? int.Parse(Rec.Rows[0]["id_Buildings"].ToString()) : -1;

            dtZdan = new DataTable();
            dtZdan = _proc.FillCbZdFl(0);
            dtZdan.DefaultView.RowFilter = "isActive = 1";
            cbZdan.DataSource = dtZdan;
            cbZdan.DisplayMember = "cName";
            cbZdan.ValueMember = "id";

            bool buildIsUnactive;
            if (rezhim != "add")
            {
                if (dtZdan.Select("isActive = 1 and id = " + id_build).Count() == 0)
                {
                    if (!onClick && Rec != null && Rec.Rows.Count > 0 && (int)Rec.Rows[0]["id_TypeContract"] != 3)
                    {
                        ErrorMessageLoad("\n - неактивное здание");
                    }
                    buildIsUnactive = true;
                }
                else
                {
                    buildIsUnactive = false;
                }
            }
            else
            {
                buildIsUnactive = false;
            }

            FillZd(id_build, zdan, buildIsUnactive);
        }

        private void FillCmbTypeDog()
        {
            DataTable dtTypes = _proc.GetContractTypes();
            dtTypes.DefaultView.RowFilter = "isActive = 1";
            dtTypes.DefaultView.Sort = "id";
            cmbTypeDog.DataSource = dtTypes;
        }

        private void cbFloor_Click(object sender, EventArgs e)
        {
            click = true;
            int selectedFloor = cbFloor.SelectedValue != null ? int.Parse(cbFloor.SelectedValue.ToString()) : -1;

            FillCbFloor(true);

            if (dtFloor.Select("id = " + selectedFloor).Count() == 0)
            {
                click = false;
                cbFloor.SelectedIndex = -1;
            }
            else
            {
                cbFloor.SelectedValue = selectedFloor;
            }
            click = false;
        }

        private void FillCbFloor(bool onClick)
        {
            //if (Rec.Rows.Count != 0 && (int)Rec.Rows[0]["id_TypeContract"] != 1) return;
            //Rec = _proc.GetLD(_id);
            floo = Rec.Rows.Count != 0 && (int)Rec.Rows[0]["id_TypeContract"] ==1 ? Rec.Rows[0]["floo"].ToString() : "";
            id_floor = Rec.Rows.Count != 0 && (int)Rec.Rows[0]["id_TypeContract"] == 1 ? int.Parse(Rec.Rows[0]["id_Floor"].ToString()) : -1;

            dtFloor = new DataTable();
            dtFloor = _proc.FillCbZdFl(1);
            dtFloor.DefaultView.RowFilter = "isActive = 1";
            cbFloor.DataSource = dtFloor;
            cbFloor.DisplayMember = "cName";
            cbFloor.ValueMember = "id";

            bool floorIsUnactive;
            if (rezhim != "add")
            {
                if (dtFloor.Select("isActive = 1 and id = " + id_floor).Count() == 0)
                {
                    if (!onClick && Rec != null && Rec.Rows.Count > 0 && (int)Rec.Rows[0]["id_TypeContract"] ==1)
                    {
                        ErrorMessageLoad("\n - неактивный этаж");
                    }
                    floorIsUnactive = true;
                }
                else
                {
                    floorIsUnactive = false;
                }
            }
            else
            {
                floorIsUnactive = false;
            }

            FillFloor(id_floor, floo, floorIsUnactive);
        }

        private void cbSec_Click(object sender, EventArgs e)
        {
            click = true;
            int selectedSec = cbSec.SelectedValue != null ? int.Parse(cbSec.SelectedValue.ToString()) : -1;

            FillCbSec(true);

            if (dtSections.Select("id = " + selectedSec).Count() == 0)
            {
                click = false;
                cbSec.SelectedIndex = -1;
            }
            else
            {
                cbSec.SelectedValue = selectedSec;
            }
            click = false;
        }

        private void FillCbSec(bool onClick)
        {
            //if (Rec.Rows.Count != 0 && (int)Rec.Rows[0]["id_TypeContract"] != 1) return;

            //Rec = _proc.GetLD(_id);
            sec = Rec.Rows.Count != 0 && (int)Rec.Rows[0]["id_TypeContract"] ==1 ? Rec.Rows[0]["sec"].ToString() : "";
            id_sec = Rec.Rows.Count != 0 && (int)Rec.Rows[0]["id_TypeContract"] ==1 ? int.Parse(Rec.Rows[0]["id_Section"].ToString()) : -1;

            dtSections = new DataTable();
            dtSections = _proc.FillCbSecTp(
                                        1,
                                        (cbZdan.SelectedValue != null ? int.Parse(cbZdan.SelectedValue.ToString()) : 0),
                                        (cbFloor.SelectedValue != null ? int.Parse(cbFloor.SelectedValue.ToString()) : 0),
                                        tbObj.Text != "" ? _idObj : 0);
            cbSec.DataSource = dtSections;
            cbSec.DisplayMember = "cName";
            cbSec.ValueMember = "id";

            bool secIsUnactive;

            if (rezhim != "add")
            {
                if (dtSections.Select("id = " + id_sec).Count() == 0)
                {
                    if (!onClick && Rec != null && Rec.Rows.Count > 0 && (int)Rec.Rows[0]["id_TypeContract"] == 1)
                    {
                        if (!floorFill)
                            ErrorMessageLoad("\n - неактивная секция");
                    }
                    secIsUnactive = true;
                }
                else
                {
                    secIsUnactive = false;
                }
            }
            else
            {
                secIsUnactive = false;
            }
            FillSec(id_sec, sec, secIsUnactive);
        }

        private void dgAddDoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((dgAddDoc.Rows.Count != 0) && (e.RowIndex >= 0))
            {
                DopSVisible();
            }
        }

        private void DopSVisible()
        {
            tbDopS.Visible = false;
            lblDopS.Visible = false;
            if (dgAddDoc.Rows.Count != 0)
            {
                if (dgAddDoc.CurrentRow != null)
                {
                    if (dgAddDoc.CurrentRow.Cells["Total_Area"].Value.ToString() != "")
                    {
                        tbDopS.Visible = true;
                        lblDopS.Visible = true;
                        tbDopS.Text = dgAddDoc.CurrentRow.Cells["Total_Area"].Value.ToString();
                    }
                }
            }
        }

        private void dgAddDoc_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            DopSVisible();
        }

        private void dgAddDoc_SelectionChanged(object sender, EventArgs e)
        {
            DopSVisible();
        }

        private void btAddDoc_Click(object sender, EventArgs e)
        {
            frmDocument frmDoc = new frmDocument(IsView) { id_Doc = _id/*, _id_type_dog =  int.Parse(cmbTypeDog.SelectedValue.ToString())*/ };

            frmDoc.str = doc.Value;
            frmDoc.setDocData(num, oldTen, oldLord, _old_id_ten, _old_id_lord, oldDoc);
            frmDoc.ShowDialog();
        }

        private void txtReklamma_KeyPress(object sender, KeyPressEventArgs e)
        {
            numTextBox.KeyPress(txtReklamma, e, false, false);
        }

        private void txtReklamma_Leave(object sender, EventArgs e)
        {
            txtReklamma.Text = numTextBox.CheckAndChange(txtReklamma.Text, 2, 0, 9999999999, false, defaultVal, format);
            FormatSumms();
        }

        private void cmbTypeDog_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TypeToVisibleElement();
            GetLandPlot();
            //lReclamaPlace.Visible = cmbReclamaPlace.Visible=  lbReklPrice.Visible = txtReklamma.Visible = lblReklNumber.Visible
            //    = tbReklNumber.Visible = lblReklSize.Visible = tbReklSize1.Visible
            //    = tbReklSize2.Visible = (int)cmbTypeDog.SelectedValue == 2;

            //  label7.Visible = cbSec.Visible = lbPrice.Visible = tbcbm.Visible = (int)cmbTypeDog.SelectedValue != 2;

            //  lTp.Visible = cbTp.Visible = cbFloor.Visible = lFloor.Visible = lblSt.Visible = tbSt.Visible = lblPhone.Visible = tbphone.Visible
            //    = (int)cmbTypeDog.SelectedValue == 1;

            if ((int)cmbTypeDog.SelectedValue == 2 && cbZdan.SelectedValue != null && _idObj !=0)
                GetDataReklamPlaceInfo((int)cbZdan.SelectedValue);
        }

        private void tbObj_TextChanged(object sender, EventArgs e)
        {
            cbZdan.Enabled = tbObj.Text != "";
        }

        private void getDiscount()
        {
            dgvData.AutoGenerateColumns = false;
            dtDiscount = _proc.getTDiscount(_id);
            dgvData.DataSource = dtDiscount;

        }
    }
}
