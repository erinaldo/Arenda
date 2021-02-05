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
    public partial class frmAddEditObject : Form
    {
        readonly Procedures proc = new Procedures(ConnectionSettings.GetServer(),
          ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(),
          ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        
        int id_Object;
        string objName, Comment, CadastralNumber;
        public frmAddEditObject(int id, string oname, string com, string CadastralNumber)
        {
            id_Object = id;
            objName = oname;
            Comment = com;
            this.CadastralNumber = CadastralNumber;
            InitializeComponent();
        }

        private void frmAddEditObject_Load(object sender, EventArgs e)
        {
            this.Text = id_Object == 0 ? "Добавить объект аренды" :
              "Редактировать объект аренды";
            tbName.Text = objName;
            tbComment.Text = Comment;
            tbCadastralNumber.Text = CadastralNumber;
            SetButtonsEnabled();
        }

        private void SetButtonsEnabled()
        {
            btSave.Enabled = SomethingChanged();
        }

        private bool SomethingChanged()
        {
            //return (id_Object == 0 && (tbName.Text.Length > 0 || tbComment.Text.Length > 0))
            //|| objName != tbName.Text || Comment != tbComment.Text;
            if (id_Object == 0)
                return (tbName.Text.Replace(" ", "").Length > 0);
            else
                return (tbName.Text.Replace(" ", "").Length > 0 && (objName != tbName.Text || Comment != tbComment.Text || CadastralNumber != tbCadastralNumber.Text));
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            SetButtonsEnabled();
        }

        private void tbComment_TextChanged(object sender, EventArgs e)
        {
            SetButtonsEnabled();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            DataTable dtObj = proc.CheckObjectName(id_Object, tbName.Text);
            if (dtObj == null || dtObj.Rows.Count == 0)
            {
                DataTable dtResult = proc.SaveObject(id_Object, tbName.Text, tbComment.Text, tbCadastralNumber.Text);

                if (id_Object == 0)
                {
                    Logging.StartFirstLevel((int)logEnum.Добавление_объекта_аренды);
                    if (dtResult != null && dtResult.Rows.Count > 0 && dtResult.Columns.Contains("id"))
                        Logging.Comment($"ID:{dtResult.Rows[0]["id"]}");

                    Logging.Comment("Наименование: " + tbName.Text.Trim());
                    Logging.Comment("Примечание: " + tbComment.Text.Trim());
                    Logging.Comment("Кадастровый номер: " + tbCadastralNumber.Text.Trim());

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                      + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();
                }
                else
                {
                    Logging.StartFirstLevel((int)logEnum.Редактирование_объекта_аренды);

                    Logging.Comment($"ID:{id_Object}");

                    Logging.VariableChange("Наименование: ", tbName.Text.Trim(), objName);
                    Logging.VariableChange("Примечание: ", tbComment.Text.Trim(), Comment);
                    Logging.VariableChange("Кадастровый номер: ", tbCadastralNumber.Text.Trim(), CadastralNumber);

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                      + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();
                }

                MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                if (int.Parse(dtObj.Rows[0]["isActive"].ToString()) == 1)
                {
                    //MessageBox.Show("Введённое наименование присутствует в БД и имеет статус \"действующая\". Сохранить введённое наименование нельзя.", "Сохранение записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MessageBox.Show("        В справочнике уже\nприсутствует объект аренды\n   с таким наименованием.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (id_Object == 0)
                {
                    if (MessageBox.Show("Введённое наименование присутствует в БД и имеет статус \"недействующая\". Вы хотите изменить статус на \"действующая\"?", "Сохранение записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {

                        Logging.StartFirstLevel(540);

                        Logging.Comment($"Смена статуса объекта аренды на действующий");
                        Logging.Comment($"ID:{dtObj.Rows[0]["id"]}");

                        Logging.Comment($"Наименование: {dtObj.Rows[0]["cName"]}");
                        Logging.Comment($"Аббревиатура: {dtObj.Rows[0]["Comment"]}");
                        Logging.Comment($"Кадастровый номер {dtObj.Rows[0]["CadastralNumber"]}");

                        Logging.StopFirstLevel();

                        proc.ChangeObjectActiveStatus(int.Parse(dtObj.Rows[0]["id"].ToString()), true, true, tbComment.Text);
                        MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btExit_Click(object sender, EventArgs e)
        {
            if (SomethingChanged() && MessageBox.Show(" На форме есть несохранённые данные.\nЗакрыть форму без сохранения данных?",
              "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }
            this.DialogResult = DialogResult.Cancel;
        }

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
