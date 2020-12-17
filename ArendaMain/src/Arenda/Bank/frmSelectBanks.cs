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

namespace Arenda.Bank
{
    public partial class frmSelectBanks : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        private DataTable dtBanks;

        public int id_TanLord { set; private get; }

        public frmSelectBanks()
        {
            InitializeComponent();
            ToolTip tp = new ToolTip();
            tp.SetToolTip(btExit, "Выход");
            tp.SetToolTip(btSelect, "Выбрать");
        }

        private void frmSelectBanks_Load(object sender, EventArgs e)
        {
            GetBanks();
        }

        private void setFilter()
        {
            if (dtBanks == null || dtBanks.Rows.Count == 0)
            {
                btSelect.Enabled = false;
                return;
            }

            try
            {
                string filter = "";

                //if (tbNumber.Text.Trim().Length != 0)
                //    filter += (filter.Length == 0 ? "" : " and ") + $"cName like '%{tbNumber.Text.Trim()}%'";

                //if (!cbBankNotActive.Checked)
                filter += (filter.Length == 0 ? "" : " and ") + $"isActive = 1";

                dtBanks.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtBanks.DefaultView.RowFilter = "id = -9999999999999";
            }
            finally
            {
                btSelect.Enabled =
               dtBanks.DefaultView.Count != 0;
                //dgvBank_SelectionChanged(null, null);
            }
        }


        private void GetBanks()
        {
            dgvBank.AutoGenerateColumns = false;
            dtBanks = _proc.GetLandlordTenantBank(id_TanLord);
            setFilter();
            dgvBank.DataSource = dtBanks;
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;                
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            SetRowToForm();
        }

        private void SetRowToForm()
        {
            DataRowView row = dtBanks.DefaultView[dgvBank.CurrentRow.Index];
            ((AddeditDoc)this.Owner).addRowBank((int)row["id"], (int)row["id_Bank"], row["cName"].ToString(), row["BIC"].ToString(), row["CorrespondentAccount"].ToString(), row["PaymentAccount"].ToString());

            this.DialogResult = DialogResult.OK;
        }


        private void dgvBank_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && dtBanks.Rows.Count > 0)
            {
                SetRowToForm();
            }
        }
    }
}
