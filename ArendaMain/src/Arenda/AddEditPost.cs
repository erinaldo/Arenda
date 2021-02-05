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
    public partial class AddEditPost : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        int _id, _isActive;
        string _cName, _Dative_case,_prz;
        public AddEditPost(string prz)
        {
            InitializeComponent();
            if (prz == "add")
            { this.Text = "Добавить запись"; }
            else { this.Text = "Редактировать запись"; }
            _id = 0;
            _cName = "";
            _isActive = 1;
            _Dative_case = "";
            _prz = prz;

        }

        public AddEditPost(int id, string cName, int isActive, string Dative_case)
        {
            InitializeComponent();
             this.Text = "Редактировать запись";
             _id = id;
             _cName = cName.Trim();
             _isActive = isActive;
             _Dative_case = Dative_case.Trim();
             tbCname.Text = _cName;
             tbDativeCname.Text = _Dative_case;            
        }


        private void AddEditPost_Load(object sender, EventArgs e)
        {
            ButtonSaveAvailability();            
        }

        private void AddEditPost_Shown(object sender, EventArgs e)
        {
            tbCname.Focus();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if ((tbCname.Text.Trim() == _cName) && (tbDativeCname.Text.Trim() == _Dative_case))
                this.Close();
            else
            {
                //DialogResult d = MessageBox.Show("Есть несохраненные данные. Выйти без сохранения?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
              DialogResult d = MessageBox.Show(" На форме есть несохранённые данные.\nЗакрыть форму без сохранения данных?",
                "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
              if (d == DialogResult.Yes)
              {
                this.Close();
              }
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            DataTable dtp = _proc.CheckPosts(_id, tbCname.Text.Trim());
            //if (_proc.CheckPosts(tbCname.Text.Trim(),tbDativeCname.Text.Trim()).Rows.Count>0)
            //{
            //  MessageBox.Show("Запись с такими параметрами уже существует", "Внимание");
            //}
            //else
            if (dtp == null || dtp.Rows.Count == 0)
            {
                if (_prz == "add")
                {
                    DataTable dtResult = _proc.addeditPost(0, tbCname.Text.Trim(), _isActive, tbDativeCname.Text.Trim());

                    Logging.StartFirstLevel(1382);
                    if (dtResult != null && dtResult.Rows.Count > 0 && dtResult.Columns.Contains("id"))
                        Logging.Comment($"ID:{dtResult.Rows[0]["id"]}");

                    Logging.Comment("Наименование должности: " + tbCname.Text.Trim());
                    Logging.Comment("Наименование должности в дательном падеже: " + tbDativeCname.Text.Trim());

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                      + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();
                }
                else
                {
                    _proc.addeditPost(_id, tbCname.Text, _isActive, tbDativeCname.Text.Trim());

                    Logging.StartFirstLevel(1383);
                    Logging.Comment("ID: " + _id);
                    Logging.VariableChange("Наименование должности: ", tbCname.Text.Trim(), _cName);
                    Logging.VariableChange("Наименование должности в дательном падеже: ", tbDativeCname.Text.Trim(), _Dative_case);

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                      + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();
                }
                //MessageBox.Show("Данные внесены", "Внимание");
                MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                if (bool.Parse(dtp.Rows[0]["isActive"].ToString()))
                {
                    //MessageBox.Show("Введённое наименование присутствует в БД и имеет статус \"действующая\". Сохранить введённое наименование нельзя.", "Сохранение записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MessageBox.Show("    В справочнике уже\nприсутствует должность\nс таким наименованием.",
                      "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (_id == 0)
                {
                    if (MessageBox.Show("Введённое наименование присутствует в БД и имеет статус \"недействующая\". Вы хотите изменить статус на \"действующая\"?",
                      "Сохранение записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                      MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {

                        Logging.StartFirstLevel(540);
                        Logging.Comment($"Смена статуса должности на действующий");

                        Logging.Comment($"Наименование должности: {dtp.Rows[0]["cName"]}");
                        Logging.Comment($"Аббревиатура должности в дательном падеже: {dtp.Rows[0]["Dative_case"]}");

                        Logging.StopFirstLevel();

                        _proc.ActiveSprav("pos", int.Parse(dtp.Rows[0]["id"].ToString()), 1);
                        MessageBox.Show("Данные сохранены.", "Сохранение данных",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    MessageBox.Show("Введённое наименование присутствует в БД и имеет статус \"недействующая\". Сохранить введённое наименование нельзя.",
                      "Сохранение записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void tbCname_TextChanged(object sender, EventArgs e)
        {
            ButtonSaveAvailability();
        }

        private void tbDativeCname_TextChanged(object sender, EventArgs e)
        {
            ButtonSaveAvailability();
        }

        private void ButtonSaveAvailability()
        {
            if ((tbCname.Text.Trim() == "") || (tbDativeCname.Text.Trim() == ""))
            {
                btAdd.Enabled = false;
            }
            else btAdd.Enabled = true;
        }

    }
}
