using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.ToExcelNew;

namespace dllPlanReportMonth
{
    public partial class frmPrint : Form
    {
        public frmPrint()
        {
            InitializeComponent();
            Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
        ExcelUnLoad rep = new ExcelUnLoad();
        DataTable dtMonthReport;
        private void btnExcel_Click(object sender, EventArgs e)
        {
            DateTime datePlan = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, 1);
            Task<DataTable> task = Config.hCntMain.getReport(datePlan);
            task.Wait();
            dtMonthReport = task.Result;
            if (dtMonthReport == null || dtMonthReport.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для отчета", "Выгрузка отчета", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // план с учетом предыдущего долга
            if (!dtMonthReport.Columns.Contains("planWithDebt"))
                dtMonthReport.Columns.Add("planWithDebt", typeof(decimal));
            foreach (DataRow dr in dtMonthReport.Rows)
            {
                dr["planWithDebt"] = (decimal)dr["sumPlan"] + (decimal)dr["debt"];
            }
            rep = new ExcelUnLoad();
            int crow = 1;
            rep.SetColumnWidth(2, 2, 2, 2, 12);
            rep.SetColumnWidth(3, 3, 3, 3, 12);
            rep.SetColumnWidth(4, 4, 4, 4, 12);
            rep.SetColumnWidth(5, 5, 5, 5, 12);
            rep.SetColumnWidth(6, 6, 6, 6, 12);
            rep.SetColumnWidth(7, 7, 7, 7, 12);
            rep.SetColumnWidth(8, 8, 8, 8, 12);
            ObjectReport(ref crow);
            task = Config.hCntMain.getAllPayments(datePlan);
            task.Wait();
            PaymentsReport(ref crow, task.Result);
            rep.SetPageSetup(1, 9999, false);
            rep.Show();
        }

        private void ObjectReport(ref int crow)
        {
            rep.AddSingleValue($"План - {Config.month[dtpDate.Value.Month]} {dtpDate.Value.Year}", crow, 1);
            rep.SetFontBold(crow, 1, crow, 1);
            crow++;
            int startRow = crow;
            rep.AddSingleValue("Объект", crow, 1);
            rep.AddSingleValue("План", crow, 2);
            rep.AddSingleValue("Оплаты плана", crow, 3);
            rep.AddSingleValue("Суммарный % оплаты", crow, 4);
            rep.AddSingleValue("Долг за предыдущий период", crow, 5);
            rep.AddSingleValue("План с учетом предыдущего долга", crow, 6);
            rep.AddSingleValue("Оплаты всего", crow, 7);
            rep.AddSingleValue("% оплаты с учетом долга", crow, 8);
            rep.SetCellAlignmentToCenter(crow, 1, crow, 8);
            rep.SetCellAlignmentToJustify(crow, 1, crow, 8);
            rep.SetFontBold(crow, 1, crow, 8);
            rep.SetWrapText(crow, 1, crow, 8);
            crow++;
            foreach (DataRow dr in dtMonthReport.Rows)
            {
                rep.AddSingleValue(dr["nameObj"].ToString(), crow, 1);// объект
                rep.AddSingleValueObject(dr["sumPlan"], crow, 2);// план
                rep.AddSingleValueObject(dr["currPayment"], crow, 3);// оплаты плана
                if (decimal.Parse(dr["sumPlan"].ToString())>0)
                    rep.AddSingleValue((Math.Round(decimal.Parse(dr["currPayment"].ToString()) *100/ decimal.Parse(dr["sumPlan"].ToString()),1)).ToString("0.0") + "%", crow, 4); //суммарный % оплаты
                rep.AddSingleValueObject(dr["debt"], crow, 5);//долг за предыдущий период
                rep.AddSingleValueObject(dr["planWithDebt"], crow, 6);//план с учетом пред. долга
                rep.AddSingleValueObject(dr["allPayments"], crow, 7);//оплаты всего
                if (decimal.Parse(dr["sumPlan"].ToString()) > 0)
                    rep.AddSingleValue((Math.Round(decimal.Parse(dr["allPayments"].ToString())*100 / decimal.Parse(dr["planWithDebt"].ToString()),1)).ToString("0.0") + "%", crow, 8); //суммарный % оплаты
                crow++;
            }
            rep.SetBorders(2, 1, crow - 1, 8);
            crow++;
        }

        private void PaymentsReport(ref int crow, DataTable dtResult)
        {
            rep.AddSingleValue($"Оплаты за {Config.month[dtpDate.Value.Month]}", crow, 1);
            rep.SetFontBold(crow, 1, crow, 1);
            crow++;
            int startRow = crow;
            rep.AddSingleValue("Дата", crow, 1);
            rep.AddSingleValue("Объект", crow, 2);
            rep.AddSingleValue("Оплата", crow, 3);
            rep.AddSingleValue("Оплата плана", crow, 4);
            rep.AddSingleValue("% оплаты", crow, 5);
            rep.AddSingleValue("Способ оплаты", crow, 6);
            rep.SetWrapText(crow, 1, crow, 6);
            rep.SetFontBold(crow, 1, crow, 6);
            rep.SetCellAlignmentToCenter(crow, 1, crow, 6);
            rep.SetCellAlignmentToJustify(crow, 1, crow, 6);
            crow++;
            //даты
            var dates = dtResult.AsEnumerable().GroupBy(r => r.Field<DateTime>("Date")).Select(s => s.Key);
            foreach (var date in dates)
            {
                rep.AddSingleValue(date.ToShortDateString(), crow, 1);
                int startDate = crow;
                //таблица по датам
                EnumerableRowCollection<DataRow> rDate = dtResult.AsEnumerable().Where(r => r.Field<DateTime>("Date") == date);
                // объекты
                var objects = rDate.GroupBy(r => r.Field<string>("nameObj")).Select(s => s.Key);
                foreach (var obj in objects)
                {
                    rep.AddSingleValue(obj, crow, 2);
                    int startObject = crow;
                    decimal plan = (decimal)dtMonthReport.AsEnumerable().Where(r => r.Field<string>("nameObj") == obj).First()["planWithDebt"];
                    //оплаты по объекту
                    EnumerableRowCollection<DataRow> rObj = rDate.Where(r => r.Field<string>("nameObj") == obj);
                    foreach (DataRow dr in rObj)
                    {
                        rep.AddSingleValueObject(dr["sumPay"], crow, 3);
                        rep.AddSingleValueObject(dr["payToPlan"], crow, 4);
                        rep.AddSingleValue(Math.Round((decimal)dr["sumPay"] *100/ plan, 1) + "%", crow, 5);
                        rep.AddSingleValue(dr["cashType"].ToString(), crow, 6);
                        crow++;
                    }
                    rep.Merge(startObject, 2, crow-1, 2);
                    rep.SetCellAlignmentToJustify(startObject, 2, startObject, 2);
                }
                rep.Merge(startDate, 1, crow-1, 1);
                rep.SetCellAlignmentToJustify(startDate, 1, startDate, 1);
            }
            rep.SetBorders(startRow, 1, crow - 1, 6);

        }
    }
}
