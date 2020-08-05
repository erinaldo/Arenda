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

namespace dllArendaDictonary.dicAdvertisingSpace
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

            btAdd.Visible = btEdit.Visible = btDelete.Visible = new List<string> { "РКВ" }.Contains(UserSettings.User.StatusCode);
        }

        private void frmList_Load(object sender, EventArgs e)
        {
            Task<DataTable> task = Config.hCntMain.getObjectLease(true);
            task.Wait();
            DataTable dtObjectLease = task.Result;

            cmbObject.DisplayMember = "cName";
            cmbObject.ValueMember = "id";
            cmbObject.DataSource = dtObjectLease;            

            get_data();
        }

        private void frmList_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == new frmAdd() { Text = "Добавить рекламное место" }.ShowDialog())
                get_data();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                DataRowView row = dtData.DefaultView[dgvData.CurrentRow.Index];
                if (DialogResult.OK == new frmAdd() { Text = "Редактировать рекламное место", row = row }.ShowDialog())
                    get_data();
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                int id = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];
                bool isActive = (bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isActive"];
                int id_Building =(int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_Building"];
                int id_ObjectLease = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_ObjectLease"];

                Int64 Length = (Int64)dtData.DefaultView[dgvData.CurrentRow.Index]["Length"];
                Int64 Width = (Int64)dtData.DefaultView[dgvData.CurrentRow.Index]["Width"];

                string NumberPlace = (string)dtData.DefaultView[dgvData.CurrentRow.Index]["NumberPlace"];

                Task<DataTable> task = Config.hCntMain.setReclamaPlace(id, NumberPlace, id_ObjectLease, id_Building, Length, Width, isActive, true, 0);
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
                        task = Config.hCntMain.setReclamaPlace(id, NumberPlace, id_ObjectLease, id_Building, Length, Width, !isActive, false, 0);
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
                        task = Config.hCntMain.setReclamaPlace(id, NumberPlace, id_ObjectLease, id_Building, Length, Width, isActive, true, 1);
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
                        task = Config.hCntMain.setReclamaPlace(id, NumberPlace, id_ObjectLease, id_Building, Length, Width, !isActive, false, 0);
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
                return;
            }

            try
            {
                string filter = "";

                if (tbNumber.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"NumberPlace like '%{tbNumber.Text.Trim()}%'";

                if ((int)cmbObject.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_ObjectLease  = {cmbObject.SelectedValue}";

                if (!chbNotActive.Checked)
                    filter += (filter.Length == 0 ? "" : " and ") + $"isActive = 1";

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
                return;
            }

            btDelete.Enabled = true;
            btEdit.Enabled = (bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isActive"];
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
                if (!(bool)dtData.DefaultView[e.RowIndex]["isActive"])
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

                Task<DataTable> task = Config.hCntMain.getReclamaPlace();
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
            tbNumber.Location = new Point(dgvData.Location.X + cObjectName.Width + 1, tbNumber.Location.Y);
            tbNumber.Size = new Size(cNumber.Width, tbNumber.Height);
        }

        private void cmbObject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }
    }
}
