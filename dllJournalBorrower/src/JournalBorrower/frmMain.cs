using Nwuram.Framework.Logging;
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

namespace JournalBorrower
{
    public partial class frmMain : Form
    {
        private DataTable dtData;


        public frmMain()
        {
            InitializeComponent();

            if(Config.hCntMain==null)
                Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

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

            task = Config.hCntMain.getTypeContract(true);
            task.Wait();
            DataTable dtTypeDoc = task.Result;

            cmbTypeDoc.DisplayMember = "cName";
            cmbTypeDoc.ValueMember = "id";
            cmbTypeDoc.DataSource = dtTypeDoc;

            task = Config.hCntMain.GetAddPayment(true);
            task.Wait();
            DataTable dtTypePayment = task.Result;

            cmbTypePayment.DisplayMember = "cName";
            cmbTypePayment.ValueMember = "id";
            cmbTypePayment.DataSource = dtTypePayment;
            


            rbPayDoc_Click(null, null);
            //getData();
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


                if (col.Index == cNameBuild.Index)
                {
                    tbBuild.Location = new Point(dgvData.Location.X + width + 1, tbTenant.Location.Y);
                    tbBuild.Size = new Size(cNameBuild.Width, tbTenant.Height);
                }

                if (col.Index == cFloor.Index)
                {
                    tbFloor.Location = new Point(dgvData.Location.X + width + 1, tbTenant.Location.Y);
                    tbFloor.Size = new Size(cFloor.Width, tbTenant.Height);
                }

                if (col.Index == cSection.Index)
                {
                    tbSection.Location = new Point(dgvData.Location.X + width + 1, tbTenant.Location.Y);
                    tbSection.Size = new Size(cSection.Width, tbTenant.Height);
                }

                width += col.Width;
            }
        }

