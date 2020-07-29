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

        public int id_Agreements { set; private get; }
        public frmAddDiscount()
        {
            InitializeComponent();
        }

        private void frmAddDiscount_Load(object sender, EventArgs e)
        {
            DataTable dtTypeDiscount = _proc.getTypeDiscount(false);

            cmbTypeDicount.DisplayMember = "cName";
            cmbTypeDicount.ValueMember = "id";
            cmbTypeDicount.DataSource = dtTypeDiscount;
            cmbTypeDicount.SelectedIndex = -1;
        }

        private void frmAddDiscount_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
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
                (sender as TextBox).Text = decimal.Parse((sender as TextBox).Text).ToString("0.00");
            }
            else
                (sender as TextBox).Text = "0,00";
        }
    }
}
