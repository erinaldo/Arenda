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
    public partial class AddEditBasement : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        int zid;
        int _mode, _needDate;
        string _cName, _abbr;
        public AddEditBasement()
        {
            InitializeComponent();
            _mode = 1;
            check();
            _cName = "";
            _abbr = "";
        }

        public AddEditBasement(int id, string cname, string abbr, int mode,int needDate)
        {
            InitializeComponent();
            zid = id;
            _cName = cname;
            _mode = mode;
            tbCname.Text = _cName;
            tbAbbr.Text = abbr;
            _abbr = abbr;
            _needDate = needDate;
            if (needDate == 1)
                checkBox1.Checked = true;
            else checkBox1.Checked = false;
        }
      
      private void btAdd_Click(object sender, EventArgs e)
      {
        int boom;
        if (checkBox1.Checked == true)
          boom = 1;
        else boom = 0;
        DataTable dtb = _proc.CheakAll(tbCname.Text, "bs");
        if(dtb == null || dtb.Rows.Count == 0)
        {
          if (_mode == 1)
          {
            _proc.AddEditBas(0, tbCname.Text, tbAbbr.Text, 1, 1, boom);

            Logging.StartFirstLevel(1379);
            Logging.Comment("Наименование основания заключения договоров: " + tbCname.Text.Trim());
            Logging.Comment("Аббревиатура основания заключения договоров: " + tbAbbr.Text.Trim());
            Logging.Comment("Наличие номера и даты у договора: " + (checkBox1.Checked ? "Да" : "Нет"));

            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
              + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();

            //MessageBox.Show("Запись добавлена", "Сообщение");
            MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.Cancel;
          }
          
          if (_mode == 0)
          {
            _proc.AddEditBas(zid, tbCname.Text, tbAbbr.Text, 0, 1, boom);
              
            Logging.StartFirstLevel(1380);
            Logging.Comment("ID: " + zid);
            Logging.VariableChange("Наименование этажа: ", tbCname.Text.Trim(), _cName);
            Logging.VariableChange("Аббревиатура этажа: ", tbAbbr.Text.Trim(), _abbr);
            Logging.VariableChange("Наличие номера и даты у договора: ", (checkBox1.Checked ? "Да" : "Нет"), (_needDate == 1 ? "Да" : "Нет"));

            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
              + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();

            //MessageBox.Show("Запись изменена", "Сообщение");
            MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.Cancel;
          }
        }
        else
        {
          if (bool.Parse(dtb.Rows[0]["isActive"].ToString()))
          {
            //MessageBox.Show("Введённое наименование присутствует в БД и имеет статус \"действующая\". Сохранить введённое наименование нельзя.", "Сохранение записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            MessageBox.Show("                       В справочнике уже\nприсутствует основание заключения договоров\n                  с таким наименованием.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }
          else if (zid == 0)
          {
            if (MessageBox.Show("Введённое наименование присутствует в БД и имеет статус \"недействующее\". Вы хотите изменить статус на \"действующее\"?", "Сохранение записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
              _proc.ChangeBasementActiveStatus(int.Parse(dtb.Rows[0]["id"].ToString()), true, true);
              MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
              this.DialogResult = DialogResult.OK;
            }
          }
          else
          {
            MessageBox.Show("Введённое наименование присутствует в БД и имеет статус \"недействующее\". Сохранить введённое наименование нельзя.",
              "Сохранение записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
          }
        }
        /* 
         * if (_proc.CheakAll(tbCname.Text, "bs").Rows.Count != 0)
         * {
         * MessageBox.Show("Запись с такими параметрами уже существует", "Внимание");
         * }
         * else
         * {
         * 
         * _proc.AddEditBas(0, tbCname.Text, tbAbbr.Text, 1, 1, boom);
         * 
         * Logging.StartFirstLevel(1379);
         * Logging.Comment("Наименование основания заключения договоров: " + tbCname.Text.Trim());
         * Logging.Comment("Аббревиатура основания заключения договоров: " + tbAbbr.Text.Trim());
         * Logging.Comment("Наличие номера и даты у договора: " + (checkBox1.Checked?"Да":"Нет"));
         * 
         * Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
         * + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
         * Logging.StopFirstLevel();
         * 
         * //MessageBox.Show("Запись добавлена", "Сообщение");
         * MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
         * DialogResult = DialogResult.Cancel;
         * }
         */
      }

        private void button1_Click(object sender, EventArgs e)
        {
            if ("" == tbCname.Text && "" == tbAbbr.Text)
            {
                 DialogResult = DialogResult.Cancel; 
            }

            if (_cName.ToLower() != tbCname.Text.ToLower() || _abbr.ToLower() != tbAbbr.Text.ToLower())
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
    }
}
