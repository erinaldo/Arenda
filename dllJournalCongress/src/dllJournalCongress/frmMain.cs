using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.User;


namespace dllJournalCongress
{
    public partial class frmMain : Form
    {
        private DataTable dtData;
        private bool isChangeValue = false;
        private DateTime _dateStart, _dateEnd;
        public frmMain()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
            ToolTip tp = new ToolTip();
            tp.SetToolTip(btExit,"Выход");
            tp.SetToolTip(btPrint, "Печать");
            tp.SetToolTip(btUpdate, "Обновить");
            tp.SetToolTip(btAcceptD, "Подтвердить");
        }

        private void frmMain_Load(object sender, EventArgs e)
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

        private void btUpdate_Click(object sender, EventArgs e)
        {
            getData();
        }

        private void getData()
        {
            Task<DataTable> task = Config.hCntMain.getJournalCongress(dtpStart.Value.Date, dtpEnd.Value.Date);
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

                if (tbLandLord.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"nameLandLord like '%{tbLandLord.Text.Trim()}%'";

                if (tbTenant.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"nameTenant like '%{tbTenant.Text.Trim()}%'";

                if (tbAgreement.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"Agreement like '%{tbAgreement.Text.Trim()}%'";

                if (tbNamePlace.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"namePlace like '%{tbNamePlace.Text.Trim()}%'";

                if ((int)cmbObject.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_ObjectLease  = {cmbObject.SelectedValue}";


                //string strFilter = "("
                //    + "((isLinkPetitionLeave = 0 AND isConfirmed = 0) " +
                //    "OR (isLinkPetitionLeave = 1 AND isConfirmed_LinkPetitionLeave = 0) " +
                //    "OR (isCancelAgreements is null AND isConfirmed = 1 AND isConfirmed_LinkPetitionLeave = 0))";
                string strFilter = "(" + "((isConfirmed = 0) OR (isConfirmed = 1 and isLinkPetitionLeave = 1 and isConfirmed_LinkPetitionLeave = 0))";

                if (chbCongressAccept.Checked) {
                    strFilter += $" OR (isLinkPetitionLeave = 0 AND isConfirmed = 1 AND isCancelAgreements is null)";
                }
                if (chbDropAgreements.Checked) {
                    strFilter += $" OR ((isLinkPetitionLeave = 1 AND isConfirmed_LinkPetitionLeave = 1) OR (isCancelAgreements is not null AND isConfirmed = 1 ))";
                }
                strFilter += ")";

                filter += (filter.Length == 0 ? "" : " and ") + strFilter;

                dtData.DefaultView.RowFilter = filter;
                dtData.DefaultView.Sort = "nameLandLord asc, nameTenant asc, nameObject asc";
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

        private void dgvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {

                Color rColor = Color.White;
                if ((!(bool)dtData.DefaultView[e.RowIndex]["isLinkPetitionLeave"] || !(bool)dtData.DefaultView[e.RowIndex]["isConfirmed_LinkPetitionLeave"]) && (bool)dtData.DefaultView[e.RowIndex]["isConfirmed"])
                    rColor = panel2.BackColor;

                if ((bool)dtData.DefaultView[e.RowIndex]["isLinkPetitionLeave"] && (bool)dtData.DefaultView[e.RowIndex]["isConfirmed_LinkPetitionLeave"])
                    rColor = panel3.BackColor;
                else if (dtData.DefaultView[e.RowIndex]["isCancelAgreements"]!=DBNull.Value && (bool)dtData.DefaultView[e.RowIndex]["isConfirmed"])
                    rColor = panel3.BackColor;

                dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;

                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;

                if ((bool)dtData.DefaultView[e.RowIndex]["isLinkPetitionLeave"])
                    dgvData.Rows[e.RowIndex].Cells[Date_of_Departure.Index].Style.BackColor =
                         dgvData.Rows[e.RowIndex].Cells[Date_of_Departure.Index].Style.SelectionBackColor = panel1.BackColor;
            }
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStart.Value.Date > dtpEnd.Value.Date)
                dtpEnd.Value = dtpStart.Value.Date;

            isChangeValue = _dateStart.Date!=dtpStart.Value.Date;
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

        private void cmbObject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void dgvData_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            tbLandLord.Location = new Point(dgvData.Location.X, tbLandLord.Location.Y);
            tbLandLord.Size = new Size(nameLandLord.Width, tbLandLord.Height);
            
            tbTenant.Location = new Point(dgvData.Location.X+ nameLandLord.Width+1, tbLandLord.Location.Y);
            tbTenant.Size = new Size(nameTenant.Width, tbLandLord.Height);

            tbAgreement.Location = new Point(dgvData.Location.X + nameLandLord.Width + nameTenant.Width + nameObject.Width + 1, tbLandLord.Location.Y);
            tbAgreement.Size = new Size(Agreement.Width, tbLandLord.Height);

            tbNamePlace.Location = new Point(dgvData.Location.X + nameLandLord.Width + nameTenant.Width + nameObject.Width + Agreement.Width + 1, tbLandLord.Location.Y);
            tbNamePlace.Size = new Size(namePlace.Width, tbLandLord.Height);
        }

        private void chbDropAgreements_CheckedChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void tbLandLord_TextChanged(object sender, EventArgs e)
        {
            setFilter();
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

            btAcceptD.Enabled = !(bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isConfirmed"] 
                || (!(bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isConfirmed_LinkPetitionLeave"] && (bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isLinkPetitionLeave"]);

            new ToolTip().SetToolTip(btAcceptD, "Подтвердить съезд");
            if((!(bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isConfirmed_LinkPetitionLeave"] && (bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isLinkPetitionLeave"]))
                new ToolTip().SetToolTip(btAcceptD, "Подтвердить аннуляцию съезда");


        }

        private void btAcceptD_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                int id = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];
                bool isLinkPetitionLeave = (bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isLinkPetitionLeave"];
                bool isConfirmed = (bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isConfirmed"];


                if (isLinkPetitionLeave)
                {
                    id = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_LinkPetitionLeave"];
                    isConfirmed = (bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isConfirmed_LinkPetitionLeave"];
                }

                if (!isConfirmed)
                {
                    if (isLinkPetitionLeave)
                    {
                        if (DialogResult.No == MessageBox.Show(Config.centralText("Вы действительно хотите\nподтвердить аннуляцию\nзаявления на съезд?\n"), "Подтверждение аннуляции съезда", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                            return;
                    }
                    else
                    {
                        if (DialogResult.No == MessageBox.Show(Config.centralText("Вы действительно хотите\nподтвердить заявление на съезд?\n"), "Подтверждение съезда", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                            return;
                    }


                    Task<DataTable> task = Config.hCntMain.setJournalCongress(id);

                    if (task.Result == null)
                    {
                        MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int result = (int)task.Result.Rows[0]["id"];

                    if (result != 0)
                    {
                        MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (isLinkPetitionLeave)
                    {
                        Logging.StartFirstLevel(1568);
                        Logging.Comment($"ID: {id}");
                        foreach (DataGridViewColumn col in dgvData.Columns)
                        {
                            if (col.Visible)
                                Logging.Comment($"{col.HeaderText}: {dgvData.CurrentRow.Cells[col.Name].Value.ToString()}");
                        }
                        Logging.StopFirstLevel();
                    }
                    else
                    {
                        Logging.StartFirstLevel(1567);
                        Logging.Comment($"ID: {id}");
                        foreach (DataGridViewColumn col in dgvData.Columns)
                        {
                            if (col.Visible)
                                Logging.Comment($"{col.HeaderText}: {dgvData.CurrentRow.Cells[col.Name].Value.ToString()}");
                        }
                        Logging.StopFirstLevel();
                    }

                    getData();
                }
            }
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для формирования отчёта!","Выгрузка отчёта",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();
            int indexRow = 1;
            int maxColumns = 0;

            foreach (DataGridViewColumn col in dgvData.Columns)
                if (col.Visible) maxColumns++;

            setWidthColumn(indexRow, 1, 25, report);
            setWidthColumn(indexRow, 2, 23, report);
            setWidthColumn(indexRow, 3, 14, report);
            setWidthColumn(indexRow, 4, 11, report);
            setWidthColumn(indexRow, 5, 50, report);
            setWidthColumn(indexRow, 6, 16, report);
            setWidthColumn(indexRow, 7, 13, report);
            setWidthColumn(indexRow, 8, 15, report);
            setWidthColumn(indexRow, 9, 13, report);
            

            #region "Head"
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue("Отчёт по съездам арендаторов", indexRow, 1);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            indexRow++;
            indexRow++;


            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Период с {dtpStart.Value.ToShortDateString()} по {dtpEnd.Value.ToShortDateString()}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Объект аренды: {cmbObject.Text}", indexRow, 1);
            indexRow++;

            if (tbLandLord.Text.Trim().Length > 0 || tbTenant.Text.Trim().Length > 0 || tbAgreement.Text.Trim().Length > 0 || tbNamePlace.Text.Trim().Length > 0)
            {
                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue($"Фильтры: " +
                    $"{(tbLandLord.Text.Trim().Length > 0 ? $"Арендодатель: {tbLandLord.Text.Trim()}" : "")}; " +
                    $"{(tbLandLord.Text.Trim().Length > 0 ? $"Арендатор: {tbTenant.Text.Trim()}" : "")}; " +
                    $"{(tbLandLord.Text.Trim().Length > 0 ? $"Номер договора: {tbAgreement.Text.Trim()}" : "")}; " +
                    $"{(tbLandLord.Text.Trim().Length > 0 ? $"Местоположение места аренды: {tbNamePlace.Text.Trim()}" : "")}; ", indexRow, 1);
                indexRow++;
            }

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue("Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername, indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue("Дата выгрузки: " + DateTime.Now.ToString(), indexRow, 1);
            indexRow++;
            indexRow++;
            #endregion

            int indexColumn = 0;
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                if (col.Visible) { indexColumn++;
                    report.AddSingleValue(col.HeaderText, indexRow, indexColumn);
                    report.SetWrapText(indexRow, indexColumn, indexRow, indexColumn);
                }
            }
            report.SetFontBold(indexRow, 1, indexRow, maxColumns);
            report.SetBorders(indexRow, 1, indexRow, maxColumns);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
            indexRow++;


            foreach (DataGridViewRow row in dgvData.Rows)
            {
                Color rColor = Color.White;
                if ((!(bool)dtData.DefaultView[row.Index]["isLinkPetitionLeave"] || !(bool)dtData.DefaultView[row.Index]["isConfirmed_LinkPetitionLeave"]) && (bool)dtData.DefaultView[row.Index]["isConfirmed"])
                    rColor = panel2.BackColor;

                if ((bool)dtData.DefaultView[row.Index]["isLinkPetitionLeave"] && (bool)dtData.DefaultView[row.Index]["isConfirmed_LinkPetitionLeave"])
                    rColor = panel3.BackColor;
                else if (dtData.DefaultView[row.Index]["isCancelAgreements"] != DBNull.Value && (bool)dtData.DefaultView[row.Index]["isConfirmed"])
                    rColor = panel3.BackColor;

                report.SetCellColor(indexRow, 1, indexRow, maxColumns, rColor);

                indexColumn = 0;
                foreach (DataGridViewColumn col in dgvData.Columns)
                {                    
                    if (col.Visible)
                    {
                        indexColumn++;
                        if (row.Cells[col.Index].Value is DateTime)
                            report.AddSingleValue(((DateTime)row.Cells[col.Index].Value).ToShortDateString(), indexRow, indexColumn);
                        else
                            report.AddSingleValue(row.Cells[col.Index].Value.ToString(), indexRow, indexColumn);
                        report.SetWrapText(indexRow, indexColumn, indexRow, indexColumn);
                        if ((bool)dtData.DefaultView[row.Index]["isLinkPetitionLeave"] && col.Index == Date_of_Departure.Index)
                            report.SetCellColor(indexRow, indexColumn, indexRow, indexColumn, panel1.BackColor);
                    }
                }
               
                report.SetBorders(indexRow, 1, indexRow, maxColumns);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                indexRow++;
            }


            indexRow++;
            report.Merge(indexRow, 2, indexRow, maxColumns);
            report.SetCellColor(indexRow, 1, indexRow, 1, panel1.BackColor);
            report.AddSingleValue($"{label4.Text}", indexRow, 2);
            indexRow++;

            report.Merge(indexRow, 2, indexRow, maxColumns);
            report.SetCellColor(indexRow, 1, indexRow, 1, panel2.BackColor);
            report.AddSingleValue($"{chbCongressAccept.Text}", indexRow, 2);
            indexRow++;

            report.Merge(indexRow, 2, indexRow, maxColumns);
            report.SetCellColor(indexRow, 1, indexRow, 1, panel3.BackColor);
            report.AddSingleValue($"{chbDropAgreements.Text}", indexRow, 2);
            report.SetPageSetup(1, 9999, true);

            report.Show();
        }

        private void setWidthColumn(int indexRow, int indexCol, int width, Nwuram.Framework.ToExcelNew.ExcelUnLoad report)
        {
            report.SetColumnWidth(indexRow, indexCol, indexRow, indexCol, width);
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dtpStart_Leave(object sender, EventArgs e)
        {
            if (isChangeValue)
                getData();
        }
    }
}
