using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nwuram.Framework.Settings.Connection;

namespace Arenda
{
    //класс для получения результатов расчета оплаты по договору
    static class GetY
    {
        static readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        public static bool exit;
        public static decimal D;
        public static int Pr;
        public static decimal SumProg;
        public static decimal SumProgNotChanged;
        public static DataTable dtX;
        public static decimal Sopl;
        public static int K;
        public static int Day;
        public static decimal N;
        public static DateTime DateProg;
        public static decimal SumPhone;
        public static decimal Peni;
        public static int id_agreement;

        public static int w;

        public static DataTable dtY;

        /// <summary>
        /// Процедура разбивки оплаты по договору на месяцы и формирования таблицы на основе этой разбивки
        /// </summary>
        /// <param name="_dtX">расчетная таблица оплат по договору</param>
        /// <param name="_Sopl">оплаченная на текущий момент сумма</param>
        /// <param name="_K">дата из настроек, до которой должны вноситься оплаты</param>
        /// <param name="_Day">число из даты начала действия договора</param>
        /// <param name="_N">параметр из настроек</param>
        /// <param name="_DateProg">дата внесения оплаты выбранная в программе на форме frmAddPayment</param>
        /// <param name="_SumProg">сумма оплаты введенная в программе на форме frmAddPayment</param>
        /// <param name="_SumPhone">ежемесячная сумма для оплаты за телефон (по договору)</param>
        /// <param name="_id_agreement">id договора</param>
        /// <returns></returns>
        public static DataTable GetYtable(DataTable _dtX, decimal _Sopl, int _Day, decimal _N, DateTime _DateProg,
            decimal _SumProg, decimal _SumPhone, int _id_agreement)
        {
            decimal ch = _proc.GetSettings("chsl", 25);

            K = Convert.ToInt32(ch);
                //int.Parse(.ToString());

            TempData.SumProgAfterCount = 0;

            dtX = _dtX;
            Sopl = _Sopl;            
            Day = _Day;
            N = _N;
            DateProg = _DateProg;
            SumProgNotChanged = SumProg = _SumProg;
            SumPhone = _SumPhone;
            id_agreement = _id_agreement;

            exit = false;
            int i = 0;
            D = 0;
            Pr = 0;
            Peni = 0;

            dtY = new DataTable();
            dtY.Columns.Add("Summa", typeof(decimal));
            dtY.Columns.Add("Peni", typeof(decimal));
            dtY.Columns.Add("Pr", typeof(Int32));
            dtY.Columns.Add("Month", typeof(DateTime));
            dtY.Columns.Add("Phone", typeof(decimal));
            dtY.AcceptChanges();

            //ищем i
            if (Sopl > 0)
            {
                for (w = 0; dtX.Rows.Count > w; w++)
                {
                    if (Sopl < XiSumm())
                    {
                        i = w;
                        break;
                    }
                }
            }            

            //w - номер индекса строки в таблице (НЕ номер строки)
            for (w = i; dtX.Rows.Count > w; w++)
            {
                if (!exit)
                {
                    if (w == 0) //номер строки = 1
                    {
                        D = XiSumm() - Sopl;

                        BlockI_1();
                    }

                    if (w == 1) //номер строки = 2
                    {
                        if (Day > K - 1)
                        {
                            D = XiSumm() - Sopl;
                            BlockI_1();
                        }
                        else
                        {
                            BlockI_2plus();
                        }
                    }

                    if (w > 1) //номер строки > 2
                    {
                        BlockI_2plus();
                    }
                }
            }

            GetSumDebt();

            TempData.SumProgAfterCount = SumProg;

            return dtY;
        }


        private static decimal XiSumm()
        {
            return decimal.Parse(dtX.Rows[w]["SUMM"].ToString());
        }

        private static DateTime XiM()
        {
            return DateTime.Parse(dtX.Rows[w]["M"].ToString());
        }

        private static DateTime XiD2()
        {
            return DateTime.Parse(dtX.Rows[w]["D2"].ToString());
        }

        private static int DateDiffDays(DateTime d1, DateTime d2)
        {
            int result = 0;

            TimeSpan ti = d1 - d2;
            result = ti.Days;

            return result;
        }

        //блок где i = 1
        private static void BlockI_1()
        {
            if (SumProg > D)
            {
                AddY(D, 0, 0, XiM());                
                Sopl = Sopl + D;
                SumProg = SumProg - D;
            }
            else
            {
                AddY(SumProg, 0, 0, XiM());
                SumProg = 0;                
                exit = true;
            }
        }

