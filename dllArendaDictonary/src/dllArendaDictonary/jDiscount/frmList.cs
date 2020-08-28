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
            tp.SetToolTip(btConfirmD, "Отклонить");

            btDeAcceptD.Visible = btConfirmD.Visible = Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.Equals("Д");
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
                return;
            }
            btDeAcceptD.Enabled = btConfirmD.Enabled = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_StatusDiscount"] == 1;
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
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                //int id = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];

                //DateTime dateStart = (DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["DateStart"];
                //DateTime? dateEnd = null;
                //if (dtData.DefaultView[dgvData.CurrentRow.Index]["DateEnd"] != DBNull.Value)
                //    dateEnd = (DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["DateEnd"];
                //int id_TypeAgreements = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_TypeAgreements"];
                //int id_TypeDiscount = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_TypeDiscount"];
                //int id_TypeTenant = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_TypeTenant"];
                //int id_StatusDiscount = 2;

                int id_discount = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];
                int _id = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_Agreements"];
                DateTime dStart = (DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["DateStart"];
                DateTime? dEnd = null;
                if (dtData.DefaultView[dgvData.CurrentRow.Index]["DateEnd"] != DBNull.Value) dEnd = (DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["DateEnd"];
                int id_TypeDiscount = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_TypeDiscount"];
                decimal discount = (decimal)dtData.DefaultView[dgvData.CurrentRow.Index]["Discount"];
                int id_Status = 2;

                //DataTable dtResult = _proc.setTDiscount(id_discount, _id, dStart, dEnd, id_TypeDiscount, id_Status, discount);



                if (DialogResult.No == MessageBox.Show("Подтвердить скидку?", "Подтверждение скидки", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) return;                        

                //Task<DataTable> task = Config.hCntMain.setTDiscount(id, dateStart, dateEnd, id_TypeDiscount, id_TypeTenant, id_TypeAgreements, id_StatusDiscount, true, false, 0);
                Task<DataTable> task = Config.hCntMain.setTDiscount(id_discount, _id, dStart, dEnd, id_TypeDiscount, id_Status, discount);
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
            }
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
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                //int id = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];

                //DateTime dateStart = (DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["DateStart"];
                //DateTime? dateEnd = null;
                //if (dtData.DefaultView[dgvData.CurrentRow.Index]["DateEnd"] != DBNull.Value)
                //    dateEnd = (DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["DateEnd"];
                //int id_TypeAgreements = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_TypeAgreements"];
                //int id_TypeDiscount = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_TypeDiscount"];
                //int id_TypeTenant = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_TypeTenant"];
                //int id_StatusDiscount = 3;


                int id_discount = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];
                int _id = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_Agreements"];
                DateTime dStart = (DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["DateStart"];
                DateTime? dEnd = null;
                if (dtData.DefaultView[dgvData.CurrentRow.Index]["DateEnd"] != DBNull.Value) dEnd = (DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["DateEnd"];
                int id_TypeDiscount = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_TypeDiscount"];
                decimal discount = (decimal)dtData.DefaultView[dgvData.CurrentRow.Index]["Discount"];
                int id_Status = 3;

                if (DialogResult.No == MessageBox.Show("Отклонить скидку?", "Отклонение скидки", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) return;

                //Task<DataTable> task = Config.hCntMain.setTDiscount(id, dateStart, dateEnd, id_TypeDiscount, id_TypeTenant, id_TypeAgreements, id_StatusDiscount, true, false, 0);
                Task<DataTable> task = Config.hCntMain.setTDiscount(id_discount, _id, dStart, dEnd, id_TypeDiscount, id_Status, discount);

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
            }
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
    }
}
