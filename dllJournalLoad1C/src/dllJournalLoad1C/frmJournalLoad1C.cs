using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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


        private DataTable dtLandLord, dtData;
        public frmJournalLoad1C()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
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
                btExcel.Enabled = false;
                return;
            }

            try
            {
                string filter = "";

                if (tbTenant.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"nameTenant like '%{tbTenant.Text.Trim()}%'";

                if (tbAgreements.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"Agreement like '%{tbAgreements.Text.Trim()}%'";

                if (tbPlace.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"namePlace like '%{tbPlace.Text.Trim()}%'";

                if ((int)cmbTypeDoc.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_TypeContract = {cmbTypeDoc.SelectedValue}";

                dtData.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally
            {
                btExcel.Enabled = dtData.DefaultView.Count != 0;
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
            IdAgreement = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];
            Agreement = (string)dtData.DefaultView[dgvData.CurrentRow.Index]["Agreement"];
            DialogResult = DialogResult.OK;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            selectAgreement();
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

        private void GetData()
        {
            int id_Object = (int)cmbObject.SelectedValue;
            dtData = Config.hCntMain.GetListJournalLoad1C(id_Object, dtpStart.Value.Date, dtpEnd.Value.Date);
            setFilter();
            dgvData.DataSource = dtData;
        }
    }
}
