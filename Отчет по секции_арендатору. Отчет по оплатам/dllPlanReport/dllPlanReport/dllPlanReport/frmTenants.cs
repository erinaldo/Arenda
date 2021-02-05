using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dllPlanReport
{
    public partial class frmTenants : Form
    {
        public int id_tenant = 0;
        public string name_tenant = "";
        public frmTenants()
        {
            InitializeComponent();
            dgvTenants.AutoGenerateColumns = false;
        }
        DataTable dtTenants;
        private void frmTenants_Load(object sender, EventArgs e)
        {
            Task<DataTable> task = Config.hCntMain.getTenants();
            task.Wait();
            dtTenants = task.Result;
            dgvTenants.DataSource = dtTenants;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            DataRow dr = dtTenants.DefaultView[dgvTenants.CurrentRow.Index].Row;
            id_tenant = (int)dr["id"];
            name_tenant = dr["aren"].ToString();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void filter()
        {
            string filter = "";
            try
            {
                if (tbTenant.Text.Length > 0)
                    filter += $"aren like '%{tbTenant.Text}%'";
            }
            catch
            {
                filter = "id = -1";
            }
            dtTenants.DefaultView.RowFilter = filter;
            enabledButtons();
        }
        void enabledButtons()
        {
            btnSelect.Enabled = dgvTenants.CurrentRow != null;
        }

        private void tbTenant_TextChanged(object sender, EventArgs e)
        {
            filter();
        }
    }
}
