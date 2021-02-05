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
    public partial class frmAddEditAnotherPayments : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        string mode;
        int id = 0;
        string cname = "";

        public frmAddEditAnotherPayments()
        {
            InitializeComponent();
            mode = "add";
        }

        public frmAddEditAnotherPayments(int _id, string _cname)
        {
            InitializeComponent();
            mode = "edit";
            id = _id;
            cname = _cname.Trim();
        }

        private void frmAddEditAnotherPayments_Load(object sender, EventArgs e)
        {
            if (mode == "add")
            {
                this.Text = "Добавить дополнительную оплату";
            }
            else
            {
                this.Text = "Редактировать дополнительную оплату";
            }

            txtCname.Text = cname;
        }

        private void txtCname_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = txtCname.Text.Trim().Length > 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (new_id == -1)
            //{
            //MessageBox.Show("Сохраняемая дополнительная \nоплата присутствует в БД. \nСохранение невозможно.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
            //else

            DataTable dtap = _proc.CheckAddPaymentName(id, txtCname.Text.Trim());
            if (dtap == null || dtap.Rows.Count == 0)
            {
                int new_id = _proc.AddEditAnotherPayments(id, txtCname.Text.Trim());
                if (id == 0)
                {
                    Logging.StartFirstLevel(1388);
                    Logging.Comment("ID: " + new_id);
                    Logging.Comment("Наименование дополнительной оплаты: " + txtCname.Text.Trim());

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                      + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();
                }
                else
                {
                    Logging.StartFirstLevel(1389);
                    Logging.Comment("ID: " + id);
                    Logging.VariableChange("Наименование дополнительной оплаты: ", txtCname.Text.Trim(), cname);

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                      + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();
                }

                //MessageBox.Show("Данные сохранены!");
                MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                if (bool.Parse(dtap.Rows[0]["isActive"].ToString()))
                {
                    MessageBox.Show("                В справочнике уже\nприсутствует дополнительная оплата\n           с таким наименованием.",
                      "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (id == 0)
                {
                    if (MessageBox.Show("Введённое наименование присутствует в БД и имеет статус \"недействующая\". Вы хотите изменить статус на \"действующая\"?",
                      "Сохранение записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                      MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {

                        Logging.StartFirstLevel(540);

                        Logging.Comment($"Смена статуса дополнительной оплаты на действующий");
                        Logging.Comment($"ID:{dtap.Rows[0]["id"]}");

                        Logging.Comment($"Наименование дополнительной оплаты: {dtap.Rows[0]["cName"]}");

                        Logging.StopFirstLevel();


                        _proc.DelAnotherPayments(int.Parse(dtap.Rows[0]["id"].ToString()), 1);
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (txtCname.Text.Trim() == cname.Trim())
            {
                this.Close();
            }
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
    }
}
