using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JournalBorrower
{
    public partial class frmMain : Form
    {
        private DataTable dtData;


        public frmMain()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Task<DataTable> task = Config.hCntMain.getObjectLease(true);
            task.Wait();
            DataTable dtObjectLease = task.Result;

            cmbObject.DisplayMember = "cName";
            cmbObject.ValueMember = "id";
            cmbObject.DataSource = dtObjectLease;
            rbPayDoc_Click(null, null);
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
                    tbAgreements.Location = new Point(dgvData.Location.X + width + 1, tbTenant.Location.Y);
                    tbAgreements.Size = new Size(cAgreements.Width, tbTenant.Height);
                }

                if (col.Index == cPlace.Index)
                {
                    tbPlace.Location = new Point(dgvData.Location.X + width + 1, tbTenant.Location.Y);
                    tbPlace.Size = new Size(cPlace.Width, tbTenant.Height);
                }

                width += col.Width;
            }
        }

        private void rbPayDoc_Click(object sender, EventArgs e)
        {
            cSumDoc.Visible = rbPayDoc.Checked;
            cDateCloseSection.Visible = rbPayDoc.Checked;
            if (rbPayDoc.Checked)
            {
                cSumPay.DataPropertyName = "SummaPaymentFine_1";
                cSumItogSum.DataPropertyName = "SummaFine_1";
                cSumOwe.DataPropertyName = "SummaPenny_1";
                cPrcOwe.DataPropertyName = "PrcPenny_1";
            }
            else {
                cSumPay.DataPropertyName = "SummaPaymentFine_2";
                cSumItogSum.DataPropertyName = "SummaFine_2";
                cSumOwe.DataPropertyName = "SummaPenny_2";
                cPrcOwe.DataPropertyName = "PrcPenny_2";
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void setWidthColumn(int indexRow, int indexCol, int width, Nwuram.Framework.ToExcelNew.ExcelUnLoad report)
        {
            report.SetColumnWidth(indexRow, indexCol, indexRow, indexCol, width);
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();

            int indexRow = 1;

            int maxColumns = 0;

            foreach (DataGridViewColumn col in dgvData.Columns)
                if (col.Visible)
                {
                    maxColumns++;
                    if (col.Name.Equals("nameTenant")) setWidthColumn(indexRow, maxColumns, 20, report);                    
                    if (col.Name.Equals("cAgreements")) setWidthColumn(indexRow, maxColumns, 22, report);
                    if (col.Name.Equals("cObject")) setWidthColumn(indexRow, maxColumns, 22, report);
                    if (col.Name.Equals("cPlace")) setWidthColumn(indexRow, maxColumns, 35, report);
                    if (col.Name.Equals("cSumMeter")) setWidthColumn(indexRow, maxColumns, 15, report);
                    if (col.Name.Equals("cSumDoc")) setWidthColumn(indexRow, maxColumns, 20, report);
                    if (col.Name.Equals("cSumPay")) setWidthColumn(indexRow, maxColumns, 18, report);
                    if (col.Name.Equals("cSumItogSum")) setWidthColumn(indexRow, maxColumns, 15, report);
                    if (col.Name.Equals("cSumOwe")) setWidthColumn(indexRow, maxColumns, 15, report);
                    if (col.Name.Equals("cPrcOwe")) setWidthColumn(indexRow, maxColumns, 15, report);
                    if (col.Name.Equals("cDateCloseSection")) setWidthColumn(indexRow, maxColumns, 20, report);
                    Console.WriteLine(col.Name);
                }

            #region "Head"
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Журнал должников по {(rbPayDopDoc.Checked?"доп.":"")} оплатам", indexRow, 1);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            indexRow++;
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Объект аренды: {cmbObject.Text}", indexRow, 1);
            indexRow++;

            if (tbAgreements.Text.Trim().Length > 0 || tbTenant.Text.Trim().Length > 0 || tbPlace.Text.Trim().Length > 0)
            {
                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue($"Фильтры: " +
                    $"{(tbTenant.Text.Trim().Length == 0 ? "" : "Арендатор: " + tbTenant.Text.Trim())} " +
                    $"{(tbPlace.Text.Trim().Length == 0 ? "" : "    Местоположение места аренды: " + tbPlace.Text.Trim())} " +
                    $"{(tbAgreements.Text.Trim().Length == 0 ? "" : "   Номер договора: " + tbAgreements.Text.Trim())}", indexRow, 1);
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
                            if (new List<int>() { nameTenant.Index, cSumPay.Index, cSumItogSum.Index, cSumOwe.Index, cPrcOwe.Index }.Contains(col.Index))
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
                    indexRow++;
                }

                indexCol = 1;
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    if (new List<int>() { nameTenant.Index, cSumPay.Index, cSumItogSum.Index, cSumOwe.Index, cPrcOwe.Index }.Contains(col.Index))
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
            

            report.Show();
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            getData();
        }

        private void tbTenant_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void cmbObject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void getData()
        {
            Task<DataTable> task = Config.hCntMain.GetListOwe();
            task.Wait();
            dtData = task.Result;

            if (dtData == null || dtData.Rows.Count == 0)
            {
                dgvData.DataSource = null; return;
            }

            dtData.Columns.Add("SummaPaymentFine_1", typeof(decimal));
            dtData.Columns.Add("SummaFine_1", typeof(decimal));
            dtData.Columns.Add("SummaPenny_1", typeof(decimal));
            dtData.Columns.Add("PrcPenny_1", typeof(decimal));

            dtData.Columns.Add("SummaPaymentFine_2", typeof(decimal));
            dtData.Columns.Add("SummaFine_2", typeof(decimal));
            dtData.Columns.Add("SummaPenny_2", typeof(decimal));
            dtData.Columns.Add("PrcPenny_2", typeof(decimal));

            //task = Config.hCntMain.GetListOweAdditionalData(1);
            task = Config.hCntMain.GetListOweAdditionalData(2);
            if (task.Result != null && task.Result.Rows.Count > 0)
            {
                foreach (DataRow row in task.Result.Rows)
                {
                    EnumerableRowCollection<DataRow> rowCollect = dtData.AsEnumerable().Where(r => r.Field<int>("id") == (int)row["id"]);
                    if (rowCollect.Count() > 0)
                    {
                        rowCollect.First()["SummaPaymentFine_2"] = row["SummaPaymentFine"];
                        rowCollect.First()["SummaFine_2"] = row["SummaFine"];
                        rowCollect.First()["SummaPenny_2"] = row["SummaPenny"];
                        rowCollect.First()["PrcPenny_2"] = row["PrcPenny"];
                    }
                }
            }

            setFilter();
            dgvData.DataSource = dtData;
        }

        private void setFilter()
        {
            if (dtData == null || dtData.Rows.Count == 0)
            {
                btPrint.Enabled = false;
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

                if ((int)cmbObject.SelectedValue !=0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_ObjectLease = {cmbObject.SelectedValue}";

                //if (!chbNotActive.Checked)
                //    filter += (filter.Length == 0 ? "" : " and ") + $"isActive = 1";

                dtData.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally
            {
                btPrint.Enabled = dtData.DefaultView.Count != 0;
                //dgvData_SelectionChanged(null, null);
            }
        }

    }
}
