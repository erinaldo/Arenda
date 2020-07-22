using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Logging;
namespace Arenda
{
    public partial class Equipment : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(),
          ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(),
          ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        DataTable Equip = null;
        public Equipment()
        {
            InitializeComponent();
            if (bgEquipment.Rows.Count == 0)
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

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
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
            else
            {
                button1.Visible
                  = button2.Visible
                  = button3.Visible = new List<string> { "РКВ" }.Contains(TempData.Rezhim);
                  //= _proc.SuperUserMode();
            }

            if (checAll.Checked == true)
            {
                Equip = _proc.GetEquip(3);
            }
            else
            {
                Equip = _proc.GetEquip(1);
            }
            //_Zdan = Zdan.DefaultView.ToTable("_Zdan", false, new[] { "cname", "abbreviation", "id", "isActive" });
            //bds.DataSource = Equip;
            bgEquipment.AutoGenerateColumns = false;
            bgEquipment.DataSource = Equip;//bds;
            cname.DataPropertyName = "cname";
            abbreviation.DataPropertyName = "abbreviation";
            ID.DataPropertyName = "id";
            isActive.DataPropertyName = "isActive";
        }

        private void checAll_CheckedChanged(object sender, EventArgs e)
        {
            ini();
            isactive();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var addEquip = new AddEquipment();

            addEquip.ShowDialog();
            ini();
            isactive();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var addEquip = new AddEquipment(bgEquipment.SelectedRows[0].Cells["id"].Value.ToString(),
                  bgEquipment.SelectedRows[0].Cells["cname"].Value.ToString(),
                  bgEquipment.SelectedRows[0].Cells["abbreviation"].Value.ToString(), true);

                addEquip.ShowDialog();
            }
            catch (Exception r)
            {
                MessageBox.Show("Нет записей для редактирования." + "\n" + r.ToString(), "Ошибка");
            }
            ini();
            isactive();
        }

        private void Equipment_Load(object sender, EventArgs e)
        {
            ini();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string rez = _proc.isActiveEquip(Convert.ToInt32(bgEquipment.SelectedRows[0].Cells["id"].Value.ToString())).Rows[0][0].ToString();
                if (rez == "False")
                {
                    {
                        if (MessageBox.Show("Сделать запись снова активной?", "Внимание", MessageBoxButtons.YesNo,
                          MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            string logEvent = "Смена статуса оборудования";

                            Logging.StartFirstLevel(764);
                            Logging.Comment(logEvent);
                            Logging.Comment("id = " + bgEquipment.SelectedRows[0].Cells["id"].Value.ToString());
                            Logging.Comment("Наименование оборудования: \""
                              + bgEquipment.SelectedRows[0].Cells["cname"].Value.ToString() + "\"");
                            Logging.Comment("Аббревиатура оборудования: \""
                              + bgEquipment.SelectedRows[0].Cells["abbreviation"].Value.ToString() + "\"");
                            Logging.Comment("Статус изменен на активный");
                            Logging.Comment("Завершение операции \"" + logEvent + "\"");
                            Logging.StopFirstLevel();

                            _proc.ChgEquip(Convert.ToInt32(bgEquipment.SelectedRows[0].Cells["id"].Value), bgEquipment.SelectedRows[0].Cells["cname"].Value.ToString(), bgEquipment.SelectedRows[0].Cells["abbreviation"].Value.ToString(), 1);
                        }
                    }
                }
                else
                  //if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Внимание", MessageBoxButtons.YesNo,
                  //                  MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                  if (MessageBox.Show("Удалить выбранную запись?", "Удаление записи",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    string cheas = _proc.befEquip(Convert.ToInt32(bgEquipment.SelectedRows[0].Cells["id"].Value.ToString())).Rows[0][0].ToString();

                    if (cheas == "0")
                    {
                        string logEvent = "Удаление оборудования из справочника";

                        Logging.StartFirstLevel(756);
                        Logging.Comment(logEvent);
                        Logging.Comment("id = " + bgEquipment.SelectedRows[0].Cells["id"].Value.ToString());
                        Logging.Comment("Наименование оборудования: \"" + bgEquipment.SelectedRows[0].Cells["cname"].Value.ToString() + "\"");
                        Logging.Comment("Аббревиатура оборудования: \"" + bgEquipment.SelectedRows[0].Cells["abbreviation"].Value.ToString() + "\"");
                        Logging.Comment("Завершение операции \"" + logEvent + "\"");
                        Logging.StopFirstLevel();

                        _proc.delEquip(Convert.ToInt32(bgEquipment.SelectedRows[0].Cells["id"].Value.ToString()));
                    }
                    else
                    {
                        if (MessageBox.Show("Удаляемая запись используется и ее невозможно удалить. Сделать ее неактивной?",
                          "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error,
                          MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            string logEvent = "Смена статуса оборудования";

                            Logging.StartFirstLevel(764);
                            Logging.Comment(logEvent);
                            Logging.Comment("id = " + bgEquipment.SelectedRows[0].Cells["id"].Value.ToString());
                            Logging.Comment("Наименование оборудования: \"" + bgEquipment.SelectedRows[0].Cells["cname"].Value.ToString() + "\"");
                            Logging.Comment("Аббревиатура оборудования: \"" + bgEquipment.SelectedRows[0].Cells["abbreviation"].Value.ToString() + "\"");
                            Logging.Comment("Статус изменен на неактивный");
                            Logging.Comment("Завершение операции \"" + logEvent + "\"");
                            Logging.StopFirstLevel();

                            _proc.ChgEquip(Convert.ToInt32(bgEquipment.SelectedRows[0].Cells["id"].Value), bgEquipment.SelectedRows[0].Cells["cname"].Value.ToString(), bgEquipment.SelectedRows[0].Cells["abbreviation"].Value.ToString(), 0);
                        }
                    }
                }
                ini();
                isactive();
            }
            catch (Exception r)
            {
                MessageBox.Show("Нет записей для удаления " + r.Message + "", "Ошибка");
            }
        }

        private void bgEquipment_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (Equip.DefaultView[e.RowIndex]["isActive"].ToString() == "False")
            {
                bgEquipment.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Coral;
            }
        }

        private void bgEquipment_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (bgEquipment.Rows.Count == 0)
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

        private void bgEquipment_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            isactive();
        }

        private void isactive()
        {
            try
            {
                if (bgEquipment.CurrentRow.Cells["isActive"].Value.ToString() == "False")
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

        private void bgEquipment_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            isactive();
        }
    }
}
