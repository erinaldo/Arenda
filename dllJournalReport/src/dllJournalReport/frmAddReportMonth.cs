using Nwuram.Framework.Settings.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dllJournalReport
{
    public partial class frmAddReportMonth : Form
    {
        private DataTable dtData;
        private bool isChangeValue = false;
        private DateTime _dateStart;

        public int id { set; private get; }
        public DataRowView row { set; private get; }
        public bool isView { set; private get; }
        public bool isAcceptData { private set; get; }

        public frmAddReportMonth()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
            ToolTip tp = new ToolTip();
            tp.SetToolTip(btExit, "Выход");
            tp.SetToolTip(btPrint, "Печать");            
            tp.SetToolTip(btAcceptD, "Подтвердить");

            tp.SetToolTip(btCalcData, "Расчитать");
            tp.SetToolTip(btClear, "Очистить данные расчёта");
            tp.SetToolTip(btSave, "Сохранить");
        }

        private void frmAddReportMonth_Load(object sender, EventArgs e)
        {
            btAcceptD.Visible = new List<string> { "СБ6" }.Contains(UserSettings.User.StatusCode) && isView;
            btCalcData.Visible = new List<string> { "РКВ" }.Contains(UserSettings.User.StatusCode) && !isView;
            btClear.Visible = new List<string> { "РКВ" }.Contains(UserSettings.User.StatusCode) && !isView;
            btSave.Visible = new List<string> { "РКВ" }.Contains(UserSettings.User.StatusCode) && !isView;

            Task<DataTable> task = Config.hCntMain.getObjectLease(false);
            task.Wait();
            DataTable dtObjectLease = task.Result;

            cmbObject.DisplayMember = "cName";
            cmbObject.ValueMember = "id";
            cmbObject.DataSource = dtObjectLease;
            cmbObject.SelectedIndex = -1;

            task = Config.hCntMain.getTypeContract(true);
            task.Wait();
            DataTable dtTypeContract = task.Result;

            cmbTypeContract.DisplayMember = "cName";
            cmbTypeContract.ValueMember = "id";
            cmbTypeContract.DataSource = dtTypeContract;

            if (id != 0)
                getdata();
            else
            {             
                dtpStart.MinDate = DateTime.Now.AddMonths(-2);
                dtpStart.MaxDate = DateTime.Now.AddMonths(2);
            }

            _dateStart = dtpStart.Value.Date;
            isChangeValue = false;
            isAcceptData = false;
        }


        Nwuram.Framework.UI.Service.EnableControlsServiceInProg fBlocker = new Nwuram.Framework.UI.Service.EnableControlsServiceInProg();
        Nwuram.Framework.UI.Forms.frmLoad fWait;

        private async Task initDateType1()
        {
            DateTime _startDate = new DateTime();
            int id_objectLeaser = 0;
            Config.DoOnUIThread(() =>
            {
                _startDate = new DateTime(dtpStart.Value.Year, dtpStart.Value.Month, 1);
                id_objectLeaser = (int)cmbObject.SelectedValue;

                fBlocker.SaveControlsEnabledState(this);
                fBlocker.SetControlsEnabled(this, false);
                fWait = new Nwuram.Framework.UI.Forms.frmLoad();
                fWait.TextWait = "Загружаю данные из базы!";
                fWait.TopMost = false;
                fWait.Owner = this;
                fWait.Show();

            }, this);

            //Thread.Sleep(5000);

            Task<DataTable> task = Config.hCntMain.getMonthReport(_startDate.Date, id_objectLeaser);
            task.Wait();

            if (task.Result == null || task.Result.Rows.Count == 0)
            {
                Config.DoOnUIThread(() =>
                {
                    dgvData.DataSource = null;
                    fWait.Dispose();
                    fBlocker.RestoreControlEnabledState(this);
                }, this); return;
            }

            dtData = task.Result;

            if (!dtData.Columns.Contains("discount"))
                dtData.Columns.Add("discount", typeof(decimal));

            if (!dtData.Columns.Contains("plane"))
                dtData.Columns.Add("plane", typeof(decimal));

            DataTable dtResultPay = new DataTable();
            dtResultPay.Columns.Add("id_Agreements", typeof(int));
            dtResultPay.Columns.Add("date", typeof(DateTime));
            dtResultPay.Columns.Add("sumOwe", typeof(decimal));
            dtResultPay.Columns.Add("sumPay", typeof(decimal));
            dtResultPay.Columns.Add("sumResult", typeof(decimal));
            dtResultPay.AcceptChanges();

            int maxCount = dtData.Rows.Count;
            int cnt = 1;

            foreach (DataRow row in dtData.Rows)
            {

                int prc = (cnt * 100) / maxCount;

                Config.DoOnUIThread(() =>
                {
                    fWait.TextWait = $"Идёт формирование данных: {prc} из 100%";
                }, this);

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

                DateTime _dateStop = _startDate.AddMonths(1).AddDays(-1);

                if (dStart.Date < _startDate.Date) dStart = _startDate.Date;
                if (dStop.Date < _dateStop.Date) _dateStop = dStop.Date;

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
                        int _id_TypeDiscount = (int)rowCollect.First()["id_TypeDiscount"];


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

                        if (_id_TypeDiscount != 2)
                        {
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
                row["timeLimit"] = $"{((DateTime)row["Start_Date"]).ToShortDateString()} - {((DateTime)row["Stop_Date"]).ToShortDateString()}";
            }


            Config.DoOnUIThread(() =>
            {
                fWait.Dispose();
                fBlocker.RestoreControlEnabledState(this);

                setFilter();
                dgvData.DataSource = dtData;
                isChangeValue = true;
                statusElements(false);
            }, this);
        }

        private void getdata()
        {
            btClear.Visible = false;
            cmbObject.SelectedValue = row["id_ObjectLease"];
            dtpStart.MinDate = (DateTime)row["PeriodMonthPlan"];
            dtpStart.MaxDate = (DateTime)row["PeriodMonthPlan"];
            dtpStart.Value = (DateTime)row["PeriodMonthPlan"];

            btAcceptD.Visible = new List<string> { "СБ6" }.Contains(UserSettings.User.StatusCode) && isView && !(bool)row["isСonfirmed"];


            DateTime _startDate = new DateTime(dtpStart.Value.Year, dtpStart.Value.Month, 1);
            int id_objectLeaser = (int)cmbObject.SelectedValue;

            Task<DataTable> task = Config.hCntMain.getMonthReport(_startDate.Date, id_objectLeaser,id);
            task.Wait();

            if (task.Result == null || task.Result.Rows.Count == 0)
            {
                dgvData.DataSource = null;
                return;
            }

            dtData = task.Result;


            foreach (DataRow row in dtData.Rows)
            {
                row["timeLimit"] = $"{((DateTime)row["Start_Date"]).ToShortDateString()} - {((DateTime)row["Stop_Date"]).ToShortDateString()}";
            }

            setFilter();
            dgvData.DataSource = dtData;
            statusElements(false);
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

                if (tbLandLord.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"nameLandLord like '%{tbLandLord.Text.Trim()}%'";

                if (tbTenant.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"nameTenant like '%{tbTenant.Text.Trim()}%'";

                if (tbAgreements.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"Agreement like '%{tbAgreements.Text.Trim()}%'";

                if ((int)cmbTypeContract.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_TypeContract  = {cmbTypeContract.SelectedValue}";

                dtData.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally
            {
                calcSumPlane();
                btPrint.Enabled = dtData.DefaultView.Count != 0;
            }
        }

        private void calcSumPlane()
        {
            if (dtData == null || dtData.Rows.Count == 0 || dtData.DefaultView.Count==0)
            {
                tbSumPlane.Text = "0,00";
                return;
            }

            object objSum = dtData.DefaultView.ToTable().Compute("SUM(plane)", "");
            tbSumPlane.Text = ((decimal)objSum).ToString("0.00");

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

                if (col.Index == cPlane.Index)
                {
                    tbSumPlane.Location = new Point(dgvData.Location.X + width , tbSumPlane.Location.Y);
                    tbSumPlane.Size = new Size(cPlane.Width, tbSumPlane.Height);
                    lSumPlane.Location = new Point(tbSumPlane.Location.X - 40, lSumPlane.Location.Y);
                }
                

                width += col.Width;
            }
        }

        private void chkHideDocColumnts_Click(object sender, EventArgs e)
        {
            cBuild.Visible = cFloor.Visible = cSection.Visible = cSquart.Visible = Cost_of_Meter.Visible = !chkHideDocColumnts.Checked;
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            DataTable dtResult;
            DateTime _startDate = new DateTime(dtpStart.Value.Year, dtpStart.Value.Month, 1);
            int _id = id;


            Task<DataTable> task = Config.hCntMain.getTMonthReport(_startDate.Date, _startDate.Date, (int)cmbObject.SelectedValue);
            task.Wait();
            if (task.Result != null && task.Result.Rows.Count > 0)
            {
                 _id = (int)task.Result.Rows[0]["id"];

                MyMessageBox.MyMessageBox mmb = new MyMessageBox.MyMessageBox($"За выбранный период для объекта:\n\"{cmbObject.Text}\"\nуже создан ежемесячный план на {dtpStart.Text}.\nПерезапись существующий план?", "", MyMessageBox.MessageBoxButtons.YesNoCancel, new List<string> { "Да", "Нет", "Отмена" });
                DialogResult dlgResult = mmb.ShowDialog();
                if (dlgResult == DialogResult.Cancel) return;
                //if (dlgResult == DialogResult.No) { dgvData.DataSource = null; dtData.Clear();setFilter(); return; }
                if (dlgResult == DialogResult.No) { dgvData.DataSource = null; dtData.Clear(); setFilter(); statusElements(true); isChangeValue = false; return; }
                if (dlgResult == DialogResult.Yes) {

                    task = Config.hCntMain.setTMonthPlan(_id, _startDate.Date, (int)cmbObject.SelectedValue, false, true, 0);
                    task.Wait();

                    dtResult = task.Result;

                    if (dtResult == null || dtResult.Rows.Count == 0)
                    {
                        MessageBox.Show("Не удалось сохранить данные", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if ((int)dtResult.Rows[0]["id"] == -2)
                    {
                        MessageBox.Show(Config.centralText("Данный план подтверждён.\nПерезапись невозможна!\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if ((int)dtResult.Rows[0]["id"] == -9999)
                    {
                        MessageBox.Show("Ошибка выполнения процедуры", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    task = Config.hCntMain.setTMonthPlan(_id, _startDate.Date, (int)cmbObject.SelectedValue, false, true, 1);
                    task.Wait();
                }
            }
            


            task = Config.hCntMain.setTMonthPlan(id, _startDate.Date, (int)cmbObject.SelectedValue, false, false, 0);
            task.Wait();

            dtResult = task.Result;

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

            id = (int)dtResult.Rows[0]["id"];

            foreach (DataRow row in dtData.Rows)
            {
                task = Config.hCntMain.setMonthPlan(id
                    , (int)row["id"]
                    , (decimal)row["Total_Sum"]
                    , (decimal)row["discount"]
                    , (decimal)row["plane"]
                    , false);


                task.Wait();

                dtResult = task.Result;

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
            }

            MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            isChangeValue = false;
            this.DialogResult = DialogResult.OK;
        }

        private void btCalcData_Click(object sender, EventArgs e)
        {
            if (cmbObject.SelectedValue == null)
            {
                MessageBox.Show($"Необходимо выбрать: \"{label3.Text}\"!", "Расчёт данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbObject.Focus();
                return;
            }


            if (id == 0)
            {
                DateTime _startDate = new DateTime(dtpStart.Value.Year, dtpStart.Value.Month, 1);
                Task<DataTable> task = Config.hCntMain.getTMonthReport(_startDate.Date, _startDate.Date, (int)cmbObject.SelectedValue);
                task.Wait();
                if (task.Result != null && task.Result.Rows.Count > 0)
                {
                    MessageBox.Show(Config.centralText($"За выбранный период для объекта:\n{cmbObject.Text}\nуже присутствует ежемесячный план.\n" +
                        $"Расчёт не возможен!\n"), "Расчёт данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            new Task(() => initDateType1()).Start();

        }

        private void btClear_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = null;
            if (dtData != null)
                dtData.Clear();
            isChangeValue = false;
            statusElements(true);                
        }

        private void statusElements(bool isEnable)
        {
            cmbObject.Enabled = isEnable;
            dtpStart.Enabled = isEnable;
            btSave.Enabled = isChangeValue;
        }

        private void cmbTypeContract_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void tbTenant_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            string status = "Не подтверждена";

            if (row != null)
                status = (bool)row["isСonfirmed"] ? "Подтверждена" : "Не подтверждена";

            reports.createReport(dtData, cmbObject.Text, status, dtpStart.Value.Date);
        }

        private void btAcceptD_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show(Config.centralText("Вы хотите подтвердить\nежемесячный план?\n"), "Подтверждение плана", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) return;

            Task<DataTable> task = Config.hCntMain.setTMonthPlan(id, dtpStart.Value.Date, (int)cmbObject.SelectedValue, true, false, 0);
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
            btAcceptD.Visible = false;
            isAcceptData = true;
        }

        private void frmAddReportMonth_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = isChangeValue && DialogResult.No == MessageBox.Show("На форме есть не сохранённые данные.\nЗакрыть форму без сохранения данных?\n", "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }
    }
}
