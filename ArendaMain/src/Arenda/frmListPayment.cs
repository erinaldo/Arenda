using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Logging;
using Nwuram.Framework.ToExcel;

namespace Arenda
{
    public partial class frmListPayment : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        int id;
        DataTable dtPayments;
        bool Reklama = false;
        decimal Phone = 0;

        public frmListPayment(int _id)
        {
            id = _id;
            InitializeComponent();
        }

        private void frmListPayment_Load(object sender, EventArgs e)
        {
            //if (TempData.Rezhim == "ПР")
            if (new List<string> { "СБ6", "Д", "ПР","КНТ" }.Contains(TempData.Rezhim))
            {
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnDel.Visible = false;
            }

            DataTable dt = new DataTable();
            dt = _proc.GetLD(id);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                txtNum.Text = dt.Rows[0]["Agreement"].ToString().Trim();
                dtpDate.Value = DateTime.Parse(dt.Rows[0]["Date_of_Conclusion"].ToString());
                txtTenant.Text = dt.Rows[0]["Tenant_name"].ToString().Trim();
                txtSum.Text = numTextBox.CheckAndChange(dt.Rows[0]["Total_Sum"].ToString().Trim(), 2, 0, 999999999, false, "0.00", "{0:# ### ### ##0.00}");
                Phone = decimal.Parse(dt.Rows[0]["Phone"].ToString());
                idArend = dt.Rows[0]["id_Tenant"].ToString();
                if (decimal.Parse(dt.Rows[0]["Reklama"].ToString()) != 0)
                {
                    Reklama = true;
                }
                GetData();
            }
            else
            {
                //MessageBox.Show("Запись уже была удалена, либо произошла ошибка получения данных!");
                this.Close();
            }
        }

        private void GetData()
        {
            dtPayments = new DataTable();
            dtPayments = _proc.GetPayments(id);


            decimal summa = 0;

            if ((dtPayments != null) && (dtPayments.Rows.Count > 0))
            {
                btnEdit.Enabled = true;
                btnDel.Enabled = true;
                for (int i = 0; dtPayments.Rows.Count > i; i++)
                {
                    summa += decimal.Parse(numTextBox.ConvertToCompPunctuation(dtPayments.Rows[i]["PaymentSum"].ToString())) * ((bool)dtPayments.Rows[i]["isToTenant"] ? -1 : 1);
                    dtPayments.Rows[i]["PaymentSum"] = numTextBox.ConvertToSqlPunctuation(dtPayments.Rows[i]["PaymentSum"].ToString());
                }
            }
            else
            {
                btnEdit.Enabled = false;
                btnDel.Enabled = false;
            }

            dtPayments.AcceptChanges();

            grdPayments.AutoGenerateColumns = false;
            grdPayments.DataSource = dtPayments;

            txtItog.Text = numTextBox.CheckAndChange(summa.ToString().Trim(), 2, -999999999, 999999999999, true, "0.00", "{0:# ### ### ##0.00}");
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListPayment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                //Add();
                btnAdd_Click(null, null);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = _proc.GetLD(id);
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                if (dt.Rows[0]["fullPayed"].ToString() == "True")
                {
                    MessageBox.Show("Договор полностью оплачен. \nДобавление оплаты невозможно!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int? id_savePayment = null;
                    if (dt.Rows[0]["id_SavePayment"] != DBNull.Value) id_savePayment = (int?)dt.Rows[0]["id_SavePayment"];
                    Add(id_savePayment);
                }                
            }            
        }

        private void Add(int? id_SavePayment)
        {
            frmAddPayment frmAddP = new frmAddPayment(0, id, txtNum.Text, Reklama, id_SavePayment);
            frmAddP.ShowDialog();
            GetData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int id_payment = int.Parse(grdPayments.CurrentRow.Cells["id_payment"].Value.ToString());
                frmAddPayment frmAddP = new frmAddPayment(id_payment, id, txtNum.Text, Reklama, null);
                frmAddP.ShowDialog();
                GetData();
            }
            catch { }
        }

        private void grdPayments_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txtEditor.Text = grdPayments.CurrentRow.Cells["Editor"].Value.ToString();
                txtDateEdit.Text = grdPayments.CurrentRow.Cells["DateEdit"].Value.ToString();
            }
            catch { txtEditor.Text = ""; txtDateEdit.Text = ""; }
            try
            {
                if (int.Parse(dtPayments.DefaultView[grdPayments.CurrentRow.Index]["id_PayType"].ToString()) == 3)
                {
                    tbType.Visible = tbSumm.Visible = tbMonth.Visible = tbDateCreate.Visible =
                        label1.Visible = label2.Visible = label3.Visible = label4.Visible = true;
                    tbType.Text = dtPayments.DefaultView[grdPayments.CurrentRow.Index]["fTypePayment"].ToString();
                    tbSumm.Text = dtPayments.DefaultView[grdPayments.CurrentRow.Index]["fSumma"].ToString();

                    tbMonth.Text = dtPayments.DefaultView[grdPayments.CurrentRow.Index]["fMonth"].ToString() == "" ? "" :
                        DateTime.Parse(dtPayments.DefaultView[grdPayments.CurrentRow.Index]["fMonth"].ToString()).ToShortDateString();
                    tbDateCreate.Text = dtPayments.DefaultView[grdPayments.CurrentRow.Index]["fDateCreate"].ToString() == "" ? "" :
                         DateTime.Parse(dtPayments.DefaultView[grdPayments.CurrentRow.Index]["fDateCreate"].ToString()).ToShortDateString();
                }
                else
                {
                    tbType.Visible = tbSumm.Visible = tbMonth.Visible = tbDateCreate.Visible =
                        label1.Visible = label2.Visible = label3.Visible = label4.Visible = false;
                    tbType.Text = tbSumm.Text = tbMonth.Text = tbDateCreate.Text = "";
                }
            }
            catch
            {
                tbType.Text = tbSumm.Text = tbMonth.Text = tbDateCreate.Text = "";
            }

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (grdPayments.CurrentRow == null)
            {
                return;
            }

            int id_payment = int.Parse(grdPayments.CurrentRow.Cells["id_payment"].Value.ToString());

            DataTable dt = new DataTable();
            dt = _proc.GetPayments(id);
            DataRow dr = dt.Select("id=" + id_payment)[0];

            //количество оплат после выбранной
            DataRow[] dr_laterdates = dt.Select("id>" + id_payment);

            if (dr_laterdates.Count() > 0)
            {
                MessageBox.Show("Разрешено удаление только последней оплаты по договору", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            DialogResult d = MessageBox.Show("Удалить выбранную запись?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (d == DialogResult.Yes)
            {
                
                DataTable dtDetails = _proc.GetPaymentDetails(id_payment);

                string operation = "Удаление оплаты по договору № " + txtNum.Text + ", id договора = " + id.ToString();

                Logging.StartFirstLevel(182);
                Logging.Comment(operation);
                Logging.Comment("");
                Logging.Comment("id оплаты = " + id_payment.ToString());
                Logging.Comment("Дата: " + dr["PaymentDate"].ToString());
                Logging.Comment("Сумма оплаты: " + dr["PaymentSum"].ToString());
                Logging.Comment("Признак оплаты: " +
                    ((!Reklama) ? "Аренда" : "Реклама")
                    );
                Logging.Comment("");

                for (int i = 0; dtDetails.Rows.Count > i; i++)
                {
                    string text =
                        "Сумма оплаты " + dtDetails.Rows[i]["Summa"].ToString()
                        + ", "
                        + "Просрочено " + dtDetails.Rows[i]["Delay"].ToString()
                        + ", "
                        + "Пени " + dtDetails.Rows[i]["Peni"].ToString()
                        + ", "
                        + "Месяц оплаты " + GetMonthYear(dtDetails.Rows[i]["Month"].ToString())
                        + ", "
                        + "в том числе сумма за телефон " + dtDetails.Rows[i]["Phone"].ToString();

                    Logging.Comment(text);
                }

                Logging.Comment("");
                Logging.Comment("Завершение операции \"" + operation + "\"");
                Logging.StopFirstLevel();

                _proc.DelPayment(id_payment);
            }
            GetData();
        }

        private string GetMonthYear(string somedate)
        {
            string res = "";

            DateTime date = DateTime.Parse(somedate);

            int numMonth = date.Month;

            switch (numMonth)
            {
                case 1: { res = "январь"; break; }
                case 2: { res = "февраль"; break; }
                case 3: { res = "март"; break; }
                case 4: { res = "апрель"; break; }
                case 5: { res = "май"; break; }
                case 6: { res = "июнь"; break; }
                case 7: { res = "июль"; break; }
                case 8: { res = "август"; break; }
                case 9: { res = "сентябрь"; break; }
                case 10: { res = "октябрь"; break; }
                case 11: { res = "ноябрь"; break; }
                case 12: { res = "декабрь"; break; }
            }

            return res + " " + date.Year;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

            Logging.StartFirstLevel(79);
            Logging.Comment("Выгрузка отчета со списком оплат по договору в Excel файл");
            Logging.Comment("Дата заключения договора: " + dateDoc);
            Logging.Comment("№ договора: " + numDoc);
            Logging.Comment("Арендатор ID: " + idArend + " ; Наименование: " + nameArend);

            Logging.Comment("Место: "+ position);

            Logging.Comment("Дата начала:" + dateStartDoc);
            Logging.Comment("Дата окончания:" + dateEndDoc);
            

            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
            + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();


            HandmadeReport Rep = new HandmadeReport();

            Rep.AddSingleValue("Список оплат по договору", 2, 2);
            Rep.SetFontSize(2, 2, 2, 2, 14);
            Rep.SetFontBold(2, 2, 2, 2);

            Rep.AddSingleValue("№ договора:", 4, 2);
            Rep.AddSingleValue("Дата договора:", 5, 2);
            Rep.AddSingleValue("Арендатор:", 6, 2);
            Rep.AddSingleValue("Аренда (руб.):", 7, 2);

            string date = dtpDate.Value.ToShortDateString();

            Rep.AddSingleValue(txtNum.Text, 4, 4);
            //Rep.AddSingleValue(dtpDate.Value.ToString("dd.MM.yyyy"), 5, 4);
            Rep.AddSingleValue(date, 5, 4);            
            Rep.AddSingleValue(txtTenant.Text, 6, 4);
            Rep.AddSingleValue(txtSum.Text, 7, 4);

            DataTable dtPrint = GridToDataTable.GetDataTableFromGridWithNum(grdPayments);

            for (int i=0; dtPrint.Rows.Count > i; i++)
            {
                try
                {
                    dtPrint.Rows[i]["Дата оплаты"] = DateTime.Parse(dtPrint.Rows[i]["Дата оплаты"].ToString()).ToShortDateString();
                }
                catch { }
            }

            Rep.AddMultiValue(dtPrint, 9, 1);
            Rep.SetFontBold(9, 1, 9, grdPayments.Columns.Count);

            //Итого
            decimal total = 0;

            for (int i = 0; grdPayments.Rows.Count > i; i++)
            {
                total += decimal.Parse(numTextBox.ConvertToCompPunctuation(grdPayments.Rows[i].Cells["PaymentSum"].Value.ToString()));
            }

            Rep.AddSingleValue("ИТОГО:", grdPayments.Rows.Count + 10, 2);
            Rep.AddSingleValue(numTextBox.ConvertToSqlPunctuation(total.ToString("0.00")), grdPayments.Rows.Count + 10, 3);

            Rep.SetColumnAutoSize(1, 1, grdPayments.Rows.Count + 10, dtPrint.Columns.Count+2);

            /*
            Rep.SetColumnWidth(1, 1, 1, 1, 13);
            Rep.SetColumnWidth(1, 2, 1, 2, 10);
            Rep.SetColumnWidth(1, 3, 1, 3, 10);
            Rep.SetColumnWidth(1, 4, 1, 4, 10);
            */

            Rep.SetBorders(9, 1, grdPayments.Rows.Count + 10, dtPrint.Columns.Count);
            /*
            Rep.SetCellAlignmentToCenter(5, 1, 5, grdReal.Columns.Count);
            Rep.SetCellAlignmentToCenter(6, 1, grdReal.Rows.Count + 5, 1);
            Rep.SetCellAlignmentToRight(6, 2, grdReal.Rows.Count + 7, grdReal.Columns.Count);
            */
                       

            /*
            Rep.SetFormat(6, 2, grdReal.Rows.Count + 7, 2, "0,000");
            Rep.SetFormat(6, 3, grdReal.Rows.Count + 7, grdReal.Columns.Count, "0,00");
            Rep.SetFormat(6, 1, grdReal.Rows.Count + 5, 1, "ДД.ММ.ГГГГ");
            */

            Rep.Show();



        }

        private string dateDoc, numDoc, nameArend, position, dateStartDoc, dateEndDoc, idArend;
        //private int idArend;

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
