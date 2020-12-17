using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arenda.Bank
{
    public partial class frmAddBank : Form
    {
        private int? idBank = null;
        private int id;
        private bool isEditData = false;


        public bool isEdit { set; private get; }

        public frmAddBank()
        {
            InitializeComponent();
            ToolTip tp = new ToolTip();
            tp.SetToolTip(btExit, "Выход");
            tp.SetToolTip(btAdd, "Сохранить");
        }

        private void btSelectBank_Click(object sender, EventArgs e)
        {
            var b = new frmBanks(1);
            if (DialogResult.OK == b.ShowDialog())
            {
                idBank = dataBank.id;
                tbName.Text = dataBank.cName;
                tbKS.Text = dataBank.cA;
                tbBik.Text = dataBank.BIK;
                isEditData = true;
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (idBank == null)
            {
                MessageBox.Show("Необходимо выбрать банк!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal _tmpInt;

            if (tbRS.Text.Trim().Length != 20 || !decimal.TryParse(tbRS.Text.Trim(),out _tmpInt))
            {
                MessageBox.Show($"Необходимо заполнить: \"{label4.Text}\"", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!((AddEditTenant)this.Owner).validateBankRow(id, (int)idBank, tbRS.Text.Trim(), true))
            {
                MessageBox.Show(TempData.centralText("В справочнике уже присутствует\nзапись с введёнными реквизитами.\n"), "Проверка на дубли",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            if (isEdit)
                ((AddEditTenant)this.Owner).updateRowBank((int)idBank, tbName.Text, tbBik.Text, tbKS.Text, tbRS.Text);
            else
                ((AddEditTenant)this.Owner).addRowBank((int)idBank, tbName.Text, tbBik.Text, tbKS.Text, tbRS.Text);

            isEditData = false;
            this.DialogResult = DialogResult.OK;
        }

        private void tbRS_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != '\b';
        }

        private void frmAddBank_Load(object sender, EventArgs e)
        {
            if (isEdit)
            {
                DataRowView row = ((AddEditTenant)this.Owner).GetBankRow();
                id = (int)row["id"];
                idBank = (int)row["id_Bank"];
                tbRS.Text =  row["PaymentAccount"].ToString();
                tbName.Text =  row["cName"].ToString();
                tbBik.Text = row["BIC"].ToString();
                tbKS.Text =  row["CorrespondentAccount"].ToString();
            }
            isEditData = false;
        }

        private void frmAddBank_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = isEditData && DialogResult.No == MessageBox.Show("На форме есть не сохранённые данные.\nЗакрыть форму без сохранения данных?\n", "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        private void tbRS_TextChanged(object sender, EventArgs e)
        {
            isEditData = true;
        }
    }
}
