using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
namespace Arenda
{
    public partial class test : Form
    {   DataTable Bank;
           // DataTable _Bank;
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        int selected =-2;
        int select;
        int mode;

        DataView view = new DataView();

        public test()
        {
            InitializeComponent();
        }
        private void ini()
        {
            Bank = _proc.getBank();
            bds1.DataSource = Bank;
            dataGridView1.DataSource = bds1;                
        }
        private void test_Load(object sender, EventArgs e)
        {
            ini();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            select = e.RowIndex;
            if (select != selected && selected != -2 && selected != -1)
            {
                save();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            selected = e.RowIndex;
            dataGridView1.ReadOnly = false;
            if (dataGridView1.Rows[selected].Cells[1].Value.ToString() == "Введите наименование")
                mode = 1;
            else mode = 0;
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("выбранный" + selected.ToString() + "текущий " + select.ToString());
        }
        

        private void save()
        {
            
            string answer = String.Format("Данные были изменены. Сохранить изменения?");
            if (MessageBox.Show(answer, "Внимание", MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                _proc.addeditBank(Convert.ToInt32(dataGridView1.Rows[selected].Cells[0].Value), dataGridView1.Rows[selected].Cells[1].Value.ToString(), dataGridView1.Rows[selected].Cells[2].Value.ToString(), dataGridView1.Rows[selected].Cells[3].Value.ToString(), mode, 1);
                selected = -2;
                dataGridView1.ReadOnly = true;
                ini();

            }
            else
            {
                dataGridView1.ReadOnly = true;
                ini();
                selected = -2;
            } 

        }

        private void btEdit_Click(object sender, EventArgs e)
        {
             Bank.Rows.Add(1, "Введите наименование", 0, 0);
             dataGridView1.Update();
             int count;
             count = dataGridView1.Rows.Count;
             dataGridView1.Rows[count-1].Selected = true;
             dataGridView1.FirstDisplayedScrollingRowIndex = count-1;
             selected = count-1;
        }


        private void FilterDataView()
        {
            try
            {
                DataTable dt;
                string Fstring;
                if (sBanks.Text == "")
                { Fstring = "*"; }
                else Fstring = sBanks.Text;
                dt = Bank;
                view = dt.DefaultView;
                StringBuilder sb = new StringBuilder();
                sb.Append("cName like '%" + Fstring + "%'");
                view.RowFilter = sb.ToString();
            }
            catch (Exception) { }

        }

        private void sBanks_TextChanged(object sender, EventArgs e)
        {
            FilterDataView();
        }

       

    }
}
