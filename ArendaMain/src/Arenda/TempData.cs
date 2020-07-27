using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arenda
{
    public static class TempData
    {
        public static string SelectedName;
        public static bool Cansel;
        public static string Rezhim = "";

        public static int isFirstScan = 0;

        //переменные для расчета просрочки, пени и проч
        public static decimal SumProgAfterCount = 0;
        public static decimal SumDebtAfterCount = 0;
        public static decimal PereplataAfterCount = 0;
        public static DateTime dateToPayAfterCount = DateTime.Now;
        public static bool isFullPayed = false;


        public static bool NeedToRefresh = false;

        public static string ScannedFileName = "";

        public static string centralText(string str)
        {
            int[] arra = new int[255];
            int count = 0;
            int maxLength = 0;
            int indexF = -1;
            arra[count] = 0;
            count++;
            indexF = str.IndexOf("\n");
            arra[count] = indexF;
            while (indexF != -1)
            {
                count++;
                indexF = str.IndexOf("\n", indexF + 1);
                arra[count] = indexF;
            }
            maxLength = arra[1] - arra[0];
            for (int i = 1; i < count; i++)
            {
                if (maxLength < (arra[i] - arra[i - 1]))
                {

                    maxLength = arra[i] - arra[i - 1];
                    if (i >= 2)
                    {
                        maxLength = maxLength - 1;
                    }
                }
            }
            string newString = "";
            string buffString = "";
            for (int i = 1; i < count; i++)
            {
                if (i >= 2)
                {

                    buffString = str.Substring(arra[i - 1] + 1, (arra[i] - arra[i - 1] - 1));
                    buffString = buffString.PadLeft(Convert.ToInt32(buffString.Length + ((maxLength - (arra[i] - arra[i - 1] - 1)) / 2) * 1.8));
                    //    buffString = buffString.PadRight(buffString.Length + ((maxLength - (arra[i] - arra[i - 1] - 1)) / 2)*2);
                    newString += buffString + "\n";
                }
                else
                {
                    buffString = str.Substring(arra[i - 1], arra[i]);
                    buffString = buffString.PadLeft(Convert.ToInt32(buffString.Length + ((maxLength - (arra[i] - arra[i - 1] - 1)) / 2) * 1.8));
                    // buffString = buffString.PadRight(buffString.Length + ((maxLength - (arra[i] - arra[i - 1])) / 2)*2);
                    newString = buffString + "\n";
                }

            }

            return newString;
        }
    }

    public static class infoPay
    {
        public static int id { set; get; }
        public static decimal Summa { set; get; }
        public static DateTime DateFines { set; get; }
        public static string cName { set; get; }
        public static decimal pfSumma { set; get; }
        public static decimal resDolg { set; get; }
        public static string PlanDate { set; get; }
    }
}
