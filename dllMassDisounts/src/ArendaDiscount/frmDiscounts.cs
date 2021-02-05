using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArendaDiscount
{
    public partial class frmDiscounts : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        DataTable dtData;
        public frmDiscounts()
        {
            InitializeComponent();
            dgvSections.AutoGenerateColumns = false;
        }

        private void frmDiscounts_Load(object sender, EventArgs e)
        {
            InitCB();
            GetData();
        }

        #region Combobox
        private void InitCB()
        {
            InitDiscounts();
            InitTypeDog();
            InitObjects();
            InitBuildings();
            InitFloors();
        }

        private void InitDiscounts()
        {
            //тип скидона
            Task<DataTable> task = _proc.getTypeDiscount();
            task.Wait();
            cmbTypeDiscount.DataSource = task.Result;
            cmbTypeDiscount.DisplayMember = "cName";
            cmbTypeDiscount.ValueMember = "id";
        }

        private void InitTypeDog()
        {
            Task<DataTable> task = _proc.GetContractTypes();
            task.Wait();
            cmbTypeContract.DataSource = task.Result;
            cmbTypeContract.DisplayMember = "TypeContract";
            cmbTypeContract.ValueMember = "id";
        }

        private void InitObjects()
        {
            Task<DataTable> task = _proc.getObjectsLease();
            task.Wait();
            cmbObject.DataSource = task.Result;
            cmbObject.DisplayMember = "cName";
            cmbObject.ValueMember = "id";
        }
        private void InitBuildings()
        {
            Task<DataTable> task = _proc.getBuildings();
            task.Wait();
            cmbBuilding.DataSource = task.Result;
            cmbBuilding.DisplayMember = "cName";
            cmbBuilding.ValueMember = "id";
        }
        private void InitFloors()
        {
            Task<DataTable> task = _proc.getFloors();
            task.Wait();
            cmbFloor.DataSource = task.Result;
            cmbFloor.DisplayMember = "cName";
            cmbFloor.ValueMember = "id";
        }
        #endregion

        private void GetData()
        {
            Task<DataTable> task = _proc.getSectionsDiscount(dtpStart.Value,dtpEnd.Value);
            task.Wait();
            dtData = task.Result;
            dgvSections.DataSource = dtData;
            filter();
        }
        private void filter()
        {
            string filter = "";
            try
            {
                filter += (filter.Length > 0 ? " and " : "") + $"id_TypeContract = {cmbTypeContract.SelectedValue}";
                if (cmbBuilding.SelectedIndex!=-1 && (int)cmbBuilding.SelectedValue!=0)
                    filter += (filter.Length > 0 ? " and " : "") + $"build_id = {cmbBuilding.SelectedValue}";
                if (cmbFloor.SelectedIndex!=-1 && (int) cmbFloor.SelectedValue!= 0)
                    filter += (filter.Length > 0 ? " and " : "") + $"floor_id = {cmbFloor.SelectedValue}";
                filter += (filter.Length > 0 ? " and " : "") + $"obj_id = {cmbObject.SelectedValue}";
                filter += (filter.Length > 0 ? " and " : "") + $"place_name like '%{tbName.Text}%'";
                filter += (filter.Length > 0 ? " and " : "") + $"tenant_name like '%{tbTenant.Text}%'";
                dtData.DefaultView.RowFilter = filter;
            }
            catch
            {

            }
        }

        private void cmbTypeContract_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (dtData.AsEnumerable().Where(r => r.Field<bool>("checked")).Count() > 0)
            {
                MessageBox.Show("Выбранные секции будут сборшены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                foreach (DataRow dr in dtData.AsEnumerable().Where(r => r.Field<bool>("checked")))
                {
                    dr["checked"] = false;
                }
                dtData.AcceptChanges();
            }
                //1 - секция, 2 - реклама, 3 - земля
            int id_typeContract = (int) cmbTypeContract.SelectedValue;
            if (id_typeContract == 1)
            {
                cmbBuilding.Enabled = cmbFloor.Enabled = true;
                if (cmbBuilding.SelectedIndex == -1)
                    cmbBuilding.SelectedValue = 0;
                if (cmbFloor.SelectedIndex == -1)
                    cmbFloor.SelectedValue = 0;
                (cmbTypeDiscount.DataSource as DataTable).DefaultView.RowFilter = "";
            }
            if (id_typeContract == 2)
            {
                cmbBuilding.Enabled = true;
                if (cmbBuilding.SelectedIndex == -1)
                    cmbBuilding.SelectedValue = 0;
                cmbFloor.Enabled = false;
                cmbFloor.SelectedIndex = -1;
                (cmbTypeDiscount.DataSource as DataTable).DefaultView.RowFilter = "id <> 2";
            }
            if (id_typeContract == 3)
            {
                cmbBuilding.Enabled = cmbFloor.Enabled = false;
                cmbBuilding.SelectedIndex = cmbFloor.SelectedIndex = -1;
                (cmbTypeDiscount.DataSource as DataTable).DefaultView.RowFilter = "";
            }
    

            filter();

        }

        private void cmb_SelectionChangeCustom(object sender, EventArgs e)
        {           
            filter();
        }

        private void cmb_SelectionChangeWithReset(object sender, EventArgs e)
        {
            if (dtData.AsEnumerable().Where(r => r.Field<bool>("checked")).Count() > 0)
            {
                MessageBox.Show("Выбранные секции будут сборшены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                foreach (DataRow dr in dtData.AsEnumerable().Where(r=>r.Field<bool>("checked")))
                {
                    dr["checked"] = false;
                }
                dtData.AcceptChanges();
            }
            filter();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbPrice.Text.Length == 0)
            {
                MessageBox.Show("Не задана скидка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbPrice.Focus();
                return;
            }
            decimal tryparse = 0.00M;
            if (!decimal.TryParse(tbPrice.Text, out tryparse))
            {
                MessageBox.Show("Введена неверная скидка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbPrice.Focus();
                return;
            }
            EnumerableRowCollection<DataRow> rowcoll = dtData.AsEnumerable().Where(r => r.Field<bool>("checked"));
            if (rowcoll.Count() == 0)
            {
                MessageBox.Show("Не выбраны секции", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string id_agreemets = "";
            foreach (DataRow dr in rowcoll)
            {
                id_agreemets += "," + dr["id"].ToString();
            }
            id_agreemets = id_agreemets.Substring(1);
            Task<DataTable> task = _proc.validateDiscount(id_agreemets, dtpStart.Value, dtpEnd.Value, cbPermanent.Checked);
            task.Wait();
            if (task.Result.Rows.Count > 0)
            {
                string agreements = "";
                foreach (DataRow dr in task.Result.Rows)
                    agreements += dr["Agreement"].ToString() + "\r\n";
                MessageBox.Show("У договоров\r\n" + agreements + "уже есть скидки", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            task = _proc.setDiscounts(id_agreemets, dtpStart.Value.Date, dtpEnd.Value.Date, int.Parse(cmbTypeDiscount.SelectedValue.ToString()),
                decimal.Parse(tbPrice.Text), cbPermanent.Checked);
            task.Wait();


            Logging.StartFirstLevel((int)logEnum.Создание_массовой_скидки);

            Logging.Comment($"Дата начала: {dtpStart.Value.ToShortDateString()}");
            Logging.Comment($"Дата Окончания: {(cbPermanent.Checked ? "Постоянная скидка" : dtpStart.Value.ToShortDateString())}");
            Logging.Comment($"Тип скидки ID:{cmbTypeDiscount.SelectedValue}; Наименование:{cmbTypeDiscount.Text}");
            Logging.Comment($"Скидка: {tbPrice.Text}");
            Logging.Comment($"Тип договора ID:{cmbTypeContract.SelectedValue}; Наименование:{cmbTypeContract.Text}");
            Logging.Comment($"Объект ID:{cmbObject.SelectedValue}; Наименование:{cmbObject.Text}");

            Logging.Comment($"Здание ID:{cmbBuilding.SelectedValue}; Наименование:{cmbBuilding.Text}");
            Logging.Comment($"Этаж ID:{cmbFloor.SelectedValue}; Наименование:{cmbFloor.Text}");

            foreach (DataRow dr in rowcoll)
            {
                Logging.Comment($"ID договора:{dr["id"]}");
                Logging.Comment($"Здание :{dr["build_name"]}");
                Logging.Comment($"Этаж  :{dr["floor_name"]}");
                Logging.Comment($"Номер договора:{dr["agreement_name"]}");
                Logging.Comment($"Секция:{dr["place_name"]}");
                Logging.Comment($"Площадь:{dr["area"]}");
                Logging.Comment($"Арендатор:{dr["tenant_name"]}");
                Logging.Comment($"Цена 1кв.м:{dr["price"]}");                
            }

            Logging.StopFirstLevel();

            MessageBox.Show("Скидки добавлены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvSections_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0 && e.ColumnIndex ==cV.Index)
                dtData.DefaultView[e.RowIndex]["checked"] = !(bool)dtData.DefaultView[e.RowIndex]["checked"];
        }

        private void tbPrice_Validating(object sender, CancelEventArgs e)
        {
            if ((sender as TextBox).Text.Length > 0)
            {
                decimal parse = 0.00M;
                if (!decimal.TryParse((sender as TextBox).Text, out parse))
                {
                    MessageBox.Show("Введена неправильная скидка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbPrice.Focus();
                    tbPrice.Text = "";
                    return;
                }
                (sender as TextBox).Text = decimal.Parse((sender as TextBox).Text.Replace(".", ",")).ToString("0.00");
            }
            else
                (sender as TextBox).Text = "0,00";
        }

        private void tbPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.ToString().Contains(e.KeyChar) || (sender as TextBox).Text.ToString().Length == 0))
            {
                e.Handled = true;
            }
            else
                if ((!Char.IsNumber(e.KeyChar) && (e.KeyChar != ',')))
            {
                if (e.KeyChar != '\b')
                { e.Handled = true; }
            }
            if (tbPrice.Text.Contains(",") && (tbPrice.Text.Length - tbPrice.Text.IndexOf(',')  > 2) && e.KeyChar!='\b')
                e.Handled = true;
        }

        private void frmDiscounts_Paint(object sender, PaintEventArgs e)
        {
            tbName.Location = new Point(dgvSections.Location.X + cV.Width + cBuilding.Width + cFloor.Width + cNameDog.Width, dgvSections.Location.Y - 30);
            tbName.Size = new Size(cName.Width - 2, tbName.Size.Height);

            tbTenant.Location = new Point(dgvSections.Location.X + cV.Width + cBuilding.Width + cFloor.Width + cName.Width + cArea.Width + cPrice.Width, dgvSections.Location.Y - 30);
            tbTenant.Size = new Size(cTenant.Width - 2, tbName.Size.Height);
        }

        private void dgvSections_Paint(object sender, PaintEventArgs e)
        {
            tbName.Location = new Point(dgvSections.Location.X + cV.Width + cBuilding.Width + cFloor.Width, dgvSections.Location.Y - 30);
            tbName.Size = new Size(cName.Width - 2, tbName.Size.Height);

            tbTenant.Location = new Point(dgvSections.Location.X + cV.Width + cBuilding.Width + cFloor.Width + cName.Width + cArea.Width + cPrice.Width, dgvSections.Location.Y - 30);
            tbTenant.Size = new Size(cTenant.Width - 2, tbName.Size.Height);
        }
        bool check = true;
        private void dgvSections_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == cV.Index)
            {
                if (dtData.DefaultView.Count>0)
                {
                    
                    foreach(DataRowView dr in dtData.DefaultView)
                    {
                        dr["checked"] = check;
                    }
                    check = !check;
                    dtData.AcceptChanges();
                }
            }
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            if (dtpEnd.Value < dtpStart.Value)
                dtpStart.Value = dtpEnd.Value;
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStart.Value > dtpEnd.Value)
                dtpEnd.Value = dtpStart.Value;
        }

        private void dgvSections_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            //Рисуем рамку для выделеной строки
            if (dgv.Rows[e.RowIndex].Selected)
            {
                int width = dgv.Width;
                Rectangle r = dgv.GetRowDisplayRectangle(e.RowIndex, false);
                Rectangle rect = new Rectangle(r.X, r.Y, width - 1, r.Height - 1);

                ControlPaint.DrawBorder(e.Graphics, rect,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid);
            }
        }

        private void dgvSections_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                Color rColor = Color.White;
                //if (!(bool)dtData.DefaultView[e.RowIndex]["isActive"])
                //    rColor = panel1.BackColor;
                dgvSections.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                dgvSections.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
                dgvSections.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;

            }
        }

        private void dtpStart_Leave(object sender, EventArgs e)
        {
            GetData();
        }

        private void dtpEnd_Leave(object sender, EventArgs e)
        {
            GetData();
        }
    }
}
