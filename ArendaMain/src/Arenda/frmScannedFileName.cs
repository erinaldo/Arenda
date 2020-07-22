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
	public partial class frmScannedFileName : Form
	{
		public frmScannedFileName()
		{
			InitializeComponent();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			txtName.Text = txtName.Text.Replace("\\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("|", "");
			
			if (txtName.Text.Length > 0)
			{
				TempData.ScannedFileName = txtName.Text;
				this.Close();
			}
			else
			{
				MessageBox.Show("Не заполнено имя файла. \nИмя не должно содержать символов \\ / : * ? < > | ");				
			}
		}


	}
}
