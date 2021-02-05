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
    public partial class frmSelectFines : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        public int id_agreements { set; private get; }
        public int id_fine { private set; get; }
        private DataTable dtData;
        public frmSelectFines()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
        }

        private void frmSelectFines_Load(object sender, EventArgs e)
        {
            getDatat();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            if (dtData == null || dtData.Rows.Count == 0 || dtData.DefaultView.Count == 0) return;

            infoPay.id = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];
            infoPay.Summa = (decimal)dtData.DefaultView[dgvData.CurrentRow.Index]["Summa"];
            infoPay.DateFines = (DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["DateFines"];
            infoPay.cName = (string)dtData.DefaultView[dgvData.CurrentRow.Index]["cName"];
            infoPay.pfSumma = (decimal)dtData.DefaultView[dgvData.CurrentRow.Index]["pfSumma"];
            infoPay.resDolg = (decimal)dtData.DefaultView[dgvData.CurrentRow.Index]["resDolg"];
            infoPay.PlanDate = dtData.DefaultView[dgvData.CurrentRow.Index]["PlanDate"].ToString();
            infoPay.sumUse = (decimal)dtData.DefaultView[dgvData.CurrentRow.Index]["sumUse"];

            id_fine = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];

            this.DialogResult = DialogResult.OK;

        }

        private void getDatat()
        {
            dtData = _proc.getFineConfirmed(id_agreements, dtpStart.Value.Date, dtpEnd.Value.Date);
            dgvData.DataSource = dtData;
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
          
        }

        private void dtpStart_CloseUp(object sender, EventArgs e)
        {
            getDatat();
        }

        private void dtpStart_Leave(object sender, EventArgs e)
        {
            getDatat();
        }
    }
}
