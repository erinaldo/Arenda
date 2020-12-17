using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using System.IO;
using Nwuram.Framework.Logging;
using System.Net;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Exc = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;
using Nwuram.Framework.ToExcel;
using System.Threading;
using Nwuram.Framework.ToExcelNew;

namespace Arenda
{
    public partial class mForm : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        ToolTip t = new ToolTip();
        DataTable _Tenant;
        DataTable _ListDoc;
        DataTable _LandLord;
        DataTable dt, dtSettings;
        DateTime SRZA;
        string _fileName;
        string ltprint;
        string username;
        string progname;
        string codeuser;
        string _landlordObject;
        bool load;
        Object wMissing = System.Reflection.Missing.Value;
        Object wTrue = true;
        Object wFalse = false;
        DataView view = new DataView();

        public mForm(string prog, string code, string name)
        {
            load = true;
            tGo.value = true;
            InitializeComponent();
            pTenant.Hide();
            pLordland.Hide();
            pListDoc.Hide();
            DataTable cb;

            object tmp = 0;
            if (cbLordland.SelectedValue != null)
                tmp = cbLordland.SelectedValue;

            cb = _proc.FillCbLord();

            /*for (int i = 1; i <= cb.Rows.Count - 1; i++)
            {
                if (cb.Rows[i - 1][0].ToString() == cb.Rows[i][0].ToString())
                    cb.Rows.RemoveAt(i);
            }*/

            DataRow dr = cb.Rows.Add();
            dr["id"] = 0;
            dr["lName"] = "Все арендодатели";
            cb.DefaultView.Sort = "lName";
            cbLordland.DataSource = cb;
            cbLordland.SelectedValue = tmp;

            for (int i = 0; i <= cb.Rows.Count - 1; i++)
            {
                //cbLordland.Items.Add(cb.Rows[i][0].ToString());
                Label l = new Label();
                l.Font = cbLordland.Font;
                l.AutoSize = true;
                l.Text = cb.Rows[i][1].ToString();
                l.Visible = false;
                this.Controls.Add(l);
                if (l.Width > cbLordland.DropDownWidth)
                    cbLordland.DropDownWidth = l.Width;
                this.Controls.Remove(l);
            }
            //cbLordland.Text = cbLordland.Items[0].ToString();
            load = false;

            btAdd.Enabled = false;
            btDel.Enabled = false;
            btEdit.Enabled = false;
            btPrint.Enabled = false;
            btExel.Enabled = false;
            btnListPayment.Enabled = false;
            btnListTaxes.Enabled = false;
            btnView.Enabled = false;
            btCopyDoc.Enabled = false;
            //btKntListTaxes.Enabled = false;

            btnListPayment.Visible = false;
            btnListTaxes.Visible = false;
            btPrint.Visible = false;
            btnReport.Visible = false;

            progname = prog;
            codeuser = code;
            username = name;
            toolStripStatusLabel1.Text = "База: " + ConnectionSettings.GetDatabase();
            toolStripStatusLabel2.Text = "Сервер: " + ConnectionSettings.GetServer();

            dgLordland.AutoGenerateColumns = false;
        }


