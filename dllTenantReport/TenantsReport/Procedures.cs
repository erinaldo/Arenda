using Nwuram.Framework.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TenantsReport
{
    class Procedures : SqlProvider
    {
        static ArrayList ap = new ArrayList();
        public Procedures(string server, string database, string username, string password, string appName)
            : base(server, database, username, password, appName)
        {
        }

        public DataTable GetObjects()
        {
            ap.Clear();
            return executeProcedure("[Arenda].[LibGetObjects]",
                new string[] { },
                new DbType[] { }, ap);
        }

        public DataTable GetTenants(int IdObject)
        {
            ap.Clear();
            ap.Add(IdObject);
            return executeProcedure("[Arenda].[LibGetLandlords]",
            new string[] { "@IdObject" },
            new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable GetActivities()
        {
            ap.Clear();
            return executeProcedure("[Arenda].[LibGetActivities]",
                new string[] { },
                new DbType[] { }, ap);
        }

        public DataTable GetTenantReport(int IdObject, int IdLandlord, int IdActivities)
        {
            ap.Clear();
            ap.Add(IdObject);
            ap.Add(IdLandlord);
            ap.Add(IdActivities);
            return executeProcedure("[Arenda].[LibGetTenantReport]",
            new string[] { "@IdObject", "@IdLandlord", "@IdActivities" },
            new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }
        public DateTime GetDate()
        {
            ap.Clear();
            return (DateTime)executeProcedure("[Arenda].[GetDate]",
                new string[] { },
                new DbType[] { }, ap).Rows[0][0];
        }
    }
}
