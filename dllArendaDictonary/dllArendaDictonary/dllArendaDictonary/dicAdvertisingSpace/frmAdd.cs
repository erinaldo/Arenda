using Nwuram.Framework.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dllArendaDictonary.dicAdvertisingSpace
{
    public partial class frmAdd : Form
    {
        public DataRowView row { set; private get; }

        private bool isEditData = false;
        private string oldName,oldObjectName,oldBuildName,oldLenght,oldWidth;
        private int id = 0,oldIdObject,oldIdBuild;    
        

        public frmAdd()
        {
            InitializeComponent();
            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose, "Выход");
            tp.SetToolTip(btSave, "Сохранить");
        }

        private void frmAdd_Load(object sender, EventArgs e)
        {
            init_combobox();
            if (row != null)
            {
                id = (int)row["id"];
                tbNumber.Text = (string)row["NumberPlace"];
                oldName = tbNumber.Text.Trim();

                cmbObject.SelectedValue = row["id_ObjectLease"];
                oldObjectName = cmbObject.Text.Trim();
                oldIdObject = (int)cmbObject.SelectedValue;

                cmbBuilding.SelectedValue = row["id_Building"];
                oldBuildName = cmbBuilding.Text.Trim();
                oldIdBuild = (int)cmbBuilding.SelectedValue;

                tbLength.Text = row["Length"].ToString();
                oldLenght = tbLength.Text.Trim();

                tbWidth.Text = row["Width"].ToString();
                oldWidth = tbWidth.Text.Trim();

            }

            isEditData = false;
        }

        private void frmAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = isEditData && DialogResult.No == MessageBox.Show("На форме есть не сохранённые данные.\nЗакрыть форму без сохранения данных?\n", "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        private void init_combobox()
        {
            Task<DataTable> task = Config.hCntMain.getBuilding(false);
            task.Wait();
            DataTable dtBuilding = task.Result;

            cmbBuilding.DisplayMember = "cName";
            cmbBuilding.ValueMember = "id";
            cmbBuilding.DataSource = dtBuilding;
            cmbBuilding.SelectedIndex = -1;

            task = Config.hCntMain.getObjectLease(false);
            task.Wait();
            DataTable dtObjectLease = task.Result;

            cmbObject.DisplayMember = "cName";
            cmbObject.ValueMember = "id";
            cmbObject.DataSource = dtObjectLease;
            cmbObject.SelectedIndex = -1;

        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (cmbObject.SelectedIndex == -1)
            {
                MessageBox.Show(Config.centralText($"Необходимо выбрать\n \"{lObject.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbObject.Focus();
                return;
            }

            if (cmbBuilding.SelectedIndex==-1)
            {
                MessageBox.Show(Config.centralText($"Необходимо выбрать\n \"{lBuilding.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbBuilding.Focus();
                return;
            }
                                  
            if (tbNumber.Text.Trim().Length == 0)
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{lNumber.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbNumber.Focus();
                return;
            }
            
            if (tbWidth.Text.Trim().Length == 0)
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{gbSizePlace.Text}: {lLength.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbWidth.Focus();
                return;
            }

            if (tbLength.Text.Trim().Length == 0)
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{gbSizePlace.Text}: {lWidth.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbLength.Focus();
                return;
            }


            Task<DataTable> task = Config.hCntMain.setReclamaPlace(id, tbNumber.Text, 
                (int)cmbObject.SelectedValue, (int)cmbBuilding.SelectedValue, 
                Int64.Parse(tbLength.Text), Int64.Parse(tbWidth.Text), true, false, 0);
            task.Wait();

            DataTable dtResult = task.Result;

            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                MessageBox.Show("Не удалось сохранить данные", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if ((int)dtResult.Rows[0]["id"] == -1)
            {
                MessageBox.Show("В справочнике уже присутствует должность с таким наименованием.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if ((int)dtResult.Rows[0]["id"] == -9999)
            {
                MessageBox.Show("Произошла неведомая хрень.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (id == 0)
            {
                id = (int)dtResult.Rows[0]["id"];
                Logging.StartFirstLevel(1558);
                //Logging.Comment("Добавить Тип документа");
                Logging.Comment($"ID: {id}");
                Logging.Comment($"Объект расположения рекламного места ID:{cmbObject.SelectedValue}; Наименование: {cmbObject.Text}");
                Logging.Comment($"Здание, на котором располагается рекламное место ID:{cmbBuilding.SelectedValue}; Наименование: {cmbBuilding.Text}");
                Logging.Comment($"Номер рекламного места : {tbNumber.Text.Trim()}");
                Logging.Comment($"Размер места Длина: {tbLength.Text.Trim()} мм");
                Logging.Comment($"Размер места Ширина: {tbWidth.Text.Trim()} мм");
                Logging.StopFirstLevel();

            }
            else
            {
                Logging.StartFirstLevel(1559);
                //Logging.Comment("Редактировать Тип документа");
                Logging.Comment($"ID: {id}");
                //Logging.VariableChange("Наименование", tbNumber.Text.Trim(), oldName);

                Logging.VariableChange($"Объект расположения рекламного места ID", cmbObject.SelectedValue, oldIdObject);
                Logging.VariableChange($"Объект расположения рекламного места Наименование", cmbObject.Text, oldObjectName);

                Logging.VariableChange($"Здание, на котором располагается рекламное место ID", cmbBuilding.SelectedValue, oldIdBuild);
                Logging.VariableChange($"Здание, на котором располагается рекламное место Наименование", cmbBuilding.Text, oldBuildName);

                Logging.VariableChange($"Номер рекламного места", tbNumber.Text.Trim(), oldName);

                Logging.VariableChange($"Размер места Длина",tbLength.Text.Trim(),oldLenght);
                Logging.VariableChange($"Размер места Ширина", tbWidth.Text.Trim(), oldWidth);

                Logging.StopFirstLevel();
            }

            isEditData = false;
            MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            isEditData = true;
        }

        private void tbLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != '\b';
        }
    }
}
