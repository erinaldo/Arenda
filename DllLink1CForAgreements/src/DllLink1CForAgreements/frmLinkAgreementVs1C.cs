using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DllLink1CForAgreements
{
    public partial class frmLinkAgreementVs1C : Form
    {
        private DataTable dtData;

        public frmLinkAgreementVs1C()
        {
            InitializeComponent();
        }

        private void frmLinkAgreementVs1C_Load(object sender, EventArgs e)
        {

        }

        private void dgvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            Color rColor = Color.White;
            //if (!(bool)dtData.DefaultView[e.RowIndex]["isActive"])
            //  rColor = panel1.BackColor;
            dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
            dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
            dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
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

        private void dgvData_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            int width = 0;
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                if (!col.Visible) continue;
               
                if (col.Index == cAgreements.Index)
                {
                    tbAgreements.Location = new Point(dgvData.Location.X + width + 1, tbAgreements.Location.Y);
                    tbAgreements.Size = new Size(cAgreements.Width, tbAgreements.Height);
                }

                if (col.Index == cNum1C.Index)
                {
                    tbNum1C.Location = new Point(dgvData.Location.X + width + 1, tbAgreements.Location.Y);
                    tbNum1C.Size = new Size(col.Width, tbAgreements.Height);
                }

                width += col.Width;
            }

        }

        private void dgvData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
        }

        private void frmLinkAgreementVs1C_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dtData != null && dtData.Rows.Count > 0)
            {
                EnumerableRowCollection<DataRow> rowCollect = dtData.AsEnumerable().Where(r => r.Field<object>("") == null);               
                e.Cancel = rowCollect.Count() > 0 && DialogResult.No == MessageBox.Show("На форме есть не сохранённые данные.\nЗакрыть форму без сохранения данных?\n", "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            }
        }
    }
}
