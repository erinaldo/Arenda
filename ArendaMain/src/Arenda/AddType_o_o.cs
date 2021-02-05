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
    public partial class AddType_o_o : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        string _cname,_abbr;
        int zid;
        int _mode; 
        public AddType_o_o()
        {
            InitializeComponent();
            _mode = 1;
            check();
            _cname = "";
            _abbr = "";
        }

        public AddType_o_o(string cName,string abbr, string id, int mode)
        {
            InitializeComponent();
            tbCname.Text = cName;
            tbAbbr.Text = abbr;
            _abbr = abbr;
            _cname = cName;
            _mode = mode;
            zid = Convert.ToInt32(id);

        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (_mode == 1)
            {
                if (_proc.CheakAll(tbCname.Text, "too").Rows.Count != 0)
                { MessageBox.Show("Запись с такими параметрами уже существует", "Внимание" ); }
                else
                {
                    DataTable dtResult = _proc.AddEditType_o_o(tbCname.Text, tbAbbr.Text, 0, 1, 1);

                    Logging.StartFirstLevel(1376);
                    if (dtResult != null && dtResult.Rows.Count > 0 && dtResult.Columns.Contains("id"))
                        Logging.Comment($"ID:{dtResult.Rows[0]["id"]}");
                    Logging.Comment("Наименование типа организации: " + tbCname.Text.Trim());
                    Logging.Comment("Аббревиатура этажа: " + tbAbbr.Text.Trim());

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();

                    //MessageBox.Show("Запись добавлена","Сообщение");
                    MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.Cancel;
                }
            }

            if (_mode == 0)
            {   if (_cname == tbCname.Text)
                    {   
                        _proc.AddEditType_o_o(tbCname.Text, tbAbbr.Text, zid, 0,1);

                    Logging.StartFirstLevel(1377);
                    Logging.Comment("ID: " + zid);
                    Logging.VariableChange("Наименование типа организации: ", tbCname.Text.Trim(), _cname);
                    Logging.VariableChange("Аббревиатура этажа: ", tbAbbr.Text.Trim(), _abbr);

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();

                    //MessageBox.Show("Запись изменена", "Сообщение");
                    MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.Cancel;
                    }
            
            else {
                if (_proc.CheakAll(tbCname.Text, "too").Rows.Count != 0)
                    { MessageBox.Show("Запись с такими параметрами уже существует", "Внимание"); }
                    else
                     {
                    _proc.AddEditType_o_o(tbCname.Text, tbAbbr.Text, zid, 0, 1);

                        Logging.StartFirstLevel(1377);
                        Logging.Comment("ID: " + zid);
                        Logging.VariableChange("Наименование типа организации: ", tbCname.Text.Trim(), _cname);
                        Logging.VariableChange("Аббревиатура этажа: ", tbAbbr.Text.Trim(), _abbr);

                        Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                        + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                        Logging.StopFirstLevel();

                        //MessageBox.Show("Запись изменена", "Сообщение");
                        MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.Cancel;
                     }
            
                }
            
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_cname.ToLower() != tbCname.Text.ToLower() || _abbr.ToLower() != tbAbbr.Text.ToLower())
            {
              //if (MessageBox.Show("Были внесены изменения. Выйти без сохранения?", "Внимание", MessageBoxButtons.YesNo,
                //                       MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              if (MessageBox.Show(" На форме есть несохранённые данные.\nЗакрыть форму без сохранения данных?",
                "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              { DialogResult = DialogResult.Cancel; }
            }
            else { DialogResult = DialogResult.Cancel; } 
        }
        private void check()
        {
            if (tbCname.Text.Trim().Length != 0 && tbAbbr.Text.Length != 0)
            { btAdd.Enabled = true; }
            else btAdd.Enabled = false;
        }
        private void tbCname_TextChanged(object sender, EventArgs e)
        {
            check();
        }

        private void tbAbbr_TextChanged(object sender, EventArgs e)
        {
            check();
        }

        private void AddType_o_o_Load(object sender, EventArgs e)
        {   
            
            
            
            }
        }
    }

