using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace Arenda
{
    public class GridToDataTable
    {
        /// <summary>
        /// Преобразование Грида в DataTable "как есть"
        /// </summary>
        /// <param name="dgv">Грид</param>
        /// <returns>таблица DataTable</returns>
        public static DataTable GetDataTableFromGrid(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            // Колонки
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Visible)
                {
                    DataColumn dc = new DataColumn(column.HeaderText.ToString());
                    dt.Columns.Add(dc);
                }
            }

            //Название колонок записываем в 1 строку
            DataRow headRow = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                headRow[dc.ColumnName] = dc.ColumnName;
            }
            dt.Rows.Add(headRow);

            // Строки
            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                DataRow dr = dt.NewRow();
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    if (dt.Columns.Contains(dgv.Columns[dgvCell.ColumnIndex].HeaderText))
                    {
                        dr[dgv.Columns[dgvCell.ColumnIndex].HeaderText] = (dgvRow.Cells[dgv.Columns[dgvCell.ColumnIndex].Name].Value == null) ? "" : dgvRow.Cells[dgv.Columns[dgvCell.ColumnIndex].Name].Value.ToString().Trim();
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;

        }

        /// <summary>
        /// Преобразование Грида в DataTable с добавлением колонки с нумерацией строк
        /// </summary>
        /// <param name="dgv">Грид</param>
        /// <returns>таблица DataTable</returns>
        public static DataTable GetDataTableFromGridWithNum(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            // Колонки
            DataColumn dcNum = new DataColumn("№");
            dt.Columns.Add(dcNum);

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Visible)
                {
                    DataColumn dc = new DataColumn(column.HeaderText.ToString());
                    dt.Columns.Add(dc);
                }
            }

            //Название колонок записываем в 1 строку
            DataRow headRow = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                headRow[dc.ColumnName] = dc.ColumnName;
            }
            dt.Rows.Add(headRow);

            // Строки
            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                DataRow dr = dt.NewRow();
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    if (dt.Columns.Contains(dgv.Columns[dgvCell.ColumnIndex].HeaderText))
                    {
                        dr[dgv.Columns[dgvCell.ColumnIndex].HeaderText] = (dgvRow.Cells[dgv.Columns[dgvCell.ColumnIndex].Name].Value == null) ? "" : dgvRow.Cells[dgv.Columns[dgvCell.ColumnIndex].Name].Value.ToString().Trim();
                    }
                }
                dt.Rows.Add(dr);
            }

            int z = 1;

            for (int i = 1; dt.Rows.Count > i; i++)
            {
                dt.Rows[i][0] = z.ToString();
                z++;
            }

            return dt;
        }
    }
}
