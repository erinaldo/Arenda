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
    public partial class Floors : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        DataTable Floor = null;
        DataTable _Floor = null;
        public Floors()
        {
            InitializeComponent();
            if (bgFloor.Rows.Count == 0)
            {
                button2.Enabled = false;
                button3.Enabled = false;

            }

            else
            {
                button2.Enabled = true;
                button3.Enabled = true;

            }

            if (Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.ToLower().Equals("пр"))
            {
                Logging.StartFirstLevel(1394);
                Logging.Comment("Открыта форма просмотра справочника этажей");

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var addedit = new AddEditFloor();

            addedit.ShowDialog();
            ini();
            isactive();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (bgFloor.Rows.Count > 0)
            {
                try
                {
                    var addedit = new AddEditFloor(bgFloor.SelectedRows[0].Cells[1].Value.ToString(), bgFloor.SelectedRows[0].Cells[2].Value.ToString(), bgFloor.SelectedRows[0].Cells[0].Value.ToString(), true);

                    addedit.ShowDialog();
                }
                catch (Exception r) { MessageBox.Show("Нет записей для редактирования." + "\n" + r.ToString(), "Ошибка"); }
            }
            ini();
            isactive();
        }

      private void button3_Click(object sender, EventArgs e)
      {
        try
        {
          string _cName = bgFloor.SelectedRows[0].Cells[1].Value.ToString();
          string _Abbr = bgFloor.SelectedRows[0].Cells[2].Value.ToString();
          int zid = Convert.ToInt32(bgFloor.SelectedRows[0].Cells[0].Value);

          string rez = _proc.isActiveFloor(Convert.ToInt32(bgFloor.SelectedRows[0].Cells[0].Value.ToString())).Rows[0][0].ToString();
          if (rez == "False")
          {
            {
              //if (MessageBox.Show("Сделать запись снова активной?", "Внимание", MessageBoxButtons.YesNo,
                //MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              if (MessageBox.Show("Сделать выбранную запись действующей?",
                "Восстановление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              {
                _proc.ChgFloor(Convert.ToInt32(bgFloor.SelectedRows[0].Cells[0].Value), bgFloor.SelectedRows[0].Cells[1].Value.ToString(), bgFloor.SelectedRows[0].Cells[2].Value.ToString(), 1);

                Logging.StartFirstLevel(540);
                Logging.Comment("Произведена смена статуса на активный у этажа");
                Logging.Comment("ID: " + zid);
                Logging.Comment("Наименование здания: " + _cName);
                Logging.Comment("Аббревиатура здания: " + _Abbr);

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                  + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
              }
            }
          }
          else
            //if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Внимание", MessageBoxButtons.YesNo,
            //                  MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
          {
            string chk1 = _proc.befFloor(Convert.ToInt32(bgFloor.SelectedRows[0].Cells[0].Value.ToString())).Rows[0][0].ToString();
            string chk2 = _proc.befFloor(Convert.ToInt32(bgFloor.SelectedRows[0].Cells[0].Value.ToString())).Rows[1][0].ToString();
            if (chk1 == "0" && chk2 == "0")
            {
              if (MessageBox.Show("Удалить выбранную запись?", "Удаление записи",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              {
                _proc.delFloor(Convert.ToInt32(bgFloor.SelectedRows[0].Cells[0].Value.ToString()));

                Logging.StartFirstLevel(1372);
                Logging.Comment("ID: " + zid);
                Logging.Comment("Наименование здания: " + _cName);
                Logging.Comment("Аббревиатура здания: " + _Abbr);

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
                _proc.ChgFloor(Convert.ToInt32(bgFloor.SelectedRows[0].Cells[0].Value), bgFloor.SelectedRows[0].Cells[1].Value.ToString(), bgFloor.SelectedRows[0].Cells[2].Value.ToString(), 0);

                Logging.StartFirstLevel(540);
                Logging.Comment("Произведена смена статуса на неактивный у этажа");
                Logging.Comment("ID: " + zid);
                Logging.Comment("Наименование здания: " + _cName);
                Logging.Comment("Аббревиатура здания: " + _Abbr);

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                  + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
              }
            }
          }
          ini();
          isactive();
        }
        catch (Exception r) { MessageBox.Show("Нет записей для удаления." + "\n" + r.ToString(), "Ошибка"); }
      }

        private void Floors_Load(object sender, EventArgs e)
        {
            ini();
        }

        private void bgFloor_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (Floor.DefaultView[e.RowIndex]["isActive"].ToString() == "False")
            {
                bgFloor.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Coral;
            }
        }

        private void Floors_Activated(object sender, EventArgs e)
        {
            //ini();
        }
        private void ini()
        {
            //if (TempData.Rezhim == "ПР")
            if (new List<string> { "СОА", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim))
            {
                button1.Visible =
                    button2.Visible =
                    button3.Visible =
                    false;
            }

            if (checAll.Checked == true)
            {
                Floor = _proc.GetFloor(3);
            }

            else { Floor = _proc.GetFloor(1); }
            _Floor = Floor.DefaultView.ToTable("_Floor", false, new[] { "cname", "abbreviation", "id", "isActive" });
            bds.DataSource = Floor;
            bgFloor.DataSource = bds;
            cname.DataPropertyName = "cname";
            abbreviation.DataPropertyName = "abbreviation";
            ID.DataPropertyName = "id";
            isActive.DataPropertyName = "isActive";
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void checAll_CheckedChanged(object sender, EventArgs e)
        {
            ini();
            isactive();
           
        }

        private void bgFloor_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (bgFloor.Rows.Count == 0)
            {
                button2.Enabled = false;
                button3.Enabled = false;
                
            }

            else
            {
                button2.Enabled = true;
                button3.Enabled = true;
                
            }
        }

        private void bgFloor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            isactive();
        }

        private void isactive()
        {
            try
            {
                if (bgFloor.SelectedRows[0].Cells[3].Value.ToString() == "False")
                {
                    button2.Enabled = false;
                }
                else
                {
                    button2.Enabled = true;
                }
            }
            catch (Exception) { }
        
        }

        private void bgFloor_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            isactive();
        }

        private void bgFloor_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            isactive();
        }
    
    }
}
