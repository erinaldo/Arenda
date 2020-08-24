using Nwuram.Framework.Settings.Connection;
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
            if (Config.hCntMain == null)
                Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            dgvData.AutoGenerateColumns = false;
            ToolTip tp = new ToolTip();
            tp.SetToolTip(btExit, "Выход");
            tp.SetToolTip(btPrint, "Печать");
            tp.SetToolTip(btUpdate, "Обновить");
            tp.SetToolTip(btAcceptD, "Подтвердить");

            tp.SetToolTip(btAdd, "Добавить");
            tp.SetToolTip(btEdit, "Редактировать");
            tp.SetToolTip(btDel, "Удалить");
        }

        private void frmReportMonth_Load(object sender, EventArgs e)
        {
            dtpStart.Value = DateTime.Now.AddMonths(-1);
            dtpEnd.Value = DateTime.Now.AddMonths(2);

            btAcceptD.Visible = new List<string> { "СБ6" }.Contains(UserSettings.User.StatusCode);
            btAdd.Visible = btDel.Visible = btEdit.Visible = new List<string> { "РКВ" }.Contains(UserSettings.User.StatusCode);

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
              
                if ((int)cmbObject.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_ObjectLease  = {cmbObject.SelectedValue}";

                if (!chbCongressAccept.Checked)
                    filter += (filter.Length == 0 ? "" : " and ") + $"isСonfirmed = 0";

                dtData.DefaultView.RowFilter = filter;             
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally
            {
                dgvData_SelectionChanged(null, null);
            }
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow == null || dgvData.CurrentRow.Index == -1 || dtData == null || dtData.DefaultView.Count == 0 || dgvData.CurrentRow.Index >= dtData.DefaultView.Count)
            {
                btPrint.Enabled = false;
                btAcceptD.Enabled = false;
                btEdit.Enabled = btDel.Enabled = false;
                tbDateEdit.Clear();
                tbFioEdit.Clear();
                return;
            }

            btPrint.Enabled = true;

            btAcceptD.Enabled = !(bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isСonfirmed"];
            btEdit.Enabled = btDel.Enabled = !(bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isСonfirmed"];
            tbFioEdit.Text = (string)dtData.DefaultView[dgvData.CurrentRow.Index]["FIO"];
            tbDateEdit.Text = ((DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["DateEdit"]).ToString();
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
            if (DialogResult.OK == new frmAddReportMonth().ShowDialog()) getData();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                int id = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];
                DataRowView row = dtData.DefaultView[dgvData.CurrentRow.Index];

                if (DialogResult.OK == new frmAddReportMonth() { id = id, row = row }.ShowDialog())
                    getData();             
            }
        }

        private void dgvData_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks > 1)
            {
                if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
                {
                    int id = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];
                    DataRowView row = dtData.DefaultView[dgvData.CurrentRow.Index];

                    frmAddReportMonth fARM = new frmAddReportMonth() { id = id, row = row, isView = true };
                    fARM.ShowDialog();
                    if (fARM.isAcceptData) getData();
                }
            }
        }

        private void btAcceptD_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                if (DialogResult.No == MessageBox.Show(Config.centralText("Вы хотите подтвердить\nежемесячный план?\n"), "Подтверждение плана", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) return;

                int id = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];
                DateTime _tmpDate = (DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["PeriodMonthPlan"];
                int _tmpObjectLease = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_ObjectLease"];

                Task<DataTable> task = Config.hCntMain.setTMonthPlan(id, _tmpDate, _tmpObjectLease, true, false, 0);
                task.Wait();

                DataTable dtResult = task.Result;

                if (dtResult == null || dtResult.Rows.Count == 0)
                {
                    MessageBox.Show("Не удалось сохранить данные", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if ((int)dtResult.Rows[0]["id"] == -9999)
                {
                    MessageBox.Show("Ошибка выполнения процедуры", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("План подтвержден!", "Подтверждение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getData();
            }
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                int id = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];
                DateTime _tmpDate = (DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["PeriodMonthPlan"];
                int _tmpObjectLease = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_ObjectLease"];
                string _nameObject  = (string)dtData.DefaultView[dgvData.CurrentRow.Index]["nameObject"];
                string status = (bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isСonfirmed"] ? "Подтверждена" : "Не подтверждена";
                reports.createReport(id, _tmpDate, _tmpObjectLease, _nameObject, status);
            }
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                int id = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];
                DateTime _tmpDate = (DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["PeriodMonthPlan"];
                int _tmpObjectLease = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_ObjectLease"];

                Task<DataTable> task = Config.hCntMain.setTMonthPlan(id, _tmpDate, _tmpObjectLease, true, true, 0);
                task.Wait();

                DataTable dtResult = task.Result;
                int result = (int)task.Result.Rows[0]["id"];


                if (dtResult == null || dtResult.Rows.Count == 0)
                {
                    MessageBox.Show("Не удалось сохранить данные", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (result == -9999)
                {
                    MessageBox.Show("Ошибка выполнения процедуры", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (result == -1)
                {
                    MessageBox.Show(Config.centralText("План удалён другим пользователем\n"), "Удаление записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getData();
                    return;
                }

                if (result == -2)
                {
                    MessageBox.Show(Config.centralText("План подтверждён!\nУдаление невозможно\n"), "Удаление записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getData();
                    return;
                }

                if (result == 0 )
                {
                    if (DialogResult.Yes == MessageBox.Show("Удалить выбранную запись?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        //setLog(id, 2);
                        task = Config.hCntMain.setTMonthPlan(id, _tmpDate, _tmpObjectLease, true, true, 1);
                        task.Wait();
                        if (task.Result == null)
                        {
                            MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        getData();
                        return;
                    }
                }
               
            }
        }

        private void dgvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                Color rColor = Color.White;
                if ((bool)dtData.DefaultView[e.RowIndex]["isСonfirmed"])
                    rColor = panel2.BackColor;

                dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;

                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
            }
        }

    }
}
