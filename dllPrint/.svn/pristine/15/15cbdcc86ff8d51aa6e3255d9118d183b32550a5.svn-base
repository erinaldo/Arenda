using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.Connection;

namespace ArendaPrint
{
    class Procedures : SqlProvider
    {
        ArrayList ap = new ArrayList();
        public Procedures(string server, string database, string username, string password, string appName)
          : base(server, database, username, password, appName)
        {

        }

        public async Task<DataTable> GetLD(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.GetLD", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }

        public async Task<DataTable> GetAdditionDocs(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("Arenda.GetAdditionDocs",
                new string[1] { "@id" },
                new DbType[1] { DbType.Int32 }, ap);
        }

        public async Task<DataTable> GetDopDocuments(int id_agr, int id_type)
        {
            ap.Clear();
            ap.Add(id_agr);
            ap.Add(id_type);

            return executeProcedure("[Arenda].[GetDopDocuments]",
                new string[] { "@id_agr", "@id_type" },
                new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        public async Task<DataTable> EditGetConf(int id_prog, string id_value, string value)
        {
            ap.Clear();
            ap.Add(id_prog);
            ap.Add(id_value);
            ap.Add(value);
            return executeProcedure("[Arenda].[EditGetConf]",
                new string[3] { "@id_prog", "@id_value", "@value" },
                new DbType[3] { DbType.Int32, DbType.String, DbType.String }, ap);
        }
        #region все ворды
        public async Task<DataTable> GetPrintDataDopSogl(int id, int id_dopsogl)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_dopsogl);

            return executeProcedure("Arenda.GetPrintDataDopSogl",
                new string[] { "@id", "@id_dopsogl" },
                new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }
       
        public async Task<DataTable> GetDevicesForPrint(int id_agreement)
        {
            ap.Clear();
            ap.AddRange(new object[] { id_agreement });
            return executeProcedure("Arenda.GetDevicesForPrint", new string[] { "@id_agreement" }, new DbType[] { DbType.Int32 }, ap);
        }
      

       
       
        public async Task<DataTable> GetPrintDataActKomm(int id, int id_act_komm)
        {
            ap.Clear();
            ap.AddRange(new object[] { id, id_act_komm });
            return executeProcedure("Arenda.GetPrintDataActKomm", new string[] { "@id", "@id_act_komm" }, new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }    
        #endregion

        // проца все в одном
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id_agreement</param>
        /// <param name="id_sogl_rastor">id addititional document</param>   
        /// <returns></returns>
        public async Task<DataTable> getPrintData(int id, int id_sogl_rastor)
        {
            ap.Clear();
            ap.AddRange(new object[] { id, id_sogl_rastor});
            return executeProcedure("Arenda.kav_getPrintData", new string[] { "@id", "@id_act"}, new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }
    }
}
