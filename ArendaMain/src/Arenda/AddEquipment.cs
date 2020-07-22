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
    public partial class AddEquipment : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        bool Chk = false;
        int zid;
        string cName;
        string Abbr;
        public AddEquipment()
        {
            InitializeComponent();
            Chk = false;
        }

        public AddEquipment(string id, string cname, string abbr, bool red)
        {
            InitializeComponent();
            tbCname.Text = cname;
            cName = cname;
            tbAbbr.Text = abbr;
            Abbr = abbr;
            Chk = red;
            zid = Convert.ToInt32(id);

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

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (Chk == false)
            {

                if (_proc.CheakAll(tbCname.Text, "eq").Rows.Count != 0)
                {
                    int uniqRec = Convert.ToInt32(_proc.CheakAll(tbCname.Text, "eq").Rows[0][0].ToString());
                    string rez = _proc.isActiveEquip(uniqRec).Rows[0][0].ToString().ToString();
                    if (rez == "False")
                    {
                        if (MessageBox.Show("Уже существует неактивная запись с таким наименованием!сделать запись активной?", "Внимание", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            _proc.ActiveSprav("equipment", uniqRec, 1);

                            string logEvent = "Смена статуса оборудования";

                            Logging.StartFirstLevel(764);
                            Logging.Comment(logEvent);
                            Logging.Comment("id = " + uniqRec.ToString());
                            Logging.Comment("Наименование оборудования: \"" + tbCname.Text + "\"");                            
                            Logging.Comment("Статус изменен на активный");
                            Logging.Comment("Завершение операции \"" + logEvent + "\"");
                            Logging.StopFirstLevel();

                            DialogResult = DialogResult.Cancel;
                        }

                    }
                    else { MessageBox.Show("Запись с таким наименованием уже существует!", "Внимание"); }
                }
                else
                {
                    zid = _proc.AddEquip(tbCname.Text, tbAbbr.Text);

                    string logEvent = "Добавление оборудования в справочник";

                    Logging.StartFirstLevel(754);
                    Logging.Comment(logEvent);
                    Logging.Comment("id = " + zid.ToString());
                    Logging.Comment("Наименование оборудования: \"" + tbCname.Text + "\"");
                    Logging.Comment("Аббревиатура оборудования: \"" + tbAbbr.Text + "\"");
                    Logging.Comment("Завершение операции \"" + logEvent + "\"");
                    Logging.StopFirstLevel();

                    //MessageBox.Show("Запись добавлена");
                    MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbCname.Clear();
                    tbAbbr.Clear();
                    DialogResult = DialogResult.Cancel;
                }

            }
            else
            {
                if (cName == tbCname.Text)
                {

                    _proc.ChgEquip(zid, tbCname.Text, tbAbbr.Text, 1);

                    string logEvent = "Сохранение отредактированного оборудования в справочник";

                    Logging.StartFirstLevel(755);
                    Logging.Comment(logEvent);
                    Logging.Comment("id = " + zid.ToString());
                    Logging.VariableChange("Наименование оборудования: ", tbCname.Text, cName);
                    Logging.VariableChange("Аббревиатура оборудования: ", tbAbbr.Text, Abbr);
                    Logging.Comment("Завершение операции \"" + logEvent + "\"");
                    Logging.StopFirstLevel();

                    MessageBox.Show("Запись изменена");
                    tbCname.Clear();
                    tbAbbr.Clear();
                    DialogResult = DialogResult.Cancel;
                }
                else
                {
                    if (_proc.CheakAll(tbCname.Text, "eq").Rows.Count != 0) { MessageBox.Show("Запись с таким наименованием уже существует!", "Внимание"); }
                    else
                    {
                        _proc.ChgEquip(zid, tbCname.Text, tbAbbr.Text, 1);

                        string logEvent = "Сохранение отредактированного оборудования в справочник";

                        Logging.StartFirstLevel(755);
                        Logging.Comment(logEvent);
                        Logging.Comment("id = " + zid.ToString());
                        Logging.VariableChange("Наименование оборудования: ", tbCname.Text, cName);
                        Logging.VariableChange("Аббревиатура оборудования: ", tbAbbr.Text, Abbr);
                        Logging.Comment("Завершение операции \"" + logEvent + "\"");
                        Logging.StopFirstLevel();

                        MessageBox.Show("Запись изменена");
                        tbCname.Clear();
                        tbAbbr.Clear();
                        DialogResult = DialogResult.Cancel;
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ("" == tbCname.Text && "" == tbAbbr.Text)
            { DialogResult = DialogResult.Cancel; }
            else
                if (cName != tbCname.Text || Abbr != tbAbbr.Text)
                {
                    //if (MessageBox.Show("Были внесены изменения. Выйти без сохранения?", "Внимание", MessageBoxButtons.YesNo,
                      //                     MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                  if (MessageBox.Show(" На форме есть несохранённые данные.\nЗакрыть форму без сохранения данных?",
                    "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                  { DialogResult = DialogResult.Cancel; }
                }
                else { DialogResult = DialogResult.Cancel; }
        }

        private void AddEquipment_Load(object sender, EventArgs e)
        {
            check();
        }
    }
}

