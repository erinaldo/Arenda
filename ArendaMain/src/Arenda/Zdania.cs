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
    public partial class Zdania : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        DataTable Zdan = null;
        DataTable _Zdan = null;
        public Zdania()
        {
            InitializeComponent();
            bgZdania.AutoGenerateColumns = false;
            if (bgZdania.Rows.Count == 0)
            {
                button2.Enabled = false;
                button3.Enabled = false;                
            }
            else
            {
                button2.Enabled = true;
                button3.Enabled = true;                
            }

            if (TempData.Rezhim.ToLower().Equals("пр"))
            {
                Logging.StartFirstLevel(1394);
                Logging.Comment("Открыта форма просмотра справочника зданий");

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void checAll_CheckedChanged(object sender, EventArgs e)
        {
            ini();
            isactive();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var addedit = new AddEditZdanie();
            addedit.ShowDialog();
            ini();
            isactive();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (bgZdania.Rows.Count > 0)
            {
                try
                {
                    var addedit = new AddEditZdanie(bgZdania.SelectedRows[0].Cells[0].Value.ToString(), bgZdania.SelectedRows[0].Cells[1].Value.ToString(), bgZdania.SelectedRows[0].Cells[2].Value.ToString(), true);

                    addedit.ShowDialog();
                }
                catch (Exception r)
                {
                    MessageBox.Show("Нет записей для редактирования." + "\n" + r.ToString(), "Ошибка");
                }
            }
            ini();
            isactive();
        }        

        private void Zdania_Load(object sender, EventArgs e)
        {
            ini();
        }
      
      private void button3_Click(object sender, EventArgs e)
      {
        try
        {
          string _cName = bgZdania.SelectedRows[0].Cells[0].Value.ToString();
          string _Abbr = bgZdania.SelectedRows[0].Cells[1].Value.ToString();
          int zid = Convert.ToInt32(bgZdania.SelectedRows[0].Cells[2].Value);

          string rez = _proc.isActive(Convert.ToInt32(bgZdania.SelectedRows[0].Cells[2].Value.ToString())).Rows[0][0].ToString();
          if (rez == "False")
          {
            {
              //if (MessageBox.Show("Сделать запись снова активной?", "Внимание", MessageBoxButtons.YesNo,
                //MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              if (MessageBox.Show("Сделать выбранную запись действующей?",
                "Восстановление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              {
                _proc.ChgZdan(Convert.ToInt32(bgZdania.SelectedRows[0].Cells[2].Value), bgZdania.SelectedRows[0].Cells[0].Value.ToString(), bgZdania.SelectedRows[0].Cells[1].Value.ToString(), 1);

                Logging.StartFirstLevel(540);
                Logging.Comment("Произведена смена статуса на активный у здания");
                Logging.Comment("ID: " + zid);
                Logging.Comment("Наименование здания: "+ _cName);
                Logging.Comment("Аббревиатура здания: "+ _Abbr);

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
            string chk1 = _proc.befDel(Convert.ToInt32(bgZdania.SelectedRows[0].Cells[2].Value.ToString())).Rows[0][0].ToString();
            string chk2 = _proc.befDel(Convert.ToInt32(bgZdania.SelectedRows[0].Cells[2].Value.ToString())).Rows[1][0].ToString();
            if (chk1 == "0" && chk2 == "0")
            {
              if (MessageBox.Show("Удалить выбранную запись?", "Удаление записи",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              {
                _proc.delZdan(Convert.ToInt32(bgZdania.SelectedRows[0].Cells[2].Value.ToString()));

                Logging.StartFirstLevel(1369);
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
                _proc.ChgZdan(Convert.ToInt32(bgZdania.SelectedRows[0].Cells[2].Value), bgZdania.SelectedRows[0].Cells[0].Value.ToString(), bgZdania.SelectedRows[0].Cells[1].Value.ToString(), 0);

                Logging.StartFirstLevel(540);
                Logging.Comment("Произведена смена статуса на неактивный у здания");
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

        private void bgZdania_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (_Zdan.DefaultView[e.RowIndex]["isActive"].ToString() == "False")
            {
                bgZdania.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Coral;
            }
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
                Zdan = _proc.GetZdan(3);
            }

            else { Zdan = _proc.GetZdan(1); }
            _Zdan = Zdan.DefaultView.ToTable("_Zdan", false, new[] { "cname", "abbreviation", "id", "isActive" });
            bds.DataSource = _Zdan;
            bgZdania.DataSource = bds;
            cname.DataPropertyName = "cname";
            abbreviation.DataPropertyName = "abbreviation";
            ID.DataPropertyName = "id";
            isActive.DataPropertyName = "isActive";

        }

        private void bgZdania_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (bgZdania.Rows.Count == 0)
            {
                button2.Enabled = false;
                button3.Enabled = false;                
            }
            else
            {
                button2.Enabled = true;
                button3.Enabled = true;                
                isactive();
            }
        }

        private void bgZdania_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            isactive();
        }

        private void bgZdania_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            isactive();
        }

        private void isactive()
        {
            try
            {
                if (bgZdania.SelectedRows[0].Cells[3].Value.ToString() == "False")
                {
                    button2.Enabled = false;
                }
                else
                {
                    button2.Enabled = true;
                }
            }
            catch (Exception) {
            }
        }
         

    }

}
