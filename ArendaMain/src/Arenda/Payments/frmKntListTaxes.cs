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

namespace Arenda.Payments
{
    public partial class frmKntListTaxes : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        private DataTable dtData;

        public frmKntListTaxes()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
            if (!new string[] { "КНТ" }.Contains(Nwuram.Framework.Settings.User.UserSettings.User.StatusCode))
            {
                btSelect.Enabled = false;
                cSelect.Visible = false;
            }
        }

        private void frmKntListTaxes_Load(object sender, EventArgs e)
        {
            DateTime nowDate = _proc.getdate();

            nowDate = nowDate.AddDays(-nowDate.Day + 1);

            DataTable dtPlaneDate = new DataTable();
            dtPlaneDate.Columns.Add("Date", typeof(string));
            dtPlaneDate.Columns.Add("valueDate", typeof(DateTime));
            dtPlaneDate.Rows.Add($"{nowDate.AddMonths(-1).Month}.{nowDate.AddMonths(-1).Year}", nowDate.AddMonths(-1));
            dtPlaneDate.Rows.Add($"{nowDate.Month}.{nowDate.Year}", nowDate);
            dtPlaneDate.Rows.Add($"{nowDate.AddMonths(1).Month}.{nowDate.AddMonths(1).Year}", nowDate.AddMonths(1));
            cmbPlaneDate.DataSource = dtPlaneDate;
            cmbPlaneDate.DisplayMember = "Date";
            cmbPlaneDate.ValueMember = "valueDate";

            getData();
        }

        private void getData()
        {
            if (cmbPlaneDate.SelectedValue == null)
            {
                dgvData.DataSource = null;                
            }

            DateTime DatePlane = (DateTime)cmbPlaneDate.SelectedValue;

            dtData = _proc.GetListTaxesForKnt(DatePlane);
            setFilter();
            dgvData.DataSource = dtData;
        }

        private void cmbPlaneDate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getData();
        }

        private void dgvData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void dgvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {

                Color rColor = Color.White;
                if ((bool)dtData.DefaultView[e.RowIndex]["isConfirmed"])
                    rColor = picBoxIsConfirmed.BackColor;

                dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;

                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;

                //if ((bool)dtTable.DefaultView[e.RowIndex]["isConfirmed"])
                //    dgvData.Rows[e.RowIndex].Cells[cFieldName.Index].Style.BackColor =
                //         dgvData.Rows[e.RowIndex].Cells[cFieldName.Index].Style.SelectionBackColor = panel1.BackColor;
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            EnumerableRowCollection<DataRow> rowCollect = dtData.AsEnumerable().Where(r => r.Field<bool>("isSelect") && !r.Field<bool>("isConfirmed"));
            if (rowCollect.Count() == 0)
            {
                MessageBox.Show("Необходимо выбрать не подтверждённые данные!","Подтверждение",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            
            foreach (DataRow row in rowCollect)
            {
                int id_taxes = (int)row["id"];
                DataTable dtResult = _proc.setConfirmedTaxes(id_taxes);
                if (dtResult == null || dtResult.Rows.Count == 0 || (int)dtResult.Rows[0]["id"] < 0)
                {
                    MessageBox.Show("Ошибка сохранения данных!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            MessageBox.Show("Данные сохранены!","Сохранение",MessageBoxButtons.OK,MessageBoxIcon.Information);

            getData();
            //this.DialogResult = DialogResult.OK;
        }

        private void chbIsConfirmed_Click(object sender, EventArgs e)
        {
            setFilter();
        }

        private void setFilter()
        {
            try
            {
                string filter = "";
                if (!chbIsConfirmed.Checked)
                    filter += $"isConfirmed = 0";

                dtData.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -9999";
            };
        }
    }
}
