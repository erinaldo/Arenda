using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using System.Text.RegularExpressions;
using Nwuram.Framework.Logging;

namespace Arenda
{
    public partial class AddEqVsSec : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        
        int _mode;
        int _id;
        DataTable dtGetEq_vs_Sec, dtEq;
        int _id_sec, _id_eq;
        string _name_Sections, _name_Equipment;
        string _count;

        public AddEqVsSec(string sec, string id)
        {
            InitializeComponent();
            inic();            
            _mode = 1; //добавление 
            _id = 0;

            _id_sec = int.Parse(id);
            tbSec.Text = _name_Sections = sec;

            cbEq.SelectedIndex = -1;            
            cbEq.Select();
            _name_Equipment = "";
            _id_eq = 0;
            _count = "";            
        }

        public AddEqVsSec(int id)
        {
            InitializeComponent();
            inic();
            _mode = 0; //редактирование
            _id = id;
            cbEq.Enabled = false;

            dtGetEq_vs_Sec = new DataTable();
            dtGetEq_vs_Sec = _proc.GetEquipment_vs_Sections(id);            

            if ((dtGetEq_vs_Sec != null) && (dtGetEq_vs_Sec.Rows.Count > 0))
            {
                _id_sec = int.Parse(dtGetEq_vs_Sec.Rows[0]["id_Sections"].ToString());
                tbSec.Text = _name_Sections = dtGetEq_vs_Sec.Rows[0]["name_Sections"].ToString();
                _name_Equipment = dtGetEq_vs_Sec.Rows[0]["name_Equipment"].ToString();
                _id_eq = int.Parse(dtGetEq_vs_Sec.Rows[0]["id_Equipment"].ToString());

                if ((dtEq != null)
                    && (dtEq.Rows.Count > 0)
                    && (dtEq.Select("id = " + _id_eq.ToString()).Count() > 0))
                {
                    cbEq.SelectedValue = _id_eq;
                }
                else
                {
                    MessageBox.Show("Не найдено оборудование");
                    this.Close();
                    return;
                }

                tbCount.Text = _count = dtGetEq_vs_Sec.Rows[0]["Quantity"].ToString();
                
            }
            else
            {
                this.Close();
            }
            
            tbCount.Select();
        }

        private void AddEqVsSec_Load(object sender, EventArgs e)
        {

        }

        private void btAddEq_Click(object sender, EventArgs e)
        {
            int idEqVsSec = 0;

            if (tbCount.Text != "" && tbCount.Text != "0")
            {
                if (_mode == 1)
                {
                    if (_proc.CheakEVS(_id_sec.ToString(), cbEq.Text, int.Parse(tbCount.Text)).Rows.Count != 0)
                    {
                        MessageBox.Show("Данное оборудование уже имеется в секции!", "Внимание"); 
                    }
                    else
                    {
                        idEqVsSec = _proc.AddEditEqVsSec(_id_sec, int.Parse(cbEq.SelectedValue.ToString()), Convert.ToInt32(tbCount.Text), _mode, 0);

                        string logEvent = "Добавить оборудование к секции";

                        Logging.StartFirstLevel(760);
                        Logging.Comment(logEvent);
                        Logging.Comment("id записи = " + idEqVsSec.ToString());
                        Logging.Comment("id секции = " + _id_sec.ToString()
                            + ", Наименование секции: \"" + tbSec.Text + "\"");
                        Logging.Comment("id оборудования = " + cbEq.SelectedValue.ToString()
                            + ", Наименование оборудования: \"" + cbEq.Text + "\"");
                        Logging.Comment("Количество добавляемого оборудования = " + tbCount.Text);
                        Logging.Comment("Завершение операции \"" + logEvent + "\"");
                        Logging.StopFirstLevel();

                        MessageBox.Show("Запись добавлена", "Внимание");
                        DialogResult = DialogResult.Cancel;
                    }
                }


                if (_mode == 0)
                {
                    if (tbSec.Text == _name_Sections)
                    {
                        idEqVsSec = _proc.AddEditEqVsSec(_id_sec, int.Parse(cbEq.SelectedValue.ToString()), Convert.ToInt32(tbCount.Text), _mode, _id);

                        string logEvent = "Редактировать оборудование у секции";

                        Logging.StartFirstLevel(761);
                        Logging.Comment(logEvent);
                        Logging.Comment("id записи = " + idEqVsSec.ToString());
                        Logging.Comment("id секции = " + _id_sec.ToString()
                            + ", Наименование секции: \"" + tbSec.Text + "\"");
                        Logging.Comment("id оборудования = " + cbEq.SelectedValue.ToString()
                            + ", Наименование оборудования: \"" + cbEq.Text + "\"");
                        Logging.VariableChange("Количество редактируемого оборудования", tbCount.Text, _count);
                        Logging.Comment("Завершение операции \"" + logEvent + "\"");
                        Logging.StopFirstLevel();

                        MessageBox.Show("Запись изменена", "Внимание");
                        DialogResult = DialogResult.Cancel;
                    }
                }

            }
            else { MessageBox.Show("Введите число", "Ошибка"); }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            if (_count.ToLower() != tbCount.Text.ToLower()
                || _name_Equipment.ToLower() != cbEq.Text.ToLower())
            {
                if (MessageBox.Show("Были внесены изменения. Выйти без сохранения?", "Внимание", MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                { DialogResult = DialogResult.Cancel; }
            }
            else { DialogResult = DialogResult.Cancel; }
        }


        private void inic()
        {
            dtEq = new DataTable();

            dtEq = _proc.FillCbEq();

            if ((dtEq != null) && (dtEq.Rows.Count > 0))
            {
                cbEq.DataSource = dtEq;
                cbEq.DisplayMember = "cName";
                cbEq.ValueMember = "id";
            }
        }

        private void tbCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex pat = new Regex(@"[\b]|[0-9]");
            bool b = pat.IsMatch(e.KeyChar.ToString());
            if (b == false)
            {
                e.Handled = true;
            }
        }

        private void check()
        {
            if (tbCount.Text.Trim().Length != 0 && cbEq.Text.Length != 0)
            {
                btAddEq.Enabled = true;
            }
            else
                btAddEq.Enabled = false;
        }

        private void tbCount_TextChanged(object sender, EventArgs e)
        {
            check();
        }

        private void cbEq_SelectedValueChanged(object sender, EventArgs e)
        {
            check();
        }
    }
}
