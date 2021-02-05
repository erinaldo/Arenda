using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nwuram.Framework.ToExcelNew;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Logging;

namespace ArendaViewSection
{
    public partial class frmView : Form
    {
       

        public frmView()
        {
            InitializeComponent();
            Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        }
        DataTable dtData;
        private void DoOnUIThread(MethodInvoker d)
        {
            if (this.InvokeRequired) { this.Invoke(d); } else { d(); }
        }

        private async void init_combobox()
        {
            DoOnUIThread(delegate () { this.Enabled = false; });
            Task<DataTable> taskB = Task.Run(() =>Config.hCntMain.getBuildings());
            Task<DataTable> taskF = Task.Run(() => Config.hCntMain.getFloors());
            Task<DataTable> taskO = Task.Run(() => Config.hCntMain.getObjectsLease());
            Task.WaitAll(new[] { taskB, taskF, taskO });

            DoOnUIThread(delegate ()
            {
                cmbBuilding.DataSource = taskB.Result;              
                cmbFloor.DataSource = taskF.Result;
                cmbObject.DataSource = taskO.Result;

                cmbBuilding.DisplayMember = cmbFloor.DisplayMember = cmbObject.DisplayMember = "cName";
                cmbBuilding.ValueMember = cmbFloor.ValueMember = cmbObject.ValueMember = "id";
                
            });
            taskB.Dispose();
            taskF.Dispose();
            taskO.Dispose();
            DoOnUIThread(delegate () { this.Enabled = true; });
        }
        private void get_data()
        {
           // DoOnUIThread(delegate () { this.Enabled = false; });

            Task<DataTable> task = Config.hCntMain.getData();
            task.Wait();
            dtData = task.Result;
            foreach (DataRow dr in dtData.Rows)
            {
                if (int.Parse(dr["typeSection"].ToString()) == 0)
                {
                    dr["StartDate"] = DBNull.Value;
                    dr["EndDate"] = DBNull.Value;
                    dr["numDoc"] = DBNull.Value;
                    dr["nameArenda"] = DBNull.Value;
                    dr["agrTotalArea"] = dr["secTotalArea"];
                }
            }
            DoOnUIThread(delegate ()
            {
                dgvData.DataSource = dtData;
            });
            setFilters();

           // DoOnUIThread(delegate () { this.Enabled = true; });
        }

        private void setFilters()
        {
            DoOnUIThread(delegate ()
            {
                try
                {
                    if (dtData == null || dtData.Rows.Count == 0)
                    {
                        btnPrint.Enabled = false;
                        return;
                    }
                    string filter = "";
                    if (tbSection.Text.Trim().Length != 0)
                        filter += (filter.Length == 0 ? "" : " and ") + $"nameSection like '%{tbSection.Text}%'";
                    if (cmbBuilding.SelectedValue != null && (int)cmbBuilding.SelectedValue != 0)
                        filter += (filter.Length == 0 ? "" : " and ") + $"idBuilding = {cmbBuilding.SelectedValue}";
                    if (cmbFloor.SelectedValue != null && (int)cmbFloor.SelectedValue != 0)
                        filter += (filter.Length == 0 ? "" : " and ") + $"idFloor = {cmbFloor.SelectedValue}";
                    if (cmbObject.SelectedValue != null && (int)cmbObject.SelectedValue != 0)
                        filter += (filter.Length == 0 ? "" : " and ") + $"idObject = {cmbObject.SelectedValue}";

                    string checkedStr = "";

                    if (chbFree.Checked)
                        checkedStr += "0,";

                    if (chbClearing.Checked)
                        checkedStr += "1,";
                    if (chbBusy.Checked)
                        checkedStr += "2,";

                    if (checkedStr.Length == 0)
                        checkedStr = "-1";
                    else
                        checkedStr = checkedStr.Substring(0, checkedStr.Length - 1);

                    filter += (filter.Length == 0 ? "" : " and ") + "typeSection in (" + checkedStr + ")";

                    dtData.DefaultView.RowFilter = filter;
                }
                catch
                {
                    dtData.DefaultView.RowFilter = "idSection = -1";
                }
                enableButtons();
            });

        }
        private void enableButtons()
        {
            
            btnPrint.Enabled = dtData.DefaultView.Count != 0;
        }
        private void frmView_Load(object sender, EventArgs e)
        {
            dgvData.AutoGenerateColumns = false;
            Task.Run(() => { init_combobox(); get_data(); });
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setFilters(object sender, EventArgs e)
        {
            setFilters();
        }

        private void cmbBuilding_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilters();
        }

