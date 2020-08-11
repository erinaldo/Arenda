using Nwuram.Framework.Settings.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dllJournalReport
{
    public partial class frmReportMonth : Form
    {
        private DataTable dtData;
        private bool isChangeValue = false;
        private DateTime _dateStart, _dateEnd;

        public frmReportMonth()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
            ToolTip tp = new ToolTip();
            tp.SetToolTip(btExit, "Выход");
            tp.SetToolTip(btPrint, "Печать");
            tp.SetToolTip(btUpdate, "Обновить");
            tp.SetToolTip(btAcceptD, "Подтвердить");
        }

        private void frmReportMonth_Load(object sender, EventArgs e)
        {
            dtpStart.Value = DateTime.Now.AddMonths(-1);
            dtpEnd.Value = DateTime.Now.AddMonths(2);

            btAcceptD.Visible = new List<string> { "Д" }.Contains(UserSettings.User.StatusCode);

            Task<DataTable> task = Config.hCntMain.getObjectLease(true);
            task.Wait();
            DataTable dtObjectLease = task.Result;

            cmbObject.DisplayMember = "cName";
            cmbObject.ValueMember = "id";
            cmbObject.DataSource = dtObjectLease;

            _dateStart = dtpStart.Value.Date;
            _dateEnd = dtpEnd.Value.Date;

            isChangeValue = false;
            getData();
        }

        private void getData()
        {
            DateTime _startDate = new DateTime(dtpStart.Value.Year, dtpStart.Value.Month, 1);
            DateTime _endDate = new DateTime(dtpEnd.Value.Year, dtpEnd.Value.Month, 1);

            Task<DataTable> task = Config.hCntMain.getTMonthReport(_startDate.Date, _endDate.Date);
            task.Wait();
            dtData = task.Result.Copy();
            task = null;

            setFilter();
            dgvData.DataSource = dtData;
            isChangeValue = false;
        }

        private void setFilter()
        {
            if (dtData == null || dtData.Rows.Count == 0)
            {
                //btEdit.Enabled = btDelete.Enabled = false;                
                return;
            }

            try
            {
                string filter = "";

                //if (tbLandLord.Text.Trim().Length != 0)
                //    filter += (filter.Length == 0 ? "" : " and ") + $"nameLandLord like '%{tbLandLord.Text.Trim()}%'";

                //if (tbTenant.Text.Trim().Length != 0)
                //    filter += (filter.Length == 0 ? "" : " and ") + $"nameTenant like '%{tbTenant.Text.Trim()}%'";

                //if (tbAgreement.Text.Trim().Length != 0)
                //    filter += (filter.Length == 0 ? "" : " and ") + $"Agreement like '%{tbAgreement.Text.Trim()}%'";

                //if (tbNamePlace.Text.Trim().Length != 0)
                //    filter += (filter.Length == 0 ? "" : " and ") + $"namePlace like '%{tbNamePlace.Text.Trim()}%'";

                if ((int)cmbObject.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_ObjectLease  = {cmbObject.SelectedValue}";


                ////string strFilter = "("
                ////    + "((isLinkPetitionLeave = 0 AND isConfirmed = 0) " +
                ////    "OR (isLinkPetitionLeave = 1 AND isConfirmed_LinkPetitionLeave = 0) " +
                ////    "OR (isCancelAgreements is null AND isConfirmed = 1 AND isConfirmed_LinkPetitionLeave = 0))";
                //string strFilter = "(" + "((isConfirmed = 0) OR (isConfirmed = 1 and isLinkPetitionLeave = 1 and isConfirmed_LinkPetitionLeave = 0))";

                //if (chbCongressAccept.Checked)
                //{
                //    strFilter += $" OR (isLinkPetitionLeave = 0 AND isConfirmed = 1 AND isCancelAgreements is null)";
                //}
                //if (chbDropAgreements.Checked)
                //{
                //    strFilter += $" OR ((isLinkPetitionLeave = 1 AND isConfirmed_LinkPetitionLeave = 1) OR (isCancelAgreements is not null AND isConfirmed = 1 ))";
                //}
                //strFilter += ")";

                //filter += (filter.Length == 0 ? "" : " and ") + strFilter;

                dtData.DefaultView.RowFilter = filter;
                //dtData.DefaultView.Sort = "nameLandLord asc, nameTenant asc, nameObject asc";
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally
            {
                //btEdit.Enabled = btDelete.Enabled =
                //dtData.DefaultView.Count != 0;
                dgvData_SelectionChanged(null, null);
            }
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow == null || dgvData.CurrentRow.Index == -1 || dtData == null || dtData.DefaultView.Count == 0 || dgvData.CurrentRow.Index >= dtData.DefaultView.Count)
            {
                btPrint.Enabled = false;
                btAcceptD.Enabled = false;
                return;
            }

            btPrint.Enabled = true;

            //btAcceptD.Enabled = !(bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isConfirmed"]
             //   || (!(bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isConfirmed_LinkPetitionLeave"] && (bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isLinkPetitionLeave"]);

            //new ToolTip().SetToolTip(btAcceptD, "Подтвердить съезд");
            //if ((!(bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isConfirmed_LinkPetitionLeave"] && (bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isLinkPetitionLeave"]))
            //    new ToolTip().SetToolTip(btAcceptD, "Подтвердить аннуляцию съезда");


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

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStart.Value.Date > dtpEnd.Value.Date)
                dtpEnd.Value = dtpStart.Value.Date;

            isChangeValue = _dateStart.Date != dtpStart.Value.Date;
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStart.Value.Date > dtpEnd.Value.Date)
                dtpStart.Value = dtpEnd.Value.Date;

            isChangeValue = _dateEnd.Date != dtpEnd.Value.Date;
        }

        private void dtpStart_CloseUp(object sender, EventArgs e)
        {
            if (isChangeValue)
                getData();
        }

        private void dtpStart_Leave(object sender, EventArgs e)
        {
            if (isChangeValue)
                getData();
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            getData();
        }

        private void cmbObject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void chbCongressAccept_Click(object sender, EventArgs e)
        {
            setFilter();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            new frmAddReportMonth().ShowDialog();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                int id = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];
                if (DialogResult.OK == new frmAddReportMonth() { id = id }.ShowDialog())
                    getData();
                    //get_data();
            }
        }

        private void dgvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                Color rColor = Color.White;
                //if ((!(bool)dtData.DefaultView[e.RowIndex]["isLinkPetitionLeave"] || !(bool)dtData.DefaultView[e.RowIndex]["isConfirmed_LinkPetitionLeave"]) && (bool)dtData.DefaultView[e.RowIndex]["isConfirmed"])
                    //rColor = panel2.BackColor;

                dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;

                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
            }
        }

    }
}
