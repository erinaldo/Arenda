﻿using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TenantsReport
{
    public partial class frmMain : Form
    {
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbObjects.DataSource = proc.GetObjects();
            cbObjects.ValueMember = "id";
            cbObjects.DisplayMember = "cName";

            cbLandlord.DataSource = proc.GetTenants(0);
            cbLandlord.ValueMember = "id";
            cbLandlord.DisplayMember = "Tenant";

            cbActivties.DataSource = proc.GetActivities();
            cbActivties.ValueMember = "id";
            cbActivties.DisplayMember = "cName";
        }
        private void cbObjects_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbObjects.SelectedIndex != -1)
            {
                cbLandlord.DataSource = proc.GetTenants((int)cbObjects.SelectedValue);
                cbLandlord.ValueMember = "id";
                cbLandlord.DisplayMember = "Tenant";
            }
        }
        private void btExit_Click(object sender, EventArgs e)
        {
            //Close();
            this.DialogResult = DialogResult.Cancel;
        }

        private async void btSave_Click(object sender, EventArgs e)
        {
            if (cbActivties.SelectedIndex != -1 && cbObjects.SelectedIndex != -1)
            {
                Logging.StartFirstLevel(79);

                Logging.Comment($"Объект ID:{cbObjects.SelectedValue}; Наименование:{cbObjects.Text}");
                Logging.Comment($"Арендодатель ID:{cbLandlord.SelectedValue}; Наименование:{cbLandlord.Text}");
                Logging.Comment($"Вид деятельности ID:{cbActivties.SelectedValue}; Наименование:{cbActivties.Text}");

                Logging.StopFirstLevel();

                await Task.Factory.StartNew(() => getReport());
            }
        }
        private void DoOnUIThread(MethodInvoker d)
        {
            if (this.InvokeRequired) { this.Invoke(d); } else { d(); }
        }
        private void getReport()
        {
            DoOnUIThread(delegate ()
            {
                this.Enabled = false;
                DataTable dtReport = proc.GetTenantReport((int)cbObjects.SelectedValue, (int)cbLandlord.SelectedValue, (int)cbActivties.SelectedValue);
                if (dtReport.Rows.Count == 0)
                {
                    MessageBox.Show("Нет данных для выгрузки", "Нет данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Enabled = true;
                    return;
                }
                else
                {
                    Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();
                    int indexRow = 1;

                    report.SetPageOrientationToLandscape();

                    ///Заголовок
                    report.Merge(indexRow, 1, indexRow, 11);
                    report.AddSingleValue("ОТЧЕТ О ВИДАХ ДЕЯТЕЛЬНОСТИ АРЕНДАТОРОВ", indexRow, 1);
                    report.SetCellAlignmentToJustify(indexRow, 1, indexRow + 1, 11);
                    report.SetCellAlignmentToCenter(indexRow, 1, indexRow + 1, 11);
                    report.SetFontBold(indexRow, 1, indexRow, 11);
                    report.SetFontSize(indexRow, 1, indexRow, 11, 16);
                    indexRow++;
                    indexRow++;

                    ///Параметры
                    report.AddSingleValue("Объект:", indexRow, 2);
                    report.SetFontBold(indexRow, 2, indexRow, 2);
                    report.AddSingleValue(cbObjects.Text.Trim(), indexRow, 3);
                    indexRow++;
                    report.AddSingleValue("Арендодатель:", indexRow, 2);
                    report.SetFontBold(indexRow, 2, indexRow, 2);
                    report.AddSingleValue(cbLandlord.Text.Trim(), indexRow, 3);
                    indexRow++;
                    report.AddSingleValue("Вид деятельности:", indexRow, 2);
                    report.SetFontBold(indexRow, 2, indexRow, 2);
                    report.AddSingleValue(cbActivties.Text.Trim(), indexRow, 3);
                    indexRow++;
                    indexRow++;
                    report.AddSingleValue("Выгрузил:", indexRow, 2);
                    report.SetFontBold(indexRow, 2, indexRow, 2);
                    report.AddSingleValue(Nwuram.Framework.Settings.User.UserSettings.User.FullUsername.Trim(), indexRow, 3);
                    indexRow++;
                    report.AddSingleValue("Дата:", indexRow, 2);
                    report.SetFontBold(indexRow, 2, indexRow, 2);
                    report.AddSingleValue(proc.GetDate().ToString(), indexRow, 3);
                    indexRow++;
                    indexRow++;

                    ///Названия столбцов
                    report.Merge(indexRow, 1, indexRow + 1, 1);
                    report.AddSingleValue("Объект", indexRow, 1);
                    report.Merge(indexRow, 2, indexRow + 1, 2);
                    report.AddSingleValue("Арендодатель", indexRow, 2);
                    report.Merge(indexRow, 3, indexRow + 1, 3);
                    report.AddSingleValue("Вид деятельности", indexRow, 3);
                    report.Merge(indexRow, 4, indexRow + 1, 4);
                    report.AddSingleValue("№ п/п", indexRow, 4);
                    report.SetWrapText(indexRow, 4, indexRow + 1, 4);
                    report.Merge(indexRow, 5, indexRow + 1, 5);
                    report.AddSingleValue("Арендатор", indexRow, 5);
                    report.Merge(indexRow, 6, indexRow + 1, 6);
                    report.AddSingleValue("Вид договора", indexRow, 6);
                    report.Merge(indexRow, 7, indexRow, 9);
                    report.AddSingleValue("Местоположение", indexRow, 7);
                    report.AddSingleValue("Здание", indexRow + 1, 7);
                    report.AddSingleValue("Этаж", indexRow + 1, 8);
                    report.AddSingleValue("№ секции", indexRow + 1, 9);
                    report.Merge(indexRow, 10, indexRow + 1, 10);
                    report.AddSingleValue("Телефон", indexRow, 10);
                    report.Merge(indexRow, 11, indexRow + 1, 11);
                    report.AddSingleValue("Эл.почта", indexRow, 11);
                    report.SetCellAlignmentToJustify(indexRow, 1, indexRow + 1, 11);
                    report.SetCellAlignmentToCenter(indexRow, 1, indexRow + 1, 11);
                    report.SetBorders(indexRow, 1, indexRow + 1, 11);

                    report.SetFontBold(indexRow, 1, indexRow + 1, 11);
                    report.SetFontSize(indexRow, 1, indexRow + 1, 11, 14);
                    indexRow++;
                    indexRow++;

                    int ObjIndexStart = indexRow, ActIndexStart = indexRow, LandlordIndexStart = indexRow;
                    int NumPP = 1;///Номер строки
                    var dtObject = dtReport.AsEnumerable().Select(r => r.Field<int>("IdObject")).Distinct(); ;
                    foreach (var obj in dtObject)
                    {
                        ObjIndexStart = indexRow;
                        EnumerableRowCollection<DataRow> rcObj = dtReport.AsEnumerable().Where(r => r.Field<int>("IdObject") == obj);
                        report.AddSingleValue(rcObj.First()["Object"].ToString(), indexRow, 1);
                        var dtLandlord = rcObj.AsEnumerable().Select(r => r.Field<int>("IdLandlord")).Distinct();
                        foreach (var Landlord in dtLandlord)
                        {
                            LandlordIndexStart = indexRow;
                            EnumerableRowCollection<DataRow> rcLandlord = dtReport.AsEnumerable().Where(r => r.Field<int>("IdObject") == obj && r.Field<int>("IdLandlord") == Landlord);
                            report.AddSingleValue(rcLandlord.First()["Landlord"].ToString(), indexRow, 2);
                            var dtActivities = rcLandlord.AsEnumerable().Select(r => r.Field<int>("IdActivities")).Distinct();
                            foreach (var act in dtActivities)
                            {
                                ActIndexStart = indexRow;
                                EnumerableRowCollection<DataRow> rcAct = dtReport.AsEnumerable().Where(r => r.Field<int>("IdObject") == obj && r.Field<int>("IdLandlord") == Landlord && r.Field<int>("IdActivities") == act);
                                report.AddSingleValue(rcAct.First()["Activities"].ToString(), indexRow, 3);
                                foreach (var dr in rcAct)
                                {
                                    report.AddSingleValue(NumPP.ToString(), indexRow, 4);
                                    report.AddSingleValue(dr["Tenant"].ToString(), indexRow, 5);
                                    report.AddSingleValue(dr["TypeContract"].ToString(), indexRow, 6);
                                    report.AddSingleValue(dr["Building"].ToString(), indexRow, 7);
                                    report.AddSingleValue(dr["Floor"].ToString(), indexRow, 8);
                                    report.AddSingleValue(dr["Section"].ToString(), indexRow, 9);
                                    report.AddSingleValue(dr["Work_phone"].ToString(), indexRow, 10);
                                    report.AddSingleValue(dr["email"].ToString(), indexRow, 11);

                                    report.SetCellAlignmentToLeft(indexRow, 4, indexRow, 11);
                                    report.SetCellAlignmentToJustify(indexRow, 4, indexRow, 11);
                                    report.SetBorders(indexRow, 4, indexRow, 11);
                                    NumPP++;
                                    indexRow++;
                                }
                                report.Merge(ActIndexStart, 3, indexRow - 1, 3);
                                report.SetCellAlignmentToLeft(ActIndexStart, 3, indexRow - 1, 3);
                                report.SetCellAlignmentToJustify(ActIndexStart, 3, indexRow - 1, 3);
                                report.SetBorders(ActIndexStart, 3, indexRow - 1, 3);
                            }
                            report.Merge(LandlordIndexStart, 2, indexRow - 1, 2);
                            report.SetCellAlignmentToLeft(LandlordIndexStart, 2, indexRow - 1, 2);
                            report.SetCellAlignmentToJustify(LandlordIndexStart, 2, indexRow - 1, 2);
                            report.SetBorders(LandlordIndexStart, 2, indexRow - 1, 2);
                        }
                        report.Merge(ObjIndexStart, 1, indexRow - 1, 1);
                        report.SetCellAlignmentToLeft(ObjIndexStart, 1, indexRow - 1, 1);
                        report.SetCellAlignmentToJustify(ObjIndexStart, 1, indexRow - 1, 1);
                        report.SetBorders(ObjIndexStart, 1, indexRow - 1, 1);
                    }
                  
                    report.SetColumnAutoSize(1, 1, indexRow - 1, 11);
                    report.SetColumnWidth(10, 4, 11, 4, 5);///Для № п/п
                    report.SetWrapText(12, 1, indexRow - 1, 11);///Перенос текста для всех данных кроме телефон и почта
                    report.SetPageSetup(1, 1, true);
                    report.Show();
                }
                this.Enabled = true;
            });
        }
    }
}
