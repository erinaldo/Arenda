using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;

namespace Arenda
{
    public partial class frmObjects : Form
    {
        readonly Procedures proc = new Procedures(ConnectionSettings.GetServer(),
          ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(),
          ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        public frmObjects()
        {
            InitializeComponent();
            dgvObjects.AutoGenerateColumns = false;
        }

        private void frmObjects_Load(object sender, EventArgs e)
        {
            GetObjects();
            SetButtonsEnabled();
        }

        private void GetObjects()
        {
            DataTable dtObj = proc.GetObjects();
            dtObj.DefaultView.Sort = "cName";
            dgvObjects.DataSource = dtObj;
            Filter();
        }

        private void SetButtonsEnabled()
        {
            if (TempData.Rezhim.Equals("РКВ"))
            {
                btDel.Enabled = dgvObjects.Rows.Count > 0 && dgvObjects.CurrentRow != null;
                btEdit.Enabled = dgvObjects.Rows.Count > 0 && dgvObjects.CurrentRow != null
                  && dgvObjects.CurrentRow.Cells["isActive"].Value.ToString() == "1";
            }
            else
                btAdd.Visible = btEdit.Visible = btDel.Visible = false;
        }

        private void Filter()
        {
            if (dgvObjects.DataSource != null)
            {
                string filter = "";

                if (tbName.Text.Length > 0)
                    filter += "cName like '%" + tbName.Text + "%'";

                if (!cbIsActive.Checked)
                    filter += (filter.Length > 0 ? " and " : "") + "isActive = 1";

                (dgvObjects.DataSource as DataTable).DefaultView.RowFilter = filter;
                SetButtonsEnabled();
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            frmAddEditObject frm = new frmAddEditObject(0, "", "", "");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GetObjects();
            }
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void cbIsActive_CheckedChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvObjects_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (!Convert.ToBoolean(dgvObjects.Rows[e.RowIndex].Cells["isActive"].Value))
                {
                    dgvObjects.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                      dgvObjects.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor =
                      pnlInActive.BackColor;
                }
                else
                {
                    dgvObjects.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                      dgvObjects.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor =
                      Color.White;
                }
            }
        }

        private void dgvObjects_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void btDel_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(dgvObjects.CurrentRow.Cells["isActive"].Value))
            {
                DataTable dtUsed = proc.CheckObjectIsUsed(Convert.ToInt32(dgvObjects.CurrentRow.Cells["id"].Value));
                //bool used = Convert.ToBoolean(dgvObjects.CurrentRow.Cells["Used"].Value);
                bool used = dtUsed != null && dtUsed.Rows.Count > 0 && Convert.ToBoolean(dtUsed.Rows[0]["Used"]);
                string message = used ? "Выбранная для удаления запись\n    используется в программе.\nСделать запись недействующей?" : "Удалить выбранную запись?";
                if (MessageBox.Show(message, "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (!used)
                    {
                        DataTable dtObj = proc.CheckObjectIsNotDel(Convert.ToInt32(dgvObjects.CurrentRow.Cells["id"].Value));
                        if (dtObj == null || dtObj.Rows.Count == 0)
                        {
                            MessageBox.Show("Запись уже удалена другим\n         пользователем.", "Удаление записи", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            GetObjects();
                            return;
                        }
                    }

                    if (used)
                    {
                        Logging.StartFirstLevel(540);

                        Logging.Comment($"Смена статуса объекта аренды на недействующий");
                        Logging.Comment($"ID:{dgvObjects.CurrentRow.Cells["id"].Value}");

                        Logging.Comment($"Наименование: {dgvObjects.CurrentRow.Cells["cName"].Value}");
                        Logging.Comment($"Аббревиатура: {dgvObjects.CurrentRow.Cells["Comment"].Value }");
                        Logging.Comment($"Кадастровый номер {dgvObjects.CurrentRow.Cells["CadastralNumber"].Value}");

                        Logging.StopFirstLevel();
                    }
                    else
                    {
                        Logging.StartFirstLevel((int)logEnum.Удаление_объекта_аренды);
                                                
                        Logging.Comment($"ID:{dgvObjects.CurrentRow.Cells["id"].Value}");

                        Logging.Comment($"Наименование: {dgvObjects.CurrentRow.Cells["cName"].Value}");
                        Logging.Comment($"Аббревиатура: {dgvObjects.CurrentRow.Cells["Comment"].Value }");
                        Logging.Comment($"Кадастровый номер {dgvObjects.CurrentRow.Cells["CadastralNumber"].Value}");

                        Logging.StopFirstLevel();
                    }

                    proc.ChangeObjectActiveStatus(Convert.ToInt32(dgvObjects.CurrentRow.Cells["id"].Value), false, used, dgvObjects.CurrentRow.Cells["Comment"].Value.ToString());
                    GetObjects();
                }
            }
            else if (MessageBox.Show("Сделать выбранную запись действующей?", "Восстановление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {

                Logging.StartFirstLevel(540);

                Logging.Comment($"Смена статуса объекта аренды на действующий");
                Logging.Comment($"ID:{dgvObjects.CurrentRow.Cells["id"].Value}");

                Logging.Comment($"Наименование: {dgvObjects.CurrentRow.Cells["cName"].Value}");
                Logging.Comment($"Аббревиатура: {dgvObjects.CurrentRow.Cells["Comment"].Value }");
                Logging.Comment($"Кадастровый номер {dgvObjects.CurrentRow.Cells["CadastralNumber"].Value}");

                Logging.StopFirstLevel();

                proc.ChangeObjectActiveStatus(Convert.ToInt32(dgvObjects.CurrentRow.Cells["id"].Value), true, true, dgvObjects.CurrentRow.Cells["Comment"].Value.ToString());
                GetObjects();
            }
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            frmAddEditObject frm = new frmAddEditObject(Convert.ToInt32(dgvObjects.CurrentRow.Cells["id"].Value),
              dgvObjects.CurrentRow.Cells["cName"].Value.ToString(),
              dgvObjects.CurrentRow.Cells["Comment"].Value.ToString(),
              dgvObjects.CurrentRow.Cells["CadastralNumber"].Value.ToString());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GetObjects();
            }
        }

        private void dgvObjects_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            tbName.Width = dgvObjects.Columns[1].Width;
        }

        private void dgvObjects_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SetButtonsEnabled();
        }

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
