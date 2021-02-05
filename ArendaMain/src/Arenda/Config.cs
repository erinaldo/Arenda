using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using System.IO;
using System.Security.AccessControl;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Nwuram.Framework.Logging;

namespace Arenda
{
    public partial class Config : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        DataTable conf;
        int chislo;
        string SRKA_old = "";
        string SRZA_old = "";
        string PotD_old = "";
        string ScanD_old = "";
        string Man_old = "";
        string Woman_old = "";
        string Peni_old = "";
        string Nds_old = "";
        string Month_old = "";
        bool view = false;
        
        public Config()
        {
            InitializeComponent();

            if (TempData.Rezhim.ToLower().Equals("пр"))
            {
                Logging.StartFirstLevel(1403);
                Logging.Comment("Открыта форма «Настройки» для просмотра");

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            if (view)
            {
                this.Close();
            }
            else
            {
                if (ParamsAreSaved())
                {
                    this.Close();
                }
                else
                {
                    DialogResult d = MessageBox.Show("На форме есть несохраненные данные. \nЗакрыть форму без сохранения?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (d == DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
            }
        }

        private bool ParamsAreSaved()
        {
            if ((SRKA_old == tbSRKA.Text)
                && (SRZA_old == tbSRZA.Text)
                && (PotD_old == tbPotD.Text)
                && (ScanD_old == tbScanD.Text)
                && (Man_old == txtMan.Text)
                && (Woman_old == txtWoman.Text)
                && (Peni_old == txtPeni.Text)
                && (Nds_old == txtNds.Text)
                && (Month_old == txtMonth.Text)
                )
                return true;
            else
                return false;
        }

        private void Config_Load(object sender, EventArgs e)
        {
            view = !new List<string> { "РКВ" }.Contains(TempData.Rezhim);

            conf =_proc.EditGetConf(ConnectionSettings.GetIdProgram(),"","");

            if (conf != null)
            {
                for (int i = 0; conf.Rows.Count > i; i++)
                {
                    if (conf.DefaultView[i]["id_value"].ToString() == "SRKA")
                        tbSRKA.Text = conf.DefaultView[i]["value"].ToString();
                    if (conf.DefaultView[i]["id_value"].ToString() == "SRZA")
                        tbSRZA.Text = conf.DefaultView[i]["value"].ToString();
                    if (conf.DefaultView[i]["id_value"].ToString() == "PotD")
                        tbPotD.Text = conf.DefaultView[i]["value"].ToString();
                    if (conf.DefaultView[i]["id_value"].ToString() == "psss")
                        tbScanD.Text = conf.DefaultView[i]["value"].ToString();
                    if (conf.DefaultView[i]["id_value"].ToString() == "pman")
                        txtMan.Text = conf.DefaultView[i]["value"].ToString();
                    if (conf.DefaultView[i]["id_value"].ToString() == "pwmn")
                        txtWoman.Text = conf.DefaultView[i]["value"].ToString();
                    if (conf.DefaultView[i]["id_value"].ToString() == "pnar")
                    {
                        txtPeni.Text = numTextBox.CheckAndChange(conf.DefaultView[i]["value"].ToString(), 2, 0, 100, false, "", "");
                    }
                    if (conf.DefaultView[i]["id_value"].ToString() == "ndsa")
                    {
                        txtNds.Text = numTextBox.CheckAndChange(conf.DefaultView[i]["value"].ToString(), 0, 0, 100, false, "", "");
                    }
                    if (conf.DefaultView[i]["id_value"].ToString() == "chsl")
                    {
                        txtMonth.Text = numTextBox.CheckAndChange(conf.DefaultView[i]["value"].ToString(), 0, 1, 31, false, "", "");
                    }
                        
                }
            }

          if(tbScanD.Text.Length == 0)
          {
            tbScanD.Text = "\\\\192.168.5.31\\Scans";
            _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "psss", tbScanD.Text);
          }

            ButtonSaveAvailability();
            Params();

            if (view)
            {
                foreach (Control con in this.Controls)
                {
                    con.Enabled = false;
                }
                
                btExit.Enabled = true;
                btAddEq.Visible = false;
            }
        }

        private void Params()
        {
            SRKA_old = tbSRKA.Text;
            SRZA_old = tbSRZA.Text;
            PotD_old = tbPotD.Text;
            ScanD_old = tbScanD.Text;
            Man_old = txtMan.Text;
            Woman_old = txtWoman.Text; 
            Peni_old = txtPeni.Text;
            Nds_old = txtNds.Text;
            Month_old = txtMonth.Text;
        }

        private void btAddEq_Click(object sender, EventArgs e)
        {
            decimal decimal_chislo = 0;

          if(!Directory.Exists(tbScanD.Text))
          {
            MessageBox.Show("   Введенный путь хранения\nотсканированных документов\n      недоступен для чтения.\n    Сохранение невозможно.",
              "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }
          else
          {
            DirectoryInfo di = new DirectoryInfo(tbScanD.Text);
            DirectorySecurity ds = di.GetAccessControl();
            var rules = ds.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
            foreach (FileSystemAccessRule rule in rules)
            {
              if (rule.FileSystemRights == FileSystemRights.Read && rule.AccessControlType == AccessControlType.Deny)
              {
                MessageBox.Show("   Введенный путь хранения\nотсканированных документов\n      недоступен для чтения.\n    Сохранение невозможно.",
                  "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
              }
            }
          }

            string error="";
            if (!int.TryParse(tbSRKA.Text.Trim(), out chislo))
                error += "\n \"" + label1.Text + "\"";

            if (!int.TryParse(tbSRZA.Text.Trim(), out chislo))
                error += "\n \"" + label2.Text + "\"";

            if (!decimal.TryParse(numTextBox.ConvertToCompPunctuation(txtPeni.Text.Trim()), out decimal_chislo))
                error += "\n \"" + lblPeni.Text + "\"";

            if (!int.TryParse(txtNds.Text.Trim(), out chislo))
                error += "\n \"" + lblNds.Text + "\"";

            if (!int.TryParse(txtMonth.Text.Trim(), out chislo))
                error += "\n \"" + txtMonth1.Text + "\"";

            if (error =="")
            {
                _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "SRKA", tbSRKA.Text.Trim());
                _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "SRZA", tbSRZA.Text.Trim());
                _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "PotD", tbPotD.Text.Trim());
                _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "psss", tbScanD.Text.Trim());
                _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "pman", txtMan.Text.Trim());
                _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "pwmn", txtWoman.Text.Trim());
                _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "pnar", txtPeni.Text.Trim());
                _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "ndsa", txtNds.Text.Trim());
                _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "chsl", txtMonth.Text.Trim());



                Logging.StartFirstLevel(353);

                Logging.VariableChange("Срок аренды: ", tbSRKA.Text.Trim(), SRKA_old);
                Logging.VariableChange("Срок предупреждения о завершении договора: ", tbSRZA.Text.Trim(), SRZA_old);
                Logging.VariableChange("Путь хранения документов по арендаторам: ", tbPotD.Text.Trim(), PotD_old);
                Logging.VariableChange("Путь хранения отсканированных документов: ", tbScanD.Text.Trim(), ScanD_old);
                Logging.VariableChange("Ображение к представителю - мужчине: ", txtMan.Text.Trim(), Man_old);
                Logging.VariableChange("Ображение к представителю - женщине: ", txtWoman.Text.Trim(), Woman_old);
                Logging.VariableChange("Размер пени: ", txtPeni.Text.Trim(), Peni_old);
                Logging.VariableChange("НДС: ", txtNds.Text.Trim(), Nds_old);
                Logging.VariableChange("Число месяца, после которого начисляются пени: ", txtMonth.Text.Trim(), Month_old);

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();



                MessageBox.Show("Внесены изменения", "Внимание");
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                MessageBox.Show("Введено некорректное значение в поле " + error, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*var fd = new FolderBrowserDialog { };
            fd.ShowDialog();
            if (fd.SelectedPath.Trim().Length == 0)
            {
                return;
            }
            tbPotD.Text = fd.SelectedPath.Trim();*/

          var fd = new FolderBrowser2 { };
          if (fd.ShowDialog(this) == DialogResult.OK)
          {
            if (fd.DirectoryPath.Trim().Length == 0)
            {
              return;
            }
            tbPotD.Text = fd.DirectoryPath.Trim();
          }
        }

        private void ButtonSaveAvailability()
        {
            if ((tbSRKA.Text.Trim() == "")
                || (tbSRZA.Text.Trim() == "")
                || (tbPotD.Text.Trim() == "")
                || (tbScanD.Text.Trim() == "")
                || (txtPeni.Text.Trim() == "")
                || (txtNds.Text.Trim() == "")
                || (txtMonth.Text.Trim() == ""))
            {
                btAddEq.Enabled = false;
            }
            else
            {
                btAddEq.Enabled = true;
            }
        }

        private void tbSRKA_TextChanged(object sender, EventArgs e)
        {
            ButtonSaveAvailability();
        }

        private void tbSRZA_TextChanged(object sender, EventArgs e)
        {
            ButtonSaveAvailability();
        }

        private void tbPotD_TextChanged(object sender, EventArgs e)
        {
            if (tbPotD.Text.Trim().Length > 64)
            {
                tbPotD.Text = "";
                MessageBox.Show("Поле \"Путь хранения документов по арендаторам\" заполнено некорректно. \nДлина поля не может превышать 64 символа.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ButtonSaveAvailability();
        }

        private void txtPeni_TextChanged(object sender, EventArgs e)
        {
            ButtonSaveAvailability();
        }

        private void txtNds_TextChanged(object sender, EventArgs e)
        {
            ButtonSaveAvailability();
        }

        private void txtMonth_TextChanged(object sender, EventArgs e)
        {
            ButtonSaveAvailability();
        }

        private void txtMan_KeyPress(object sender, KeyPressEventArgs e)
        {
            lockNumb(e);
        }

        private void txtWoman_KeyPress(object sender, KeyPressEventArgs e)
        {
            lockNumb(e);
        }

        private static void lockNumb(KeyPressEventArgs e)
        {
            Regex pat = new Regex(@"[\b]|[а-яА-Я]|[-]|[/]|[\]|[.]|[,]|[*]|[+]|[:]|[(]|[)]|[\s]|[a-zA-Z]");
            bool b = pat.IsMatch(e.KeyChar.ToString());
            if (b == false)
            {
                e.Handled = true;
            }
        }

        private void tbSRKA_KeyPress(object sender, KeyPressEventArgs e)
        {
            numTextBox.KeyPress(tbSRKA, e, true, false);            
        }

        private void tbSRZA_KeyPress(object sender, KeyPressEventArgs e)
        {
            numTextBox.KeyPress(tbSRZA, e, true, false);
        }

        private void txtPeni_KeyPress(object sender, KeyPressEventArgs e)
        {
            numTextBox.KeyPress(txtPeni, e, false, false);
        }

        private void txtNds_KeyPress(object sender, KeyPressEventArgs e)
        {
            numTextBox.KeyPress(txtNds, e, true, false);
        }

        private void txtMonth_KeyPress(object sender, KeyPressEventArgs e)
        {
            numTextBox.KeyPress(txtMonth, e, true, false);            
        }


        private void tbSRKA_Leave(object sender, EventArgs e)
        {
            //tbSRKA.Text = numTextBox.CheckAndChange(tbSRKA.Text, 0, 0, 100, false,"", "");
        }

        private void tbSRZA_Leave(object sender, EventArgs e)
        {
            //tbSRZA.Text = numTextBox.CheckAndChange(tbSRZA.Text, 0, 0, 100, false,"", "");
        }

        private void txtPeni_Leave(object sender, EventArgs e)
        {
            txtPeni.Text = numTextBox.CheckAndChange(txtPeni.Text, 2, 0, 100, false, "", "");
        }

        private void txtNds_Leave(object sender, EventArgs e)
        {
            txtNds.Text = numTextBox.CheckAndChange(txtNds.Text, 0, 0, 100, false, "", "");
        }

        private void txtMonth_Leave(object sender, EventArgs e)
        {
            txtMonth.Text = numTextBox.CheckAndChange(txtMonth.Text, 0, 1, 31, false, "", "");            
        }

        private void button2_Click(object sender, EventArgs e)
        {
          var fd = new FolderBrowser2 { };//new FolderBrowserDialog { };
          if (fd.ShowDialog(this) == DialogResult.OK)
          {
            if (fd.DirectoryPath.Trim().Length == 0)
            {
              return;
            }
            tbScanD.Text = fd.DirectoryPath.Trim();
          }
          ButtonSaveAvailability();
        }

        string tmptxt;

        private void tbScanD_KeyPress(object sender, KeyPressEventArgs e)
        {
          tmptxt = tbScanD.Text;
          if (e.KeyChar == '\b' || (!char.IsControl(e.KeyChar) && e.KeyChar != 'v'))
            e.Handled = true;
        }

        private void tbScanD_TextChanged(object sender, EventArgs e)
        {
          if (tbScanD.Text == tmptxt + "v")
            tbScanD.Text = tmptxt;
        }

        private void tbScanD_KeyDown(object sender, KeyEventArgs e)
        {
          if (e.KeyData == Keys.Delete)
            e.Handled = true;
        }
    }
}
