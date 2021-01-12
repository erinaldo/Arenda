using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dllJournalLoad1C
{
    public partial class frmLog : Form
    {
        public List<string> listError { set; private get; }
        public frmLog()
        {
            InitializeComponent();
        }

        private void frmLog_Load(object sender, EventArgs e)
        {
            foreach (string str in listError)
            {
                listBox1.Items.Add(str);
            }
            //string[] countries = { "Бразилия", "Аргентина", "Чили", "Уругвай", "Колумбия" };
            //listBox1.Items.AddRange(countries);
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
