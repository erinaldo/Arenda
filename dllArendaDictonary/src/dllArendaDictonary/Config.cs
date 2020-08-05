using System;
using System.Windows.Forms;

namespace dllArendaDictonary
{
    class Config
    {
        public static Procedures hCntMain { get; set; } //осн. коннект

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

        public static void DoOnUIThread(MethodInvoker d, Form _this)
        {
            if (_this.InvokeRequired) { _this.Invoke(d); } else { d(); }
        }
    }

    public class Deps
    {
        //private List<int> listDeps = new List<int>();
        private int id_deps;
        private bool isPost;
        private int id_Post;

        public void setIdDeps(int id_deps) { this.id_deps = id_deps; }

        public int getIdDeps() { return this.id_deps; }

        public void setIsPost(bool isPost) { this.isPost = isPost; }

        public bool getIsPost() { return this.isPost; }

        public void setIdPost(int id_Post) { this.id_Post = id_Post; }

        public int getIdPost() { return this.id_Post; }
    }

    public class Document
    {
        /// <summary>
        /// Код документа
        /// </summary>
        public int id_document { get; set; }
        /// <summary>
        /// Код связки документа и должности
        /// </summary>
        public int id_documentVsPost { get; set; }
        /// <summary>
        /// Код статуса документа
        /// </summary>
        public int id_Status { get; set; }
        /// <summary>
        /// Признак просмотра пользователем
        /// </summary>
        public bool isBrowse { get; set; }

        /// <summary>
        /// Признак отдела пользователя
        /// </summary>
        public bool isWorkDep { get; set; }
    }
}
