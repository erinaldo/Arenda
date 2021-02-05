using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.Data;
using System.Collections;
using System.Data;

namespace dllPlanReportMonth
{
    class Procedures : SqlProvider
    {
        public Procedures(string server, string database, string username, string password, string progName) : base(server, database, username, password, progName)
        {

        }

        ArrayList ap = new ArrayList();

        public async Task<DataTable> getReport(DateTime datePlan)
        {
            ap.Clear();
            ap.Add(datePlan);
            return executeProcedure("[Arenda].[getReportByObjects]",
                new string[] { "@date_plan" },
                new DbType[] { DbType.DateTime }, ap);
        }
        public async Task<DataTable> getAllPayments(DateTime datePlan)
        {
            ap.Clear();
            ap.Add(datePlan);
            return executeProcedure("[Arenda].[getReportAllPayments]",
                new string[] { "@date_plan" },
                new DbType[] { DbType.DateTime }, ap);
        }
    }
}
