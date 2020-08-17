using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dllJournalPlaneReport
{
    public partial class frmViewPayment : Form
    {
        public int id_Agreements { set; private get; }
        public DateTime datePlane { set; private get; }
        public bool isData { private set; get; }
        private DataTable dtData;
        public frmViewPayment()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;            
        }

        public frmViewPayment(int id_Agreements, DateTime datePlane)
        {
            this.id_Agreements = id_Agreements;
            this.datePlane = datePlane;
            if (!getData()) return;
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
        }

        private void frmViewPayment_Load(object sender, EventArgs e)
        {
            getData();

            tbAgreements.Text = dtData.Rows[0]["nameTenant"].ToString();
            tbDate.Text = ((DateTime)dtData.Rows[0]["PeriodMonthPlan"]).ToShortDateString();
            tbObject.Text = dtData.Rows[0]["nameObject"].ToString();
            dgvData.DataSource = dtData;
        }

        private bool getData()
        {
            Task<DataTable> task = Config.hCntMain.getPlaneReportDopInfoPay(id_Agreements, datePlane);
            task.Wait();
            if (task.Result == null || task.Result.Rows.Count == 0)
            {
                MessageBox.Show("Не было оплат за текущий период!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isData = false;
                Close();
                return false;
            }
            isData = true;
            dtData = task.Result.Copy();
            return true;
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
                    if (col.Name.Equals("cDate")) setWidthColumn(indexRow, maxColumns, 20, report);
                    if (col.Name.Equals("cTypePayDoc")) setWidthColumn(indexRow, maxColumns, 22, report);
                    if (col.Name.Equals("cSumma")) setWidthColumn(indexRow, maxColumns, 22, report);
                    if (col.Name.Equals("cDatePay")) setWidthColumn(indexRow, maxColumns, 35, report);
                    if (col.Name.Equals("cSummPay")) setWidthColumn(indexRow, maxColumns, 15, report);
                    if (col.Name.Equals("cTypePay")) setWidthColumn(indexRow, maxColumns, 20, report);                    
                }

            #region "Head"
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Отчёт по прочим платежам", indexRow, 1);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            indexRow++;
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Договор: {tbAgreements.Text}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"{label3.Text}: {tbDate.Text}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"{label1.Text}: {tbObject.Text}", indexRow, 1);
            indexRow++;


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


            var groupByPost = dtData.DefaultView.ToTable().AsEnumerable()
                .GroupBy(r => new { id_fines = r.Field<int>("id_fines"), DateFines = r.Field<DateTime>("DateFines"), namePayment = r.Field<string>("namePayment"), summaFines = r.Field<decimal>("summaFines") })
                .Select(s => new { s.Key.id_fines, s.Key.DateFines, s.Key.namePayment, s.Key.summaFines });

            foreach (var gPost in groupByPost)
            {
                EnumerableRowCollection<DataRow> rowCollect = dtData.DefaultView.ToTable().AsEnumerable().Where(r => r.Field<int>("id_fines") == gPost.id_fines);
                int startMergRow = indexRow;

                foreach (DataRow row in rowCollect)
                {
                    indexCol = 1;
                    report.SetWrapText(indexRow, indexCol, indexRow, maxColumns);
                    foreach (DataGridViewColumn col in dgvData.Columns)
                    {
                        if (col.Visible)
                        {
                            if (new List<int>() { cSumma.Index,cTypePayDoc.Index, cDate.Index }.Contains(col.Index))
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
                    if (new List<int>() { cSumma.Index, cTypePayDoc.Index, cDate.Index }.Contains(col.Index))
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

        private void btExit_Click(object sender, EventArgs e)
        {
            Close(); 
        }
    }
}