        private void rbPayDoc_Click(object sender, EventArgs e)
        {
            cSumDoc.Visible = rbPayDoc.Checked;
            cDateCloseSection.Visible = rbPayDoc.Checked;
            cNameAddPayment.Visible = !rbPayDoc.Checked;
            
            label2.Visible = cmbTypePayment.Visible = !rbPayDoc.Checked;


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

            ChangeTableForType();

            dgvData.DataSource = dtData;
            setFilter();
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
            Logging.StartFirstLevel(79);

            foreach (Control cnt in this.Controls)
            {
                if (!cnt.Visible) continue;

                if (cnt is TextBox)
                {
                    Logging.Comment($"{cnt.Tag}: {cnt.Text}");                    
                }
            }

            Logging.Comment($"Объект ID:{cmbObject.SelectedValue}; Наименование:{cmbObject.Text}");
            Logging.Comment($"Тип договора ID:{cmbTypeDoc.SelectedValue}; Наименование:{cmbTypeDoc.Text}");
            Logging.Comment($"Тип оплат ID:{cmbTypePayment.SelectedValue}; Наименование:{cmbTypePayment.Text}");
            foreach (Control cnt in this.groupBox1.Controls)
            {
                if (!cnt.Visible) continue;

                if (cnt is RadioButton)
                {
                    if ((cnt as RadioButton).Checked)
                    {
                        Logging.Comment($"Тип долгов:{cnt.Text}");
                        break;
                    }
                }
            }
            Logging.StopFirstLevel();

            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();

            int indexRow = 1;

            int maxColumns = 0;

            int indexSumPay = 9, indexSumItogSum = 10, indexSumOwe = 11;

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
                    if (col.Name.Equals("cSumPay"))
                    {
                        indexSumPay = maxColumns; setWidthColumn(indexRow, maxColumns, 18, report);
                    }
                    if (col.Name.Equals("cSumItogSum"))
                    {
                        indexSumItogSum = maxColumns;   
                        setWidthColumn(indexRow, maxColumns, 15, report);
                    }
                    if (col.Name.Equals("cSumOwe"))
                    {
                        indexSumOwe = maxColumns;
                        setWidthColumn(indexRow, maxColumns, 15, report);
                    }
                    if (col.Name.Equals("cPrcOwe")) setWidthColumn(indexRow, maxColumns, 15, report);
                    if (col.Name.Equals("cDateCloseSection")) setWidthColumn(indexRow, maxColumns, 20, report);
                    if(col.Name.Equals("cNameAddPayment")) setWidthColumn(indexRow, maxColumns, 23, report);
                    //Console.WriteLine(col.Name);
                    
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

            #region "Блок для долгов"
            indexRow++;
            var ResultGroup = dtData.DefaultView.ToTable().AsEnumerable().GroupBy(r => r.Field<int>("id_Tenant"));
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Общее кол-во должников: {ResultGroup.Count()}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Кол-во секций с долгами: {dtData.DefaultView.Count}", indexRow, 1);
            indexRow++;

            EnumerableRowCollection<DataRow> RowCollect = dtData.DefaultView.ToTable().AsEnumerable().Where(r => r.Field<decimal>(rbPayDoc.Checked ? "PrcPenny_1" : "PrcPenny_2") >= 100);
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Кол-во должников с долгом от 100% и выше: {RowCollect.Count()}", indexRow, 1);
            indexRow++;

            RowCollect = dtData.DefaultView.ToTable().AsEnumerable().Where(r => r.Field<decimal>(rbPayDoc.Checked ? "PrcPenny_1" : "PrcPenny_2") >= 50 && r.Field<decimal>(rbPayDoc.Checked ? "PrcPenny_1" : "PrcPenny_2") <= 99);
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Кол-во должников с долгом от 50 до 99%: {RowCollect.Count()}", indexRow, 1);
            indexRow++;


            RowCollect = dtData.DefaultView.ToTable().AsEnumerable().Where(r => r.Field<decimal>(rbPayDoc.Checked ? "PrcPenny_1" : "PrcPenny_2") >= 0 && r.Field<decimal>(rbPayDoc.Checked ? "PrcPenny_1" : "PrcPenny_2") <= 49);
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Кол-во должников с долгом от 0 до 49%: {RowCollect.Count()}", indexRow, 1);
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
            report.SetPrintRepeatHead(indexRow, indexRow);
            indexRow++;

            foreach (DataRowView row in dtData.DefaultView)
            {
                indexCol = 1;
                report.SetWrapText(indexRow, indexCol, indexRow, maxColumns);
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    if (col.Visible)
                    {

                        if (row[col.DataPropertyName] is DateTime)
                            report.AddSingleValue(((DateTime)row[col.DataPropertyName]).ToShortDateString(), indexRow, indexCol);
                        else
                           if (row[col.DataPropertyName] is decimal || row[col.DataPropertyName] is double)
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

            #region "Bottom"
            report.AddSingleValue($"Итого", indexRow, 1);
            report.SetBorders(indexRow, 1, indexRow, 1);
            
            object varSum = dtData.DefaultView.ToTable().Compute($"SUM({(rbPayDoc.Checked ? "SummaPaymentFine_1" : "SummaPaymentFine_2")})", "");
            report.AddSingleValue($"{((decimal)varSum).ToString("0.00")}", indexRow, indexSumPay);
            
            varSum = dtData.DefaultView.ToTable().Compute($"SUM({(rbPayDoc.Checked ? "SummaFine_1" : "SummaFine_2")})", "");
            report.AddSingleValue($"{((decimal)varSum).ToString("0.00")}", indexRow, indexSumItogSum);
            
            varSum = dtData.DefaultView.ToTable().Compute($"SUM({(rbPayDoc.Checked ? "SummaPenny_1" : "SummaPenny_2")})", "");
            report.AddSingleValue($"{((decimal)varSum).ToString("0.00")}", indexRow, indexSumOwe);
            report.SetFormat(indexRow, indexSumPay, indexRow, indexSumOwe, "0.00");
            report.SetBorders(indexRow, indexSumPay, indexRow, indexSumOwe);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
            indexRow++;

            /*
            indexRow++;
            var ResultGroup = dtData.DefaultView.ToTable().AsEnumerable().GroupBy(r => r.Field<int>("id_Tenant"));
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Общее кол-во должников: {ResultGroup.Count()}", indexRow, 1);
            indexRow++;


            EnumerableRowCollection<DataRow> RowCollect = dtData.DefaultView.ToTable().AsEnumerable().Where(r => r.Field<decimal>(rbPayDoc.Checked ? "PrcPenny_1" : "PrcPenny_2") >= 100);
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Кол-во должников с долгом от 100% и выше: {RowCollect.Count()}", indexRow, 1);
            indexRow++;

            RowCollect = dtData.DefaultView.ToTable().AsEnumerable().Where(r => r.Field<decimal>(rbPayDoc.Checked ? "PrcPenny_1" : "PrcPenny_2") >= 50 && r.Field<decimal>(rbPayDoc.Checked ? "PrcPenny_1" : "PrcPenny_2") <= 99);
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Кол-во должников с долгом от 50 до 99%: {RowCollect.Count()}", indexRow, 1);
            indexRow++;


            RowCollect = dtData.DefaultView.ToTable().AsEnumerable().Where(r => r.Field<decimal>(rbPayDoc.Checked ? "PrcPenny_1" : "PrcPenny_2") >= 0 && r.Field<decimal>(rbPayDoc.Checked ? "PrcPenny_1" : "PrcPenny_2") <= 49);
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Кол-во должников с долгом от 0 до 49%: {RowCollect.Count()}", indexRow, 1);
            indexRow++;
            */
            #endregion


            report.SetPageSetup(1, 9999, true);

            /*
            var groupByPost = dtData.DefaultView.ToTable().AsEnumerable().GroupBy(r => new { id = r.Field<int>("id_Tenant") })
                .Select(s => new { s.Key.id });

            foreach (var gPost in groupByPost)
            {
                EnumerableRowCollection<DataRow> rowCollect = dtData.DefaultView.ToTable().AsEnumerable().Where(r => r.Field<int>("id_Tenant") == gPost.id);
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
            */

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

        Nwuram.Framework.UI.Service.EnableControlsServiceInProg fBlocker = new Nwuram.Framework.UI.Service.EnableControlsServiceInProg();
        Nwuram.Framework.UI.Forms.frmLoad fWait;
        private DataTable DateTableType1, DateTableType2;
        private void getData()
        {
            int id_object = (int)cmbObject.SelectedValue;
            new Task(() =>
            {

                Config.DoOnUIThread(() =>
                {
                    fBlocker.SaveControlsEnabledState(this);
                    fBlocker.SetControlsEnabled(this, false);
                    fWait = new Nwuram.Framework.UI.Forms.frmLoad();
                    fWait.TopMost = false;
                    fWait.Owner = this;
                    fWait.TextWait = "Загружаю данные из базы!";
                    fWait.Show();
                    dgvData.DataSource = null;
                }, this);



                Task<DataTable> task = Config.hCntMain.GetListOwe(id_object);
                task.Wait();
                dtData = task.Result;

                if (dtData == null || dtData.Rows.Count == 0)
                {
                    Config.DoOnUIThread(() =>
                    {
                        dgvData.DataSource = null;
                        fWait.Dispose();
                        fBlocker.RestoreControlEnabledState(this);
                    }, this); return;
                }

                dtData.Columns.Add("SummaPaymentFine_1", typeof(decimal));
                dtData.Columns.Add("SummaFine_1", typeof(decimal));
                dtData.Columns.Add("SummaPenny_1", typeof(decimal));
                dtData.Columns.Add("PrcPenny_1", typeof(decimal));
                dtData.Columns.Add("SummaPaymentFine_1_filter", typeof(decimal));

                dtData.Columns.Add("SummaPaymentFine_2", typeof(decimal));
                dtData.Columns.Add("SummaFine_2", typeof(decimal));
                dtData.Columns.Add("SummaPenny_2", typeof(decimal));
                dtData.Columns.Add("PrcPenny_2", typeof(decimal));

                task = Config.hCntMain.GetListOweAdditionalData(1, id_object);
                task.Wait();

                if (task.Result != null && task.Result.Rows.Count > 0)
                {
                    initDateType1(task.Result);                    
                }
                DateTableType1 = dtData.Copy();
                dtData.Clear();


                task = Config.hCntMain.GetListOweAdditionalData(2, id_object);
                task.Wait();
                if (task.Result != null && task.Result.Rows.Count > 0)
                {
                    EnumerableRowCollection<DataRow> rowsCOl = task.Result.AsEnumerable().Where(r => r.Field<decimal>("PrcPenny_2") > 0);
                    if(rowsCOl.Count()>0)
                        DateTableType2 = rowsCOl.CopyToDataTable();

                    //DateTableType2  = task.Result.AsEnumerable().Where(r => r.Field<decimal>("PrcPenny_2") > 0).CopyToDataTable();                   


                    //foreach (DataRow row in task.Result.Rows)
                    //{
                    //    EnumerableRowCollection<DataRow> rowCollect = dtData.AsEnumerable().Where(r => r.Field<int>("id") == (int)row["id"]);
                    //    if (rowCollect.Count() > 0)
                    //    {
                    //        rowCollect.First()["SummaPaymentFine_2"] = row["SummaPaymentFine"];
                    //        rowCollect.First()["SummaFine_2"] = row["SummaFine"];
                    //        rowCollect.First()["SummaPenny_2"] = row["SummaPenny"];
                    //        rowCollect.First()["PrcPenny_2"] = Math.Round((decimal)row["PrcPenny"], 0);
                    //    }
                    //}
                }
                Config.DoOnUIThread(() =>
                {
                    ChangeTableForType();
                    fWait.Dispose();
                    fBlocker.RestoreControlEnabledState(this);

                    setFilter();
                    dgvData.DataSource = dtData;
                }, this);
            }).Start();
        }

        private void ChangeTableForType()
        {
            if (rbPayDoc.Checked)
            {
                if (DateTableType1 == null) dtData = null; else
                 dtData = DateTableType1.Copy();
            }
            else
            {
                if (DateTableType2 == null) dtData = null;
                else
                    dtData = DateTableType2.Copy();
            }

            if (dtData != null)
                dtData.AcceptChanges();
        }

        Dictionary<int, DataTable> dicPayMonth = new Dictionary<int, DataTable>();

        private void initDateType1(DataTable dtTmpData)
        {
            try
            {
                dicPayMonth = new Dictionary<int, DataTable>();
                DataTable dtResultPay = new DataTable();
                dtResultPay.Columns.Add("id_Agreements", typeof(int));
                dtResultPay.Columns.Add("date", typeof(DateTime));
                dtResultPay.Columns.Add("sumOwe", typeof(decimal));
                dtResultPay.Columns.Add("sumPay", typeof(decimal));
                dtResultPay.Columns.Add("sumResult", typeof(decimal));
                dtResultPay.AcceptChanges();

                var groupIdAgreements = dtTmpData.AsEnumerable()
                        .GroupBy(r => new { id_Agreements = r.Field<int>("id_Agreements") })
                        .Select(s => new
                        {
                            s.Key.id_Agreements
                        });


                int maxCount = groupIdAgreements.Count();
                int cnt = 1;

                foreach (var gIdAgreements in groupIdAgreements)
                {

                    //if (gIdAgreements.id_Agreements == 2165)
                    //{ 
                    
                    //}

                    int prc = (cnt * 100) / maxCount;

                    Config.DoOnUIThread(() =>
                    {
                        fWait.TextWait = $"Идёт формирование данных: {prc} из 100%";
                    }, this);


                    dtResultPay.Clear();
                    EnumerableRowCollection<DataRow> rowCollect = dtData.AsEnumerable()
                        .Where(r => r.Field<int>("id") == gIdAgreements.id_Agreements);

                    if (rowCollect.Count() > 0)
                    {
                        DateTime dStart = (DateTime)rowCollect.First()["Start_Date"];
                        DateTime dStop = (DateTime)rowCollect.First()["Stop_Date"];
                        decimal Total_Sum = (decimal)rowCollect.First()["Total_Sum"];
                        decimal Cost_of_Meter = (decimal)rowCollect.First()["Cost_of_Meter"];
                        decimal Phone = 0;
                        if (dtData.Columns.Contains("Phone"))
                            Phone = (decimal)rowCollect.First()["Phone"];

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
                                int _id_TypeDiscount = (int)rowCollect.First()["id_TypeDiscount"];
                                decimal _tmpDec = Total_Sum;

                                EnumerableRowCollection<DataRow> rows = rowCollect.Where(r => r.Field<object>("DateEnd") != null && r.Field<int>("id_TypeDiscount") == 2);
                                if (rows.Count() > 0)
                                {
                                    _tmpDec = (decimal)rows.First()["Discount"];
                                    _tmpDec = _tmpDec * (decimal)rows.First()["Total_Area"]+ Phone;
                                }
                                else
                                {
                                    rows = rowCollect.Where(r => r.Field<object>("DateEnd") == null && r.Field<int>("id_TypeDiscount") == 2);
                                    if (rows.Count() > 0)
                                    {
                                        _tmpDec = (decimal)rows.First()["Discount"];
                                        _tmpDec = _tmpDec * (decimal)rows.First()["Total_Area"]+ Phone;
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



                        Task<DataTable> task = Config.hCntMain.GetPaymentsForAgreemetns(gIdAgreements.id_Agreements);
                        task.Wait();

                        decimal pay = 0;

                        if (task.Result != null && task.Result.Rows.Count > 0)
                        {
                            foreach (DataRow row in task.Result.Rows)
                            {
                                pay += (decimal)row["Summa"];
                            }
                        }


                        for (DateTime dI = dStart.Date; dI.Date <= _dateStop.Date; dI = dI.AddMonths(1))
                        {
                            DateTime useDate = new DateTime(dI.Year, dI.Month, 1);

                            IEnumerable<DateTime> rowDates = dicDate.Keys.AsEnumerable().Where(r => r.Month == dI.Month && r.Year == dI.Year);
                            decimal sumMonth = 0;
                            foreach (DateTime tt in rowDates)
                            {
                                sumMonth += dicDate[tt.Date];

                            }

                            sumMonth = Math.Round(sumMonth, 0);

                            if (pay == 0)
                            {
                                dtResultPay.Rows.Add(gIdAgreements.id_Agreements, useDate, sumMonth, pay, sumMonth);
                            }
                            else
                            if (sumMonth > pay)
                            {
                                dtResultPay.Rows.Add(gIdAgreements.id_Agreements, useDate, sumMonth, pay, sumMonth - pay);
                                pay = 0;
                            }
                            else
                            {
                                dtResultPay.Rows.Add(gIdAgreements.id_Agreements, useDate, sumMonth, sumMonth, 0);
                                pay = pay - sumMonth;
                            }
                        }

                        var gSumItog = dtResultPay.AsEnumerable()
                            .Where(r => r.Field<decimal>("sumResult") != 0)
                            .GroupBy(r => new { id_Agreements = r.Field<int>("id_Agreements") })
                            .Select(s => new
                            {
                                s.Key.id_Agreements,
                                sumOwe = Math.Round(s.Sum(r => r.Field<decimal>("sumOwe")),0),
                                sumPay = s.Sum(r => r.Field<decimal>("sumPay"))
                            });

                        if (gSumItog.Count() == 0)
                        {
                            EnumerableRowCollection<DataRow> rowMainCollect = dtData.AsEnumerable()
                                        .Where(r => r.Field<int>("id") == gIdAgreements.id_Agreements);
                            if (rowMainCollect.Count() > 0)
                            {
                                rowMainCollect.First()["SummaPaymentFine_1"] = ((decimal)0);
                                rowMainCollect.First()["SummaFine_1"] = ((decimal)0);
                                rowMainCollect.First()["SummaPenny_1"] = ((decimal)0);
                                rowMainCollect.First()["PrcPenny_1"] = ((decimal)0);
                                rowMainCollect.First()["SummaPaymentFine_1_filter"] = ((decimal)0);
                            }
                        }
                        else
                        {
                            foreach (var gItog in gSumItog)
                            {
                                EnumerableRowCollection<DataRow> rowMainCollect = dtData.AsEnumerable()
                                    .Where(r => r.Field<int>("id") == gItog.id_Agreements);
                                if (rowMainCollect.Count() > 0)
                                {
                                    if (gItog.sumOwe == 0)
                                    {
                                        rowMainCollect.First()["SummaPaymentFine_1"] = gItog.sumOwe;
                                        rowMainCollect.First()["SummaFine_1"] = gItog.sumPay;
                                        rowMainCollect.First()["SummaPenny_1"] = gItog.sumOwe;
                                        rowMainCollect.First()["PrcPenny_1"] = gItog.sumOwe;
                                        rowMainCollect.First()["SummaPaymentFine_1_filter"] = gItog.sumOwe;
                                    }
                                    else
                                    {
                                        rowMainCollect.First()["SummaPaymentFine_1"] = gItog.sumOwe;
                                        rowMainCollect.First()["SummaFine_1"] = gItog.sumPay;
                                        rowMainCollect.First()["SummaPenny_1"] = (gItog.sumOwe - gItog.sumPay);
                                        //rowMainCollect.First()["PrcPenny_1"] = Math.Round(((gItog.sumOwe - gItog.sumPay) * 100 / gItog.sumOwe), 0);
                                        rowMainCollect.First()["PrcPenny_1"] = Math.Round(((gItog.sumOwe - gItog.sumPay) * 100 / Total_Sum), 0);
                                        

                                        rowMainCollect.First()["SummaPaymentFine_1_filter"] = gItog.sumOwe;
                                    }
                                }
                            }
                        }
                    }
                    else
                    { 
                    
                    }
                    dicPayMonth.Add(gIdAgreements.id_Agreements, dtResultPay.Copy());
                    cnt++;
                }


                //var gSumData = dtData.AsEnumerable()
                //            .Where(r => r.Field<object>("SummaPenny_1") != null && r.Field<decimal>("SummaPenny_1") != 0)
                //           .GroupBy(r => new { id_Tenant = r.Field<int>("id_Tenant") })
                //           .Select(s => new
                //           {
                //               s.Key.id_Tenant,
                //               SummaPaymentFine_1 = s.Sum(r => r.Field<object>("SummaPaymentFine_1") != null ? r.Field<decimal>("SummaPaymentFine_1") : 0),
                //               SummaFine_1 = s.Sum(r => r.Field<object>("SummaFine_1") != null ? r.Field<decimal>("SummaFine_1") : 0),
                //               SummaPenny_1 = s.Sum(r => r.Field<object>("SummaPenny_1") != null ? r.Field<decimal>("SummaPenny_1") : 0),
                //               Total_Sum = s.Sum(r => r.Field<object>("Total_Sum") != null ? r.Field<decimal>("Total_Sum") : 0)
                //           });

                //if (gSumData.Count() > 0)
                //{
                //    foreach (var gSD in gSumData)
                //    {
                //        EnumerableRowCollection<DataRow> rowCollect = dtData.AsEnumerable()
                //            .Where(r => r.Field<int>("id_Tenant") == gSD.id_Tenant);

                //        if (rowCollect.Count() > 0)
                //        {
                //            foreach (DataRow row in rowCollect)
                //            {

                //                row["SummaPaymentFine_1"] = gSD.SummaPaymentFine_1;
                //                row["SummaFine_1"] = gSD.SummaFine_1;
                //                row["SummaPenny_1"] = gSD.SummaPenny_1;
                //                if (gSD.SummaPenny_1 == 0 || gSD.SummaPaymentFine_1 == 0)
                //                    row["PrcPenny_1"] = 0;
                //                else
                //                    row["PrcPenny_1"] = Math.Round(((gSD.SummaPenny_1) / gSD.SummaPaymentFine_1) * 100, 0);

                //                if (gSD.SummaPenny_1 > gSD.Total_Sum) row["PrcPenny_1"] = 100;

                //            }
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            { 
            
            }
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

                //if (tbPlace.Text.Trim().Length != 0)
                //    filter += (filter.Length == 0 ? "" : " and ") + $"namePlace like '%{tbPlace.Text.Trim()}%'";

                if ((int)cmbObject.SelectedValue !=0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_ObjectLease = {cmbObject.SelectedValue}";

                if ((int)cmbTypeDoc.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_TypeContract = {cmbTypeDoc.SelectedValue}";

                if (tbBuild.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"buildName like '%{tbBuild.Text.Trim()}%'";

                if (tbFloor.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"floorName like '%{tbFloor.Text.Trim()}%'";

                if (tbSection.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"sectionName like '%{tbSection.Text.Trim()}%'";

                //if (!chbNotActive.Checked)
                //    filter += (filter.Length == 0 ? "" : " and ") + $"isActive = 1";

                if (rbPayDopDoc.Checked)
                {
                    if ((int)cmbTypePayment.SelectedValue != 0)
                        filter += (filter.Length == 0 ? "" : " and ") + $"id_АddPayment = {cmbTypePayment.SelectedValue}";
                }

                if (rbPayDoc.Checked)
                    filter += (filter.Length == 0 ? "" : " and ") + $"SummaPaymentFine_1_filter <> 0"; 
                else if(rbPayDopDoc.Checked)
                    filter += (filter.Length == 0 ? "" : " and ") + $"SummaFine_2 is not null"; 

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

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            if (e.RowIndex < 1 || e.ColumnIndex < 0)
                return;

            if (!new List<int>() { nameTenant.Index, cSumPay.Index, cSumItogSum.Index, cSumOwe.Index, cPrcOwe.Index }.Contains(e.ColumnIndex))
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
            DataGridViewCell cell1 = dgvData[column, row];
            DataGridViewCell cell2 = dgvData[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }

            int id = (int)dtData.DefaultView[row]["id_Tenant"];
            int id_pre = (int)dtData.DefaultView[row - 1]["id_Tenant"];

            return cell1.Value.ToString() == cell2.Value.ToString() && id == id_pre;
        }

        private void dgvData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0)
                return;


            if (!new List<int>() { nameTenant.Index, cSumPay.Index, cSumItogSum.Index, cSumOwe.Index, cPrcOwe.Index }.Contains(e.ColumnIndex))
            { return; }

            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        private void dgvData_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (rbPayDoc.Checked && e.RowIndex != -1 && e.Button==MouseButtons.Left)
            {
                int id = (int)dtData.DefaultView[e.RowIndex]["id"];
                if (dicPayMonth.ContainsKey(id))
                    new frmView() { dt = dicPayMonth[id] }.ShowDialog();
            }
        }

        private void cmbTypePayment_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void dgvData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (e.ColumnIndex != cSumItogSum.Index) return;
            if (e.Button == MouseButtons.Right)
            {
                int id = (int)dtData.DefaultView[e.RowIndex]["id"];
                new frmListPaymentContract() { id_Agreements = id }.ShowDialog();
            }
        }
    }
}
