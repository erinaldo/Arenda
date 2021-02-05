using Nwuram.Framework.Logging;
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

namespace dllArendaJournalAccrualsPenalties
{
    public partial class frmMain : Form
    {
        private DataTable dtData;
        private bool isChangeValue = false;

        public frmMain()
        {
            InitializeComponent();

            if(Config.hCntMain == null)
                Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            dgvData.AutoGenerateColumns = false;

            ToolTip tp = new ToolTip();
            tp.SetToolTip(btExit,"Выход");
            tp.SetToolTip(btPrint, "Печать");
            tp.SetToolTip(btUpdate, "Обновить");
            tp.SetToolTip(btAcceptD, "Подтвердить пени");


            //dgvData.AllowUserToAddRows = false;
            //dgvData.AllowUserToResizeRows = false;
            //dgvData.RowHeadersVisible = false;

            //this.dgvData.CellPainting -= new DataGridViewCellPaintingEventHandler(dgvData_CellPainting);
            //this.dgvData.CellFormatting -= new DataGridViewCellFormattingEventHandler(dgvData_CellFormatting);
            //this.dgvData.Paint += new PaintEventHandler(dataGrid_Paint);

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Task<DataTable> task = Config.hCntMain.getObjectLease(true);
            task.Wait();
            DataTable dtObjectLease = task.Result;

            cmbObject.DisplayMember = "cName";
            cmbObject.ValueMember = "id";
            cmbObject.DataSource = dtObjectLease;

            task = Config.hCntMain.getTypeContract(true);
            task.Wait();
            DataTable dtTypeContract = task.Result;

            cmbTypeContract.DisplayMember = "cName";
            cmbTypeContract.ValueMember = "id";
            cmbTypeContract.DataSource = dtTypeContract;

            task = Config.hCntMain.getListPeriodCredit(false);
            task.Wait();
            DataTable dtPeriodCredit = task.Result;

            cmbPeriodCredit.DisplayMember = "PeriodCredit";
            cmbPeriodCredit.ValueMember = "PeriodCredit";
            cmbPeriodCredit.DataSource = dtPeriodCredit;

            cSummaPenalty.ReadOnly = !new List<string> { "Д" }.Contains(UserSettings.User.StatusCode);
            cPrcPenalty.ReadOnly = !new List<string> { "Д" }.Contains(UserSettings.User.StatusCode);
            btAcceptD.Visible = new List<string> { "Д" }.Contains(UserSettings.User.StatusCode);

            getData();
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
                    tbAgreement.Location = new Point(dgvData.Location.X + width + 1, tbTenant.Location.Y);
                    tbAgreement.Size = new Size(cAgreements.Width, tbTenant.Height);
                }

