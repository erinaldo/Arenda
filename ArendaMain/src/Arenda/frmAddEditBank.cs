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
  public partial class frmAddEditBank : Form
  {
    readonly Procedures proc = new Procedures(ConnectionSettings.GetServer(),
      ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(),
      ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
    int id_bank;
    string bankName, CorrAcc, BIC;
    public frmAddEditBank(int id, string bname, string ca, string bic)
    {
      id_bank = id;
      bankName = bname;
      CorrAcc = ca;
      BIC = bic;
      InitializeComponent();
    }

    private void frmAddEditBank_Load(object sender, EventArgs e)
    {
      this.Text = id_bank == 0 ? "Добавить банк" :
        "Редактировать банк";
      tbName.Text = bankName;
      tbCA.Text = CorrAcc;
      tbBIC.Text = BIC;
      SetButtonsEnabled();
    }

    private void SetButtonsEnabled()
    {
      btSave.Enabled = SomethingChanged();
    }

    private bool SomethingChanged()
    {
      if (id_bank == 0)
        return (tbName.Text.Replace(" ", "").Length > 0
          && tbCA.Text.Replace(" ", "").Length == 20
          && tbBIC.Text.Replace(" ", "").Length == 9);
      else
        return (tbName.Text.Replace(" ", "").Length > 0
          && tbCA.Text.Replace(" ", "").Length == 20
          && tbBIC.Text.Replace(" ", "").Length == 9
          && (bankName != tbName.Text || CorrAcc != tbCA.Text || BIC != tbBIC.Text));
    }

    private void tb_TextChanged(object sender, EventArgs e)
    {
      SetButtonsEnabled();
    }

    private void btSave_Click(object sender, EventArgs e)
    {
      if(tbCA.Text.Trim().Length != 20)
      {
        MessageBox.Show("Длина корр. счёта должна быть равна 20 символам.\nСохранение невозможно.",
          "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      if (tbBIC.Text.Trim().Length != 9)
      {
        MessageBox.Show("Длина БИК должна быть равна 9 символам.\nСохранение невозможно.",
          "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      Save();
    }

    private void Save()
    {
      DataTable dtBank = proc.CheckBankName(id_bank, tbName.Text);
      if (dtBank == null || dtBank.Rows.Count == 0)
      {
        proc.addeditBank(id_bank, tbName.Text, tbCA.Text, tbBIC.Text,
          id_bank == 0 ? 1 : 0, 1);
        MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK,
          MessageBoxIcon.Information);
        this.DialogResult = DialogResult.OK;
      }
      else
      {
        if (bool.Parse(dtBank.Rows[0]["isActive"].ToString()))
        {
          MessageBox.Show("        В справочнике уже\n         присутствует банк\n   с таким наименованием.",
            "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
        else if (id_bank == 0)
        {
          if (MessageBox.Show("Введённое наименование присутствует в БД и имеет статус \"недействующая\". Вы хотите изменить статус на \"действующая\"?",
            "Сохранение записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2) == DialogResult.Yes)
          {
            proc.addeditBank(int.Parse(dtBank.Rows[0]["id"].ToString()),
              dtBank.Rows[0]["cName"].ToString(),
              dtBank.Rows[0]["CorrespondentAccount"].ToString(),
              dtBank.Rows[0]["BIC"].ToString(), 0, 1);
            MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK,
              MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
          }
        }
        else
        {
          MessageBox.Show("Введённое наименование присутствует в БД и имеет статус \"недействующая\". Сохранить введённое наименование нельзя.",
            "Сохранение записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }
      }
    }

    private void btExit_Click(object sender, EventArgs e)
    {
      if (SomethingChanged() && MessageBox.Show(" На форме есть несохранённые данные.\nЗакрыть форму без сохранения данных?",
        "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
      {
        return;
      }
      this.DialogResult = DialogResult.Cancel;
    }

    private void tb_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
        e.Handled = true;
    }
  }
}
