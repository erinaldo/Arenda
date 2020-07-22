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
    public partial class AddEditTypePremis : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        bool Chk = false;
        int zid;
        string cName;
        public AddEditTypePremis()
        {
            InitializeComponent();
        }

        public AddEditTypePremis(string cname, string id, bool red)
        {
            InitializeComponent();
            cName = cname;
            tbCname.Text = cname;
            Chk = red;
            zid = Convert.ToInt32(id);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ("" == tbCname.Text )
            { DialogResult = DialogResult.Cancel; }
            else 
            if (tbCname.Text != cName)
            {
                //if (MessageBox.Show("Были внесены изменения. Выйти без сохранения?", "Внимание", MessageBoxButtons.YesNo,
                  //                     MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              if (MessageBox.Show(" На форме есть несохранённые данные.\nЗакрыть форму без сохранения данных?",
                "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              { DialogResult = DialogResult.Cancel; }
            }
            else { DialogResult = DialogResult.Cancel; }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (Chk == false)
            {
                if (_proc.CheakAll(tbCname.Text, "tp").Rows.Count != 0)
                {
                    int uniqRec = Convert.ToInt32(_proc.CheakAll(tbCname.Text, "tp").Rows[0][0].ToString());
                    string rez = _proc.isActiveTypePr(uniqRec).Rows[0][0].ToString().ToString();
                    if (rez == "False")
                    {
                        if (MessageBox.Show("Уже существует неактивная запись с таким наименованием!сделать запись активной?", "Внимание", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            _proc.ActiveSprav("typeprem", uniqRec, 1);
                            DialogResult = DialogResult.Cancel;
                        }

                    }
                    else { MessageBox.Show("Запись с таким наименованием уже существует!", "Внимание"); }
                }
                else
                {
                    _proc.AddTypePr(tbCname.Text);

                    Logging.StartFirstLevel(1373);
                    Logging.Comment("Наименование помещения: " + tbCname.Text.Trim());                    

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();

                    //MessageBox.Show("Запись добавлена");
                    MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbCname.Clear();

                    DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                if (cName == tbCname.Text)
                {
                    _proc.ChgTypePr(zid, tbCname.Text, 1);

                    Logging.StartFirstLevel(1374);
                    Logging.Comment("ID: " + zid);
                    Logging.VariableChange("Наименование этажа: ", tbCname.Text.Trim(), cName);                    

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();

                    MessageBox.Show("Запись изменена");
                    tbCname.Clear();
                    DialogResult = DialogResult.Cancel;
                }
                else
                {
                    if (_proc.CheakAll(tbCname.Text, "tp").Rows.Count != 0)
                    { MessageBox.Show("Запись с таким наименованием уже существует!", "Внимание"); }
                    else
                    {
                        _proc.ChgTypePr(zid, tbCname.Text, 1);

                        Logging.StartFirstLevel(1374);
                        Logging.Comment("ID: " + zid);
                        Logging.VariableChange("Наименование этажа: ", tbCname.Text.Trim(), cName);

                        Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                        + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                        Logging.StopFirstLevel();

                        MessageBox.Show("Запись изменена");
                        tbCname.Clear();
                        DialogResult = DialogResult.Cancel;
                    }
                }
            }
        }


        private void check()
        {
            if (tbCname.Text.Trim().Length != 0)
            { btAdd.Enabled = true; }
            else btAdd.Enabled = false;
        }

        private void AddEditTypePremis_Load(object sender, EventArgs e)
        {
            if (TempData.Rezhim == "ПР")
            {
                this.Text = "Просмотр";
                btAdd.Visible = false;

                foreach (Control con in this.Controls)
                {
                    if (con.Name != "button1")
                        con.Enabled = false;
                    else
                        con.Enabled = true;
                }
            }
            else
            {
                btAdd.Visible = true;
            }

            check();
        }

        private void tbCname_TextChanged(object sender, EventArgs e)
        {
            check();
        }

    }
}