                if (col.Index == cItogPenalty.Index)
                {
                    tbItogPenalty.Location = new Point(dgvData.Location.X + width + 1, tbItogPenalty.Location.Y);
                    tbItogPenalty.Size = new Size(cItogPenalty.Width, tbItogPenalty.Height);
                    lItogoPenalty.Location = new Point(dgvData.Location.X + width - 45, lItogoPenalty.Location.Y);
                }
                width += col.Width;
            }
        }

        private void getData()
        {
            if (cmbPeriodCredit.SelectedValue == null) { dgvData.DataSource = null; return; }

            Task<DataTable> task = Config.hCntMain.getPenalty((string)cmbPeriodCredit.SelectedValue);
            task.Wait();
            dtData = task.Result.Copy();
            task = null;

            if (dtData != null && dtData.Rows.Count > 0)
            {
                var groupSumPenalty = dtData.AsEnumerable()
                    .GroupBy(r => new { id = r.Field<int>("id") })
                    .Select(s => new
                    {
                        s.Key.id,
                        sumItogPenalty = s.Sum(r => r.Field<decimal>("SummaPenalty"))
                    });

                dtData.Columns.Add("sumItogPenalty", typeof(decimal));

                foreach (var gSum in groupSumPenalty)
                {
                    EnumerableRowCollection<DataRow> rowCollect = dtData.AsEnumerable().Where(r => r.Field<int>("id") == gSum.id);
                    foreach (DataRow row in rowCollect)
                    {
                        row["sumItogPenalty"] = gSum.sumItogPenalty;
                    }
                }
            }

            setFilter();
            dgvData.DataSource = dtData;
            isChangeValue = false;
        }

        private void setFilter()
        {
            if (dtData == null || dtData.Rows.Count == 0)
            {
                //btEdit.Enabled = btDelete.Enabled = false;             
                btAcceptD.Enabled = btPrint.Enabled = false;
                tbItogPenalty.Text = "0.00";
                return;
            }

            try
            {
                string filter = "";


                if (tbTenant.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"nameTenant like '%{tbTenant.Text.Trim()}%'";

                if (tbAgreement.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"Agreement like '%{tbAgreement.Text.Trim()}%'";

                if ((int)cmbObject.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_ObjectLease  = {cmbObject.SelectedValue}";

                if ((int)cmbTypeContract.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_TypeContract  = {cmbTypeContract.SelectedValue}";

                if (!chbCongressAccept.Checked)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_StatusPenalty  <> 3";


                dtData.DefaultView.RowFilter = filter;
                dtData.DefaultView.Sort = "nameTenant asc";
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally
            {
                object objItogSum = dtData.DefaultView.ToTable().Compute("SUM(SummaPenalty)", "");
                if (objItogSum != null && objItogSum != DBNull.Value)
                    tbItogPenalty.Text = ((decimal)objItogSum).ToString("0.00");
                else
                    tbItogPenalty.Text = "0.00";


                btPrint.Enabled =
                 dtData.DefaultView.Count != 0;
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

            btAcceptD.Enabled = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_StatusPenalty"] != 3;
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
                if ((int)dtData.DefaultView[e.RowIndex]["id_StatusPenalty"] == 3)
                    rColor = panel2.BackColor;

                dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;               
            }
        }

        #region "Объединение"
        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            if (e.RowIndex < 1 || e.ColumnIndex < 0)
                return;

            if (!new List<int>() { nameTenant.Index, cTypeContract.Index, cAgreements.Index, cPeriodCredit.Index, cItogPenalty.Index }.Contains(e.ColumnIndex))
            {
                e.AdvancedBorderStyle.Top = dgvData.AdvancedCellBorderStyle.Top;
                //e.AdvancedBorderStyle.Bottom = dgvData.AdvancedCellBorderStyle.Bottom;
                if (e.RowIndex == dgvData.Rows.Count - 1)
                {
                    e.AdvancedBorderStyle.Bottom = dgvData.AdvancedCellBorderStyle.Bottom;
                }
                return;
            }

            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Top = dgvData.AdvancedCellBorderStyle.Top;
            }

            if (e.RowIndex == dgvData.Rows.Count - 1)
            {
                e.AdvancedBorderStyle.Bottom = dgvData.AdvancedCellBorderStyle.Bottom;
            }
        }

        bool IsTheSameCellValue(int column, int row)
        {
            if (dtData == null) return false;
            if (dtData.DefaultView.Count == 0) return false;

            DataGridViewCell cell1 = dgvData[column, row];
            DataGridViewCell cell2 = dgvData[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }

            int id = (int)dtData.DefaultView[row]["id"];
            int id_pre = (int)dtData.DefaultView[row-1]["id"];

            return cell1.Value.ToString() == cell2.Value.ToString() && id == id_pre;
        }

        private void dgvData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0)
                return;


            if (!new List<int>() { nameTenant.Index, cTypeContract.Index, cAgreements.Index, cPeriodCredit.Index, cItogPenalty.Index }.Contains(e.ColumnIndex))
            { return; }

            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }
        #endregion

        #region "Новое объединение"
        private List<string> MergedRowsInFirstColumn = new List<string>();

        private void dataGrid_Paint(object sender, PaintEventArgs e)
        {
            Merge();
        }

        private void Merge()
        {
            int[] RowsToMerge = new int[2];
            RowsToMerge[0] = -1;

            //Merge first column at first
            for (int i = 0; i < dtData.DefaultView.Count - 1; i++)
            {
                //Console.WriteLine($"{dtData.DefaultView[i]["id_Agreements"]}   =   {dtData.DefaultView[i + 1]["id_Agreements"]}");
                if (dtData.DefaultView[i]["id_Agreements"].Equals(dtData.DefaultView[i + 1]["id_Agreements"]))
                {
                    if (RowsToMerge[0] == -1)
                    {
                        RowsToMerge[0] = i;
                        RowsToMerge[1] = i + 1;
                    }
                    else
                    {
                        RowsToMerge[1] = i + 1;
                    }
                }
                else
                {
                    if (RowsToMerge[0] == -1) { RowsToMerge[0] = i; RowsToMerge[1] = i; }

                    MergeCells(RowsToMerge[0], RowsToMerge[1], dgvData.Columns["nameTenant"].Index, isSelectedCell(RowsToMerge, dgvData.Columns["nameTenant"].Index) ? true : false);
                    CollectMergedRowsInFirstColumn(RowsToMerge[0], RowsToMerge[1]);
                    RowsToMerge[0] = -1;
                }

                if (i == dtData.DefaultView.Count - 2)
                {
                    if (RowsToMerge[0] == -1) { RowsToMerge[0] = i; RowsToMerge[1] = i; }

                    MergeCells(RowsToMerge[0], RowsToMerge[1], dgvData.Columns["nameTenant"].Index, isSelectedCell(RowsToMerge, dgvData.Columns["nameTenant"].Index) ? true : false);
                    CollectMergedRowsInFirstColumn(RowsToMerge[0], RowsToMerge[1]);
                    RowsToMerge[0] = -1;
                }
            }

            return;
            /*
            if (RowsToMerge[0] != -1)
            {
                MergeCells(RowsToMerge[0], RowsToMerge[1], dataGrid.Columns["Manufacture"].Index, isSelectedCell(RowsToMerge, dataGrid.Columns["Manufacture"].Index) ? true : false);
                RowsToMerge[0] = -1;
            }

            //merge all other columns
            for (int iColumn = 1; iColumn < dataSet.Tables["tbl_main"].Columns.Count - 1; iColumn++)
            {
                for (int iRow = 0; iRow < dataSet.Tables["tbl_main"].Rows.Count - 1; iRow++)
                {
                    if ((dataSet.Tables["tbl_main"].Rows[iRow][iColumn] == dataSet.Tables["tbl_main"].Rows[iRow + 1][iColumn]) &&
                         (isRowsHaveOneCellInFirstColumn(iRow, iRow + 1)))
                    {
                        if (RowsToMerge[0] == -1)
                        {
                            RowsToMerge[0] = iRow;
                            RowsToMerge[1] = iRow + 1;
                        }
                        else
                        {
                            RowsToMerge[1] = iRow + 1;
                        }
                    }
                    else
                    {
                        if (RowsToMerge[0] != -1)
                        {
                            MergeCells(RowsToMerge[0], RowsToMerge[1], iColumn, isSelectedCell(RowsToMerge, iColumn) ? true : false);
                            RowsToMerge[0] = -1;
                        }
                    }
                }
                if (RowsToMerge[0] != -1)
                {
                    MergeCells(RowsToMerge[0], RowsToMerge[1], iColumn, isSelectedCell(RowsToMerge, iColumn) ? true : false);
                    RowsToMerge[0] = -1;
                }
            }*/
        }

        private bool isRowsHaveOneCellInFirstColumn(int RowId1, int RowId2)
        {

            foreach (string rowsCollection in MergedRowsInFirstColumn)
            {
                string[] RowsNumber = rowsCollection.Split(';');

                if ((isStringInArray(RowsNumber, RowId1.ToString())) &&
                    (isStringInArray(RowsNumber, RowId2.ToString())))
                {
                    return true;
                }
            }
            return false;
        }

        private bool isStringInArray(string[] Array, string value)
        {
            foreach (string item in Array)
            {
                if (item == value)
                {
                    return true;
                }

            }
            return false;
        }

        private void CollectMergedRowsInFirstColumn(int RowId1, int RowId2)
        {
            string MergedRows = String.Empty;

            for (int i = RowId1; i <= RowId2; i++)
            {
                MergedRows += i.ToString() + ";";
            }
            MergedRowsInFirstColumn.Add(MergedRows.Remove(MergedRows.Length - 1, 1));
        }

        private void MergeCells(int RowId1, int RowId2, int Column, bool isSelected)
        {
            Graphics g = dgvData.CreateGraphics();
            Pen gridPen = new Pen(dgvData.GridColor);

            //Cells Rectangles
            Rectangle CellRectangle1 = dgvData.GetCellDisplayRectangle(Column, RowId1, true);
            Rectangle CellRectangle2 = dgvData.GetCellDisplayRectangle(Column, RowId2, true);

            int rectHeight = 0;
            string MergedRows = String.Empty;

            for (int i = RowId1; i <= RowId2; i++)
            {
                rectHeight += dgvData.GetCellDisplayRectangle(Column, i, false).Height;
            }

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            sf.Trimming = StringTrimming.EllipsisCharacter;

            Rectangle newCell = new Rectangle(CellRectangle1.X, CellRectangle1.Y, CellRectangle1.Width, rectHeight);

            g.FillRectangle(new SolidBrush(isSelected ? dgvData.DefaultCellStyle.SelectionBackColor : dgvData.DefaultCellStyle.BackColor), newCell);

            g.DrawRectangle(gridPen, newCell);

            g.DrawString(dgvData.Rows[RowId1].Cells[Column].Value.ToString(), dgvData.DefaultCellStyle.Font, new SolidBrush(isSelected ? dgvData.DefaultCellStyle.SelectionForeColor : dgvData.DefaultCellStyle.ForeColor), newCell.X + newCell.Width / 3, newCell.Y + newCell.Height / 3, sf);
        }

        private bool isSelectedCell(int[] Rows, int ColumnIndex)
        {
            //if (Rows[0] == -1) return false;

            if (dgvData.SelectedCells.Count > 0)
            {
                for (int iCell = Rows[0]; iCell <= Rows[1]; iCell++)
                {
                    for (int iSelCell = 0; iSelCell < dgvData.SelectedCells.Count; iSelCell++)
                    {
                        if (dgvData.Rows[iCell].Cells[ColumnIndex] == dgvData.SelectedCells[iSelCell])
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        #endregion


        private void btUpdate_Click(object sender, EventArgs e)
        {
            getData();
        }

        private void cmbObject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void tbTenant_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void cmbPeriodCredit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getData();
        }

        private void chbCongressAccept_Click(object sender, EventArgs e)
        {
            setFilter();
        }

        private void dgvData_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tb = (TextBox)e.Control;
            tb.KeyPress -= new KeyPressEventHandler(tbSumPenalty_KeyPress);
            tb.KeyPress += new KeyPressEventHandler(tbSumPenalty_KeyPress);
        }

        private void tbSumPenalty_KeyPress(object sender, KeyPressEventArgs e)
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
            //if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            //e.Handled = true;
        }

        private bool isEditCell = false;
        private decimal preCountFeedBack;
        private string preProName;

        private void dgvData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            isEditCell = true;

            string _proName = dgvData.Columns[e.ColumnIndex].DataPropertyName;
            preCountFeedBack = (decimal)dtData.DefaultView[e.RowIndex][_proName];
            preProName = _proName;
        }

        private void dgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string _proName = dgvData.Columns[e.ColumnIndex].DataPropertyName;
            string nameTitle = dgvData.Columns[e.ColumnIndex].HeaderText;

            decimal PercentPenalty = (decimal)dtData.DefaultView[e.RowIndex]["PercentPenalty"];
            decimal SummaPenalty = (decimal)dtData.DefaultView[e.RowIndex]["SummaPenalty"];
            int id_payment = (int)dtData.DefaultView[e.RowIndex]["id_payment"];
            int id = (int)dtData.DefaultView[e.RowIndex]["id"];


            decimal postCountFeedBack = (decimal)dtData.DefaultView[e.RowIndex][preProName];
            if (postCountFeedBack == preCountFeedBack) { isEditCell = false; return; }

            if (preProName.Equals("PercentPenalty"))
            {
                int CountDaysCredit = (int)dtData.DefaultView[e.RowIndex]["CountDaysCredit"];
                decimal SummaCredit = (decimal)dtData.DefaultView[e.RowIndex]["SummaCredit"];
                SummaPenalty = Math.Round(((SummaCredit * PercentPenalty) / 100)* CountDaysCredit);                
            }



            Task<DataTable> task = Config.hCntMain.setPenalty(id_payment, SummaPenalty, PercentPenalty);
            task.Wait();

            if (task.Result == null || task.Result.Rows.Count == 0)
            {
                MessageBox.Show("Не удалось сохранить данные", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isEditCell = false;
                return;
            }

            if ((int)task.Result.Rows[0]["id"] == -9999)
            {
                MessageBox.Show("Произошла неведомая фигня.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isEditCell = false;
                return;
            }

            if (preProName.Equals("PercentPenalty"))                       
            {
                Logging.StartFirstLevel(1570);
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    if (col.Visible && new List<string> {nameTenant.Name,cTypeContract.Name,cAgreements.Name,cPeriodCredit.Name,
                        cSummaCredit.Name,cDatePay.Name,cSumma.Name,cCountDaysCredit.Name }.Contains(col.Name))
                    {
                        Logging.Comment($"{col.HeaderText}: {dgvData.CurrentRow.Cells[col.Name].Value.ToString()}");
                    }                   
                }
                Logging.VariableChange(cPrcPenalty.HeaderText, postCountFeedBack, preCountFeedBack);
                Logging.StopFirstLevel();
            }
            else
            {
                Logging.StartFirstLevel(1571);
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    if (col.Visible && new List<string> {nameTenant.Name,cTypeContract.Name,cAgreements.Name,cPeriodCredit.Name,
                        cSummaCredit.Name,cDatePay.Name,cSumma.Name,cCountDaysCredit.Name }.Contains(col.Name))
                    {
                        Logging.Comment($"{col.HeaderText}: {dgvData.CurrentRow.Cells[col.Name].Value.ToString()}");
                    }
                }
                Logging.VariableChange(cSummaPenalty.HeaderText, postCountFeedBack, preCountFeedBack);
                
                Logging.StopFirstLevel();
            }


            isEditCell = false;
            
            if (preProName.Equals("PercentPenalty"))
            {
                dtData.DefaultView[e.RowIndex]["SummaPenalty"] = SummaPenalty.ToString("0.00");
            }

            var groupSumPenalty = dtData.AsEnumerable()
                    .Where(r => r.Field<int>("id") == id)
                    .GroupBy(r => new { id = r.Field<int>("id") })
                    .Select(s => new
                    {
                        s.Key.id,
                        sumItogPenalty = s.Sum(r => r.Field<decimal>("SummaPenalty"))
                    });

            foreach (var gSum in groupSumPenalty)
            {
                EnumerableRowCollection<DataRow> rowCollect = dtData.AsEnumerable().Where(r => r.Field<int>("id") == gSum.id);
                foreach (DataRow row in rowCollect)
                {
                    row["sumItogPenalty"] = gSum.sumItogPenalty.ToString("0.00");
                }
            }
            setFilter();
        }

        private void dgvData_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!isEditCell) return;

            string _proName = dgvData.Columns[e.ColumnIndex].DataPropertyName;

            decimal _value;
            if (!decimal.TryParse(e.FormattedValue.ToString(), out _value))
            {
                e.Cancel = true;
                return;
            }

            if (_proName.Equals("PercentPenalty"))
                if (_value > 100) { e.Cancel = true; return; }
            

            if (_value == preCountFeedBack) { return; }

            if (DialogResult.No == MessageBox.Show("Сохранить изменения пени?", "Запрос на сохранение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) 
            { e.Cancel = true; return; }


            // else if (_value == 0) { e.Cancel = true; return; }
        }

        private void dgvData_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!isEditCell) return;

            string _proName = dgvData.Columns[e.ColumnIndex].DataPropertyName;

            decimal inValue = (decimal)dtData.DefaultView[e.RowIndex][_proName];
            dtData.DefaultView[e.RowIndex][_proName] = inValue.ToString("0.00");
        }

        private void btAcceptD_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                if (DialogResult.Yes == MessageBox.Show(Config.centralText("Вы действительно хотите подтвердить\nначисленные пени?\n"),"Подтверждение пени",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2))
                {

                    int id_Agreements = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_Agreements"];
                    int id = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];
                    decimal summPenalty = (decimal)dtData.DefaultView[dgvData.CurrentRow.Index]["sumItogPenalty"];
                    string period  = (string)dtData.DefaultView[dgvData.CurrentRow.Index]["PeriodCredit"];

                    Task<DataTable> task = Config.hCntMain.setFines(id_Agreements, id, summPenalty, period);
                    task.Wait();

                    if (task.Result == null || task.Result.Rows.Count == 0)
                    {
                        MessageBox.Show("Не удалось сохранить данные", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        isEditCell = false;
                        return;
                    }

                    if ((int)task.Result.Rows[0]["id"] == -1)
                    {
                        MessageBox.Show("Не найдена запись в Arenda.s_AddPayment!", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        isEditCell = false;
                        return;
                    }

                    if ((int)task.Result.Rows[0]["id"] == -9999)
                    {
                        MessageBox.Show("Произошла неведомая фигня.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        isEditCell = false;
                        return;
                    }

                    Logging.StartFirstLevel((int)logEnum.Подтверждение_пени);
                    Logging.Comment($"ID: {id}");
                    Logging.Comment($"ID договора: {id_Agreements}");
                    foreach (DataGridViewColumn col in dgvData.Columns)
                    {
                        if (col.Visible)
                            Logging.Comment($"{col.HeaderText}: {dgvData.CurrentRow.Cells[col.Name].Value.ToString()}");
                    }
                    Logging.StopFirstLevel();

                    getData();
                }
            }
        }

        private void setWidthColumn(int indexRow, int indexCol, int width, Nwuram.Framework.ToExcelNew.ExcelUnLoad report)
        {
            report.SetColumnWidth(indexRow, indexCol, indexRow, indexCol, width);
        }

        private void btPrint_Click(object sender, EventArgs e)
        {

            Logging.StartFirstLevel(79);

            Logging.Comment($"Фомирование отчёта из \"{this.Text}\"");

            Logging.Comment($"Договор:{tbAgreement.Text}");
            Logging.Comment($"Арендатор:{tbTenant.Text}");
            Logging.Comment($"Период начисления:{cmbPeriodCredit.Text}");
            Logging.Comment($"Объект аренды:{cmbObject.Text}");
            Logging.Comment($"Тип договора:{cmbTypeContract.Text}");
            Logging.Comment($"Подтвержденные пени: {(chbCongressAccept.Checked ? "Да" : "Нет")}");

            Logging.StopFirstLevel();

            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();

            int indexRow = 1;

            int maxColumns = 0;

            foreach (DataGridViewColumn col in dgvData.Columns)
                if (col.Visible)
                {
                    maxColumns++;
                    if (col.Name.Equals("nameTenant")) setWidthColumn(indexRow, maxColumns, 20, report);
                    if (col.Name.Equals("cTypeContract")) setWidthColumn(indexRow, maxColumns, 21, report);
                    if (col.Name.Equals("cAgreements")) setWidthColumn(indexRow, maxColumns, 22, report);
                    if (col.Name.Equals("cPeriodCredit")) setWidthColumn(indexRow, maxColumns, 22, report);
                    if (col.Name.Equals("cSummaCredit")) setWidthColumn(indexRow, maxColumns, 18, report);
                    if (col.Name.Equals("cDatePay")) setWidthColumn(indexRow, maxColumns, 15, report);
                    if (col.Name.Equals("cSumma")) setWidthColumn(indexRow, maxColumns, 16, report);
                    if (col.Name.Equals("cCountDaysCredit")) setWidthColumn(indexRow, maxColumns, 18, report);
                    if (col.Name.Equals("cPrcPenalty")) setWidthColumn(indexRow, maxColumns, 15, report);
                    if (col.Name.Equals("cSummaPenalty")) setWidthColumn(indexRow, maxColumns, 15, report);
                    if (col.Name.Equals("cItogPenalty")) setWidthColumn(indexRow, maxColumns, 15, report);
                }

            #region "Head"
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue("Отчёт по начислению пени", indexRow, 1);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            indexRow++;
            indexRow++;


            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Период начисления {cmbPeriodCredit.Text}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Объект аренды: {cmbObject.Text}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Тип договора: {cmbTypeContract.Text}", indexRow, 1);
            indexRow++;

            if (tbAgreement.Text.Trim().Length > 0 || tbTenant.Text.Trim().Length > 0)
            {
                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue($"Фильтры: " +
                    $"{(tbTenant.Text.Trim().Length == 0 ? "" : "Арендатор: " + tbTenant.Text.Trim())} " +
                    $"{(tbAgreement.Text.Trim().Length == 0 ? "" : "Номер договора: " + tbAgreement.Text.Trim())}", indexRow, 1);
                indexRow++;
            }

            report.Merge(indexRow, 1, indexRow, 6);
            report.AddSingleValue("Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername, indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, 6);
            report.AddSingleValue("Дата выгрузки: " + DateTime.Now.ToString(), indexRow, 1);
            indexRow++;
            indexRow++;
            #endregion

            int indexCol = 0;
            foreach (DataGridViewColumn col in dgvData.Columns)
                if (col.Visible)
                {
                    indexCol++;
                    report.AddSingleValue(col.HeaderText, indexRow, indexCol);
                }
            report.SetFontBold(indexRow, 1, indexRow, maxColumns);
            report.SetBorders(indexRow, 1, indexRow, maxColumns);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
            indexRow++;

            var groupByPost = dtData.DefaultView.ToTable().AsEnumerable().GroupBy(r => new { id = r.Field<int>("id") })
                .Select(s => new { s.Key.id });

            foreach (var gPost in groupByPost)
            {
                EnumerableRowCollection<DataRow> rowCollect = dtData.DefaultView.ToTable().AsEnumerable().Where(r => r.Field<int>("id") == gPost.id);
                int startMergRow = indexRow;

                foreach (DataRow row in rowCollect)
                {
                    indexCol = 1;                    
                    report.SetWrapText(indexRow, indexCol, indexRow, maxColumns);
                    foreach (DataGridViewColumn col in dgvData.Columns)
                    {
                        if (col.Visible)
                        {
                            if (new List<int>() { nameTenant.Index, cTypeContract.Index, cAgreements.Index, cPeriodCredit.Index, cItogPenalty.Index }.Contains(col.Index))
                            {
                                indexCol++;
                                continue;
                            }


                            if (row[col.DataPropertyName] is DateTime)
                                report.AddSingleValue(((DateTime)row[col.DataPropertyName]).ToShortDateString(), indexRow, indexCol);
                            else
                               if (row[col.DataPropertyName] is decimal)
                            {
                                report.AddSingleValueObject(row[col.DataPropertyName], indexRow, indexCol);
                                report.SetFormat(indexRow, indexCol, indexRow, indexCol, "0.00");
                            }
                            else
                                report.AddSingleValue(row[col.DataPropertyName].ToString(), indexRow, indexCol);
                            
                            indexCol++;
                        }
                    }

                    report.SetBorders(indexRow, 1, indexRow, maxColumns);
                    report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                    report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);

                    if ((int)row["id_StatusPenalty"] == 3)
                        report.SetCellColor(indexRow, 1, indexRow, maxColumns, panel2.BackColor);
                    indexRow++;
                }

                indexCol = 1;
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    if(new List<int>() { nameTenant.Index, cTypeContract.Index, cAgreements.Index, cPeriodCredit.Index, cItogPenalty.Index }.Contains(col.Index))
                    {
                        report.Merge(startMergRow, indexCol, indexRow - 1, indexCol);

                        if (rowCollect.First()[col.DataPropertyName] is DateTime)
                            report.AddSingleValue(((DateTime)rowCollect.First()[col.DataPropertyName]).ToShortDateString(), startMergRow, indexCol);
                        else
                             if (rowCollect.First()[col.DataPropertyName] is decimal)
                        {
                            report.AddSingleValueObject(rowCollect.First()[col.DataPropertyName], startMergRow, indexCol);
                            report.SetFormat(startMergRow, indexCol, startMergRow, indexCol, "0.00");
                        }
                        else
                            report.AddSingleValue(rowCollect.First()[col.DataPropertyName].ToString(), startMergRow, indexCol);

                    }
                    indexCol++;
                }
            }

            /*
            indexCol = 0;
            int indexStartRow = indexRow;
            //foreach (DataGridViewRow row in dgvData.Rows)
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                if (col.Visible)
                {
                    indexCol++;
                    indexRow = indexStartRow;
                    int startMergRow = indexRow;
                    object tmpValue = null;
                    int preId = 0;
                    //foreach (DataGridViewColumn col in dgvData.Columns)
                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        if ((int)dtData.DefaultView[row.Index]["id_StatusPenalty"] == 3)
                            report.SetCellColor(indexRow, 1, indexRow, maxColumns, panel2.BackColor);

                        report.SetWrapText(indexRow, indexCol, indexRow, indexCol);

                        if (new List<int>() { nameTenant.Index, cTypeContract.Index, cAgreements.Index, cPeriodCredit.Index, cItogPenalty.Index }.Contains(col.Index))
                        {
                            if (indexRow == indexStartRow)
                            {
                                tmpValue = row.Cells[col.Index].Value;
                                preId = (int)dtData.DefaultView[row.Index]["id"];
                            }
                            else if (indexRow - indexStartRow == dgvData.Rows.Count - 1)
                            {
                                report.Merge(startMergRow, indexCol, indexRow-1, indexCol);

                                if (tmpValue is DateTime)
                                    report.AddSingleValue(((DateTime)tmpValue).ToShortDateString(), startMergRow, indexCol);
                                if (tmpValue is decimal)
                                {
                                    report.AddSingleValueObject(tmpValue, startMergRow, indexCol);
                                    report.SetFormat(startMergRow, indexCol, startMergRow, indexCol, "0.00");
                                }
                                else
                                    report.AddSingleValue(tmpValue.ToString(), startMergRow, indexCol);


                                if (row.Cells[col.Index].Value is DateTime)
                                    report.AddSingleValue(((DateTime)row.Cells[col.Index].Value).ToShortDateString(), indexRow, indexCol);
                                else
                                 if (row.Cells[col.Index].Value is decimal)
                                {
                                    report.AddSingleValueObject(row.Cells[col.Index].Value, indexRow, indexCol);
                                    report.SetFormat(indexRow, indexCol, indexRow, indexCol, "0.00");
                                }
                                else
                                    report.AddSingleValue(row.Cells[col.Index].Value.ToString(), indexRow, indexCol);

                            }
                            else if (indexRow != indexStartRow)
                            {
                                if (!tmpValue.Equals(row.Cells[col.Index].Value) || preId != (int)dtData.DefaultView[row.Index]["id"])
                                {
                                    report.Merge(startMergRow, indexCol, indexRow - 1, indexCol);

                                    if (tmpValue is DateTime)
                                        report.AddSingleValue(((DateTime)tmpValue).ToShortDateString(), startMergRow, indexCol);
                                    else
                                        if (tmpValue is decimal)
                                    {
                                        report.AddSingleValueObject(tmpValue, startMergRow, indexCol);
                                        report.SetFormat(startMergRow, indexCol, startMergRow, indexCol, "0.00");
                                    }
                                    else
                                        report.AddSingleValue(tmpValue.ToString(), startMergRow, indexCol);

                                    startMergRow = indexRow;
                                    tmpValue = row.Cells[col.Index].Value;
                                    preId = (int)dtData.DefaultView[row.Index]["id"];
                                }
                            }
                        }
                        else
                        {
                            if (row.Cells[col.Index].Value is DateTime)
                                report.AddSingleValue(((DateTime)row.Cells[col.Index].Value).ToShortDateString(), indexRow, indexCol);
                            else
                                if (row.Cells[col.Index].Value is decimal)
                            {
                                report.AddSingleValueObject(row.Cells[col.Index].Value, indexRow, indexCol);
                                report.SetFormat(indexRow, indexCol, indexRow, indexCol, "0.00");
                            }
                            else
                                report.AddSingleValue(row.Cells[col.Index].Value.ToString(), indexRow, indexCol);
                        }


                        report.SetBorders(indexRow, 1, indexRow, maxColumns);
                        report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                        report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                        indexRow++;
                    }
                }                
            }
            */

            report.SetCellColor(indexRow, 1, indexRow, 1, panel2.BackColor);
            report.Merge(indexRow, 2, indexRow, 4);
            report.AddSingleValue(chbCongressAccept.Text, indexRow, 2);

            report.SetFontBold(indexRow, maxColumns - 1, indexRow, maxColumns);
            report.SetBorders(indexRow, maxColumns - 1, indexRow, maxColumns);
            report.SetCellAlignmentToRight(indexRow, maxColumns - 1, indexRow, maxColumns);
            report.AddSingleValue("Итого", indexRow, maxColumns - 1);
            report.AddSingleValue(tbItogPenalty.Text, indexRow, maxColumns);



            report.Show();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dgvData_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if ((new List<string> { "Д" }.Contains(UserSettings.User.StatusCode) && (int)dtData.DefaultView[e.RowIndex]["id_StatusPenalty"] != 3))
            {
                if (cSummaPenalty.Index==e.ColumnIndex || cPrcPenalty.Index==e.ColumnIndex)
                    dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;else
                    dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
            } else
            {
                dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
            }
        }
    }
}
