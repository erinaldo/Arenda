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
    public partial class AddEditZdanie : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        bool Chk = false;
        int zid;
        string cName;
        string Abbr;
        public AddEditZdanie()
        {
            InitializeComponent();
             Chk = false;
        }

        public AddEditZdanie(string cname, string abbr, string id, bool red)
        {
            InitializeComponent();
            cName = cname;
            tbCname.Text = cName;
            tbAbbr.Text = abbr;
            Abbr = abbr;
            Chk = red;
            zid = Convert.ToInt32(id);
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if ("" == tbCname.Text && "" == tbAbbr.Text)
            { DialogResult = DialogResult.Cancel; }
            else 
            if (cName != tbCname.Text || tbAbbr.Text != Abbr)
            {
                //if (MessageBox.Show("Были внесены изменения. Выйти без сохранения?", "Внимание", MessageBoxButtons.YesNo,
                  //                     MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              if (MessageBox.Show(" На форме есть несохранённые данные.\nЗакрыть форму без сохранения данных?",
                "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              { DialogResult = DialogResult.Cancel; }
            }
            else { DialogResult = DialogResult.Cancel; } 
            }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Chk == false)
            {
                DataTable dtBil = _proc.CheakAll(tbCname.Text, "bl");
                if (dtBil.Rows.Count != 0)
                {
                    int uniqRec = Convert.ToInt32(dtBil.Rows[0]["id"].ToString());
                    string rez = _proc.isActive(uniqRec).Rows[0][0].ToString().ToString();
                    if (rez == "False")
                    {
                        if (MessageBox.Show("Уже существует неактивная запись с таким наименованием!сделать запись активной?", "Внимание", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {

                            Logging.StartFirstLevel(540);

                            Logging.Comment($"Произведена смена статуса на активный у здания");
                            Logging.Comment($"ID:{dtBil.Rows[0]["id"]}");
                            Logging.Comment($"Наименование здания: {dtBil.Rows[0]["cName"]}");
                            Logging.Comment($"Аббревиатура здания: {dtBil.Rows[0]["Abbreviation"]}");
                            
                            Logging.StopFirstLevel();

                            _proc.ActiveSprav("build", uniqRec, 1);
                            DialogResult = DialogResult.Cancel;
                        }

                    }
                    else { MessageBox.Show("Запись с таким наименованием уже существует!", "Внимание"); }
                }
                else
                {

                    DataTable dtResult = _proc.AddZdan(tbCname.Text, tbAbbr.Text);

                    Logging.StartFirstLevel(1367);
                    if (dtResult != null && dtResult.Rows.Count > 0 && dtResult.Columns.Contains("id"))
                        Logging.Comment($"ID:{dtResult.Rows[0]["id"]}");

                    Logging.Comment("Наименование здания: " + tbCname.Text.Trim());
                    Logging.Comment("Аббревиатура здания: " + tbAbbr.Text.Trim());

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();

                   
                    //MessageBox.Show("Запись добавлена");
                    MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbCname.Clear();
                    tbAbbr.Clear();
                    DialogResult = DialogResult.Cancel;
                }
            }
            else {

                if (cName == tbCname.Text)
                {
                    Logging.StartFirstLevel(1368);
                    Logging.Comment("ID: " + zid);
                    Logging.VariableChange("Наименование здания: " ,tbCname.Text.Trim(), cName);
                    Logging.VariableChange("Аббревиатура здания: " , tbAbbr.Text.Trim(),Abbr);

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();

                    _proc.ChgZdan(zid, tbCname.Text, tbAbbr.Text, 1);
                    MessageBox.Show("Запись изменена");
                    tbCname.Clear();
                    tbAbbr.Clear();
                    DialogResult = DialogResult.Cancel;
                }
                else
                {
                    if (_proc.CheakAll(tbCname.Text, "bl").Rows.Count != 0)
                        MessageBox.Show("Запись с таким наименованием уже существует!", "Внимание");
                    else
                    {
                        Logging.StartFirstLevel(1368);
                        Logging.Comment("ID: " + zid);
                        Logging.VariableChange("Наименование здания: ", tbCname.Text.Trim(), cName);
                        Logging.VariableChange("Аббревиатура здания: ", tbAbbr.Text.Trim(), Abbr);

                        Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                        + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                        Logging.StopFirstLevel();

                        _proc.ChgZdan(zid, tbCname.Text, tbAbbr.Text, 1);
                        MessageBox.Show("Запись изменена");
                        tbCname.Clear();
                        tbAbbr.Clear();
                        DialogResult = DialogResult.Cancel;
                    }
                } 
            }
        }

        private void AddEditZdanie_Load(object sender, EventArgs e)
        {
            check();
        }

        private void tbCname_TextChanged(object sender, EventArgs e)
        {
            check();
        }

        private void tbAbbr_TextChanged(object sender, EventArgs e)
        {
            check();
        }

        private void check()
        {
            if (tbCname.Text.Trim().Length != 0 && tbAbbr.Text.Trim().Length != 0)
            { btAdd.Enabled = true; }
            else btAdd.Enabled = false;
        }
    }
}
