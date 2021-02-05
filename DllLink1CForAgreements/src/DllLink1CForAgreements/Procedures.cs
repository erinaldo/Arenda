using System.Text;
using System.Collections;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.Connection;
using System.Data;
using System;
using Nwuram.Framework.Settings.User;
using System.Threading.Tasks;
using System.Threading;

namespace DllLink1CForAgreements
{
    class Procedures : SqlProvider
    {
        public Procedures(string server, string database, string username, string password, string appName)
              : base(server, database, username, password, appName)
        {
        }
        ArrayList ap = new ArrayList();

        #region ""

        /// <summary>
        /// Получение списка объектов
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public DataTable getObjectLease(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Arenda].[spg_getObjectLease]",
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

                    row["cName"] = "Все Объекты";
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


        /// <summary>
        /// Получение списка объектов
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public DataTable getTypeContract(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Arenda].[spg_getTypeContract]",
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

                    row["cName"] = "Все типы";
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

        public DataTable GetListLandlord(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Arenda].[GetListLandlord]",
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

                    row["cName"] = "Все арендодатели";
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

        public DataTable GetListAgreementTo1C(int id_object)
        {
            ap.Clear();
            ap.Add(id_object);

            return executeProcedure("Arenda.GetListAgreementTo1C",
              new string[1] { "@id_object" },
              new DbType[1] { DbType.Int32 }, ap);
        }

        #endregion

        public DataTable EditGetConf(int id_prog, string id_value, string value)
        {
            ap.Clear();
            ap.Add(id_prog);
            ap.Add(id_value);
            ap.Add(value);
            return executeProcedure("[Arenda].[EditGetConf]",
                new string[3] { "@id_prog", "@id_value", "@value" },
                new DbType[3] { DbType.Int32, DbType.String, DbType.String }, ap);
        }

        public DataTable FindAgreement1CForAgreement(string inAgreement)
        {
            ap.Clear();
            ap.Add(inAgreement);

            return executeProcedure("Arenda.FindAgreement1CForAgreement",
              new string[1] { "@inAgreement" },
              new DbType[1] { DbType.String }, ap);
        }


        public DataTable SetAgreement1CForAgreement(int id_Agreements,string NumberAccount,DateTime DateAccount,string NumberAgreement,string TypePayment,bool isAdd,int id_Scan,bool isNewData)
        {
            ap.Clear();
            ap.Add(id_Agreements);
            ap.Add(NumberAccount);
            ap.Add(DateAccount);
            ap.Add(NumberAgreement);
            ap.Add(TypePayment);
            ap.Add(isAdd);
            ap.Add(id_Scan);            
            ap.Add(UserSettings.User.Id);
            ap.Add(isNewData);

            return executeProcedure("Arenda.SetAgreement1CForAgreement",
              new string[9] { "@id_Agreements", "@NumberAccount", "@DateAccount", "@NumberAgreement", "@TypePayment", "@isAdd", "@id_Scan", "@id_user", "@isNewData" },
              new DbType[9] { DbType.Int32, DbType.String, DbType.DateTime, DbType.String, DbType.String, DbType.Boolean, DbType.Int32, DbType.Int32,DbType.Boolean }, ap);
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
              new string[] { "@id_Doc", "@nameFile", "@idUser", "@Extension", "@id_DocType", "@DateDocument", "@Path" },
              new DbType[] { DbType.Int32, DbType.String, DbType.Int32, DbType.String, DbType.Int32,
              DbType.DateTime, DbType.String}, ap);
        }

        public DataTable UpdateAgreements1C(int id_Agreements, string NumberAgreement)
        {
            ap.Clear();
            ap.Add(id_Agreements);
            ap.Add(NumberAgreement);

            return executeProcedure("Arenda.UpdateAgreements1C",
              new string[2] { "@id_Agreements", "@NumberAgreement" },
              new DbType[2] { DbType.Int32, DbType.String }, ap);
        }
    }
}
