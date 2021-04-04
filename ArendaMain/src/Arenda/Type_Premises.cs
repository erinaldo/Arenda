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
    public partial class Type_Premises : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        DataTable TypePrem = null;

        public Type_Premises()
        {
            InitializeComponent();

            if (TempData.Rezhim.ToLower().Equals("пр"))
            {
                Logging.StartFirstLevel(1394);
                Logging.Comment("Открыта форма просмотра справочника типов помещений");

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
            var addedit = new AddEditTypePremis();
            addedit.ShowDialog();
            ini();
            isactive();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var addedit = new AddEditTypePremis(bgTypePrem.SelectedRows[0].Cells["cName"].Value.ToString(), bgTypePrem.SelectedRows[0].Cells["tId"].Value.ToString(), true);
                addedit.ShowDialog();
            }
            catch (Exception r) { MessageBox.Show("Нет записей для изменения." + "\n" + r.ToString(), "Ошибка"); }
            ini();
            isactive();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string _cName = bgTypePrem.SelectedRows[0].Cells["cName"].Value.ToString();
                int zid = Convert.ToInt32(bgTypePrem.SelectedRows[0].Cells["tId"].Value);

                string rez = _proc.isActiveTypePr(zid).Rows[0][0].ToString();
                if (rez == "False")
                {
                    {
                        //if (MessageBox.Show("Сделать запись снова активной?", "Внимание", MessageBoxButtons.YesNo,
                        //MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        if (MessageBox.Show("Сделать выбранную запись действующей?",
                          "Восстановление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                          MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            _proc.ChgTypePr(zid, _cName, 1);

                            Logging.StartFirstLevel(540);
                            Logging.Comment("Произведена смена статуса на активный у помещения");
                            Logging.Comment("ID: " + zid);
                            Logging.Comment("Наименование помещения: " + _cName);

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
                    string chk = _proc.befTypePr(zid).Rows[0][0].ToString();
                    if (chk == "0")
                    {
                        if (MessageBox.Show("Удалить выбранную запись?", "Удаление записи",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                          MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            _proc.delTypePr(zid);

                            Logging.StartFirstLevel(1375);
                            Logging.Comment("ID: " + zid);
                            Logging.Comment("Наименование помещения: " + _cName);

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
                            _proc.ChgTypePr(zid, _cName, 0);

                            Logging.StartFirstLevel(540);
                            Logging.Comment("Произведена смена статуса на неактивный  у помещения");
                            Logging.Comment("ID: " + zid);
                            Logging.Comment("Наименование помещения: " + _cName);

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

        private void Type_Premises_Load(object sender, EventArgs e)
        {
            ini();
        }

        private void bgFloor_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (TypePrem.DefaultView[e.RowIndex]["isActive"].ToString() == "False")
            {
                bgTypePrem.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Coral;
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
                TypePrem = _proc.GetTypePr(3);
            }

            else { TypePrem = _proc.GetTypePr(1); }
            bgTypePrem.AutoGenerateColumns = false;
            bds.DataSource = TypePrem;
            bgTypePrem.DataSource = bds;
            if (bgTypePrem.Rows.Count == 0)
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

        private void checAll_CheckedChanged(object sender, EventArgs e)
        {
            ini();
            isactive();
        }

        private void bgTypePrem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            isactive();
        }

        private void isactive()
        {
            if (bgTypePrem.SelectedRows == null || bgTypePrem.SelectedRows.Count == 0)
            {
                button2.Enabled = false;
                return;
            }

            try
            {
                if (bgTypePrem.SelectedRows[0].Cells[2].Value.ToString() == "False")
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

        private void bgTypePrem_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            isactive();
        }

        private void bgTypePrem_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            isactive();
        }
    }
}
