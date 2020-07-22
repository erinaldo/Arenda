using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using CrystalDecisions.CrystalReports.Engine;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Logging;

namespace Arenda
{
    public partial class frmReport : Form
    {
        int type;
        string number;
        bool rekl;

        DataTable dtPrintResult, dtEquipment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_dtPrintResult">id договора</param>
        /// <param name="_dtEquipment">id акта</param>
        /// <param name="_type">тип: 1 - акт приема-передачи, 2 - акт приема-передачи (возврат)</param>
        public frmReport(DataTable _dtPrintResult, DataTable _dtEquipment, string _number, int _type)
        {
            InitializeComponent();
            this.dtPrintResult = _dtPrintResult;
            this.dtEquipment = _dtEquipment;
            this.type = _type;
            this.number = _number;
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            if (type == 1)
            {
                PrintAct();
            }
            
            if (type == 2)
            {
                PrintActVozvr();
            }
        }

        /// <summary>
        ////Заменяем двойные кавычки на одинарные
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string RQuotes(string text)
        {
            text = text.Replace("\"", "'").Replace("«", "'").Replace("»", "'");

            //text = text.Replace("\r\n", "' + chr(10) + '");
            text = text.Replace("\r\n", "");

            return text;
        }

        private void PrintAct()
        {
            if ((dtPrintResult != null) && (dtPrintResult.Rows.Count > 0))
            {
                rptAct rptReport = new rptAct();

                //rptActG rptReport = new rptActG();

                DataTable dtEq = new DataTable();
                if (dtEquipment != null)
                {
                    if (dtEquipment.Rows.Count > 0)
                    {
                        dtEq = dtEquipment.Copy();                        
                    }
                    else
                    {
                        dtEq.Columns.Add("equipment", typeof(string));
                        dtEq.Columns.Add("count", typeof(string));
                        DataRow new_dr = dtEq.NewRow();
                        new_dr["equipment"] = "__________";
                        new_dr["count"] = "___";
                        dtEq.Rows.Add(new_dr);
                        dtEq.AcceptChanges();
                    }
                }
                else
                {
                    dtEq.Columns.Add("equipment", typeof(string));
                    dtEq.Columns.Add("count", typeof(string));
                    DataRow new_dr = dtEq.NewRow();
                    new_dr["equipment"] = "__________";
                    new_dr["count"] = "___";
                    dtEq.Rows.Add(new_dr);
                    dtEq.AcceptChanges();
                }

                rptReport.SetDataSource((DataTable)dtEq);


                string Landlord_basement = ",";
                if (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length != 0)
                    Landlord_basement = ", действующего на основании "
                        + (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_basement"].ToString());

                if (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length != 0)
                    Landlord_basement += (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length == 0 ? "" : " № " + dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString());
                if (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length != 0)
                    Landlord_basement += " от " + (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString());

                if (Landlord_basement != ",")
                    Landlord_basement += ",";

                string Tenant_basement = ",";
                if (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length != 0)
                    Tenant_basement = ", действующего на основании "
                        + (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_basement"].ToString());

                if (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length != 0)
                    Tenant_basement += (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length == 0 ? "" : " № " + dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString());
                if (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length != 0)
                    Tenant_basement += " от " + (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString());

                if (Tenant_basement != ",")
                    Tenant_basement += ",";

                string adress = (dtPrintResult.DefaultView[0]["adress"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["adress"].ToString());
                string num = "к договору № " + number + " от " + (dtPrintResult.DefaultView[0]["date_con"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_con"].ToString());

                string date_act = (dtPrintResult.DefaultView[0]["date_act"].ToString().Trim().Length == 0) ? " - " : dtPrintResult.DefaultView[0]["date_act"].ToString();
                
                string arendodatel_str = (dtPrintResult.DefaultView[0]["arendodatel_str"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["arendodatel_str"].ToString());

                string arendodatel_str_full = "в лице "
                        + (dtPrintResult.DefaultView[0]["post_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["post_arendodatel"].ToString())
                        + " " + (dtPrintResult.DefaultView[0]["FIO_arendodatel_Par"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["FIO_arendodatel_Par"].ToString())
                        + Landlord_basement
                        + " именуемое в дальнейшем «Арендодатель», и";

                string arendator_str = (dtPrintResult.DefaultView[0]["arendator_str"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["arendator_str"].ToString());

                string arendator_str_full = "в лице "
                        + (dtPrintResult.DefaultView[0]["post_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["post_arendator"].ToString())
                        + " " + (dtPrintResult.DefaultView[0]["FIO_arendator_Par"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["FIO_arendator_Par"].ToString())
                        + Tenant_basement + " именуемое в дальнейшем «Арендатор», составили настоящий Акт о нижеследующем:";

                string section = (dtPrintResult.DefaultView[0]["section"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["section"].ToString());

                string floors = (dtPrintResult.DefaultView[0]["floors"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["floors"].ToString());

                string S_arend = (dtPrintResult.DefaultView[0]["S_arend"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["S_arend"].ToString());

                string punkt1 = "1. Во исполнение Договора аренды объекта нежилого фонда №"
                        + number
                        + " от "
                        + (dtPrintResult.DefaultView[0]["date_con"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_con"].ToString())
                        + ", заключенного между "
                        + arendodatel_str + " («Арендодатель») и "
                        + arendator_str + " («Арендатор»). Арендодатель перед подписанием настоящего акта произвел передачу Арендатору объекта нежилого фонда, расположенного по адресу: "
                        + adress + ", сек. №"
                        + section + " (" + floors + ") , общей площадью "
                        + S_arend + "кв.м., именуемое далее «Объект».";

                string count_lamps = "          - Имущества: светильники в кол-ве        "
                        + (dtPrintResult.DefaultView[0]["count_lamps"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["count_lamps"].ToString())
                        + " шт.";

                string count_phone = "          - Телефонные линии в количестве         "
                        + (dtPrintResult.DefaultView[0]["count_phone"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["count_phone"].ToString())
                        + " шт.";
                
                string FIO_arendator = (dtPrintResult.DefaultView[0]["FIO_arendator"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["FIO_arendator"].ToString());

                string FIO_arendodatel = (dtPrintResult.DefaultView[0]["FIO_arendodatel"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["FIO_arendodatel"].ToString());

                string failComm = dtPrintResult.DefaultView[0]["failComment"] == DBNull.Value ? "" : dtPrintResult.DefaultView[0]["failComment"].ToString();

                rptReport.DataDefinition.FormulaFields["adress"].Text = "\"" + RQuotes(adress) + "\"";
                rptReport.DataDefinition.FormulaFields["num"].Text = "\"" + RQuotes(num) + "\"";
                rptReport.DataDefinition.FormulaFields["date_act"].Text = "\"" + RQuotes(date_act) + "\"";
                rptReport.DataDefinition.FormulaFields["arendodatel_str"].Text = "\"" + RQuotes(arendodatel_str) + "\"";
                rptReport.DataDefinition.FormulaFields["arendodatel_str_full"].Text = "\"" + RQuotes(arendodatel_str_full) + "\"";
                rptReport.DataDefinition.FormulaFields["arendator_str"].Text = "\"" + RQuotes(arendator_str) + "\"";
                rptReport.DataDefinition.FormulaFields["arendator_str_full"].Text = "\"" + RQuotes(arendator_str_full) + "\"";
                rptReport.DataDefinition.FormulaFields["punkt1"].Text = "\"" + RQuotes(punkt1) + "\"";
                rptReport.DataDefinition.FormulaFields["count_lamps"].Text = "\"" + RQuotes(count_lamps) + "\"";
                rptReport.DataDefinition.FormulaFields["count_phone"].Text = "\"" + RQuotes(count_phone) + "\"";
                rptReport.DataDefinition.FormulaFields["FIO_arendator"].Text = "\"" + RQuotes(FIO_arendator) + "\"";
                rptReport.DataDefinition.FormulaFields["FIO_arendodatel"].Text = "\"" + RQuotes(FIO_arendodatel) + "\"";
                rptReport.DataDefinition.FormulaFields["failComment"].Text = "\"" + RQuotes(failComm) + "\"";
                //rptReport.DataDefinition.FormulaFields["failComment"].Text = "'" + RQuotes(failComm) + "'";                
                
                //rptReport.DataDefinition.FormulaFields["date"].Text = "\"" + "Дата: " + DateTime.Parse(dtHead.Rows[0]["date"].ToString()).ToString("dd.MM.yyyy") + "\"";
                //rptReport.DataDefinition.FormulaFields["credit"].Text = "\"" + dtHead.Rows[0]["credit"].ToString() + "\"";
                //rptReport.DataDefinition.FormulaFields["SummaCrPlan"].Text = "\"" + summaCrPlan.ToString("### ### ##0.00") + "\"";
                //rptReport.DataDefinition.FormulaFields["SummaCrFact"].Text = "\"" + summaCrFact.ToString("### ### ##0.00") + "\"";

                crystalReportViewer1.ReportSource = rptReport;
                crystalReportViewer1.Refresh();
            }
        }

        private void PrintActVozvr()
        {
            if ((dtPrintResult != null) && (dtPrintResult.Rows.Count > 0))
            {
                rptActVozvr rptReport = new rptActVozvr();

                DataTable dtEq = new DataTable();
                if (dtEquipment != null)
                {
                    if (dtEquipment.Rows.Count > 0)
                    {
                        dtEq = dtEquipment.Copy();
                    }
                    else
                    {
                        dtEq.Columns.Add("equipment", typeof(string));
                        dtEq.Columns.Add("count", typeof(string));
                        DataRow new_dr = dtEq.NewRow();
                        new_dr["equipment"] = "__________";
                        new_dr["count"] = "___";
                        dtEq.Rows.Add(new_dr);
                        dtEq.AcceptChanges();
                    }
                }
                else
                {
                    dtEq.Columns.Add("equipment", typeof(string));
                    dtEq.Columns.Add("count", typeof(string));
                    DataRow new_dr = dtEq.NewRow();
                    new_dr["equipment"] = "__________";
                    new_dr["count"] = "___";
                    dtEq.Rows.Add(new_dr);
                    dtEq.AcceptChanges();
                }

                rptReport.SetDataSource((DataTable)dtEq);

                //---

                //переменные исходные

                string object_address = (dtPrintResult.DefaultView[0]["object_address"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["object_address"].ToString());
                string date_act = (dtPrintResult.DefaultView[0]["date_doc"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_doc"].ToString());
                string date_con = (dtPrintResult.DefaultView[0]["date_con"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["date_con"].ToString());
                
                string Landlord_post = (dtPrintResult.DefaultView[0]["Landlord_post"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_post"].ToString());
                string Landlord_FIO = (dtPrintResult.DefaultView[0]["Landlord_FIO"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_FIO"].ToString());
                string Landlord_type_full = (dtPrintResult.DefaultView[0]["Landlord_type_full"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_type_full"].ToString());
                string Landlord_name = (dtPrintResult.DefaultView[0]["Landlord_name"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_name"].ToString());
                string Landlord_type = (dtPrintResult.DefaultView[0]["Landlord_type"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_type"].ToString());

                string Tenant_type_full = (dtPrintResult.DefaultView[0]["Tenant_type_full"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_type_full"].ToString());
                string Tenant_name = (dtPrintResult.DefaultView[0]["Tenant_name"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_name"].ToString());
                string Tenant_post = (dtPrintResult.DefaultView[0]["Tenant_post"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_post"].ToString());
                string Tenant_FIO = (dtPrintResult.DefaultView[0]["Tenant_FIO"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_FIO"].ToString());
                string Tenant_type = (dtPrintResult.DefaultView[0]["Tenant_type"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_type"].ToString());
                
                string section = (dtPrintResult.DefaultView[0]["section"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["section"].ToString());
                string floors = (dtPrintResult.DefaultView[0]["floors"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["floors"].ToString());
                string object_area = (dtPrintResult.DefaultView[0]["object_area"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["object_area"].ToString());
                
                //переменные составные

                // -------------

                string Landlord_basement = ",";

                if (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length != 0)
                    Landlord_basement = ", действующего на основании "
                        + (dtPrintResult.DefaultView[0]["Landlord_basement"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_basement"].ToString());

                if (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length != 0)
                    Landlord_basement += (dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString().Trim().Length == 0 ? "" : " № " + dtPrintResult.DefaultView[0]["Landlord_number_base"].ToString());
                if (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length != 0)
                    Landlord_basement += " от " + (dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_date_base"].ToString());

                if (Landlord_basement != ",")
                    Landlord_basement += ",";

                string Tenant_basement = ",";

                if (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length != 0)
                    Tenant_basement = ", действующего на основании "
                        + (dtPrintResult.DefaultView[0]["Tenant_basement"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_basement"].ToString());

                if (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length != 0)
                    Tenant_basement += (dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString().Trim().Length == 0 ? "" : " № " + dtPrintResult.DefaultView[0]["Tenant_number_base"].ToString());
                if (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length != 0)
                    Tenant_basement += " от " + (dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_date_base"].ToString());

                if (Tenant_basement != ",")
                    Tenant_basement += ",";

                // -------------

                string arendodatel_str = Landlord_type_full + " " + Landlord_name;

                string arendodatel_str_full = "в лице "
                    + Landlord_post
                    + " " + Landlord_FIO + Landlord_basement
                    + " именуемое в дальнейшем «Арендодатель», и ";

                string arendator_str = Tenant_type_full + " " + Tenant_name;

                string arendator_str_full = "в лице "
                        + Tenant_post + " " + Tenant_FIO + Tenant_basement
                        + " именуемое в дальнейшем «Арендатор», составили настоящий Акт о нижеследующем:";

                string punkt1 = "1. Во исполнении Договора аренды объекта нежилого фонда, расположенного по адресу: "
                        + object_address + ", № "
                        + number + " от " + date_con
                        + ", заключенного между "
                        + Landlord_type + " " + Landlord_name + " («Арендодатель») и "
                        + Tenant_type + " " + Tenant_name + " («Арендатор»), "
                        + Tenant_type + " " + Tenant_name + " произвело перед подписанием настоящего Акта возврата объекта нежилого фонда, расположенного по адресу: "
                        + object_address + ", сек. №"
                        + section + " (" + floors + ") общей площадью "
                        + object_area + " кв.м., именуемое далее «Объект».";

                string count_lamps = "          - Имущества: светильники в кол-ве        "
                        + (dtPrintResult.DefaultView[0]["lamps_count"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["lamps_count"].ToString())
                        + " шт.";

                string count_phone = "          - Телефонные линии в количестве         "
                        + (dtPrintResult.DefaultView[0]["tel_count"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["tel_count"].ToString())
                        + " шт.";

                string FIO_arendodatel = (dtPrintResult.DefaultView[0]["Landlord_FIO2"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Landlord_FIO2"].ToString());

                string FIO_arendator = (dtPrintResult.DefaultView[0]["Tenant_FIO2"].ToString().Trim().Length == 0 ? " - " : dtPrintResult.DefaultView[0]["Tenant_FIO2"].ToString());

                rptReport.DataDefinition.FormulaFields["adress"].Text = "\"" + RQuotes(object_address) + "\"";
                //rptReport.DataDefinition.FormulaFields["num"].Text = "\"" + RQuotes(num) + "\"";
                rptReport.DataDefinition.FormulaFields["date_act"].Text = "\"" + RQuotes(date_act) + "\"";
                rptReport.DataDefinition.FormulaFields["arendodatel_str"].Text = "\"" + RQuotes(arendodatel_str) + "\"";
                rptReport.DataDefinition.FormulaFields["arendodatel_str_full"].Text = "\"" + RQuotes(arendodatel_str_full) + "\"";
                rptReport.DataDefinition.FormulaFields["arendator_str"].Text = "\"" + RQuotes(arendator_str) + "\"";
                rptReport.DataDefinition.FormulaFields["arendator_str_full"].Text = "\"" + RQuotes(arendator_str_full) + "\"";
                rptReport.DataDefinition.FormulaFields["punkt1"].Text = "\"" + RQuotes(punkt1) + "\"";
                rptReport.DataDefinition.FormulaFields["count_lamps"].Text = "\"" + RQuotes(count_lamps) + "\"";
                rptReport.DataDefinition.FormulaFields["count_phone"].Text = "\"" + RQuotes(count_phone) + "\"";
                rptReport.DataDefinition.FormulaFields["FIO_arendator"].Text = "\"" + RQuotes(FIO_arendator) + "\"";
                rptReport.DataDefinition.FormulaFields["FIO_arendodatel"].Text = "\"" + RQuotes(FIO_arendodatel) + "\"";

                //rptReport.DataDefinition.FormulaFields["date"].Text = "\"" + "Дата: " + DateTime.Parse(dtHead.Rows[0]["date"].ToString()).ToString("dd.MM.yyyy") + "\"";
                //rptReport.DataDefinition.FormulaFields["credit"].Text = "\"" + dtHead.Rows[0]["credit"].ToString() + "\"";
                //rptReport.DataDefinition.FormulaFields["SummaCrPlan"].Text = "\"" + summaCrPlan.ToString("### ### ##0.00") + "\"";
                //rptReport.DataDefinition.FormulaFields["SummaCrFact"].Text = "\"" + summaCrFact.ToString("### ### ##0.00") + "\"";

                crystalReportViewer1.ReportSource = rptReport;
                crystalReportViewer1.Refresh();
            }
        }

    }
}
