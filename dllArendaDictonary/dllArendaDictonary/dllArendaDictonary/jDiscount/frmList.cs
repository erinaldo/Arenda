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
            tp.SetToolTip(btAdd, "Добавить");
            tp.SetToolTip(btEdit, "Редактировать");
            tp.SetToolTip(btDelete, "Удалить");
            tp.SetToolTip(btClose, "Выход");
            tp.SetToolTip(btConfirmD, "Подтвердить");

            btConfirmD.Visible = Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.Equals("Д");
            btAdd.Visible = btEdit.Visible = btDelete.Visible = new List<string> { "РКВ" }.Contains(UserSettings.User.StatusCode);
        }

        private void frmList_Load(object sender, EventArgs e)
        {
            get_data();
        }

        private void frmList_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == new frmAdd() { Text = "Добавить скидку" }.ShowDialog())
                get_data();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                DataRowView row = dtData.DefaultView[dgvData.CurrentRow.Index];
                if (DialogResult.OK == new frmAdd() { Text = "Редактировать скидку", row = row }.ShowDialog())
                    get_data();
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                int id = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];
                DateTime dateStart = (DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["DateStart"];
                DateTime? dateEnd = null;
                if (dtData.DefaultView[dgvData.CurrentRow.Index]["DateEnd"] !=DBNull.Value)
                    dateEnd = (DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["DateEnd"];

                int id_TypeDiscount = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_TypeDiscount"];
                int id_TypeTenant = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_TypeTenant"];
                int id_TypeAgreements = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_TypeAgreements"];
                int id_StatusDiscount = 1;
                bool isActive = true;

                Task<DataTable> task = Config.hCntMain.setTDiscount(id, dateStart, dateEnd, id_TypeDiscount, id_TypeTenant, id_TypeAgreements, id_StatusDiscount, isActive, true, 0);
                task.Wait();

                if (task.Result == null)
                {
                    MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int result = (int)task.Result.Rows[0]["id"];

                if (result == -1)
                {
                    MessageBox.Show(Config.centralText("Запись уже удалена другим пользователем\n"), "Удаление записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    get_data();
                    return;
                }


                if (result == -2 && isActive)
                {
                    if (DialogResult.Yes == MessageBox.Show(Config.centralText("Выбранная для удаления запись используется в программе.\nСделать запись недействующей?\n"), "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        setLog(id, 3);
                        task = Config.hCntMain.setTDiscount(id, dateStart, dateEnd, id_TypeDiscount, id_TypeTenant, id_TypeAgreements, id_StatusDiscount, !isActive, false, 0);
                        task.Wait();
                        if (task.Result == null)
                        {
                            MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        get_data();
                        return;
                    }
                }
                else
                if (result == 0 && isActive)
                {
                    if (DialogResult.Yes == MessageBox.Show("Удалить выбранную запись?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        setLog(id, 2);
                        task = Config.hCntMain.setTDiscount(id, dateStart, dateEnd, id_TypeDiscount, id_TypeTenant, id_TypeAgreements, id_StatusDiscount, isActive, true, 1);
                        task.Wait();
                        if (task.Result == null)
                        {
                            MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        get_data();
                        return;
                    }
                }
                else if (!isActive)
                {
                    if (DialogResult.Yes == MessageBox.Show("Сделать выбранную запись действующей?", "Восстановление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        setLog(id, 4);
                        task = Config.hCntMain.setTDiscount(id, dateStart, dateEnd, id_TypeDiscount, id_TypeTenant, id_TypeAgreements, id_StatusDiscount, !isActive, false, 0);
                        task.Wait();
                        if (task.Result == null)
                        {
                            MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        get_data();
                        return;
                    }
                }
            }
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
                btEdit.Enabled = btDelete.Enabled = false;
                btConfirmD.Enabled = false;
                return;
            }

            try
            {
                string filter = "";

                //if (tbNumber.Text.Trim().Length != 0)
                //    filter += (filter.Length == 0 ? "" : " and ") + $"cName like '%{tbNumber.Text.Trim()}%'";

                //if (!chbNotActive.Checked)
                //    filter += (filter.Length == 0 ? "" : " and ") + $"isActive = 1";

                dtData.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally
            {
                btEdit.Enabled = btDelete.Enabled =
                dtData.DefaultView.Count != 0;
                dgvData_SelectionChanged(null, null);
            }
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow == null || dgvData.CurrentRow.Index == -1 || dtData == null || dtData.DefaultView.Count == 0 || dgvData.CurrentRow.Index >= dtData.DefaultView.Count)
            {
                btDelete.Enabled = false;
                btEdit.Enabled = false;
                btConfirmD.Enabled = false;
                tbDiscountPrice.Text = tbPercentDiscount.Text = tbPrice.Text = tbTotalPrice.Text = "";
                return;
            }

            btDelete.Enabled = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_StatusDiscount"] == 1;
            btEdit.Enabled = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_StatusDiscount"] == 1;
            btConfirmD.Enabled = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_StatusDiscount"] == 1;

            tbDiscountPrice.Text = dtData.DefaultView[dgvData.CurrentRow.Index]["DiscountPrice"].ToString();
            tbPercentDiscount.Text = dtData.DefaultView[dgvData.CurrentRow.Index]["PercentDiscount"].ToString(); 
            tbPrice.Text = dtData.DefaultView[dgvData.CurrentRow.Index]["Price"].ToString(); 
            tbTotalPrice.Text = dtData.DefaultView[dgvData.CurrentRow.Index]["TotalPrice"].ToString();

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
            //tbNumber.Location = new Point(dgvData.Location.X + 1, tbNumber.Location.Y);
            //tbNumber.Size = new Size(cName.Width, tbNumber.Height);
        }

        private void cmbObject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void btConfirmD_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
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
                    MessageBox.Show("Произошла неведомая хрень.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                get_data();
            }
        }
    }
}
