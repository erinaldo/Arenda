using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Logging;
using Exc = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace Arenda
{
    public partial class frmBanks : Form
    {
        readonly Procedures proc = new Procedures(ConnectionSettings.GetServer(),
          ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(),
          ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        int _choose;

        public frmBanks(int choose)
        {
            InitializeComponent();

            if (Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.ToLower().Equals("пр"))
            {
                Logging.StartFirstLevel(1394);
                Logging.Comment("Открыта форма просмотра справочника банков");

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }

            _choose = choose;
        }

        private void frmBanks_Load(object sender, EventArgs e)
        {
            //if (TempData.Rezhim == "ПР")
            if (new List<string> { "СОА", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim))
            {
                btAdd.Visible =
                    btEdit.Visible =
                    btChoose.Visible =
                    false;

                btChoose.Visible = new List<string> { "МНД" }.Contains(TempData.Rezhim);

                dgBanks.ReadOnly = true;
            }

            if (TempData.Rezhim == "МН")
            {
                btAdd.Visible = btEdit.Visible = false;
            }

            if (_choose == 1)
            {
                btChoose.Enabled = true;                
            }
            else
                btChoose.Enabled = false;

            GetBanks();
            SetButtonsEnabled();
        }

        private void GetBanks()
        {
            DataTable dtBanks = proc.getBank();
            dtBanks.DefaultView.Sort = "cName";
            dgBanks.DataSource = dtBanks;
            Filter();
        }

        private void SetButtonsEnabled()
        {
            if (Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.ToLower() == "адм")
            {
                btDel.Enabled = dgBanks.Rows.Count > 0 && dgBanks.CurrentRow != null;
                btEdit.Enabled = dgBanks.Rows.Count > 0 && dgBanks.CurrentRow != null
                  && bool.Parse(dgBanks.CurrentRow.Cells["isActive"].Value.ToString());
            }
            else
                btAdd.Visible = btEdit.Visible = btDel.Visible = false;
        }

        private void Filter()
        {
            if (dgBanks.DataSource != null)
            {
                string filter = "";

                if (tbName.Text.Length > 0)
                    filter += "cName like '%" + tbName.Text + "%'";

                if (tbCA.Text.Length > 0)
                    filter += (filter.Length > 0 ? " and " : "") + "CorrespondentAccount like '%"
                      + tbCA.Text + "%'";

                if (tbBIC.Text.Length > 0)
                    filter += (filter.Length > 0 ? " and " : "") + "BIC like '%" + tbBIC.Text + "%'";

                if (!cbIsActive.Checked)
                    filter += (filter.Length > 0 ? " and " : "") + "isActive = 1";

                (dgBanks.DataSource as DataTable).DefaultView.RowFilter = filter;
                SetButtonsEnabled();
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            frmAddEditBank frm = new frmAddEditBank(0, "", "", "");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GetBanks();
            }
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void tbCA_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void tbBIC_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void cbIsActive_CheckedChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgBanks_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (!Convert.ToBoolean(dgBanks.Rows[e.RowIndex].Cells["isActive"].Value))
                {
                    dgBanks.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                      dgBanks.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor =
                      pnlInActive.BackColor;
                }
                else
                {
                    dgBanks.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                      dgBanks.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor =
                      Color.White;
                }
            }
        }

        private void dgBanks_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            //Рисуем рамку для выделеной строки
            if (dgv.Rows[e.RowIndex].Selected)
            {
                int width = dgv.Width;
                Rectangle r = dgv.GetRowDisplayRectangle(e.RowIndex, false);
                Rectangle rect = new Rectangle(r.X, r.Y, width - 1, r.Height - 1);

                ControlPaint.DrawBorder(e.Graphics, rect,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid);
            }
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(dgBanks.CurrentRow.Cells["isActive"].Value))
            {
                bool used = Convert.ToBoolean(dgBanks.CurrentRow.Cells["Used"].Value);
                string message = used ? "Выбранная для удаления запись\n    используется в программе.\nСделать запись недействующей?" : "Удалить выбранную запись?";
                if (MessageBox.Show(message, "Удаление записи", MessageBoxButtons.YesNo,
                  MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (!used)
                    {
                        DataTable dtBanks = proc.CheckBankIsNotDel(Convert.ToInt32(dgBanks.CurrentRow.Cells["id"].Value));
                        if (dtBanks == null || dtBanks.Rows.Count == 0)
                            MessageBox.Show("Запись уже удалена другим\n         пользователем.", "Удаление записи", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        else
                        {
                            proc.delBank(Convert.ToInt32(dgBanks.CurrentRow.Cells["id"].Value));
                            Logging.StartFirstLevel(1387);
                            Logging.Comment("ID: " + Convert.ToInt32(dgBanks.CurrentRow.Cells["id"].Value));
                            Logging.Comment("Наименование банка: " + dgBanks.CurrentRow.Cells["cName"].Value.ToString());
                            Logging.Comment("Корр. счет банка: " + dgBanks.CurrentRow.Cells["CorrespondentAccount"].Value.ToString());
                            Logging.Comment("БИК банка: " + dgBanks.CurrentRow.Cells["BIC"].Value.ToString());

                            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                            + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                            Logging.StopFirstLevel();
                        }
                    }
                    else
                    {
                        proc.addeditBank(Convert.ToInt32(dgBanks.CurrentRow.Cells["id"].Value),
                          dgBanks.CurrentRow.Cells["cName"].Value.ToString(),
                          dgBanks.CurrentRow.Cells["CorrespondentAccount"].Value.ToString(),
                          dgBanks.CurrentRow.Cells["BIC"].Value.ToString(), 0, 0);
                        Logging.StartFirstLevel(540);
                        Logging.Comment("Произведена смена статуса на недействующий у банка");
                        Logging.Comment("ID: " + Convert.ToInt32(dgBanks.CurrentRow.Cells["id"].Value));
                        Logging.Comment("Наименование банка: " + dgBanks.CurrentRow.Cells["cName"].Value.ToString());
                        Logging.Comment("Корр. счет банка: " + dgBanks.CurrentRow.Cells["CorrespondentAccount"].Value.ToString());
                        Logging.Comment("БИК банка: " + dgBanks.CurrentRow.Cells["BIC"].Value.ToString());

                        Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                        + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                        Logging.StopFirstLevel();
                    }
                    GetBanks();
                }
            }
            else if (MessageBox.Show("Сделать выбранную запись действующей?",
              "Восстановление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                proc.addeditBank(Convert.ToInt32(dgBanks.CurrentRow.Cells["id"].Value),
                  dgBanks.CurrentRow.Cells["cName"].Value.ToString(),
                  dgBanks.CurrentRow.Cells["CorrespondentAccount"].Value.ToString(),
                  dgBanks.CurrentRow.Cells["BIC"].Value.ToString(), 0, 1);
                Logging.StartFirstLevel(540);
                Logging.Comment("Произведена смена статуса на действующий у банка");
                Logging.Comment("ID: " + Convert.ToInt32(dgBanks.CurrentRow.Cells["id"].Value));
                Logging.Comment("Наименование банка: " + dgBanks.CurrentRow.Cells["cName"].Value.ToString());
                Logging.Comment("Корр. счет банка: " + dgBanks.CurrentRow.Cells["CorrespondentAccount"].Value.ToString());
                Logging.Comment("БИК банка: " + dgBanks.CurrentRow.Cells["BIC"].Value.ToString());

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
                GetBanks();
            }
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            frmAddEditBank frm = new frmAddEditBank(Convert.ToInt32(dgBanks.CurrentRow.Cells["id"].Value),
              dgBanks.CurrentRow.Cells["cName"].Value.ToString(),
              dgBanks.CurrentRow.Cells["CorrespondentAccount"].Value.ToString(),
              dgBanks.CurrentRow.Cells["BIC"].Value.ToString());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GetBanks();
            }
        }

        private void dgBanks_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (dgBanks.Columns["cName"] != null
              && dgBanks.Columns["CorrespondentAccount"] != null
              && dgBanks.Columns["BIC"] != null)
            {
                tbName.Width = dgBanks.Columns["cName"].Width;
                tbCA.Location = new Point(tbName.Location.X + tbName.Width + 1, tbCA.Location.Y);
                tbCA.Width = dgBanks.Columns["CorrespondentAccount"].Width;
                tbBIC.Location = new Point(tbCA.Location.X + tbCA.Width + 1, tbBIC.Location.Y);
                tbBIC.Width = dgBanks.Columns["BIC"].Width;
            }
        }

        private void dgBanks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SetButtonsEnabled();
        }

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void tbCA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void tbBIC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        string _fileName;
        frmLoad frmWait = new frmLoad();

        private void btExel_Click(object sender, EventArgs e)
        {
            var fd = new SaveFileDialog { Filter = @"Файлы Excel|*.xls" };
            fd.ShowDialog();
            if (fd.FileName.Trim().Length == 0)
            {
                return;
            }

            _fileName = fd.FileName.Trim();
            if (!_fileName.EndsWith(".xls", StringComparison.CurrentCultureIgnoreCase))
            {
                _fileName += ".xls";
            }

            this.Enabled = false;

            frmWait = new frmLoad();
            frmWait.TextWait = "ЖДИТЕ. ИДЁТ ВЫГРУЗКА";
            frmWait.Show();

            backgroundWorker1.RunWorkerAsync(new object[] { "\\Templates\\Sections.xls" });
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            frmWait.Dispose();

            Logging.StartFirstLevel(472);
            Logging.Comment("Выгрузка отчета со списком банков в Excel файл");

            Logging.Comment("Наименование Excel файла: " + System.IO.Path.GetFileName(_fileName));
            Logging.Comment("Путь выгрузки Excel файла: " + System.IO.Path.GetDirectoryName(_fileName));

            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
            + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();

            this.Enabled = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Exc.Application appExc = new Exc.Application();
            appExc.DisplayAlerts = false;
            appExc.Visible = false;
            appExc.SheetsInNewWorkbook = 1;
            Exc.Workbook book = appExc.Workbooks.Add(1);
            Exc.Worksheet sheet = (Exc.Worksheet)book.Worksheets[1];

            sheet.Cells[1, 1] = "Справочник банков";
            sheet.get_Range("A1", "C1").Merge();
            sheet.get_Range("A1", "C1").BorderAround();
            sheet.get_Range("A1", "A1").HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
            sheet.get_Range("A1", "A1").Font.Bold = true;
            sheet.get_Range("A1", "A1").Font.Size = 16;

            sheet.Cells[3, 1] = "Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername;
            sheet.get_Range("A3", "C3").Merge();

            sheet.Cells[4, 1] = "Дата выгрузки: " + DateTime.Now;
            sheet.get_Range("A4", "C4").Merge();

            sheet.Cells[6, 1] = "Наименование банка";
            sheet.Cells[6, 2] = "Корреспондентский счет";
            sheet.Cells[6, 3] = "БИК";

            sheet.get_Range("A6", "A6").ColumnWidth = 25;
            sheet.get_Range("B6", "B6").ColumnWidth = 23;
            sheet.get_Range("C6", "C6").ColumnWidth = 9;


            sheet.get_Range("A6", "A6").Font.Bold = true;
            sheet.get_Range("B6", "B6").Font.Bold = true;
            sheet.get_Range("C6", "C6").Font.Bold = true;

            sheet.get_Range("A6", "C6").HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
            sheet.get_Range("A6", "C6").Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            sheet.get_Range("A6", "C6").WrapText = true;

            if (dgBanks.Rows.Count > 0)
            {
                for (int i = 0; i < dgBanks.Rows.Count; i++)
                {
                    string A, G;
                    A = "A" + (i + 7);
                    G = "C" + (i + 7);

                    sheet.Cells[i + 7, 1] = dgBanks.Rows[i].Cells["cName"].Value;
                    sheet.Cells[i + 7, 2] = dgBanks.Rows[i].Cells["CorrespondentAccount"].Value;
                    sheet.Cells[i + 7, 3] = dgBanks.Rows[i].Cells["BIC"].Value;

                    sheet.get_Range(A, G).WrapText = true;
                    sheet.get_Range(A, G).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop;
                    sheet.get_Range(A, G).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    sheet.PageSetup.PrintArea = "A1:" + G;
                    sheet.get_Range("B6", G).NumberFormat = 0;
                }
            }
            sheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
            sheet.PageSetup.LeftMargin = 13.88;
            sheet.PageSetup.RightMargin = 13.88;
            sheet.PageSetup.TopMargin = 13.88;
            sheet.PageSetup.BottomMargin = 13.88;
            sheet.PageSetup.HeaderMargin = 0;
            sheet.PageSetup.FooterMargin = 0;
            appExc.Visible = true;
            object[] args = new object[2];
            args[0] = @_fileName;
            args[1] = 39;
            book.GetType().InvokeMember("SaveAs", BindingFlags.InvokeMethod, null, book, args);
        }

        private void btChoose_Click(object sender, EventArgs e)
        {
            dataBank.id = Convert.ToInt32(dgBanks.SelectedRows[0].Cells[0].Value);
            dataBank.cName = dgBanks.SelectedRows[0].Cells[1].Value.ToString();
            dataBank.cA = dgBanks.SelectedRows[0].Cells[2].Value.ToString();
            dataBank.BIK = dgBanks.SelectedRows[0].Cells[3].Value.ToString();
            DialogResult = DialogResult.Cancel;
        }
    }
}
