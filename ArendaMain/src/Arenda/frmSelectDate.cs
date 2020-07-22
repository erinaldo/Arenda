using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Arenda
{
    public partial class frmSelectDate : Form
    {
        public DateTime dateMin { set;private get; }
        public DateTime date { private set; get; }
        public frmSelectDate()
        {
            InitializeComponent();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            date = dtpDate.Value.Date;
            this.DialogResult = DialogResult.OK;
        }

        private void frmSelectDate_Load(object sender, EventArgs e)
        {
            dtpDate.MinDate = dateMin.Date;
        }
    }
}
