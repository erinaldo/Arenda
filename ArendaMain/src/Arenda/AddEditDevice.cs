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
    public partial class AddEditDevice : Form
    {
        private Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        private Device device = null;

        public AddEditDevice(Device device)
        {
            this.device = device;
            InitializeComponent(); 
        }

        private void AddEditDevice_Load(object sender, EventArgs e)
        {
            this.Text = device.Id == 0 ? "Добавить запись" : "Редактировать запись";
            if (device.Id != 0)
            {
                txtName.Text = device.Name;
                txtAbbr.Text = device.Abbreviation;
                txtUnit.Text = device.Unit;
            }
            SetButtonsEnabled();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //if (!SomethingChanged() || MessageBox.Show("На форме остались несохранённые изменения. Выйти без сохранения?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
          if (!SomethingChanged() || MessageBox.Show(" На форме есть несохранённые данные.\nЗакрыть форму без сохранения данных?",
            "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
          {
            this.DialogResult = DialogResult.Cancel;
          }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
          DataTable dtd = proc.CheckDeviceName(device.Id, txtName.Text);
            if (dtd == null || dtd.Rows.Count == 0)
            {
             int idNew =   proc.SaveDevice(device.Id, txtName.Text, txtAbbr.Text, txtUnit.Text);

                if (device.Id == 0)
                {
                    Logging.StartFirstLevel(1391);

                    Logging.Comment($"ID:{idNew}");
                    Logging.Comment("Наименование прибора: " + txtName.Text.Trim());
                    Logging.Comment("Аббревиатура прибора: " + txtAbbr.Text.Trim());
                    Logging.Comment("Единицы измерения: " + txtUnit.Text.Trim());

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();
                }
                else
                {
                    Logging.StartFirstLevel(1392);
                    Logging.Comment("ID: " + device.Id);
                    Logging.VariableChange("Наименование этажа: ", txtName.Text.Trim(), device.Name);
                    Logging.VariableChange("Аббревиатура этажа: ", txtAbbr.Text.Trim(), device.Abbreviation);
                    Logging.VariableChange("Единицы измерения: ", txtUnit.Text.Trim(), device.Unit);

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();
                }

                MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                if (bool.Parse(dtd.Rows[0]["isActive"].ToString()))
                {
                    //MessageBox.Show("Введённое наименование присутствует в БД и имеет статус \"действующая\". Сохранить введённое наименование нельзя.", "Сохранение записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MessageBox.Show("        В справочнике уже\n      присутствует прибор\n   с таким наименованием.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (device.Id == 0)
                {
                    if (MessageBox.Show("Введённое наименование присутствует в БД и имеет статус \"недействующий\". Вы хотите изменить статус на \"действующий\"?", "Сохранение записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        Logging.StartFirstLevel(540);
                        Logging.Comment($"Смена статуса прибора на действующий");

                        Logging.Comment($"ID:{dtd.Rows[0]["id"]}");
                        Logging.Comment($"Наименование прибора: {dtd.Rows[0]["cName"]}");
                        Logging.Comment($"Аббревиатура прибора: {dtd.Rows[0]["Abbreviation"]}");
                        Logging.Comment($"Единицы измерения: {dtd.Rows[0]["Unit"]}");

                        proc.RestoreDevice(int.Parse(dtd.Rows[0]["id"].ToString()), true, false);
                        MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    MessageBox.Show("Введённое наименование присутствует в БД и имеет статус \"недействующий\". Сохранить введённое наименование нельзя.",
                      "Сохранение записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private bool SomethingChanged()
        {
            return device != null && (txtName.Text != device.Name || txtAbbr.Text != device.Abbreviation || txtUnit.Text != device.Unit);
        }

        private void SetButtonsEnabled()
        {
            btnSave.Enabled = SomethingChanged() && txtName.Text.Length > 0 && txtAbbr.Text.Length > 0 && txtUnit.Text.Length > 0;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            SetButtonsEnabled();
        }
    }
}
