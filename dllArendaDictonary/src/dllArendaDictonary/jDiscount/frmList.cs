using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;
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
using Nwuram.Framework.ToExcelNew;

namespace dllArendaDictonary.jDiscount
{
    public partial class frmList : Form
    {
        private DataTable dtData;
        public frmList()
        {
            InitializeComponent();

            if (Config.hCntMain == null)
                Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            dgvData.AutoGenerateColumns = false;

            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose, "Выход");
            tp.SetToolTip(btConfirmD, "Подтвердить");
            tp.SetToolTip(btDeAcceptD, "Отклонить");
            tp.SetToolTip(btnBackInPast, "Возврат в неподтвержденные");
            btDeAcceptD.Visible = btConfirmD.Visible = cV.Visible = btnBackInPast.Visible = Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.Equals("Д");
        }

        private void frmList_Load(object sender, EventArgs e)
        {
            init_combobox();
            get_data();
        }

        private void init_combobox()
        {
            Task<DataTable> task = Config.hCntMain.getObjectLease(true);
            task.Wait();
            DataTable dtObjectLease = task.Result;

            cmbObject.DisplayMember = "cName";
            cmbObject.ValueMember = "id";
            cmbObject.DataSource = dtObjectLease;


            task = Config.hCntMain.getTypeContract(true);
            task.Wait();
            DataTable dtTypeContract = task.Result;

            cmbTypeContract.DisplayMember = "cName";
            cmbTypeContract.ValueMember = "id";
            cmbTypeContract.DataSource = dtTypeContract;
        }

        private void frmList_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void chbNotActive_CheckedChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void setFilter()
        {
            if (dtData == null || dtData.Rows.Count == 0)
            {
                btConfirmD.Enabled = btDeAcceptD.Enabled = false;
                return;
            }

            try
            {
                string filter = "";

                if((int)cmbObject.SelectedValue!= 0 )
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_ObjectLease = {cmbObject.SelectedValue}";

                if ((int)cmbTypeContract.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_TypeContract = {cmbTypeContract.SelectedValue}";

                if (tbLandLord.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"nameLandLord like '%{tbLandLord.Text.Trim()}%'";

                if (tbAgreements.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"Agreement like '%{tbAgreements.Text.Trim()}%'";

                if (!chbNotActive.Checked && !chbIsAccept.Checked)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_StatusDiscount = 1";
                else if (chbIsAccept.Checked && chbNotActive.Checked)
                    filter += "";
                else if (chbIsAccept.Checked && !chbNotActive.Checked)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_StatusDiscount in (1,2)";
                else if (!chbIsAccept.Checked && chbNotActive.Checked)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_StatusDiscount in (1,3)";

                filter += (filter.Length == 0 ? "" : " and ") + $"(('{dtpStart.Value.Date}'<=DateStart and DateStart<='{dtpEnd.Value.Date}') OR (('{dtpStart.Value.Date}'<=DateEnd and DateEnd<='{dtpEnd.Value.Date}')))";


                dtData.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally
            {                
                dgvData_SelectionChanged(null, null);
            }
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow == null || dgvData.CurrentRow.Index == -1 || dtData == null || dtData.DefaultView.Count == 0 || dgvData.CurrentRow.Index >= dtData.DefaultView.Count)
            {
                btDeAcceptD.Enabled = false;
                btConfirmD.Enabled = false;
                btnBackInPast.Enabled = false;
                return;
            }
            btDeAcceptD.Enabled = btConfirmD.Enabled = dtData.AsEnumerable().Where(r => r.Field<bool>("selected")).Count() > 0;
            //возврат в статус 1 кнопочка
            btnBackInPast.Enabled = new string[] { "2", "3" }.Contains(dtData.DefaultView[dgvData.CurrentRow.Index]["id_StatusDiscount"].ToString());    
                //(int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_StatusDiscount"] == 1;
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

        private void dgvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                Color rColor = Color.White;
                    if ((int)dtData.DefaultView[e.RowIndex]["id_StatusDiscount"]==2)
                        rColor = pConfirmD.BackColor;
                else if ((int)dtData.DefaultView[e.RowIndex]["id_StatusDiscount"] == 3)
                    rColor = panel1.BackColor;

                dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;

            }
        }


        private void get_data()
        {
            Task.Run(() =>
            {
                Config.DoOnUIThread(() => { this.Enabled = false; }, this);

                Task<DataTable> task = Config.hCntMain.getTDiscount();
                task.Wait();
                dtData = task.Result;

                Config.DoOnUIThread(() =>
                {
                    DataGridViewColumn oldCol = dgvData.SortedColumn;
                    ListSortDirection direction = ListSortDirection.Ascending;
                    if (oldCol != null)
                    {
                        if (dgvData.SortOrder == System.Windows.Forms.SortOrder.Ascending)
                        {
                            direction = ListSortDirection.Ascending;
                        }
                        else
                        {
                            direction = ListSortDirection.Descending;
                        }
                    }
                    setFilter();
                    dgvData.DataSource = dtData;


                    if (oldCol != null)
                    {
                        dgvData.Sort(oldCol, direction);
                        oldCol.HeaderCell.SortGlyphDirection =
                            direction == ListSortDirection.Ascending ?
                            System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;
                    }

                }, this);

                Config.DoOnUIThread(() => { this.Enabled = true; }, this);
            });
        }

        private void setLog(int id, int id_log)
        {
            Logging.StartFirstLevel(id_log);
            switch (id_log)
            {
                case 2: Logging.Comment("Удаление Типа документа"); break;
                case 3: Logging.Comment("Тип документа переведён в недействующие "); break;
                case 4: Logging.Comment("Тип документа переведён  в действующие"); break;
                default: break;
            }

            Logging.Comment($"ID:{id}");
            //Logging.Comment($"Наименование: {(string)dtData.DefaultView[dgvData.CurrentRow.Index]["cName"]}");

            Logging.StopFirstLevel();
        }

        private void dgvData_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            int width = 0;

            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                if (!col.Visible) continue;
                if (col.Index == nameLandLord.Index)
                {
                    tbLandLord.Location = new Point(dgvData.Location.X + width + 1, tbLandLord.Location.Y);
                    tbLandLord.Size = new Size(nameLandLord.Width, tbLandLord.Height);
                }

                if (col.Index == cAgreements.Index)
                {
                    tbAgreements.Location = new Point(dgvData.Location.X + width + 1, tbLandLord.Location.Y);
                    tbAgreements.Size = new Size(cAgreements.Width, tbLandLord.Height);
                }
                width += col.Width;
            }
        }

