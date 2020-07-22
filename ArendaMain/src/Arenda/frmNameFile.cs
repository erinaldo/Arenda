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
  public partial class frmNameFile : Form
  {
    public string getComment { set; get; }
    public frmNameFile()
    {
      InitializeComponent();
      ToolTip tt = new ToolTip();
      tt.SetToolTip(btClose, "Выход");
      tt.SetToolTip(btSelect, "Подтвердить");
    }

    private void btClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btSelect_Click(object sender, EventArgs e)
    {
      if (tbName.Text.Trim().Length == 0)
      {
        MessageBox.Show("Необходимо ввести имя файла!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }

      getComment = tbName.Text.Trim();
      this.DialogResult = DialogResult.OK;
    }

    private void frmNameFile_Load(object sender, EventArgs e)
    {
      tbName.Text = getComment;
    }
  }
}