        private void cmbFloor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilters();
        }

        private void dgvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex!= -1 && dtData!= null && dtData.DefaultView.Count !=0)
            {
                Color rcolor = Color.White;
                if (int.Parse(dtData.DefaultView[e.RowIndex]["typeSection"].ToString()) == 1)
                    rcolor = panel2.BackColor;
                else if (int.Parse(dtData.DefaultView[e.RowIndex]["typeSection"].ToString()) == 2)
                    rcolor = panel1.BackColor;

                dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = rcolor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rcolor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;

                if (dtData.DefaultView[e.RowIndex]["EndDate"] != DBNull.Value
                    && (DateTime)dtData.DefaultView[e.RowIndex]["EndDate"] < DateTime.Now.Date)
                    dgvData.Rows[e.RowIndex].Cells["cEndArenda"].Style.BackColor =
                        dgvData.Rows[e.RowIndex].Cells["cEndArenda"].Style.SelectionBackColor = panel3.BackColor;
                if ((bool)dtData.DefaultView[e.RowIndex]["haveAgrNew"])
                    dgvData.Rows[e.RowIndex].Cells["cSection"].Style.BackColor =
                       dgvData.Rows[e.RowIndex].Cells["cSection"].Style.SelectionBackColor = panel4.BackColor;

            }
        }

        private void dgvData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            //Рисуем рамку для выделеной строки
            if (dgv.Rows[e.RowIndex].Selected)
            {
                int width = dgv.Width;
                Rectangle r = dgv.GetRowDisplayRectangle(e.RowIndex, false);
                Rectangle rect = new Rectangle(r.X, r.Y, width - 1, r.Height - 1);

                ControlPaint.DrawBorder(e.Graphics, rect,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            Logging.StartFirstLevel(79);
            Logging.Comment("Выгрузка отчета о занятости секций");

            Logging.Comment($"Объект ID:{cmbObject.SelectedValue}; Наименование:{cmbObject.Text}");
            Logging.Comment($"Здание ID:{cmbBuilding.SelectedValue}; Наименование:{cmbBuilding.Text}");
            Logging.Comment($"Этаж ID:{cmbFloor.SelectedValue}; Наименование:{cmbFloor.Text}");
            Logging.Comment($"Поиск по секции:{tbSection.Text}");

            Logging.Comment($"{chbBusy.Text}:{(chbBusy.Checked?"Да":"Нет")}");
            Logging.Comment($"{chbClearing.Text}:{(chbClearing.Checked ? "Да" : "Нет")}");
            Logging.Comment($"{chbFree.Text}:{(chbFree.Checked ? "Да" : "Нет")}");

            Logging.StopFirstLevel();

            DataTable dtReport = dtData.DefaultView.ToTable();
            ExcelUnLoad rep = new ExcelUnLoad();
            rep.SetPageOrientationToLandscape();
            int maxColumns = 8;
            int cRow = 1;

            #region колонки
            rep.SetColumnWidth(1, 1, 1, 1, 10);
            rep.SetColumnWidth(2, 2, 2, 2, 18);
            rep.SetColumnWidth(3, 3, 3, 3, 12);
            rep.SetColumnWidth(4, 4, 4, 4, 20);
            rep.SetColumnWidth(5, 5, 5, 5, 12);
            rep.SetColumnWidth(6, 6, 6, 6, 12);
            rep.SetColumnWidth(7, 7, 7, 7, 12);
            rep.SetColumnWidth(8, 8, 8, 8, 15);
            #endregion

            #region Шапка
            rep.AddSingleValue("Отчет по секциям", 1, 1);
            rep.Merge(cRow, 1, cRow, maxColumns);
            rep.SetCellAlignmentToCenter(1, 1, 1, 1);
            cRow++;
            rep.AddSingleValue("Объект", cRow, 1);
            rep.AddSingleValue(cmbObject.Text, cRow, 2);
            rep.AddSingleValue("Дата выгрузки", cRow, 3);
            rep.AddSingleValue(DateTime.Now.ToString(), cRow, 4);
            cRow++;
            rep.AddSingleValue("Здание", cRow, 1);
            rep.AddSingleValue(cmbBuilding.Text, cRow, 2);
            rep.AddSingleValue("Выгрузил", cRow, 3);
            rep.AddSingleValue(UserSettings.User.FullUsername, cRow, 4);
            cRow++;
            rep.AddSingleValue("Этаж", cRow, 1);
            rep.AddSingleValue(cmbFloor.Text, cRow, 2);

            cRow += 2;
            #endregion

            #region Шапка таблицы
            int startRow = cRow;
            rep.AddSingleValue("Объект", cRow, 1);
            rep.AddSingleValue("Здание", cRow, 2);
            rep.AddSingleValue("Этаж", cRow, 3);
            rep.AddSingleValue("Секция", cRow, 4);
            rep.AddSingleValue("Начало аренды", cRow, 5);
            rep.AddSingleValue("Конец аренды", cRow, 6);
            rep.AddSingleValue("Номер договора", cRow, 7);
            rep.AddSingleValue("Арендатор", cRow, 8);

            rep.SetFontBold(cRow, 1, cRow, maxColumns);
            rep.SetCellAlignmentToCenter(cRow, 1, cRow, maxColumns);
            rep.SetWrapText(cRow, 1, cRow, maxColumns);
            cRow++;
            #endregion

            #region таблица
            foreach (DataRow dr in dtReport.Rows)
            {
                rep.AddSingleValue(dr["nameObject"].ToString(), cRow, 1);
                rep.AddSingleValue(dr["nameBuilding"].ToString(), cRow, 2);
                rep.AddSingleValue(dr["nameFloor"].ToString(), cRow, 3);
                rep.AddSingleValue(dr["nameSection"].ToString(), cRow, 4);
                rep.AddSingleValue(dr["StartDate"].ToString().Length > 0 ? 
                    dr["StartDate"].ToString().Substring(0, 10) : dr["StartDate"].ToString(), cRow, 5) ;
                rep.AddSingleValue(dr["EndDate"].ToString().Length>0 ? dr["EndDate"].ToString().Substring(0,10) : dr["EndDate"].ToString(), cRow, 6);
                rep.AddSingleValue(dr["numDoc"].ToString(), cRow, 7);
                rep.AddSingleValue(dr["nameArenda"].ToString(), cRow, 8);
                if (dr["typeSection"].ToString() == "1")
                    rep.SetCellColor(cRow, 1, cRow, maxColumns, panel2.BackColor);
                if (dr["typeSection"].ToString() == "2")
                    rep.SetCellColor(cRow, 1, cRow, maxColumns, panel1.BackColor);
                cRow++;
            }
            cRow--;
            rep.SetWrapText(startRow, 1, cRow, maxColumns);
            rep.SetBorders(startRow, 1, cRow, maxColumns);

            cRow += 2;
            rep.SetCellColor(cRow, 1, cRow, 1, panel1.BackColor);
            rep.AddSingleValue("-Занятые секции", cRow, 2);
            cRow++;
            rep.SetCellColor(cRow, 1, cRow, 1, panel2.BackColor);
            rep.AddSingleValue("-Освобождающиеся секции", cRow, 2);

            #endregion
            rep.Show();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Task.Run(()=>get_data());
        }
    }
}
