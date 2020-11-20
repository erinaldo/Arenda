using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JournalBorrower
{
    public partial class frmListPaymentContract : Form
    {
        public int id_Agreements { set; private get; }
        public frmListPaymentContract()
        {
            InitializeComponent();
        }

        private void frmListPaymentContract_Load(object sender, EventArgs e)
        {
            Task<DataTable> tast = Config.hCntMain.GetListPaymentContractForAgreements(id_Agreements);
            tast.Wait();
            dgvData.DataSource = tast.Result;
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
