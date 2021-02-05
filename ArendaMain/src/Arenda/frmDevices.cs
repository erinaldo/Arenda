using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Logging;

namespace Arenda
{
    public partial class frmDevices : Form
    {
        private Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        public frmDevices()
        {
            InitializeComponent();
            dgvDevices.AutoGenerateColumns = false;

            if (TempData.Rezhim.ToLower().Equals("пр"))
            {
                Logging.StartFirstLevel(1394);
                Logging.Comment("Открыта форма просмотра справочника приборов");

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }
        }

        private void frmDevices_Load(object sender, EventArgs e)
        {
            //if (TempData.Rezhim == "ПР")
            if (new List<string> { "СОА", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim))
            {
                btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = false;
            }

            dgvDevices_Load();
            SetButtonsEnabled();
        }

        private void dgvDevices_Load()
        {
            dgvDevices.AutoGenerateColumns = false;
            dgvDevices.DataSource = proc.GetDevices();
            Filter();
        }

        private void dgvDevices_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Color color = Color.White;

                if (!Convert.ToBoolean(dgvDevices.Rows[e.RowIndex].Cells["is_active"].Value))
                {
                    color = pnlInactive.BackColor;
                }

                dgvDevices.Rows[e.RowIndex].DefaultCellStyle.BackColor = dgvDevices.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = color;
            }
            
        }

        private void dgvDevices_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
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

        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            if (dgvDevices.DataSource != null)
            {
                string filter = "";
                if (!cbAll.Checked)
                {
                    filter = "is_active = true";
                }
                (dgvDevices.DataSource as DataTable).DefaultView.RowFilter = filter;
                SetButtonsEnabled();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditDevice frmAdd = new AddEditDevice(new Device { Id = 0, Name = "", Abbreviation = "", Unit = "" });
            if (frmAdd.ShowDialog() == DialogResult.OK)
            {
                dgvDevices_Load();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataGridViewRow gridRow = dgvDevices.CurrentRow;

            AddEditDevice frmEdit = new AddEditDevice(new Device { Id = Convert.ToInt32(gridRow.Cells["id"].Value), Name = gridRow.Cells["name"].Value.ToString(), Abbreviation = gridRow.Cells["abbreviation"].Value.ToString(), Unit = gridRow.Cells["unit"].Value.ToString() });
            if (frmEdit.ShowDialog() == DialogResult.OK)
            {
                dgvDevices_Load();
            }
        }

        private void SetButtonsEnabled()
        {
            btnEdit.Enabled = btnDelete.Enabled = dgvDevices.CurrentRow != null;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
      private void btnDelete_Click(object sender, EventArgs e)
      {
        string _cName = dgvDevices.CurrentRow.Cells["name"].Value.ToString();
        string _Abbr = dgvDevices.CurrentRow.Cells["abbreviation"].Value.ToString();
        string _unit = dgvDevices.CurrentRow.Cells["unit"].Value.ToString();
        int zid = Convert.ToInt32(dgvDevices.CurrentRow.Cells["id"].Value);

        if (Convert.ToBoolean(dgvDevices.CurrentRow.Cells["is_active"].Value))
        {
          //if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
          if (Convert.ToBoolean(dgvDevices.CurrentRow.Cells["used"].Value))// && MessageBox.Show("Удаляемая запись испольхуется и её невозможно удалить. Сделать запись неактивной?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
          {
            if (MessageBox.Show("Выбранная для удаления запись\n    используется в программе.\nСделать запись недействующей?",
              "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
              Logging.StartFirstLevel(540);
              Logging.Comment("Произведена смена статуса на неактивный у прибора");
              Logging.Comment("ID: " + zid);
              Logging.Comment("Наименование прибора: " + _cName);
              Logging.Comment("Аббревиатура прибора: " + _Abbr);
              Logging.Comment("Единицы измерения: " + _unit);

              Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
              Logging.StopFirstLevel();

              //proc.DeleteDevice(Convert.ToInt32(dgvDevices.CurrentRow.Cells["id"].Value), true);
              proc.RestoreDevice(Convert.ToInt32(dgvDevices.CurrentRow.Cells["id"].Value), false, true);
            }
          }
          else
          {
            if (MessageBox.Show("Удалить выбранную запись?", "Удаление записи",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
              Logging.StartFirstLevel(1393);
              Logging.Comment("Удаление прибора");
              Logging.Comment("ID: " + zid);
              Logging.Comment("Наименование прибора: " + _cName);
              Logging.Comment("Аббревиатура прибора: " + _Abbr);
              Logging.Comment("Единицы измерения: " + _unit);

              Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
              Logging.StopFirstLevel();

              //proc.DeleteDevice(Convert.ToInt32(dgvDevices.CurrentRow.Cells["id"].Value), false);
              proc.RestoreDevice(Convert.ToInt32(dgvDevices.CurrentRow.Cells["id"].Value), false, false);
            }
          }
          dgvDevices_Load();
        }
        else
        {
          //if (MessageBox.Show("Сделать запись снова активной?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
          if (MessageBox.Show("Сделать выбранную запись действующей?",
            "Восстановление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2) == DialogResult.Yes)
          {
            Logging.StartFirstLevel(540);
            Logging.Comment("Произведена смена статуса на активный  у прибора");
            Logging.Comment("ID: " + zid);
            Logging.Comment("Наименование прибора: " + _cName);
            Logging.Comment("Аббревиатура прибора: " + _Abbr);
            Logging.Comment("Единицы измерения: " + _unit);

            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
              + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();

            proc.RestoreDevice(Convert.ToInt32(dgvDevices.CurrentRow.Cells["id"].Value), true, false);
            dgvDevices_Load();
          }
        }
      }

      private void dgvDevices_SelectionChanged(object sender, EventArgs e)
      {
        if (dgvDevices.SelectedRows.Count > 0)
          btnEdit.Enabled = Convert.ToBoolean(dgvDevices.CurrentRow.Cells["is_active"].Value);
        else
          btnEdit.Enabled = false;
      }
    }
}
