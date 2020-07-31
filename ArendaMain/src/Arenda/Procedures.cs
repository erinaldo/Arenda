using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Data;
using Nwuram.Framework.Data;
using System.Windows.Forms;
using Nwuram.Framework.Logging;
using Nwuram.Framework;
using Nwuram.Framework.Settings.Connection;

namespace Arenda
{
    class Procedures : SqlProvider
    {
        ArrayList ap = new ArrayList();

        public Procedures(string server, string database, string username, string password, string appName)
            : base(server, database, username, password, appName)
        {
        }
        #region Здания
        public DataTable AddZdan(string cname, string abbr)
        {
            ap.Clear();
            ap.Add(cname);
            ap.Add(abbr);
            return executeProcedure("Arenda.AddZdan", new string[2] { "@cname", "@abbreviation" }, new DbType[2] { DbType.String, DbType.String }, ap);
        }


        public DataTable GetZdan(int isActive)
        {
            ap.Clear();
            ap.Add(isActive);
            return executeProcedure("Arenda.GetZdan",  new string[1]{"@isActive"}, new DbType[1] { DbType.Byte }, ap);
        }

        public DataTable ChgZdan(int id, string cname, string abbr, int isActive)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(cname);
            ap.Add(abbr);
            ap.Add(isActive);
            return executeProcedure("Arenda.ChgZdan", new string[4] { "@id", "@cname", "@abbreviation", "@isActive" }, new DbType[4] { DbType.Int32, DbType.String, DbType.String, DbType.Int32 }, ap);
        }


