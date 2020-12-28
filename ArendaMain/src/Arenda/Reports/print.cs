using Nwuram.Framework.Settings.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arenda.Reports
{
    class print
    {
        readonly static Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);


        #region "Печать реквизитов по арендодателю"
        private static string pathTemplateFile = "";
        private static string pathUnloadTemplateFile = "";
        private static string pathUnloadTemplateFile_tmp = "";
        private static Nwuram.Framework.ToExcel.Report reportTemplate;
        public static void printUpd(DataTable dtHead, int id)
        {            
            DataTable dtBanks = _proc.GetLandlordTenantBank(id);
            DataTable dt = _proc.GetTenantAddInfo(id);

            pathTemplateFile = Application.StartupPath + @"\Report\TemplateTenant";// + ".xls";
            pathUnloadTemplateFile_tmp = Application.StartupPath + @"\ReportFinish\NewReport_tmp";// + ".xls";
            pathUnloadTemplateFile = Application.StartupPath + @"\ReportFinish\NewReport";// + ".xls";


            if (!File.Exists(pathTemplateFile + ".xls"))
            {
                MessageBox.Show("Нет шаблона для выгрузки реквизитов", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if(!Directory.Exists(Application.StartupPath + @"\ReportFinish\"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\ReportFinish\");
            }

            reportTemplate = new Nwuram.Framework.ToExcel.Report();

            #region "Шапка"

            AddSingleValue("@FIOUnLoader", Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            AddSingleValue("@dateUnLoad", $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");

            AddSingleValue("@NameTenant", $"{ dtHead.Rows[0]["cName"]}");
            AddSingleValue("@abbTypeOrg", $"{ dtHead.Rows[0]["type_abb"]}");


            AddSingleValue("@INN", $"{dtHead.Rows[0]["INN"]}");
            AddSingleValue("@base_name", $"{dtHead.Rows[0]["base_name"]}");
            if (dt == null || dt.Rows.Count == 0) AddSingleValue("@orgnip", "."); else AddSingleValue("@orgnip", $"{dt.Rows[0]["orgnip"]}");
            AddSingleValue("@Number_basement", $"{ dtHead.Rows[0]["Number_basement"]}");
            AddSingleValue("@Date_basement", $"{ dtHead.Rows[0]["Date_basement"]}");

            AddSingleValue("@namePost", $"{ dtHead.Rows[0]["namePost"]}");
            AddSingleValue("@Contact_Firstname", $"{ dtHead.Rows[0]["Contact_Firstname"]}");
            AddSingleValue("@Contact_Lastname_Par", $"{ dtHead.Rows[0]["Contact_Lastname_Par"]}");
            AddSingleValue("@Contact_Lastname", $"{ dtHead.Rows[0]["Contact_Lastname"]}");
            AddSingleValue("@Contact_Middlename", $"{ dtHead.Rows[0]["Contact_Middlename"]}");
            AddSingleValue("@Sex", $"{ ((bool)dtHead.Rows[0]["Sex"] ? "М" : "Ж")}");


            AddSingleValue("@Work_phone", $"{ dtHead.Rows[0]["Work_phone"]}");
            AddSingleValue("@Home_phone", $"{ dtHead.Rows[0]["Home_phone"]}");
            AddSingleValue("@Mobile_phone", $"{ dtHead.Rows[0]["Mobile_phone"]}");


            AddSingleValue("@Who_is_Registered", $"{ dtHead.Rows[0]["Who_is_Registered"]}");
            AddSingleValue("@DateRegistration", $"{ dtHead.Rows[0]["DateRegistration"]}");
            AddSingleValue("@RegistrationNumber", $"{ dtHead.Rows[0]["RegistrationNumber"]}");
            AddSingleValue("@Number_of_Certificate", $"{ dtHead.Rows[0]["Number_of_Certificate"]}");
            AddSingleValue("@Series_od_Certificate", $"{ dtHead.Rows[0]["Series_od_Certificate"]}");


            AddSingleValue("@Who_put_on_Account", $"{ dtHead.Rows[0]["Who_put_on_Account"]}");
            AddSingleValue("@Number_Accounting", $"{ dtHead.Rows[0]["Number_Accounting"]}");
            AddSingleValue("@Series_of_Accounting", $"{ dtHead.Rows[0]["Series_of_Accounting"]}");


            AddSingleValue("@nameObjectLease", $"{ dtHead.Rows[0]["nameObjectLease"]}");
            AddSingleValue("@Address_trade_premises", $"{ dtHead.Rows[0]["Address_trade_premises"]}");


            AddSingleValue("@Address", $"{ dtHead.Rows[0]["Address"]}");
            AddSingleValue("@Remark", $"{ dtHead.Rows[0]["Remark"]}");

            AddSingleValue("@outReport", $"{ ((bool)dtHead.Rows[0]["outReport"] ? "Да" : "Нет")}");
            AddSingleValue("@Vat_Nds", $"{ ((bool)dtHead.Rows[0]["Vat_Nds"] ? "Да" : "Нет")}");
            AddSingleValue("@Path", $"{ dtHead.Rows[0]["Path"]}");

            //reportTemplate.AddSingleValue("@", $"{ dtHead.Rows[0][""]}");
            //reportTemplate.AddSingleValue("", $"{ dtHead.Rows[0][""]}");

            #endregion

            #region "Тело"
            if (dtBanks != null && dtBanks.Rows.Count > 0)
            {
                dtBanks.Columns.Remove("id");
                dtBanks.Columns.Remove("id_Bank");
                dtBanks.Columns.Remove("isActive");

                reportTemplate.AddMultiValues(dtBanks, "_");
            }
            else if (dtBanks != null)
            {
                dtBanks.Columns.Remove("id");
                dtBanks.Columns.Remove("id_Bank");
                dtBanks.Columns.Remove("isActive");
                dtBanks.Rows.Add(".", ".", ".", ".");
                reportTemplate.AddMultiValues(dtBanks, "_");
            }
            #endregion

            try
            {
                if (reportTemplate.CreateTemplate(pathTemplateFile, pathUnloadTemplateFile_tmp, null))
                {
                    //File.Copy(pathUnloadTemplateFile_tmp + ".xls", pathUnloadTemplateFile + "_" + id.ToString() + ".xls", true);
                    File.Copy(pathUnloadTemplateFile_tmp + ".xls", pathUnloadTemplateFile + "_" + id + ".xls", true);


                    reportTemplate.OpenFile(pathUnloadTemplateFile + "_" + id.ToString());
                }
            }
            catch
            {

            }

            try { File.Delete(pathUnloadTemplateFile_tmp + ".xls"); } catch { }
        }

        private static void AddSingleValue(string field, object value)
        {
            if (value == null || value == DBNull.Value || value.ToString().Trim().Length==0)
                reportTemplate.AddSingleValue(field, $".");
            else
                reportTemplate.AddSingleValue(field, $"{value}");
        }
        #endregion

    }
}
