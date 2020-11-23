using Nwuram.Framework.Settings.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arenda.Reports
{
    public partial class frmReportFinesPay : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        private Nwuram.Framework.UI.Service.EnableControlsServiceInProg blockers = new Nwuram.Framework.UI.Service.EnableControlsServiceInProg();
        private Nwuram.Framework.ToExcelNew.ExcelUnLoad report = null;
        private string[] monthNames = DateTimeFormatInfo.CurrentInfo.MonthNames;

        public frmReportFinesPay()
        {
            InitializeComponent();
        }

        private void frmReportFinesPay_Load(object sender, EventArgs e)
        {

        }

        private void setWidthColumn(int indexRow, int indexCol, int width, Nwuram.Framework.ToExcelNew.ExcelUnLoad report)
        {
            report.SetColumnWidth(indexRow, indexCol, indexRow, indexCol, width);
        }

        private void addDataToCell(object value, int indexRow, int indexCol, Nwuram.Framework.ToExcelNew.ExcelUnLoad report, bool isFullTime = false)
        {
            if (value is DateTime)
            {
                if (isFullTime)
                    report.AddSingleValue(((DateTime)value).ToString(), indexRow, indexCol);
                else
                    report.AddSingleValue(((DateTime)value).ToShortDateString(), indexRow, indexCol);
            }
            else if (value is bool)
                report.AddSingleValue((bool)value ? "Да" : "Нет", indexRow, indexCol);
            else if (value is decimal || value is double)
            {
                report.AddSingleValueObject(value, indexRow, indexCol);
                report.SetFormat(indexRow, indexCol, indexRow, indexCol, "0.00");
            }
            else
                report.AddSingleValue(value.ToString(), indexRow, indexCol);
        }

        private void DoOnUIThread(MethodInvoker d, Form _this)
        {
            if (_this.InvokeRequired) { _this.Invoke(d); } else { d(); }
        }

        private async void btPrint_Click(object sender, EventArgs e)
        {

            report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();
            blockers.SaveControlsEnabledState(this);
            blockers.SetControlsEnabled(this, false);
            progressBar1.Visible = true;

            DateTime date = new DateTime(dtpYear.Value.Year, dtpMonth.Value.Month, 1);

            var result = await Task<bool>.Factory.StartNew(() =>
            {
                DataTable dtReport = _proc.GetReportFinesPay(date.Date);

                if (dtReport == null || dtReport.Rows.Count == 0)
                {
                    DoOnUIThread(() =>
                    {
                        MessageBox.Show("Нет данных для выгрузки", "Выгрузка отчёта", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        blockers.RestoreControlEnabledState(this);
                        progressBar1.Visible = false;
                    }, this);
                    return false;
                }


                int indexRow = 1;
                int maxColumns = 11;

                setWidthColumn(indexRow, 1, 8, report);
                setWidthColumn(indexRow, 2, 12, report);
                setWidthColumn(indexRow, 3, 30, report);
                setWidthColumn(indexRow, 4, 10, report);
                setWidthColumn(indexRow, 5, 17, report);
                setWidthColumn(indexRow, 6, 18, report);
                setWidthColumn(indexRow, 7, 10, report);
                setWidthColumn(indexRow, 8, 17, report);
                setWidthColumn(indexRow, 9, 18, report);



                #region "Head"
                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue($"Отчёт по дополнительным оплатам за {monthNames[date.Month - 1]} {date.Year}", indexRow, 1);
                report.SetFontBold(indexRow, 1, indexRow, 1);
                report.SetFontSize(indexRow, 1, indexRow, 1, 16);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
                indexRow++;
                indexRow++;


                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue("Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername, indexRow, 1);
                indexRow++;

                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue("Дата выгрузки: " + DateTime.Now.ToString(), indexRow, 1);
                indexRow++;
                indexRow++;
                #endregion


                report.AddSingleValue("№", indexRow, 1);
                report.AddSingleValue("Объект", indexRow, 2);
                report.AddSingleValue("Арендатор", indexRow, 3);
                report.AddSingleValue("№ договора", indexRow, 4);
                report.AddSingleValue("Тип оплаты", indexRow, 5);
                report.AddSingleValue("Сумма доп. оплаты", indexRow, 6);
                report.AddSingleValue("Дата выписки доп. оплаты", indexRow, 7);
                report.AddSingleValue("Показания счётчика", indexRow, 8);
                report.AddSingleValue("Оплаченная сумма", indexRow, 9);
                report.AddSingleValue("Долг", indexRow, 10);
                report.AddSingleValue("Примечание", indexRow, 11);

                report.SetFontBold(indexRow, 1, indexRow, maxColumns);
                report.SetBorders(indexRow, 1, indexRow, maxColumns);
                report.SetWrapText(indexRow, 1, indexRow, maxColumns);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                indexRow++;

                int npp = 1;
                foreach (DataRow row in dtReport.Rows)
                {
                    report.SetWrapText(indexRow, 1, indexRow, maxColumns);

                    addDataToCell(npp, indexRow, 1, report);
                    addDataToCell(row["nameObject"], indexRow, 2, report);
                    addDataToCell(row["nameTenant"], indexRow, 3, report);
                    addDataToCell(row["Agreement"], indexRow, 4, report);
                    addDataToCell(row["cName"], indexRow, 5, report);
                    addDataToCell(row["Summa"], indexRow, 6, report);

                    addDataToCell(row["DateFines"], indexRow, 7, report);
                    addDataToCell(row["MetersData"], indexRow, 8, report);
                    addDataToCell(row["sumPay"], indexRow, 9, report);
                    addDataToCell(row["resultSum"], indexRow, 10, report);
                    addDataToCell(row["Comment"], indexRow, 11, report);

                    report.SetBorders(indexRow, 1, indexRow, maxColumns);
                    report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                    report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                    indexRow++;
                    npp++;
                }

                

                addDataToCell("Итого", indexRow, 5, report);

                object objValue = dtReport.Compute("SUM(Summa)", "");
                addDataToCell(objValue, indexRow, 6, report);

                objValue = dtReport.Compute("SUM(sumPay)", "");
                addDataToCell(objValue, indexRow, 9, report);

                objValue = dtReport.Compute("SUM(resultSum)", "");
                addDataToCell(objValue, indexRow, 10, report);

                report.SetBorders(indexRow, 5, indexRow, 6);
                report.SetBorders(indexRow, 9, indexRow, 10);
                report.SetFontBold(indexRow, 1, indexRow, maxColumns);

                indexRow++;


                report.SetPageSetup(1, 9999, false);
                report.Show();


                DoOnUIThread(() =>
                {
                    blockers.RestoreControlEnabledState(this);
                    progressBar1.Visible = false;
                }, this);

                return true;
            });
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
