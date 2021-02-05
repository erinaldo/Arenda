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
    public partial class frmAddTaxes : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        string mode, num;
        int id;
        int id_agreement;
        string defaultVal = "0.00";

        DateTime oldDate = DateTime.Now;
        string oldSum;
        string oldComment;
        string oldPayment;
        int oldAnotherPayID;
        string oldAnotherPay;

        int oldPlaneDateID;
        string oldPlaneDate;

        public string txtNum { set; private get; }
        public string txtTenant { set; private get; }
        public string idArend { set; private get; }


        public frmAddTaxes(int _id, int _id_agreement, string _num)
        {
            id = _id;
            id_agreement = _id_agreement;
            mode = (_id == 0) ? "add" : "edit";
            num = _num;
            InitializeComponent();
        }

        private void frmAddTaxes_Load(object sender, EventArgs e)
        {

            DateTime nowDate = _proc.getdate();

            nowDate = nowDate.AddDays(-nowDate.Day+1);

            DataTable dtPlaneDate = new DataTable();
            dtPlaneDate.Columns.Add("Date", typeof(string));
            dtPlaneDate.Columns.Add("valueDate", typeof(DateTime));
            dtPlaneDate.Rows.Add($"{nowDate.AddMonths(-1).Month}.{nowDate.AddMonths(-1).Year}", nowDate.AddMonths(-1));
            dtPlaneDate.Rows.Add($"{nowDate.Month}.{nowDate.Year}", nowDate);
            dtPlaneDate.Rows.Add($"{nowDate.AddMonths(1).Month}.{nowDate.AddMonths(1).Year}", nowDate.AddMonths(1));
            cmbPlaneDate.DataSource = dtPlaneDate;
            cmbPlaneDate.DisplayMember = "Date";
            cmbPlaneDate.ValueMember = "valueDate";


            if (mode == "add")
            {
                this.Text = "Добавить доп. оплату по договору № " + num;
                cboAnotherPayFill(false);
                cboAnotherPay.SelectedValue = -1;
                cboAnotherPay_SelectionChangeCommitted(null, null);
            }
            else
            {
                this.Text = "Редактировать доп. оплату  по договору № " + num;
                DataTable dt = new DataTable();
                dt = _proc.GetTaxes(id_agreement);
                DataRow dr = dt.Select("id=" + id)[0];

                dtpDate.Value = DateTime.Parse(dr["TaxDate"].ToString());
                txtSum.Text = dr["penalty"].ToString();
                txtComment.Text = dr["Comment"].ToString().Trim();

                cboAnotherPayFill(true);
                cboAnotherPay.Enabled = false;
                try
                {
                    cboAnotherPay.SelectedValue = int.Parse(dr["PaymentId"].ToString());
                }
                catch
                {
                    cboAnotherPay.SelectedValue = -1;
                }
                cboAnotherPay_SelectionChangeCommitted(null, null);

                if (dr["PlanDate"] != DBNull.Value)
                {
                    cmbPlaneDate.SelectedValue = dr["PlanDate"];
                }

                if (dr["MetersData"] != DBNull.Value)
                {
                    tbDataMeters.Text = dr["MetersData"].ToString();
                }

                cmbPlaneDate.Enabled = false;
            }
            oldDate = dtpDate.Value;
            oldSum = txtSum.Text = numTextBox.CheckAndChange(txtSum.Text, 2, 0, 9999999999, false, defaultVal, "{0:# ### ### ##0.00}");
            oldComment = txtComment.Text.Trim();
            oldPayment = cboAnotherPay.Text;
            oldAnotherPayID = (cboAnotherPay.SelectedValue == null) ? 0 : int.Parse(cboAnotherPay.SelectedValue.ToString());
            oldAnotherPay = cboAnotherPay.Text;


            oldPlaneDateID = (cmbPlaneDate.SelectedValue == null) ? 0 : int.Parse(cmbPlaneDate.SelectedValue.ToString());
            oldPlaneDate = cmbPlaneDate.Text;

            tbDataMeters.Text = numTextBox.CheckAndChange(tbDataMeters.Text, 2, 0, 9999999999, false, defaultVal, "{0:# ### ### ##0.00}");

        }

        private void cboAnotherPayFill(bool all)
        {
            DataTable dtAnotherPay = new DataTable();

            dtAnotherPay = _proc.GetAnotherPayments(all);

            cboAnotherPay.DataSource = dtAnotherPay;
            cboAnotherPay.DisplayMember = "cName";
            cboAnotherPay.ValueMember = "id";            
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if ((oldDate != dtpDate.Value) 
                || (oldSum != txtSum.Text) 
                || (oldComment != txtComment.Text)
                || (oldPayment != cboAnotherPay.Text))
            {
                DialogResult d = MessageBox.Show("На форме есть несохраненные данные. \nЗакрыть форму без сохранения?", "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (d == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void txtSum_KeyPress(object sender, KeyPressEventArgs e)
        {
            numTextBox.KeyPress(txtSum, e, false, false);
        }

        private void txtSum_Leave(object sender, EventArgs e)
        {
            txtSum.Text = numTextBox.CheckAndChange(txtSum.Text, 2, 0, 9999999999, false, defaultVal, "{0:# ### ### ##0.00}");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSum.Text == defaultVal)
            {
                MessageBox.Show("Введите сумму доп. оплаты!");
                return;
            }

            if ((cboAnotherPay.SelectedValue == null)
                ||
                (int.Parse(cboAnotherPay.SelectedValue.ToString()) < 0))
            {
                MessageBox.Show("Выберите наименование доп. оплаты!");
                return;
            }

            DataTable dtAgreement = new DataTable();
            dtAgreement = _proc.GetLD(id_agreement);

            if ((dtAgreement == null) || (dtAgreement.Rows.Count == 0))
            {
                MessageBox.Show("Ошибка получения данных по договору!");
                return;
            }

            DateTime AgrDate = DateTime.Parse(dtAgreement.Rows[0]["Date_of_Conclusion"].ToString()).Date;
            if (dtpDate.Value.Date < AgrDate)
            {
                MessageBox.Show("Дата договора - " + AgrDate.ToShortDateString() + "\nДата доп. оплаты не может быть меньше!");
                return;
            }

						DateTime AgrDateEnd = DateTime.Parse(dtAgreement.Rows[0]["Stop_Date"].ToString()).Date;
						if (dtpDate.Value.Date > AgrDateEnd)
						{
							DialogResult d = MessageBox.Show("Дата выписки оплаты больше \nдаты окончания договора. \nСохранить введенные данные?", "Сохранение данных", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
							if (d == DialogResult.No)
							{
								return;
							}
						}

            DataTable dt = new DataTable();
            dt = _proc.CheckAnotherTaxes(id,
                                 id_agreement,
                                 dtpDate.Value.Date,
																 int.Parse(cboAnotherPay.SelectedValue.ToString())
                                 );

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                if (int.Parse(dt.Rows[0][0].ToString()) > 0)
                {
                    DialogResult d = MessageBox.Show("На дату по выбранной доп. оплате \nуже есть запись. \nСохранить введенные данные?", "Сохранение данных", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (d == DialogResult.Yes)
                    {
                        save();
                    }
                }
                else
                {
                    save();
                }
            }
            else
            {
                //MessageBox.Show("Ошибка соединения с сервером");
                return;
            }
        }

        private void cboAnotherPay_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboAnotherPay.SelectedValue == null)
            {
                lMeters.Visible = tbDataMeters.Visible = false;
                return;
            }
            EnumerableRowCollection<DataRow> rowCollect = (cboAnotherPay.DataSource as DataTable).AsEnumerable().Where(r => r.Field<int>("id") == (int)cboAnotherPay.SelectedValue && r.Field<bool>("isMeter"));

            if (rowCollect.Count() > 0)
            {
                lMeters.Visible = tbDataMeters.Visible = true;
            }
            else
            {
                lMeters.Visible = tbDataMeters.Visible = false;
            }
        }

        private void tbDataMeters_Leave(object sender, EventArgs e)
        {
            tbDataMeters.Text = numTextBox.CheckAndChange(tbDataMeters.Text, 2, 0, 9999999999, false, defaultVal, "{0:# ### ### ##0.00}");
        }

        private void save()
        {
            int id_row = 0;

            DateTime datePlane = DateTime.Parse(cmbPlaneDate.Text);
            decimal? _metes = null;
            if (tbDataMeters.Visible)
                _metes = decimal.Parse(tbDataMeters.Text.Replace(".", ","));


            id_row  = _proc.AddEditTaxes(id,
                     id_agreement,
                     dtpDate.Value.Date,
                     decimal.Parse(numTextBox.ConvertToCompPunctuation(txtSum.Text)),
                     txtComment.Text.Trim(),
                     int.Parse(cboAnotherPay.SelectedValue.ToString()),
                     datePlane,
                     _metes
                     );

            

            string operation = "";
            if (mode == "add")
            {
                Logging.StartFirstLevel(1405);

                operation = "Добавление дополнительной оплаты по договору № " + num + ", id договора = " + id_agreement.ToString();

                Logging.Comment(operation);


                Logging.Comment("Информация по договору:");
                Logging.Comment($"ID договора:{id_agreement}");
                Logging.Comment($"Номер договора:{txtNum}");
                Logging.Comment($"ID арендатора:{idArend}");
                Logging.Comment($"Наименование арендатора:{txtTenant}");
                
                Logging.Comment("");
                Logging.Comment("id записи = " + id_row.ToString());
                Logging.Comment("Id и наименование План отчёта: " + cmbPlaneDate.SelectedValue.ToString() + ", " + cmbPlaneDate.Text);
                Logging.Comment("Id и наименование дополнительной оплаты: " + cboAnotherPay.SelectedValue.ToString() + ", " + cboAnotherPay.Text);
                Logging.Comment("Дата: " + dtpDate.Value.ToShortDateString());
                Logging.Comment("Сумма: " + txtSum.Text);
                Logging.Comment("Примечание: " + txtComment.Text);
            }
            else
            {
                Logging.StartFirstLevel(1406);

                operation = "Редактирование дополнительной оплаты по договору № " + num + ", id договора = " + id_agreement.ToString();

                Logging.Comment(operation);
                Logging.Comment("Информация по договору:");
                Logging.Comment($"ID договора:{id_agreement}");
                Logging.Comment($"Номер договора:{txtNum}");
                Logging.Comment($"ID арендатора:{idArend}");
                Logging.Comment($"Наименование арендатора:{txtTenant}");

                Logging.Comment("");
                Logging.Comment("id записи = " + id.ToString());

                Logging.VariableChange("Id и наименование План отчёта: ",
                    cmbPlaneDate.SelectedValue.ToString() + ", " + cmbPlaneDate.Text,
                    oldPlaneDateID.ToString() + ", " + oldPlaneDate);

                Logging.VariableChange("Id и наименование дополнительной оплаты: ", 
                    cboAnotherPay.SelectedValue.ToString() + ", " + cboAnotherPay.Text, 
                    oldAnotherPayID.ToString() + ", " + oldAnotherPay);
                Logging.VariableChange("Дата", dtpDate.Value.ToShortDateString(), oldDate.ToShortDateString());
                Logging.VariableChange("Сумма", txtSum.Text, oldSum);
                Logging.VariableChange("Примечание",
                    txtComment.Text,
                    oldComment);
            }

            Logging.Comment("");
            Logging.Comment("Завершение операции \"" + operation + "\"");
            Logging.StopFirstLevel();


            oldDate = dtpDate.Value;
            oldSum = txtSum.Text = numTextBox.CheckAndChange(txtSum.Text, 2, 0, 9999999999, false, defaultVal, "{0:# ### ### ##0.00}");
            oldComment = txtComment.Text.Trim();

            MessageBox.Show("Данные сохранены", "Сообщение", MessageBoxButtons.OK);
            this.Close();
        }
    }
}
