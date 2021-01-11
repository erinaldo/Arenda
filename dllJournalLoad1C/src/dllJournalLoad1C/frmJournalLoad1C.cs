using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dllJournalLoad1C
{
    public partial class frmJournalLoad1C : Form
    {
        public int IdAgreement { private set; get; }
        public string Agreement { private set; get; }

        private NetworkShare net;
        private DataTable dtLandLord, dtData;
        public frmJournalLoad1C()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
            net = new NetworkShare(true, false);
        }

        private void frmSelectAgreementsTo1C_Load(object sender, EventArgs e)
        {
            DataTable dtObjectLease = Config.hCntMain.getObjectLease(true);

            cmbObject.DisplayMember = "cName";
            cmbObject.ValueMember = "id";
            cmbObject.DataSource = dtObjectLease;

            DataTable dtTypeDoc = Config.hCntMain.getTypeContract(true);

            cmbTypeDoc.DisplayMember = "cName";
            cmbTypeDoc.ValueMember = "id";
            cmbTypeDoc.DataSource = dtTypeDoc;

            dtLandLord = Config.hCntMain.GetListLandlord(true);            
            GetData();
        }

        private void dgvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            Color rColor = Color.White;
            if (dtData.DefaultView[e.RowIndex]["DateSendMail"]!=DBNull.Value)
              rColor = panel1.BackColor;
            dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
            dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
            dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void dgvData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void dgvData_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            int width = 0;
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                if (!col.Visible) continue;
                if (col.Index == nameTenant.Index)
                {
                    tbTenant.Location = new Point(dgvData.Location.X + width + 1, tbTenant.Location.Y);
                    tbTenant.Size = new Size(col.Width, tbTenant.Height);
                }

                if (col.Index == cAgreements.Index)
                {
                    tbAgreements.Location = new Point(dgvData.Location.X + width + 1, tbTenant.Location.Y);
                    tbAgreements.Size = new Size(col.Width, tbTenant.Height);
                }

                if (col.Index == cPlace.Index)
                {
                    tbPlace.Location = new Point(dgvData.Location.X + width + 1, tbTenant.Location.Y);
                    tbPlace.Size = new Size(col.Width, tbTenant.Height);
                }

                if (col.Index == cLandLord.Index)
                {
                    tbLandLord.Location = new Point(dgvData.Location.X + width + 1, tbTenant.Location.Y);
                    tbLandLord.Size = new Size(col.Width, tbTenant.Height);
                }

                if (col.Index == cAgreement1C.Index)
                {
                    tbAgreement1C.Location = new Point(dgvData.Location.X + width + 1, tbTenant.Location.Y);
                    tbAgreement1C.Size = new Size(col.Width, tbTenant.Height);
                }

                if (col.Index == cTypePay.Index)
                {
                    tbTypePay.Location = new Point(dgvData.Location.X + width + 1, tbTenant.Location.Y);
                    tbTypePay.Size = new Size(col.Width, tbTenant.Height);
                }

                width += col.Width;
            }
        }

        private void cmbObject_SelectionChangeCommitted(object sender, EventArgs e)
        {            
            GetData();
        }
       
        private void tbTenant_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void setFilter()
        {
            if (dtData == null || dtData.Rows.Count == 0)
            {
                btSendMail.Enabled =  btExcel.Enabled = false;
                return;
            }

            try
            {
                string filter = "";

                if (tbTenant.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"nameTenant like '%{tbTenant.Text.Trim()}%'";

                if (tbLandLord.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"nameLandLord like '%{tbLandLord.Text.Trim()}%'";

                if (tbAgreements.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"Agreement like '%{tbAgreements.Text.Trim()}%'";

                if (tbPlace.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"namePlace like '%{tbPlace.Text.Trim()}%'";

                if ((int)cmbTypeDoc.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_TypeContract = {cmbTypeDoc.SelectedValue}";

                if (tbAgreement1C.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"NumberAccount = {tbAgreement1C.Text.Trim()}";

                if (tbTypePay.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"TypePayment like '%{tbTypePay.Text.Trim()}%'";

                dtData.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally
            {
                btSendMail.Enabled = btExcel.Enabled = dtData.DefaultView.Count != 0;
            }
        }

        private void cmbLandLord_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            selectAgreement();
        }

        private void selectAgreement()
        {
            DataRowView viewRow = dtData.DefaultView[dgvData.CurrentRow.Index];
            IdAgreement = (int)viewRow["id"];
            Agreement = (string)viewRow["Agreement"];

            int id_Scan = (int)viewRow["id_Scan"];
            DataTable dtScanData = Config.hCntMain.getScan(0, id_Scan);

            if (dtScanData == null || dtScanData.Rows.Count == 0) return;
            
            byte[] img;
            img = net.GetFileWithPathBytes(dtScanData.Rows[0]["id_Doc"].ToString(), dtScanData.Rows[0]["cName"].ToString(), dtScanData.Rows[0]["Extension"].ToString(), dtScanData.Rows[0]["Path"].ToString());

            if (img == null) return;

            string fileName = dtScanData.Rows[0]["cName"].ToString() + dtScanData.Rows[0]["Extension"].ToString();
            if (!Directory.Exists("tmp\\"))
                Directory.CreateDirectory("tmp\\");
            fileName = "tmp\\" + fileName;
            File.WriteAllBytes(fileName, img);
            Process.Start(fileName);
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStart.Value.Date > dtpEnd.Value.Date)
                dtpEnd.Value = dtpStart.Value.Date;
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStart.Value.Date > dtpEnd.Value.Date)
                dtpStart.Value = dtpEnd.Value.Date;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = "harelove@yandex.ru";
            string pass = "xkrbtshtjivqlggu";


            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("nonenane@yandex.ru", "SGP");
            // кому отправляем
            MailAddress to = new MailAddress("nonenane@gmail.com");
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Тест";
            // текст письма
            m.Body = "<h2>Письмо-тест работы smtp-клиента</h2>";
            // письмо представляет код html
            m.IsBodyHtml = true;

            Attachment data = new Attachment(
                         @"D:\Disk E\Img\EjPg0vSUcAASql3.jpg",
                         MediaTypeNames.Application.Octet);
            // your path may look like Server.MapPath("~/file.ABC")
            m.Attachments.Add(data);

            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential(user, pass);
            smtp.EnableSsl = true;
            smtp.Send(m);

        }


        private Nwuram.Framework.UI.Service.EnableControlsServiceInProg blockers = new Nwuram.Framework.UI.Service.EnableControlsServiceInProg();
        private Nwuram.Framework.ToExcelNew.ExcelUnLoad report = null;

        private void setWidthColumn(int indexRow, int indexCol, int width, Nwuram.Framework.ToExcelNew.ExcelUnLoad report)
        {
            report.SetColumnWidth(indexRow, indexCol, indexRow, indexCol, width);
        }

        private async void btExcel_Click(object sender, EventArgs e)
        {
            if (dtData == null || dtData.Rows.Count == 0 || dtData.DefaultView.Count == 0)
            {
                MessageBox.Show("Нет данных для формирования отчёта.","Печать",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();

            int indexRow = 1;
            int maxColumns = 0;
            blockers.SaveControlsEnabledState(this);
            blockers.SetControlsEnabled(this, false);
            //progressBar1.Visible = true;
            var result = await Task<bool>.Factory.StartNew(() =>
            {

                foreach (DataGridViewColumn col in dgvData.Columns)
                    if (col.Visible)
                    {
                        maxColumns++;
                        if (col.Name.Equals(cDate.Name)) setWidthColumn(indexRow, maxColumns, 14, report);
                        if (col.Name.Equals(cObject.Name)) setWidthColumn(indexRow, maxColumns, 15, report);
                        if (col.Name.Equals(cLandLord.Name)) setWidthColumn(indexRow, maxColumns, 20, report);
                        if (col.Name.Equals(nameTenant.Name)) setWidthColumn(indexRow, maxColumns, 20, report);
                        if (col.Name.Equals(cAgreements.Name)) setWidthColumn(indexRow, maxColumns, 16, report);
                        if (col.Name.Equals(cTypeContract.Name)) setWidthColumn(indexRow, maxColumns, 20, report);
                        if (col.Name.Equals(cPlace.Name)) setWidthColumn(indexRow, maxColumns, 22, report);
                        if (col.Name.Equals(cAgreement1C.Name)) setWidthColumn(indexRow, maxColumns, 16, report);
                        if (col.Name.Equals(cDate1C.Name)) setWidthColumn(indexRow, maxColumns, 16, report);
                        if (col.Name.Equals(cTypePay.Name)) setWidthColumn(indexRow, maxColumns, 17, report);
                    }


                #region "Head"
                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue($"{this.Text}", indexRow, 1);
                report.SetFontBold(indexRow, 1, indexRow, 1);
                report.SetFontSize(indexRow, 1, indexRow, 1, 16);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
                indexRow++;
                indexRow++;

                Config.DoOnUIThread(() =>
                {
                    report.Merge(indexRow, 1, indexRow, maxColumns);
                    report.AddSingleValue($"Период с {dtpStart.Value.ToShortDateString()} по {dtpEnd.Value.ToShortDateString()} ", indexRow, 1);
                    indexRow++;

                    report.Merge(indexRow, 1, indexRow, maxColumns);
                    report.AddSingleValue($"Объект: {cmbObject.Text}", indexRow, 1);
                    indexRow++;

                    report.Merge(indexRow, 1, indexRow, maxColumns);
                    report.AddSingleValue($"Тип договора: {cmbTypeDoc.Text}", indexRow, 1);
                    indexRow++;


                    //if (tbEan.Text.Trim().Length != 0 || tbName.Text.Trim().Length != 0)
                    //{
                    //    report.Merge(indexRow, 1, indexRow, maxColumns);
                    //    report.AddSingleValue($"Фильтр: {(tbEan.Text.Trim().Length != 0 ? $"EAN:{tbEan.Text.Trim()} | " : "")} {(tbName.Text.Trim().Length != 0 ? $"Наименование:{tbName.Text.Trim()}" : "")}", indexRow, 1);
                    //    indexRow++;
                    //}

                }, this);

                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue("Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername, indexRow, 1);
                indexRow++;

                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue("Дата выгрузки: " + DateTime.Now.ToString(), indexRow, 1);
                indexRow++;
                indexRow++;
                #endregion

                int indexCol = 0;
                foreach (DataGridViewColumn col in dgvData.Columns)
                    if (col.Visible)
                    {
                        indexCol++;
                        report.AddSingleValue(col.HeaderText, indexRow, indexCol);
                    }
                report.SetFontBold(indexRow, 1, indexRow, maxColumns);
                report.SetBorders(indexRow, 1, indexRow, maxColumns);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                report.SetWrapText(indexRow, 1, indexRow, maxColumns);
                indexRow++;

                foreach (DataRowView row in dtData.DefaultView)
                {
                    indexCol = 1;
                    report.SetWrapText(indexRow, indexCol, indexRow, maxColumns);
                    foreach (DataGridViewColumn col in dgvData.Columns)
                    {
                        if (col.Visible)
                        {
                            if (row[col.DataPropertyName] is DateTime)
                                report.AddSingleValue(((DateTime)row[col.DataPropertyName]).ToShortDateString(), indexRow, indexCol);
                            else
                               if (row[col.DataPropertyName] is decimal || row[col.DataPropertyName] is double)
                            {
                                report.AddSingleValueObject(row[col.DataPropertyName], indexRow, indexCol);
                                report.SetFormat(indexRow, indexCol, indexRow, indexCol, "0.00");
                            }
                            else
                                report.AddSingleValue(row[col.DataPropertyName].ToString(), indexRow, indexCol);

                            indexCol++;
                        }
                    }

                    if (row["DateSendMail"] != DBNull.Value)
                        report.SetCellColor(indexRow, 1, indexRow, maxColumns, panel1.BackColor);

                    report.SetBorders(indexRow, 1, indexRow, maxColumns);
                    report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                    report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);

                    indexRow++;
                }

                indexRow++;
                report.SetCellColor(indexRow, 1, indexRow, 1, panel1.BackColor);
                report.Merge(indexRow, 2, indexRow, maxColumns);
                report.AddSingleValue($"{label5.Text}", indexRow, 2);

                Config.DoOnUIThread(() =>
                {
                    blockers.RestoreControlEnabledState(this);
                    //progressBar1.Visible = false;
                }, this);

                report.Show();
                return true;
            });
        }

        private async void btSendMail_Click(object sender, EventArgs e)
        {
            var result = await Task<bool>.Factory.StartNew(() =>
            {
                Config.DoOnUIThread(() =>
                {

                }, this);

                foreach (DataGridViewRow gridRow in dgvData.SelectedRows)
                {
                    DataRowView viewRow = dtData.DefaultView[gridRow.Index];

                    int id_Scan = (int)viewRow["id_Scan"];
                    DataTable dtScanData = Config.hCntMain.getScan(0, id_Scan);

                    if (dtScanData == null || dtScanData.Rows.Count == 0) continue;


                    byte[] img;
                    img = net.GetFileWithPathBytes(dtScanData.Rows[0]["id_Doc"].ToString(), dtScanData.Rows[0]["cName"].ToString(), dtScanData.Rows[0]["Extension"].ToString(), dtScanData.Rows[0]["Path"].ToString());

                    if (img == null) continue;

                    string fileName = dtScanData.Rows[0]["cName"].ToString() + dtScanData.Rows[0]["Extension"].ToString();
                    string user = viewRow["emailSender"].ToString();
                    string pass = "xkrbtshtjivqlggu";
                    string userName = (string)viewRow["nameLandLord"];
                    string ToEmail = viewRow["emailSend"].ToString();

                    if (user == null || user.Trim().Length == 0)
                    {
                        MessageBox.Show(Config.centralText("У арендодателя отсутствует email.\nОтправка счёта невозможна.\n"),"Отправка счёта по email арендатору",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return false;
                    }

                    if (ToEmail == null || ToEmail.Trim().Length == 0)
                    {
                        MessageBox.Show(Config.centralText("У арендатора отсутствует email.\nОтправка счёта невозможна.\n"), "Отправка счёта по email арендатору", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    sendMail(user, userName, pass, ToEmail, img, fileName);


                }
                return true;
            });
        }

        private void sendMail(string user,string userTitle, string pass,string toEmail, byte[] file,string fileName)
        {
            //string user = "harelove@yandex.ru";
            //string pass = "xkrbtshtjivqlggu";


            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress(user, userTitle);
            // кому отправляем
            MailAddress to = new MailAddress(toEmail);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = fileName;
            // текст письма
            m.Body = "";
            // письмо представляет код html
            m.IsBodyHtml = true;

            Attachment data = new Attachment(
                         new MemoryStream(file), fileName);
            // your path may look like Server.MapPath("~/file.ABC")

            //Attachment data = new Attachment(
            //             @"D:\Disk E\Img\EjPg0vSUcAASql3.jpg",
            //             MediaTypeNames.Application.Octet);
            //// your path may look like Server.MapPath("~/file.ABC")
            m.Attachments.Add(data);

            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential(user, pass);
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(m);
            }
            catch
            { }
        }

        private void GetData()
        {
            int id_Object = (int)cmbObject.SelectedValue;
            dtData = Config.hCntMain.GetListJournalLoad1C(id_Object, dtpStart.Value.Date, dtpEnd.Value.Date);
            setFilter();
            dgvData.DataSource = dtData;
        }
    }
}
