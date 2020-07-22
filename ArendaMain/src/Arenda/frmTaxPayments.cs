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
    public partial class frmTaxPayments : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        string num;
        int id_Tax;
        int id_agreement;
        string defaultVal = "0.00";
        string pName = "";

        public frmTaxPayments(int _id_Tax, int _id_agreement, string _num, string _pName)
        {
            id_Tax = _id_Tax;
            id_agreement = _id_agreement;
            num = _num;
            pName = _pName;
            InitializeComponent();
        }

        private void frmTaxPayments_Load(object sender, EventArgs e)
        {
            if (TempData.Rezhim == "ПР")
            {
                this.Text = "Просмотр дополнительной оплаты";
                btnAdd.Visible = false;
                btnDel.Visible = false;
                gbxPaymentTax.Visible = false;

                toolTip1.SetToolTip(btnScan, "Просмотр изображения");
            } 

            DataTable dt = new DataTable();
            dt = _proc.GetTaxes(id_agreement);
            DataRow dr = dt.Select("id=" + id_Tax)[0];

            dtpDate.Value = DateTime.Parse(dr["TaxDate"].ToString());
            txtSum.Text = numTextBox.CheckAndChange(dr["penalty"].ToString(), 2, 0, 9999999999, false, defaultVal, "{0:# ### ### ##0.00}");                
            txtComment.Text = dr["Comment"].ToString().Trim();

            CboFill();

            GetTaxPayments();            
        }

        private void CboFill()
        {
            DataTable dtCombo = new DataTable();
            dtCombo.Columns.Add("cName", typeof(string));
            dtCombo.Columns.Add("id", typeof(int));

            DataRow all = dtCombo.NewRow();
            all["cName"] = pName;
            all["id"] = 0;
            dtCombo.Rows.InsertAt(all, 0);

            cboAnotherPay.DataSource = dtCombo;
            cboAnotherPay.DisplayMember = "cName";
            cboAnotherPay.ValueMember = "id";
            cboAnotherPay.SelectedValue = 0;
        }

        private void GetTaxPayments()
        {
            DataTable dtTaxPayments = new DataTable();
            dtTaxPayments = _proc.GetTaxPayments(id_Tax);

            grdPayments.AutoGenerateColumns = false;

            decimal PaymentSum = 0;

            if ((dtTaxPayments != null) && (dtTaxPayments.Rows.Count > 0))
            {
                for (int i = 0; dtTaxPayments.Rows.Count > i; i++)
                {
                    PaymentSum += decimal.Parse(numTextBox.ConvertToCompPunctuation(dtTaxPayments.Rows[i]["Summa"].ToString()));
                    dtTaxPayments.Rows[i]["Summa"] = numTextBox.ConvertToSqlPunctuation(dtTaxPayments.Rows[i]["Summa"].ToString());
                }

                grdPayments.DataSource = dtTaxPayments;
                btnDel.Enabled = true;
            }
            else
            {
                grdPayments.DataSource = null;
                txtEditor.Text = ""; txtDateEdit.Text = "";
                btnDel.Enabled = false;
            }

            txtPayment.Text = numTextBox.CheckAndChange(PaymentSum.ToString().Trim(), 2, 0, 999999999999, false, "0.00", "{0:# ### ### ##0.00}");

            dtpDatePaymentTax.Value = _proc.getCurDate();            
            txtPaymentTax.Text = numTextBox.CheckAndChange(defaultVal, 2, 0, 9999999999, false, defaultVal, "{0:# ### ### ##0.00}");
        }

        private void grdPayments_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txtEditor.Text = grdPayments.CurrentRow.Cells["Editor"].Value.ToString();
                txtDateEdit.Text = grdPayments.CurrentRow.Cells["DateEdit"].Value.ToString();                
            }
            catch { txtEditor.Text = ""; txtDateEdit.Text = ""; }
        }

        private void txtPaymentTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            numTextBox.KeyPress(txtPaymentTax, e, false, false);
        }

        private void txtPaymentTax_Leave(object sender, EventArgs e)
        {
            txtPaymentTax.Text = numTextBox.CheckAndChange(txtPaymentTax.Text, 2, 0, 9999999999, false, defaultVal, "{0:# ### ### ##0.00}");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dtpDatePaymentTax.Value.Date < dtpDate.Value.Date)
            {
                MessageBox.Show("Дата выписки - " + dtpDate.Value.ToShortDateString() + "\nДата оплаты не может быть меньше!");
                return;
            }

            if (txtPaymentTax.Text == defaultVal)
            {
                MessageBox.Show("Введите сумму оплаты!");
                return;
            }
            
            DataTable dtTaxPayments = new DataTable();
            dtTaxPayments = _proc.GetTaxPayments(id_Tax);

            decimal PaymentSum = 0;

            if ((dtTaxPayments != null) && (dtTaxPayments.Rows.Count > 0))
            {
                for (int i = 0; dtTaxPayments.Rows.Count > i; i++)
                {
                    PaymentSum += decimal.Parse(numTextBox.ConvertToCompPunctuation(dtTaxPayments.Rows[i]["Summa"].ToString()));
                }
            }

            PaymentSum += decimal.Parse(numTextBox.ConvertToCompPunctuation(txtPaymentTax.Text));

            decimal TotalTax = decimal.Parse(numTextBox.ConvertToCompPunctuation(txtSum.Text));

            if (TotalTax < PaymentSum)
            {
                MessageBox.Show("Сумма всех оплат не может превышать сумму выписанной доп. оплаты!");
                return;
            }

            int id_row = 0;
            id_row = _proc.AddTaxPayments(id_Tax, dtpDatePaymentTax.Value.Date,
                                 decimal.Parse(numTextBox.ConvertToCompPunctuation(txtPaymentTax.Text)));


            string operation = "Добавление оплаты дополнительной оплаты по договору № " + num + ", id договора = " + id_agreement.ToString();

            Logging.StartFirstLevel(1408);

            Logging.Comment(operation);
            Logging.Comment("id дополнительной оплаты = " + id_Tax.ToString());
            Logging.Comment("Сумма дополнительной оплаты = " + txtSum.Text);
            Logging.Comment("Id и наименование дополнительной оплаты = " + cboAnotherPay.SelectedValue.ToString() + ", " + cboAnotherPay.Text);
            Logging.Comment("");
            Logging.Comment("id оплаты дополнительной оплаты = " + id_row.ToString());
            Logging.Comment("Дата: " + dtpDatePaymentTax.Value.ToShortDateString());
            Logging.Comment("Сумма оплаты дополнительной оплаты: " + txtPaymentTax.Text);
            Logging.Comment("");
            Logging.Comment("Завершение операции \"" + operation + "\"");
            Logging.StopFirstLevel();

            
            MessageBox.Show("Оплата добавлена", "Сообщение", MessageBoxButtons.OK);
            GetTaxPayments();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Удалить выбранную запись?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (d == DialogResult.Yes)
            {
                int id = int.Parse(grdPayments.CurrentRow.Cells["id"].Value.ToString());

                string operation = "Удаление оплаты дополнительной оплаты по договору № " + num + ", id договора = " + id_agreement.ToString();

                Logging.StartFirstLevel(1409);

                Logging.Comment(operation);
                Logging.Comment("id дополнительной оплаты = " + id_Tax.ToString() + ", " + cboAnotherPay.Text);
                Logging.Comment("Сумма дополнительной оплаты = " + txtSum.Text);
                Logging.Comment("");
                Logging.Comment("id оплаты дополнительной оплаты = " + id.ToString());
                Logging.Comment("Дата: " + grdPayments.CurrentRow.Cells["TaxPaymentDate"].Value.ToString());
                Logging.Comment("Сумма оплаты дополнительной оплаты: " + grdPayments.CurrentRow.Cells["PaymentSum"].Value.ToString());
                Logging.Comment("");
                Logging.Comment("Завершение операции \"" + operation + "\"");
                Logging.StopFirstLevel();


                MessageBox.Show("Оплата удалена", "Сообщение", MessageBoxButtons.OK);

                _proc.DelTaxPayment(id);
            }
            GetTaxPayments();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            frmScannedDocs frmScan = new frmScannedDocs(id_Tax, cboAnotherPay.Text, dtpDate.Value, txtSum.Text);
            frmScan.setData(dateDoc, numDoc, nameArend, position, dateStartDoc, dateEndDoc, idArend);
            frmScan.ShowDialog();
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
