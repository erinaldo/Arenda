using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dllJournalReport
{
    public class reports
    {

        public static void createReport(int id_tMonthPlane,DateTime _startDate, int id_objectLeaser,string _nameObject,string status)
        {
            Task<DataTable> task = Config.hCntMain.getMonthReport(_startDate.Date, id_objectLeaser, id_tMonthPlane);
            task.Wait();

            if (task.Result == null || task.Result.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для отчёта!","Выгрузка отчёта",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            createReport(task.Result, _nameObject, status, _startDate);
        }

        public static void createReport(DataTable dtReport, string _nameObject,string status,DateTime _startDate)
        {
            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();
            int indexRow = 1;

            Control[] cnt = new frmAddReportMonth().Controls.Find("dgvData", false);
            DataGridView dgvData = (DataGridView)cnt[0];

            int maxColumns = 0;

            foreach (DataGridViewColumn col in dgvData.Columns)
                if (col.Visible)
                {
                    maxColumns++;
                    if (col.Name.Equals("nameLandLord")) setWidthColumn(indexRow, maxColumns, 20, report);
                    if (col.Name.Equals("nameTenant")) setWidthColumn(indexRow, maxColumns, 20, report);
                    if (col.Name.Equals("TypeContract")) setWidthColumn(indexRow, maxColumns, 13, report);
                    if (col.Name.Equals("cAgreements")) setWidthColumn(indexRow, maxColumns, 22, report);
                    if (col.Name.Equals("timeLimit")) setWidthColumn(indexRow, maxColumns, 14, report);
                    if (col.Name.Equals("cBuild")) setWidthColumn(indexRow, maxColumns, 9, report);
                    if (col.Name.Equals("cFloor")) setWidthColumn(indexRow, maxColumns, 10, report);
                    if (col.Name.Equals("cSection")) setWidthColumn(indexRow, maxColumns, 10, report);
                    if (col.Name.Equals("cSquart")) setWidthColumn(indexRow, maxColumns, 12, report);
                    if (col.Name.Equals("Cost_of_Meter")) setWidthColumn(indexRow, maxColumns, 14, report);
                    if (col.Name.Equals("cSumDog")) setWidthColumn(indexRow, maxColumns, 19, report);
                    if (col.Name.Equals("cDiscount")) setWidthColumn(indexRow, maxColumns, 10, report);
                    if (col.Name.Equals("cPlane")) setWidthColumn(indexRow, maxColumns, 20, report);
                    //Console.WriteLine(col.Name);
                }

            #region "Head"
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue("Ежемесячный отчёт", indexRow, 1);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            indexRow++;
            indexRow++;

            string[] monthNames = DateTimeFormatInfo.CurrentInfo.MonthNames;
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Период ежемесячного отчёта {monthNames[_startDate.Month]}.{_startDate.Year}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Объект аренды: {_nameObject}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Статус: {status}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue("Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername, indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
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

            var groupByPost = dtReport.DefaultView.ToTable().AsEnumerable().GroupBy(r => new { id = r.Field<int>("id_Landlord") })
               .Select(s => new { s.Key.id,
               sumPlan = s.Sum(r=>r.Field<decimal>("plane"))
               });
            decimal maxResultPlane = 0;

            foreach (var gPost in groupByPost)
            {
                EnumerableRowCollection<DataRow> rowCollect = dtReport.DefaultView.ToTable().AsEnumerable().Where(r => r.Field<int>("id_Landlord") == gPost.id);
                int startMergRow = indexRow;

                foreach (DataRow row in rowCollect)
                {
                    indexCol = 1;
                    report.SetWrapText(indexRow, indexCol, indexRow, maxColumns);
                    foreach (DataGridViewColumn col in dgvData.Columns)
                    {
                        if (col.Visible)
                        {
                            if (new List<string>() { "nameLandLord" }.Contains(col.Name))
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
                    if (new List<string>() { "nameLandLord" }.Contains(col.Name))
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
                    else if (new List<string>() { "cPlane" }.Contains(col.Name))
                    {
                        report.AddSingleValueObject(gPost.sumPlan, indexRow, indexCol);
                        report.SetFormat(indexRow, indexCol, indexRow, indexCol, "0.00");
                        report.Merge(indexRow, 1, indexRow, indexCol - 1);
                        report.AddSingleValue("Итого:", indexRow, 1);
                        report.SetCellAlignmentToRight(indexRow, 1, indexRow, indexCol - 1);
                        indexRow++;
                    }
                    indexCol++;
                }
                maxResultPlane += gPost.sumPlan;            
            }

            report.AddSingleValueObject(maxResultPlane, indexRow, maxColumns);
            report.SetFormat(indexRow, maxColumns, indexRow, maxColumns, "0.00");
            report.Merge(indexRow, 1, indexRow, maxColumns - 1);
            report.AddSingleValue("Итого:", indexRow, 1);
            report.SetCellAlignmentToRight(indexRow, 1, indexRow, maxColumns - 1);
            report.SetFontBold(indexRow, 1, indexRow, maxColumns);
            indexRow++;


            report.Show();            
        }

        private static void setWidthColumn(int indexRow, int indexCol, int width, Nwuram.Framework.ToExcelNew.ExcelUnLoad report)
        {
            report.SetColumnWidth(indexRow, indexCol, indexRow, indexCol, width);
        }
    }
}
