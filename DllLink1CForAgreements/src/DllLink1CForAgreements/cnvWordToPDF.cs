using System;
using Word =  Microsoft.Office.Interop.Word;
using System.IO;

namespace DllLink1CForAgreements
{
    public class cnvWordToPDF
    {

        public static void ConvertData(string files)
        {
            if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + @"\Report\"))
                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\Report\");

            object paramSourceDocPath = files;//System.Windows.Forms.Application.StartupPath + @"\" + files;// @"D:\test.xls";
            object paramMissing = Type.Missing;

            string name = Path.GetFileNameWithoutExtension(files);
            string paramExportFilePath = System.Windows.Forms.Application.StartupPath + @"\Report\" + name + ".pdf";
            Word.WdExportFormat paramExportFormat = Word.WdExportFormat.wdExportFormatPDF;

            bool result;
            Word.ApplicationClass wordApplication = new Word.ApplicationClass();
            Word.Document wordDocument = null;
            try
            {                
                bool paramOpenAfterExport = false;
                Word.WdExportOptimizeFor paramExportOptimizeFor =
                Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
                Word.WdExportRange paramExportRange = Word.WdExportRange.wdExportAllDocument;
                int paramStartPage = 0;
                int paramEndPage = 0;
                Word.WdExportItem paramExportItem = Word.WdExportItem.wdExportDocumentContent;
                bool paramIncludeDocProps = true;
                bool paramKeepIRM = true;
                Word.WdExportCreateBookmarks paramCreateBookmarks =
                Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;
                bool paramDocStructureTags = true;
                bool paramBitmapMissingFonts = true;
                bool paramUseISO19005_1 = false;
                wordDocument = wordApplication.Documents.Open(
                                       ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                                       ref paramMissing, ref paramMissing, ref paramMissing,
                                       ref paramMissing, ref paramMissing, ref paramMissing,
                                       ref paramMissing, ref paramMissing, ref paramMissing,
                                       ref paramMissing, ref paramMissing, ref paramMissing,
                                       ref paramMissing);

                if (wordDocument != null)
                    wordDocument.ExportAsFixedFormat(paramExportFilePath,
                    paramExportFormat, paramOpenAfterExport,
                    paramExportOptimizeFor, paramExportRange, paramStartPage,
                    paramEndPage, paramExportItem, paramIncludeDocProps,
                    paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
                    paramBitmapMissingFonts, paramUseISO19005_1,
                    ref paramMissing);
                result = true;
            }
            finally
            {
                if (wordDocument != null)
                {
                    wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordDocument = null;
                }
                if (wordApplication != null)
                {
                    wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordApplication = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