        private void cmbObject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void btConfirmD_Click(object sender, EventArgs e)
        {
            EnumerableRowCollection<DataRow> rowcoll = dtData.AsEnumerable().Where(r => r.Field<bool>("selected"));
            if (rowcoll.Count() == 0)
                return;
            string id = "";
            Logging.StartFirstLevel((int)logEnum.Подтверждение_скидки);
            foreach (DataRow dr in rowcoll)
            {
                id += $"{dr["id"]},";

                Logging.Comment($"ID:{dr["id"]}");
                Logging.Comment($"Объект ID:{dr["id_ObjectLease"]}; Наименование:{dr["nameObjectLease"]}");
                Logging.Comment($"Арендатор ID:{dr["id_Tenant"]}; Наименование:{dr["nameLandLord"]}");
                Logging.Comment($"№ договора:{dr["Agreement"]}");
                Logging.Comment($"Тип договора:{dr["TypeContract"]}");
                Logging.Comment($"Дата начала:{dr["DateStart"]}");
                Logging.Comment($"Дата окончания:{dr["DateEnd"]}");
                Logging.Comment($"Тип скидки:{dr["nameTypeDiscount"]}");
                Logging.Comment($"Скидка:{dr["Discount"]}");                
            }
            Logging.StopFirstLevel();
            id = id.Substring(0, id.Length - 1);
            if (dtData.AsEnumerable().Where(r=>r.Field<bool>("selected")).Count()>0)
            {
                Task<DataTable> task = Config.hCntMain.setTDiscountsAll(id, 2);
                task.Wait();
                if (task.Result == null)
                {
                    MessageBox.Show("Ошибка обновления статуса скидок", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Скидки подтверждены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                get_data();
            }

            #region gosha
            /* if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
             {
                 int id = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];

                 DateTime dateStart = (DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["DateStart"];
                 DateTime? dateEnd = null;
                 if (dtData.DefaultView[dgvData.CurrentRow.Index]["DateEnd"] != DBNull.Value)
                     dateEnd = (DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["DateEnd"];
                 int id_TypeAgreements = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_TypeAgreements"];
                 int id_TypeDiscount = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_TypeDiscount"];
                 int id_TypeTenant = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_TypeTenant"];
                 int id_StatusDiscount = 2;

                 if (DialogResult.No == MessageBox.Show("Подтвердить скидку?", "Подтверждение скидки", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) return;                        

                 Task<DataTable> task = Config.hCntMain.setTDiscount(id, dateStart, dateEnd, id_TypeDiscount, id_TypeTenant, id_TypeAgreements, id_StatusDiscount, true, false, 0);
                 task.Wait();

                 DataTable dtResult = task.Result;

                 if (dtResult == null || dtResult.Rows.Count == 0)
                 {
                     MessageBox.Show("Не удалось сохранить данные", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return;
                 }


                 if ((int)dtResult.Rows[0]["id"] == -1)
                 {
                     MessageBox.Show("В справочнике уже присутствует должность с таким наименованием.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return;
                 }


                 if ((int)dtResult.Rows[0]["id"] == -9999)
                 {
                     MessageBox.Show("Произошла неведомая фигня.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return;
                 }

                 get_data();
             }*/
            #endregion
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpStart.Value.Date > dtpEnd.Value.Date)
                    dtpEnd.Value = dtpStart.Value.Date;
            }
            catch { }
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpStart.Value.Date > dtpEnd.Value.Date)
                    dtpStart.Value = dtpEnd.Value.Date;
            }
            catch { }
        }

        private void chbUnlimitedDiscount_CheckedChanged(object sender, EventArgs e)
        {
            lDateEnd.Visible = dtpEnd.Visible = !chbUnlimitedDiscount.Checked;
        }

        private void btDeAcceptD_Click(object sender, EventArgs e)
        {
            EnumerableRowCollection<DataRow> rowcoll = dtData.AsEnumerable().Where(r => r.Field<bool>("selected"));
            if (rowcoll.Count() == 0)
                return;
            string id = "";
            Logging.StartFirstLevel((int)logEnum.Отклонение_скидки);
            foreach (DataRow dr in rowcoll)
            {
                id += $"{dr["id"]},";

                Logging.Comment($"ID:{dr["id"]}");
                Logging.Comment($"Объект ID:{dr["id_ObjectLease"]}; Наименование:{dr["nameObjectLease"]}");
                Logging.Comment($"Арендатор ID:{dr["id_Tenant"]}; Наименование:{dr["nameLandLord"]}");
                Logging.Comment($"№ договора:{dr["Agreement"]}");
                Logging.Comment($"Тип договора:{dr["TypeContract"]}");
                Logging.Comment($"Дата начала:{dr["DateStart"]}");
                Logging.Comment($"Дата окончания:{dr["DateEnd"]}");
                Logging.Comment($"Тип скидки:{dr["nameTypeDiscount"]}");
                Logging.Comment($"Скидка:{dr["Discount"]}");
            }
            Logging.StopFirstLevel();
            id = id.Substring(0, id.Length - 1);
            if (dtData.AsEnumerable().Where(r => r.Field<bool>("selected")).Count() > 0)
            {


                Task<DataTable> task = Config.hCntMain.setTDiscountsAll(id, 3);
                task.Wait();
                if (task.Result == null)
                {
                    MessageBox.Show("Ошибка обновления статуса скидок", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Скидки отклонены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                get_data();
            }
            #region gosha
            /* if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
             {
                 int id = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];

                 DateTime dateStart = (DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["DateStart"];
                 DateTime? dateEnd = null;
                 if (dtData.DefaultView[dgvData.CurrentRow.Index]["DateEnd"] != DBNull.Value)
                     dateEnd = (DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["DateEnd"];
                 int id_TypeAgreements = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_TypeAgreements"];
                 int id_TypeDiscount = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_TypeDiscount"];
                 int id_TypeTenant = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_TypeTenant"];
                 int id_StatusDiscount = 3;

                 if (DialogResult.No == MessageBox.Show("Отклонить скидку?", "Отклонение скидки", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) return;

                 Task<DataTable> task = Config.hCntMain.setTDiscount(id, dateStart, dateEnd, id_TypeDiscount, id_TypeTenant, id_TypeAgreements, id_StatusDiscount, true, false, 0);
                 task.Wait();

                 DataTable dtResult = task.Result;

                 if (dtResult == null || dtResult.Rows.Count == 0)
                 {
                     MessageBox.Show("Не удалось сохранить данные", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return;
                 }


                 if ((int)dtResult.Rows[0]["id"] == -1)
                 {
                     MessageBox.Show("В справочнике уже присутствует должность с таким наименованием.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return;
                 }


                 if ((int)dtResult.Rows[0]["id"] == -9999)
                 {
                     MessageBox.Show("Произошла неведомая фигня.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return;
                 }

                 get_data();
             }*/
            #endregion
        }

        private void cmbObject_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            setFilter();
        }

        private void dtpStart_CloseUp(object sender, EventArgs e)
        {
            setFilter();
        }

        private void dtpStart_Leave(object sender, EventArgs e)
        {
            setFilter();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == cV.Index)
            {
                if (dtData.DefaultView[e.RowIndex]["id_StatusDiscount"].ToString() != "1")
                    return;
                dtData.DefaultView[e.RowIndex]["selected"] = !(bool)dtData.DefaultView[e.RowIndex]["selected"];
                if (dtData != null || dtData.Rows.Count > 0 || dtData.DefaultView.Count > 0)
                    btConfirmD.Enabled = btDeAcceptD.Enabled = dtData.AsEnumerable().Where(r => r.Field<bool>("selected")).Count() > 0;
            }
        }

        bool selected = true;
        private void dgvData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == cV.Index)
            {
                foreach (DataRowView dr in dtData.DefaultView)
                {
                    if ((int)dr["id_StatusDiscount"] == 1)
                        dr["selected"] = selected;
                }
                dtData.AcceptChanges();
                if (dtData != null || dtData.Rows.Count > 0 || dtData.DefaultView.Count >0 )
                    btConfirmD.Enabled = btDeAcceptD.Enabled = dtData.AsEnumerable().Where(r => r.Field<bool>("selected")).Count() > 0;
                selected = !selected;
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (dtData == null || dtData.Rows.Count == 0)
                return;

            Logging.StartFirstLevel(79);

            Logging.Comment($"Объект ID:{cmbObject.SelectedValue}; Наименование:{cmbObject.Text}");
            Logging.Comment($"Тип договора ID:{cmbTypeContract.SelectedValue}; Наименование:{cmbTypeContract.Text}");

            Logging.Comment($"Договор:{tbAgreements.Text}");
            Logging.Comment($"Арендатор:{tbLandLord.Text}");

            Logging.Comment($"Подтвержденные скидки: {(chbIsAccept.Checked ? "Да" : "Нет")}");
            Logging.Comment($"Отклоненные скидки: {(chbNotActive.Checked ? "Да" : "Нет")}");

            Logging.Comment($"Дата начала:{dtpStart.Value.ToShortDateString()}");
            Logging.Comment($"Дата окончания:{dtpEnd.Value.ToShortDateString()}");
            Logging.Comment($"Отклоненные скидки: {(chbUnlimitedDiscount.Checked ? "Да" : "Нет")}");


            Logging.StopFirstLevel();
            
            ExcelUnLoad rep = new ExcelUnLoad("Скидки");
            int row = 1;
            rep.AddSingleValue($"Отчет по скидкам с {dtpStart.Value.ToShortDateString()}" +(!chbIsAccept.Checked ? $" по {dtpEnd.Value.ToShortDateString()}":""), row, 1) ;
            rep.SetFontBold(row, 1, row, 1);
            row++;
            rep.AddSingleValue($"Дата выгрузки: {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}", row, 1);
            row++;
            rep.AddSingleValue($"Выгрузил: {UserSettings.User.FullUsername}", row, 1);
            row += 2;
            int startTable = row;
            #region шапка
            rep.AddSingleValue("Объект", row, 1);//nameObjectLease
            rep.AddSingleValue("Арендатор", row, 2);//nameLandLord
            rep.AddSingleValue("№ договора", row, 3);//Agreement
            rep.AddSingleValue("Тип договора", row, 4);//TypeContract
            rep.AddSingleValue("Дата начала", row, 5); //DateStart
            rep.AddSingleValue("Дата окончания", row, 6);//DateEnd
            rep.AddSingleValue("Тип скидки", row, 7);//nameTypeDiscount
            rep.AddSingleValue("Скидка", row, 8);//Discount
            rep.SetFontBold(row, 1, row, 8);
            rep.SetWrapText(row, 1, row, 8);
            rep.SetCellAlignmentToCenter(row, 1, row, 8);
            rep.SetCellAlignmentToJustify(row, 1, row, 8);
            #endregion
            #region колонки
            rep.SetColumnWidth(1, 1, 1, 1, 8);
            rep.SetColumnWidth(2, 2, 2, 2, 23);
            rep.SetColumnWidth(3, 3, 3, 3, 15);
            rep.SetColumnWidth(4, 4, 4, 4, 20);
            rep.SetColumnWidth(5, 5, 5, 5, 12);
            rep.SetColumnWidth(6, 6, 6, 6, 12);
            rep.SetColumnWidth(7, 7, 7, 7, 25);
            rep.SetColumnWidth(8, 8, 8, 8, 15);
            rep.SetPageOrientationToLandscape();
            #endregion
            row++;
            rep.AddSingleValue("Неподтвержденные скидки", row, 1);
            rep.Merge(row, 1, row, 8);
            rep.SetCellAlignmentToCenter(row, 1, row, 1);
            rep.SetFontBold(row, 1, row, 1);
            row++;
            addTable(ref rep, ref row, 1);
            rep.AddSingleValue("Подтвержденные скидки", row, 1);
            rep.Merge(row, 1, row, 8);
            rep.SetCellAlignmentToCenter(row, 1, row, 1);
            rep.SetFontBold(row, 1, row, 1);
            row++;
            addTable(ref rep, ref row, 2);
            rep.AddSingleValue("Отклоненные скидки", row, 1);
            rep.Merge(row, 1, row, 8);
            rep.SetCellAlignmentToCenter(row, 1, row, 1);
            rep.SetFontBold(row, 1, row, 1);
            row++;
            addTable(ref rep, ref row, 3);

            rep.SetBorders(startTable, 1, row - 1, 8);
            rep.Show();
        }

        private void addTable(ref ExcelUnLoad rep, ref int row, int status)
        {
            EnumerableRowCollection<DataRow> rowcoll = dtData.AsEnumerable().Where(r => r.Field<int>("id_StatusDiscount") == status);
            foreach (DataRow dr in rowcoll)
            {
                rep.AddSingleValue(dr["nameObjectLease"].ToString(), row, 1);
                rep.AddSingleValue(dr["nameLandLord"].ToString(), row, 2);
                rep.AddSingleValue(dr["Agreement"].ToString(), row, 3);
                rep.AddSingleValue(dr["TypeContract"].ToString(), row, 4);
                rep.AddSingleValue(DateTime.Parse(dr["DateStart"].ToString()).ToShortDateString(), row, 5);
                if (dr["DateEnd"]!=DBNull.Value)
                    rep.AddSingleValue(DateTime.Parse(dr["DateEnd"].ToString()).ToShortDateString(), row, 6);
                rep.AddSingleValue(dr["nameTypeDiscount"].ToString(), row, 7);
                rep.AddSingleValue(dr["Discount"].ToString(), row, 8);
                rep.SetWrapText(row, 1, row, 9);
                row++;
            }
        }

        private void btnBackInPast_Click(object sender, EventArgs e)
        {
            string id = dtData.DefaultView[dgvData.CurrentRow.Index]["id"].ToString();
            Task<DataTable> task = Config.hCntMain.setTDiscountsAll(id, 1);
            task.Wait();
            if (task.Result == null)
            {
                MessageBox.Show("Ошибка обновления статуса скидок", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Скидка возвращена в статус \"Неподтвержденные\"", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            get_data();
        }
    }
}
