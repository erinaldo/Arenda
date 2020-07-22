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
  public partial class frmPrintPlanOtchet : Form
  {
    readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(),
      ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(),
      ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
    DataTable dtCopy;
    DataTable wrap;

    public frmPrintPlanOtchet()
    {
      InitializeComponent();
    }

    private void FillCbo()
    {
      DataTable dtMonth = new DataTable();
      dtMonth.Columns.Add("id", typeof(string));
      dtMonth.Columns.Add("month", typeof(string));
      dtMonth.Rows.Add("1", "Январь");
      dtMonth.Rows.Add("2", "Февраль");
      dtMonth.Rows.Add("3", "Март");
      dtMonth.Rows.Add("4", "Апрель");
      dtMonth.Rows.Add("5", "Май");
      dtMonth.Rows.Add("6", "Июнь");
      dtMonth.Rows.Add("7", "Июль");
      dtMonth.Rows.Add("8", "Август");
      dtMonth.Rows.Add("9", "Сентябрь");
      dtMonth.Rows.Add("10", "Октябрь");
      dtMonth.Rows.Add("11", "Ноябрь");
      dtMonth.Rows.Add("12", "Декабрь");
      dtMonth.AcceptChanges();

      cbMonth.DataSource = dtMonth;
      cbMonth.DisplayMember = "month";
      cbMonth.ValueMember = "id";
      cbMonth.SelectedValue = DateTime.Now.Month;

      DataTable dtYear = new DataTable();
      dtYear = _proc.GetYears();
      
      cbYear.DataSource = dtYear;
      cbYear.DisplayMember = "year";
      cbYear.ValueMember = "id";

      cbYear.SelectedValue = int.Parse(dtYear.Select("year = "
        + DateTime.Now.Year.ToString())[0]["id"].ToString());

      if (cbYear.SelectedValue == null)
      {
        cbYear.SelectedIndex = 0;
      }
    }

    private void frmPrintPlanOtchet_Load(object sender, EventArgs e)
    {
      FillCbo();
    }

    private void btnQuit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnPrint_Click(object sender, EventArgs e)
    {
      if (cbYear.SelectedValue == null)
      {
        MessageBox.Show("Не выбран год");
        return;
      }

      if (cbMonth.SelectedValue == null)
      {
        MessageBox.Show("Не выбран месяц");
        return;
      }

      prbExcel.Visible = true;
      prbExcel2.Visible = true;
      lblPercent.Visible = true;

      Logging.StartFirstLevel(79);
      Logging.Comment("Выгрузка план - отчета");

      Logging.Comment("Месяц отчета: " + cbMonth.Text);
      Logging.Comment("Год отчета: " + cbYear.Text);

      foreach (Control cnr in groupBox1.Controls)
      {
        if (cnr is RadioButton)
        {
          if ((cnr as RadioButton).Checked)
          {
            Logging.Comment("Вид отчета: " + (cnr as RadioButton).Text);
            break;
          }
        }
      }

      Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
        + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
      Logging.StopFirstLevel();

      this.lblPercent.Text = ("Обработка данных: 0%");
      this.Enabled = false;
      bgwToExcel.RunWorkerAsync();
    }

    private void bgwToExcel_DoWork(object sender, DoWorkEventArgs e)
    {
      BackgroundWorker worker = sender as BackgroundWorker;            
      int mon = 1;
      int yea = DateTime.Now.Year;
      int type = 0;

      cbMonth.Invoke((MethodInvoker)delegate
      {
        mon = int.Parse(cbMonth.SelectedValue.ToString());
      });

      cbYear.Invoke((MethodInvoker)delegate
      {
        yea = int.Parse(cbYear.Text);
      });
            
      rbCommon.Invoke((MethodInvoker)delegate
      {
        if (rbCommon.Checked)
          type = 1;
      });

      rbFact.Invoke((MethodInvoker)delegate
      {
        if (rbFact.Checked)
          type = 2;
      });

      rbPlan.Invoke((MethodInvoker)delegate
      {
        if (rbPlan.Checked)
          type = 3;
      });

      DataTable dtPrint = new DataTable();

      if (type == 1)
      {
        dtPrint = _proc.GetPrintReportPlanCommon(mon, yea);
      }

      if (type == 2)
      {
        dtPrint = _proc.GetPrintReportPlanFact(mon, yea);
      }

      if (type == 3)
      {
        dtPrint = _proc.GetPrintReportPlan(mon, yea);
      }

      if ((dtPrint != null) && (dtPrint.Rows.Count > 0))
      {
        Report(dtPrint, worker, type);
      }
      else
      {
        MessageBox.Show("Нет данных для выгрузки");
      }
    }

    private void bgwToExcel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      prbExcel.Visible = false;
      prbExcel2.Visible = false;
      lblPercent.Visible = false;
      this.Enabled = true;
    }

    private void Report(DataTable dt, BackgroundWorker worker, int type)
    {
      //type = 1 Общий отчет. Видны все колонки
      //type = 2 Факт. Скрыты колонки Плановая начало и Плановая конец
      //type = 3 План. Скрыты все колонки после Плановая конец

      //Ищем и объединяем строки по одному договору
      wrap = new DataTable();
      wrap.Columns.Add("start", typeof(Int32)); //1-я строка в блоке нал/безнал по арендодателю
      wrap.Columns.Add("end", typeof(Int32)); //последняя строка в блоке нал/безнал по арендодателю

      if (type == 2)
      {
        dt.Columns.Remove("Plan_Start");
        dt.Columns.Remove("Plan_End");
        dt.AcceptChanges();
      }
      worker.ReportProgress(0);
      string month = "";
      cbMonth.Invoke((MethodInvoker)delegate
      {
        month = cbMonth.Text;
      });

      string year = "";
      cbYear.Invoke((MethodInvoker)delegate
      {
        year = cbYear.Text;
      });

      int dtRowStart = 4;
      int Number = 0;
      int rowsPassed = 0; //количество выгруженных строк

      HandmadeReport Rep = new HandmadeReport();
      //Rep.Show();
      if (type == 1)
        Rep.AddSingleValue("ОТЧЕТ ОБЩИЙ " + month + " " + year + " г.", 2, 2);
      if (type == 2)
        Rep.AddSingleValue("ОТЧЕТ-ФАКТ " + month + " " + year + " г.", 2, 2);
      if(type == 3)
        Rep.AddSingleValue("ОТЧЕТ-ПЛАН " + month + " " + year + " г.", 2, 2);            
      Rep.SetFontBold(1, 1, 2, 2);
      Rep.SetFontSize(1, 1, 2, 2, 12);

      int colStart = 1;
      int col = colStart;

      Rep.AddSingleValue("№", dtRowStart, col);
      col++;
      Rep.AddSingleValue("Дата", dtRowStart, col);
      col++;
      Rep.AddSingleValue("№ дог", dtRowStart, col);
      col++;
      Rep.AddSingleValue("Арендатор", dtRowStart, col);
      col++;
      Rep.AddSingleValue("Адрес", dtRowStart, col);
      col++;
      Rep.AddSingleValue("ст", dtRowStart, col);
      col++;
      Rep.AddSingleValue("эт", dtRowStart, col);
      col++;
      Rep.AddSingleValue("№ сек", dtRowStart, col);
      col++;
      Rep.AddSingleValue("S,м2", dtRowStart, col);
      col++;
      Rep.AddSingleValue("тел.", dtRowStart, col);
      col++;
      Rep.AddSingleValue("расц.", dtRowStart, col);
      col++;
      Rep.AddSingleValue("НДС", dtRowStart, col);
      col++;
      Rep.AddSingleValue("Общ.ст-ть по дог.", dtRowStart, col);
      if (type != 2)
      {
        col++;
        Rep.AddSingleValue("Плановая начало", dtRowStart, col);
        col++;
        Rep.AddSingleValue("Плановая конец", dtRowStart, col);                
      }
      if (type != 3)
      {
        col++;
        Rep.AddSingleValue("Дата", dtRowStart, col);
        col++;
        Rep.AddSingleValue("телефон", dtRowStart, col);
        col++;
        Rep.AddSingleValue("Общ.ст-ть (руб.)", dtRowStart, col);
        col++;
        Rep.AddSingleValue("пени", dtRowStart, col);
        col++;
        Rep.AddSingleValue("тип оплаты", dtRowStart, col);
        col++;
        Rep.AddSingleValue("сумма оплаты", dtRowStart, col);
      }

      Rep.SetFontBold(dtRowStart, 1, dtRowStart, dt.Columns.Count + 1);

      int columnsCount = col;

      string LandLord_Name = string.Empty;
      int countLandLord = 0;
      string payment = string.Empty;

      for (int i = 0; dt.Rows.Count > i; i++)
      {
        if (LandLord_Name != dt.Rows[i]["LandLord_Name"].ToString())
        {
          LandLord_Name = dt.Rows[i]["LandLord_Name"].ToString();
          countLandLord ++; //наименование арендодателя
          countLandLord++; //подитог
          countLandLord++; //общий итог
          payment = dt.Rows[i]["Payment_Type"].ToString();
        }

        if (payment != dt.Rows[i]["Payment_Type"].ToString())
        {
          countLandLord++;//подитог 2 если есть как договора по налу, так и по безналу
        }
      }
      
      //для каждого арендодателя создается 4 дополнительные строки: 
      // с заголовком + 2 строки итого (нал и безнал) + общая итоговая по арендодателю

      int totalRows = dtRowStart + dt.Rows.Count + countLandLord;

      int c = 1;
      Rep.SetColumnWidth(1, c, 1, c, 5); //№
      c++; //2
      Rep.SetColumnWidth(1, c, 1, c, 9); //Дата
      c++; //3
      Rep.SetColumnWidth(1, c, 1, c, 5); //№ дог
      c++; //4
      Rep.SetColumnWidth(1, c, 1, c, 17); //Арендатор
      c++; //5
      Rep.SetColumnWidth(1, c, 1, c, 22); //Адрес
      c++; //6
      Rep.SetColumnWidth(1, c, 1, c, 8); //ст
      c++; //7
      Rep.SetColumnWidth(1, c, 1, c, 4); //эт
      c++; //8
      Rep.SetColumnWidth(1, c, 1, c, 7); //№ сек
      c++; //9
      Rep.SetColumnWidth(1, c, 1, c, 8); //S, м2
      c++; //10
      Rep.SetColumnWidth(1, c, 1, c, 9); //тел
      c++; //11
      Rep.SetColumnWidth(1, c, 1, c, 9); //расц.
      c++; //12
      Rep.SetColumnWidth(1, c, 1, c, 9); //ндс
      c++; //13
      Rep.SetColumnWidth(1, c, 1, c, 11); //общ ст-ть
      if (type != 2)
      {
        c++; //14
        Rep.SetColumnWidth(1, c, 1, c, 11); //план начало
        c++; //15
        Rep.SetColumnWidth(1, c, 1, c, 11); //план конец
      }
      if (type != 3)
      {
        c++; //16
        Rep.SetColumnWidth(1, c, 1, c, 9); //дата
        c++; //17
        Rep.SetColumnWidth(1, c, 1, c, 9); //телефон
        c++; //18
        Rep.SetColumnWidth(1, c, 1, c, 11); //общ ст-ть
        c++; //19
        Rep.SetColumnWidth(1, c, 1, c, 11); //пени
        c++; //20
        Rep.SetColumnWidth(1, c, 1, c, 12); //тип оплаты
        c++; //21
        Rep.SetColumnWidth(1, c, 1, c, 11); //сумма оплаты
      }

      Rep.SetWrapText(dtRowStart, colStart, dtRowStart, columnsCount);

      Rep.SetPageOrientationToLandscape();

      Rep.SetFontName(1, 1, totalRows, columnsCount, "TimesNewRoman");
      Rep.SetFontSize(dtRowStart, 1, totalRows, columnsCount, 8);

      if (type == 1)
      {
        Rep.SetFormat(dtRowStart + 1, 9, totalRows, 15, "## ### ### ##0,00");
        Rep.SetFormat(dtRowStart + 1, 16, totalRows, 21, "@");
      }

      if (type == 2)
      {
        Rep.SetFormat(dtRowStart + 1, 9, totalRows, 13, "## ### ### ##0,00");
        Rep.SetFormat(dtRowStart + 1, 14, totalRows, 19, "@");
      }

      if (type == 3)
      {
        Rep.SetFormat(dtRowStart + 1, 9, totalRows, 15, "## ### ### ##0,00");
      }

      if (type == 1)
      {
        Rep.SetCellAlignmentToCenter(dtRowStart, 1, dtRowStart, columnsCount);
        Rep.SetCellAlignmentToCenter(dtRowStart + 1, 2, totalRows, 2);//дата
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 3, totalRows, 3);//№ дог
        Rep.SetCellAlignmentToCenter(dtRowStart + 1, 8, totalRows, 8);//№ сек
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 9, totalRows, 15);// с "S,м2" по "план конец"
        Rep.SetCellAlignmentToCenter(dtRowStart + 1, 16, totalRows, 16);//дата
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 17, totalRows, 19);// с "телефон" до пени                
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 21, totalRows, 21);// сумма оплаты                
      }

      if (type == 2)
      {
        Rep.SetCellAlignmentToCenter(dtRowStart, 1, dtRowStart, columnsCount);
        Rep.SetCellAlignmentToCenter(dtRowStart + 1, 2, totalRows, 2);//дата
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 3, totalRows, 3);//№ дог
        Rep.SetCellAlignmentToCenter(dtRowStart + 1, 8, totalRows, 8);//№ сек
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 9, totalRows, 13);// с "S,м2" по "общ стоимость"
        Rep.SetCellAlignmentToCenter(dtRowStart + 1, 14, totalRows, 14);//дата
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 15, totalRows, 17);// с "телефон" до пени
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 19, totalRows, 19);// сумма оплаты
      }

      if (type == 3)
      {
        Rep.SetCellAlignmentToCenter(dtRowStart, 1, dtRowStart, columnsCount);
        Rep.SetCellAlignmentToCenter(dtRowStart + 1, 2, totalRows, 2);//дата
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 3, totalRows, 3);//№ дог
        Rep.SetCellAlignmentToCenter(dtRowStart + 1, 8, totalRows, 8);//№ сек
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 9, totalRows, 13);// с "S,м2" по "план конец"
      }

      string LandLord = string.Empty;
      bool Payment_Type = false;
      int lastrowinexcel = dtRowStart + 1;

      decimal Total_Area = 0;
      decimal Phone = 0;
      decimal Total_Sum = 0;
      decimal Plan_Start = 0;
      decimal Plan_End = 0;
      decimal PaymentTelSum = 0;
      decimal PaymentTotalSum = 0;
      decimal Penni = 0;
      decimal DopPayment = 0;

      decimal total_Total_Area = 0;
      decimal total_Phone = 0;
      decimal total_Total_Sum = 0;
      decimal total_Plan_Start = 0;
      decimal total_Plan_End = 0;
      decimal total_PaymentTelSum = 0;
      decimal total_PaymentTotalSum = 0;
      decimal total_Penni = 0;
      decimal total_DopPayment = 0;
      
      if (type != 3)
      {
        for (int r = 0; dt.Rows.Count > r; r++)
        {
          //исходные данные из sql получаем с разделителем sql, т.е. с точкой. сделано для того, чтобы было все "Красиво" как говорит Света и не было нулей, 
          //иначе если в sql получать числовые значения в колонку, то в Excel будут подставляться нули, там где должна быть пустая ячейка
          //чтобы все было "красиво" сначала получаем текстовое значение в колонке а затем заменяем на разделитель целой и дробной части, который установлен на компе

          dt.Rows[r]["Total_Area"] = numTextBox.ConvertToCompPunctuation(dt.Rows[r]["Total_Area"].ToString());
          dt.Rows[r]["Phone"] = numTextBox.ConvertToCompPunctuation(dt.Rows[r]["Phone"].ToString());
          dt.Rows[r]["Cost_of_Meter"] = numTextBox.ConvertToCompPunctuation(dt.Rows[r]["Cost_of_Meter"].ToString());
          dt.Rows[r]["nds"] = numTextBox.ConvertToCompPunctuation(dt.Rows[r]["nds"].ToString());
          dt.Rows[r]["Total_Sum"] = numTextBox.ConvertToCompPunctuation(dt.Rows[r]["Total_Sum"].ToString());

          dt.Rows[r]["PaymentTelSum"] = numTextBox.ConvertToCompPunctuation(dt.Rows[r]["PaymentTelSum"].ToString());
          dt.Rows[r]["PaymentTotalSum"] = numTextBox.ConvertToCompPunctuation(dt.Rows[r]["PaymentTotalSum"].ToString());
          dt.Rows[r]["Penni"] = numTextBox.ConvertToCompPunctuation(dt.Rows[r]["Penni"].ToString());
          dt.Rows[r]["DopPayment"] = numTextBox.ConvertToCompPunctuation(dt.Rows[r]["DopPayment"].ToString());
        }
      }

      for (int i = 0; dt.Rows.Count > i; i++)
      {
        if (LandLord != dt.Rows[i]["LandLord_Name"].ToString())
        {
          if (i != 0)
          {
            Rep.AddSingleValue("ИТОГО по арендодателю:", lastrowinexcel, 1);
            if (type == 1)
            {
              Rep.AddSingleValue(total_Total_Area.ToString(), lastrowinexcel, 9);
              Rep.AddSingleValue(total_Phone.ToString(), lastrowinexcel, 10);
              Rep.AddSingleValue(total_Total_Sum.ToString(), lastrowinexcel, 13);
              Rep.AddSingleValue(total_Plan_Start.ToString(), lastrowinexcel, 14);
              Rep.AddSingleValue(total_Plan_End.ToString(), lastrowinexcel, 15);
              Rep.AddSingleValue(total_PaymentTelSum.ToString(), lastrowinexcel, 17);
              Rep.AddSingleValue(total_PaymentTotalSum.ToString(), lastrowinexcel, 18);
              Rep.AddSingleValue(total_Penni.ToString(), lastrowinexcel, 19);
              Rep.AddSingleValue(total_DopPayment.ToString(), lastrowinexcel, 21);
              Rep.SetFontBold(lastrowinexcel, colStart, lastrowinexcel, columnsCount);
            }

            if (type == 2)
            {
              Rep.AddSingleValue(total_Total_Area.ToString(), lastrowinexcel, 9);
              Rep.AddSingleValue(total_Phone.ToString(), lastrowinexcel, 10);
              Rep.AddSingleValue(total_Total_Sum.ToString(), lastrowinexcel, 13);
              //Rep.AddSingleValue(total_Plan_Start.ToString(), lastrowinexcel, 14);
              //Rep.AddSingleValue(total_Plan_End.ToString(), lastrowinexcel, 15);
              Rep.AddSingleValue(total_PaymentTelSum.ToString(), lastrowinexcel, 15);
              Rep.AddSingleValue(total_PaymentTotalSum.ToString(), lastrowinexcel, 16);
              Rep.AddSingleValue(total_Penni.ToString(), lastrowinexcel, 17);
              Rep.AddSingleValue(total_DopPayment.ToString(), lastrowinexcel, 19);
              Rep.SetFontBold(lastrowinexcel, colStart, lastrowinexcel, columnsCount);
            }

            if (type == 3)
            {
              Rep.AddSingleValue(total_Total_Area.ToString(), lastrowinexcel, 9);
              Rep.AddSingleValue(total_Phone.ToString(), lastrowinexcel, 10);
              Rep.AddSingleValue(total_Total_Sum.ToString(), lastrowinexcel, 13);
              Rep.AddSingleValue(total_Plan_Start.ToString(), lastrowinexcel, 14);
              Rep.AddSingleValue(total_Plan_End.ToString(), lastrowinexcel, 15);
              //Rep.AddSingleValue(total_PaymentTelSum.ToString(), lastrowinexcel, 15);
              //Rep.AddSingleValue(total_PaymentTotalSum.ToString(), lastrowinexcel, 16);
              //Rep.AddSingleValue(total_Penni.ToString(), lastrowinexcel, 17);
              //Rep.AddSingleValue(total_DopPayment.ToString(), lastrowinexcel, 18);
              Rep.SetFontBold(lastrowinexcel, colStart, lastrowinexcel, columnsCount);
            }

            total_Total_Area =
              total_Phone =
              total_Total_Sum =
              total_Plan_Start =
              total_Plan_End =
              total_PaymentTelSum =
              total_PaymentTotalSum =
              total_Penni =
              total_DopPayment = 0;
            lastrowinexcel++;
          }

          Rep.AddSingleValue(dt.Rows[i]["LandLord_Name"].ToString(), lastrowinexcel, colStart);
          Rep.SetFontBold(lastrowinexcel, colStart + 1, lastrowinexcel, colStart);
          lastrowinexcel++;

          dtCopy = new DataTable();

          dtCopy = dt.Select("LandLord_Name like '%" + dt.Rows[i]["LandLord_Name"].ToString() + "%' AND Payment_Type = " + dt.Rows[i]["Payment_Type"].ToString()).CopyToDataTable();
          dtCopy.Columns.Remove("LandLord_Name");
          dtCopy.Columns.Remove("Payment_Type");
          dtCopy.AcceptChanges();

          if (type != 3)
          {
            string num = "";
            int wrapstart = 0;
            bool fixedfirstrow = false;

            for (int q = 0; dtCopy.Rows.Count > q; q++)
            {
              if (q == 0)
              {
                num = dtCopy.Rows[q]["id"].ToString();
                Number++;
                dtCopy.Rows[q]["num"] = Number;
              }
              else
              {
                if (dtCopy.Rows[q]["id"].ToString() == num)
                {
                  if (!fixedfirstrow)
                  {
                    wrapstart = q - 1;
                    fixedfirstrow = true;
                  }
                  dtCopy.Rows[q]["num"] = Number;
                }

                if ((dtCopy.Rows[q]["id"].ToString() != num)
                  || (q == dtCopy.Rows.Count - 1))
                {
                  num = dtCopy.Rows[q]["id"].ToString();
                  Number++;
                  dtCopy.Rows[q]["num"] = Number;
                  if (fixedfirstrow)
                  {
                    if (q == dtCopy.Rows.Count - 1)
                    {
                      wrap.Rows.Add(new Object[] { wrapstart + lastrowinexcel, q + lastrowinexcel });
                    }
                    else
                    {
                      wrap.Rows.Add(new Object[] { wrapstart + lastrowinexcel, q - 1 + lastrowinexcel });
                    }
                    wrap.AcceptChanges();
                    fixedfirstrow = false;
                  }
                }
              }
            }
          }

          dtCopy.Columns.Remove("id");
          dtCopy.AcceptChanges();

          string Ten = "";

          for (int m = 0; dtCopy.Rows.Count > m; m++)
          {
            if (Ten != dtCopy.Rows[m]["Tenant_Name"].ToString())
            {
              Ten = dtCopy.Rows[m]["Tenant_Name"].ToString();
              Rep.AddSingleValue(dtCopy.Rows[m]["num"].ToString(), lastrowinexcel, 1);
              Rep.AddSingleValue(dtCopy.Rows[m]["agr_date"].ToString(), lastrowinexcel, 2);
              Rep.AddSingleValue(dtCopy.Rows[m]["agr_num"].ToString(), lastrowinexcel, 3);
              Rep.AddSingleValue(dtCopy.Rows[m]["Tenant_Name"].ToString(), lastrowinexcel, 4);
              Rep.AddSingleValue(dtCopy.Rows[m]["LandLord_Adress"].ToString(), lastrowinexcel, 5);
              Rep.AddSingleValue(dtCopy.Rows[m]["Building"].ToString(), lastrowinexcel, 6);
              Rep.AddSingleValue(dtCopy.Rows[m]["Floor"].ToString(), lastrowinexcel, 7);
              Rep.AddSingleValue(dtCopy.Rows[m]["Section"].ToString(), lastrowinexcel, 8);
              Rep.AddSingleValue(dtCopy.Rows[m]["Total_Area"].ToString(), lastrowinexcel, 9);
              Rep.AddSingleValue(dtCopy.Rows[m]["Phone"].ToString(), lastrowinexcel, 10);
              Rep.AddSingleValue(dtCopy.Rows[m]["Cost_of_Meter"].ToString(), lastrowinexcel, 11);
              Rep.AddSingleValue(dtCopy.Rows[m]["nds"].ToString(), lastrowinexcel, 12);
              Rep.AddSingleValue(dtCopy.Rows[m]["Total_Sum"].ToString(), lastrowinexcel, 13);
            }

            if ((type == 1) || (type == 3))
            {
              Rep.AddSingleValue(dtCopy.Rows[m]["Plan_Start"].ToString(), lastrowinexcel, 14);
              Rep.AddSingleValue(dtCopy.Rows[m]["Plan_End"].ToString(), lastrowinexcel, 15);
              if (type == 1)
              {
                Rep.AddSingleValue(dtCopy.Rows[m]["paymentDate"].ToString(), lastrowinexcel, 16);
                Rep.AddSingleValue(dtCopy.Rows[m]["PaymentTelSum"].ToString(), lastrowinexcel, 17);
                Rep.AddSingleValue(dtCopy.Rows[m]["PaymentTotalSum"].ToString(), lastrowinexcel, 18);
                Rep.AddSingleValue(dtCopy.Rows[m]["Penni"].ToString(), lastrowinexcel, 19);
                Rep.AddSingleValue(dtCopy.Rows[m]["DopPaymentType"].ToString(), lastrowinexcel, 20);
                Rep.AddSingleValue(dtCopy.Rows[m]["DopPayment"].ToString(), lastrowinexcel, 21);
              }
            }

            if (type == 2)
            {
              Rep.AddSingleValue(dtCopy.Rows[m]["paymentDate"].ToString(), lastrowinexcel, 14);
              Rep.AddSingleValue(dtCopy.Rows[m]["PaymentTelSum"].ToString(), lastrowinexcel, 15);
              Rep.AddSingleValue(dtCopy.Rows[m]["PaymentTotalSum"].ToString(), lastrowinexcel, 16);
              Rep.AddSingleValue(dtCopy.Rows[m]["Penni"].ToString(), lastrowinexcel, 17);
              Rep.AddSingleValue(dtCopy.Rows[m]["DopPaymentType"].ToString(), lastrowinexcel, 18);
              Rep.AddSingleValue(dtCopy.Rows[m]["DopPayment"].ToString(), lastrowinexcel, 19);
            }

            lastrowinexcel++;
          }

          rowsPassed += dtCopy.Rows.Count;
          int perccc = Convert.ToInt32(rowsPassed * 100 / dt.Rows.Count);
          if (perccc > 100)
            perccc = 100;
          worker.ReportProgress(perccc);

          LandLord = dt.Rows[i]["LandLord_Name"].ToString();
          Payment_Type = bool.Parse(dt.Rows[i]["Payment_Type"].ToString());

          string Tenant = "";
          for (int y = 0; dtCopy.Rows.Count > y; y++)
          {
            if (Tenant != dtCopy.Rows[y]["Tenant_Name"].ToString())
            {
              Tenant = dtCopy.Rows[y]["Tenant_Name"].ToString();
              Total_Area += decimal.Parse(dtCopy.Rows[y]["Total_Area"].ToString());
              Phone += decimal.Parse(dtCopy.Rows[y]["Phone"].ToString());
              Total_Sum += decimal.Parse(dtCopy.Rows[y]["Total_Sum"].ToString());
              if (type != 2)
              {
                try
                {
                  decimal tmpStart = 0;
                  decimal.TryParse(dtCopy.Rows[y]["Plan_Start"].ToString(), out tmpStart);
                  //Plan_Start += decimal.Parse(dtCopy.Rows[y]["Plan_Start"].ToString());
                  Plan_Start += tmpStart;
                  decimal tmpEnd = 0;
                  decimal.TryParse(dtCopy.Rows[y]["Plan_End"].ToString(), out tmpEnd);
                  //Plan_End += decimal.Parse(dtCopy.Rows[y]["Plan_End"].ToString());
                  Plan_End += tmpEnd;
                }
                catch (Exception ex) { }
              }
            }

            if (type != 3)
            {
              PaymentTelSum += (dtCopy.Rows[y]["PaymentTelSum"].ToString() != "")
                ? decimal.Parse(
                numTextBox.ConvertToCompPunctuation(dtCopy.Rows[y]["PaymentTelSum"].ToString())
                )
                : 0;
              PaymentTotalSum += (dtCopy.Rows[y]["PaymentTotalSum"].ToString() != "")
                ? decimal.Parse(
                numTextBox.ConvertToCompPunctuation(dtCopy.Rows[y]["PaymentTotalSum"].ToString())
                )
                : 0;
              Penni += (dtCopy.Rows[y]["Penni"].ToString() != "")
                ? decimal.Parse(
                numTextBox.ConvertToCompPunctuation(dtCopy.Rows[y]["Penni"].ToString())
                )
                : 0;
              DopPayment += (dtCopy.Rows[y]["DopPayment"].ToString() != "")
                ? decimal.Parse(
                numTextBox.ConvertToCompPunctuation(dtCopy.Rows[y]["DopPayment"].ToString())
                )
                : 0;
            }
          }

          if (bool.Parse(dt.Rows[i]["Payment_Type"].ToString()) == true)
          {
            Rep.AddSingleValue("Итого по арендодателю (нал)", lastrowinexcel, 1);
          }
          else
          {
            Rep.AddSingleValue("Итого по арендодателю (безнал)", lastrowinexcel, 1);
          }

          if (type == 1)
          {
            Rep.AddSingleValue(Total_Area.ToString(), lastrowinexcel, 9);
            Rep.AddSingleValue(Phone.ToString(), lastrowinexcel, 10);
            Rep.AddSingleValue(Total_Sum.ToString(), lastrowinexcel, 13);
            Rep.AddSingleValue(Plan_Start.ToString(), lastrowinexcel, 14);
            Rep.AddSingleValue(Plan_End.ToString(), lastrowinexcel, 15);
            Rep.AddSingleValue(PaymentTelSum.ToString(), lastrowinexcel, 17);
            Rep.AddSingleValue(PaymentTotalSum.ToString(), lastrowinexcel, 18);
            Rep.AddSingleValue(Penni.ToString(), lastrowinexcel, 19);
            Rep.AddSingleValue(DopPayment.ToString(), lastrowinexcel, 21);
            Rep.SetFontBold(lastrowinexcel, colStart, lastrowinexcel, columnsCount);
          }

          if (type == 2)
          {
            Rep.AddSingleValue(Total_Area.ToString(), lastrowinexcel, 9);
            Rep.AddSingleValue(Phone.ToString(), lastrowinexcel, 10);
            Rep.AddSingleValue(Total_Sum.ToString(), lastrowinexcel, 13);
            //Rep.AddSingleValue(Plan_Start.ToString(), lastrowinexcel, 14);
            //Rep.AddSingleValue(Plan_End.ToString(), lastrowinexcel, 15);
            Rep.AddSingleValue(PaymentTelSum.ToString(), lastrowinexcel, 15);
            Rep.AddSingleValue(PaymentTotalSum.ToString(), lastrowinexcel, 16);
            Rep.AddSingleValue(Penni.ToString(), lastrowinexcel, 17);
            Rep.AddSingleValue(DopPayment.ToString(), lastrowinexcel, 19);
            Rep.SetFontBold(lastrowinexcel, colStart, lastrowinexcel, columnsCount);
          }
          
          if (type == 3)
          {
            Rep.AddSingleValue(Total_Area.ToString(), lastrowinexcel, 9);
            Rep.AddSingleValue(Phone.ToString(), lastrowinexcel, 10);
            Rep.AddSingleValue(Total_Sum.ToString(), lastrowinexcel, 13);
            Rep.AddSingleValue(Plan_Start.ToString(), lastrowinexcel, 14);
            Rep.AddSingleValue(Plan_End.ToString(), lastrowinexcel, 15);
            //Rep.AddSingleValue(PaymentTelSum.ToString(), lastrowinexcel, 15);
            //Rep.AddSingleValue(PaymentTotalSum.ToString(), lastrowinexcel, 16);
            //Rep.AddSingleValue(Penni.ToString(), lastrowinexcel, 17);
            //Rep.AddSingleValue(DopPayment.ToString(), lastrowinexcel, 18);
            Rep.SetFontBold(lastrowinexcel, colStart, lastrowinexcel, columnsCount);
          }

          Rep.SetFontBold(lastrowinexcel, colStart, lastrowinexcel, columnsCount);

          total_Total_Area += Total_Area;
          total_Phone += Phone;
          total_Total_Sum += Total_Sum;
          total_Plan_Start += Plan_Start;
          total_Plan_End += Plan_End;
          total_PaymentTelSum += PaymentTelSum;
          total_PaymentTotalSum += PaymentTotalSum;
          total_Penni += Penni;
          total_DopPayment += DopPayment;

          lastrowinexcel++;
          Total_Area =
            Phone =
            Total_Sum =
            Plan_Start =
            Plan_End =
            PaymentTelSum =
            PaymentTotalSum =
            Penni =
            DopPayment = 0;
        }
        else
        {
          if (Payment_Type != bool.Parse(dt.Rows[i]["Payment_Type"].ToString()))
          {
            dtCopy = dt.Select("LandLord_Name like '%" + LandLord
              + "%' AND Payment_Type = "
              + dt.Rows[i]["Payment_Type"].ToString()).CopyToDataTable();
            dtCopy.Columns.Remove("LandLord_Name");
            dtCopy.Columns.Remove("Payment_Type");
            dtCopy.AcceptChanges();

            if (type != 3)
            {
              string num = "";
              int wrapstart = 0;
              bool fixedfirstrow = false;

              for (int q = 0; dtCopy.Rows.Count > q; q++)
              {
                if (q == 0)
                {
                  num = dtCopy.Rows[q]["id"].ToString();
                  Number++;
                  dtCopy.Rows[q]["num"] = Number;
                }
                else
                {
                  if (dtCopy.Rows[q]["id"].ToString() == num)
                  {
                    if (!fixedfirstrow)
                    {
                      wrapstart = q - 1;
                      fixedfirstrow = true;
                    }
                    dtCopy.Rows[q]["num"] = Number;
                  }

                  if ((dtCopy.Rows[q]["id"].ToString() != num)
                    || (q == dtCopy.Rows.Count - 1))
                  {
                    num = dtCopy.Rows[q]["id"].ToString();
                    Number++;
                    dtCopy.Rows[q]["num"] = Number;
                    if (fixedfirstrow)
                    {
                      if (q == dtCopy.Rows.Count - 1)
                      {
                        wrap.Rows.Add(new Object[] { wrapstart + lastrowinexcel, q + lastrowinexcel });
                      }
                      else
                      {
                        wrap.Rows.Add(new Object[] { wrapstart + lastrowinexcel, q - 1 + lastrowinexcel });
                      }
                      wrap.AcceptChanges();
                      fixedfirstrow = false;
                    }
                  }
                }
              }
            }

            dtCopy.Columns.Remove("id");
            dtCopy.AcceptChanges();

            string Ten = "";

            for (int m = 0; dtCopy.Rows.Count > m; m++)
            {
              if (Ten != dtCopy.Rows[m]["Tenant_Name"].ToString())
              {
                Ten = dtCopy.Rows[m]["Tenant_Name"].ToString();
                Rep.AddSingleValue(dtCopy.Rows[m]["num"].ToString(), lastrowinexcel, 1);
                Rep.AddSingleValue(dtCopy.Rows[m]["agr_date"].ToString(), lastrowinexcel, 2);
                Rep.AddSingleValue(dtCopy.Rows[m]["agr_num"].ToString(), lastrowinexcel, 3);
                Rep.AddSingleValue(dtCopy.Rows[m]["Tenant_Name"].ToString(), lastrowinexcel, 4);
                Rep.AddSingleValue(dtCopy.Rows[m]["LandLord_Adress"].ToString(), lastrowinexcel, 5);
                Rep.AddSingleValue(dtCopy.Rows[m]["Building"].ToString(), lastrowinexcel, 6);
                Rep.AddSingleValue(dtCopy.Rows[m]["Floor"].ToString(), lastrowinexcel, 7);
                Rep.AddSingleValue(dtCopy.Rows[m]["Section"].ToString(), lastrowinexcel, 8);
                Rep.AddSingleValue(dtCopy.Rows[m]["Total_Area"].ToString(), lastrowinexcel, 9);
                Rep.AddSingleValue(dtCopy.Rows[m]["Phone"].ToString(), lastrowinexcel, 10);
                Rep.AddSingleValue(dtCopy.Rows[m]["Cost_of_Meter"].ToString(), lastrowinexcel, 11);
                Rep.AddSingleValue(dtCopy.Rows[m]["nds"].ToString(), lastrowinexcel, 12);
                Rep.AddSingleValue(dtCopy.Rows[m]["Total_Sum"].ToString(), lastrowinexcel, 13);
              }

              if ((type == 1) || (type == 3))
              {
                Rep.AddSingleValue(dtCopy.Rows[m]["Plan_Start"].ToString(), lastrowinexcel, 14);
                Rep.AddSingleValue(dtCopy.Rows[m]["Plan_End"].ToString(), lastrowinexcel, 15);
                if (type == 1)
                {
                  Rep.AddSingleValue(dtCopy.Rows[m]["paymentDate"].ToString(), lastrowinexcel, 16);
                  Rep.AddSingleValue(dtCopy.Rows[m]["PaymentTelSum"].ToString(), lastrowinexcel, 17);
                  Rep.AddSingleValue(dtCopy.Rows[m]["PaymentTotalSum"].ToString(), lastrowinexcel, 18);
                  Rep.AddSingleValue(dtCopy.Rows[m]["Penni"].ToString(), lastrowinexcel, 19);
                  Rep.AddSingleValue(dtCopy.Rows[m]["DopPaymentType"].ToString(), lastrowinexcel, 20);
                  Rep.AddSingleValue(dtCopy.Rows[m]["DopPayment"].ToString(), lastrowinexcel, 21);
                }
              }

              if (type == 2)
              {
                Rep.AddSingleValue(dtCopy.Rows[m]["paymentDate"].ToString(), lastrowinexcel, 14);
                Rep.AddSingleValue(dtCopy.Rows[m]["PaymentTelSum"].ToString(), lastrowinexcel, 15);
                Rep.AddSingleValue(dtCopy.Rows[m]["PaymentTotalSum"].ToString(), lastrowinexcel, 16);
                Rep.AddSingleValue(dtCopy.Rows[m]["Penni"].ToString(), lastrowinexcel, 17);
                Rep.AddSingleValue(dtCopy.Rows[m]["DopPayment"].ToString(), lastrowinexcel, 19);
              }

              lastrowinexcel++;
            }

            rowsPassed += dtCopy.Rows.Count;
            int perccc = Convert.ToInt32(rowsPassed * 100 / dt.Rows.Count);
            if (perccc > 100)
              perccc = 100;
            worker.ReportProgress(perccc);

            Payment_Type = bool.Parse(dt.Rows[i]["Payment_Type"].ToString());

            string Tenant = "";
            for (int y = 0; dtCopy.Rows.Count > y; y++)
            {
              if (Tenant != dtCopy.Rows[y]["Tenant_Name"].ToString())
              {
                Tenant = dtCopy.Rows[y]["Tenant_Name"].ToString();
                Total_Area += decimal.Parse(dtCopy.Rows[y]["Total_Area"].ToString());
                Phone += decimal.Parse(dtCopy.Rows[y]["Phone"].ToString());
                Total_Sum += decimal.Parse(dtCopy.Rows[y]["Total_Sum"].ToString());
                if (type != 2)
                {
                  try
                  {
                    decimal tmpStart = 0;
                    decimal.TryParse(dtCopy.Rows[y]["Plan_Start"].ToString(), out tmpStart);
                    //Plan_Start += decimal.Parse(dtCopy.Rows[y]["Plan_Start"].ToString());
                    Plan_Start += tmpStart;
                    decimal tmpEnd = 0;
                    decimal.TryParse(dtCopy.Rows[y]["Plan_End"].ToString(), out tmpEnd);
                    //Plan_End += decimal.Parse(dtCopy.Rows[y]["Plan_End"].ToString());
                    Plan_End += tmpEnd;
                  }
                  catch (Exception ex) { }
                }
              }
              if (type != 3)
              {
                PaymentTelSum += (dtCopy.Rows[y]["PaymentTelSum"].ToString() != "")
                  ? decimal.Parse(
                  numTextBox.ConvertToCompPunctuation(dtCopy.Rows[y]["PaymentTelSum"].ToString())
                  )
                  : 0;
                PaymentTotalSum += (dtCopy.Rows[y]["PaymentTotalSum"].ToString() != "")
                  ? decimal.Parse(
                  numTextBox.ConvertToCompPunctuation(dtCopy.Rows[y]["PaymentTotalSum"].ToString())
                  )
                  : 0;
                Penni += (dtCopy.Rows[y]["Penni"].ToString() != "")
                  ? decimal.Parse(
                  numTextBox.ConvertToCompPunctuation(dtCopy.Rows[y]["Penni"].ToString())
                  )
                  : 0;
                DopPayment += (dtCopy.Rows[y]["DopPayment"].ToString() != "")
                  ? decimal.Parse(
                  numTextBox.ConvertToCompPunctuation(dtCopy.Rows[y]["DopPayment"].ToString())
                  )
                  : 0;
              }
            }

            if (bool.Parse(dt.Rows[i]["Payment_Type"].ToString()) == true)
            {
              Rep.AddSingleValue("Итого по арендодателю (нал)", lastrowinexcel, 1);
            }
            else
            {
              Rep.AddSingleValue("Итого по арендодателю (безнал)", lastrowinexcel, 1);
            }

            if (type == 1)
            {
              Rep.AddSingleValue(Total_Area.ToString(), lastrowinexcel, 9);
              Rep.AddSingleValue(Phone.ToString(), lastrowinexcel, 10);
              Rep.AddSingleValue(Total_Sum.ToString(), lastrowinexcel, 13);
              Rep.AddSingleValue(Plan_Start.ToString(), lastrowinexcel, 14);
              Rep.AddSingleValue(Plan_End.ToString(), lastrowinexcel, 15);
              Rep.AddSingleValue(PaymentTelSum.ToString(), lastrowinexcel, 17);
              Rep.AddSingleValue(PaymentTotalSum.ToString(), lastrowinexcel, 18);
              Rep.AddSingleValue(Penni.ToString(), lastrowinexcel, 19);
              Rep.AddSingleValue(DopPayment.ToString(), lastrowinexcel, 21);
              Rep.SetFontBold(lastrowinexcel, colStart, lastrowinexcel, columnsCount);
            }

            if (type == 2)
            {
              Rep.AddSingleValue(Total_Area.ToString(), lastrowinexcel, 9);
              Rep.AddSingleValue(Phone.ToString(), lastrowinexcel, 10);
              Rep.AddSingleValue(Total_Sum.ToString(), lastrowinexcel, 13);
              //Rep.AddSingleValue(Plan_Start.ToString(), lastrowinexcel, 14);
              //Rep.AddSingleValue(Plan_End.ToString(), lastrowinexcel, 15);
              Rep.AddSingleValue(PaymentTelSum.ToString(), lastrowinexcel, 15);
              Rep.AddSingleValue(PaymentTotalSum.ToString(), lastrowinexcel, 16);
              Rep.AddSingleValue(Penni.ToString(), lastrowinexcel, 17);
              Rep.AddSingleValue(DopPayment.ToString(), lastrowinexcel, 19);
              Rep.SetFontBold(lastrowinexcel, colStart, lastrowinexcel, columnsCount);
            }

            if (type == 3)
            {
              Rep.AddSingleValue(Total_Area.ToString(), lastrowinexcel, 9);
              Rep.AddSingleValue(Phone.ToString(), lastrowinexcel, 10);
              Rep.AddSingleValue(Total_Sum.ToString(), lastrowinexcel, 13);
              Rep.AddSingleValue(Plan_Start.ToString(), lastrowinexcel, 14);
              Rep.AddSingleValue(Plan_End.ToString(), lastrowinexcel, 15);
              //Rep.AddSingleValue(PaymentTelSum.ToString(), lastrowinexcel, 15);
              //Rep.AddSingleValue(PaymentTotalSum.ToString(), lastrowinexcel, 16);
              //Rep.AddSingleValue(Penni.ToString(), lastrowinexcel, 17);
              //Rep.AddSingleValue(DopPayment.ToString(), lastrowinexcel, 18);
              Rep.SetFontBold(lastrowinexcel, colStart, lastrowinexcel, columnsCount);
            }
            Rep.SetFontBold(lastrowinexcel, colStart, lastrowinexcel, columnsCount);

            total_Total_Area += Total_Area;
            total_Phone += Phone;
            total_Total_Sum += Total_Sum;
            total_Plan_Start += Plan_Start;
            total_Plan_End += Plan_End;
            total_PaymentTelSum += PaymentTelSum;
            total_PaymentTotalSum += PaymentTotalSum;
            total_Penni += Penni;
            total_DopPayment += DopPayment;

            lastrowinexcel++;
            Total_Area =
              Phone =
              Total_Sum =
              Plan_Start =
              Plan_End =
              PaymentTelSum =
              PaymentTotalSum =
              Penni =
              DopPayment = 0;
          }
        }
      }

      Rep.AddSingleValue("ИТОГО по арендодателю:", lastrowinexcel, 1);
      if (type == 1)
      {
        Rep.AddSingleValue(total_Total_Area.ToString(), lastrowinexcel, 9);
        Rep.AddSingleValue(total_Phone.ToString(), lastrowinexcel, 10);
        Rep.AddSingleValue(total_Total_Sum.ToString(), lastrowinexcel, 13);
        Rep.AddSingleValue(total_Plan_Start.ToString(), lastrowinexcel, 14);
        Rep.AddSingleValue(total_Plan_End.ToString(), lastrowinexcel, 15);
        Rep.AddSingleValue(total_PaymentTelSum.ToString(), lastrowinexcel, 17);
        Rep.AddSingleValue(total_PaymentTotalSum.ToString(), lastrowinexcel, 18);
        Rep.AddSingleValue(total_Penni.ToString(), lastrowinexcel, 19);
        Rep.AddSingleValue(total_DopPayment.ToString(), lastrowinexcel, 21);
        Rep.SetFontBold(lastrowinexcel, colStart, lastrowinexcel, columnsCount);
      }

      if (type == 2)
      {
        Rep.AddSingleValue(total_Total_Area.ToString(), lastrowinexcel, 9);
        Rep.AddSingleValue(total_Phone.ToString(), lastrowinexcel, 10);
        Rep.AddSingleValue(total_Total_Sum.ToString(), lastrowinexcel, 13);
        //Rep.AddSingleValue(total_Plan_Start.ToString(), lastrowinexcel, 14);
        //Rep.AddSingleValue(total_Plan_End.ToString(), lastrowinexcel, 15);
        Rep.AddSingleValue(total_PaymentTelSum.ToString(), lastrowinexcel, 15);
        Rep.AddSingleValue(total_PaymentTotalSum.ToString(), lastrowinexcel, 16);
        Rep.AddSingleValue(total_Penni.ToString(), lastrowinexcel, 17);
        Rep.AddSingleValue(total_DopPayment.ToString(), lastrowinexcel, 19);
        Rep.SetFontBold(lastrowinexcel, colStart, lastrowinexcel, columnsCount);
      }

      if (type == 3)
      {
        Rep.AddSingleValue(total_Total_Area.ToString(), lastrowinexcel, 9);
        Rep.AddSingleValue(total_Phone.ToString(), lastrowinexcel, 10);
        Rep.AddSingleValue(total_Total_Sum.ToString(), lastrowinexcel, 13);
        Rep.AddSingleValue(total_Plan_Start.ToString(), lastrowinexcel, 14);
        Rep.AddSingleValue(total_Plan_End.ToString(), lastrowinexcel, 15);
        //Rep.AddSingleValue(total_PaymentTelSum.ToString(), lastrowinexcel, 15);
        //Rep.AddSingleValue(total_PaymentTotalSum.ToString(), lastrowinexcel, 16);
        //Rep.AddSingleValue(total_Penni.ToString(), lastrowinexcel, 17);
        //Rep.AddSingleValue(total_DopPayment.ToString(), lastrowinexcel, 18);
        Rep.SetFontBold(lastrowinexcel, colStart, lastrowinexcel, columnsCount);
      }
      Rep.SetFontBold(lastrowinexcel, colStart, lastrowinexcel, columnsCount);

      if (type == 1)
      {
        Rep.SetCellAlignmentToCenter(dtRowStart + 1, 2, lastrowinexcel, 2);//дата
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 3, lastrowinexcel, 3);//№ дог
        Rep.SetCellAlignmentToCenter(dtRowStart + 1, 8, lastrowinexcel, 8);//№ сек
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 9, lastrowinexcel, 15);// с "S,м2" по "план конец"
        Rep.SetCellAlignmentToCenter(dtRowStart + 1, 16, lastrowinexcel, 16);//дата
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 17, totalRows, 19);// с "телефон" до пени
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 21, totalRows, 21);// сумма оплаты
      }

      if (type == 2)
      {
        Rep.SetCellAlignmentToCenter(dtRowStart + 1, 2, lastrowinexcel, 2);//дата
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 3, lastrowinexcel, 3);//№ дог
        Rep.SetCellAlignmentToCenter(dtRowStart + 1, 8, lastrowinexcel, 8);//№ сек
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 9, lastrowinexcel, 13);// с "S,м2" по "общ ст-ть"
        Rep.SetCellAlignmentToCenter(dtRowStart + 1, 14, lastrowinexcel, 14);//дата
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 15, totalRows, 17);// с "телефон" до пени
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 19, totalRows, 19);// сумма оплаты
      }

      if (type == 3)
      {
        Rep.SetCellAlignmentToCenter(dtRowStart + 1, 2, lastrowinexcel, 2);//дата
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 3, lastrowinexcel, 3);//№ дог
        Rep.SetCellAlignmentToCenter(dtRowStart + 1, 8, lastrowinexcel, 8);//№ сек
        Rep.SetCellAlignmentToRight(dtRowStart + 1, 9, lastrowinexcel, 15);// с "S,м2" по "план конец"
      }

      if ((wrap!= null) && (wrap.Rows.Count > 0))
      {
        for (int t = 0; wrap.Rows.Count > t; t++)
        {
          //13 количество объединяемых колонок 
          for (int z = 0; 13 > z; z++)
          {
            int start = int.Parse(wrap.Rows[t]["start"].ToString());
            int end = int.Parse(wrap.Rows[t]["end"].ToString());
            Rep.Merge(start, z+1, end, z+1);
          }
        }
      }
      worker.ReportProgress(100);

      Rep.SetBorders(dtRowStart, colStart, lastrowinexcel, columnsCount);
      Rep.Show();
    }

    private void bgwToExcel_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      this.lblPercent.Text = ("Обработка данных: " + e.ProgressPercentage.ToString() + "%");
      this.prbExcel.Value = e.ProgressPercentage;
    }
  }
}
