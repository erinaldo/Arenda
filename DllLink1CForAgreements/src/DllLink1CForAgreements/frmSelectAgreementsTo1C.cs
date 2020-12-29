using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DllLink1CForAgreements
{
    public partial class frmSelectAgreementsTo1C : Form
    {
        public int IdAgreement { private set; get; }
        public string Agreement { private set; get; }
        

        private DataTable dtLandLord, dtData;
        public frmSelectAgreementsTo1C()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
        }

        private void frmSelectAgreementsTo1C_Load(object sender, EventArgs e)
        {
            DataTable dtObjectLease = Config.hCntMain.getObjectLease(true);

            cmbObject.DisplayMember = "cName";
            cmbObject.ValueMember = "id";
            cmbObject.DataSource = dtObjectLease;

            DataTable dtTypeDoc = Config.hCntMain.getTypeContract(true);

            cmbTypeDoc.DisplayMember = "cName";
            cmbTypeDoc.ValueMember = "id";
            cmbTypeDoc.DataSource = dtTypeDoc;

            dtLandLord = Config.hCntMain.GetListLandlord(true);
            GetListTenantFilter();
            GetData();
        }

        private void dgvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            Color rColor = Color.White;
            //if (!(bool)dtData.DefaultView[e.RowIndex]["isActive"])
            //  rColor = panel1.BackColor;
            dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
            dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
            dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void dgvData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            //Рисуем рамку для выделеной строки
            if (dgv.Rows[e.RowIndex].Selected)
            {
                int width = dgv.Width;
                Rectangle r = dgv.GetRowDisplayRectangle(e.RowIndex, false);
                Rectangle rect = new Rectangle(r.X, r.Y, width - 1, r.Height - 1);

                ControlPaint.DrawBorder(e.Graphics, rect,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid);
            }
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
            if (dtLandLord == null || dtLandLord.Rows.Count == 0) { cmbLandLord.DataSource = null; return; }

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
            setFilter();
        }

        private void setFilter()
        {
            if (dtData == null || dtData.Rows.Count == 0)
            {
                btSave.Enabled = false;
                return;
            }

            try
            {
                string filter = "";

                if (tbTenant.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"nameTenant like '%{tbTenant.Text.Trim()}%'";

                if (tbAgreements.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"Agreement like '%{tbAgreements.Text.Trim()}%'";

                if (tbPlace.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"namePlace like '%{tbPlace.Text.Trim()}%'";

                if ((int)cmbLandLord.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_Landlord = {cmbLandLord.SelectedValue}";

                if ((int)cmbTypeDoc.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_TypeContract = {cmbTypeDoc.SelectedValue}";

                dtData.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally
            {
                btSave.Enabled = dtData.DefaultView.Count != 0;                
            }
        }

        private void cmbLandLord_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            selectAgreement();
        }

        private void selectAgreement()
        {
            IdAgreement = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];
            Agreement = (string)dtData.DefaultView[dgvData.CurrentRow.Index]["Agreement"];
            DialogResult = DialogResult.OK;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            selectAgreement();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void GetData()
        {
            int id_Object = (int)cmbObject.SelectedValue;
            dtData = Config.hCntMain.GetListAgreementTo1C(id_Object);
            setFilter();
            dgvData.DataSource = dtData;
        }
    }
}
