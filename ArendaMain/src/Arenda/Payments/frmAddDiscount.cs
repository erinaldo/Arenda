using Nwuram.Framework.Settings.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arenda.Payments
{
    public partial class frmAddDiscount : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        private bool isEditData = false;

        public int id_Agreements { set; private get; }
        public frmAddDiscount()
        {
            InitializeComponent();
            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose, "Выход");
            tp.SetToolTip(btSave, "Сохранить");
        }

        private void frmAddDiscount_Load(object sender, EventArgs e)
        {
            DataTable dtTypeDiscount = _proc.getTypeDiscount(false);

            cmbTypeDicount.DisplayMember = "cName";
            cmbTypeDicount.ValueMember = "id";
            cmbTypeDicount.DataSource = dtTypeDiscount;
            //cmbTypeDicount.SelectedIndex = -1;
            cmbTypeDicount_SelectionChangeCommitted(null, null);
            isEditData = false;
        }

        private void frmAddDiscount_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = isEditData && DialogResult.No == MessageBox.Show("На форме есть не сохранённые данные.\nЗакрыть форму без сохранения данных?\n", "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void tbPercentDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.ToString().Contains(e.KeyChar) || (sender as TextBox).Text.ToString().Length == 0))
            {
                e.Handled = true;
            }
            else
                if ((!Char.IsNumber(e.KeyChar) && (e.KeyChar != ',')))
            {
                if (e.KeyChar != '\b')
                { e.Handled = true; }
            }
        }

        private void tbDiscountPrice_Validating(object sender, CancelEventArgs e)
        {
            if ((sender as TextBox).Text.Length > 0)
            {
                (sender as TextBox).Text = decimal.Parse((sender as TextBox).Text.Replace(".", ",")).ToString("0.00");
            }
            else
                (sender as TextBox).Text = "0,00";
            isEditData = true;
        }

        private void chbUnlimitedDiscount_Click(object sender, EventArgs e)
        {
            lDateEnd.Visible = dtpEnd.Visible = !chbUnlimitedDiscount.Checked;
        }

        private void cmbTypeDicount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbTypeDicount.SelectedValue == null) return;

            if ((int)cmbTypeDicount.SelectedValue == 1) {
                lPercentDiscount.Text = "Процент скидки от общей стоимости договора";
                tbPercentDiscount.Text = "0.00";
            }
            else
                if ((int)cmbTypeDicount.SelectedValue == 2) {
                lPercentDiscount.Text = "Новая цена стоимости 1 квадратного мета";
                tbPercentDiscount.Text = "0.00";
            }
            isEditData = true;
            //"Процент скидки от общей стоимости договора";
            //"Новая цена стоимости 1 квадратного мета";
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (cmbTypeDicount.SelectedIndex == -1)
            {
                MessageBox.Show(TempData.centralText($"Необходимо выбрать\n \"{lTypeDiscont.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTypeDicount.Focus();
                return;
            }

            if (tbPercentDiscount.Text.Trim().Length == 0)
            {
                MessageBox.Show(TempData.centralText($"Необходимо заполнить\n \"{lPercentDiscount.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbPercentDiscount.Focus();
                return;
            }

            decimal discount;
            if (!decimal.TryParse(tbPercentDiscount.Text.Replace(".",","), out discount))
            {
                MessageBox.Show(TempData.centralText($"Некорректное значение:\n \"{lPercentDiscount.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbPercentDiscount.Focus();
                return;
            }

            if (discount == 0)            
            {
                MessageBox.Show(TempData.centralText($"\"{lPercentDiscount.Text}\" не может ровняться 0\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbPercentDiscount.Focus();
                return;
            }

            DateTime dStart = dtpStart.Value.Date;
            DateTime? dEnd = null;
            if (!chbUnlimitedDiscount.Checked) dEnd = dtpEnd.Value.Date;
            int id_TypeDiscount = (int)cmbTypeDicount.SelectedValue;
            int id_Status = 1;


            DataTable dtResult = _proc.setTDiscount(0, id_Agreements, dStart, dEnd, id_TypeDiscount, id_Status, discount);

            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                MessageBox.Show("Не удалось сохранить данные", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if ((int)dtResult.Rows[0]["id"] == -1)
            {
                MessageBox.Show("В справочнике уже присутствует должность с таким наименованием.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if ((int)dtResult.Rows[0]["id"] == -9999)
            {
                MessageBox.Show("Произошла неведомая хрень.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            isEditData = false;
            MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpStart.Value.Date > dtpEnd.Value.Date)
                    dtpEnd.Value = dtpStart.Value.Date;
            }
            catch { }
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpStart.Value.Date > dtpEnd.Value.Date)
                    dtpStart.Value = dtpEnd.Value.Date;
            }
            catch { }
        }
    }
}
