using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using System.Text.RegularExpressions;

namespace Arenda
{
  public partial class frmAddDoc : Form
  {
    readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

    int _id;//, _id_type_dog;
    DateTime start;
    DataTable dtTypes;
    public int id_DocType;
    public DateTime date_Doc;
    public string name;

    public frmAddDoc(int id, DateTime str/*, int idtd*/)
    {
      InitializeComponent();
      _id = id;
      //_id_type_dog = idtd;
      fillcb();

      start = dateadddoc.Value = dateadddoc.MinDate = str;
    }

    private void fillcb()
    {
      dtTypes = _proc.GetDocTypes();

      if ((dtTypes != null) && (dtTypes.Rows.Count > 0))
      {
        cbTypeDoc.DataSource = dtTypes;
        cbTypeDoc.DisplayMember = "Rus_Name";
        cbTypeDoc.ValueMember = "id";
        /*if (_id_type_dog == 3)
          dtTypes.DefaultView.RowFilter = "id in (3, 6, 7)";*/
      }
      else
      {
        this.Close();
      }
    }

    private void btExit_Click(object sender, EventArgs e)
    {
      /*if (cbTypeDoc.Text != "" || dateadddoc.Value != start)
      {
        if (MessageBox.Show("На форме были внесены изменения. \nВыйти без сохранения?", "Запрос на выход", MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
        { DialogResult = DialogResult.Cancel; }
      }
      else*/ DialogResult = DialogResult.Cancel;
    }

    private void btAdd_Click(object sender, EventArgs e)
    {
      if (cbTypeDoc.Text == "")
      {
        MessageBox.Show("Не выбран тип доп. документа.\nСохранение невозможно", "Сохранение доп.документа");
        return;
      }
      else
      {
        int.TryParse(cbTypeDoc.SelectedValue.ToString(), out id_DocType);
        name = cbTypeDoc.Text;
      }

      if (CheckDate())
      {
        return;
      }

      this.DialogResult = DialogResult.OK;
      //_proc.AddeditTD(1, _id, Convert.ToDateTime(dateadddoc.Text), _id_type_doc, num, prolong, AreaS, departureDate);
      //MessageBox.Show("Данные сохранены", "Сохранение доп.документа");
      //DialogResult = DialogResult.Cancel;
    }

    private bool CheckDate()
    {
      /*if (dateadddoc.Value < start)
      {
        MessageBox.Show("Дата документа не должна быть меньше даты договора.", "Внимание", MessageBoxButtons.OK,
          MessageBoxIcon.Information);
        return true;
      }
      else*/
      date_Doc = dateadddoc.Value;
      name += " " + date_Doc.ToString().Substring(0,10);

      DataTable dtCheckDocTypeAndDate = new DataTable();
      dtCheckDocTypeAndDate = _proc.CheckDocTypeAndDate(
                  _id,
                  int.Parse(cbTypeDoc.SelectedValue.ToString()),
                  dateadddoc.Value.Date);

      if (dtCheckDocTypeAndDate.Rows.Count > 0)
      {
        /*MessageBox.Show("Для договора уже существует документ \n\"" + cbTypeDoc.Text 
            + "\" от " + dateadddoc.Value.ToShortDateString() + "\nСохранение невозможно.");
        return true;*/
        name += "_" + dtCheckDocTypeAndDate.Rows.Count.ToString();
      }

      return false;
    }

    private void tbNumber_KeyPress(object sender, KeyPressEventArgs e)
    {
      Regex pat = new Regex(@"[\b]|[0-9]|[\s]");
      bool b = pat.IsMatch(e.KeyChar.ToString());
      if (b == false)
      {
        e.Handled = true;
      }
    }
  }
}
