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
    public partial class frmTenantReport : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        int id_Tenant;
        string Name_Tenant;

        public frmTenantReport(int id, string name)
        {
            InitializeComponent();
            //id_Tenant = 701;
            id_Tenant = id;
            Name_Tenant = name;
        }

        private void frmTenantReport_Load(object sender, EventArgs e)
        {
            DateTime CurDate = _proc.getdate();
            dtpDate1.Value = CurDate.AddDays(-7);
            dtpDate2.Value = CurDate;
            cboContractFill();
        }

        private void cboContractFill()
        {
            DataTable dtContract = new DataTable();

            dtContract = _proc.GetTenantContracts(id_Tenant);

            cboContract.DataSource = dtContract;
            cboContract.DisplayMember = "cName";
            cboContract.ValueMember = "id";
            cboContract.SelectedValue = -1;
        }

        private void dtpDate2_ValueChanged(object sender, EventArgs e)
        {
            dtpDate1.MaxDate = dtpDate2.Value.Date;
        }

        private void dtpDate1_ValueChanged(object sender, EventArgs e)
        {
            dtpDate2.MinDate = dtpDate1.Value.Date;
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            //морозов последний договор
            bool error = false;

            if ((cboContract.SelectedValue == null) || (int.Parse(cboContract.SelectedValue.ToString()) == -1))
            {
                MessageBox.Show("Не выбран договор.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            DataTable dtReport1 = new DataTable();
            dtReport1 = _proc.GetTenantReport1(int.Parse(cboContract.SelectedValue.ToString()),dtpDate1.Value.Date,dtpDate2.Value.Date);
                        
            if (dtReport1 == null) 
            {
                error = true;
            }

            DataTable dtReport2 = new DataTable();
            dtReport2 = _proc.GetTenantReport2(int.Parse(cboContract.SelectedValue.ToString()), dtpDate1.Value.Date, dtpDate2.Value.Date);
            
            if (dtReport2 == null)
            {
                error = true;
            }

            if ((dtReport1.Rows.Count == 0) && (dtReport2.Rows.Count == 0))
            {
                error = true;
            }

            if (error)
            {
                MessageBox.Show("Данных для отчета нет.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int id_fin = 0;

            for (int i = 0; dtReport2.Rows.Count > i; i++)
            {
                if (i == 0)
                {
                    id_fin = int.Parse(dtReport2.Rows[i]["id"].ToString());
                }
                else
                {
                    if (id_fin == int.Parse(dtReport2.Rows[i]["id"].ToString()))
                    {
                        dtReport2.Rows[i]["dopDateFines"] = "";
                        dtReport2.Rows[i]["dopTypePayment"] = "";                        
                        dtReport2.Rows[i]["dopSumma"] = "";

                    }
                    else
                    {
                        id_fin = int.Parse(dtReport2.Rows[i]["id"].ToString());
                    }
                }
            }

            for (int i = 0; dtReport2.Rows.Count > i; i++)
            {
                dtReport2.Rows[i]["dopSumma"] = numTextBox.ConvertToCompPunctuation(dtReport2.Rows[i]["dopSumma"].ToString());
            }

            dtReport2.Columns.Remove("id");

            DataTable dtAgreem = new DataTable();
            dtAgreem = _proc.GetLD(int.Parse(cboContract.SelectedValue.ToString()));

            DateTime CurDate = _proc.getdatetime();


            Logging.StartFirstLevel(79);
            Logging.Comment("Выгрузка отчета по арендатору");

            Logging.Comment("Арендатор ID: " + _id + " ; Наименование: " + _Tenent);
            Logging.Comment("ФИО представителя: " + _Pred);
            Logging.Comment("Местоположение: " + _Locate);
            Logging.Comment("Примечание: " + _remark);

            Logging.Comment("Параметры отчета");
            Logging.Comment("Период с " + dtpDate1.Value.ToShortDateString() + " по " + dtpDate2.Value.ToShortDateString());
            Logging.Comment("Договор ID: " + cboContract.SelectedValue + " ; Наименование: " + cboContract.Text);

            Logging.Comment($"Эл.почта:{dtAgreem.Rows[0]["email"]}");
            Logging.Comment($"Раб.телефон:{dtAgreem.Rows[0]["Work_phone"]}");

            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
            + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();

            HandmadeReport Rep = new HandmadeReport();

            Rep.AddSingleValue("Отчет по арендатору " + Name_Tenant 
                + " за период с " + dtpDate1.Value.ToString("dd.MM.yyyy")
                + " по " + dtpDate2.Value.ToString("dd.MM.yyyy"), 2, 1);            

            Rep.SetFontSize(2, 1, 2, 1, 14);
            Rep.SetFontBold(2, 1, 2, 1);

            Rep.AddSingleValue("Договор №", 4, 1);
            Rep.AddSingleValue("Дата договора:", 5, 1);
            Rep.AddSingleValue("Сумма по договору:", 6, 1);

            DateTime dAgr = DateTime.Parse(dtAgreem.Rows[0]["Date_of_Conclusion"].ToString());

            Rep.AddSingleValue(dtAgreem.Rows[0]["Agreement"].ToString(), 4, 3);
            Rep.AddSingleValue(dAgr.ToString("dd.MM.yyyy"), 5, 3);

            if (decimal.Parse(dtAgreem.Rows[0]["Reklama"].ToString()) > 0)
            {
                Rep.AddSingleValue(dtAgreem.Rows[0]["Reklama"].ToString(), 6, 3);
            }
            else
            {
                Rep.AddSingleValue(dtAgreem.Rows[0]["Total_Sum"].ToString(), 6, 3);
            }
            

            Rep.SetFormat(6, 2, 6, 2, "0,00");

            Rep.AddSingleValue("Дата:", 4, 8);
            Rep.SetFormat(4, 9, 4, 9, "@");
            Rep.AddSingleValue(CurDate.ToString("dd.MM.yyyy HH:mm"), 4, 9);            
            //Rep.AddSingleValue(CurDate.ToString("dd.MM.yyyy"), 2, 8);

            dtReport1.Columns.RemoveAt(2);            
            dtReport1.Columns.RemoveAt(1);
            dtReport1.Columns.RemoveAt(0);
            dtReport1.AcceptChanges();


            Rep.AddSingleValue("№", 8, 1);
            Rep.AddSingleValue("Оплата аренды", 8, 2);
            Rep.AddSingleValue("Дополнительные оплаты", 8, 6);

            Rep.AddSingleValue("Дата оплаты", 9, 2);
            Rep.AddSingleValue("Сумма оплаты", 9, 3);
            Rep.AddSingleValue("Месяц", 9, 5);
            Rep.AddSingleValue("Дата выписки оплаты", 9, 6);
            Rep.AddSingleValue("Тип оплаты", 9, 7);
            Rep.AddSingleValue("Сумма выписанн. оплаты", 9, 8);
            Rep.AddSingleValue("Дата оплаты", 9, 9);            
            Rep.AddSingleValue("Сумма", 9, 10);

            Rep.AddSingleValue("Аренда", 10, 3);
            Rep.AddSingleValue("Пени", 10, 4);

            Rep.SetFontBold(8, 1, 10, 10);

            Rep.AddMultiValue(dtReport1, 11, 2);

            Rep.AddMultiValue(dtReport2, 11, 6);

            int num = 0;

            if (dtReport1.Rows.Count > dtReport2.Rows.Count)
            {
                num = dtReport1.Rows.Count;
            }
            else
            {
                num = dtReport2.Rows.Count;
            }

            for (int i = 0; num > i; i++)
            {
                Rep.AddSingleValue((i+1).ToString(), 11+i, 1);
            }
            
            int t = 1;
            Rep.SetColumnWidth(1, t, 1, t, 10); //№
            t++;
            Rep.SetColumnWidth(1, t, 1, t, 10); //Дата оплаты
            t++;
            Rep.SetColumnWidth(1, t, 1, t, 10); //Аренда
            t++;
            Rep.SetColumnWidth(1, t, 1, t, 10); //Пени
            t++;
            Rep.SetColumnWidth(1, t, 1, t, 10); //Месяц
            t++;
            Rep.SetColumnWidth(1, t, 1, t, 10); //Дата выписки оплаты
            t++;
            Rep.SetColumnWidth(1, t, 1, t, 10); //Тип оплаты
            t++;
            Rep.SetColumnWidth(1, t, 1, t, 10); //Сумма выписанн. оплаты
            t++;
            Rep.SetColumnWidth(1, t, 1, t, 10); //Дата оплаты            
            t++;
            Rep.SetColumnWidth(1, t, 1, t, 10); //Сумма

            Rep.Merge(8, 1, 10, 1);
            Rep.Merge(8, 2, 8, 5);
            Rep.Merge(9, 2, 10, 2);
            Rep.Merge(9, 3, 9, 4);
            Rep.Merge(9, 5, 10, 5);
            Rep.Merge(8, 6, 8, 10);
            Rep.Merge(9, 6, 10, 6);
            Rep.Merge(9, 7, 10, 7);
            Rep.Merge(9, 8, 10, 8);
            Rep.Merge(9, 9, 10, 9);
            Rep.Merge(9, 10, 10, 10);

            Rep.SetWrapText(8, 1, 10, 10);

            Rep.SetCellAlignmentToCenter(8, 1, 10, 10);

            Rep.SetBorders(8, 1, num + 8+2, 10);
                        

            //Итого
            decimal totalAr = 0;
            decimal totalPeni = 0;

            for (int i = 0; dtReport1.Rows.Count > i; i++)
            {
                decimal d = 0;
                decimal.TryParse(dtReport1.Rows[i]["arendaSum"].ToString(), out d);
                totalAr += d;

                d = 0;
                decimal.TryParse(dtReport1.Rows[i]["arendaPeni"].ToString(), out d);                
                totalPeni += d;
            }

            Rep.AddSingleValue("ИТОГО:", num + 8 + 3, 2);
            Rep.AddSingleValue(totalAr.ToString("### ### ### ##0.00"), num + 8 + 3, 3);
            Rep.AddSingleValue(totalPeni.ToString("### ### ### ##0.00"), num + 8 + 3, 4);
            Rep.SetBorders(num + 8 + 3, 3, num + 8 + 3, 4);

            //Итого
            decimal totalDopSumToPay = 0;
            decimal totalDopSumPayed = 0;            

            for (int i = 0; dtReport2.Rows.Count > i; i++)
            {
                decimal dToPay = 0;
                decimal.TryParse(dtReport2.Rows[i]["dopSumma"].ToString(), out dToPay);

                decimal dPayed = 0;
                decimal.TryParse(dtReport2.Rows[i]["dopSum"].ToString(), out dPayed);
                
                totalDopSumToPay += dToPay;
                totalDopSumPayed += dPayed;
            }

            Rep.AddSingleValue("ИТОГО:", num + 8 + 3, 9);
            Rep.AddSingleValue(totalDopSumPayed.ToString("### ### ### ##0.00"), num + 8 + 3, 10);
            Rep.SetBorders(num + 8 + 3, 10, num + 8 + 3, 10);

            Rep.AddSingleValue("Долг:", num + 8 + 3 + 2, 2);
            decimal TotalDebtSum = getDebt();
            Rep.AddSingleValue(TotalDebtSum.ToString("### ### ### ##0.00"), num + 8 + 3 + 2, 3);
            Rep.SetBorders(num + 8 + 3 + 2, 3, num + 8 + 3 + 2, 3);

            Rep.AddSingleValue("Долг:", num + 8 + 3 + 2, 9);
            decimal tt = totalDopSumToPay - totalDopSumPayed;

            Rep.AddSingleValue(tt.ToString("### ### ### ##0.00"), num + 8 + 3 + 2, 10);
            Rep.SetBorders(num + 8 + 3 + 2, 10, num + 8 + 3 + 2, 10);

            Rep.SetCellAlignmentToCenter(11, 1, 11 + num - 1, 1);
            Rep.SetCellAlignmentToCenter(11, 2, 11 + num - 1, 2);
            Rep.SetCellAlignmentToRight(11, 3, 11 + num + 2, 3);
            Rep.SetCellAlignmentToRight(11, 4, 11 + num, 4);
            Rep.SetCellAlignmentToCenter(11, 5, 11 + num - 1, 5);
            Rep.SetCellAlignmentToCenter(11, 6, 11 + num - 1, 6);
            Rep.SetCellAlignmentToCenter(11, 7, 11 + num - 1, 7);
            Rep.SetCellAlignmentToRight(11, 8, 11 + num - 1, 8);
            Rep.SetCellAlignmentToCenter(11, 9, 11 + num - 1, 9);            
            Rep.SetCellAlignmentToRight(11, 10, 11 + num + 2, 10);


            Rep.SetFormat(11, 3, 11 + num, 3, "0,00");
            Rep.SetFormat(11, 4, 11 + num, 4, "0,00");
            Rep.SetFormat(11, 8, 11 + num - 1, 8, "0,00");
            Rep.SetFormat(11, 10, 11 + num, 10, "0,00");

            Rep.Show();
        }

        private decimal getDebt()
        {
            decimal D = 0;           

            DateTime Date = dtpDate2.Value.Date;            
            
            int id_agreement = int.Parse(cboContract.SelectedValue.ToString());

            DataTable dt = new DataTable();
            dt = _proc.GetLD(id_agreement);

            if ((dt == null) || (dt.Rows.Count == 0))
            {                
                return 0;
            }

            DateTime StartDate = DateTime.Parse(dt.Rows[0]["Start_Date"].ToString());
            //DateTime DateEnd = GetDateEnd(dt);
            DateTime DateEnd = GetX.GetDateEnd(dt, id_agreement);

            //кол-во дней в месяце из DateStart
            int countDaysStart = GetX.getCountDays(StartDate);
            //кол-во дней в месяце из DateEnd
            int countDaysEnd = GetX.getCountDays(DateEnd);

            bool Reklama = ((decimal.Parse(dt.Rows[0]["Reklama"].ToString()) > 0) ? true : false);

            decimal Phone = (Reklama) ? 0 : decimal.Parse(dt.Rows[0]["Phone"].ToString());

            decimal TS = (Reklama) ? decimal.Parse(dt.Rows[0]["Reklama"].ToString())
                                   : decimal.Parse(dt.Rows[0]["Total_Sum"].ToString());

            decimal OST = TS * (countDaysStart - StartDate.Day + 1) / countDaysStart;
            OST = Math.Round(OST, 2);

            decimal L = TS / countDaysEnd * DateEnd.Day;
            L = Math.Round(L, 2);

            decimal Cost_of_Meter = decimal.Parse(dt.Rows[0]["Cost_of_Meter"].ToString());

            DataTable dtX = new DataTable();
            dtX = GetX.GetXtable(StartDate, DateEnd, TS, OST, L, Reklama, Cost_of_Meter, Phone, id_agreement);

            if ((dtX == null) || (dtX.Rows.Count == 0))
            {                
                return 0;
            }
            
            decimal Sopl = _proc.GetSopl(id_agreement);
                                    
            for (int w = 0; dtX.Rows.Count > w; w++)
            {
                DateTime XiD2 = DateTime.Parse(dtX.Rows[w]["D2"].ToString());

                if ((Date <= XiD2) || (w == dtX.Rows.Count - 1))
                {
                    //если смотрим последнюю запись в dtX
                    if (w == dtX.Rows.Count - 1)
                    {

                        D = ReturnD(D, Sopl,
                                decimal.Parse(dtX.Rows[w]["SUMM"].ToString()));
                        break;
                    }
                    //если смотрим не последнюю запись в dtX
                    else
                    {
                        if (DateTime.Parse(dtX.Rows[w]["D2"].ToString()) != DateTime.Parse(dtX.Rows[w + 1]["D2"].ToString()))
                        {
                            if (((Date.Month == XiD2.Month) && (Date.Year == XiD2.Year)) 
                                || 
                                (w == 0))
                            {
                                D = ReturnD(D, Sopl,
                                    decimal.Parse(dtX.Rows[w]["SUMM"].ToString()));
                                break;
                            }
                            else
                            {
                                D = ReturnD(D, Sopl,
                                    decimal.Parse(dtX.Rows[w-1]["SUMM"].ToString()));
                                break;
                            }
                        }
                    }
                }
            }
            
            return D;
        }

        private decimal ReturnD(decimal D, decimal Sopl, decimal XiSumm)
        {
            if (Sopl < XiSumm)
            {
                D = XiSumm - Sopl;                
            }

            return D;
        }
        
        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string _id, _Tenent, _Pred, _Locate, _remark;

        public void setData(string _id, string _Tenent, string _Pred, string _Locate, string _remark)
        {
            this._id = _id;
            this._Tenent = _Tenent;
            this._Pred = _Pred;
            this._Locate = _Locate;
            this._remark = _remark;
        }

    }
}
