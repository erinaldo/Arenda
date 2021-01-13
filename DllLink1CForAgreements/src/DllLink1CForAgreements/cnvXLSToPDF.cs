﻿using System;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace DllLink1CForAgreements
{
    public class cnvXLSToPDF
    {

        public static void ConvertData(string files)
        {
            if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + @"\Report\"))
                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\Report\");

            ApplicationClass excelApplication = new ApplicationClass();
            Workbook excelWorkBook = null;

            string paramSourceBookPath = files;//System.Windows.Forms.Application.StartupPath + @"\" + files;// @"D:\test.xls";
            object paramMissing = Type.Missing;

            string name = Path.GetFileNameWithoutExtension(files);

            string paramExportFilePath = System.Windows.Forms.Application.StartupPath + @"\Report\" + name + ".pdf";

            //MessageBox.Show(paramExportFilePath);

            XlFixedFormatType paramExportFormat = XlFixedFormatType.xlTypePDF;
            XlFixedFormatQuality paramExportQuality =
                XlFixedFormatQuality.xlQualityStandard;
            bool paramOpenAfterPublish = false;
            bool paramIncludeDocProps = true;
            bool paramIgnorePrintAreas = true;
            object paramFromPage = Type.Missing;
            object paramToPage = Type.Missing;

            try
            {
                // Open the source workbook.
                excelWorkBook = excelApplication.Workbooks.Open(paramSourceBookPath,
                    paramMissing, paramMissing, paramMissing, paramMissing,
                    paramMissing, paramMissing, paramMissing, paramMissing,
                    paramMissing, paramMissing, paramMissing, paramMissing,
                    paramMissing, paramMissing);

                // Save it in the target format.
                if (excelWorkBook != null)
                {
                    excelWorkBook.ExportAsFixedFormat(paramExportFormat,
                        paramExportFilePath, paramExportQuality,
                        paramIncludeDocProps, paramIgnorePrintAreas, paramFromPage,
                        paramToPage, paramOpenAfterPublish,
                        paramMissing);
                    // MessageBox.Show(paramExportFilePath);
                }


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                // Respond to the error.
            }
            finally
            {
                // Close the workbook object.
                if (excelWorkBook != null)
                {
                    excelWorkBook.Close(false, paramMissing, paramMissing);
                    excelWorkBook = null;
                }

                // Quit Excel and release the ApplicationClass object.
                if (excelApplication != null)
                {
                    excelApplication.Quit();
                    excelApplication = null;
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