        private void справочникЗданийToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var zdan = new Zdania() { ShowInTaskbar = false };
            //zdan.ShowInTaskbar = false;
            zdan.ShowDialog();
        }

        private void справочникЭтажейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Floor = new Floors() { ShowInTaskbar = false };
            Floor.ShowDialog();
        }

        private void справочникТиповПомещенийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tPremises = new Type_Premises() { ShowInTaskbar = false };
            tPremises.ShowDialog();
        }

        private void справочникОборудованияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Eqiup = new Equipment() { ShowInTaskbar = false };
            Eqiup.ShowDialog();
        }

        private void справочникСекцииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var Section = new Sections() { ShowInTaskbar = false };
                Section.ShowDialog();
            }
            catch (Exception r) { MessageBox.Show(r.Message.ToString()); }
        }

        private void справочникТиповОрганизацийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var too = new Type_o_o() { ShowInTaskbar = false };
            too.ShowDialog();
        }

        private void справочникОснованийЗаключенияДоговоровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var bas = new Basement() { ShowInTaskbar = false };
            bas.ShowDialog();
        }

        private void ShowAllButtons()
        {
            foreach (Control con in this.Controls)
            {
                if (con.GetType().ToString() == "System.Windows.Forms.Button")
                {
                    con.Visible = true;
                }
            }

            btnView.Visible = false;

            StatusUserForVisibleButtons();
        }

        private void StatusUserForVisibleButtons()
        {
            btnReport.Visible = false;

            btJournalSealSections.Visible = new List<string> { "СОА", "РКВ" }.Contains(TempData.Rezhim) && pListDoc.Visible;
            btAcceptDoc.Visible = new List<string> { "СОА", "РКВ", "КНТ" }.Contains(TempData.Rezhim) && pListDoc.Visible;
            btCopyDoc.Visible = new List<string> { "СОА", "РКВ" }.Contains(TempData.Rezhim) && pListDoc.Visible;
            btKntListTaxes.Visible = new List<string> { "КНТ", "РКВ", "Д" }.Contains(TempData.Rezhim) && pListDoc.Visible;
        }

        private void арендаторыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Text = "Аренда " +" Арендаторы " + username;
            this.Text = progname + " Арендаторы " /*+ codeuser*/ + " " + username;
            label5.Text = "Арендаторы";
            btAdd.Enabled = true;
            btExel.Enabled = true;
            iniTenant();
            pTenant.Show();
            pLordland.Hide();
            pListDoc.Hide();

            //показываем ВСЕ кнопки
            ShowAllButtons();

            //скрываем ненужные            
            btnListPayment.Visible = false;
            btnListTaxes.Visible = false;
            btnReport.Visible = false;

            if (new List<string> { "СБ6", "Д", "ПР", "КНТ" }.Contains(TempData.Rezhim))
            {
                btAdd.Visible = false;
                btDel.Visible = false;
                btEdit.Visible = false;

                btnView.Visible = true;
                btnView.Location = btEdit.Location;
            }





            t.SetToolTip(btPrint, "Отчет по арендатору");
        }

        private void pTenant_VisibleChanged(object sender, EventArgs e)
        {
            iniTenant();
            sName.Clear();
            sTenant.Clear();
            cbTenant.Checked = false;
        }

        private void mForm_Load(object sender, EventArgs e)
        {           
            //this.Text ="Аренда " + username;
            this.Text = progname + " " /*+ codeuser*/ + " " + username;
            TempData.Rezhim = Nwuram.Framework.Settings.User.UserSettings.User.StatusCode;
            dgLordland.AllowUserToResizeColumns = true;
            UserAccessAndElements();

            tsmiReport.Visible = new List<string> { "СОА", "РКВ", "КНТ", "СБ6", "Д" }.Contains(TempData.Rezhim);


            //if (TempData.Rezhim == "МН")
            //{
            //    справочникиToolStripMenuItem.Enabled = false;
            //    настройкиToolStripMenuItem.Enabled = false;
            //}
            //выгрузкаДокументовToolStripMenuItem.Visible = TempData.Rezhim == "АДМ";

            StatusUserForVisibleButtons();
        }


        private void UserAccessAndElements() {


            справочникЗданийToolStripMenuItem.Visible = new List<string> { "СОА", "РКВ", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim);
            справочникЭтажейToolStripMenuItem.Visible = new List<string> { "СОА", "РКВ", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim);
            справочникТиповПомещенийToolStripMenuItem.Visible = new List<string> { "СОА", "РКВ", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim);
            справочникОборудованияToolStripMenuItem.Visible = new List<string> { "СОА", "РКВ", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim);
            справочникСекцииToolStripMenuItem.Visible = new List<string> { "СОА", "РКВ", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim);
            справочникТиповОрганизацийToolStripMenuItem.Visible = new List<string> { "СОА", "РКВ", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim);
            справочникОснованийЗаключенияДоговоровToolStripMenuItem.Visible = new List<string> { "СОА", "РКВ", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim);
            справочникБанковToolStripMenuItem.Visible = new List<string> { "СОА", "РКВ", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim);
            справочникДолжностейToolStripMenuItem.Visible = new List<string> { "СОА", "РКВ", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim);
            справочникДопОплатToolStripMenuItem.Visible = new List<string> { "СОА", "РКВ", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim);
            справочникПриборовToolStripMenuItem.Visible = new List<string> { "СОА", "РКВ", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim);
            справочникОбъектовToolStripMenuItem.Visible = new List<string> { "СОА", "РКВ", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim);
            справочникРекламныхМестToolStripMenuItem.Visible = new List<string> { "СОА", "РКВ", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim);
            справочникЗемельныхУчастковToolStripMenuItem.Visible = new List<string> { "СОА", "РКВ", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim);
            справочникВидаДейтельностиToolStripMenuItem.Visible = new List<string> { "СОА", "РКВ", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim);
            справочникСкидокToolStripMenuItem.Visible = new List<string> { "СОА", "РКВ", "МНД", "ПР", "КНТ", "Д" }.Contains(TempData.Rezhim);
            //справочникиToolStripMenuItem.Visible = new List<string> { "СОА", "РКВ", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim);
            выгрузкаДокументовToolStripMenuItem.Visible = new List<string> { "РКВ" }.Contains(TempData.Rezhim);

            журналДолжниковToolStripMenuItem.Visible = new List<string> { "РКВ", "СОА", "Д" }.Contains(TempData.Rezhim);
            журналСъездовToolStripMenuItem.Visible = new List<string> { "РКВ", "СОА", "Д", "МНД" }.Contains(TempData.Rezhim);
            журналНачисленияПениToolStripMenuItem.Visible = new List<string> { "РКВ", "СОА", "Д" }.Contains(TempData.Rezhim);

            журналЕжемесячныхПлановToolStripMenuItem.Visible = new List<string> { "РКВ", "СОА", "Д","СБ6" }.Contains(TempData.Rezhim);            
            журналПланОтчётовToolStripMenuItem.Visible = new List<string> { "РКВ", "СОА", "Д", "СБ6" }.Contains(TempData.Rezhim);

            btnMassDiscounts.Visible = new List<string> { "РКВ", "СОА" }.Contains(TempData.Rezhim);


            //if (TempData.Rezhim == "РКВ") { }
            //if (TempData.Rezhim == "СОА") { }
            //if (TempData.Rezhim == "МНД") { }
            //if (TempData.Rezhim == "СБ6") { }
            //if (TempData.Rezhim == "Д") { }
            //if (TempData.Rezhim == "ПР") { }
            //if (TempData.Rezhim == "КНТ") { }
        }


        private void iniTenant()
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

            SRZA = _proc.getdate().AddDays(SRZA_days);

            if (cbTenant.Checked == true)
            {
                _Tenant = _proc.GetTetant(0);
            }
            else
            {
                _Tenant = _proc.GetTetant(1);
            }

            bds.DataSource = _Tenant;
            dgTenant.DataSource = bds;
            Tenent.DisplayIndex = 0;
            Pred.DisplayIndex = 1;
            Locate.DisplayIndex = 2;
            cEmail.DisplayIndex = 3;
            cPhone.DisplayIndex = 4;
            remark.DisplayIndex = 5;
            FilterDataView();
        }

        private void FilterDataView()
        {
            try
            {
                string Fstring, Fstring1, Fstring2, Fstring3, Fstring4;
                if (sTenant.Text == "")
                { Fstring = "*"; }
                else Fstring = sTenant.Text;
                if (sName.Text == "")
                { Fstring1 = "*"; }
                else Fstring1 = sName.Text;
                if (sPlace.Text == "")
                { Fstring2 = "*"; }
                else Fstring2 = sPlace.Text;
                if (sEmail.Text == "")
                { Fstring3 = "*"; }
                else Fstring3 = sEmail.Text;
                if (sPhone.Text == "")
                { Fstring4 = "*"; }
                else Fstring4 = sPhone.Text;
                DataTable dt = _Tenant;
                view = dt.DefaultView;
                StringBuilder sb = new StringBuilder();
                sb.Append("aren like '%" + Fstring + "%'");
                sb.Append(" and pred like '%" + Fstring1 + "%'");
                sb.Append(" and loc like '%" + Fstring2 + "%'");
                if (sEmail.Text.Length > 0)
                    sb.Append(" and email like '%" + Fstring3 + "%'");
                sb.Append(" and Work_phone like '%" + Fstring4 + "%'");
                view.RowFilter = sb.ToString();
            }
            catch (Exception) { }
        }

        private void sTenant_TextChanged(object sender, EventArgs e)
        {
            FilterDataView();
        }

        private void sName_TextChanged(object sender, EventArgs e)
        {
            FilterDataView();
        }

        private int dayLimitToEnd = 0;
        private void договорыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Text = "Аренда " + " Список договоров " + username;
            this.Text = progname + " Список договоров " /*+ codeuser*/ + " " + username;
            label5.Text = "Список договоров";
            iniListDoc();
            pTenant.Hide();
            pListDoc.Show();
            pLordland.Hide();

            DataTable conf = _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "", "");
            if (conf != null && conf.Rows.Count > 0)
            {
                EnumerableRowCollection<DataRow> rowCollect = conf.AsEnumerable()
                    .Where(r => r.Field<string>("id_value").Trim().Equals("SRZA"));
                dayLimitToEnd = 0;
                if (rowCollect.Count() > 0)
                    dayLimitToEnd = int.Parse(rowCollect.First()["value"].ToString());
            }
            //показываем ВСЕ кнопки и прячем кнопку просмотра
            ShowAllButtons();

            //скрываем ненужные            
            btnListPayment.Visible = 
            btnListTaxes.Visible = !new List<string> { "МНД" }.Contains(TempData.Rezhim);
            btReportTenant.Visible = new List<string> { "РКВ", "МНД", "СОА" }.Contains(TempData.Rezhim);
            btDicDiscount.Visible = new List<string> { "РКВ", "Д", "СОА" }.Contains(TempData.Rezhim);

            //скрываем ненужные
            // ---на этой вкладке все нужны

            //if (TempData.Rezhim == "ПР")
            if (new List<string> { "СБ6", "Д", "ПР", "КНТ" }.Contains(TempData.Rezhim))
            {
                btAdd.Visible = false;
                btDel.Visible = false;
                btEdit.Visible = false;

                btnView.Visible = true;
                btnView.Location = btEdit.Location;
            }

            if (dgListDoc.Rows.Count == 0)
            {
                btDel.Enabled = false;
                btEdit.Enabled = false;
                btPrint.Enabled = false;
                btnListPayment.Enabled = false;
                btnListTaxes.Enabled = false;
                btnView.Enabled = false;
                btCopyDoc.Enabled = false;
            }
            else
            {
                btDel.Enabled = true;
                btEdit.Enabled = true;
                btPrint.Enabled = true;
                btnListPayment.Enabled = true;
                btnListTaxes.Enabled = true;
                btnView.Enabled = true;
                btCopyDoc.Enabled = true;
            }
            btAdd.Enabled = true;
            btExel.Enabled = true;


            t.SetToolTip(btPrint, "Выгрузить в Excel");
            t.SetToolTip(btnReport, "Отчет по неоплаченным дополнительным оплатам");

            if (new List<string> { "СОА", "РКВ" }.Contains(TempData.Rezhim))
            {
                btAcceptDoc.Image = global::Arenda.Properties.Resources.pict_ok;
                ToolTip tp = new ToolTip();
                tp.SetToolTip(btAcceptDoc, "Подтвердить договор");
            }
            else if (new List<string> { "КНТ" }.Contains(TempData.Rezhim))
            {
                btAcceptDoc.Image = global::Arenda.Properties.Resources.DeleteHS;
                ToolTip tp = new ToolTip();
                tp.SetToolTip(btAcceptDoc, "Отменить подтверждение договора");
            }


        }

        private void pListDoc_VisibleChanged(object sender, EventArgs e)
        {
            iniListDoc();
            dateTimePicker1.Value = DateTime.Now.AddDays(-7);
            DatesForbidden();
        }

        private void DatesForbidden()
        {
            dateTimePicker1.MaxDate = dateTimePicker2.Value;
            dateTimePicker2.MinDate = dateTimePicker1.Value;
        }

        private void iniListDoc()
        {
            object tmp = 0;
            if (cmbObj.SelectedValue != null)
                tmp = cmbObj.SelectedValue;
            object tmp1 = 0;
            if (cmbType.SelectedValue != null)
                tmp1 = cmbType.SelectedValue;
            _ListDoc = _proc.GetListDoc();
            bds1.DataSource = _ListDoc;
            dgListDoc.DataSource = bds1;
            //dgLordland.DataSource = dbs2;
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("cName");
            dt.Columns.Add("Comment");
            dt.Columns.Add("isActive");
            //dt.Columns.Add("Used");
            DataTable dtObj = _proc.GetObjects();
            DataRow dr = dt.Rows.Add();
            dr["id"] = 0;
            dr["cName"] = "Все объекты";
            dr["isActive"] = 1;
            foreach (DataRow r in dtObj.Rows)
            {
                DataRow dro = dt.Rows.Add();
                dro["id"] = r["id"];
                dro["cName"] = r["cName"];
                dro["Comment"] = r["Comment"];
                dro["isActive"] = r["isActive"];
                //dro["Used"] = r["Used"];
            }
            dt.DefaultView.RowFilter = "isActive = 1";
            //dtObj.DefaultView.Sort = "cName";
            cmbObj.DataSource = dt;
            cmbObj.SelectedValue = tmp;
            DataTable dtTypes = _proc.GetContractTypes();
            DataRow dr1 = dtTypes.Rows.Add();
            dr1["id"] = 0;
            dr1["TypeContract"] = "Все типы";
            dr1["isActive"] = 1;
            dtTypes.DefaultView.RowFilter = "isActive = 1";
            dtTypes.DefaultView.Sort = "id";
            cmbType.DataSource = dtTypes;
            cmbType.SelectedValue = tmp1;

            for (int i = 0; i <= dtTypes.Rows.Count - 1; i++)
            {
                Label l = new Label();
                l.Font = cmbType.Font;
                l.AutoSize = true;
                l.Text = dtTypes.Rows[i][1].ToString();
                l.Visible = false;
                this.Controls.Add(l);
                if (l.Width > cmbType.DropDownWidth)
                    cmbType.DropDownWidth = l.Width;
                this.Controls.Remove(l);
            }
            FilterDataView1();
        }

        private void FilterDataView1()
        {
            try
            {
                string Fstring, Fstring1, Fstring2, Fstring4, Fstring5;
                if (tbNumber.Text == "")
                { Fstring4 = "*"; }
                else Fstring4 = tbNumber.Text.Trim(); ;

                if (tbDoc.Text == "")
                { Fstring = "*"; }
                else Fstring = tbDoc.Text;

                if (tbPlace.Text == "")
                { Fstring5 = "*"; }
                else Fstring5 = tbPlace.Text;

                Fstring1 = dateTimePicker1.Value.ToShortDateString();
                Fstring2 = dateTimePicker2.Value.ToShortDateString();

                if (Convert.ToDateTime(Fstring1) > Convert.ToDateTime(Fstring2))
                {
                    // MessageBox.Show("Ошибка в выборе дат", "Ошибка");

                }
                else
                {
                    DataTable dt = _ListDoc;
                    view = dt.DefaultView;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Арендатор like '%" + Fstring + "%'");
                    sb.Append(" and № like '%" + Fstring4 + "%'");

                    /*sb.Append(" and Дата >= '" + Fstring1 + "'");
                    sb.Append(" and Дата <= '" + Fstring2 + "'");*/

                    /*sb.Append(" and ((Начало <= '" + Fstring1 + "'");
                    sb.Append(" and Конец >= '" + Fstring1 + "') OR ");

                    sb.Append(" (Начало <= '" + Fstring2 + "'");
                    sb.Append(" and Конец >= '" + Fstring2 + "'))");*/

                    //sb.Append(" and ((Начало <= '" + Fstring1 + "'");
                    //sb.Append(" and Конец >= '" + Fstring1 + "') OR ");

                    //sb.Append(" (Начало <= '" + Fstring2 + "'");
                    //sb.Append(" and Конец >= '" + Fstring2 + "'))");

                    sb.Append(" and (Начало <= '" + Fstring2 + "'");
                    sb.Append(" and Конец >= '" + Fstring1 + "')");

                    if (!chbCancelDoc.Checked)
                        sb.Append(" and isCancelDoc = 0");

                    if (int.Parse(cmbObj.SelectedValue.ToString()) != 0)
                        sb.Append(" and id_ObjectLease = " + cmbObj.SelectedValue.ToString());
                    if ((int)cmbType.SelectedValue != 0)
                        sb.Append(" and id_TypeContract = " + cmbType.SelectedValue.ToString());
                    if (cbLordland.SelectedValue != null && int.Parse(cbLordland.SelectedValue.ToString()) != 0)
                        sb.Append(" and id_lord = " + cbLordland.SelectedValue.ToString());
                    sb.Append(" and Место like '%" + Fstring5 + "%'");
                    view.RowFilter = sb.ToString();
                }

                dgListDoc_SelectionChanged(null, null);
            }
            catch (Exception) { }
        }

        private void tbDoc_TextChanged(object sender, EventArgs e)
        {
            FilterDataView1();
        }

        private void арендодателиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Text = "Аренда " + " Арендодатели " + username;
            this.Text = progname + " Арендодатели " /*+ codeuser*/ + " " + username;
            label5.Text = "Арендодатели";
            iniLandLord();
            pTenant.Hide();
            pLordland.Show();
            pListDoc.Hide();

            //показываем ВСЕ кнопки и прячем кнопку просмотра
            ShowAllButtons();

            //скрываем ненужные
            btnListPayment.Visible = false;
            btnListTaxes.Visible = false;
            btPrint.Visible = false;
            

            //if (TempData.Rezhim == "ПР")
            if (new List<string> { "СБ6", "Д", "ПР", "КНТ", "МНД" }.Contains(TempData.Rezhim))
            {
                btAdd.Visible = false;
                btDel.Visible = false;
                btEdit.Visible = false;

                btnView.Visible = true;
                btnView.Location = btEdit.Location;
            }

            btAdd.Enabled = true;
            btExel.Enabled = true;
            btPrint.Enabled = true;

            t.SetToolTip(btPrint, "Сформировать план-отчет");
            t.SetToolTip(btnReport, "Ежедневный отчет по арендодателю");
        }

        private void pLordland_VisibleChanged(object sender, EventArgs e)
        {
            iniLandLord();
            tbLandlord.Clear();
            tbLandpred.Clear();
            chbLandlord.Checked = false;
        }

        private void iniLandLord()
        {
            object tmp = 0;
            if (cmbObject.SelectedValue != null)
                tmp = cmbObject.SelectedValue;
            if (chbLandlord.Checked == true)
            {

                _LandLord = _proc.GetLandLord(0);
            }
            else
                _LandLord = _proc.GetLandLord(1);

            dbs2.DataSource = _LandLord;
            dgLordland.DataSource = dbs2;
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("cName");
            dt.Columns.Add("Comment");
            dt.Columns.Add("isActive");
            //dt.Columns.Add("Used");
            DataTable dtObj = _proc.GetObjects();
            DataRow dr = dt.Rows.Add();
            dr["id"] = 0;
            dr["cName"] = "Все объекты";
            dr["isActive"] = 1;
            foreach (DataRow r in dtObj.Rows)
            {
                DataRow dro = dt.Rows.Add();
                dro["id"] = r["id"];
                dro["cName"] = r["cName"];
                dro["Comment"] = r["Comment"];
                dro["isActive"] = r["isActive"];
                //dro["Used"] = r["Used"];
            }
            dt.DefaultView.RowFilter = "isActive = 1";
            //dtObj.DefaultView.Sort = "cName";
            cmbObject.DataSource = dt;
            cmbObject.SelectedValue = tmp;
            FilterDataView2();

            DateTime dt1;
            DateTime dt2;

            for (int i = 1; i < dgLordland.Rows.Count;)
            {
                if (dgLordland.Rows[i - 1].Cells[1].Value.ToString() != dgLordland.Rows[i].Cells[1].Value.ToString() || dgLordland.Rows[i - 1].Cells[2].Value.ToString() != dgLordland.Rows[i].Cells[2].Value.ToString() || dgLordland.Rows[i - 1].Cells["Adress_trade"].Value.ToString() != dgLordland.Rows[i].Cells["Adress_trade"].Value.ToString())
                {
                    i++;
                }
                else
                {
                    if (DateTime.TryParse(dgLordland.Rows[i - 1].Cells[6].Value.ToString(), out dt1) && DateTime.TryParse(dgLordland.Rows[i].Cells[6].Value.ToString(), out dt2))
                    {
                        if (dt1 > dt2)
                            dgLordland.Rows.RemoveAt(i);
                        else
                            dgLordland.Rows.RemoveAt(i - 1);
                    }
                    else
                    {
                        i++;
                    }
                }

                if (checkBox1.Checked == true)
                {
                    if (dgLordland.Rows[i - 1].Cells[5].Value.ToString() == "" || Convert.ToDateTime(dgLordland.Rows[i - 1].Cells[7].Value) > DateTime.Now)
                    {
                        dgLordland.Rows[i - 1].DefaultCellStyle.BackColor = Color.LightBlue;
                    }
                }
                else { dgLordland.Rows[i - 1].DefaultCellStyle.BackColor = Color.White; }
            }
        }

        private void FilterDataView2()
        {
            try
            {
                string Fstring, Fstring1;
                if (tbLandlord.Text == "")
                { Fstring = "*"; }
                else Fstring = tbLandlord.Text;
                if (tbLandpred.Text == "")
                { Fstring1 = "*"; }
                else Fstring1 = tbLandpred.Text;
                DataTable dt = _LandLord;
                view = dt.DefaultView;
                StringBuilder sb = new StringBuilder();
                sb.Append("Арендодатель like '%" + Fstring + "%'");
                sb.Append(" and Представитель like '%" + Fstring1 + "%'");
                if (int.Parse(cmbObject.SelectedValue.ToString()) != 0)
                    sb.Append(" and id_ObjectLease = " + cmbObject.SelectedValue.ToString());
                view.RowFilter = sb.ToString();
            }
            catch (Exception) { }

        }

        private void tbLandlord_TextChanged(object sender, EventArgs e)
        {
            //iniLandLord();
            FilterDataView2();
        }

        private void tbLandpred_TextChanged(object sender, EventArgs e)
        {
            //iniLandLord();
            FilterDataView2();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DatesForbidden();
            FilterDataView1();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DatesForbidden();
            FilterDataView1();
        }

        private void cbLordland_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!load)
            {
                FilterDataView1();
                ltprint = cbLordland.Text;
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Выйти из программы?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (d == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void chbLandlord_CheckedChanged(object sender, EventArgs e)
        {
            iniLandLord();
        }

        private void cbTenant_CheckedChanged(object sender, EventArgs e)
        {
            iniTenant();
        }

        private void dgLordland_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                var editLord = new AddEditTenant(Convert.ToInt32(dgLordland.SelectedRows[0].Cells[0].Value.ToString()), false, true);
                editLord.ShowDialog();
                iniLandLord();
            }
        }

        private void btDelSec_Click(object sender, EventArgs e)
        {
            if (pListDoc.Visible == true)
            {
                int _id = Convert.ToInt32(dgListDoc.SelectedRows[0].Cells[0].Value);
                bool _IsConfirmed = (bool)dgListDoc.SelectedRows[0].Cells["cIsConfirmed"].Value;
                if (_IsConfirmed)
                {
                    MessageBox.Show(TempData.centralText("Договор подтверждён.\nУдаление договора невозможно.\n"), "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                DataTable dtPayments = new DataTable();
                dtPayments = _proc.GetPayments(_id);

                if (dtPayments.Rows.Count > 0)
                {
                    MessageBox.Show("По договору есть оплаты. Удаление документа невозможно.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                delDoc();
                iniListDoc();
            }

            if (pLordland.Visible == true)
            {
                int _id = Convert.ToInt32(dgLordland.SelectedRows[0].Cells[0].Value);
                if (_proc.BefLordTen(_id, "lord").Rows[0][0].ToString() == "0")
                {
                    delLord(_id); iniLandLord();

                }
                else
                {
                    if (dgLordland.SelectedRows[0].Cells[4].Value.ToString() == "False")
                    {
                        if (MessageBox.Show("Сделать запись активной?", "Внимание", MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {

                            Logging.StartFirstLevel(540);
                            Logging.Comment("Произведена смена статуса у арендодателя на активный");

                            Logging.Comment("Арендадатель ID: " + dgLordland.CurrentRow.Cells["id_Landlord"].Value.ToString() + " ; Наименование: " + dgLordland.CurrentRow.Cells["Landlord"].Value.ToString());
                            Logging.Comment("ФИО представителя: " + dgLordland.CurrentRow.Cells["LordPreds"].Value.ToString());
                            Logging.Comment("Адрес : " + dgLordland.CurrentRow.Cells["Adress"].Value.ToString());
                            Logging.Comment("Адрес сдаваемого помещения : " + dgLordland.CurrentRow.Cells["Adress_trade"].Value.ToString());

                            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                            + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                            Logging.StopFirstLevel();

                            _proc.Active(_id, 1);
                            iniLandLord();
                        }
                    }
                    else

                        if (MessageBox.Show("Выбранный арендодатель используется в договорах. \n Удалить запись нельзя.\nСделать запись неактивной?", "Внимание", MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {

                        Logging.StartFirstLevel(540);
                        Logging.Comment("Произведена смена статуса у арендодателя на неактивный");

                        Logging.Comment("Арендадатель ID: " + dgLordland.CurrentRow.Cells["id_Landlord"].Value.ToString() + " ; Наименование: " + dgLordland.CurrentRow.Cells["Landlord"].Value.ToString());
                        Logging.Comment("ФИО представителя: " + dgLordland.CurrentRow.Cells["LordPreds"].Value.ToString());
                        Logging.Comment("Адрес : " + dgLordland.CurrentRow.Cells["Adress"].Value.ToString());
                        Logging.Comment("Адрес сдаваемого помещения : " + dgLordland.CurrentRow.Cells["Adress_trade"].Value.ToString());

                        Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                        + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                        Logging.StopFirstLevel();

                        _proc.Active(_id, 0);
                        iniLandLord();
                    }
                };
            }

            if (pTenant.Visible == true)
            {
                int _id = Convert.ToInt32(dgTenant.SelectedRows[0].Cells["id"].Value);
                string _Tenent = dgTenant.SelectedRows[0].Cells["Tenent"].Value.ToString();
                string _Pred = dgTenant.SelectedRows[0].Cells["Pred"].Value.ToString();
                string _Locate = dgTenant.SelectedRows[0].Cells["Locate"].Value.ToString();
                string _remark = dgTenant.SelectedRows[0].Cells["remark"].Value.ToString();

                if (_proc.BefLordTen(_id, "ten").Rows[0][0].ToString() == "0")
                {


                    delTen(_id);
                    iniTenant();
                }
                else
                {
                    if (dgTenant.SelectedRows[0].Cells["isActive"].Value.ToString() == "False")
                    {
                        if (MessageBox.Show("Сделать запись активной?", "Внимание", MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {

                            Logging.StartFirstLevel(540);
                            Logging.Comment("Произведена смена статуса записи Арендатора в ПО «Аренда.Остров» на «активный»");
                            Logging.Comment("ID: " + _id);
                            Logging.Comment("Наименование арендатора: " + _Tenent);
                            Logging.Comment("ФИО представителя: " + _Pred);
                            Logging.Comment("Местоположение : " + _Locate);
                            Logging.Comment("Примечание: " + _remark);

                            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                            + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                            Logging.StopFirstLevel();

                            _proc.Active(_id, 1); iniTenant();
                        }
                    }
                    else
                        if (MessageBox.Show("Выбранный арендатор используется в договорах. \n Удалить запись нельзя.\nСделать запись неактивной?", "Внимание", MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        Logging.StartFirstLevel(540);
                        Logging.Comment("Произведена смена статуса записи Арендатора в ПО «Аренда.Остров» на «неактивный»");
                        Logging.Comment("ID: " + _id);
                        Logging.Comment("Наименование арендатора: " + _Tenent);
                        Logging.Comment("ФИО представителя: " + _Pred);
                        Logging.Comment("Местоположение : " + _Locate);
                        Logging.Comment("Примечание: " + _remark);

                        Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                        + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                        Logging.StopFirstLevel();

                        _proc.Active(_id, 0); iniTenant();
                    }
                };
            }
        }

        private void delDoc()
        {
            if (MessageBox.Show("Удалить запись? \n" + dgListDoc.SelectedRows[0].Cells[1].Value.ToString() + " " + dgListDoc.SelectedRows[0].Cells[2].Value.ToString() + " ", "Внимание", MessageBoxButtons.YesNo,
              MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                dt = new DataTable();
                dt = _proc.GetLD(Convert.ToInt32(dgListDoc.SelectedRows[0].Cells[0].Value.ToString()));
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Удаление договора невозможно. Договор уже удален.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int _id = Convert.ToInt32(dgListDoc.SelectedRows[0].Cells[0].Value);
                    _proc.DelDTL(_id, "listdoc");

                    Logging.StartFirstLevel(533);
                    Logging.Comment("ID: " + _id);
                    Logging.Comment("Дата документа: " + dgListDoc.SelectedRows[0].Cells["Date"].Value.ToString());
                    Logging.Comment("Арендатель Наименование: " + dgListDoc.SelectedRows[0].Cells["tTenant"].Value.ToString());
                    Logging.Comment("Арендодатель Наименование: " + dgListDoc.SelectedRows[0].Cells["id_lord"].Value.ToString());
                    Logging.Comment("№ договора: " + dgListDoc.SelectedRows[0].Cells["number"].Value.ToString());
                    Logging.Comment("Начало: " + dgListDoc.SelectedRows[0].Cells["DataStart"].Value.ToString());
                    Logging.Comment("Конец: " + dgListDoc.SelectedRows[0].Cells["DataEnd"].Value.ToString());
                    Logging.Comment("Место: " + dgListDoc.SelectedRows[0].Cells["ALocate"].Value.ToString());

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                      + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();
                }
            }
        }

        private void delTen(int id)
        {
            if (MessageBox.Show("Удалить запись? \n" + dgTenant.SelectedRows[0].Cells["Tenent"].Value.ToString() + " " + dgTenant.SelectedRows[0].Cells["Pred"].Value.ToString() + " ", "Внимание", MessageBoxButtons.YesNo,
              MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                dt = new DataTable();
                dt = _proc.getLT(id);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Удаление арендатора невозможно. Арендатор уже удален.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string _Tenent = dgTenant.SelectedRows[0].Cells["Tenent"].Value.ToString();
                    string _Pred = dgTenant.SelectedRows[0].Cells["Pred"].Value.ToString();
                    string _Locate = dgTenant.SelectedRows[0].Cells["Locate"].Value.ToString();
                    string _remark = dgTenant.SelectedRows[0].Cells["remark"].Value.ToString();

                    Logging.StartFirstLevel(1400);
                    Logging.Comment("ID: " + id);
                    Logging.Comment("Наименование арендатора: " + _Tenent);
                    Logging.Comment("ФИО представителя: " + _Pred);
                    Logging.Comment("Местоположение : " + _Locate);
                    Logging.Comment("Примечание: " + _remark);

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                      + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();

                    _proc.DelDTL(id, "land_tenant");
                }
            }
        }

        private void delLord(int id)
        {
            if (MessageBox.Show("Удалить запись? \n" + dgLordland.SelectedRows[0].Cells[1].Value.ToString() + " " + dgLordland.SelectedRows[0].Cells[2].Value.ToString() + " ", "Внимание", MessageBoxButtons.YesNo,
              MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                dt = new DataTable();
                dt = _proc.getLT(id);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Удаление арендодателя невозможно. Арендодатель уже удален.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Logging.StartFirstLevel(1404);

                    Logging.Comment("Произведено удаление арендодателя");
                    Logging.Comment("Арендодатель ID: " + dgLordland.CurrentRow.Cells["id_Landlord"].Value.ToString() + " ; Наименование: " + dgLordland.CurrentRow.Cells["Landlord"].Value.ToString());
                    Logging.Comment("ФИО представителя: " + dgLordland.CurrentRow.Cells["LordPreds"].Value.ToString());
                    Logging.Comment("Адрес : " + dgLordland.CurrentRow.Cells["Adress"].Value.ToString());
                    Logging.Comment("Адрес сдаваемого помещения : " + dgLordland.CurrentRow.Cells["Adress_trade"].Value.ToString());

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                      + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();

                    _proc.DelDTL(id, "land_tenant");
                }
            }
        }

        private void dgLordland_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (pLordland.Visible == true)
            {
                if (dgLordland.Rows.Count == 0)
                {
                    btDel.Enabled = false;
                    btEdit.Enabled = false;
                    btnView.Enabled = false;
                    btnReport.Enabled = false;
                }
                else
                {
                    btDel.Enabled = true;
                    btEdit.Enabled = true;
                    btnView.Enabled = true;

                    if ((dgLordland.SelectedRows != null) && (dgLordland.SelectedRows.Count > 0))
                    {
                        if (dgLordland.SelectedRows[0].Cells["outReport"].Value.ToString().ToLower() == "true")
                        {
                            //btnReport.Enabled = true;
                        }
                        else
                        {
                            btnReport.Enabled = false;
                        }
                    }
                    else
                    {
                        btnReport.Enabled = false;
                    }
                }
            }
        }

        private void dgListDoc_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (pListDoc.Visible == true)
            {
                if (dgListDoc.Rows.Count == 0)
                {
                    btDel.Enabled = false;
                    btEdit.Enabled = false;
                    btPrint.Enabled = false;
                    btnListPayment.Enabled = false;
                    btnListTaxes.Enabled = false;
                    btnView.Enabled = false;
                    btnReport.Enabled = false;
                    btAcceptDoc.Enabled = false;
                    btCopyDoc.Enabled = false;
                }
                else
                {
                    btDel.Enabled = true;
                    btEdit.Enabled = true;
                    btPrint.Enabled = true;
                    btnListPayment.Enabled = true;
                    btnListTaxes.Enabled = true;
                    btnView.Enabled = true;
                    btAcceptDoc.Enabled = true;
                    btCopyDoc.Enabled = true;
                    //btnReport.Enabled = true;
                }
            }
        }

        private void dgTenant_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (pTenant.Visible == true)
            {
                if (dgTenant.Rows.Count == 0)
                {
                    btDel.Enabled = false;
                    btEdit.Enabled = false;
                    btnView.Enabled = false;
                    btPrint.Enabled = false;
                }
                else
                {
                    btDel.Enabled = true;
                    btEdit.Enabled = true;
                    btnView.Enabled = true;

                    if ((dgTenant.SelectedRows != null) && (dgTenant.SelectedRows.Count > 0))
                    {
                        if (dgTenant.SelectedRows[0].Cells["outReportTen"].Value.ToString().ToLower() == "true")
                        {
                            btPrint.Enabled = true;
                        }
                        else
                        {
                            btPrint.Enabled = false;
                        }
                    }
                    else
                    {
                        btPrint.Enabled = false;
                    }
                }
            }
        }

        private void dgTenant_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            dgTenant.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
            Color rColor = Color.White;

            if (_Tenant.DefaultView[e.RowIndex]["isActive"].ToString() == "False")
            {
                rColor = picUnactive.BackColor;
            }
            else
            {
                if (_Tenant.DefaultView[e.RowIndex]["Date_of_Conclusion"].ToString() != "")
                {
                    if (_Tenant.DefaultView[e.RowIndex]["Stop_Date"].ToString() != "")
                    {
                        if (DateTime.Parse(_Tenant.DefaultView[e.RowIndex]["Stop_Date"].ToString()) <= SRZA)
                        {
                            //dgTenant.Rows[e.RowIndex].DefaultCellStyle.BackColor = picConEnding.BackColor;
                        }
                        else
                        {
                            rColor = Color.White;
                        }
                    }
                    else
                    {
                        rColor = Color.White;
                    }
                }
                else
                {
                    //if (_Tenant.DefaultView[e.RowIndex]["Prev_Date_Con"].ToString() != "")
                    //{
                    //    //dgTenant.Rows[e.RowIndex].DefaultCellStyle.BackColor = picConEnded.BackColor;
                    //}
                    //else
                    //{
                    //    rColor = picUnContract.BackColor;
                    //}
                    if (!(bool)_Tenant.DefaultView[e.RowIndex]["isConfirmed"]) rColor = picUnContract.BackColor;
                }
            }

            dgTenant.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor =
                           dgTenant.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
        }

        private void dgLordland_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (_LandLord.DefaultView[e.RowIndex]["isActive"].ToString() == "False")
            {
                dgLordland.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Coral;
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (pListDoc.Visible == true)
            {
                var doc = new AddeditDoc();
                doc.ShowDialog();
                iniListDoc();
            }
            if (pLordland.Visible == true)
            {
                var lord = new AddEditTenant(false);
                lord.ShowDialog();
                iniLandLord();
            }

            if (pTenant.Visible == true)
            {
                var ten = new AddEditTenant(true);
                ten.ShowDialog();
                iniTenant();
            }
        }

        private void NewWindow()
        {
            var doc = new AddeditDoc();
            doc.ShowDialog();

        }

        private void sTenant_KeyPress(object sender, KeyPressEventArgs e)
        {
            lockSimbols(e);
        }

        private void lockSimbols(KeyPressEventArgs e)
        {
            /*
            Regex pat = new Regex(@"[\b]|[\w]|[\s]");
            bool b = pat.IsMatch(e.KeyChar.ToString());
            if (b == false)
            {
                e.Handled = true;
            }
            */
        }

        private void sName_KeyPress(object sender, KeyPressEventArgs e)
        {
            lockSimbols(e);
        }

        private void tbLandlord_KeyPress(object sender, KeyPressEventArgs e)
        {
            lockSimbols(e);
        }

        private void tbLandpred_KeyPress(object sender, KeyPressEventArgs e)
        {
            lockSimbols(e);
        }

        private void tbDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            lockSimbols(e);
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (pListDoc.Visible == true)
            {
                dt = new DataTable();
                dt = _proc.GetLD(Convert.ToInt32(dgListDoc.SelectedRows[0].Cells[0].Value.ToString()));
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Редактирование договора невозможно. Договор удален.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    bool isConfirmed = (bool)dgListDoc.SelectedRows[0].Cells["cisConfirmed"].Value;
                    var editDoc = new AddeditDoc(Convert.ToInt32(dgListDoc.SelectedRows[0].Cells[0].Value.ToString()), false, isConfirmed);
                    editDoc.ShowDialog();
                }
                iniListDoc();
            }
            if (pLordland.Visible == true)
            {
                dt = new DataTable();
                dt = _proc.getLT(Convert.ToInt32(dgLordland.SelectedRows[0].Cells["id_landlord"].Value));

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Редактирование арендодателя невозможно. Арендодатель удален.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    iniLandLord();
                }
                else
                {
                    if (dgLordland.SelectedRows[0].Cells[4].Value.ToString() == "False")
                    {
                        MessageBox.Show("Нельзя редактировать эту запись", "Внимание!");
                    }
                    else
                    {
                        var editLord = new AddEditTenant(Convert.ToInt32(dgLordland.SelectedRows[0].Cells[0].Value.ToString()), false, false);
                        editLord.ShowDialog();
                        iniLandLord();
                    }
                }
            }
            if (pTenant.Visible == true)
            {
                dt = new DataTable();
                dt = _proc.getLT(Convert.ToInt32(dgTenant.SelectedRows[0].Cells["id"].Value));
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Редактирование арендатора невозможно. Арендатор удален.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    iniTenant();
                }
                else
                {
                    if (dgTenant.SelectedRows[0].Cells["isActive"].Value.ToString() == "False")
                    {
                        MessageBox.Show("Нельзя редактировать эту запись", "Внимание!");
                    }
                    else
                    {
                        var editTen = new AddEditTenant(Convert.ToInt32(dgTenant.SelectedRows[0].Cells["id"].Value.ToString()), true, false);
                        editTen.ShowDialog();
                        iniTenant();
                    }
                }
            }
        }

        private void btExel_Click(object sender, EventArgs e)
        {
            if (nullPrint())
            {
                if (!pListDoc.Visible && !pLordland.Visible)
                {
                    var fd = new SaveFileDialog { Filter = @"Файлы Excel|*.xls" };
                    fd.ShowDialog();
                    if (fd.FileName.Trim().Length == 0)
                    {
                        return;
                    }
                    _fileName = fd.FileName.Trim();
                }


                if (pListDoc.Visible == true)
                {

                    Logging.StartFirstLevel(472);
                    Logging.Comment("Выгрузка отчета со списком договоров в Excel файл");

                    //Logging.Comment("Файл: " + _fileName);

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();

                    prbExcel.Visible = true;
                    this.Enabled = false;
                    PrintExel("Agreements");
                    prbExcel.Visible = false;
                    this.Enabled = true;
                }
                if (pLordland.Visible == true)
                {

                    MyMessageBox.MyMessageBox mMSG = new MyMessageBox.MyMessageBox("Выберите тип выгружаемого отчёта", "Выгрузить в Excel", MyMessageBox.MessageBoxButtons.YesNoCancel, new List<string>(new string[] { "Список аренд", "Реквизиты аренд", "Отмена" }));
                    
                    DialogResult dResult = mMSG.ShowDialog();

                    if (dResult == DialogResult.Cancel)return;

                    if (dResult == DialogResult.Yes)
                    {
                        var fd = new SaveFileDialog { Filter = @"Файлы Excel|*.xls" };
                        fd.ShowDialog();
                        if (fd.FileName.Trim().Length == 0)
                        {
                            return;
                        }
                        _fileName = fd.FileName.Trim();

                        Logging.StartFirstLevel(472);
                        Logging.Comment("Выгрузка отчета со списком арендадателей в Excel файл");

                        Logging.Comment("Файл: " + _fileName);

                        Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                        + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                        Logging.StopFirstLevel();

                        prbExcel.Visible = true;
                        this.Enabled = false;
                        bgwExcel.RunWorkerAsync(new object[] { "Landlord" });
                    }

                    if (DialogResult.No == dResult)
                    {
                        dt = new DataTable();
                        dt = _proc.getLT(Convert.ToInt32(dgLordland.SelectedRows[0].Cells["id_landlord"].Value));

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Арендодатель удален.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            iniLandLord();
                            return;
                        }

                        Reports.print.printUpd(dt, Convert.ToInt32(dgLordland.SelectedRows[0].Cells["id_landlord"].Value));
                        return;
                    }
                }
                if (pTenant.Visible == true)
                {
                    Logging.StartFirstLevel(472);
                    Logging.Comment("Выгрузка отчета со списком арендаторов в Excel файл");

                    Logging.Comment("Файл: " + _fileName);

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();

                    prbExcel.Visible = true;
                    this.Enabled = false;
                    bgwExcel.RunWorkerAsync(new object[] { "Tenant" });
                }
            }
            else { MessageBox.Show("Нет данных для печати", "Ошибка"); }
        }

        private void PrintExel(string template)
        {
            int color;
            #region Арендаторы
            if (template == "Tenant")
            {
                Exc.Application appExc = new Exc.Application();
                appExc.DisplayAlerts = false;
                appExc.Visible = false;
                appExc.SheetsInNewWorkbook = 1;
                Exc.Workbook book = appExc.Workbooks.Add(1);
                Exc.Worksheet sheet = (Exc.Worksheet)book.Worksheets[1];

                sheet.Cells[1, 1] = "Справочник арендаторов";
                sheet.get_Range("A1", "F1").Merge();
                sheet.get_Range("A1", "F1").Font.Size = 16;
                sheet.get_Range("A1", "F1").Font.Bold = true;
                sheet.get_Range("A1", "F1").BorderAround();
                sheet.get_Range("A1", "F1").HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;

                sheet.Cells[3, 1] = "Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername;
                sheet.get_Range("A3", "F3").Merge();
                sheet.Cells[4, 1] = "Дата выгрузки: " + DateTime.Now;
                sheet.get_Range("A4", "F4").Merge();
                sheet.get_Range("A3", "F4").Font.Bold = true;

                sheet.Cells[6, 1] = "Арендатор";
                sheet.Cells[6, 2] = "Представитель";
                sheet.Cells[6, 3] = "Местоположение";
                sheet.Cells[6, 4] = "Электронная почта";
                sheet.Cells[6, 5] = "Рабочий телефон";
                sheet.Cells[6, 6] = "Примечание";

                sheet.get_Range("A1", "A1").ColumnWidth = 23;
                sheet.get_Range("B1", "B2").Font.Bold = true;
                sheet.get_Range("B1", "B1").ColumnWidth = 23;
                sheet.get_Range("C1", "C1").ColumnWidth = 23;
                sheet.get_Range("C1", "C1").WrapText = true;
                sheet.get_Range("D1", "D1").ColumnWidth = 23;
                sheet.get_Range("E1", "E1").ColumnWidth = 23;
                sheet.get_Range("F1", "F1").ColumnWidth = 23;
                sheet.get_Range("A6", "F6").Font.Bold = true;
                sheet.get_Range("A6", "F6").HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                sheet.get_Range("A6", "F6").Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                if (_Tenant.Rows.Count > 0)
                {
                    for (int i = 0; i < dgTenant.Rows.Count; i++)
                    {
                        string A, F;
                        A = "A" + (i + 7);
                        F = "F" + (i + 7);
                        /*color = 2;
                        if (dgTenant.Rows[i].DefaultCellStyle.BackColor == Color.SkyBlue)
                        { color = 33; }
                        if (dgTenant.Rows[i].DefaultCellStyle.BackColor == Color.DarkSeaGreen)
                        { color = 50; }
                        if (dgTenant.Rows[i].DefaultCellStyle.BackColor == Color.Coral)
                        { color = 22; }*/

                        //sheet.get_Range(A, D).Interior.ColorIndex = color;
                        sheet.Cells[i + 7, 1] = _Tenant.DefaultView[i]["aren"];
                        sheet.Cells[i + 7, 2] = _Tenant.DefaultView[i]["pred"];
                        sheet.Cells[i + 7, 3] = _Tenant.DefaultView[i]["loc"];
                        sheet.Cells[i + 7, 4] = _Tenant.DefaultView[i]["email"];
                        sheet.Cells[i + 7, 5] = _Tenant.DefaultView[i]["Work_phone"];
                        sheet.Cells[i + 7, 6] = _Tenant.DefaultView[i]["remark"];
                        sheet.get_Range(A, F).WrapText = true;
                        sheet.get_Range(A, F).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft;
                        sheet.get_Range(A, F).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop;
                        sheet.get_Range(A, F).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        sheet.PageSetup.PrintArea = "A1:" + F;
                    }
                }
                sheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;
                sheet.PageSetup.LeftMargin = 13.88;
                sheet.PageSetup.RightMargin = 13.88;
                sheet.PageSetup.TopMargin = 13.88;
                sheet.PageSetup.BottomMargin = 13.88;
                sheet.PageSetup.HeaderMargin = 0;
                sheet.PageSetup.FooterMargin = 0;
                appExc.Visible = true;
                object[] args = new object[2];
                args[0] = @_fileName;
                args[1] = 39;
                book.GetType().InvokeMember("SaveAs", BindingFlags.InvokeMethod, null, book, args);
            }
            #endregion

            #region Арендодатели
            if (template == "Landlord")
            {
                Exc.Application appExc = new Exc.Application();
                appExc.DisplayAlerts = false;
                appExc.Visible = false;
                appExc.SheetsInNewWorkbook = 1;
                Exc.Workbook book = appExc.Workbooks.Add(1);
                Exc.Worksheet sheet = (Exc.Worksheet)book.Worksheets[1];

                sheet.Cells[1, 1] = "Справочник арендодателей";
                sheet.get_Range("A1", "E1").Merge();
                sheet.get_Range("A1", "E1").Font.Size = 16;
                sheet.get_Range("A1", "E1").Font.Bold = true;
                sheet.get_Range("A1", "E1").BorderAround();
                sheet.get_Range("A1", "E1").HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;

                sheet.Cells[3, 1] = "Объект: " + _landlordObject;
                sheet.get_Range("A3", "E3").Merge();
                sheet.Cells[5, 1] = "Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername;
                sheet.get_Range("A5", "E5").Merge();
                sheet.Cells[6, 1] = "Дата выгрузки: " + DateTime.Now;
                sheet.get_Range("A6", "E6").Merge();

                sheet.Cells[8, 1] = "Объект";
                sheet.Cells[8, 2] = "Арендодатель";
                sheet.Cells[8, 3] = "Представитель";
                sheet.Cells[8, 4] = "Адрес";
                sheet.Cells[8, 5] = "Адрес сдаваемого помещения";
                sheet.get_Range("A1", "A1").ColumnWidth = 8;
                sheet.get_Range("B1", "B1").ColumnWidth = 25;
                sheet.get_Range("C1", "C1").ColumnWidth = 25;
                sheet.get_Range("C1", "C1").WrapText = true;
                sheet.get_Range("A8", "E8").Font.Bold = true;

                sheet.get_Range("D1", "D1").ColumnWidth = 40;
                sheet.get_Range("D1", "D1").WrapText = true;
                sheet.get_Range("E1", "E1").ColumnWidth = 40;
                sheet.get_Range("E1", "E1").WrapText = true;

                sheet.get_Range("A8", "E8").HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                sheet.get_Range("A8", "E8").Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                if (_LandLord.Rows.Count > 0)
                {
                    for (int i = 0; dgLordland.Rows.Count > i; i++)
                    {
                        string A, E;
                        A = "A" + (i + 9);
                        E = "E" + (i + 9);
                        color = 2;
                        if (dgLordland.Rows[i].DefaultCellStyle.BackColor == Color.LightBlue)
                        { color = 34; }
                        if (dgLordland.Rows[i].DefaultCellStyle.BackColor == Color.Coral)
                        { color = 22; }
                        sheet.get_Range(A, E).Interior.ColorIndex = color;
                        sheet.Cells[i + 9, 1] = _LandLord.DefaultView[i]["ObjName"];
                        sheet.Cells[i + 9, 2] = _LandLord.DefaultView[i]["Арендодатель"];
                        sheet.Cells[i + 9, 3] = _LandLord.DefaultView[i]["Представитель"];
                        sheet.Cells[i + 9, 4] = _LandLord.DefaultView[i]["Адрес"];
                        sheet.Cells[i + 9, 5] = _LandLord.DefaultView[i]["Address_trade_premises"];
                        sheet.get_Range(A, E).WrapText = true;
                        sheet.get_Range(A, E).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        sheet.get_Range(A, E).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop;
                        sheet.get_Range(A, E).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft;
                        sheet.PageSetup.PrintArea = "A1:" + E;
                    }
                }
                sheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;
                sheet.PageSetup.LeftMargin = 13.88;
                sheet.PageSetup.RightMargin = 13.88;
                sheet.PageSetup.TopMargin = 13.88;
                sheet.PageSetup.BottomMargin = 13.88;
                sheet.PageSetup.HeaderMargin = 0;
                sheet.PageSetup.FooterMargin = 0;
                appExc.Visible = true;
                object[] args = new object[2];
                args[0] = @_fileName;
                args[1] = 39;
                book.GetType().InvokeMember("SaveAs", BindingFlags.InvokeMethod, null, book, args);

            }
            #endregion

            #region Список договоров
            if (template == "Agreements")
            {
                ExcelUnLoad rep = new ExcelUnLoad("Договора");

                rep.AddSingleValue("Список договоров", 1, 1);
                rep.Merge(1, 1, 1, 11);
                rep.SetFontBold(1, 1, 1, 11);
                rep.SetFontSize(1, 1, 1, 1, 16);
                rep.SetCellAlignmentToCenter(1, 1, 1, 1);
                #region Начало отчета
                int crow = 3;
                rep.AddSingleValue($"Период: с {dateTimePicker1.Value.ToShortDateString()} по {dateTimePicker2.Value.ToShortDateString()}", crow, 1);
                rep.Merge(crow, 1, crow, 11);
                rep.SetFontBold(crow, 1, crow, 1);
                crow++;
                rep.AddSingleValue($"Арендодатель: {cbLordland.Text}", crow, 1);
                rep.Merge(crow, 1, crow, 11);
                rep.SetFontBold(crow, 1, crow, 11);
                crow++;
                rep.AddSingleValue($"Объект: {cmbObj.Text}", crow, 1);
                rep.Merge(crow, 1, crow, 11);
                rep.SetFontBold(crow, 1, crow, 11);
                crow++;
                rep.AddSingleValue($"Тип договора: {cmbType.Text}", crow, 1);
                rep.Merge(crow, 1, crow, 11);
                rep.SetFontBold(crow, 1, crow, 11);
                crow += 2;
                rep.AddSingleValue($"Выгрузил: {Nwuram.Framework.Settings.User.UserSettings.User.FullUsername}",crow,1);
                rep.Merge(crow, 1, crow, 11);
                rep.SetFontBold(crow, 1, crow, 11);
                crow++;
                rep.AddSingleValue($"Дата выгрузки: {DateTime.Now}", crow, 1);
                rep.Merge(crow, 1, crow, 11);
                rep.SetFontBold(crow, 1, crow, 11);
                crow += 2;
                #endregion
                int startrow = crow;
                #region Шапка
                rep.AddSingleValue("Дата заключения", crow, 1);
                rep.AddSingleValue("Объект", crow, 2);
                rep.AddSingleValue("Арендатор", crow, 3);
                rep.AddSingleValue("Номер договора", crow, 4);
                rep.AddSingleValue("Тип договора", crow, 5);
                rep.AddSingleValue("Начало действия", crow, 6);
                rep.AddSingleValue("Конец действия", crow, 7);
                rep.AddSingleValue("Место", crow, 8);
                rep.AddSingleValue("S, m2", crow, 9);
                rep.AddSingleValue("Стоимость аренды", crow, 10);
                rep.AddSingleValue("Оплата телефона", crow, 11);
                rep.SetFontBold(crow, 1, crow, 11);
                rep.SetCellAlignmentToCenter(crow, 1, crow, 11);
                rep.SetWrapText(crow, 1, crow, 11);
                crow++;
                #endregion

                rep.SetColumnWidth(1, 1, 1, 1, 11);
                rep.SetColumnWidth(2, 2, 2, 2, 7);
                rep.SetColumnWidth(3, 3, 3, 3, 15);
                rep.SetColumnWidth(4, 4, 4, 4, 11);
                rep.SetColumnWidth(5, 5, 5, 5, 16);
                rep.SetColumnWidth(6, 6, 6, 6, 11);
                rep.SetColumnWidth(7, 7, 7, 7, 11);
                rep.SetColumnWidth(8, 8, 8, 8, 29);
                rep.SetColumnWidth(9, 9, 9, 9, 8);
                rep.SetColumnWidth(10, 10, 10, 10, 10);
                rep.SetColumnWidth(11, 11, 11, 11, 10);

                if (_ListDoc.DefaultView.Count>0)
                {
                    DataTable dtReportAgr = _ListDoc.DefaultView.ToTable();
                    foreach (DataRow dr in dtReportAgr.Rows)
                    {
                        rep.AddSingleValue(DateTime.Parse(dr["Дата"].ToString()).ToShortDateString(), crow, 1);
                        rep.AddSingleValue(dr["ObjName"].ToString(), crow, 2);
                        rep.AddSingleValue(dr["Арендатор"].ToString(), crow, 3);
                        rep.AddSingleValue(dr["№"].ToString(), crow, 4);
                        rep.AddSingleValue(dr["TypeContract"].ToString(), crow, 5);
                        rep.AddSingleValue(DateTime.Parse(dr["Начало"].ToString()).ToShortDateString(), crow, 6);
                        rep.AddSingleValue(DateTime.Parse(dr["Конец"].ToString()).ToShortDateString(), crow, 7);
                        rep.AddSingleValue(dr["Место"].ToString(), crow, 8);
                        rep.AddSingleValue(dr["S"].ToString(), crow, 9);
                        rep.AddSingleValue(dr["Аренда"].ToString(), crow, 10);
                        rep.AddSingleValue(dr["Тел"].ToString(), crow, 11);

                        rep.SetCellAlignmentToLeft(crow, 1, crow, 11);
                        rep.SetCellAlignmentToTop(crow, 1, crow, 11);
                        rep.SetWrapText(crow, 1, crow, 11);

                        crow++;
                    }
                }
                rep.SetBorders(startrow, 1, crow - 1, 11);
                rep.SetPageOrientationToLandscape();
                rep.SetPageSetup(1, 1000, true);
                rep.Show();
                /*if (_ListDoc.Rows.Count > 0)
                {
                    for (int i = 0; i < dgListDoc.Rows.Count; i++)
                    {
                        sheet.Cells[i + 12, 1] = _ListDoc.DefaultView[i]["Дата"];
                        sheet.Cells[i + 12, 2] = _ListDoc.DefaultView[i]["ObjName"];
                        sheet.Cells[i + 12, 3] = _ListDoc.DefaultView[i]["Арендатор"];
                        //sheet.Cells[i + 12, 4] = _ListDoc.DefaultView[i]["№"].ToString();
                        sheet.Cells[i + 12, 5] = _ListDoc.DefaultView[i]["TypeContract"];
                        sheet.Cells[i + 12, 6] = _ListDoc.DefaultView[i]["Начало"];
                        sheet.Cells[i + 12, 7] = _ListDoc.DefaultView[i]["Конец"];
                        sheet.Cells[i + 12, 8] = _ListDoc.DefaultView[i]["Место"];
                        sheet.Cells[i + 12, 9] = _ListDoc.DefaultView[i]["S"];
                        sheet.Cells[i + 12, 10] = _ListDoc.DefaultView[i]["Аренда"];
                        sheet.Cells[i + 12, 11] = _ListDoc.DefaultView[i]["Тел"];
                    }
                    sheet.PageSetup.PrintArea = "A1:" + "K" + (11 + dgListDoc.Rows.Count);
                    sheet.get_Range("A11", "K" + (11 + dgListDoc.Rows.Count)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    sheet.get_Range("A11", "K" + (11 + dgListDoc.Rows.Count)).WrapText = true;
                    sheet.get_Range("A11", "K" + (11 + dgListDoc.Rows.Count)).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop;
                    sheet.get_Range("A12", "K" + (11 + dgListDoc.Rows.Count)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft;
                }

                sheet.PageSetup.LeftMargin = 13.88;
                sheet.PageSetup.RightMargin = 13.88;
                sheet.PageSetup.TopMargin = 13.88;
                sheet.PageSetup.BottomMargin = 13.88;
                sheet.PageSetup.HeaderMargin = 0;
                sheet.PageSetup.FooterMargin = 0;
                sheet.PageSetup.Orientation = Exc.XlPageOrientation.xlLandscape;
                appExc.Visible = true;
                object[] args = new object[2];
                args[0] = @_fileName;
                args[1] = 39;
                book.GetType().InvokeMember("SaveAs", BindingFlags.InvokeMethod, null, book, args);*/
            }
            #endregion

            if (template == "dogovor")
            {
                DataTable dtPrintResult = new DataTable();
                dtPrintResult = _proc.GetPrintData(int.Parse(dgListDoc.CurrentRow.Cells["id_agreements"].Value.ToString()));

                if (dtPrintResult != null)
                {
                    if (dtPrintResult.Rows.Count != 0)
                    {
                        string number = dtPrintResult.DefaultView[0]["num"].ToString();

                        Report temp = new Report();
                        temp.AddSingleValue("{num}", number);
                        temp.AddSingleValue("{adress}", dtPrintResult.DefaultView[0]["adress"].ToString());
                        temp.AddSingleValue("{date_con}", dtPrintResult.DefaultView[0]["date_con"].ToString());

                        temp.AddSingleValue("{arendodatel_str}", dtPrintResult.DefaultView[0]["arendodatel_str"].ToString());
                        temp.AddSingleValue("{post_arendodatel}", dtPrintResult.DefaultView[0]["post_arendodatel"].ToString());
                        temp.AddSingleValue("{FIO_arendodatel_Par}", dtPrintResult.DefaultView[0]["FIO_arendodatel_Par"].ToString());
                        temp.AddSingleValue("{osnovanie_arendodatel}", dtPrintResult.DefaultView[0]["osnovanie_arendodatel"].ToString());

                        temp.AddSingleValue("{arendator_str}", dtPrintResult.DefaultView[0]["arendator_str"].ToString());
                        temp.AddSingleValue("{post_arendator}", dtPrintResult.DefaultView[0]["post_arendator"].ToString());
                        temp.AddSingleValue("{FIO_arendator_Par}", dtPrintResult.DefaultView[0]["FIO_arendator_Par"].ToString());
                        temp.AddSingleValue("{osnovanie_arendator}", dtPrintResult.DefaultView[0]["osnovanie_arendator"].ToString());

                        temp.AddSingleValue("{buildings}", dtPrintResult.DefaultView[0]["buildings"].ToString());
                        temp.AddSingleValue("{floors}", dtPrintResult.DefaultView[0]["floors"].ToString());
                        temp.AddSingleValue("{section}", dtPrintResult.DefaultView[0]["section"].ToString());
                        temp.AddSingleValue("{S_arend}", dtPrintResult.DefaultView[0]["S_arend"].ToString());
                        temp.AddSingleValue("{dop_str}", dtPrintResult.DefaultView[0]["dop_str"].ToString());
                        temp.AddSingleValue("{type_of_premises}", dtPrintResult.DefaultView[0]["type_of_premises"].ToString());
                        temp.AddSingleValue("{NDS}", dtPrintResult.DefaultView[0]["NDS"].ToString());
                        temp.AddSingleValue("{Summa}", dtPrintResult.DefaultView[0]["Summa"].ToString());
                        temp.AddSingleValue("{Summa_str}", dtPrintResult.DefaultView[0]["Summa_str"].ToString());
                        temp.AddSingleValue("{date_start}", dtPrintResult.DefaultView[0]["date_start"].ToString());
                        temp.AddSingleValue("{date_end}", dtPrintResult.DefaultView[0]["date_end"].ToString());

                        temp.AddSingleValue("{arendodatel}", dtPrintResult.DefaultView[0]["arendodatel"].ToString());

                        temp.AddSingleValue("{adress_arendodatel}", dtPrintResult.DefaultView[0]["adress_arendodatel"].ToString());
                        temp.AddSingleValue("{inn_arendodatel}", dtPrintResult.DefaultView[0]["inn_arendodatel"].ToString());
                        temp.AddSingleValue("{PaymentAccount_arendodatel}", dtPrintResult.DefaultView[0]["PaymentAccount_arendodatel"].ToString());
                        temp.AddSingleValue("{bank_arendodatel}", dtPrintResult.DefaultView[0]["bank_arendodatel"].ToString());
                        temp.AddSingleValue("{bik_arendodatel}", dtPrintResult.DefaultView[0]["bik_arendodatel"].ToString());
                        temp.AddSingleValue("{CorrespondentAccount_arendodatel}", dtPrintResult.DefaultView[0]["CorrespondentAccount_arendodatel"].ToString());
                        temp.AddSingleValue("{kpp_arendodatel}", dtPrintResult.DefaultView[0]["kpp_arendodatel"].ToString());
                        temp.AddSingleValue("{FIO_arendodatel}", dtPrintResult.DefaultView[0]["FIO_arendodatel"].ToString());

                        temp.AddSingleValue("{arendator}", dtPrintResult.DefaultView[0]["arendator"].ToString());

                        temp.AddSingleValue("{adress_arendator}", dtPrintResult.DefaultView[0]["adress_arendator"].ToString());
                        temp.AddSingleValue("{inn_arendator}", dtPrintResult.DefaultView[0]["inn_arendator"].ToString());
                        temp.AddSingleValue("{PaymentAccount_arendator}", dtPrintResult.DefaultView[0]["PaymentAccount_arendator"].ToString());
                        temp.AddSingleValue("{bank_arendator}", dtPrintResult.DefaultView[0]["bank_arendator"].ToString());
                        temp.AddSingleValue("{bik_arendator}", dtPrintResult.DefaultView[0]["bik_arendator"].ToString());
                        temp.AddSingleValue("{CorrespondentAccount_arendator}", dtPrintResult.DefaultView[0]["CorrespondentAccount_arendator"].ToString());
                        temp.AddSingleValue("{kpp_arendator}", dtPrintResult.DefaultView[0]["kpp_arendator"].ToString());
                        temp.AddSingleValue("{FIO_arendator}", dtPrintResult.DefaultView[0]["FIO_arendator"].ToString());

                        if (!temp.CreateTemplate(Application.StartupPath + "\\Templates\\dogovor", Application.StartupPath + "\\Договор №" + number, null))
                        {
                            MessageBox.Show(temp.ErrorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        Thread.Sleep(100);

                        temp.OpenFile(Application.StartupPath + "\\Договор №" + number);
                    }
                }
            }
        }

        private void bgwExcel_DoWork(object sender, DoWorkEventArgs e)
        {
            bgwExcel.WorkerSupportsCancellation = false;
            var args = (object[])e.Argument;

            PrintExel(args[0].ToString());
        }

        private void bgwExcel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            prbExcel.Visible = false;
            this.Enabled = true;
        }

        private bool nullPrint()
        {
            int pr = 0;

            if (pListDoc.Visible == true)
            {
                if (dgListDoc.Rows.Count != 0)
                    pr = 1;
                else pr = 0;
            }
            if (pLordland.Visible == true)
            {
                if (dgLordland.Rows.Count != 0)
                    pr = 1;
                else pr = 0;
            }
            if (pTenant.Visible == true)
            {
                if (dgTenant.Rows.Count != 0)
                    pr = 1;
                else pr = 0;
            }


            if (pr == 1) return true;
            else return false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            iniLandLord();
        }

        private void справочникБанковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var bank = new Banks(0);
            var bank = new frmBanks(0) { ShowInTaskbar = false };
            bank.ShowDialog();
        }

        private void dgTenant_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                var editTen = new AddEditTenant(Convert.ToInt32(dgTenant.SelectedRows[0].Cells["id"].Value.ToString()), true, true);
                editTen.ShowDialog();
                iniTenant();
            }
        }

        private void dgListDoc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dt = new DataTable();
                dt = _proc.GetLD(Convert.ToInt32(dgListDoc.SelectedRows[0].Cells[0].Value.ToString()));
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Открытие документа невозможно. Документ удален.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var editDoc = new AddeditDoc(Convert.ToInt32(dgListDoc.SelectedRows[0].Cells["id_agreements"].Value.ToString()), true, false);
                    editDoc.ShowDialog();
                }
                iniListDoc();
            }
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            if (pListDoc.Visible)
            {
                /*PrintForm f = new PrintForm(int.Parse(dgListDoc.CurrentRow.Cells["id_agreements"].Value.ToString()),
                    dgListDoc.CurrentRow.Cells["number"].Value.ToString().Trim(),
                    int.Parse(dgListDoc.CurrentRow.Cells["id_type"].Value.ToString()));*/
                ArendaPrint.frmPrint f = new ArendaPrint.frmPrint(int.Parse(dgListDoc.CurrentRow.Cells["id_agreements"].Value.ToString()),
                    dgListDoc.CurrentRow.Cells["number"].Value.ToString().Trim(),
                    int.Parse(dgListDoc.CurrentRow.Cells["id_type"].Value.ToString()));
                f.setData(
                        dgListDoc.CurrentRow.Cells["Date"].Value.ToString(),
                        dgListDoc.CurrentRow.Cells["number"].Value.ToString(),
                        dgListDoc.CurrentRow.Cells["tTenant"].Value.ToString(),
                        dgListDoc.CurrentRow.Cells["ALocate"].Value.ToString(),
                        dgListDoc.CurrentRow.Cells["DataStart"].Value.ToString(),
                        dgListDoc.CurrentRow.Cells["DataEnd"].Value.ToString(),
                        dgListDoc.CurrentRow.Cells["id_lord"].Value.ToString());


                f.ShowDialog();
                iniListDoc();
            }

            if (pLordland.Visible)
            {
                frmPrintPlanOtchet frmPrintPlan = new frmPrintPlanOtchet();
                frmPrintPlan.ShowDialog();
                iniLandLord();
            }

            if (pTenant.Visible)
            {
                int id_Ten = int.Parse(dgTenant.CurrentRow.Cells["id"].Value.ToString());
                string Name_Ten = dgTenant.CurrentRow.Cells["Tenent"].Value.ToString();
                frmTenantReport frmRep = new frmTenantReport(id_Ten, Name_Ten);

                frmRep.setData(dgTenant.CurrentRow.Cells["id"].Value.ToString(),
                    dgTenant.CurrentRow.Cells["Tenent"].Value.ToString(),
                    dgTenant.CurrentRow.Cells["Pred"].Value.ToString(),
                    dgTenant.CurrentRow.Cells["Locate"].Value.ToString(),
                    dgTenant.CurrentRow.Cells["remark"].Value.ToString());

                frmRep.ShowDialog();
                iniTenant();
            }
        }

        private void справочникДолжностейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var post = new Post() { ShowInTaskbar = false };
            post.ShowDialog();
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var conf = new Config();
            conf.ShowDialog();
        }

        private void tbNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Regex pat = new Regex(@"[\b]|[0-9]|[a-z]|[A-Z]|[а-я]|[А-Я]|[\\]");
            //bool b = pat.IsMatch(e.KeyChar.ToString());
            //if (b == false)
            //{
            //    e.Handled = true;
            //}
        }

        private void tbNumber_TextChanged(object sender, EventArgs e)
        {
            FilterDataView1();
        }

        private void btnListTaxes_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dgListDoc.CurrentRow.Cells["id_agreements"].Value.ToString());
            frmListTaxes frmListT = new frmListTaxes(id) { ShowInTaskbar = false };

            frmListT.setData(
                        dgListDoc.CurrentRow.Cells["Date"].Value.ToString(),
                        dgListDoc.CurrentRow.Cells["number"].Value.ToString(),
                        dgListDoc.CurrentRow.Cells["tTenant"].Value.ToString(),
                        dgListDoc.CurrentRow.Cells["ALocate"].Value.ToString(),
                        dgListDoc.CurrentRow.Cells["DataStart"].Value.ToString(),
                        dgListDoc.CurrentRow.Cells["DataEnd"].Value.ToString(),
                        dgListDoc.CurrentRow.Cells["id_lord"].Value.ToString());

            frmListT.ShowDialog();
        }

        private void btnListPayment_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dgListDoc.CurrentRow.Cells["id_agreements"].Value.ToString());
            frmListPayment frmListP = new frmListPayment(id) { ShowInTaskbar = false };
            frmListP.setData(
                     dgListDoc.CurrentRow.Cells["Date"].Value.ToString(),
                     dgListDoc.CurrentRow.Cells["number"].Value.ToString(),
                     dgListDoc.CurrentRow.Cells["tTenant"].Value.ToString(),
                     dgListDoc.CurrentRow.Cells["ALocate"].Value.ToString(),
                     dgListDoc.CurrentRow.Cells["DataStart"].Value.ToString(),
                     dgListDoc.CurrentRow.Cells["DataEnd"].Value.ToString(),
                     dgListDoc.CurrentRow.Cells["id_lord"].Value.ToString());
            frmListP.ShowDialog();
        }

        private void справочникДопОплатToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAnotherPayments frmAnPay = new frmAnotherPayments() { ShowInTaskbar = false };
            frmAnPay.ShowDialog();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (pListDoc.Visible == true)
            {
                dt = new DataTable();
                dt = _proc.GetLD(Convert.ToInt32(dgListDoc.SelectedRows[0].Cells[0].Value.ToString()));
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Открытие документа невозможно. Документ удален.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var editDoc = new AddeditDoc(Convert.ToInt32(dgListDoc.SelectedRows[0].Cells["id_agreements"].Value.ToString()), true, false);
                    editDoc.ShowDialog();
                }
                iniListDoc();
            }
            if (pLordland.Visible == true)
            {
                var editLord = new AddEditTenant(Convert.ToInt32(dgLordland.SelectedRows[0].Cells["id_landlord"].Value.ToString()), false, true);
                editLord.ShowDialog();
                iniLandLord();
            }
            if (pTenant.Visible == true)
            {
                var editTen = new AddEditTenant(Convert.ToInt32(dgTenant.SelectedRows[0].Cells["id"].Value.ToString()), true, true);
                editTen.ShowDialog();
                iniTenant();
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (pListDoc.Visible == true)
            {
                DataTable dtReport = new DataTable();

                dtReport = _proc.GetNotPayedReport();

                if ((dtReport != null) && (dtReport.Rows.Count > 0))
                {
                    DateTime CurDate = _proc.getdatetime();

                    Logging.StartFirstLevel(79);
                    Logging.Comment("Выгрузка отчета по неоплаченным дополнительным оплатам");

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();


                    HandmadeReport Rep = new HandmadeReport();

                    Rep.AddSingleValue("Отчет по неоплаченным дополнительным оплатам", 2, 1);
                    Rep.SetFontSize(2, 1, 2, 1, 14);
                    Rep.SetFontBold(2, 1, 2, 1);

                    Rep.AddSingleValue("Дата:", 4, 7);
                    Rep.AddSingleValue(CurDate.ToString("dd.MM.yyyy"), 4, 8);

                    Rep.AddSingleValue("№", 6, 1);
                    Rep.AddSingleValue("Арендатор", 6, 2);
                    Rep.AddSingleValue("№ договора", 6, 3);
                    Rep.AddSingleValue("Дата выписки доп. оплаты", 6, 4);
                    Rep.AddSingleValue("Сумма доп. оплаты", 6, 5);
                    Rep.AddSingleValue("Тип оплаты", 6, 6);
                    Rep.AddSingleValue("Оплаченная сумма", 6, 7);
                    Rep.AddSingleValue("Долг", 6, 8);
                    Rep.SetFontBold(6, 1, 6, 8);

                    Rep.AddMultiValue(dtReport, 7, 1);

                    Rep.SetBorders(6, 1, dtReport.Rows.Count + 6, 8);

                    //Итого
                    decimal total = 0;

                    for (int i = 0; dtReport.Rows.Count > i; i++)
                    {
                        total += decimal.Parse(dtReport.Rows[i]["Debt"].ToString());
                    }

                    Rep.AddSingleValue("ИТОГО:", dtReport.Rows.Count + 7, 7);
                    Rep.AddSingleValue(total.ToString("### ### ### ##0.00"), dtReport.Rows.Count + 7, 8);
                    Rep.SetBorders(dtReport.Rows.Count + 7, 8, dtReport.Rows.Count + 7, 8);
                    /*
                    Rep.SetColumnAutoSize(1, 1, grdPayments.Rows.Count + 10, dtPrint.Columns.Count);
                    */

                    int t = 1;
                    Rep.SetColumnWidth(1, t, 1, t, 5);
                    t++;
                    Rep.SetColumnWidth(1, t, 1, t, 30);
                    t++;
                    Rep.SetColumnWidth(1, t, 1, t, 15);
                    t++;
                    Rep.SetColumnWidth(1, t, 1, t, 10);
                    t++;
                    Rep.SetColumnWidth(1, t, 1, t, 10);
                    t++;
                    Rep.SetColumnWidth(1, t, 1, t, 12);
                    t++;
                    Rep.SetColumnWidth(1, t, 1, t, 10);
                    t++;
                    Rep.SetColumnWidth(1, t, 1, t, 10);

                    Rep.SetWrapText(6, 1, 6, 8);

                    Rep.SetCellAlignmentToCenter(6, 1, 6, 8);

                    Rep.SetCellAlignmentToCenter(7, 1, dtReport.Rows.Count + 6, 1);
                    Rep.SetCellAlignmentToRight(7, 3, dtReport.Rows.Count + 6, 3);
                    Rep.SetCellAlignmentToCenter(7, 4, dtReport.Rows.Count + 6, 4);
                    Rep.SetCellAlignmentToRight(7, 5, dtReport.Rows.Count + 6, 5);
                    Rep.SetCellAlignmentToRight(7, 7, dtReport.Rows.Count + 6, 7);
                    Rep.SetCellAlignmentToRight(7, 8, dtReport.Rows.Count + 7, 8);

                    Rep.SetFormat(7, 4, dtReport.Rows.Count + 6, 4, "ДД.ММ.ГГГГ");
                    Rep.SetFormat(7, 5, dtReport.Rows.Count + 6, 5, "0,00");
                    Rep.SetFormat(7, 7, dtReport.Rows.Count + 6, 7, "0,00");
                    Rep.SetFormat(7, 8, dtReport.Rows.Count + 7, 8, "0,00");

                    Rep.Show();
                }
                else
                {
                    MessageBox.Show("Данных для отчета нет.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                iniListDoc();
            }
            if (pLordland.Visible == true)
            {
                int id_LandLord = int.Parse(dgLordland.SelectedRows[0].Cells["id_landlord"].Value.ToString());
                string Name_LandLord = dgLordland.SelectedRows[0].Cells["Landlord"].Value.ToString();

                frmLordlandDailyReport frmRep = new frmLordlandDailyReport(id_LandLord, Name_LandLord);
                frmRep.setData(dgLordland.SelectedRows[0].Cells["id_landlord"].Value.ToString(),
                    dgLordland.SelectedRows[0].Cells["Landlord"].Value.ToString(),
                    dgLordland.SelectedRows[0].Cells["LordPreds"].Value.ToString(),
                    dgLordland.SelectedRows[0].Cells["Adress"].Value.ToString(),
                    dgLordland.SelectedRows[0].Cells["Adress_trade"].Value.ToString()
                    );

                frmRep.ShowDialog();

                iniLandLord();
            }
        }

        private void справочникПриборовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDevices frmDevices = new frmDevices() { ShowInTaskbar = false };
            frmDevices.ShowDialog();
        }

        private void справочникОбъектовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmObjects frm = new frmObjects() { ShowInTaskbar = false };
            frm.ShowDialog();
        }

        private void cmbObject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _landlordObject = cmbObject.Text;
            FilterDataView2();
        }

        private void cmbType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterDataView1();
        }

        private void cmbObj_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterDataView1();
        }

        private void tbPlace_TextChanged(object sender, EventArgs e)
        {
            FilterDataView1();
        }

        private void sPlace_TextChanged(object sender, EventArgs e)
        {
            FilterDataView();
        }

        private void sEmail_TextChanged(object sender, EventArgs e)
        {
            FilterDataView();
        }

        private void sPhone_TextChanged(object sender, EventArgs e)
        {
            FilterDataView();
        }

        private void dgLordland_Paint(object sender, PaintEventArgs e)
        {
            int X = dgLordland.Location.X + dgLordland.Columns["ObjName"].Width;
            tbLandlord.Location = new Point(X, tbLandlord.Location.Y);
            tbLandlord.Width = dgLordland.Columns["Landlord"].Width;
            X += tbLandlord.Width;
            tbLandpred.Location = new Point(X, tbLandpred.Location.Y);
            tbLandpred.Width = dgLordland.Columns["LordPreds"].Width;
        }

        private void dgListDoc_Paint(object sender, PaintEventArgs e)
        {
            int X = dgListDoc.Location.X + dgListDoc.Columns["Date"].Width
              + dgListDoc.Columns["ObjNameD"].Width;
            tbDoc.Location = new Point(X, tbDoc.Location.Y);
            tbDoc.Width = dgListDoc.Columns["tTenant"].Width;
            X += tbDoc.Width;
            tbNumber.Location = new Point(X, tbNumber.Location.Y);
            tbNumber.Width = dgListDoc.Columns["number"].Width;
            X += tbNumber.Width + dgListDoc.Columns["Type"].Width
              + dgListDoc.Columns["DataStart"].Width
              + dgListDoc.Columns["DataEnd"].Width;
            tbPlace.Location = new Point(X, tbPlace.Location.Y);
            tbPlace.Width = dgListDoc.Columns["ALocate"].Width;
        }

        private void dgTenant_Paint(object sender, PaintEventArgs e)
        {
            int X = dgTenant.Location.X;
            sTenant.Location = new Point(X, sTenant.Location.Y);
            sTenant.Width = dgTenant.Columns["Tenent"].Width;
            X += sTenant.Width;
            sName.Location = new Point(X, sName.Location.Y);
            sName.Width = dgTenant.Columns["Pred"].Width;
            X += sName.Width;
            sPlace.Location = new Point(X, sPlace.Location.Y);
            sPlace.Width = dgTenant.Columns["Locate"].Width;
            X += sPlace.Width;
            sEmail.Location = new Point(X, sEmail.Location.Y);
            sEmail.Width = dgTenant.Columns["cEmail"].Width;
            X += sEmail.Width;
            sPhone.Location = new Point(X, sPhone.Location.Y);
            sPhone.Width = dgTenant.Columns["cPhone"].Width;
        }

        private void выгрузкаДокументовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArendaFileExport.Form1 frm = new ArendaFileExport.Form1();
            frm.Show();
        }

        private void tbPlace_KeyPress(object sender, KeyPressEventArgs e)
        {
            lockSimbols(e);
        }

        private void sPlace_KeyPress(object sender, KeyPressEventArgs e)
        {
            lockSimbols(e);
        }

        private void sEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex pat = new Regex(@"[\b]|[a-zA-Z0-9а-яА-Я]|[.-@]");
            bool b = pat.IsMatch(e.KeyChar.ToString());
            if (b == false)
            {
                e.Handled = true;
            }
        }

        private void dgTenant_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void btJournalSealSections_Click(object sender, EventArgs e)
        {
            try
            {
                int id_agreements = Convert.ToInt32(dgListDoc.SelectedRows[0].Cells["id_agreements"].Value.ToString());
                new frmSealSections() { id_agreements = id_agreements }.ShowDialog();
            }
            catch { }
        }

        private void btAcceptDoc_Click(object sender, EventArgs e)
        {
            //new List<string> { "СОА", "РКВ", "КНТ" }.Contains(TempData.Rezhim);
            try
            {
                int id_agreements = Convert.ToInt32(dgListDoc.SelectedRows[0].Cells["id_agreements"].Value.ToString());
                bool isConfirmed = (bool)dgListDoc.SelectedRows[0].Cells["cisConfirmed"].Value;

                if (!isConfirmed && new List<string> { "СОА", "РКВ" }.Contains(TempData.Rezhim))
                { if (DialogResult.No == MessageBox.Show("Подтвердить договор?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) return; }
                else
                    if (isConfirmed && new List<string> { "КНТ" }.Contains(TempData.Rezhim))
                { if (DialogResult.No == MessageBox.Show("Отменить подтверждение договор?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) return; }
                else return;


                _proc.setConfirm(id_agreements, !isConfirmed);

                iniListDoc();
            }
            catch { }
        }

        private void dgListDoc_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {

            if (e.RowIndex != -1)
            {
                //(bool)dgListDoc.SelectedRows[0].Cells["cisConfirmed"].Value

                //dgListDoc.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;

                Color rColor = Color.White;

                if (!(bool)dgListDoc.Rows[e.RowIndex].Cells["cisConfirmed"].Value)
                {
                    dgListDoc.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                    dgListDoc.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
                    dgListDoc.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
                }
                else
                {
                    if ((bool)dgListDoc.Rows[e.RowIndex].Cells["isCancelDoc"].Value)
                        rColor = pCancelDoc.BackColor;

                    dgListDoc.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                    dgListDoc.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
                    dgListDoc.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;



                    if ((bool)dgListDoc.Rows[e.RowIndex].Cells["isDocForLostTime"].Value && !(bool)dgListDoc.Rows[e.RowIndex].Cells["isCancelDoc"].Value)
                        dgListDoc.Rows[e.RowIndex].Cells["DataEnd"].Style.BackColor
                             = dgListDoc.Rows[e.RowIndex].Cells["DataEnd"].Style.SelectionBackColor
                             = pDocForLostTime.BackColor;
                    else
                     if ((bool)dgListDoc.Rows[e.RowIndex].Cells["isEndingDoc"].Value)
                        dgListDoc.Rows[e.RowIndex].Cells["DataEnd"].Style.BackColor
                             = dgListDoc.Rows[e.RowIndex].Cells["DataEnd"].Style.SelectionBackColor
                             = pEndingDoc.BackColor;


                    if ((bool)dgListDoc.Rows[e.RowIndex].Cells["isUseDopData"].Value)
                        dgListDoc.Rows[e.RowIndex].Cells["Date"].Style.BackColor =
                             dgListDoc.Rows[e.RowIndex].Cells["Date"].Style.SelectionBackColor
                            = pUseDopData.BackColor;

                    //if(!(bool)dgListDoc.SelectedRows[0].Cells["cisConfirmed"].Value)
                    //    dgListDoc.Rows[e.RowIndex].Cells["tTenant"].Style.BackColor = pUseDopData.BackColor;

                    //if (!(bool)dtTable.DefaultView[e.RowIndex]["isUse"])
                    //    dgvTable.Rows[e.RowIndex].Cells[cFieldName.Index].Style.BackColor =
                    //         dgvTable.Rows[e.RowIndex].Cells[cFieldName.Index].Style.SelectionBackColor = panel1.BackColor;
                }
            }

        }

        private void dgListDoc_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {


            DataGridView dgv = sender as DataGridView;
            //Рисуем рамку для выделеной строки
            if (dgv.Rows[e.RowIndex].Selected)
            {
                int width = dgv.Width;
                Rectangle r = dgv.GetRowDisplayRectangle(e.RowIndex, false);
                Rectangle rect = new Rectangle(r.X, r.Y, width - 1, r.Height - 1);

                //ControlPaint.DrawBorder(e.Graphics, rect,
                //    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                //    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                //    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                //    SystemColors.Highlight, 2, ButtonBorderStyle.Solid);

                ControlPaint.DrawBorder(e.Graphics, rect,
                       Color.Red, 2, ButtonBorderStyle.Solid,
                       Color.Red, 2, ButtonBorderStyle.Solid,
                       Color.Red, 2, ButtonBorderStyle.Solid,
                       Color.Red, 2, ButtonBorderStyle.Solid);
            }
        }

        private void chbCancelDoc_Click(object sender, EventArgs e)
        {
            FilterDataView1();
        }

        private void справочникРекламныхМестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new dllArendaDictonary.dicAdvertisingSpace.frmList().ShowDialog();
        }

        private void справочникЗемельныхУчастковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new dllArendaDictonary.dicLandPlot.frmList().ShowDialog();
        }

        private void справочникВидаДейтельностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new dllArendaDictonary.dicTypeActivities.frmList().ShowDialog();
        }

        private void справочникСкидокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new dllArendaDictonary.jDiscount.frmList().ShowDialog();
        }

        private void dgListDoc_SelectionChanged(object sender, EventArgs e)
        {
            if (dgListDoc.CurrentRow == null || dgListDoc.CurrentRow.Index == -1) { btAcceptDoc.Enabled = false; return; }

            try
            {
                if ((bool)dgListDoc.Rows[dgListDoc.CurrentRow.Index].Cells["cisConfirmed"].Value && new List<string> { "КНТ" }.Contains(TempData.Rezhim))
                {
                    btAcceptDoc.Enabled = true;
                }
                else if (!(bool)dgListDoc.Rows[dgListDoc.CurrentRow.Index].Cells["cisConfirmed"].Value && new List<string> { "СОА", "РКВ" }.Contains(TempData.Rezhim))
                {
                    btAcceptDoc.Enabled = true;
                }
                else
                    btAcceptDoc.Enabled = false;

                btEdit.Enabled = btDel.Enabled = !(((bool)dgListDoc.Rows[dgListDoc.CurrentRow.Index].Cells["cisConfirmed"].Value && new List<string> { "МНД" }.Contains(TempData.Rezhim)));
                
                }
            catch
            {
                btAcceptDoc.Enabled = false;
            }
        }

        private void btCopyDoc_Click(object sender, EventArgs e)
        {
            if (pListDoc.Visible == true)
            {
                dt = new DataTable();
                dt = _proc.GetLD(Convert.ToInt32(dgListDoc.SelectedRows[0].Cells[0].Value.ToString()));
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Редактирование договора невозможно. Договор удален.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    bool isConfirmed = (bool)dgListDoc.SelectedRows[0].Cells["cisConfirmed"].Value;
                    var editDoc = new AddeditDoc(Convert.ToInt32(dgListDoc.SelectedRows[0].Cells[0].Value.ToString()), false, isConfirmed,true);
                    //editDoc.isCopyDoc = true;
                    editDoc.ShowDialog();
                }
                iniListDoc();
            }
        }

        private void btKntListTaxes_Click(object sender, EventArgs e)
        {
            new Payments.frmKntListTaxes().ShowDialog();
        }

        private void журналНачисленияПениToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new dllArendaJournalAccrualsPenalties.frmMain().ShowDialog();
        }

        private void журналДолжниковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new JournalBorrower.frmMain().ShowDialog();
        }

        private void журналСъездовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new dllJournalCongress.frmMain().ShowDialog();
        }

        private void журналЕжемесячныхПлановToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new dllJournalReport.frmReportMonth().ShowDialog();
        }

        private void журналПланОтчётовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new dllJournalPlaneReport.frmReportPlane().ShowDialog();
        }

        private void отчетПоВидамДеятельностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TenantsReport.frmMain() { ShowInTaskbar = false }.ShowDialog();
        }

        private void отчетОЗанятостиСекцийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ArendaViewSection.frmView().ShowDialog();
        }

        private void btReportTenant_Click(object sender, EventArgs e)
        {
            new TenantsReport.frmMain() { ShowInTaskbar = false }.ShowDialog();
        }

        private void btDicDiscount_Click(object sender, EventArgs e)
        {
            new dllArendaDictonary.jDiscount.frmList().ShowDialog();
        }

        private void btnMassDiscounts_Click(object sender, EventArgs e)
        {
            new ArendaDiscount.frmDiscounts().ShowDialog();
        }

        private void chbCancelDoc_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tsmiReportPayTypeDates_Click(object sender, EventArgs e)
        {
            new Reports.frmRepotPayTypeDates() { Text = tsmiReportPayTypeDates.Text }.ShowDialog();
        }

        private void sPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex pat = new Regex(@"[\b]|[0-9]|[+-]");
            bool b = pat.IsMatch(e.KeyChar.ToString());
            if (b == false)
            {
                e.Handled = true;
            }
        }
    }
}   
