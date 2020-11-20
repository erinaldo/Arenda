using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dllJournalPlaneReport
{
    public class reports
    {

        public static void createReport(int id_tMonthPlane,DateTime _startDate, int id_objectLeaser,string _nameObject,string status)
        {
            Task<DataTable> task = Config.hCntMain.getPlanReport(_startDate.Date, id_objectLeaser, id_tMonthPlane);
            task.Wait();

            if (task.Result == null || task.Result.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для отчёта!","Выгрузка отчёта",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            DataTable dtData = task.Result;

            foreach (DataRow row in dtData.Rows)
            {
                row["timeLimit"] = $"{((DateTime)row["Start_Date"]).ToShortDateString()} - {((DateTime)row["Stop_Date"]).ToShortDateString()}";
            }

            createReport(dtData, _nameObject, status, _startDate);
        }

        public static void createReport(DataTable dtReport, string _nameObject,string status,DateTime _startDate)
        {
            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();
            int indexRow = 1;

            List<string> listColumnsSum = new List<string>() { "preCredit", "preOverPayment", "prePlan", "EndPlan", "Penalty", "OtherPayments", "ultraResult", "Included", "Credit", "OverPayment" };
            List<string> listColumnGroup = new List<string>() { "nameLandLord" };
            Dictionary<string, int> dicColumnNameForIndex = new Dictionary<string, int>();

            Control[] cnt = new frmAddReportPlane().Controls.Find("dgvData", false);
            DataGridView dgvData = (DataGridView)cnt[0];

            int maxColumns = 0;
            bool isAddItog = false;

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
                    dicColumnNameForIndex.Add(col.Name, maxColumns);
                }

            #region "Head"
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue("План отчёт", indexRow, 1);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            indexRow++;
            indexRow++;

            string[] monthNames = DateTimeFormatInfo.CurrentInfo.MonthNames;
            report.Merge(indexRow, 1, indexRow, maxColumns);
//<<<<<<< HEAD
            report.AddSingleValue($"Период план-отчёта {monthNames[_startDate.Month-1]}.{_startDate.Year}", indexRow, 1);
//=======
//            report.AddSingleValue($"Период ежемесячного отчёта {monthNames[_startDate.Month-1]}.{_startDate.Year}", indexRow, 1);
//>>>>>>> 5b40c2b8d571ec7b2bfd3ee230bb62ce5596e5b9
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
                   preCredit = s.Sum(r=>r.Field<decimal>("preCredit")),
                   preOverPayment = s.Sum(r => r.Field<decimal>("preOverPayment")),
                   prePlan = s.Sum(r => r.Field<decimal>("prePlan")),
                   EndPlan = s.Sum(r => r.Field<decimal>("EndPlan")),
                   Penalty = s.Sum(r => r.Field<decimal>("Penalty")),
                   OtherPayments = s.Sum(r => r.Field<decimal>("OtherPayments")),
                   ultraResult = s.Sum(r => r.Field<decimal>("ultraResult")),
                   Included = s.Sum(r => r.Field<decimal>("Included")),
                   Credit = s.Sum(r => r.Field<decimal>("Credit")),
                   OverPayment = s.Sum(r => r.Field<decimal>("OverPayment"))
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
                isAddItog = false;
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    if (listColumnGroup.Contains(col.Name))
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
                    else if (listColumnsSum.Contains(col.Name))
                    {
                        switch(col.Name)
                        {
                            case "preCredit": report.AddSingleValueObject(gPost.preCredit, indexRow, indexCol);break;
                            case "preOverPayment": report.AddSingleValueObject(gPost.preOverPayment, indexRow, indexCol); break;
                            case "prePlan": report.AddSingleValueObject(gPost.prePlan, indexRow, indexCol); break;
                            case "EndPlan": report.AddSingleValueObject(gPost.EndPlan, indexRow, indexCol); break;
                            case "Penalty": report.AddSingleValueObject(gPost.Penalty, indexRow, indexCol); break;
                            case "OtherPayments": report.AddSingleValueObject(gPost.OtherPayments, indexRow, indexCol); break;
                            case "ultraResult": report.AddSingleValueObject(gPost.ultraResult, indexRow, indexCol); break;
                            case "Included": report.AddSingleValueObject(gPost.Included, indexRow, indexCol); break;
                            case "Credit": report.AddSingleValueObject(gPost.Credit, indexRow, indexCol); break;
                            case "OverPayment": report.AddSingleValueObject(gPost.OverPayment, indexRow, indexCol); break;
                            default: break;

                        }
                        //report.AddSingleValueObject(gPost.preCredit, indexRow, indexCol);


                        report.SetFormat(indexRow, indexCol, indexRow, indexCol, "0.00");
                        if (!isAddItog)
                        {
                            report.Merge(indexRow, 1, indexRow, indexCol - 1);
                            report.AddSingleValue($" Итого:", indexRow, 1);

                            isAddItog = true;
                        }
                        report.SetCellAlignmentToRight(indexRow, 1, indexRow, indexCol - 1);                        
                    }
                    indexCol++;
                }
                report.SetBorders(indexRow, 1, indexRow, maxColumns);
                report.SetFontBold(indexRow, 1, indexRow, maxColumns);
                indexRow++;
                maxResultPlane += gPost.preCredit;            
            }

            indexRow++;

            isAddItog = false;
            foreach (string lCol in listColumnsSum)
            {
                if (!isAddItog)
                {
                    report.Merge(indexRow, 1, indexRow, dicColumnNameForIndex[lCol] - 1);
                    report.AddSingleValue($" Итого:", indexRow, 1);
                    
                    isAddItog = true;
                }
                report.AddSingleValueObject(dtReport.AsEnumerable().Sum(r => r.Field<decimal>(lCol)), indexRow, dicColumnNameForIndex[lCol]);
                report.SetFormat(indexRow, dicColumnNameForIndex[lCol], indexRow, dicColumnNameForIndex[lCol], "0.00");
            }

            //report.AddSingleValueObject(maxResultPlane, indexRow, maxColumns);
            //report.SetFormat(indexRow, maxColumns, indexRow, maxColumns, "0.00");
            //report.Merge(indexRow, 1, indexRow, maxColumns - 1);
            //report.AddSingleValue("Итого:", indexRow, 1);
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
