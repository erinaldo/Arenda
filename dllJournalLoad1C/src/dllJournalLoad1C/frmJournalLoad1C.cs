using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            //if (!(bool)dtData.DefaultView[e.RowIndex]["isActive"])
            //  rColor = panel1.BackColor;
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
                btSave.Enabled = false;
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
                btSave.Enabled = dtData.DefaultView.Count != 0;
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
            //try
            //{
            //    string attachFile = @"C:\Users\user\Downloads\5364ab4274a6f36b8d4340e42d7bfbed.jpg";
            //    string from = "harelove@yandex.ru";

            //    MailMessage mail = new MailMessage();
            //    mail.From = new MailAddress(from);
            //    mail.To.Add(new MailAddress("nonenane@gmail.com"));
            //    mail.Subject = "head test";
            //    mail.Body = "test";
            //    if (!string.IsNullOrEmpty(attachFile))
            //        mail.Attachments.Add(new Attachment(attachFile));
            //    SmtpClient client = new SmtpClient();
            //    client.Host = "smtp.yandex.ru";
            //    client.Port = 465;
            //    client.EnableSsl = true;
            //    client.Credentials = new NetworkCredential(from.Split('@')[0], ".ktxrf");
            //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    client.UseDefaultCredentials = false;
            //    client.Send(mail);
            //    mail.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Mail.Send: " + ex.Message);
            //}
            string attachFile = @"C:\Users\user\Downloads\5364ab4274a6f36b8d4340e42d7bfbed.jpg";
            string from = "harelove@yandex.ru";
            using (MailMessage mm = new MailMessage(from, "nonenane@yandex.ru"))
            {
                mm.Subject = "Mail Subject";
                mm.Body = "Mail Body";
                mm.IsBodyHtml = false;
                //if (!string.IsNullOrEmpty(attachFile))
                //    mm.Attachments.Add(new Attachment(attachFile));
                using (SmtpClient sc = new SmtpClient("smtp.yandex.ru", 465))
                {
                    sc.EnableSsl = true;
                    //sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //sc.UseDefaultCredentials = false;
                    sc.Credentials = new NetworkCredential(from, "seibjzyxnztaewle");
                    sc.Send(mm);
                }
            }

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
