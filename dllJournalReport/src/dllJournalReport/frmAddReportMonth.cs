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
    public partial class frmAddReportMonth : Form
    {
        private DataTable dtData;
        private bool isChangeValue = false;
        private DateTime _dateStart;


        public frmAddReportMonth()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
            ToolTip tp = new ToolTip();
            tp.SetToolTip(btExit, "Выход");
            tp.SetToolTip(btPrint, "Печать");            
            tp.SetToolTip(btAcceptD, "Подтвердить");
        }

        private void frmAddReportMonth_Load(object sender, EventArgs e)
        {
            //dtpStart.Value = DateTime.Now.AddMonths(-1);            

            btAcceptD.Visible = new List<string> { "Д" }.Contains(UserSettings.User.StatusCode);

            Task<DataTable> task = Config.hCntMain.getObjectLease(true);
            task.Wait();
            DataTable dtObjectLease = task.Result;

            cmbObject.DisplayMember = "cName";
            cmbObject.ValueMember = "id";
            cmbObject.DataSource = dtObjectLease;

            _dateStart = dtpStart.Value.Date;            

            isChangeValue = false;            
        }

        private void initDateType1()
        {
            DateTime _startDate = new DateTime(dtpStart.Value.Year, dtpStart.Value.Month, 1);

            Task<DataTable> task = Config.hCntMain.getMonthReport(_startDate.Date);
            task.Wait();

            if (task.Result == null || task.Result.Rows.Count == 0)
            {
                dgvData.DataSource = null;
                return;
            }

            dtData = task.Result;

            dtData.Columns.Add("discount", typeof(decimal));
            dtData.Columns.Add("plane", typeof(decimal));

            DataTable dtResultPay = new DataTable();
            dtResultPay.Columns.Add("id_Agreements", typeof(int));
            dtResultPay.Columns.Add("date", typeof(DateTime));
            dtResultPay.Columns.Add("sumOwe", typeof(decimal));
            dtResultPay.Columns.Add("sumPay", typeof(decimal));
            dtResultPay.Columns.Add("sumResult", typeof(decimal));
            dtResultPay.AcceptChanges();

            foreach (DataRow row in dtData.Rows)
            {
                int id_Agreements = (int)row["id"];
                bool isDiscount = false;
       
                task = Config.hCntMain.getTDiscount(id_Agreements);
                task.Wait();
                //if (task.Result == null || task.Result.Rows.Count == 0)
                //{ continue; }

                DataTable dtTmp = task.Result;

                DateTime dStart = (DateTime)row["Start_Date"];
                DateTime dStop = (DateTime)row["Stop_Date"];
                decimal Total_Sum = (decimal)row["Total_Sum"];
                decimal Cost_of_Meter = (decimal)row["Cost_of_Meter"];

                //DateTime _dateStop = DateTime.Now.Day < 25 ?
                //    new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1)
                //    : new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(2).AddDays(-1);
                DateTime _dateStop = _startDate.AddMonths(1).AddDays(-1);

                if (dStart.Date < _startDate.Date) dStart = _startDate.Date;
                if (dStop.Date < _dateStop.Date)  _dateStop = dStop.Date;

                Dictionary<DateTime, decimal> dicDate = new Dictionary<DateTime, decimal>();

                for (DateTime dI = dStart.Date; dI.Date <= _dateStop.Date; dI = dI.AddDays(1))
                {
                    if (dI.Date > dStop.Date)
                        break;

                    int days = DateTime.DaysInMonth(dI.Year, dI.Month);

                    EnumerableRowCollection<DataRow> rowCollect = dtTmp.AsEnumerable()
                            .Where(r => r.Field<int>("id_StatusDiscount") == 2 &&
                             ((r.Field<DateTime>("DateStart").Date <= dI.Date && r.Field<object>("DateEnd") == null)
                             || (r.Field<DateTime>("DateStart").Date <= dI.Date && dI.Date <= r.Field<DateTime>("DateEnd").Date))
                            ).OrderByDescending(r => r.Field<DateTime>("DateStart"));

                    if (rowCollect.Count() > 0)
                    {
                        decimal _tmpDec = Total_Sum;
                        isDiscount = true;

                        EnumerableRowCollection<DataRow> rows = rowCollect.Where(r => r.Field<object>("DateEnd") != null && r.Field<int>("id_TypeDiscount") == 2);
                        if (rows.Count() > 0)
                        {
                            _tmpDec = (decimal)rows.First()["Discount"];
                            _tmpDec = _tmpDec * (decimal)row["Total_Area"];
                        }
                        else
                        {
                            rows = rowCollect.Where(r => r.Field<object>("DateEnd") == null && r.Field<int>("id_TypeDiscount") == 2);
                            if (rows.Count() > 0)
                            {
                                _tmpDec = (decimal)rows.First()["Discount"];
                                _tmpDec = _tmpDec * (decimal)row["Total_Area"];
                            }
                        }


                        rows = rowCollect.Where(r => r.Field<object>("DateEnd") != null && r.Field<int>("id_TypeDiscount") == 1);
                        if (rows.Count() > 0)
                        {
                            _tmpDec = _tmpDec - (_tmpDec * (decimal)rows.First()["Discount"]) / 100;
                        }
                        else
                        {
                            rows = rowCollect.Where(r => r.Field<object>("DateEnd") == null && r.Field<int>("id_TypeDiscount") == 1);
                            if (rows.Count() > 0)
                            {
                                _tmpDec = _tmpDec - (_tmpDec * (decimal)rows.First()["Discount"]) / 100;
                            }
                        }

                        dicDate.Add(dI.Date, _tmpDec / days);
                    }
                    else
                    {
                        dicDate.Add(dI.Date, Total_Sum / days);
                    }
                }

                IEnumerable<DateTime> rowDates = dicDate.Keys.AsEnumerable().Where(r => r.Month == _startDate.Month && r.Year == _startDate.Year);
                decimal sumMonth = 0;
                foreach (DateTime tt in rowDates)
                {
                    sumMonth += dicDate[tt.Date];

                }

                sumMonth = Math.Round(sumMonth, 2);

                row["plane"] = sumMonth;
                row["discount"] = isDiscount ? Math.Round(Total_Sum - sumMonth, 2) : 0;

            }
            dgvData.DataSource = dtData;





            /*

            var groupIdAgreements = dtTmpData.AsEnumerable()
                    .GroupBy(r => new { id_Agreements = r.Field<int>("id_Agreements") })
                    .Select(s => new
                    {
                        s.Key.id_Agreements
                    });

            foreach (var gIdAgreements in groupIdAgreements)
            {
                dtResultPay.Clear();

                EnumerableRowCollection<DataRow> rowCollect = dtData.AsEnumerable()
                    .Where(r => r.Field<int>("id") == gIdAgreements.id_Agreements);

                if (rowCollect.Count() > 0)
                {
                    DateTime dStart = (DateTime)rowCollect.First()["Start_Date"];
                    DateTime dStop = (DateTime)rowCollect.First()["Stop_Date"];
                    decimal Total_Sum = (decimal)rowCollect.First()["Total_Sum"];
                    decimal Cost_of_Meter = (decimal)rowCollect.First()["Cost_of_Meter"];

                    DateTime _dateStop = DateTime.Now.Day < 25 ?
                        new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1)
                        : new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(2).AddDays(-1);


                    Dictionary<DateTime, decimal> dicDate = new Dictionary<DateTime, decimal>();

                    for (DateTime dI = dStart.Date; dI.Date <= _dateStop.Date; dI = dI.AddDays(1))
                    {
                        if (dI.Date > dStop.Date)
                            break;

                        int days = DateTime.DaysInMonth(dI.Year, dI.Month);

                        rowCollect = dtTmpData.AsEnumerable()
                               .Where(r => r.Field<int>("id_Agreements") == gIdAgreements.id_Agreements &&
                                r.Field<object>("id_discount") != null &&
                                ((r.Field<DateTime>("DateStart").Date <= dI.Date && r.Field<object>("DateEnd") == null)
                                || (r.Field<DateTime>("DateStart").Date <= dI.Date && dI.Date <= r.Field<DateTime>("DateEnd").Date))
                               ).OrderByDescending(r => r.Field<DateTime>("DateStart"));

                        if (rowCollect.Count() > 0)
                        {
                            decimal _tmpDec = Total_Sum;

                            EnumerableRowCollection<DataRow> rows = rowCollect.Where(r => r.Field<object>("DateEnd") != null && r.Field<int>("id_TypeDiscount") == 2);
                            if (rows.Count() > 0)
                            {
                                _tmpDec = (decimal)rows.First()["Discount"];
                                _tmpDec = _tmpDec * (decimal)rows.First()["Total_Area"];
                            }
                            else
                            {
                                rows = rowCollect.Where(r => r.Field<object>("DateEnd") == null && r.Field<int>("id_TypeDiscount") == 2);
                                if (rows.Count() > 0)
                                {
                                    _tmpDec = (decimal)rows.First()["Discount"];
                                    _tmpDec = _tmpDec * (decimal)rows.First()["Total_Area"];
                                }
                            }


                            rows = rowCollect.Where(r => r.Field<object>("DateEnd") != null && r.Field<int>("id_TypeDiscount") == 1);
                            if (rows.Count() > 0)
                            {
                                _tmpDec = _tmpDec - (_tmpDec * (decimal)rows.First()["Discount"]) / 100;
                            }
                            else
                            {
                                rows = rowCollect.Where(r => r.Field<object>("DateEnd") == null && r.Field<int>("id_TypeDiscount") == 1);
                                if (rows.Count() > 0)
                                {
                                    _tmpDec = _tmpDec - (_tmpDec * (decimal)rows.First()["Discount"]) / 100;
                                }
                            }

                            dicDate.Add(dI.Date, _tmpDec / days);
                        }
                        else
                        {
                            dicDate.Add(dI.Date, Total_Sum / days);
                        }
                    }

                }
            }

    */
        }

        private void dgvData_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            int width = 0;
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                if (!col.Visible) continue;

                if (col.Index == nameLandLord.Index)
                {
                    tbLandLord.Location = new Point(dgvData.Location.X + width + 1, tbTenant.Location.Y);
                    tbLandLord.Size = new Size(nameLandLord.Width, tbTenant.Height);
                }

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

                

                width += col.Width;
            }
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            initDateType1();
        }

        private void chkHideDocColumnts_Click(object sender, EventArgs e)
        {
            cBuild.Visible = cFloor.Visible = cSection.Visible = cSquart.Visible = Cost_of_Meter.Visible = !chkHideDocColumnts.Checked;
        }
    }
}
