using System;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.InteropServices;

namespace DllLink1CForAgreements
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

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
        [DllImport("kernel32.dll")]
        //static extern uint GetLastError();
        static extern IntPtr SetLastError(int dwErrCode);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr EndTask(IntPtr hWnd);

        public static void EnsureProcessKilled(IntPtr MainWindowHandle, string Caption)
        {
            SetLastError(0);
            // for Excel versions <10, this won't be set yet
            if (IntPtr.Equals(MainWindowHandle, IntPtr.Zero))
                MainWindowHandle = FindWindow(null, Caption);
            if (IntPtr.Equals(MainWindowHandle, IntPtr.Zero))
                return; // at this point, presume the window has been closed.
            int iRes, iProcID;
            iRes = GetWindowThreadProcessId(MainWindowHandle, out iProcID);
            if (iProcID == 0)
            {
                if (EndTask(MainWindowHandle) != (IntPtr)0)
                    return; // success
                throw new ApplicationException("Failed to close.");
            }
            System.Diagnostics.Process proc;
            //proc = System.Diagnostics.Process.GetProcessById(ProcessID);
            proc = System.Diagnostics.Process.GetProcessById(iProcID);
            proc.CloseMainWindow();
            proc.Refresh();
            if (proc.HasExited)
                return;
            proc.Kill();
        }

    }

    public class FileData
    {
        public string FileName { private set; get; }
        private string Path;
        public string Number { private set; get; }
        public DateTime Date { private set; get; }
        public string Agreement { private set; get; }
        public string TypePay { private set; get; }
        public int idAgreement { set; get; }
        public string nameLandLord { set; get; }
        public typeFile tFile { private set; get; }
        public int id_Landlord { set; get; }

        public string nameObject { set; get; }
        public bool isAdd { set; get; }        

        public void setData(string FileName, string Path, string Number, DateTime Date, string Agreement, string TypePay, int idAgreement, bool isAdd, string nameLandLord, typeFile tFile,int id_LandLord,string nameObject)
        {
            this.FileName = FileName;
            this.Path = Path;
            this.Number = Number;
            this.Date = Date;
            this.Agreement = Agreement;
            this.TypePay = TypePay;
            this.idAgreement = idAgreement;
            this.isAdd = isAdd;
            this.nameLandLord = nameLandLord;
            this.tFile = tFile;
            this.id_Landlord = id_LandLord;
            this.nameObject = nameObject;
        }
    }

    public enum typeFile
    {
        excel = 1,
        word = 2
    }


}
