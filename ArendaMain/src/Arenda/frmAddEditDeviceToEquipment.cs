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
    public partial class frmAddEditDeviceToEquipment : Form
    {
        private Section section;
        private Device device;
        private Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        private bool load = true;

        public frmAddEditDeviceToEquipment(Section section, Device device)
        {
            this.section = section;
            this.device = device;
            InitializeComponent();
        }

        private void frmAddEditDeviceToEquipment_Load(object sender, EventArgs e)
        {
            this.Text = (device.Id == 0 ? "Добавить" : "Редактировать") + " приборы";
            txtSectionName.Text = section.Name;
            cmbDevices_Load();
            if (device.Quantity != 0)
            {
                txtQuantity.Text = device.Quantity.ToString();
            }
            SetButtonsEnabled();
            load = false;
        }

        private void cmbDevices_Load()
        {
            DataTable devices = proc.GetDevices();
            if (devices != null)
            {
                devices.DefaultView.RowFilter = "is_active = true";
                cmbDevices.DataSource = devices;
            }
            cmbDevices.ValueMember = "id";
            cmbDevices.DisplayMember = "cname";
            if (device.Id != 0)
            {
                cmbDevices.SelectedValue = device.Id;
            }
        }

        private bool SomethingChanged()
        {
            return (cmbDevices.SelectedValue != null && device.Id != Convert.ToInt32(cmbDevices.SelectedValue)) || (txtQuantity.Text.Length > 0 && device.Quantity.ToString() != txtQuantity.Text);
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }

        private void SetButtonsEnabled()
        {
            btnSave.Enabled = SomethingChanged() && txtQuantity.Text.Length != 0;
        }

        private void TextChanged(object sender, EventArgs e)
        {
            if (!load)
            {
                SetButtonsEnabled();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (!SomethingChanged() || MessageBox.Show("На форме остались несохранённые изменения. Выйти без сохранения?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (!int.TryParse(txtQuantity.Text, out count))
            {
                MessageBox.Show("Количество имеет неверный формат!");
                return;
            }
            
            if (device.Id != Convert.ToInt32(cmbDevices.SelectedValue))
            {
                proc.RemoveDeviceFromSection(section.Id, device.Id);
            }

            DataTable dtGetSecInfo = new DataTable();
            dtGetSecInfo = proc.GetSecInfo(section.Id);

            DataTable dtResult =  proc.AddDeviceToSection(section.Id, Convert.ToInt32(cmbDevices.SelectedValue), Convert.ToInt32(txtQuantity.Text));


            if (device.Id == 0)
            {             
                Logging.StartFirstLevel(1395);
                Logging.Comment("Информация о секции:");

                Logging.Comment("id = " + section.Id.ToString()
                     + ", Наименование секции: \"" + dtGetSecInfo.Rows[0]["Sec"].ToString() + "\"");

                Logging.Comment("id объекта  = " + dtGetSecInfo.Rows[0]["id_ObjectLease"].ToString()
                 + ", Наименование объекта : \"" + dtGetSecInfo.Rows[0]["Obj"].ToString() + "\"");

                Logging.Comment("id здания = " + dtGetSecInfo.Rows[0]["id_Build"].ToString()
                   + ", Наименование здания: \"" + dtGetSecInfo.Rows[0]["Build"].ToString() + "\"");

                Logging.Comment("id этажа = " + dtGetSecInfo.Rows[0]["id_Floo"].ToString()
                + ", Наименование этажа: \"" + dtGetSecInfo.Rows[0]["Floo"].ToString() + "\"");
                Logging.Comment("Количество телефонных линий: " + dtGetSecInfo.Rows[0]["Telephone_lines"].ToString());
                Logging.Comment("Количество светильников: " + dtGetSecInfo.Rows[0]["Lamps"].ToString());
                Logging.Comment("Номер телефона: " + dtGetSecInfo.Rows[0]["Phone_number"].ToString());
                Logging.Comment("Общая площадь: " + dtGetSecInfo.Rows[0]["Total_Area"].ToString());
                Logging.Comment("Площадь торгового зала: " + dtGetSecInfo.Rows[0]["Area_of_Trading_Hall"].ToString());
                Logging.Comment("Признак АППЗ: " + ((bool.Parse(dtGetSecInfo.Rows[0]["isAPPZ"].ToString())) ? "ДА" : "НЕТ"));

                Logging.Comment("Информация о добавленном приборе:");
                
                if (dtResult != null && dtResult.Rows.Count > 0 && dtResult.Columns.Contains("id"))
                    Logging.Comment($"ID:{dtResult.Rows[0]["id"]}");

                Logging.Comment("Тип прибора ID: " + cmbDevices.SelectedValue + " ;Наименование: " + cmbDevices.Text);
                Logging.Comment("Количество: " + txtQuantity.Text.Trim());

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }
            else
            {
                Logging.StartFirstLevel(1396);
                Logging.Comment("Информация о секции:");

                Logging.Comment("id = " + section.Id.ToString()
                     + ", Наименование секции: \"" + dtGetSecInfo.Rows[0]["Sec"].ToString() + "\"");

                Logging.Comment("id объекта  = " + dtGetSecInfo.Rows[0]["id_ObjectLease"].ToString()
                 + ", Наименование объекта : \"" + dtGetSecInfo.Rows[0]["Obj"].ToString() + "\"");

                Logging.Comment("id здания = " + dtGetSecInfo.Rows[0]["id_Build"].ToString()
                   + ", Наименование здания: \"" + dtGetSecInfo.Rows[0]["Build"].ToString() + "\"");

                Logging.Comment("id этажа = " + dtGetSecInfo.Rows[0]["id_Floo"].ToString()
                + ", Наименование этажа: \"" + dtGetSecInfo.Rows[0]["Floo"].ToString() + "\"");
                Logging.Comment("Количество телефонных линий: " + dtGetSecInfo.Rows[0]["Telephone_lines"].ToString());
                Logging.Comment("Количество светильников: " + dtGetSecInfo.Rows[0]["Lamps"].ToString());
                Logging.Comment("Номер телефона: " + dtGetSecInfo.Rows[0]["Phone_number"].ToString());
                Logging.Comment("Общая площадь: " + dtGetSecInfo.Rows[0]["Total_Area"].ToString());
                Logging.Comment("Площадь торгового зала: " + dtGetSecInfo.Rows[0]["Area_of_Trading_Hall"].ToString());
                Logging.Comment("Признак АППЗ: " + ((bool.Parse(dtGetSecInfo.Rows[0]["isAPPZ"].ToString())) ? "ДА" : "НЕТ"));

                Logging.Comment("Информация о редактируемом приборе:");

                if (dtResult != null && dtResult.Rows.Count > 0 && dtResult.Columns.Contains("id"))
                    Logging.Comment($"ID:{dtResult.Rows[0]["id"]}");

                //Logging.VariableChange("Тип прибора ID: " , cmbDevices.SelectedValue ,device.Id);
                //Logging.VariableChange("Тип прибора Наименование: ", cmbDevices.Text,device.Name);
                Logging.Comment("Тип прибора ID: " + cmbDevices.SelectedValue + " ;Наименование: " + cmbDevices.Text);
                Logging.VariableChange("Количество: ", txtQuantity.Text.Trim(), device.Quantity);

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}
