using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nwuram.Framework.Settings.Connection;
using System.Data;

namespace Arenda
{
    static class GetX
    {
        static readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        public static DataTable dtX;

        public static DataTable GetXtable(
            DateTime StartDate, DateTime DateEnd, 
            decimal TS, decimal OST,decimal L,
            bool Reklama, decimal CostOfMeter, decimal Phone, int id_agreement)
        {
            decimal ch = _proc.GetSettings("chsl", 25);

            int K = Convert.ToInt32(ch);
                //int.Parse(ch.ToString());

            bool inDiapazon = (StartDate.Day < K);
            //тут прям совсем закомментить
            //sveta dtX = _proc.GetTempTableForPenni(K, StartDate, DateEnd, TS, OST, L, inDiapazon);

            //для договора рекламы не имеет значение соглашение об изменении площади 
           /*sveta if (!Reklama)
            {

                int countDaysEnd = getCountDays(DateEnd);

                dtX = CheckChangeArea(
                            dtX,
                            CostOfMeter,
                            TS, StartDate, countDaysEnd, DateEnd, Phone, id_agreement);
            }*/
            //sveta return dtX;
            return null;
        }

        //передать TS назвать TSforcount 
        // дата начала действя договора для расчета kol1 в случае если меняется площадь 
        //с первого месяца действия договора
        public static DataTable CheckChangeArea(
            DataTable dtX, 
            decimal CostOfMeter, 
            decimal TSforcount, 
            DateTime DS, 
            int countDaysEnd, 
            DateTime DateEnd, 
            decimal SumPhoneAgr,
            int id_agreement)
        {
            DataTable dtAdditionDocs = new DataTable();
            dtAdditionDocs = _proc.GetAddDocs(id_agreement);

            //количество доп документов об изменении площади
            DataTable dtSquareChange = new DataTable();
            DataView view = new DataView(dtAdditionDocs);
            view.RowFilter = "id_TypeDoc = 5";
            if (view.Count != 0)
            {
                //доп соглашений может быть несколько
                //меняем сортировку на сортировку по дате начала изменений
                view.Sort = "DateRenewal asc";
                dtSquareChange = view.ToTable();

                //доп соглашений может быть несколько
                for (int i = 0; dtSquareChange.Rows.Count > i; i++)
                {
                    DateTime DateChange = DateTime.Parse(dtSquareChange.Rows[i]["DateRenewal"].ToString());

                    decimal TotalArea = decimal.Parse(dtSquareChange.Rows[i]["Total_Area"].ToString());

                    if (TotalArea != 0)
                    {
                        decimal NewTS = TotalArea * CostOfMeter + SumPhoneAgr;
                        NewTS = Math.Round(NewTS, 2);

                        //id строки с которой нужно вносить изменения
                        int stringStartChanges = -1;

                        for (int z = 0; dtX.Rows.Count > z; z++)
                        {
                            int mon = DateTime.Parse(dtX.Rows[z]["M"].ToString()).Month;
                            int year = DateTime.Parse(dtX.Rows[z]["M"].ToString()).Year;

                            if (
                                (mon == DateChange.Month)
                                &&
                                (year == DateChange.Year)
                                )
                            {
                                stringStartChanges = z;
                            }
                        }

                        if (stringStartChanges != -1)
                        {
                            int kol1 = 0;
                            int kol2 = 0;

                            kol1 = DateChange.Day - 1;
                            kol2 = GetX.getCountDays(DateChange) - DateChange.Day + 1;

                            //если площадь изменилась в первый месяц
                            if (stringStartChanges == 0)
                            {
                                kol1 = DateChange.Day - DS.Day;
                            }

                            for (int z = 0; dtX.Rows.Count > z; z++)
                            {
                                decimal prevSumm = 0;
                                decimal tempDec = decimal.Parse(dtX.Rows[z]["SUMM"].ToString());
                                tempDec = Math.Round(tempDec, 2);

                                if (z != 0)
                                {
                                    prevSumm = decimal.Parse(dtX.Rows[z - 1]["SUMM"].ToString());
                                }

                                //для строки с которой нужно вносить изменения
                                if (z == stringStartChanges)
                                {
                                    //если изменение площади произошло не в первый месяц действия договора
                                    //prevSumm будет равен 0
                                    int countDaysMonth = GetX.getCountDays(DateChange);
                                    tempDec = prevSumm + TSforcount * kol1 / countDaysMonth + NewTS * kol2 / countDaysMonth;
                                }

                                //для последующих строк
                                if (z > stringStartChanges)
                                {
                                    if (z != dtX.Rows.Count)
                                    {
                                        tempDec = prevSumm + NewTS;
                                    }
                                    //для последней строки в таблице
                                    else
                                    {
                                        //L считаем от NEWTS
                                        decimal L = NewTS / countDaysEnd * DateEnd.Day;
                                        L = Math.Round(L, 2);

                                        tempDec = prevSumm + L;
                                    }
                                }

                                dtX.Rows[z]["SUMM"] = Math.Round(tempDec, 2);
                            }

                            //перезаписать TSforcount на NEWTS для того чтобы если несколько соглашений 
                            //при расчете следующего использовался NewTs
                            TSforcount = NewTS;
                        }
                    }
                }
            }

            return dtX;
        }

        public static DateTime GetDateEnd(DataTable dt, int id_agreement)
        {
            DateTime result = DateTime.Parse(dt.Rows[0]["Stop_Date"].ToString());

            //альтернативное значение DateEnd

            DataTable dtAdditionDocs = new DataTable();
            dtAdditionDocs = _proc.GetAddDocs(id_agreement);

            //количество доп документов о расторжении
            int RastDocs = dtAdditionDocs.Select("id_TypeDoc = 4").Count();

            //количество доп документов о продлении
            int ProdltDocs = dtAdditionDocs.Select("id_TypeDoc = 1").Count();

            if (RastDocs > 0)
            {
                result = DateTime.Parse(dtAdditionDocs.Select("id_TypeDoc = 4")[0]["Date_of_Departure"].ToString());
            }
            else
            {
                if (ProdltDocs > 0)
                {
                    result = DateTime.Parse(dtAdditionDocs.Select("id_TypeDoc = 1")[0]["DateRenewal"].ToString());
                }
            }

            return result;
        }

        /// <summary>
        ////процедура получения количества дней в месяце из даты
        /// </summary>
        /// <param name="date">дата</param>
        /// <returns>количество дней в месяце из даты</returns>
        public static int getCountDays(DateTime date)
        {
            int result = 0;

            //временная переменная для расчета countDaysStart
            DateTime tempDate =
                DateTime.Parse(
                    "01."
                    + (date.Month < 10 ? "0" + date.Month.ToString() : date.Month.ToString())
                    + "." + date.Year.ToString()
                );
            //кол-во дней в месяце из DateStart
            result = tempDate.AddMonths(1).AddDays(-1).Day;

            return result;
        }


    }
}
