using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace ArendaPrint
{
    class ReportArenda
    {
        Word.Application app;
        Word.Document doc;
        object fileName = "";
        object Dir;
        object nameToSave;

        Dictionary<int, string> folders = new Dictionary<int, string>
        {
            { 1, "Площадь" },
            { 2, "Реклама" },
            { 3, "Земельный участок" }
        };

        public ReportArenda(string Temp, string startup, string Dir, string nameToSave, string template, int _idtd)
        {
            fileName = startup + $"\\Templates\\dot\\{folders[_idtd]}\\{template}.doc";
            app = new Word.Application();
            this.Dir = Dir;
            this.nameToSave = nameToSave;
        }

        public void Report(DataRow dr)
        {
            doc = app.Documents.Open(ref fileName, ref missing, ref missing, ref missing, ref missing, ref missing,
                                                      ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                                                      ref missing, ref missing, ref missing, ref missing);
            app.Visible = false;

            foreach (Word.Bookmark mark in doc.Bookmarks)
            {
                string rowColumn = mark.Name;
                if (rowColumn.Contains("__"))
                    rowColumn = rowColumn.Remove(rowColumn.IndexOf("__"), rowColumn.Length - rowColumn.IndexOf("__"));
                doc.Bookmarks[mark].Range.Text = dr[rowColumn].ToString();
            }         
            SaveReport();
            Close(false);
            Process.Start(Dir + nameToSave.ToString() + ".doc");
        }
        private object missing = Missing.Value;
        private void SaveReport()
        {
            object save = Dir + nameToSave.ToString() + ".doc";
            doc.SaveAs(ref save, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                           ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
        }
        private void Close(object saveChanges)
        {
            doc.Close(ref saveChanges, ref missing, ref missing);
        }
    }
}
