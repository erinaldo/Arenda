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
  public partial class frmLoad : Form
  {
    public string TextWait
    {
      //get { return password; }
      set { label1.Text = value; }
    }

    public frmLoad()
    {
      InitializeComponent();
    }

    private bool _altF4Pressed = false;
    private void frmLoad_KeyPress(object sender, KeyPressEventArgs e)
    {

    }

    private void frmLoad_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Alt && e.KeyCode == Keys.F4)
        _altF4Pressed = true;
    }

    private void frmLoad_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (_altF4Pressed)
      {
        if (e.CloseReason == CloseReason.UserClosing)
          e.Cancel = true;
        _altF4Pressed = false;
      }
    }
  }
}
