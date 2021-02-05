using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Settings.User;

namespace dllPlanReport
{
    class Procedures : SqlProvider
    {
        public Procedures(string server, string database, string login, string password, string progName) : base (server,database,login, password, progName)
        {

        }

        ArrayList ap = new ArrayList();

        public async Task<DataTable> getPlanAgreement()
        {
            ap.Clear();
            return executeProcedure("[Arenda].[getPlanAgreement]",
                new string[] { },
                new DbType[] { }, ap);
        }

        public async Task<DataTable> getDiscounts()
        {
            ap.Clear();
            return executeProcedure("[Arenda].[kav_getDiscounts]",
                new string[] { },
                new DbType[] { }, ap);
        }

        public async Task<DataTable> getLeaves()
        {
            ap.Clear();
            return executeProcedure("[Arenda].[getDocumentsWithLeave]",
                new string[] { },
                new DbType[] { }, ap);
        }

        public async Task<DataTable> setPlanData(string xmlMain, string xmlLeave)
        {
            ap.Clear();
            ap.Add(xmlMain);
            ap.Add(xmlLeave);
            ap.Add(UserSettings.User.Id);
            return executeProcedure("[Arenda].[setPlanData]",
                new string[] { "@xmlMain", "@xmlLeave", "@id_user" },
                new DbType[] { DbType.Xml, DbType.Xml, DbType.Int32 }, ap);
        }

        public async Task<DataTable> getDataReport(int id_Object, int id_Building, int id_Floor, int id_Section, int id_Tenant)
        {
            ap.Clear();
            ap.Add(id_Object);
            ap.Add(id_Building);
            ap.Add(id_Floor);
            ap.Add(id_Section);
            ap.Add(id_Tenant);
            ap.Add(ConnectionSettings.GetIdProgram());
            return executeProcedure("[Arenda].[getMainTablePlan]",
                new string[] {"@id_obj","@id_building","@id_floor","@id_section","@id_tenant", "@id_prog" },
                new DbType[] {DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32,DbType.Int32 ,DbType.Int32 }, ap);
        }

        #region combobox
        public async Task<DataTable> getObjects()
        {
            ap.Clear();
            DataTable dt =  executeProcedure("[Arenda].[spg_getObjectLease]",
                new string[] { },
                new DbType[] { }, ap);
            DataRow dr = dt.NewRow();
            dr["id"] = 0;
            dr["cName"] = "";
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }
        public async Task<DataTable> getBuildings()
        {
            ap.Clear();
            DataTable dt = executeProcedure("Arenda.spg_getBuilding",
                new string[] { },
                new DbType[] { }, ap);
            DataRow dr = dt.NewRow();
            dr["id"] = 0;
            dr["cName"] = "";
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }
        public async Task<DataTable> getFloors()
        {
            ap.Clear();
            DataTable dt = executeProcedure("[Arenda].[spg_getFloors]",
                new string[] { },
                new DbType[] { }, ap);
            DataRow dr = dt.NewRow();
            dr["id"] = 0;
            dr["cName"] = "";
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }

        public async Task<DataTable> getSections()
        {
            ap.Clear();
            DataTable dt = executeProcedure("[Arenda].kav_getAllSections",
                new string[] { },
                new DbType[] { }, ap);
            DataRow dr = dt.NewRow();
            dr["id"] = 0;
            dr["nameSection"] = "";
            dt.Rows.InsertAt(dr, 0);
            return dt;

        }
        public async Task<DataTable> getTenants()
        {
            ap.Clear();
            ap.Add(1);
            return executeProcedure("[Arenda].[GetTenant]",
                new string[] { "@isActive" },
                new DbType[] { DbType.Int32 }, ap).AsEnumerable().OrderBy(r=>r.Field<string>("aren")).CopyToDataTable();
        }

        #endregion





    }
}
