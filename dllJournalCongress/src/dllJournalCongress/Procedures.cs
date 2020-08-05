using System.Text;
using System.Collections;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.Connection;
using System.Data;
using System;
using Nwuram.Framework.Settings.User;
using System.Threading.Tasks;
using System.Threading;

namespace dllJournalCongress
{
    class Procedures : SqlProvider
    {
        public Procedures(string server, string database, string username, string password, string appName)
              : base(server, database, username, password, appName)
        {
        }
        ArrayList ap = new ArrayList();

        /// <summary>
        /// Получение списка объектов
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getObjectLease(bool withAllDeps = false)
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
        /// Получение Журнала съездов
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getJournalCongress(DateTime dateStart, DateTime dateEnd)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(dateEnd);

            return executeProcedure("[Arenda].[spg_getJournalCongress]",
                 new string[2] { "@dateStart", "@dateEnd" },
                 new DbType[2] { DbType.Date, DbType.Date }, ap);
            
        }

        /// <summary>
        /// Подтверждение или аннуляция съезда
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> setJournalCongress(int id)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(UserSettings.User.Id);


            return executeProcedure("[Arenda].[spg_setJournalCongress]",
                 new string[2] { "@id", "@id_user" },
                 new DbType[2] { DbType.Int32, DbType.Int32 }, ap);

        }
    }
}
