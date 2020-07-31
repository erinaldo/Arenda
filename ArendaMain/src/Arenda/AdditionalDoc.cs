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
using Nwuram.Framework.Logging;

namespace Arenda
{
    public partial class AdditionalDoc : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        int _id , _id_type_doc, _id_type_dog;
        int y;
        string Area;
        string mes;
        DateTime start, stop, depart;
        DataTable dtTypes;

        public AdditionalDoc(int id , DateTime str, DateTime st, int idtd)
        {            
            InitializeComponent();
            _id = id;
            _id_type_dog = idtd;
            start = str;
            stop = st;
        }

        private void FormatSumms()
        {
            if (tbAreaNew.Text == "")
                tbAreaNew.Text = "0";
            tbAreaNew.Text = String.Format("{0:### ### ##0.00}", decimal.Parse(tbAreaNew.Text)).Trim();            
        }

        private void fillcb()
        {
            dtTypes = _proc.FillCbTD();

            if ((dtTypes != null) && (dtTypes.Rows.Count > 0))
            {
                cbTypeDoc.DataSource = dtTypes;
                if (_id_type_dog == 3)
                  dtTypes.DefaultView.RowFilter = "id in (3, 6, 7)";
            }
            else
            {
                this.Close();
            }
        }


        private void btExit_Click(object sender, EventArgs e)
        {
            /*if (cbTypeDoc.Text != "" || dateadddoc.Value != start || dateren.Value != stop || tbAreaNew.Text != Area || dtpDeparture.Value != depart)
            {
                if (MessageBox.Show("На форме были внесены изменения. \nВыйти без сохранения?", "Запрос на выход", MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                { DialogResult = DialogResult.Cancel; }
            }
            else*/   DialogResult = DialogResult.Cancel;
        }

        private void btAdd_Click(object sender, EventArgs e)
        {  
            int? num=0;
            decimal? AreaS;
            
            if (tbAreaNew.Text == Area)
            {
                AreaS = null;
            }
            else
            {
                AreaS = decimal.Parse(tbAreaNew.Text);
            }
            
            if (tbNumber.Text == "")
            { num = null; }
            else
            { num = Convert.ToInt32(tbNumber.Text); }

            if (cbTypeDoc.Text == "")
            { 
                MessageBox.Show("Не выбран тип доп. документа.\nСохранение невозможно", "Сохранение доп.документа");
                return;
            }

            if ((tbAreaNew.Visible == true) && (tbAreaNew.Text == Area))
            {
                MessageBox.Show("Не заполнено поле \"Общ. площадь\".\nСохранение невозможно", "Сохранение доп.документа");
                return;
            }

            if (CheckDate())
            {
                return;
            }

            DateTime? prolong;
            if (dateren.Visible == true)
                prolong = dateren.Value;
            else
                prolong = null;

            DateTime? departureDate;
            if (dtpDeparture.Visible == true)
                departureDate = dtpDeparture.Value;
            else
                departureDate = null;

            if(dtpOutDate.Visible)
                departureDate = dtpOutDate.Value.Date;

            string comment = null;
            if (tbComment.Visible)
                comment = tbComment.Text;



            _proc.AddeditTD(1, _id, Convert.ToDateTime(dateadddoc.Text), _id_type_doc, num, prolong, AreaS, departureDate, comment);

            Logging.StartFirstLevel(1402);
            //Logging.Comment("ID: " + id_DopDoc);
            Logging.Comment("№ документа: " + num);
            Logging.Comment("Тип документа ID: " + _id_type_doc + " ; Наименование: " + cbTypeDoc.Text);
            Logging.Comment("Дата документа: " + dateadddoc.Value.ToShortDateString());

            Logging.Comment("Данные арендатора, к которому добавляется доп.документ");
            Logging.Comment("Дата заключения договора: " + oldDoc.ToShortDateString());
            Logging.Comment("Номер договора: " + num_doc);
            Logging.Comment("Арендатор ID: " + _old_id_ten + "; Наименование: " + oldTen);
            Logging.Comment("Арендодатель ID: " + _old_id_lord + "; Наименование: " + oldLord);


            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
            + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();

            MessageBox.Show("Данные сохранены", "Сохранение доп.документа");
            DialogResult = DialogResult.Cancel;
        }

