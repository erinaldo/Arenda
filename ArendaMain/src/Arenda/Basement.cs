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
    public partial class Basement : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        DataTable tBas;
        public Basement()
        {
            InitializeComponent();
            bgBase.AutoGenerateColumns = false;

            if (Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.ToLower().Equals("пр"))
            {
                Logging.StartFirstLevel(1394);
                Logging.Comment("Открыта форма просмотра справочника оснований заключения договоров");

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }
        }

        private void btAddtoo_Click(object sender, EventArgs e)
        {
            var aebas = new AddEditBasement();
            aebas.ShowDialog();
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
                tBas = _proc.GetBas(0);

            else tBas = _proc.GetBas(1);
            bds.DataSource = tBas;
            bgBase.DataSource = bds;
            id.DataPropertyName = "id";
            cName.DataPropertyName = "cName";
            abbr.DataPropertyName = "Abbreviation";
            isActive.DataPropertyName = "isActive";

            if (bgBase != null)
            {
                if (bgBase.Rows.Count != 0)
                {
                    bgBase.ClearSelection();
                    bgBase.Rows[0].Selected = true;
                }
            }

            isactive();
        }

        private void tbEdit_Click(object sender, EventArgs e)
        {
            var aebas = new AddEditBasement(Convert.ToInt32(bgBase.SelectedRows[0].Cells[0].Value), bgBase.SelectedRows[0].Cells[1].Value.ToString(), bgBase.SelectedRows[0].Cells[2].Value.ToString(), 0, Convert.ToInt32(bgBase.SelectedRows[0].Cells["needDate"].Value));
            aebas.ShowDialog();
            ini();            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void checAll_CheckedChanged(object sender, EventArgs e)
        {
            ini();
        }

        private void bgBase_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (tBas.DefaultView[e.RowIndex]["isActive"].ToString() == "False")
            {
                bgBase.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Coral;
            }
            else
            {
                if (checkBox1.Checked == true && tBas.DefaultView[e.RowIndex]["needDate"].ToString() == "True")
                {
                    bgBase.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
                }
                else
                {
                    bgBase.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

      private void btDel_Click(object sender, EventArgs e)
      {
        try
        {
          string _cName = bgBase.SelectedRows[0].Cells[1].Value.ToString();
          string _Abbr = bgBase.SelectedRows[0].Cells[2].Value.ToString();
          int zid = Convert.ToInt32(bgBase.SelectedRows[0].Cells[0].Value);
          int _need = Convert.ToInt32(bgBase.SelectedRows[0].Cells["needDate"].Value);

          string rez = bgBase.SelectedRows[0].Cells["isActive"].Value.ToString();
          if (rez == "False")
          {
            {
              //if (MessageBox.Show("Сделать запись снова активной?", "Внимание", MessageBoxButtons.YesNo,
                //MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              if (MessageBox.Show("Сделать выбранную запись действующей?",
                "Восстановление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              {
                Logging.StartFirstLevel(540);
                Logging.Comment("Произведена смена статуса на активный у основания заключения договоров");
                Logging.Comment("ID: " + zid);
                Logging.Comment("Наименование основания заключения договоров: " + _cName);
                Logging.Comment("Аббревиатура основания заключения договоров: " + _Abbr);
                Logging.Comment("Наличие номера и даты у договора: " + (_need==1 ? "Да" : "Нет"));

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                  + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
                
                ChangeActive();
              }
            }
          }
          else
          {
            //if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Внимание", MessageBoxButtons.YesNo,
            //                  MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            string chk = _proc.BefBas(Convert.ToInt32(bgBase.SelectedRows[0].Cells[0].Value.ToString())).Rows[0][0].ToString() ;
            if (chk == "0")
            {
              if (MessageBox.Show("Удалить выбранную запись?", "Удаление записи",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              {
                _proc.DelBas(Convert.ToInt32(bgBase.SelectedRows[0].Cells[0].Value.ToString()));

                Logging.StartFirstLevel(1381);
                Logging.Comment("ID: " + zid);
                Logging.Comment("Наименование основания заключения договоров: " + _cName);
                Logging.Comment("Аббревиатура основания заключения договоров: " + _Abbr);
                Logging.Comment("Наличие номера и даты у договора: " + (_need == 1 ? "Да" : "Нет"));

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
                Logging.StartFirstLevel(540);
                Logging.Comment("Произведена смена статуса на неактивный у основания заключения договоров");
                Logging.Comment("ID: " + zid);
                Logging.Comment("Наименование основания заключения договоров: " + _cName);
                Logging.Comment("Аббревиатура основания заключения договоров: " + _Abbr);
                Logging.Comment("Наличие номера и даты у договора: " + (_need == 1 ? "Да" : "Нет"));

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                  + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();

                ChangeActive();
              }
            }
          }
          ini();
        }
        catch (Exception r) { MessageBox.Show("Нет записей для удаления." + "\n" + r.ToString(), "Ошибка"); }
      }

        private void ChangeActive()
        {
            _proc.AddEditBas(Convert.ToInt32(bgBase.SelectedRows[0].Cells["id"].Value),
                                    bgBase.SelectedRows[0].Cells["cName"].Value.ToString(),
                                    bgBase.SelectedRows[0].Cells["abbr"].Value.ToString(),
                                    0,
                                    bgBase.SelectedRows[0].Cells["isActive"].Value.ToString() == "True" ? 0 : 1,
                                    Convert.ToInt32(bgBase.SelectedRows[0].Cells["needDate"].Value));
        }

        private void bgBase_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (bgBase.Rows.Count == 0)
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

        private void isactive()
        {
            //if (bgBase != null)
            //{
            //    if (bgBase.Rows.Count == 0)
            //    {
            //    }
            //    else
            //    {
            //        try
            //        {
            //            if (bgBase.SelectedRows[0].Cells["isActive"].Value.ToString() == "False")
            //            {
            //                tbEdit.Enabled = false;
            //            }
            //            else
            //            {
            //                tbEdit.Enabled = true;
            //            }
            //        }
            //        catch
            //        {
            //        }
            //    }
            //}

            if (bgBase.Rows.Count == 0)
            {
                tbEdit.Enabled = false;
                btDel.Enabled = false;
            }
            else
            {
                try
                {
                    if (bgBase.SelectedRows[0].Cells["isActive"].Value.ToString() == "False")
                        tbEdit.Enabled = false;
                    else
                        tbEdit.Enabled = true;
                }
                catch { }                
                btDel.Enabled = true;
            }
        }

        private void bgBase_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            isactive();
        }

        private void bgBase_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            isactive();
        }

        private void bgBase_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            isactive();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bgBase.Refresh();
        }

        private void Basement_Load(object sender, EventArgs e)
        {
            ini();
        }
    
    
    }
}