        //блок где i > 2
        private static void BlockI_2plus()
        {
            if (DateProg > XiD2())
            {
                Pr = DateDiffDays(DateProg, XiD2());
                D = XiSumm() - Sopl;

                if (SumProg >= D)
                {
                    Peni = Pr * N * D / 100;
                    Peni = Math.Round(Peni, 2);
                }
                else
                {
                    Peni = Pr * N * SumProg / 100;
                    Peni = Math.Round(Peni, 2);
                }

                if (SumProg > (D + Peni))
                {
                    AddY(D, Peni, Pr, XiM());
                    SumProg = SumProg - D - Peni;
                    Sopl = Sopl + D;
                }
                else
                {
                    AddY(SumProg, Peni, Pr, XiM());
                    SumProg = 0;
                    exit = true;
                }
            }
            else
            {
                if ((Sopl + SumProg) > XiSumm())
                {
                    AddY((XiSumm() - Sopl), 0, 0, XiM());
                    decimal m = XiSumm() - Sopl;
                    SumProg = SumProg - m;
                    Sopl = Sopl + XiSumm() - Sopl;
                }
                else
                {
                    AddY(SumProg, 0, 0, XiM());
                    SumProg = 0;
                    exit = true;
                }
            }
        }

        /// <summary>
        /// Процедура добавления записи в dtY 
        /// </summary>
        /// <param name="ySumma"></param>
        /// <param name="yPeni"></param>
        /// <param name="yPr"></param>
        /// <param name="yMonth"></param>
        private static void AddY(decimal ySumma, decimal yPeni, int yPr, DateTime yMonth)
        {
            dtY.Rows.Add(ySumma, yPeni, yPr, yMonth, GetPhone(yPeni));
        }        

        /// <summary>
        /// Процедура расчета суммы оплаты за телефон
        /// </summary>
        /// <returns>сумма оплаты за телефон</returns>
        private static decimal GetPhone(decimal yPeni)
        {
            decimal result = 0;

            if (SumPhone == 0)
            {
                return result;
            }

            if (w == 0) //номер строки = 1
            {
                return result;
            }

            if (w == (dtX.Rows.Count -1)) //последняя строка
            {
                return result;
            }

            decimal P1 = _proc.GetPhone(id_agreement,XiM());

            if (P1 == SumPhone)
            {
                return result;
            }

            decimal Dphone = SumPhone - P1;

            if (Dphone > SumProg - yPeni)
            {
                result = SumProg - yPeni;                
            }
            else
            {
                result = Dphone;
            }

            return result;
        }       

        /// <summary>
        /// Процедура получения суммы долга
        /// </summary>
        private static void GetSumDebt()
        {
            TempData.SumDebtAfterCount = 0;
            TempData.PereplataAfterCount = 0;
            TempData.dateToPayAfterCount = DateTime.Now;
            TempData.isFullPayed = false;

            decimal res = 0;
            decimal Pereplata = 0;

            //сумма предыдущих оплат
            decimal SoplPred = _proc.GetSopl(id_agreement);
            
            /*
            Sopl = Sopl 
                //+ SumProg
                + decimal.Parse(dtY.Rows[dtY.Rows.Count - 1]["Summa"].ToString())
                - Peni;
            */

            decimal PeniY = 0;

            for (int i = 0; dtY.Rows.Count > i; i++)
            {
                PeniY += decimal.Parse(dtY.Rows[i]["Peni"].ToString());
            }

            Sopl = SoplPred + SumProgNotChanged - PeniY;

            for (w = 0; dtX.Rows.Count > w; w++)
            {
                if ((DateProg <= XiD2()) || (w == dtX.Rows.Count-1))
                {
                    //если смотрим последнюю запись в dtX
                    if (w == dtX.Rows.Count - 1)
                    {
                        TempData.dateToPayAfterCount = XiD2();

                        if (Sopl >= XiSumm())
                        {
                            Pereplata = Sopl - XiSumm();
                        }
                        else
                        {
                            res = XiSumm() - Sopl;
                        }
                        break;
                    }
                    //если смотрим не последнюю запись в dtX
                    else                    
                    {
                        if (DateTime.Parse(dtX.Rows[w]["D2"].ToString()) != DateTime.Parse(dtX.Rows[w + 1]["D2"].ToString()))
                        {
                            TempData.dateToPayAfterCount = XiD2();

                            if (Sopl >= XiSumm())
                            {
                                Pereplata = Sopl - XiSumm();
                            }
                            else
                            {
                                res = XiSumm() - Sopl;
                            }
                            break;
                        }
                    }                    
                }
            }

            decimal TotalSumToPay = decimal.Parse(dtX.Rows[dtX.Rows.Count - 1]["SUMM"].ToString());

            if (TotalSumToPay == Sopl)
            {
                TempData.isFullPayed = true;
            }

            TempData.SumDebtAfterCount = res;
            TempData.PereplataAfterCount = Pereplata;
        }
    }
}