        private string num_doc, oldTen, oldLord;
        private int _old_id_ten, _old_id_lord;
        private DateTime oldDoc;

        public void setDocData (string num_doc, string oldTen, string oldLord, int _old_id_ten, int _old_id_lord,DateTime oldDoc)
        {
            this.num_doc = num_doc;
            this.oldTen = oldTen;
            this.oldLord = oldLord;

            this._old_id_ten = _old_id_ten;
            this._old_id_lord = _old_id_lord;

            this.oldDoc = oldDoc;
        }

        private void cbTypeDoc_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbTypeDoc.SelectedValue != null)
            {
                //DataTable dtTypeDoc = new DataTable();
                //dtTypeDoc = _proc.FillCbTD();
                //y = cbTypeDoc.SelectedIndex;
                //_id_type_doc = Convert.ToInt32(_proc.FillCbTD().Rows[y][0].ToString());
                _id_type_doc = int.Parse(cbTypeDoc.SelectedValue.ToString());
                y = dtTypes.Rows.IndexOf(dtTypes.Select("id = " + _id_type_doc.ToString())[0]);

                lblDeparture.Visible = false;
                dtpDeparture.Visible = false;
                lblAreaNew.Visible = false;
                tbAreaNew.Visible = false;
                label4.Visible = false;
                dateren.Visible = false;

                tbComment.Visible = false;
                dtpOutDate.Visible = false;
                tbNumber.Visible = true;

                label4.Text = "Дата продления\nдоговора";
                label3.Text = "№";
                dateadddoc.Location = new Point(128, 40);
                label2.Text = "Дата доп. документа:";

                if (dtTypes.Rows[y]["NeedProlong"].ToString() == "True")
                {
                    label4.Text = "Дата продления \nдоговора";
                    mes = "Дата продления договора";
                    if (_proc.FillCbTD().Rows[y]["Rus_Name"].ToString() == "Соглашение о расторжении договора")
                    {
                        label4.Text = "Дата расторжения \nдоговора";
                        mes = "Дата расторжения договора";
                        lblDeparture.Visible = true;
                        dtpDeparture.Visible = true;
                    }

                    if (dtTypes.Rows[y]["Rus_Name"].ToString() == "Доп. соглашение на изменение площади")
                    {
                        label4.Text = "Дата вступления \nв силу";
                        mes = "Дата вступления в силу";
                    }

                    label4.Visible = true;
                    dateren.Visible = true;
                }


                if (dtTypes.Rows[y]["NeedChangeArea"].ToString() == "True")
                {
                    lblAreaNew.Visible = true;
                    tbAreaNew.Visible = true;
                }

                if (dtTypes.Rows[y]["Rus_Name"].Equals("Заявление на съезд"))
                {
                    label4.Visible = true;
                    label4.Text = "Примечание";
                    tbComment.Visible = true;
                    dtpOutDate.Visible = true;
                    label3.Text = "Планируемая дата съезда:";
                    tbNumber.Visible = false;
                    dateadddoc.Location = new Point(190, 40);
                    label2.Text = "Дата подачи заявления: ";
                }
                else
                    if (dtTypes.Rows[y]["Rus_Name"].Equals("Аннуляция заявления на съезд"))
                {

                }
            }
        }

        private bool CheckDate()
        {
          if (cbTypeDoc.SelectedValue != null)
          {
            if (dateadddoc.Value < start)
            {
              MessageBox.Show("Дата доп. документа \nне должна быть меньше даты договора");
              return true;
            }

            if (dateren.Visible == true)
            {
              if ((dtTypes.Rows[y][1].ToString() == "Соглашение о расторжении договора")
                    || (dtTypes.Rows[y][1].ToString() == "Доп. соглашение на изменение площади"))
              {
                if (dateren.Value < start)
                {
                  MessageBox.Show(mes + " не должна быть \nменьше даты договора");
                  return true;
                }
              }
              else
              {
                if (dateren.Value < stop)
                {
                  MessageBox.Show(mes + " не должна быть \nменьше даты окончания договора +1 ");
                  return true;
                }
              }
            }

            if (dtpDeparture.Visible == true)
            {
              DataTable dtAgreement = new DataTable();
              dtAgreement = _proc.GetLD(_id);

              if ((dtAgreement == null) || (dtAgreement.Rows.Count == 0))
              {
                MessageBox.Show("Ошибка получения данных по договору!");
                return true;
              }

              DateTime AgrDate = DateTime.Parse(dtAgreement.Rows[0]["Date_of_Conclusion"].ToString()).Date;
              if (dtpDeparture.Value.Date < AgrDate)
              {
                MessageBox.Show("Дата договора - " + AgrDate.ToShortDateString() + "\nДата выезда не может быть меньше!");
                return true;
              }
            }

            DataTable dtCheckSameDocTypeAndDateExists = new DataTable();
            dtCheckSameDocTypeAndDateExists = _proc.CheckSameDocTypeAndDateExists(
                        _id,
                        int.Parse(cbTypeDoc.SelectedValue.ToString()),
                        dateadddoc.Value.Date);

            if (dtCheckSameDocTypeAndDateExists.Rows.Count > 0)
            {
              MessageBox.Show("Для договора уже существует документ \n\"" + cbTypeDoc.Text
                  + "\" от " + dateadddoc.Value.ToShortDateString() + "\nСохранение невозможно.");
              return true;
            }

            //если выбрано "Доп.соглашение на изменение площади"
            if (int.Parse(cbTypeDoc.SelectedValue.ToString()) == 5)
            {
              if (CheckPaymentsOnMonthth(_id, dateren.Value.Date, "Дата вступления в силу"))
              {
                return true;
              }
            }

            //если выбрано "Соглашение о расторжении договора"
            if (int.Parse(cbTypeDoc.SelectedValue.ToString()) == 4)
            {
              if (CheckPaymentsOnMonthth(_id, dtpDeparture.Value.Date, "Дата выезда"))
              {
                return true;
              }
            }
            return false;
          }
            return false;
        }

        private bool CheckPaymentsOnMonthth(int id, DateTime date, string CalendarName)
        {
            bool res = false;            

            DataTable dt = new DataTable();
            dt = _proc.CheckPaymentsOnMonth(id, date);

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("За месяц, выбранный в календаре \n\"" + CalendarName
                    + "\", уже имеются оплаты по договору. \nСохранение дополнительного документа невозможно.");
                return true;
            }

            return res;
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

        private void tbAreaNew_Leave(object sender, EventArgs e)
        {
            FormatSumms();
        }

        private void lockSimbols(KeyPressEventArgs e)
        {
            Regex pat = new Regex(@"[\b]|[0-9]|[,]");
            bool b = pat.IsMatch(e.KeyChar.ToString());
            if (b == false)
            {
                e.Handled = true;
            }

        }

        private void tbAreaNew_KeyPress(object sender, KeyPressEventArgs e)
        {
            lockSimbols(e);
        }

        private void AdditionalDoc_Load(object sender, EventArgs e)
        {
            fillcb();
            label4.Visible = false;
            dateren.Visible = false;
            lblAreaNew.Visible = false;
            tbAreaNew.Visible = false;

            start = dateadddoc.Value = start;
            depart = dtpDeparture.Value = stop = dateren.Value = stop.AddDays(1);
            FormatSumms();
            Area = tbAreaNew.Text;
        }

    }
}
