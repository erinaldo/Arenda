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

namespace Arenda.AddNewDocToFolder
{
    public partial class frmSelectAgreementsTo1C : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(),
             ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        private DataTable dtLandLord,dtData;
        public frmSelectAgreementsTo1C()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
        }

        private void frmSelectAgreementsTo1C_Load(object sender, EventArgs e)
        {
            DataTable dtObjectLease = _proc.getObjectLease(true);

            cmbObject.DisplayMember = "cName";
            cmbObject.ValueMember = "id";
            cmbObject.DataSource = dtObjectLease;

            DataTable dtTypeDoc = _proc.getTypeContract(true);

            cmbTypeDoc.DisplayMember = "cName";
            cmbTypeDoc.ValueMember = "id";
            cmbTypeDoc.DataSource = dtTypeDoc;

            dtLandLord = _proc.GetListLandlord(true);
            GetListTenantFilter();
            GetData();
        }

        private void dgvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {

        }

        private void dgvData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

        }

        private void dgvData_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            int width = 0;
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                if (!col.Visible) continue;
                if (col.Index == nameTenant.Index)
                {
                    tbTenant.Location = new Point(dgvData.Location.X + width + 1, tbTenant.Location.Y);
                    tbTenant.Size = new Size(nameTenant.Width, tbTenant.Height);
                }

                if (col.Index == cAgreements.Index)
                {
                    tbAgreements.Location = new Point(dgvData.Location.X + width + 1, tbTenant.Location.Y);
                    tbAgreements.Size = new Size(cAgreements.Width, tbTenant.Height);
                }

                if (col.Index == cPlace.Index)
                {
                    tbPlace.Location = new Point(dgvData.Location.X + width + 1, tbTenant.Location.Y);
                    tbPlace.Size = new Size(cPlace.Width, tbTenant.Height);
                }


                //if (col.Index == cNameBuild.Index)
                //{
                //    tbBuild.Location = new Point(dgvData.Location.X + width + 1, tbTenant.Location.Y);
                //    tbBuild.Size = new Size(cNameBuild.Width, tbTenant.Height);
                //}

                //if (col.Index == cFloor.Index)
                //{
                //    tbFloor.Location = new Point(dgvData.Location.X + width + 1, tbTenant.Location.Y);
                //    tbFloor.Size = new Size(cFloor.Width, tbTenant.Height);
                //}

                //if (col.Index == cSection.Index)
                //{
                //    tbSection.Location = new Point(dgvData.Location.X + width + 1, tbTenant.Location.Y);
                //    tbSection.Size = new Size(cSection.Width, tbTenant.Height);
                //}

                width += col.Width;
            }
        }

        private void cmbObject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetListTenantFilter();
            GetData();
        }

        private void GetListTenantFilter()
        {
            if(dtLandLord==null || dtLandLord.Rows.Count == 0) { cmbLandLord.DataSource = null;return; }

            dtLandLord.DefaultView.RowFilter = "";
            if ((int)cmbObject.SelectedValue != 0)
            {
                dtLandLord.DefaultView.RowFilter = $"id=0 or id_ObjectLease = {cmbObject.SelectedValue}";
            }
            

            cmbLandLord.DisplayMember = "cName";
            cmbLandLord.ValueMember = "id";
            cmbLandLord.DataSource = dtLandLord.DefaultView;
        }

        private void tbTenant_TextChanged(object sender, EventArgs e)
        {

        }

        private void GetData()
        {
            int id_Object = (int)cmbObject.SelectedValue;
            dtData = _proc.GetListAgreementTo1C(id_Object);
            dgvData.DataSource = dtData;
        }
    }
}
