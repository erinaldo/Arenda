using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nwuram.Framework.Data;
using Nwuram.Framework;
using Nwuram.Framework.Settings.Connection;
using System.Threading.Tasks;

namespace ArendaFileExport
{
    class Procedures : SqlProvider
    {
        ArrayList ap = new ArrayList();

        public Procedures(string server, string database, string username, string password, string appName)
          : base(server, database, username, password, appName)
        {
        }
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

        public DataTable GetDocumens()
        {
            ap.Clear();

            return executeProcedure("[Arenda].[spg_GetDocuments]",
              new string[] { }, new DbType[] { }, ap);
        }

        public DataTable GetDocumentsBody(int id)
        {
            ap.Clear();
            ap.Add(id);

            return executeProcedure("[Arenda].[spg_GetDocumentsBody]",
              new string[1] { "@id" },
              new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable SetDocument(int id, string path)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(path);

            return executeProcedure("[Arenda].[SetDocument]",
              new string[2] { "@id", "@path" },
              new DbType[2] { DbType.Int32, DbType.String }, ap);
        }
    }
}