        public DataTable delZdan(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.DelZdan", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable befDel(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.BefDel", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable isActive(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.isActive", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }

        #endregion
        #region Этажи

        public DataTable GetFloor(int isActive)
        {
            ap.Clear();
            ap.Add(isActive);
            return executeProcedure("Arenda.GetFloor", new string[1] { "@isActive" }, new DbType[1] { DbType.Byte }, ap);
        }

        public DataTable AddFloor(string cname, string abbr)
        {
            ap.Clear();
            ap.Add(cname);
            ap.Add(abbr);
            return executeProcedure("Arenda.AddFloor", new string[2] { "@cname", "@abbr" }, new DbType[2] { DbType.String, DbType.String }, ap);
        }

        public DataTable ChgFloor(int id, string cname, string abbr, int isActive)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(cname);
            ap.Add(abbr);
            ap.Add(isActive);
            return executeProcedure("Arenda.ChgFloor", new string[4] { "@id", "@cname", "@abbreviation", "@isActive" }, new DbType[4] { DbType.Int32, DbType.String, DbType.String, DbType.Int32 }, ap);
        }


        public DataTable delFloor(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.DelFloor", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable befFloor(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.BefFloor", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable isActiveFloor(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.isActiveFloor", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }




        #endregion
        #region Типы помещений

        public DataTable GetTypePr(int isActive)
        {
            ap.Clear();
            ap.Add(isActive);
            return executeProcedure("Arenda.GetTypePr", new string[1] { "@isActive" }, new DbType[1] { DbType.Byte }, ap);
        }


        public DataTable AddTypePr(string cname)
        {
            ap.Clear();
            ap.Add(cname);
            return executeProcedure("Arenda.AddTypePr", new string[1] { "@cname" }, new DbType[1] { DbType.String }, ap);
        }

        public DataTable ChgTypePr(int id, string cname, int isActive)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(cname);
            ap.Add(isActive);
            return executeProcedure("Arenda.ChgTypePr", new string[3] { "@id", "@cname",  "@isActive" }, new DbType[3] { DbType.Int32, DbType.String, DbType.Int32 }, ap);
        }


        public DataTable delTypePr(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.DelTypePr", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable befTypePr(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.BefTypePr", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable isActiveTypePr(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.isActiveTypePr", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }


        #endregion
        #region Оборудование


        public DataTable GetEquip(int isActive)
        {
            ap.Clear();
            ap.Add(isActive);
            return executeProcedure("Arenda.GetEquip", new string[1] { "@isActive" }, new DbType[1] { DbType.Byte }, ap);
        }

        public int AddEquip(string cname, string abbr)
        {
            int res = 0;

            ap.Clear();
            ap.Add(cname);
            ap.Add(abbr);
            DataTable dt = new DataTable();
            dt = executeProcedure("Arenda.AddEquip", new string[2] { "@cname", "@abbreviation" }, new DbType[2] { DbType.String, DbType.String }, ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                res = int.Parse(dt.Rows[0][0].ToString());
            }

            return res;
        }

        public DataTable ChgEquip(int id, string cname, string abbr, int isActive)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(cname);
            ap.Add(abbr);
            ap.Add(isActive);
            return executeProcedure("Arenda.ChgEquip", new string[4] { "@id", "@cname", "@abbreviation", "@isActive" }, new DbType[4] { DbType.Int32, DbType.String, DbType.String, DbType.Int32 }, ap);
        }


        public DataTable delEquip(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.DelEquip", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable befEquip(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.BefEquip", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable isActiveEquip(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.isActiveEquip", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }


        #endregion
        public DataTable CheakAll(string cname,  string prz, int id = 0, int id_Obj = 0)
        {
            ap.Clear();
            ap.Add(cname);
            ap.Add(prz);
            ap.Add(id);
            ap.Add(id_Obj);

            return executeProcedure("Arenda.CheakALL",
              new string[4] { "@cname", "@prz", "@id", "@id_Obj" },
              new DbType[4] { DbType.String, DbType.String, DbType.Int32, DbType.Int32 }, ap);
        }
      
      public DataTable CheckPosts(int id, string cname)
      {
        ap.Clear();
        ap.Add(id);
        ap.Add(cname);
        return executeProcedure("Arenda.CheckPosts",
          new string[2] { "@id", "@cName" },
          new DbType[2] { DbType.Int32, DbType.String }, ap);
      }

        #region Справочник оборудования и секции
        public DataTable FillCbEq()
        {
            ap.Clear();
            return executeProcedure("Arenda.FillCbEq", new string[0] {  }, new DbType[0] { }, ap);
        }

        public int AddEditEqVsSec(int sec, int id_eq, int count, int mode, int id)
        {
            int idEqVsSec = 0;

            ap.Clear();
            ap.Add(sec);
            ap.Add(id_eq);
            ap.Add(count);
            ap.Add(mode);
            ap.Add(id);
            DataTable dt = new DataTable();
            dt = executeProcedure("Arenda.AddEditEqVsSec", 
                new string[5] { "@Sec", "@Eq", "@Count", "@mode", "@id" },
                new DbType[5] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32 }, ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                idEqVsSec = int.Parse(dt.Rows[0][0].ToString());
            }

            return idEqVsSec;
        }

        public DataTable AddEditSec(string cname, string bl, string fl, int mode,
          int id, int isActive, int? lamp , int? tl, string phn, decimal total_area,
          decimal area_trade, int id_Obj = 0)
        {
            System.Data.DbType i,y,u;

            ap.Clear();
            ap.Add(cname);
            ap.Add(bl);
            ap.Add(fl);
            ap.Add(mode);
            ap.Add(id);
            ap.Add(isActive);
            ap.Add(lamp);
            ap.Add(tl);
            ap.Add(phn);
            ap.Add(total_area);
            ap.Add(area_trade);
            ap.Add(id_Obj);
            if (lamp == null)
            { i = DbType.Boolean; }
            else i = DbType.Int32;

            if (tl == null)
            { y = DbType.Boolean; }
            else y = DbType.Int32;

            if (phn == null)
            { u = DbType.Boolean; }
            else u = DbType.String;

            return executeProcedure("Arenda.AddEditSec",
              new string[12] { "@cName", "@id_bl", "@id_fl", "@mode", "@id",
                "@isActive", "@lamps", "@telephone_lines", "@phone_number",
                "@Total_Area", "@Area_of_Trading_Hall", "@id_Obj" },
                new DbType[12] { DbType.String, DbType.String, DbType.String,
                  DbType.Int32, DbType.Int32, DbType.Int32, i, y, u, DbType.Decimal,
                  DbType.Decimal, DbType.Int32 }, ap);
        }

        public int AddEditSec2(string cname, string bl, string fl, int mode, int id,
          int isActive, int? lamp, int? tl, string phn, decimal total_area,
          decimal area_trade, bool isAPPZ, int id_Obj)
        {
            int resultId = 0;

            System.Data.DbType i, y, u;

            ap.Clear();
            ap.Add(cname);
            ap.Add(bl);
            ap.Add(fl);
            ap.Add(mode);
            ap.Add(id);
            ap.Add(isActive);
            ap.Add(lamp);
            ap.Add(tl);
            ap.Add(phn);
            ap.Add(total_area);
            ap.Add(area_trade);
            ap.Add(isAPPZ);
            ap.Add(id_Obj);
            
            if (lamp == null)
            { i = DbType.Boolean; }
            else i = DbType.Int32;

            if (tl == null)
            { y = DbType.Boolean; }
            else y = DbType.Int32;

            if (phn == null)
            { u = DbType.Boolean; }
            else u = DbType.String;

            DataTable dt = new DataTable();

            dt = executeProcedure("Arenda.AddEditSec",
                new string[] { "@cName", "@id_bl", "@id_fl", "@mode", "@id",
                  "@isActive", "@lamps", "@telephone_lines", "@phone_number",
                  "@Total_Area", "@Area_of_Trading_Hall", "@isAPPZ", "@id_Obj" }, 
                new DbType[] { DbType.String, DbType.String, DbType.String,
                  DbType.Int32, DbType.Int32, DbType.Int32, i, y, u, DbType.Decimal,
                  DbType.Decimal, DbType.Boolean, DbType.Int32 }, ap);

            if ((dt!=null) && (dt.Rows.Count>0))
            {
                resultId = int.Parse(dt.Rows[0][0].ToString());
            }

            return resultId;
        }

        public DataTable FillCbZdFl(int mode)
        {
            ap.Clear();
            ap.Add(mode);
            return executeProcedure("Arenda.FillCbZdFl", new string[1] {"@mode" }, new DbType[1] {DbType.Int32 }, ap);
        }

        public DataTable FillCbZdFl(int mode, int id_build)
        {
            ap.Clear();
            ap.Add(mode);
            ap.Add(id_build);
            return executeProcedure("Arenda.FillCbZdFl", new string[2] { "@mode", "@id_build" }, new DbType[2] { DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable GetSec(int isActive)
        {
            ap.Clear();
            ap.Add(isActive);
            return executeProcedure("Arenda.GetSec", new string[1] { "@isActive" }, new DbType[1] {  DbType.Int32 }, ap);
        }


        public DataTable GetEqVsSec(string cName, int mode)
        {
            ap.Clear();
            ap.Add(cName);
            ap.Add(mode);
            return executeProcedure("Arenda.GetEqVsSec", new string[2] { "@cName","@mode" }, new DbType[2] { DbType.String, DbType.Int32 }, ap);
        }

        public DataTable isActiveSec(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.isActiveSec", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable delSec(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.DelSec", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable BefSec(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.BefSec", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable CheakEVS(string sec, string item, int count)
        { 
            ap.Clear();
            ap.Add(sec);
            ap.Add(item);
            ap.Add(count);
            return executeProcedure("Arenda.CheakEVS", new string[3] { "@sec", "@item", "@count" }, new DbType[3] { DbType.String, DbType.String, DbType.Int32 }, ap);
        }

        public DataTable DelEVS(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.DelEVS", new string[1] { "@id"}, new DbType[1] { DbType.Int32 }, ap);
        
        }

        public DataTable CheakSec(string cname, /*string build, string floor,*/ int id_Obj)
        {
            ap.Clear();
            ap.Add(cname);
            //ap.Add(build);
            //ap.Add(floor);
            ap.Add(id_Obj);
            return executeProcedure("Arenda.CheakSec",
              new string[] { "@cName", /*"@id_b", "@id_f",*/ "@id_Obj" },
              new DbType[] { DbType.String, /*DbType.String, DbType.String,*/ DbType.Int32 },
              ap);
        }

        #endregion
        #region Типы организаций
        public DataTable AddEditType_o_o(string cName, string abbr,int id, int mode, int isActive)
        {
            ap.Clear();
            ap.Add(cName);
            ap.Add(abbr);
            ap.Add(id);
            ap.Add(mode);
            ap.Add(isActive);

            return executeProcedure("Arenda.AddEditType_o_o", new string[5] { "@cName", "@abbr", "@id", "@mode", "@isActive" }, new DbType[5] { DbType.String, DbType.String, DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable GetTOO(int isActive)
        {
            ap.Clear();
            ap.Add(isActive);
            return executeProcedure("Arenda.GetTOO", new string[1] { "@isActive" }, new DbType[1] { DbType.Int32 }, ap);
        }
        public DataTable BefTOO(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.BefTOO", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable delTOO(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.delTOO", new string[1] { "@id" }, new DbType[1] { DbType.Int32}, ap);
        }
        #endregion
        #region Основания заключения договоров

        public DataTable AddEditBas(int id, string cName, string abbr, int mode, int isActive, int needDate)
        {
            ap.Clear();
            ap.Add(cName);
            ap.Add(abbr);
            ap.Add(id);
            ap.Add(mode);
            ap.Add(isActive);
            ap.Add(needDate);
            return executeProcedure("Arenda.AddEditBas", new string[6] { "@cName", "@abbr", "@id", "@mode", "@isActive", "@needDate" }, new DbType[6] { DbType.String, DbType.String, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable GetBas(int isActive)
        {
            ap.Clear();
            ap.Add(isActive);

            return executeProcedure("Arenda.GetBas", new string[1] {"@isActive" }, new DbType[1] { DbType.Int32 }, ap);
        }
        public DataTable BefBas(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.BefBas", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable DelBas(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.DelBas", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }
      
      public DataTable ChangeBasementActiveStatus(int id, bool isActive, bool used)
      {
        ap.Clear();
        ap.Add(id);
        ap.Add(isActive);
        ap.Add(used);

        return executeProcedure("Arenda.ChangeBasementActiveStatus",
          new string[] { "@id", "@IsActive", "@Used" },
          new DbType[] { DbType.Int32, DbType.Boolean, DbType.Boolean }, ap);
      }
        #endregion
        #region Арендаторы

        public DataTable GetTetant(int isActive)
        {
            ap.Clear();
            ap.Add(isActive);
            return executeProcedure("Arenda.GetTenant", new string[1] { "@isActive" }, new DbType[1] { DbType.Int32 }, ap);
        }



        #endregion
        #region Список договоров

        public DataTable GetListDoc()
        {
            ap.Clear();

            return executeProcedure("Arenda.GetListDoc", new string[0] { }, new DbType[0] { }, ap);
        }

        public DataTable GetLD(int id)
        {
            ap.Clear();
            ap.Add(id);

            return executeProcedure("Arenda.GetLD", new string[1] { "@id"}, new DbType[1] { DbType.Int32 }, ap);
        }

        public DateTime getdate()
        {
            DataTable dt = executeProcedure("Arenda.GetDate", new string[0] { }, new DbType[0] { }, ap);
            if (dt.Rows.Count == 0)
            {
                return DateTime.Parse(DateTime.Now.Date.ToShortDateString());
            }
            else
            {
                return DateTime.Parse(DateTime.Parse(dt.Rows[0][0].ToString()).ToShortDateString());
            }
        }

        public DateTime getdatetime()
        {
            DataTable dt = executeProcedure("Arenda.GetDate", new string[0] { }, new DbType[0] { }, ap);
            if (dt.Rows.Count == 0)
            {
                return DateTime.Now;
            }
            else
            {
                return DateTime.Parse(dt.Rows[0][0].ToString());
            }
        }

        public DataTable AddEditLD(int id,
            int id_ten,
            int id_lord,
            string agr,
            DateTime doc,
            DateTime startdate,
            DateTime stopdate,
            int? build,
            int? floor,
            int sec,
            int? tp,
            decimal totalarea,
            decimal totalareaoftrade,
            decimal costofmetr,
            decimal phone,
            decimal totalsum,
            int payment,
            string remark,
            decimal reklama,
            decimal ReklLength,
            decimal ReklWidth,
            decimal ReklArea,
            int ReklNumber,
            string failComment,
            int id_TypeDog,
            string KadNum,
            int idObj,
            int RentalVacation,
            int? id_SavePayment)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_ten);
            ap.Add(id_lord);
            ap.Add(agr);
            ap.Add(doc);
            ap.Add(startdate);
            ap.Add(stopdate);
            ap.Add(build);
            ap.Add(floor);
            ap.Add(sec);
            ap.Add(tp);
            ap.Add(totalarea);
            ap.Add(totalareaoftrade);
            ap.Add(costofmetr);
            ap.Add(phone);
            ap.Add(totalsum);
            ap.Add(payment);
            ap.Add(remark);
            ap.Add(reklama);
            ap.Add(ReklLength);
            ap.Add(ReklWidth);
            ap.Add(ReklArea);
            ap.Add(ReklNumber);
            ap.Add(failComment);
            ap.Add(id_TypeDog);
            ap.Add(KadNum);
            ap.Add(idObj);
            ap.Add(RentalVacation);
            ap.Add(id_SavePayment);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);


            return executeProcedure("Arenda.AddEditLD",
                new string[] { 
                    "@id",
                    "@ten",
                    "@lord",
                    "@agreement",
                    "@Date_of_Conclusion",
                    "@Start_Date",
                    "@Stop_Date",
                    "@build",
                    "@Floor",
                    "@Section",
                    "@Type_of_Premises",
                    "@Total_Area",
                    "@Area_of_Trading_Hall",
                    "@Cost_of_Meter",
                    "@Phone", 
                    "@Total_Sum",
                    "@Payment_Type",  
                    "@Remark", 
                    "@Reklama", 
                    "@ReklLength",
                    "@ReklWidth", 
                    "@ReklArea", 
                    "@ReklNumber", 
                    "@failComment",
                    "@id_TypeDog", 
                    "@KadNum", 
                    "@id_obg",
                    "@RentalVacation",
                    "@id_SavePayment",
                    "@id_user"},
                new DbType[] { 
                    DbType.Int32,
                    DbType.Int32,
                    DbType.Int32,
                    DbType.String,
                    DbType.DateTime,
                    DbType.DateTime,
                    DbType.DateTime,
                    DbType.Int32,
                    DbType.Int32,
                    DbType.Int32, 
                    DbType.Int32,   
                    DbType.Decimal, 
                    DbType.Decimal, 
                    DbType.Decimal, 
                    DbType.Decimal,
                    DbType.Decimal, 
                    DbType.Int32,  
                    DbType.String, 
                    DbType.Decimal,
                    DbType.Decimal, 
                    DbType.Decimal, 
                    DbType.Decimal, 
                    DbType.Int32,
                    DbType.String, 
                    DbType.Int32, 
                    DbType.String, 
                    DbType.Int32,
                    DbType.Int32, 
                    DbType.Int32, 
                    DbType.Int32
                }, ap);
        }

        public DataTable CheckDogNum(int id, string num)
        {
          ap.Clear();
          ap.Add(id);
          ap.Add(num);

          return executeProcedure("Arenda.CheckDogNum",
            new string[] { "@id", "@cNum" },
            new DbType[] { DbType.Int32, DbType.String }, ap);
        }

        public DataTable GetTD(int id)

        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.GetTD", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        
        }

        public DataTable AddeditTD(int id, int id_agreements, DateTime datedoc, int id_typedoc, int? number,
            DateTime? daterenewal, decimal? Total_Area, DateTime? departureDate)
        {
            DbType x, w, depDateType, numType;

            ap.Clear();
            ap.Add(id);
            ap.Add(id_agreements);
            ap.Add(datedoc);
            ap.Add(id_typedoc);
            ap.Add(number);
            ap.Add(daterenewal);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(Total_Area);
            ap.Add(departureDate);            

            if (number == null)
                numType = DbType.Boolean;
            else numType = DbType.Int32;

            if (daterenewal == null)
                x = DbType.Boolean;
            else x = DbType.DateTime;

            if (Total_Area == null)
                w = DbType.Boolean;
            else w = DbType.Decimal;

            if (departureDate == null)
                depDateType = DbType.Boolean;
            else depDateType = DbType.DateTime;
                         
                                                                                                                                                                                                                                                       //"@id",        "@id_agr",    "@datedoc",    "@id_type_doc", "@number",   "@isActive" ,   "@daterenewal", "@id_creator", "@datecreate", "@id_editors", "@dateedit",    "@mode"
          return  executeProcedure("Arenda.AddEditTD",
              new string[] { "@id", "@id_agr", "@datedoc", "@id_type_doc", "@number", 
                             "@daterenewal", "@id_user", "@Total_Area", "@DepartureDate" },
              new DbType[] { DbType.Int32, DbType.Int32, DbType.DateTime, DbType.Int32, numType, 
                             x, DbType.Int32, w, depDateType }, ap);
        
        }

        #endregion 
        public DataTable FillCbLord()
        {
            ap.Clear();

            return executeProcedure("Arenda.FillCbLord", new string[0] { }, new DbType[0] { }, ap);
        }
        public DataTable FillCbTen(int prz)
        {
            ap.Clear();
            ap.Add(prz);

            return executeProcedure("Arenda.FillCbTen", new string[1] {"@prz" }, new DbType[1] { DbType.Int32}, ap);
        }
        public DataTable FillCbSecTp(int mode)
        {
            ap.Clear();
            ap.Add(mode);
            return executeProcedure("Arenda.FillCbSecTp", new string[1] { "@mode" }, new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable FillCbSecTp(int mode, int id_building, int id_floor, int id_obj)
        {
            ap.Clear();
            ap.Add(mode); 
            ap.Add(id_building);            
            ap.Add(id_floor);
            ap.Add(id_obj);
            return executeProcedure("Arenda.FillCbSecTp",
              new string[4] { "@mode", "@id_building", "@id_floor", "@id_obj" },
              new DbType[4] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32 },
              ap);
        }

        public DataTable FillCbToo()
        {
            ap.Clear();

            return executeProcedure("Arenda.FillCbToo", new string[0] { }, new DbType[0] { }, ap);
        }
        public DataTable FillCbBas()
        {
            ap.Clear();

            return executeProcedure("Arenda.FillCbBas", new string[0] { }, new DbType[0] { }, ap);
        }
        public DataTable FillCbTD()
        {
            ap.Clear();

            return executeProcedure("Arenda.FillCbTD", new string[0] { }, new DbType[0] { }, ap);
        }
        public DataTable DelDTL(int id, string prz)

        {
            ap.Clear();
            ap.Add(id);
            ap.Add(prz);
            return executeProcedure("Arenda.DelDTL", new string[2] {"@id","@prz" }, new DbType[2] { DbType.Int32, DbType.String}, ap);
        }
        #region Арендодатели

        public DataTable GetLandLord(int isActive)
        {
            ap.Clear();
            ap.Add(isActive);
            return executeProcedure("Arenda.GetLandlord", new string[1] { "@isActive" }, new DbType[1] { DbType.Int32 }, ap);
        }

        #endregion
        public DataTable getLT(int id)
        {
            ap.Clear();
            ap.Add(id);

            return executeProcedure("Arenda.GetLT", new string[1]{"@tid"}, new DbType[1]{DbType.Int32}, ap);
  
        }
        public DataTable BefLordTen(int id, string prz)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(prz);
            return executeProcedure("Arenda.BefLordTen", new string[2] { "@id", "@prz" }, new DbType[2] { DbType.Int32, DbType.String }, ap);
        }

        public DataTable Active (int id, int active)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(active);
            return executeProcedure("Arenda.Active", new string[2] { "@id", "@isActive" }, new DbType[2] { DbType.Int32, DbType.Int32}, ap);
        }
        public int addedintLT(
                int id, string type, string cName, string name, string otc, 
                string fam, string fam_par, int sex, string wphone, string hphone, 
                string mphone, string adress, int id_bank, string pa, string okpo, 
                string kpp, string inn, int? id_basment, string WiS, DateTime? dateReg,
                string regNum, string numCert, string serCer, string WPON, string numAcc, 
                string serAcc, bool nds, int slt, string remark, 
                int mode, string numbase, DateTime? datebas, int id_Posts, string Adress_trade, 
                bool outReport, int id_obj, string path, string email,string factAdress)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(type);
            ap.Add(cName);
            ap.Add(name);
            ap.Add(otc);
            ap.Add(fam);
            ap.Add(fam_par);
            ap.Add(sex);
            ap.Add(wphone);
            ap.Add(hphone);
            ap.Add(mphone);
            ap.Add(adress);
            ap.Add(id_bank);
            ap.Add(pa);
            ap.Add(okpo);
            ap.Add(kpp);
            ap.Add(inn);
            ap.Add(id_basment);
            ap.Add(WiS);
            ap.Add(dateReg);
            ap.Add(regNum);
            ap.Add(numCert);
            ap.Add(serCer);
            ap.Add(WPON);
            ap.Add(numAcc);
            ap.Add(serAcc);
            ap.Add(nds);
            ap.Add(slt);
            ap.Add(remark);
            ap.Add(mode);
            ap.Add(numbase);
            ap.Add(datebas);
            ap.Add(id_Posts);
            ap.Add(Adress_trade);
            ap.Add(outReport);
            ap.Add(id_obj);
            ap.Add(path);
            ap.Add(email);
            ap.Add(factAdress);

            DataTable dt = executeProcedure("Arenda.AddEditTL", 
                new string[] { "@id", "@type", "@cName", "@name", "@otc", 
                               "@fam", "@fam_par", "@sex", "@wphone", "@hphone", 
                               "@mphone", "@adress", "@id_bank", "@pa", "@okpo", 
                               "@kpp", "@inn", "@id_basment", "@WiS", "@dateReg", 
                               "@regNum", "@numCert", "@serCer", "@WPON", "@numAcc", 
                               "@serAcc", "@nds", "@slt", "@remark", 
                               "@mode", "@numofbas", "@datebas", "@id_Posts", "@adress_trade",
                               "@outReport", "@id_obj", "@path", "@email","@factAdress"},
                               
                                 //  "@id",       "@type",      "@cName",      "@name",      "@otc",       
                new DbType[] { DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.String, 
                                 //"@fam",          @fam_par    "@sex",        "@wphone",    "@hphone",     
                                 DbType.String, DbType.String, DbType.Int32, DbType.String, DbType.String, 
                                 //"@mphone",      "@adress",    "@id_bank",    "@pa",        "@okpo",        
                                 DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, 
                                 //"@kpp",        "@inn",       "@id_basment", "@WiS",        "@dateReg",     
                                 DbType.String, DbType.String, DbType.Int32, DbType.String, DbType.DateTime, 
                                 //"@regNum",      "@numCert",   "@serCer",     "@WPON",       "@numAcc"                                 
                                 DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, 
                                 //"@serAcc",     "@nds",       "@slt",       "@remark",     
                                 DbType.String, DbType.Boolean, DbType.Int32, DbType.String, 
                                 //"@mode",     "@numofbas",    "@datebas",     "@id_Posts", "@adress_trade"
                                 DbType.Int32, DbType.String, DbType.DateTime, DbType.Int32, DbType.String,
                                 //"@outReport", "@id_obj"      "@path"         "@email"    "@factAdress"
                                 DbType.Boolean, DbType.Int32, DbType.String, DbType.String,DbType.String}, ap);
            return dt != null && dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["id"]) : 0;
        }
        public DataTable CheakLT(string cName, string INN, string too, string area_trade)
        {
            ap.Clear();
            ap.Add(cName);
            ap.Add(INN);
            ap.Add(too);
            ap.Add(area_trade);
            return executeProcedure("Arenda.CheakLT", new string[4] { "@cName", "@INN", "@type", "@area_trade" }, new DbType[4] { DbType.String, DbType.String, DbType.String , DbType.String}, ap);
        }

        public DataTable GetDocById(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.GetDocById", new string[1] {"@id"} , new DbType[1]{DbType.Int32},ap);        
        }

        public DataTable GetPrintData(int id)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(ConnectionSettings.GetIdProgram());
            return executeProcedure("Arenda.GetPrintData",
                new string[2] { "@id", "@id_prog" },
                new DbType[2] { DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable GetPrintDataRekl(int id)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(ConnectionSettings.GetIdProgram());
            return executeProcedure("Arenda.GetPrintDataRekl",
                new string[2] { "@id", "@id_prog" },
                new DbType[2] { DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable GetPrintDataZem(int id)
        {
          ap.Clear();
          ap.Add(id);
          ap.Add(ConnectionSettings.GetIdProgram());
          return executeProcedure("Arenda.GetPrintDataZem",
            new string[2] { "@id", "@id_prog" },
            new DbType[2] { DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable GetPrintDataAct(int id, int id_actpriema)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(ConnectionSettings.GetIdProgram());
            ap.Add(id_actpriema);

            return executeProcedure("Arenda.GetPrintDataAct",
                new string[] { "@id", "@id_prog", "@id_actpriema" },
                new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable GetPrintDataActVozvr(int id, int id_vozvr)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(ConnectionSettings.GetIdProgram());
            ap.Add(id_vozvr);

            return executeProcedure("Arenda.GetPrintDataActVozvr",
                new string[] { "@id", "@id_prog", "@id_vozvr" },
                new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable GetPrintDataDopSogl(int id, int id_dopsogl)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_dopsogl);

            return executeProcedure("Arenda.GetPrintDataDopSogl",
                new string[] { "@id", "@id_dopsogl" },
                new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable GetAdditionDocs(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.GetAdditionDocs", 
                new string[1] { "@id" }, 
                new DbType[1] { DbType.Int32 }, ap);
        }

        
        public DataTable GetPrintDataActEquipment(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.GetPrintDataActEquipment", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }
        

        /// <summary>
        /// Получение списка действующих должностей
        /// </summary>
        /// <returns></returns>
        public DataTable GetArendaPosts()
        {
            return executeCommand("[Arenda].[GetArendaPosts]");
        }


        public DataTable addeditPost(int id, string cname, int isActive, string Dative_case)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(cname);            
            ap.Add(isActive);
            ap.Add(Dative_case);
            return executeProcedure("[Arenda].[addeditPost]",
                    new string[4] { "@id", "@cName", "@isActive", "Dative_case" },
                    new DbType[4] { DbType.Int32, DbType.String, DbType.Int32, DbType.String }, ap);
        }

       

        #region Банки
        public DataTable getBank()
        {
            ap.Clear();
            return executeProcedure("Arenda.getBanks", new string[0] { }, new DbType[0] { }, ap);
        }
        public DataTable addeditBank(int id, string cName , string ca, string bik, int mode,int isActive)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(cName);
            ap.Add(ca);
            ap.Add(bik);
            ap.Add(mode);
            ap.Add(isActive);

            return executeProcedure("Arenda.AddEditBank", new string[6] { "@id", "@cName", "@ca", "@bik" , "@mode", "@isActive" }, new DbType[6] { DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.Int32 , DbType.Int32}, ap); 
        
        
        }
        public DataTable CheakBK (string cName, string cA, string bik)
        {
            ap.Clear();
            ap.Add(cName);
            ap.Add(cA);
            ap.Add(bik);
            return executeProcedure("Arenda.CheakBK", new string[3] { "@cName", "@item", "@bic" }, new DbType[3] { DbType.String, DbType.String, DbType.String }, ap);
        }
        public DataTable delBank(int id)
        {
            ap.Clear();
            ap.Add(id);

            return executeProcedure("Arenda.delBank", new string[1] { "@id"}, new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable CheckBankIsNotDel(int id)
        {
          ap.Clear();
          ap.Add(id);

          return executeProcedure("Arenda.CheckBankIsNotDel", new string[] { "@id" },
            new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable CheckBankName(int id, string name)
        {
          ap.Clear();
          ap.Add(id);
          ap.Add(name);

          return executeProcedure("Arenda.CheckBankName",
            new string[] { "@id", "@cName" },
            new DbType[] { DbType.Int32, DbType.String }, ap);
        }

        public DataTable ActiveSprav(string basa, int id, int isActive, int id_Obj = 0)
        {
            ap.Clear();
            ap.Add(basa);
            ap.Add(id);
            ap.Add(isActive);
            ap.Add(id_Obj);
            return executeProcedure("Arenda.ActiveSpav",
              new string[4] { "@table", "@id", "@active", "@id_Obj" },
              new DbType[4] { DbType.String, DbType.Int32, DbType.Int32, DbType.Int32 },
              ap);
        }


        public DataTable delPos(int id)
        {
            ap.Clear();
            ap.Add(id);
            DataTable dt = executeProcedure("[Arenda].[DelPos]", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
            return executeProcedure("[Arenda].[DelPos]", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable CheckDeviceName(int id, string name)
        {
          ap.Clear();
          ap.Add(id);
          ap.Add(name);

          return executeProcedure("Arenda.CheckDeviceName",
            new string[] { "@id", "@cName" },
            new DbType[] { DbType.Int32, DbType.String }, ap);
        }

#endregion

       // @id_prog, 	@id_value,	@type_value,	@value_name, @value, 	@comment, 	@mode 

        public DataTable EditGetConf(int id_prog, string id_value , string value)
        {
            ap.Clear();
            ap.Add(id_prog);
            ap.Add(id_value);
            ap.Add(value);
            return executeProcedure("[Arenda].[EditGetConf]", 
                new string[3] { "@id_prog", "@id_value", "@value" }, 
                new DbType[3] { DbType.Int32, DbType.String, DbType.String }, ap);
        }


        public DataTable GetLastRec()
        {
            return executeCommand("[Arenda].[GetLast]");
        }

        public DataTable CheckAnotherPayment(int id, int id_Agreements, DateTime Date, decimal Summa)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_Agreements);
            ap.Add(Date);
            ap.Add(Summa);            
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);

            return executeProcedure("[Arenda].[CheckAnotherPayment]",
                new string[] { "@id", "@id_Agreements", "@Date", "@Summa",  
                               "@id_Editor" },
                new DbType[] { DbType.Int32, DbType.Int32, DbType.DateTime, DbType.Decimal, 
                               DbType.Int32 }, ap);
        }

        public DataTable CheckAfterPayments(int id, int id_Agreements, DateTime Date)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_Agreements);
            ap.Add(Date);            

            return executeProcedure("[Arenda].[CheckAfterPayments]",
                new string[] { "@id", "@id_Agreements", "@Date" },
                new DbType[] { DbType.Int32, DbType.Int32, DbType.DateTime }, ap);
        }        

        public DataTable AddEditPayment(int id, int id_Agreements, DateTime Date, decimal Summa, int id_PayType,DateTime PlaneDate,bool isRealMoney,bool isSendMoney,int? id_Fine)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_Agreements);
            ap.Add(Date);
            ap.Add(Summa);
            ap.Add(id_PayType);
            ap.Add(PlaneDate);
            ap.Add(isRealMoney);
            ap.Add(isSendMoney);
            ap.Add(id_Fine);

            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);

            return executeProcedure("[Arenda].[AddEditPayment]",
                new string[] { "@id", "@id_Agreements", "@Date", "@Summa", "@id_PayType","@PlaneDate","@isRealMoney","@isSendMoney","@id_Fines",
                               "@id_Editor" },
                new DbType[] { DbType.Int32, DbType.Int32, DbType.DateTime, DbType.Decimal, DbType.Int32,DbType.DateTime,DbType.Boolean,DbType.Boolean,DbType.Int32,
                               DbType.Int32 }, ap);                        
        }

        public DataTable GetPayments(int id_Agreements)
        {
            ap.Clear();
            ap.Add(id_Agreements);

            return executeProcedure("[Arenda].[GetPayments]",
                new string[] { "@id_Agreements" },
                new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable GetPaymentsLastMonth(int id_Agreements, DateTime Month)
        {
            ap.Clear();
            ap.Add(id_Agreements);
            ap.Add(Month);

            return executeProcedure("[Arenda].[GetPaymentsLastMonth]",
                new string[] { "@id_Agreements", "@Month" },
                new DbType[] { DbType.Int32, DbType.DateTime }, ap);
        }        

        public DataTable DelPayment(int id)
        {
            ap.Clear();
            ap.Add(id);

            return executeProcedure("[Arenda].[DelPayment]",
                new string[] { "@id" },
                new DbType[] { DbType.Int32 }, ap);
        }

				public DataTable CheckAnotherTaxes(int id, int id_Agreements, DateTime Date, int id_АddPayment)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_Agreements);
            ap.Add(Date);
						ap.Add(id_АddPayment);            

            return executeProcedure("[Arenda].[CheckAnotherTaxes]",
								new string[] { "@id", "@id_Agreements", "@Date", "@id_АddPayment" },
                new DbType[] { DbType.Int32, DbType.Int32, DbType.DateTime, DbType.Int32 }, ap);
        }

        public int AddEditTaxes(int id, int id_Agreements, DateTime Date, decimal Summa, string Comment, int id_АddPayment,DateTime datePlane, decimal? meters)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_Agreements);
            ap.Add(Date);
            ap.Add(Summa);
            ap.Add(Comment);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(id_АddPayment);
            ap.Add(datePlane);
            ap.Add(meters);

            DataTable dt = new DataTable();
            dt = executeProcedure("[Arenda].[AddEditTaxes]",
                new string[] { "@id", "@id_Agreements", "@Date", "@Summa", "@Comment",
                               "@id_Editor", "@id_АddPayment","@datePlane","@meters" },
                new DbType[] { DbType.Int32, DbType.Int32, DbType.DateTime, DbType.Decimal, DbType.String,
                               DbType.Int32, DbType.Int32,DbType.Date,DbType.Decimal }, ap);

            int id_row = 0;
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                id_row = int.Parse(dt.Rows[0][0].ToString());
            }

            return id_row;
        }

        public DataTable GetTaxes(int id_Agreements)
        {
            ap.Clear();
            ap.Add(id_Agreements);

            return executeProcedure("[Arenda].[GetTaxes]",
                new string[] { "@id_Agreements" },
                new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable DelTax(int id)
        {
            ap.Clear();
            ap.Add(id);

            return executeProcedure("[Arenda].[DelTax]",
                new string[] { "@id" },
                new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable GetTaxPayments(int id_Tax)
        {
            ap.Clear();
            ap.Add(id_Tax);

            return executeProcedure("[Arenda].[GetTaxPayments]",
                new string[] { "@id_Tax" },
                new DbType[] { DbType.Int32 }, ap);
        }

        public DateTime getCurDate()
        {
            ap.Clear();

            DataTable dt = executeProcedure("[Arenda].[GetCurDate]",
                new string[] { },
                new DbType[] { }, ap);
            if ((dt != null) && (dt.Rows.Count != 0))
            {
                return DateTime.Parse(dt.Rows[0][0].ToString()).Date;
            }
            else
            {
                return DateTime.Now.Date;
            }
        }

        public int AddTaxPayments(int id_Tax, DateTime Date, decimal Summa)
        {
            ap.Clear();
            ap.Add(id_Tax);            
            ap.Add(Date);
            ap.Add(Summa);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);

            DataTable dt = new DataTable();
            dt = executeProcedure("[Arenda].[AddTaxPayments]",
                new string[] { "@id_Tax", "@Date", "@Summa", "@id_Editor" },
                new DbType[] { DbType.Int32, DbType.DateTime, DbType.Decimal, DbType.Int32 }, ap);

            int id_row = 0;
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                id_row = int.Parse(dt.Rows[0][0].ToString());
            }

            return id_row;
        }

        public DataTable DelTaxPayment(int id)
        {
            ap.Clear();
            ap.Add(id);

            return executeProcedure("[Arenda].[DelTaxPayment]",
                new string[] { "@id" },
                new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable GetYears()
        {
            ap.Clear();            

            return executeProcedure("[Arenda].[GetYears]",
                new string[] { },
                new DbType[] { }, ap);
        }

        public DataTable GetPrintReportPlanCommon(int mon, int yea)
        {
            ap.Clear();
            ap.Add(mon);
            ap.Add(yea);
            ap.Add(ConnectionSettings.GetIdProgram());

            return executeProcedure("[Arenda].[GetPrintReportPlanCommon]",
                new string[] { "@mon", "@yea", "@id_prog" },
                new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable GetPrintReportPlanFact(int mon, int yea)
        {
            ap.Clear();
            ap.Add(mon);
            ap.Add(yea);
            ap.Add(ConnectionSettings.GetIdProgram());

            return executeProcedure("[Arenda].[GetPrintReportPlanFact]",
                new string[] { "@mon", "@yea", "@id_prog" },
                new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable GetPrintReportPlan(int mon, int yea)
        {
            ap.Clear();
            ap.Add(mon);
            ap.Add(yea);
            ap.Add(ConnectionSettings.GetIdProgram());

            return executeProcedure("[Arenda].[GetPrintReportPlan]",
                new string[] { "@mon", "@yea", "@id_prog" },
                new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable GetDopDocuments(int id_agr, int id_type)
        {
            ap.Clear();
            ap.Add(id_agr);
            ap.Add(id_type);            

            return executeProcedure("[Arenda].[GetDopDocuments]",
                new string[] { "@id_agr", "@id_type" },
                new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        public bool SuperUserMode()
        {
            bool res = false;

            ap.Clear();
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);

            DataTable dt = new DataTable();
            dt = executeProcedure("[Arenda].[CheckSuperUserMode]",
                new string[] { "@id_User" },
                new DbType[] { DbType.Int32 }, ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                res = (int.Parse(dt.Rows[0][0].ToString()) == 1) ? true : false;
            }

            return res;            
        }

        public DataTable GetSecInfo(int id)
        {
            ap.Clear();
            ap.Add(id);

            return executeProcedure("[Arenda].[GetSecInfo]",
                new string[] { "@id" },
                new DbType[] { DbType.Int32 }, ap);
        }


        public DataTable GetEquipment_vs_Sections(int id)
        {
            ap.Clear();
            ap.Add(id);

            return executeProcedure("[Arenda].[GetEquipment_vs_Sections]",
                new string[] { "@id" },
                new DbType[] { DbType.Int32 }, ap);
        }

        public bool CanPrintAPPZAct(int idAgreements)
        {
            bool res = false;

            ap.Clear();
            ap.Add(idAgreements);

            DataTable dt = new DataTable();
            dt =  executeProcedure("[Arenda].[CheckCanPrintAPPZAct]",
                new string[] { "@id" },
                new DbType[] { DbType.Int32 }, ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                res = bool.Parse(dt.Rows[0][0].ToString());
            }

            return res;
        }

        public DataTable GetAnotherPayments(bool All)
        {
            ap.Clear();
            ap.Add(All);

            return executeProcedure("[Arenda].[GetAnotherPayments]",
                new string[] { "@All" },
                new DbType[] { DbType.Boolean }, ap);
        }

        public DataTable GetAnotherPaymentsForAgreement(int idAgreement)
        {
            ap.Clear();
            ap.Add(idAgreement);

            return executeProcedure("[Arenda].[GetAnotherPaymentsForAgreement]",
                new string[] { "@idAgreement" },
                new DbType[] { DbType.Int32 }, ap);
        }

        public int AddEditAnotherPayments(int id, string cname)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(cname);

            DataTable dt = new DataTable();
            dt = executeProcedure("[Arenda].[AddEditAnotherPayments]",
                new string[] { "@id", "@cname" },
                new DbType[] { DbType.Int32, DbType.String }, ap);

            if ((dt!= null) && (dt.Rows.Count>0))
            {
                id = int.Parse(dt.Rows[0][0].ToString());
            }

            return id;
        }

        public int AnotherPaymentsIsUsed(int id)
        {
            int used = 0;

            ap.Clear();
            ap.Add(id);

            DataTable dt = new DataTable();

            dt =  executeProcedure("[Arenda].[AnotherPaymentsIsUsed]",
                new string[] { "@id" },
                new DbType[] { DbType.Int32 }, ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                used = int.Parse(dt.Rows[0][0].ToString());
            }

            return used;
        }

        public DataTable DelAnotherPayments(int id, int mode)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(mode);

            return executeProcedure("[Arenda].[DelAnotherPayments]",
                new string[] { "@id", "@mode" },
                new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable CheckAddPaymentName(int id, string name)
        {
          ap.Clear();
          ap.Add(id);
          ap.Add(name);

          return executeProcedure("Arenda.CheckAddPaymentName",
            new string[] { "@id", "@cName" },
            new DbType[] { DbType.Int32, DbType.String }, ap);
        }

        public decimal GetSettings(string id_value, decimal defaultval)
        {
            decimal res = defaultval;

            ap.Clear();
            ap.AddRange(new object[] { ConnectionSettings.GetIdProgram(), id_value });
            DataTable dt = new DataTable();

            dt = executeProcedure("[Arenda].[GetSettings]",
                new string[] { "@id_prog", "@id_value" }, 
                new DbType[] { DbType.Int32, DbType.String  }, ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                string val = dt.Rows[0]["value"].ToString().Replace('.',NumericSeparator());

                decimal.TryParse(val, out res);                
            }

            return res;
        }

        /// <summary>
        /// Процедура получения текущего разделителя целой и дробной части числа на локальном компьютере
        /// </summary>
        /// <returns> разделитель </returns>
        public static char NumericSeparator()
        {
            //обновление информации по региональным настройкам windows
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            return char.Parse(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        }

        /// <summary>
        /// Добавление изображения
        /// </summary>
        /// <param name="id_Fines"></param>
        /// <param name="cName"></param>
        /// <param name="DocScan"></param>
        /// <returns></returns>
        public int AddPicture(int id_Fines, string cName, byte[] DocScan)
        {
            int id = 0;

            ap.Clear();
            ap.Add(id_Fines);
            ap.Add(cName);
            ap.Add(DocScan);

            DataTable dt = new DataTable();
            dt = executeProcedure("Arenda.AddPicture",
                new string[] { "@id_Fines", "@cName", "@DocScan" },
                new DbType[] { DbType.Int32, DbType.String, DbType.Binary }, ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                id = int.Parse(dt.Rows[0][0].ToString());
            }

            return id;
        }

        public DataTable GetPictures(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.GetPictures",
                new string[] { "@id_Fines" },
                new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable DelPicture(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.DelPicture",
                new string[] { "@id" },
                new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable GetNotPayedReport()
        {
            ap.Clear();
            
            return executeProcedure("Arenda.GetNotPayedReport",
                new string[] { },
                new DbType[] { }, ap);
        }

        public DataTable GetLordlandDailyReport(int id_LandLord, DateTime Date)
        {
            ap.Clear();
            ap.Add(id_LandLord);
            ap.Add(Date);

            return executeProcedure("Arenda.GetLordlandDailyReport",
                new string[] { "@id_LandLord", "@date" },
                new DbType[] { DbType.Int32, DbType.Date }, ap);
        }

        public DataTable GetTenantContracts(int id_Tenant)
        {
            ap.Clear();
            ap.Add(id_Tenant);            

            return executeProcedure("Arenda.GetTenantContracts",
                new string[] { "@id_Tenant" },
                new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable GetTenantReport1(int id_Agreement, DateTime d1, DateTime d2)
        {
            ap.Clear();
            ap.Add(id_Agreement);
            ap.Add(d1);
            ap.Add(d2);

            return executeProcedure("Arenda.GetTenantReport1",
                new string[] { "@id_Agreement", "@d1", "@d2" },
                new DbType[] { DbType.Int32, DbType.Date, DbType.Date }, ap);
        }

        public DataTable GetTenantReport2(int id_Agreement, DateTime d1, DateTime d2)
        {
            ap.Clear();
            ap.Add(id_Agreement);
            ap.Add(d1);
            ap.Add(d2);

            return executeProcedure("Arenda.GetTenantReport2",
                new string[] { "@id_Agreement", "@d1", "@d2" },
                new DbType[] { DbType.Int32, DbType.Date, DbType.Date }, ap);
        }

        public int AddPaymentDetails(int id_PaymentContract, decimal Summa, int Delay,
            decimal Peni, DateTime Month, decimal Phone)
        {
            int result = 0;

            ap.Clear();
            ap.Add(id_PaymentContract);
            ap.Add(Summa);
            ap.Add(Delay);
            ap.Add(Peni);
            ap.Add(Month);
            ap.Add(Phone);

            DataTable dt = new DataTable();
            dt = executeProcedure("Arenda.AddPaymentDetails",
                new string[] { "@id_PaymentContract", "@Summa", "@Delay", 
                                "@Peni", "@Month", "@Phone" },
                new DbType[] { DbType.Int32, DbType.Decimal, DbType.Int32,
                                DbType.Decimal, DbType.Date, DbType.Decimal }, ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                result = int.Parse(dt.Rows[0][0].ToString());
            }

            return result;


        }

        public DataTable GetAddDocs(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.GetAddDocs", 
                new string[1] { "@id" }, 
                new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable GetTempTableForPenni(int K, DateTime StartDate, DateTime DateEnd, decimal TS, 
                                    decimal OST, decimal L, bool inDiapazon)
        {
            ap.Clear();
            ap.Add(K);
            ap.Add(StartDate);
            ap.Add(DateEnd);
            ap.Add(TS);
            ap.Add(OST);
            ap.Add(L);
            ap.Add(inDiapazon);

            return executeProcedure("Arenda.GetTempTableForPenni",
                new string[] { "@K", "@StartDate", "@DateEnd", "@TS", 
                                "@OST", "@L", "@inDiapazon" },
                new DbType[] { DbType.Int32, DbType.DateTime, DbType.DateTime, DbType.Decimal, 
                                DbType.Decimal, DbType.Decimal, DbType.Boolean }, ap);
        }

        /// <summary>
        /// Получение суммы оплат для расчета пени по договору аренды
        /// </summary>
        /// <param name="id">id договора</param>
        /// <returns>сумма (оплаты - пени)</returns>
        public decimal GetSopl(int id)
        {
            decimal result = 0;
            ap.Clear();
            ap.Add(id);

            DataTable dt = new DataTable();
            dt = executeProcedure("Arenda.GetSopl",
                new string[1] { "@id_Agreements" },
                new DbType[1] { DbType.Int32 }, ap);
            
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                result = decimal.Parse(dt.Rows[0][0].ToString());
            }

            return result;
        }

        public decimal GetPhone(int id, DateTime date)
        {
            decimal result = 0;
            ap.Clear();
            ap.Add(id);
            ap.Add(date);            

            DataTable dt = new DataTable();
            dt = executeProcedure("Arenda.GetPhone",
                new string[] { "@id_Agreements", "@month" },
                new DbType[] { DbType.Int32, DbType.Date }, ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                result = decimal.Parse(dt.Rows[0][0].ToString());
            }

            return result;
        }

        public DataTable CheckSameDocTypeAndDateExists(int id_agr, int id, DateTime date)
        {
            ap.Clear();
            ap.Add(id_agr);            
            ap.Add(id);
            ap.Add(date);

            return executeProcedure("Arenda.CheckSameDocTypeAndDateExists",
                new string[] { "@id_agreements", "@id_TypeDoc", "@DateDocument" },
                new DbType[] { DbType.Int32, DbType.Int32, DbType.DateTime }, ap);
        }

        public DataTable CheckPaymentsOnMonth(int id, DateTime date)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(date);

            return executeProcedure("Arenda.CheckPaymentsOnMonth",
                new string[] { "@id_agreement", "@date" },
                new DbType[] { DbType.Int32, DbType.DateTime }, ap);
        }
        
        public DataTable GetPaymentDetails(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.GetPaymentDetails",
                new string[1] { "@id_PaymentContract" },
                new DbType[1] { DbType.Int32 }, ap);
        }

        public int CheckBeforeDelDopDocs(int id)
        {
            int result = -1;

            ap.Clear();
            ap.Add(id);            

            DataTable dt = new DataTable();
            dt = executeProcedure("Arenda.CheckBeforeDelDopDocs",
                new string[] { "@id" },
                new DbType[] { DbType.Int32 }, ap);

            if((dt!=null) && (dt.Rows.Count>0))
            {
                result = int.Parse(dt.Rows[0][0].ToString());
            }

            return result;
        }

        public DataTable DelTD(int id)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
           
            return executeProcedure("Arenda.DelTD",
                new string[] { "@id", "@id_user" },
                new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        public void FullPayed(int id, bool fullpayed)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(fullpayed);

            executeProcedure("Arenda.FullPayed",
                new string[] { "@id", "@fullPayed" },
                new DbType[] { DbType.Int32, DbType.Boolean }, ap);
        }

        public int SaveDevice(int id, string name, string abbreviation, string unit)
        {
            ap.Clear();
            ap.AddRange(new object[] { id, name, abbreviation, unit });

            DataTable dt = executeProcedure("Arenda.SaveDevice", new string[] { "@id", "@name", "@abbreviation", "@unit" }, new DbType[] { DbType.Int32, DbType.String, DbType.String, DbType.String }, ap);
            return dt != null && dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["id"]) : 0;
        }

        public void SaveTenantAddInfo(int id_landlord_tenant, string last_name, string name, string second_name, string department, DateTime date_issue, string passport, string issued, string address, bool sex, string orgnip)
        {
            ap.Clear();
            ap.AddRange(new object[] { id_landlord_tenant, last_name, name, second_name, department, date_issue, passport, issued, address, sex, orgnip });
            executeProcedure("Arenda.SaveTenantAddInfo", new string[] { "@id_Landlord_Tenant", "@last_name", "@name", "@second_name", "@department", "@date_issue", "@passport", "@issued", "@address", "@sex", "@orgnip" },
                                                          new DbType[] { DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.String, DbType.DateTime, DbType.String, DbType.String, DbType.String, DbType.Boolean, DbType.String }, ap);
        }

        public DataTable GetTenantAddInfo(int id_landlord_tenant)
        {
            ap.Clear();
            ap.Add(id_landlord_tenant);
            return executeProcedure("Arenda.GetTenantAddInfo", new string[] { "@id_landlord_tenant" }, new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable GetDevices()
        {
            ap.Clear();
            return executeProcedure("Arenda.GetDevices", new string[] { }, new DbType[] { }, ap);
        }

        public void DeleteDevice(int id, bool used)
        {
            ap.Clear();
            ap.AddRange(new object[] { id, used });
            executeProcedure("Arenda.DeleteDevice", new string[] { "@id", "@used" }, new DbType[] { DbType.Int32, DbType.Boolean }, ap);
        }
      
      public void RestoreDevice(int id, bool isActive, bool Used)
      {
        ap.Clear();
        //ap.AddRange(new object[] { id });
        ap.Add(id);
        ap.Add(isActive);
        ap.Add(Used);
        executeProcedure("Arenda.RestoreDevice",
          new string[] { "@id", "@IsActive", "@Used" },
          new DbType[] { DbType.Int32, DbType.Boolean, DbType.Boolean }, ap);
      }

        public DataTable GetSectionDevices(int id_section)
        {
            ap.Clear();
            ap.Add(id_section);
            return executeProcedure("Arenda.GetSectionDevices", new string[] { "@id_section" }, new DbType[] { DbType.Int32 }, ap);
        }

        public void AddDeviceToSection(int id_section, int id_device, int quantity)
        {
            ap.Clear();
            ap.AddRange(new object[] { id_section, id_device, quantity });
            executeProcedure("Arenda.AddDeviceToSection", new string[] { "@id_section", "@id_device", "@quantity" }, new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        public void RemoveDeviceFromSection(int id_section, int id_device)
        {
            ap.Clear();
            ap.AddRange(new object[] { id_section, id_device });
            executeProcedure("Arenda.RemoveDeviceFromSection", new string[] { "@id_section", "@id_device" }, new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable GetPrintDataActReklama(int id, int id_act_rekl)
        {
            ap.Clear();
            ap.AddRange(new object[] { id, id_act_rekl });
            return executeProcedure("Arenda.GetPrintDataActReklama", new string[] { "@id", "@id_act_rekl" }, new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable GetDevicesForPrint(int id_agreement)
        {
            ap.Clear();
            ap.AddRange(new object[] { id_agreement });
            return executeProcedure("Arenda.GetDevicesForPrint", new string[] { "@id_agreement" }, new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable GetPrintDataActVozvrat(int id, int id_act_vozvrat)
        {
            ap.Clear();
            ap.AddRange(new object[] { id, id_act_vozvrat });
            return executeProcedure("Arenda.GetPrintDataActVozvrat", new string[] { "@id", "@id_act_vozvrat" }, new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable GetEquipmentForPrint(int id_agreement)
        {
            ap.Clear();
            ap.AddRange(new object[] { id_agreement });
            return executeProcedure("Arenda.GetEquipmentForPrint", new string[] { "@id_agreement" }, new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable GetPrintDataActPriemPeredacha(int id, int id_act_priema)
        {
            ap.Clear();
            ap.AddRange(new object[] { id, id_act_priema });
            return executeProcedure("Arenda.GetPrintDataActPriemPeredacha", new string[] { "@id", "@id_act_priema" }, new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable GetPrintDataDogovorReklama(int id)
        {
            ap.Clear();
            ap.AddRange(new object[] { id });
            return executeProcedure("Arenda.GetPrintDataDogovorReklama", new string[] { "@id" }, new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable GetPrintDataSoglRastor(int id, int id_sogl_rastor)
        {
            ap.Clear();
            ap.AddRange(new object[] { id, id_sogl_rastor });
            return executeProcedure("Arenda.GetPrintDataSoglRastor", new string[] { "@id", "@id_sogl_rastor" }, new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable GetPrintDataActKomm(int id, int id_act_komm)
        {
            ap.Clear();
            ap.AddRange(new object[] { id, id_act_komm });
            return executeProcedure("Arenda.GetPrintDataActKomm", new string[] { "@id", "@id_act_komm" }, new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable GetPrintDataDogovorArenda(int id)
        {
            ap.Clear();
            ap.AddRange(new object[] { id });
            return executeProcedure("Arenda.GetPrintDataDogovorArenda", new string[] { "@id" }, new DbType[] { DbType.Int32 }, ap);
        }

        #region "Работа с файлами"

        public DataTable setScan(int id_Doc, string nameFile, string Extension, int id_DocType,
          DateTime DateDocument, string path)
        {
          ap.Clear();
          ap.Add(id_Doc);
          ap.Add(nameFile);
          ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
          ap.Add(Extension);
          ap.Add(id_DocType);
          ap.Add(DateDocument);
          ap.Add(path);

          return executeProcedure("[Arenda].[setScan]",
            new string[] { "@id_Doc", "@nameFile", "@idUser", "@Extension", "@id_DocType", "@DateDocument", "@Path"},
            new DbType[] { DbType.Int32, DbType.String, DbType.Int32, DbType.String, DbType.Int32,
              DbType.DateTime, DbType.String}, ap);
        }

        public DataTable getScan(int id_Doc, int id)
        {
          ap.Clear();
          ap.Add(id_Doc);
          ap.Add(id);

          return executeProcedure("[Arenda].[getScan]",
            new string[] { "@id_Doc", "@id" },
            new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable updateScanName(int id, string nameFile)
        {
          ap.Clear();
          ap.Add(id);
          ap.Add(nameFile);

          return executeProcedure("[Arenda].[updateScanName]",
            new string[] { "@id", "@nameFile" },
            new DbType[] { DbType.Int32, DbType.String }, ap);
        }

        public DataTable delScan(int id)
        {
          ap.Clear();
          ap.Add(id);

          return executeProcedure("[Arenda].[delScan]",
            new string[] { "@id" },
            new DbType[] { DbType.Int32 }, ap);
        }

        #endregion
        public DataTable GetDocTypes()
        {
          ap.Clear();

          return executeProcedure("Arenda.getDocTypes",
                  new string[] { },
                  new DbType[] { }, ap);
        }

        public DataTable CheckDocTypeAndDate(int id_Doc, int id_DocType, DateTime DateDocument)
        {
          ap.Clear();
          ap.Add(id_Doc);
          ap.Add(id_DocType);
          ap.Add(DateDocument);

          return executeProcedure("Arenda.CheckDocTypeAndDate",
            new string[] { "@id_Doc", "@id_DocType", "@DateDocument" },
            new DbType[] { DbType.Int32, DbType.Int32, DbType.DateTime }, ap);
        }
      
      public DataTable GetObjects()
      {
        ap.Clear();

        return executeProcedure("Arenda.GetObjects", new string[] { }, new DbType[] { }, ap);
      }

      public void ChangeObjectActiveStatus(int id, bool active, bool used, string com)
      {
        ap.Clear();
        ap.AddRange(new object[] { id, active, used,
          Nwuram.Framework.Settings.User.UserSettings.User.Id, com });
        executeProcedure("Arenda.ChangeObjectActiveStatus",
          new string[] { "@id", "@IsActive", "@Used", "@id_User", "@Comment" },
          new DbType[] { DbType.Int32, DbType.Boolean, DbType.Boolean, DbType.Int32, DbType.String },
          ap);
      }

      public DataTable CheckObjectName(int id, string name)
      {
        ap.Clear();
        ap.Add(id);
        ap.Add(name);

        return executeProcedure("Arenda.CheckObjectName",
          new string[] { "@id", "@cName" },
          new DbType[] { DbType.Int32, DbType.String }, ap);
      }

        public DataTable SaveObject(int id, string name, string com, string CadastralNumber)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(name);
            ap.Add(com);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(CadastralNumber);

            return executeProcedure("Arenda.SaveObject",
              new string[] { "@id", "@cName", "@Comment", "@id_User", "@CadastralNumber" },
              new DbType[] { DbType.Int32, DbType.String, DbType.String, DbType.Int32, DbType.String },
              ap);
        }

      public DataTable CheckObjectIsNotDel(int id)
      {
        ap.Clear();
        ap.Add(id);

        return executeProcedure("Arenda.CheckObjectIsNotDel", new string[] { "@id" },
          new DbType[] { DbType.Int32 }, ap);
      }

      public DataTable CheckObjectIsUsed(int id)
      {
        ap.Clear();
        ap.Add(id);

        return executeProcedure("Arenda.CheckObjectIsUsed", new string[] { "@id" },
          new DbType[] { DbType.Int32 }, ap);
      }

      public DataTable GetContractTypes()
      {
        ap.Clear();

        return executeProcedure("Arenda.GetContractTypes", new string[] { }, new DbType[] { }, ap);
      }

      public DataTable GetParentChildTenant(int id, int mode)
      {
        ap.Clear();
        ap.Add(id);
        ap.Add(mode);

        return executeProcedure("Arenda.GetParentChildTenant",
          new string[2] { "@id", "@mode" },
          new DbType[2] { DbType.Int32, DbType.Int32 }, ap);
      }

      public DataTable CheckParentChildTenant(int id, int mode)
      {
        ap.Clear();
        ap.Add(id);
        ap.Add(mode);

        return executeProcedure("Arenda.CheckParentChildTenant",
          new string[2] { "@id", "@mode" },
          new DbType[2] { DbType.Int32, DbType.Int32}, ap);
      }

      public DataTable SetParentChildTenant(int idp, int idc)
      {
        ap.Clear();
        ap.Add(idp);
        ap.Add(idc);
        ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);

        return executeProcedure("Arenda.SetParentChildTenant",
          new string[3] { "@id_parent", "@id_child", "@id_creator" },
          new DbType[3] { DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
      }

        //NEW
        public DataTable getSavePayment()
        {
            ap.Clear();

            return executeProcedure("Arenda.spg_getSavePayment",
              new string[0] { },
              new DbType[0] { }, ap);
        }

        public DataTable getSealSections(int id_agreements)
        {
            ap.Clear();
            ap.Add(id_agreements);


            return executeProcedure("Arenda.spg_getSealSections",
              new string[1] {"@id_agreements" },
              new DbType[1] {DbType.Int32 }, ap);
        }

        public DataTable setSealSections(int id_agreements,DateTime date, int type)
        {
            ap.Clear();
            ap.Add(id_agreements);
            ap.Add(date);
            ap.Add(type);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);


            return executeProcedure("Arenda.spg_setSealSections",
              new string[4] { "@id_agreements", "@date", "@type", "@id_user" },
              new DbType[4] { DbType.Int32, DbType.Date, DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable setConfirm(int id_agreements,bool isConfirm)
        {
            ap.Clear();
            ap.Add(id_agreements);
            ap.Add(isConfirm);


            return executeProcedure("Arenda.spg_setConfirm",
              new string[2] { "@id_agreements", "@isConfirm" },
              new DbType[2] { DbType.Int32, DbType.Boolean }, ap);
        }

        public DataTable getReclamaPlace(int id_object, int id_build)
        {
            ap.Clear();
            ap.Add(id_object);
            ap.Add(id_build);

            return executeProcedure("Arenda.spg_getReclamaPlace",
              new string[2] { "@id_object", "@id_build" },
              new DbType[2] { DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable getLandPlot(int id_object)
        {
            ap.Clear();
            ap.Add(id_object);

            return executeProcedure("Arenda.spg_getLandPlot",
              new string[1] { "@id_ObjectLease" },
              new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable getInfoUsedSection(int id,DateTime dateStart,DateTime dateEnd,int id_section,int id_TypeContract)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(dateStart);
            ap.Add(dateEnd);
            ap.Add(id_section);
            ap.Add(id_TypeContract);

            return executeProcedure("Arenda.spg_getInfoUsedSection",
              new string[5] { "@id", "@dateStart", "@dateEnd", "@id_section", "@id_TypeContract" },
              new DbType[5] { DbType.Int32,DbType.Date,DbType.Date,DbType.Int32,DbType.Int32 }, ap);
        }

        public DataTable getPayType(bool withAllDeps = false)
        { 
            ap.Clear();

            DataTable dtResult = executeProcedure("[Arenda].[spg_getPayType]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            if (withAllDeps)
            {
                if (dtResult != null)
                {
                    if (!dtResult.Columns.Contains("isMain"))
                    {
                        DataColumn col = new DataColumn("isMain", typeof(int));
                        col.DefaultValue = 1;
                        dtResult.Columns.Add(col);
                        dtResult.AcceptChanges();
                    }

                    DataRow row = dtResult.NewRow();

                    row["cName"] = "Все Типы";
                    row["id"] = 0;
                    row["isMain"] = 0;
                    row["isActive"] = 1;
                    dtResult.Rows.Add(row);
                    dtResult.AcceptChanges();
                    dtResult.DefaultView.RowFilter = "isActive = 1";
                    dtResult.DefaultView.Sort = "isMain asc, cName asc";
                    dtResult = dtResult.DefaultView.ToTable().Copy();
                }
            }
            else
            {
                dtResult.DefaultView.RowFilter = "isActive = 1";
                dtResult.DefaultView.Sort = "cName asc";
                dtResult = dtResult.DefaultView.ToTable().Copy();
            }

            return dtResult;
        }

        public DataTable getFineConfirmed(int id_Agreements,DateTime dateStart, DateTime dateEnd)
        {
            ap.Clear();
            ap.Add(id_Agreements);
            ap.Add(dateStart);
            ap.Add(dateEnd);

            return executeProcedure("Arenda.spg_getFineConfirmed",
              new string[3] { "@id_Agreements", "@dateStart", "@dateEnd" },
              new DbType[3] { DbType.Int32,DbType.Date,DbType.Date }, ap);
        }

        public DataTable GetListTaxesForKnt(DateTime DatePlane)
        {
            ap.Clear();
            ap.Add(DatePlane);

            return executeProcedure("Arenda.spg_GetListTaxesForKnt",
              new string[1] { "@DatePlane" },
              new DbType[1] { DbType.Date }, ap);
        }

        public DataTable setConfirmedTaxes(int id)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);


            return executeProcedure("Arenda.spg_setConfirmedTaxes",
              new string[2] { "@id", "@id_user" },
              new DbType[2] { DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable getTypeDiscount(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Arenda].[spg_getTypeDiscount]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            if (withAllDeps)
            {
                if (dtResult != null)
                {
                    if (!dtResult.Columns.Contains("isMain"))
                    {
                        DataColumn col = new DataColumn("isMain", typeof(int));
                        col.DefaultValue = 1;
                        dtResult.Columns.Add(col);
                        dtResult.AcceptChanges();
                    }

                    DataRow row = dtResult.NewRow();

                    row["cName"] = "Все Типы скидки";
                    row["id"] = 0;
                    row["isMain"] = 0;
                    dtResult.Rows.Add(row);
                    dtResult.AcceptChanges();
                    dtResult.DefaultView.Sort = "isMain asc, cName asc";
                    dtResult = dtResult.DefaultView.ToTable().Copy();
                }
            }

            return dtResult;
        }

        public DataTable setTDiscount(int id,int id_Agreements,DateTime dateStart, DateTime? dateEnd,int id_TypeDiscount,int id_StatusDiscount,decimal discount,int result = 0, bool isDel = false)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_Agreements);
            ap.Add(dateStart);
            ap.Add(dateEnd);
            ap.Add(id_TypeDiscount);
            ap.Add(id_StatusDiscount);
            ap.Add(discount);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(result);
            ap.Add(isDel);


            return executeProcedure("Arenda.spg_setTDiscount",
              new string[10] { "@id", "@id_Agreements", "@dateStart", "@dateEnd", "@id_TypeDiscount", "@id_StatusDiscount", "@Discount", "@id_user", "@result", "@isDel" },
              new DbType[10] { DbType.Int32, DbType.Int32, DbType.Date, DbType.Date, DbType.Int32, DbType.Int32, DbType.Decimal, DbType.Int32, DbType.Int32, DbType.Boolean }, ap);
        }

        public DataTable getTDiscount(int id_Agreements)
        {
            ap.Clear();
            ap.Add(id_Agreements);

            return executeProcedure("Arenda.spg_getTDiscount",
              new string[1] { "@id_Agreements" },
              new DbType[1] { DbType.Int32}, ap);
        }
    }    
}




