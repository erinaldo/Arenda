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


        #region "Печать УПД"
        private static string pathTemplateFile = "";
        private static string pathUnloadTemplateFile = "";
        private static string pathUnloadTemplateFile_tmp = "";
        public static void printUpd(DataTable dtHead, int id)
        {            
            DataTable dtBanks = _proc.GetLandlordTenantBank(id);
            DataTable dt = _proc.GetTenantAddInfo(id);

            pathTemplateFile = Application.StartupPath + @"\Report\TemplateTenant";// + ".xls";
            pathUnloadTemplateFile_tmp = Application.StartupPath + @"\NewReport_tmp";// + ".xls";
            pathUnloadTemplateFile = Application.StartupPath + @"\NewReport";// + ".xls";

            Nwuram.Framework.ToExcel.Report reportTemplate = new Nwuram.Framework.ToExcel.Report();

            #region "Шапка"

            reportTemplate.AddSingleValue("@FIOUnLoader", Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            reportTemplate.AddSingleValue("@dateUnLoad", $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");

            reportTemplate.AddSingleValue("@NameTenant", $"{ dtHead.Rows[0]["cName"]}");
            reportTemplate.AddSingleValue("@abbTypeOrg", $"{ dtHead.Rows[0]["type_abb"]}");


            reportTemplate.AddSingleValue("@INN", $"{dtHead.Rows[0]["INN"]}");
            reportTemplate.AddSingleValue("@base_name", $"{dtHead.Rows[0]["base_name"]}");            
            reportTemplate.AddSingleValue("@orgnip", $"{dt.Rows[0]["orgnip"]}");
            reportTemplate.AddSingleValue("@Number_basement", $"{ dtHead.Rows[0]["Number_basement"]}");
            reportTemplate.AddSingleValue("@Date_basement", $"{ dtHead.Rows[0]["Date_basement"]}");

            reportTemplate.AddSingleValue("@namePost", $"{ dtHead.Rows[0]["namePost"]}");
            reportTemplate.AddSingleValue("@Contact_Firstname", $"{ dtHead.Rows[0]["Contact_Firstname"]}");
            reportTemplate.AddSingleValue("@Contact_Lastname_Par", $"{ dtHead.Rows[0]["Contact_Lastname_Par"]}");
            reportTemplate.AddSingleValue("@Contact_Lastname", $"{ dtHead.Rows[0]["Contact_Lastname"]}");
            reportTemplate.AddSingleValue("@Contact_Middlename", $"{ dtHead.Rows[0]["Contact_Middlename"]}");
            reportTemplate.AddSingleValue("@Sex", $"{ ((bool)dtHead.Rows[0]["Sex"] ? "М" : "Ж")}");


            reportTemplate.AddSingleValue("@Work_phone", $"{ dtHead.Rows[0]["Work_phone"]}");
            reportTemplate.AddSingleValue("@Home_phone", $"{ dtHead.Rows[0]["Home_phone"]}");
            reportTemplate.AddSingleValue("@Mobile_phone", $"{ dtHead.Rows[0]["Mobile_phone"]}");


            reportTemplate.AddSingleValue("@Who_is_Registered", $"{ dtHead.Rows[0]["Who_is_Registered"]}");
            reportTemplate.AddSingleValue("@DateRegistration", $"{ dtHead.Rows[0]["DateRegistration"]}");
            reportTemplate.AddSingleValue("@RegistrationNumber", $"{ dtHead.Rows[0]["RegistrationNumber"]}");
            reportTemplate.AddSingleValue("@Number_of_Certificate", $"{ dtHead.Rows[0]["Number_of_Certificate"]}");
            reportTemplate.AddSingleValue("@Series_od_Certificate", $"{ dtHead.Rows[0]["Series_od_Certificate"]}");


            reportTemplate.AddSingleValue("@Who_put_on_Account", $"{ dtHead.Rows[0]["Who_put_on_Account"]}");
            reportTemplate.AddSingleValue("@Number_Accounting", $"{ dtHead.Rows[0]["Number_Accounting"]}");
            reportTemplate.AddSingleValue("@Series_of_Accounting", $"{ dtHead.Rows[0]["Series_of_Accounting"]}");


            reportTemplate.AddSingleValue("@nameObjectLease", $"{ dtHead.Rows[0]["nameObjectLease"]}");
            reportTemplate.AddSingleValue("@Address_trade_premises", $"{ dtHead.Rows[0]["Address_trade_premises"]}");


            reportTemplate.AddSingleValue("@Address", $"{ dtHead.Rows[0]["Address"]}");
            reportTemplate.AddSingleValue("@Remark", $"{ dtHead.Rows[0]["Remark"]}");

            reportTemplate.AddSingleValue("@outReport", $"{ ((bool)dtHead.Rows[0]["outReport"] ? "Да" : "Нет")}");
            reportTemplate.AddSingleValue("@Vat_Nds", $"{ ((bool)dtHead.Rows[0]["Vat_Nds"] ? "Да" : "Нет")}");
            reportTemplate.AddSingleValue("@Path", $"{ dtHead.Rows[0]["Path"]}");

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
        #endregion

    }
}
