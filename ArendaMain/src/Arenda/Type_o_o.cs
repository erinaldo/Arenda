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
    public partial class Type_o_o : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        DataTable tTOO;
        public Type_o_o()
        {
            InitializeComponent();

            if (Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.ToLower().Equals("пр"))
            {
                Logging.StartFirstLevel(1394);
                Logging.Comment("Открыта форма просмотра справочника типов организаций");

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }
        }

        private void btAddtoo_Click(object sender, EventArgs e)
        {
            var tooadd = new AddType_o_o();
            tooadd.ShowDialog();
            ini();
        }

        private void tbEdit_Click(object sender, EventArgs e)
        {
            var tooadd = new AddType_o_o(grdType.CurrentRow.Cells["cName"].Value.ToString(), grdType.CurrentRow.Cells["abbr"].Value.ToString(), grdType.CurrentRow.Cells["id"].Value.ToString(), 0);
            tooadd.ShowDialog();
            ini();
        }


        private void ini()
        {
            //if (TempData.Rezhim == "ПР")
            if (new List<string> { "СОА", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim))
            {
                btAddtoo.Visible =
                    tbEdit.Visible =
                    btDel.Visible =
                    false;
            }

            if (checAll.Checked == true)
                tTOO = _proc.GetTOO(0);
             else 
                tTOO = _proc.GetTOO(1);                        

            grdType.AutoGenerateColumns = false;
            grdType.DataSource = tTOO;
            
            isActive();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
      
      private void btDel_Click(object sender, EventArgs e)
      {
        try
        {
          string _cName = grdType.CurrentRow.Cells["cName"].Value.ToString();
          string _Abbr = grdType.CurrentRow.Cells["abbr"].Value.ToString();
          int zid = Convert.ToInt32(grdType.CurrentRow.Cells["id"].Value);

          //string rez = grdType.SelectedRows[0].Cells["isActive2"].Value.ToString();

          string rez = grdType.CurrentRow.Cells["isActive2"].Value.ToString();
          if (rez == "False")
          {
            {
              //if (MessageBox.Show("Сделать запись снова активной?", "Внимание", MessageBoxButtons.YesNo,
                //MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              if (MessageBox.Show("Сделать выбранную запись действующей?",
                "Восстановление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              {
                _proc.AddEditType_o_o(grdType.CurrentRow.Cells["cName"].Value.ToString(), grdType.CurrentRow.Cells["abbr"].Value.ToString(), Convert.ToInt32(grdType.CurrentRow.Cells["id"].Value), 0, 1);

                Logging.StartFirstLevel(540);
                Logging.Comment("Произведена смена статуса на активный у организации");
                Logging.Comment("ID: " + zid);
                Logging.Comment("Наименование типа организации: " + _cName);
                Logging.Comment("Аббревиатура типа организации: " + _Abbr);

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                  + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
              }
            }
          }
          else
          {
            //if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Внимание", MessageBoxButtons.YesNo,
            //                  MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            string chk = _proc.BefTOO(Convert.ToInt32(grdType.CurrentRow.Cells["id"].Value.ToString())).Rows[0][0].ToString(); ;
            if (chk == "0")
            {
              if (MessageBox.Show("Удалить выбранную запись?", "Удаление записи",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              {
                _proc.delTOO(Convert.ToInt32(grdType.CurrentRow.Cells["id"].Value.ToString()));

                Logging.StartFirstLevel(1378);
                Logging.Comment("ID: " + zid);
                Logging.Comment("Наименование типа организации: " + _cName);
                Logging.Comment("Аббревиатура типа организации: " + _Abbr);

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                  + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
              }
            }
            else
            {
              //if (MessageBox.Show("Удаляемая запись используется и ее невозможно удалить. Сделать ее неактивной?", "Ошибка", MessageBoxButtons.YesNo,
              //MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              if (MessageBox.Show("Выбранная для удаления запись\n    используется в программе.\nСделать запись недействующей?",
                "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              {
                _proc.AddEditType_o_o(grdType.CurrentRow.Cells["cName"].Value.ToString(), grdType.CurrentRow.Cells["abbr"].Value.ToString(), Convert.ToInt32(grdType.CurrentRow.Cells["id"].Value), 0, 0);

                Logging.StartFirstLevel(540);
                Logging.Comment("Произведена смена статуса на неактивный  у организации");
                Logging.Comment("ID: " + zid);
                Logging.Comment("Наименование типа организации: " + _cName);
                Logging.Comment("Аббревиатура типа организации: " + _Abbr);

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                  + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
              }
            }
          }
          ini();
        }
        catch (Exception r) { MessageBox.Show("Нет записей для удаления." + "\n" + r.ToString(), "Ошибка"); }
      }

        private void checAll_CheckedChanged(object sender, EventArgs e)
        {
            ini();            
        }

        private void isActive()
        {
            if (grdType.Rows.Count == 0)
            {
                tbEdit.Enabled = false;
                btDel.Enabled = false;
            }
            else
            {
                try
                {
                    if (grdType.CurrentRow.Cells["isActive2"].Value.ToString() == "False")
                        tbEdit.Enabled = false;                    
                    else 
                        tbEdit.Enabled = true;
                }
                catch (Exception) { }                
                btDel.Enabled = true;
            }

        }

        private void Type_o_o_Load(object sender, EventArgs e)
        {
            ini();
        }

        private void grdType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            isActive();
        }

        private void grdType_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            isActive();
        }

        private void grdType_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            isActive();
        }

        private void grdType_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (tTOO.DefaultView[e.RowIndex]["isActive"].ToString() == "False")
            {
                grdType.Rows[e.RowIndex].DefaultCellStyle.BackColor = pictureBox2.BackColor;
            }
        }

        private void grdType_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (grdType.Rows.Count == 0)
            {
                tbEdit.Enabled = false;
                btDel.Enabled = false;

            }

            else
            {
                tbEdit.Enabled = true;
                btDel.Enabled = true;
            }
        }
    }
}
