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
    public partial class frmAnotherPayments : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        DataTable dtAnotherPayments;

        public frmAnotherPayments()
        {
            InitializeComponent();

            if (TempData.Rezhim.ToLower().Equals("пр"))
            {
                Logging.StartFirstLevel(1394);
                Logging.Comment("Открыта форма просмотра справочника дополнительных оплат");

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }

        }

        private void frmAnotherPayments_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            //if (TempData.Rezhim == "ПР")
            if (new List<string> { "СОА", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim))
            {
                btnAdd.Visible =
                    btnEdit.Visible =
                    btnDel.Visible =
                    false;
            }

            dtAnotherPayments = new DataTable();
            dtAnotherPayments = _proc.GetAnotherPayments(chbAll.Checked);

            dgAnotherPayments.AutoGenerateColumns = false;
            dgAnotherPayments.DataSource = dtAnotherPayments;

            Buttons();
        }

        private void chbAll_CheckedChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void Buttons()
        {
            if (dgAnotherPayments.Rows.Count > 0)
            {
                btnDel.Enabled = true;
                
                bool isActive = bool.Parse(dgAnotherPayments.Rows[dgAnotherPayments.CurrentRow.Index].Cells["isActive"].Value.ToString());

                btnEdit.Enabled = isActive;
            }
            else
            {
                btnEdit.Enabled = false;
                btnDel.Enabled = false;
            }
        }

        private void dgAnotherPayments_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dgAnotherPayments.CurrentCell != null)
            {
                bool isActive = bool.Parse(dgAnotherPayments.Rows[e.RowIndex].Cells["isActive"].Value.ToString());

                if (isActive == false)
                {
                    dgAnotherPayments.Rows[e.RowIndex].DefaultCellStyle.BackColor = pbUnactive.BackColor;
                }
                else
                {
                    dgAnotherPayments.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgAnotherPayments_SelectionChanged(object sender, EventArgs e)
        {
            Buttons();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddEditAnotherPayments frmAdd = new frmAddEditAnotherPayments();
            frmAdd.ShowDialog();
            GetData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgAnotherPayments.CurrentCell != null)
            {
                int idPay = int.Parse(dgAnotherPayments.Rows[dgAnotherPayments.CurrentRow.Index].Cells["id"].Value.ToString());
                string namePay = dgAnotherPayments.Rows[dgAnotherPayments.CurrentRow.Index].Cells["cName"].Value.ToString();

                frmAddEditAnotherPayments frmEdit = new frmAddEditAnotherPayments(idPay, namePay);
                frmEdit.ShowDialog();

                GetData();
            }
        }
      
      private void btnDel_Click(object sender, EventArgs e)
      {
        if (dgAnotherPayments.CurrentCell != null)
        {
          string _cName = dgAnotherPayments.Rows[dgAnotherPayments.CurrentRow.Index].Cells["cName"].Value.ToString();
          int zid = int.Parse(dgAnotherPayments.Rows[dgAnotherPayments.CurrentRow.Index].Cells["id"].Value.ToString());

          int idPay = int.Parse(dgAnotherPayments.Rows[dgAnotherPayments.CurrentRow.Index].Cells["id"].Value.ToString());
          bool isActive = bool.Parse(dgAnotherPayments.Rows[dgAnotherPayments.CurrentRow.Index].Cells["isActive"].Value.ToString());

          bool used = (_proc.AnotherPaymentsIsUsed(idPay) == 1) ? true : false;

          if (isActive)
          {
            if (used)
            {
              //DialogResult d = MessageBox.Show("Удаляемая запись используется и ее невозможно удалить. Сделать ее неактивной?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
              DialogResult d = MessageBox.Show("Выбранная для удаления запись\n    используется в программе.\nСделать запись недействующей?",
                "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
              if (d == DialogResult.Yes)
              {
                Logging.StartFirstLevel(540);
                Logging.Comment("Произведена смена статуса на неактивный у дополнительной оплаты");
                Logging.Comment("ID: " + zid);
                Logging.Comment("Наименование дополнительной оплаты: " + _cName);

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                  + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();

                //сменить статус на недействующую
                _proc.DelAnotherPayments(idPay, 0);
              }
            }
            
            if (!used)
            {
              //DialogResult d = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
              DialogResult d = MessageBox.Show("Удалить выбранную запись?",
                "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
              if (d == DialogResult.Yes)
              {
                Logging.StartFirstLevel(1390);
                Logging.Comment("ID: " + zid);
                Logging.Comment("Наименование дополнительной оплаты: " + _cName);

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                  + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
                //удаление записи
                _proc.DelAnotherPayments(idPay, -1);
              }
            }
          }
          else
          {
            //DialogResult d = MessageBox.Show("Сделать запись снова активной?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            DialogResult d = MessageBox.Show("Сделать выбранную запись действующей?",
              "Восстановление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2);
            if (d == DialogResult.Yes)
            {
              Logging.StartFirstLevel(540);
              Logging.Comment("Произведена смена статуса на активный у дополнительной оплаты");
              Logging.Comment("ID: " + zid);
              Logging.Comment("Наименование дополнительной оплаты: " + _cName);                        

              Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
              Logging.StopFirstLevel();

              //сменить статус на действующую
              _proc.DelAnotherPayments(idPay, 1);
            }
          }
          
          GetData();
        }
      }
    }
}
