using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arenda
{
    public partial class frmPenniTest : Form
    {
        public frmPenniTest(DataTable dt)
        {
            InitializeComponent();
            dataGridView1.DataSource = dt;
        }

        private void frmPenniTest_Load(object sender, EventArgs e)
        {

        }
    }
}
