using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.ToExcel;

namespace Arenda
{
    public partial class frmLordlandDailyReport : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        int id_LandLord;
        string Name_LandLord;

        public frmLordlandDailyReport(int id, string name)
        {
            InitializeComponent();
            id_LandLord = id;
            Name_LandLord = name;
        }

        private void frmLordlandDailyReport_Load(object sender, EventArgs e)
        {
            DateTime CurDate = _proc.getdate();
            dtpDate.Value 
                = dtpDate.MaxDate 
                = CurDate;
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            DataTable dtReport = new DataTable();

            dtReport = _proc.GetLordlandDailyReport(id_LandLord, dtpDate.Value.Date);

            if ((dtReport != null) && (dtReport.Rows.Count > 0))
            {


                for (int i = 0; dtReport.Rows.Count > i; i++)
                {
                    dtReport.Rows[i]["arendaSum"] = numTextBox.ConvertToCompPunctuation(dtReport.Rows[i]["arendaSum"].ToString());
                    dtReport.Rows[i]["arendaPeni"] = numTextBox.ConvertToCompPunctuation(dtReport.Rows[i]["arendaPeni"].ToString());
                    dtReport.Rows[i]["dopSum"] = numTextBox.ConvertToCompPunctuation(dtReport.Rows[i]["dopSum"].ToString());
                }

                DateTime CurDate = _proc.getdatetime();


                Logging.StartFirstLevel(79);
                Logging.Comment("Выгрузка ежедневного отчета по арендадателю");

                Logging.Comment("Арендадатель ID: " + _id + " ; Наименование: " + _nameArend);
                Logging.Comment("ФИО представителя: " + _FIO);
                Logging.Comment("Адрес : " + _addres_1);
                Logging.Comment("Адрес сдаваемого помещения : " + _addres_2);

                Logging.Comment("Параметры отчета");
                Logging.Comment("Дата отчета: " + dtpDate.Value.ToShortDateString());

                

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
              + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();


                HandmadeReport Rep = new HandmadeReport();

                Rep.AddSingleValue("Ежедневный отчет по арендодателю", 3, 2);
                Rep.AddSingleValue(Name_LandLord, 3, 5);
                
                Rep.SetFontSize(3, 2, 3, 2, 14);
                Rep.SetFontBold(3, 2, 3, 2);

                Rep.AddSingleValue("Дата:", 2, 7);
                Rep.AddSingleValue(CurDate.ToString(), 2, 8);
                //Rep.AddSingleValue(CurDate.ToString("dd.MM.yyyy"), 2, 8);

                Rep.AddSingleValue("Отчет №", 6, 1);
                Rep.AddSingleValue("Дата", 6, 2);
                Rep.AddSingleValue("№ договора", 6, 3);
                Rep.AddSingleValue("Арендатор", 6, 4);
                Rep.AddSingleValue("Оплата аренды", 6, 5);                
                Rep.AddSingleValue("Дополнительные оплаты", 6, 8);
                
                Rep.AddSingleValue("Сумма", 7, 5);
                Rep.AddSingleValue("Месяц", 7, 6);
                Rep.AddSingleValue("Пени", 7, 7);
                Rep.AddSingleValue("Тип оплаты", 7, 8);
                Rep.AddSingleValue("Сумма оплаты", 7, 9);
                                
                Rep.SetFontBold(6, 1, 7, 9);

                Rep.AddMultiValue(dtReport, 8, 3);

                Rep.Merge(6, 1, 7, 1);
                Rep.Merge(6, 2, 7, 2);
                Rep.Merge(6, 3, 7, 3);
                Rep.Merge(6, 4, 7, 4);
                Rep.Merge(6, 5, 6, 7);
                Rep.Merge(6, 8, 6, 9);

                Rep.SetBorders(6, 1, dtReport.Rows.Count + 6+1, 9);

                //Итого
                decimal total = 0;

                for (int i = 0; dtReport.Rows.Count > i; i++)
                {
                    decimal d = 0;
                    decimal.TryParse(dtReport.Rows[i]["arendaSum"].ToString(), out d);
                    total += d;
                }

                Rep.AddSingleValue("ИТОГО:", dtReport.Rows.Count + 7+1, 4);
                Rep.AddSingleValue(total.ToString("### ### ### ##0.00"), dtReport.Rows.Count + 7+1, 5);
                Rep.SetBorders(dtReport.Rows.Count + 7+1, 5, dtReport.Rows.Count + 7+1, 5);
                /*
                Rep.SetColumnAutoSize(1, 1, grdPayments.Rows.Count + 10, dtPrint.Columns.Count);
                */

                
                int t = 1;
                Rep.SetColumnWidth(1, t, 1, t, 10); //Отчет №
                t++;
                Rep.SetColumnWidth(1, t, 1, t, 10); //Дата
                t++;
                Rep.SetColumnWidth(1, t, 1, t, 15); //№ договора
                t++;
                Rep.SetColumnWidth(1, t, 1, t, 30); //Арендатор
                t++;
                Rep.SetColumnWidth(1, t, 1, t, 10); //Сумма
                t++;
                Rep.SetColumnWidth(1, t, 1, t, 15); //Месяц
                t++;
                Rep.SetColumnWidth(1, t, 1, t, 10); //Пени
                t++;
                Rep.SetColumnWidth(1, t, 1, t, 15); //Тип оплаты
                t++;
                Rep.SetColumnWidth(1, t, 1, t, 10); //Сумма оплаты
                

                Rep.SetWrapText(6, 1, 7, 9);

                Rep.SetCellAlignmentToCenter(6, 1, 7, 9);

                Rep.AddSingleValue(dtpDate.Value.ToString("dd.MM.yyyy"), 8, 2);

                Rep.SetCellAlignmentToRight(8, 3, dtReport.Rows.Count + 6 + 1, 3);
                Rep.SetCellAlignmentToRight(8, 5, dtReport.Rows.Count + 6 + 1 + 1, 5);
                Rep.SetCellAlignmentToRight(8, 7, dtReport.Rows.Count + 6 + 1, 7);
                Rep.SetCellAlignmentToRight(8, 9, dtReport.Rows.Count + 6 + 1, 9);

                Rep.SetFormat(8, 5, dtReport.Rows.Count + 6 + 1 + 1, 5, "0,00");
                Rep.SetFormat(8, 7, dtReport.Rows.Count + 6 + 1, 7, "0,00");
                Rep.SetFormat(8, 9, dtReport.Rows.Count + 6 + 1, 9, "0,00");

                /*
                Rep.SetCellAlignmentToCenter(7, 1, dtReport.Rows.Count + 6, 1);
                Rep.SetCellAlignmentToRight(7, 3, dtReport.Rows.Count + 6, 3);
                Rep.SetCellAlignmentToCenter(7, 4, dtReport.Rows.Count + 6, 4);
                Rep.SetCellAlignmentToRight(7, 5, dtReport.Rows.Count + 6, 5);
                Rep.SetCellAlignmentToRight(7, 7, dtReport.Rows.Count + 6, 7);
                Rep.SetCellAlignmentToRight(7, 8, dtReport.Rows.Count + 7, 8);

                Rep.SetFormat(7, 4, dtReport.Rows.Count + 6, 4, "ДД.ММ.ГГГГ");
                Rep.SetFormat(7, 5, dtReport.Rows.Count + 6, 5, "0,00");
                Rep.SetFormat(7, 7, dtReport.Rows.Count + 6, 7, "0,00");
                Rep.SetFormat(7, 8, dtReport.Rows.Count + 7, 8, "0,00");
                */

                Rep.Show();
            }
            else
            {
                MessageBox.Show("Данных для отчета нет.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        string _id, _nameArend, _FIO, _addres_1, _addres_2;

        public void setData(string _id, string _nameArend, string _FIO, string _addres_1, string _addres_2)
        {
            this._id = _id;
            this._nameArend = _nameArend;
            this._FIO = _FIO;
            this._addres_1 = _addres_1;
            this._addres_2 = _addres_2;
        }
    }
}
