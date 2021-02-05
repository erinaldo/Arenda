using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;

namespace dllPlanReport
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
            // Task.Run(() => get_data());
          
        }
        int id_tenant = 0;
        private void DoOnUIThread(MethodInvoker d)
        {
            if (this.InvokeRequired) { this.Invoke(d); } else { d(); }
        }
        Nwuram.Framework.UI.Forms.frmLoad frm;
        private void get_data()
        {
            int crowPerc = 0;
            DoOnUIThread(delegate ()
            {
                frm = new Nwuram.Framework.UI.Forms.frmLoad();
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.Owner = this;
                frm.TopMost = false;
                frm.TextWait = $"Формирование планов";
                frm.Show();
            });

            PlanReport plan = new PlanReport();
            plan.createTable();
            DoOnUIThread(delegate ()
            {
                frm.Dispose();
            });
        }
        DataTable dtSections;
        private void cmb_init()
        {
            Task<DataTable> task = Config.hCntMain.getObjects();
            task.Wait();
            cmbObject.DataSource = task.Result;
            cmbObject.DisplayMember = "cName";
            cmbObject.ValueMember = "id";
            task = Config.hCntMain.getBuildings();
            task.Wait();
            cmbBuilding.DataSource = task.Result;
            cmbBuilding.DisplayMember = "cName";
            cmbBuilding.ValueMember = "id";
            task = Config.hCntMain.getFloors();
            task.Wait();
            cmbFloor.DataSource = task.Result;
            cmbFloor.DisplayMember = "cName";
            cmbFloor.ValueMember = "id";
            task = Config.hCntMain.getSections();
            task.Wait();
            dtSections = task.Result;
            cmbSection.DataSource = dtSections;
            cmbSection.DisplayMember = "nameSection";
            cmbSection.ValueMember = "id";
           
            filter_section(null,null);
        }

        private void filter_section(object sender, EventArgs e)
        {
            string filter = "";
            if ((int)cmbObject.SelectedValue != 0)
                filter += (filter.Length == 0 ? "" : " and ") + $"(id_ObjectLease = {cmbObject.SelectedValue.ToString()} or id = 0)";
            if ((int)cmbFloor.SelectedValue != 0)
                filter += (filter.Length == 0 ? "" : " and ") + $"(id_Floor = {cmbFloor.SelectedValue.ToString()} or id = 0)";
            if ((int)cmbBuilding.SelectedValue != 0)
                filter += (filter.Length == 0 ? "" : " and ") + $"(id_Building = {cmbBuilding.SelectedValue.ToString()} or id = 0)";
            dtSections.DefaultView.RowFilter = filter;
        }

        private void setEnabledComboBoxs()
        {

        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            cmb_init();
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
            this.Close();
        }

        private void btnSection_Click(object sender, EventArgs e)
        {
            PlanReport plan = new PlanReport((int) cmbObject.SelectedValue, (int) cmbBuilding.SelectedValue, (int) cmbFloor.SelectedValue, (int) cmbSection.SelectedValue, 0);
            if (plan.dtReport==null || plan.dtReport.Rows.Count==0)
            {
                MessageBox.Show("Нет данных для отчета", "Отчет по секциям", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            plan.createReportSection();
        }

        private void btnTenant_Click(object sender, EventArgs e)
        {
            PlanReport plan = new PlanReport(0,0,0,0,id_tenant);
            if (plan.dtReport == null || plan.dtReport.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для отчета", "Отчет по арендаторам", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            plan.createReportTenant();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmTenants frm = new frmTenants();
            frm.ShowDialog();
            id_tenant = frm.id_tenant;
            tbTenant.Text = frm.name_tenant;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            id_tenant = 0;
            tbTenant.Text = "";
        }

        private void btnUpdatePlan_Click(object sender, EventArgs e)
        {
            Task.Run(() => get_data());
        }
    }
}
