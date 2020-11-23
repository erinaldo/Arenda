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

namespace Arenda.Reports
{
    public partial class frmRepotPayTypeDates : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        private Nwuram.Framework.UI.Service.EnableControlsServiceInProg blockers = new Nwuram.Framework.UI.Service.EnableControlsServiceInProg();
        private Nwuram.Framework.ToExcelNew.ExcelUnLoad report = null;


        public frmRepotPayTypeDates()
        {
            InitializeComponent();
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStart.Value.Date > dtpEnd.Value.Date)
                dtpEnd.Value = dtpStart.Value.Date;
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStart.Value.Date > dtpEnd.Value.Date)
                dtpStart.Value = dtpEnd.Value.Date;
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Close();
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

            var result = await Task<bool>.Factory.StartNew(() =>
            {
                DataTable dtReport = _proc.GetReportPayAgreement(dtpStart.Value.Date, dtpEnd.Value.Date);


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
                int maxColumns = 9;
                bool isFirstLoad = false;

                List<int> numbers = new List<int>() { 1, 3, 2 };
                //for (int i = 1; i <= 3; i++)
                foreach(int i in numbers)
                {
                    EnumerableRowCollection<DataRow> rowCollect = dtReport.AsEnumerable().Where(r => r.Field<int>("id_PayType") == i);
                    if (rowCollect.Count() == 0) continue;


                    if (!isFirstLoad)
                    {
                        isFirstLoad = true;
                        report.changeNameTab(rowCollect.First()["namePayType"].ToString());
                    }
                    else report.GoToNextSheet(rowCollect.First()["namePayType"].ToString());
                    
                    indexRow = 1;

                    #region "Head"
                    report.Merge(indexRow, 1, indexRow, maxColumns);
                    report.AddSingleValue($"{rowCollect.First()["namePayType"]}", indexRow, 1);
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

                    setWidthColumn(indexRow, 1, 21, report);
                    setWidthColumn(indexRow, 2, 21, report);
                    setWidthColumn(indexRow, 3, 20, report);
                    setWidthColumn(indexRow, 4, 30, report);

                    report.AddSingleValue("Арендодатель", indexRow, 1);
                    report.AddSingleValue("Арендатор", indexRow, 2);
                    report.AddSingleValue("Номер договора", indexRow, 3);
                    report.AddSingleValue("Местоположение по договору", indexRow, 4);




                    #region "Обеспечительный платёж"
                    if (i == 1) {                                               
                        maxColumns = 7;                    
                        setWidthColumn(indexRow, 5, 14, report);
                        setWidthColumn(indexRow, 6, 17, report);
                        setWidthColumn(indexRow, 7, 20, report);
                                               
                        report.AddSingleValue("Дата оплаты", indexRow, 5);
                        report.AddSingleValue("Сумма оплаты", indexRow, 6);
                        report.AddSingleValue("Тип обеспечительного платежа", indexRow, 7);

                        report.SetFontBold(indexRow, 1, indexRow, maxColumns);
                        report.SetBorders(indexRow, 1, indexRow, maxColumns);
                        report.SetWrapText(indexRow, 1, indexRow, maxColumns);
                        report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                        report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                        indexRow++;

                        EnumerableRowCollection<DataRow> Rows = rowCollect.Where(r => r.Field<bool>("isCash"));
                        if (Rows.Count() >0)
                        {
                            report.Merge(indexRow, 1, indexRow, maxColumns);
                            report.AddSingleValue("Наличные", indexRow,1);
                            report.SetFontBold(indexRow, 1, indexRow, maxColumns);
                            report.SetBorders(indexRow, 1, indexRow, maxColumns);
                            report.SetWrapText(indexRow, 1, indexRow, maxColumns);
                            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                            indexRow++;

                            foreach (DataRow row in Rows)
                            {
                                report.SetWrapText(indexRow, 1, indexRow, maxColumns);

                                addDataToCell(row["nameTenant"], indexRow, 1, report);
                                addDataToCell(row["nameLandLord"], indexRow, 2, report);
                                addDataToCell(row["Agreement"], indexRow, 3, report);
                                addDataToCell(row["namePlace"], indexRow, 4, report);

                                addDataToCell(row["Date"], indexRow, 5, report);
                                addDataToCell(row["Summa"], indexRow, 6, report);
                                addDataToCell(row["nameSavePayment"], indexRow, 7, report);

                                report.SetBorders(indexRow, 1, indexRow, maxColumns);
                                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                                report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                                indexRow++;
                            }
                        }

                        Rows = rowCollect.Where(r => !r.Field<bool>("isCash"));
                        if (Rows.Count() > 0)
                        {
                            report.Merge(indexRow, 1, indexRow, maxColumns);
                            report.AddSingleValue("Безналичный расчёт", indexRow, 1);
                            report.SetFontBold(indexRow, 1, indexRow, maxColumns);
                            report.SetBorders(indexRow, 1, indexRow, maxColumns);
                            report.SetWrapText(indexRow, 1, indexRow, maxColumns);
                            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                            indexRow++;

                            foreach (DataRow row in Rows)
                            {
                                report.SetWrapText(indexRow, 1, indexRow, maxColumns);

                                addDataToCell(row["nameTenant"], indexRow, 1, report);
                                addDataToCell(row["nameLandLord"], indexRow, 2, report);
                                addDataToCell(row["Agreement"], indexRow, 3, report);
                                addDataToCell(row["namePlace"], indexRow, 4, report);

                                addDataToCell(row["Date"], indexRow, 5, report);
                                addDataToCell(row["Summa"], indexRow, 6, report);
                                addDataToCell(row["nameSavePayment"], indexRow, 7, report);

                                report.SetBorders(indexRow, 1, indexRow, maxColumns);
                                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                                report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                                indexRow++;
                            }
                        }
                    }
                    #endregion

                    #region "Оплата аренды"
                    if (i == 2) {
                        maxColumns = 8;
                        setWidthColumn(indexRow, 5, 14, report);
                        setWidthColumn(indexRow, 6, 17, report);
                        setWidthColumn(indexRow, 7, 10, report);
                        setWidthColumn(indexRow, 8, 17, report);

                        report.AddSingleValue("Дата оплаты", indexRow, 5);
                        report.AddSingleValue("Сумма оплаты", indexRow, 6);
                        report.AddSingleValue("Тип операции", indexRow, 7);
                        report.AddSingleValue("План в который попадает оплата", indexRow, 8);


                        report.SetFontBold(indexRow, 1, indexRow, maxColumns);
                        report.SetBorders(indexRow, 1, indexRow, maxColumns);
                        report.SetWrapText(indexRow, 1, indexRow, maxColumns);
                        report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                        report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                        indexRow++;

                        EnumerableRowCollection<DataRow> Rows = rowCollect.Where(r => r.Field<bool>("isCash"));
                        if (Rows.Count() > 0)
                        {
                            report.Merge(indexRow, 1, indexRow, maxColumns);
                            report.AddSingleValue("Наличные", indexRow, 1);
                            report.SetFontBold(indexRow, 1, indexRow, maxColumns);
                            report.SetBorders(indexRow, 1, indexRow, maxColumns);
                            report.SetWrapText(indexRow, 1, indexRow, maxColumns);
                            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                            indexRow++;

                            foreach (DataRow row in Rows)
                            {
                                report.SetWrapText(indexRow, 1, indexRow, maxColumns);

                                addDataToCell(row["nameTenant"], indexRow, 1, report);
                                addDataToCell(row["nameLandLord"], indexRow, 2, report);
                                addDataToCell(row["Agreement"], indexRow, 3, report);
                                addDataToCell(row["namePlace"], indexRow, 4, report);

                                addDataToCell(row["Date"], indexRow, 5, report);
                                addDataToCell(row["Summa"], indexRow, 6, report);
                                addDataToCell(row["nameToTenant"], indexRow, 7, report);
                                addDataToCell(row["PlaneDate"], indexRow, 8, report);

                                report.SetBorders(indexRow, 1, indexRow, maxColumns);
                                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                                report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                                indexRow++;
                            }
                        }

                        Rows = rowCollect.Where(r => !r.Field<bool>("isCash"));
                        if (Rows.Count() > 0)
                        {
                            report.Merge(indexRow, 1, indexRow, maxColumns);
                            report.AddSingleValue("Безналичный расчёт", indexRow, 1);
                            report.SetFontBold(indexRow, 1, indexRow, maxColumns);
                            report.SetBorders(indexRow, 1, indexRow, maxColumns);
                            report.SetWrapText(indexRow, 1, indexRow, maxColumns);
                            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                            indexRow++;

                            foreach (DataRow row in Rows)
                            {
                                report.SetWrapText(indexRow, 1, indexRow, maxColumns);

                                addDataToCell(row["nameTenant"], indexRow, 1, report);
                                addDataToCell(row["nameLandLord"], indexRow, 2, report);
                                addDataToCell(row["Agreement"], indexRow, 3, report);
                                addDataToCell(row["namePlace"], indexRow, 4, report);

                                addDataToCell(row["Date"], indexRow, 5, report);
                                addDataToCell(row["Summa"], indexRow, 6, report);
                                addDataToCell(row["nameToTenant"], indexRow, 7, report);
                                addDataToCell(row["PlaneDate"], indexRow, 8, report);

                                report.SetBorders(indexRow, 1, indexRow, maxColumns);
                                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                                report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                                indexRow++;
                            }
                        }
                    }
                    #endregion

                    #region "Дополнительная оплата"
                    if (i == 3) {
                        maxColumns = 10;
                        setWidthColumn(indexRow, 5, 17, report);
                        setWidthColumn(indexRow, 6, 13, report);
                        setWidthColumn(indexRow, 7, 12, report);
                        setWidthColumn(indexRow, 8, 12, report);
                        setWidthColumn(indexRow, 9, 15, report);
                        setWidthColumn(indexRow, 10, 13, report);


                        report.AddSingleValue("Тип доп оплаты", indexRow, 5);
                        report.AddSingleValue("Дата выписки оплаты", indexRow, 6);
                        report.AddSingleValue("Дата оплаты", indexRow, 7);
                        report.AddSingleValue("Сумма оплаты", indexRow, 8);
                        report.AddSingleValue("Тип операции", indexRow, 9);
                        report.AddSingleValue("План в который попадает оплата", indexRow, 10);


                        report.SetFontBold(indexRow, 1, indexRow, maxColumns);
                        report.SetBorders(indexRow, 1, indexRow, maxColumns);
                        report.SetWrapText(indexRow, 1, indexRow, maxColumns);
                        report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                        report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                        indexRow++;

                        EnumerableRowCollection<DataRow> Rows = rowCollect.Where(r => r.Field<bool>("isCash"));
                        if (Rows.Count() > 0)
                        {
                            report.Merge(indexRow, 1, indexRow, maxColumns);
                            report.AddSingleValue("Наличные", indexRow, 1);
                            report.SetFontBold(indexRow, 1, indexRow, maxColumns);
                            report.SetBorders(indexRow, 1, indexRow, maxColumns);
                            report.SetWrapText(indexRow, 1, indexRow, maxColumns);
                            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                            indexRow++;

                            foreach (DataRow row in Rows)
                            {
                                report.SetWrapText(indexRow, 1, indexRow, maxColumns);

                                addDataToCell(row["nameTenant"], indexRow, 1, report);
                                addDataToCell(row["nameLandLord"], indexRow, 2, report);
                                addDataToCell(row["Agreement"], indexRow, 3, report);
                                addDataToCell(row["namePlace"], indexRow, 4, report);


                                addDataToCell(row["nameAddPayment"], indexRow, 5, report);
                                addDataToCell(row["DateFines"], indexRow, 6, report);

                                addDataToCell(row["Date"], indexRow, 7, report);
                                addDataToCell(row["Summa"], indexRow, 8, report);
                                addDataToCell(row["nameToTenant"], indexRow, 9, report);
                                addDataToCell(row["PlaneDate"], indexRow, 10, report);

                                report.SetBorders(indexRow, 1, indexRow, maxColumns);
                                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                                report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                                indexRow++;
                            }
                        }

                        Rows = rowCollect.Where(r => !r.Field<bool>("isCash"));
                        if (Rows.Count() > 0)
                        {
                            report.Merge(indexRow, 1, indexRow, maxColumns);
                            report.AddSingleValue("Безналичный расчёт", indexRow, 1);
                            report.SetFontBold(indexRow, 1, indexRow, maxColumns);
                            report.SetBorders(indexRow, 1, indexRow, maxColumns);
                            report.SetWrapText(indexRow, 1, indexRow, maxColumns);
                            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                            indexRow++;

                            foreach (DataRow row in Rows)
                            {
                                report.SetWrapText(indexRow, 1, indexRow, maxColumns);

                                addDataToCell(row["nameTenant"], indexRow, 1, report);
                                addDataToCell(row["nameLandLord"], indexRow, 2, report);
                                addDataToCell(row["Agreement"], indexRow, 3, report);
                                addDataToCell(row["namePlace"], indexRow, 4, report);

                                addDataToCell(row["nameAddPayment"], indexRow, 5, report);
                                addDataToCell(row["DateFines"], indexRow, 6, report);

                                addDataToCell(row["Date"], indexRow, 7, report);
                                addDataToCell(row["Summa"], indexRow, 8, report);
                                addDataToCell(row["nameToTenant"], indexRow, 9, report);
                                addDataToCell(row["PlaneDate"], indexRow, 10, report);

                                report.SetBorders(indexRow, 1, indexRow, maxColumns);
                                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                                report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                                indexRow++;
                            }
                        }



                    }
                    #endregion

                    report.SetPageSetup(1, 9999, true);
                }
               
                report.Show();


                DoOnUIThread(() =>
                {
                    blockers.RestoreControlEnabledState(this);
                    progressBar1.Visible = false;
                }, this);

                return true;
            });
        }
    }
}
