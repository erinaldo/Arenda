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

namespace Arenda
{
    public partial class frmListTaxes : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        int id;
        DataTable dtTaxes;
        BindingSource bs;

        public frmListTaxes(int _id)
        {
            id = _id;
            InitializeComponent();
            grdPayments.AutoGenerateColumns = false;
            
        }

        private void frmListTaxes_Load(object sender, EventArgs e)
        {
            //if (TempData.Rezhim == "ПР")
            if(new List<string> { "СБ6", "Д", "ПР"}.Contains(TempData.Rezhim))
            {
                toolTip1.SetToolTip(btnScan, "Просмотр изображения");
                toolTip1.SetToolTip(btnPay, "Просмотр дополнительной оплаты к договору");
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnDel.Visible = false;
                
                
            } 

            DataTable dt = new DataTable();
            dt = _proc.GetLD(id);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                txtNum.Text = dt.Rows[0]["Agreement"].ToString().Trim();                
                txtTenant.Text = dt.Rows[0]["Tenant_name"].ToString().Trim();
                idArend = dt.Rows[0]["id_Tenant"].ToString();
                GetData();
            }
            else
            {                
                this.Close();
            }
        }

        private void cboAnotherPayFill()
        {
            DataTable dtAnotherPay = new DataTable();

            dtAnotherPay = _proc.GetAnotherPaymentsForAgreement(id);

            DataRow all = dtAnotherPay.NewRow();
            all["cName"] = "Все";
            all["id"] = 0;
            dtAnotherPay.Rows.InsertAt(all, 0);

            cboAnotherPay.DataSource = dtAnotherPay;
            cboAnotherPay.DisplayMember = "cName";
            cboAnotherPay.ValueMember = "id";
            cboAnotherPay.SelectedValue = 0;
        }

        private void GetData()
        {
            cboAnotherPayFill();

            dtTaxes = new DataTable();
            dtTaxes = _proc.GetTaxes(id);

            if ((dtTaxes != null) && (dtTaxes.Rows.Count > 0))
            {
                for (int i = 0; dtTaxes.Rows.Count > i; i++)
                {
                    dtTaxes.Rows[i]["penalty"] = numTextBox.ConvertToSqlPunctuation(dtTaxes.Rows[i]["penalty"].ToString());
                    dtTaxes.Rows[i]["PaymentSum"] = numTextBox.ConvertToSqlPunctuation(dtTaxes.Rows[i]["PaymentSum"].ToString());
                    dtTaxes.Rows[i]["Debt"] = numTextBox.ConvertToSqlPunctuation(dtTaxes.Rows[i]["Debt"].ToString());
                }
            }

            dtTaxes.AcceptChanges();
            FilterDataView();            
        }

        private void FilterDataView()
        {
            bs = new BindingSource();

            if ((dtTaxes != null) && (dtTaxes.Rows.Count > 0))
            {
                Filter();
            }
            else
            {
                //если нет данных или ошибка получения данных
                EmptyGrid();
            }

            ButtonsAndTexts();
            grdPayments_SelectionChanged(null, null);
        }

        private void Filter()
        {            
            bs.DataSource = dtTaxes.DefaultView;
            grdPayments.AutoGenerateColumns = false;
            grdPayments.DataSource = bs;

            try
            {
                string filter = "";
                
                filter += ((cboAnotherPay.SelectedValue != null && cboAnotherPay.SelectedValue.ToString() != "0")
                            ? (filter.Length > 0 ? " AND " : "") + "PaymentId = " + cboAnotherPay.SelectedValue.ToString()
                            : "");

                bs.Filter = filter;
            }
            catch
            {
            }
        }

        private void EmptyGrid()
        {
            grdPayments.AutoGenerateColumns = false;
            grdPayments.DataSource = bs;
            bs.Filter = "0=1";
        }

        private void ButtonsAndTexts()
        {
            btnEdit.Enabled
                = btnDel.Enabled
                = btnPay.Enabled 
                = btnScan.Enabled 
                = (grdPayments.Rows.Count > 0);

            decimal penalty = 0;
            decimal PaymentSum = 0;
            decimal Debt = 0;

            for (int i = 0; grdPayments.Rows.Count > i; i++)
            {
                penalty += decimal.Parse(numTextBox.ConvertToCompPunctuation(grdPayments.Rows[i].Cells["penalty"].Value.ToString()));

                PaymentSum += decimal.Parse(numTextBox.ConvertToCompPunctuation(grdPayments.Rows[i].Cells["PaymentSum"].Value.ToString()));

                Debt += decimal.Parse(numTextBox.ConvertToCompPunctuation(grdPayments.Rows[i].Cells["Debt"].Value.ToString()));
            }

            txtPenalty.Text = numTextBox.CheckAndChange(penalty.ToString().Trim(), 2, 0, 999999999999, false, "0.00", "{0:# ### ### ##0.00}");
            txtPayment.Text = numTextBox.CheckAndChange(PaymentSum.ToString().Trim(), 2, 0, 999999999999, false, "0.00", "{0:# ### ### ##0.00}");
            txtDebt.Text = numTextBox.CheckAndChange(Debt.ToString().Trim(), 2, 0, 999999999999, false, "0.00", "{0:# ### ### ##0.00}");
            
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListTaxes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                Add();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void Add()
        {
            frmAddTaxes frmAddT = new frmAddTaxes(0, id, txtNum.Text);
            frmAddT.ShowDialog();
            GetData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id_tax = int.Parse(grdPayments.CurrentRow.Cells["id_tax"].Value.ToString());

            DataTable dtTaxPayments = new DataTable();
            dtTaxPayments = _proc.GetTaxPayments(id_tax);

            if (dtTaxPayments.Rows.Count > 0)
            {
                MessageBox.Show("Редактирование невозможно, т.к. была произведена оплата!");
                GetData();
                return;
            }

            try
            {                
                frmAddTaxes frmAddT = new frmAddTaxes(id_tax, id, txtNum.Text);
                frmAddT.ShowDialog();
                GetData();
            }
            catch { }   
        }

        private void grdPayments_SelectionChanged(object sender, EventArgs e)
        {
            if(grdPayments.CurrentRow==null || grdPayments.CurrentRow.Index == -1)
            {
                txtEditor.Text = ""; txtDateEdit.Text = ""; txtComment.Text = "";
                btnDel.Enabled = btnEdit.Enabled = false;
                return;
            }

            try
            {
                txtEditor.Text = grdPayments.CurrentRow.Cells["Editor"].Value.ToString();
                txtDateEdit.Text = grdPayments.CurrentRow.Cells["DateEdit"].Value.ToString();
                txtComment.Text = grdPayments.CurrentRow.Cells["Comment"].Value.ToString();
                btnDel.Enabled = btnEdit.Enabled = !(bool)grdPayments.CurrentRow.Cells["isConfirmed"].Value;
            }
            catch
            {
                txtEditor.Text = ""; txtDateEdit.Text = ""; txtComment.Text = "";
                btnDel.Enabled = btnEdit.Enabled = false;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int id_tax = int.Parse(grdPayments.CurrentRow.Cells["id_tax"].Value.ToString());

            DataTable dtTaxPayments = new DataTable();
            dtTaxPayments = _proc.GetTaxPayments(id_tax);

            if (dtTaxPayments.Rows.Count > 0)
            {
                MessageBox.Show("Удаление невозможно, т.к. была произведена оплата!");
                GetData();
                return;
            }

            DialogResult d = MessageBox.Show("Удалить выбранную запись?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (d == DialogResult.Yes)
            {
                string operation = "Удаление дополнительной оплаты по договору № " + txtNum.Text + " , id договора = " + id.ToString();

                DataTable dt = new DataTable();
                dt = _proc.GetTaxes(id);
                DataRow dr = dt.Select("id=" + id_tax)[0];

                Logging.StartFirstLevel(1407);
                Logging.Comment(operation);
                Logging.Comment("");
                Logging.Comment("id дополнительной оплаты = " + id_tax.ToString());
                Logging.Comment("Дата: " + dr["TaxDate"].ToString());
                Logging.Comment("Сумма дополнительной оплаты: " + dr["penalty"].ToString());
                Logging.Comment("Сумма оплат дополнительной оплаты: " + dr["PaymentSum"].ToString());
                Logging.Comment("Примечание: " + dr["Comment"].ToString().Trim());
                Logging.Comment("");
                Logging.Comment("Завершение операции \"" + operation + "\"");
                Logging.StopFirstLevel();

                _proc.DelTax(id_tax);
            }
            GetData();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            int id_tax = int.Parse(grdPayments.CurrentRow.Cells["id_tax"].Value.ToString());
            string pName = grdPayments.CurrentRow.Cells["PaymentName"].Value.ToString();

            frmTaxPayments frmTaxPaym = new frmTaxPayments(id_tax, id, txtNum.Text, pName) { ShowInTaskbar = false };
            frmTaxPaym.setData(dateDoc, numDoc, nameArend, position, dateStartDoc, dateEndDoc, idArend);
            frmTaxPaym.ShowDialog();
            GetData();
        }

        private void cboAnotherPay_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterDataView();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            int id_tax = int.Parse(grdPayments.CurrentRow.Cells["id_tax"].Value.ToString());
            string PntName = grdPayments.CurrentRow.Cells["PaymentName"].Value.ToString();
            DateTime dd = DateTime.Parse(grdPayments.CurrentRow.Cells["TaxDate"].Value.ToString());
            string sum = grdPayments.CurrentRow.Cells["penalty"].Value.ToString();


            frmScannedDocs frmScan = new frmScannedDocs(id_tax, PntName, dd, sum) { ShowInTaskbar = false };
            frmScan.setData(dateDoc, numDoc, nameArend, position, dateStartDoc, dateEndDoc, idArend);
            frmScan.ShowDialog();
            GetData();
        }

        private string dateDoc, numDoc, nameArend, position, dateStartDoc, dateEndDoc, idArend;

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
