using System.Text;
using System.Collections;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.Connection;
using System.Data;
using System;
using Nwuram.Framework.Settings.User;
using System.Threading.Tasks;
using System.Threading;

namespace dllArendaJournalAccrualsPenalties
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
        /// Получение списка объектов
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getTypeContract(bool withAllDeps = false)
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

                    row["cName"] = "Все договора";
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
        /// Получение списка преиода кредитования
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getListPeriodCredit(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Arenda].[spg_getListPeriodCredit]",
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

                    row["PeriodCredit"] = "Все периоды";
                    row["isMain"] = 0;
                    dtResult.Rows.Add(row);
                    dtResult.AcceptChanges();
                    dtResult.DefaultView.Sort = "isMain asc, PeriodCredit desc";
                    dtResult = dtResult.DefaultView.ToTable().Copy();
                }
            }
            else
            {
                
                dtResult.DefaultView.Sort = "PeriodCredit desc";
                dtResult = dtResult.DefaultView.ToTable().Copy();
            }

            return dtResult;
        }

        /// <summary>
        /// Получение списка пений за период
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getPenalty(string period)
        {
            ap.Clear();
            ap.Add(period);

            DataTable dtResult = executeProcedure("[Arenda].[spg_getPenalty]",
                 new string[1] { "@period" },
                 new DbType[1] { DbType.String }, ap);

            return dtResult;
        }

        /// <summary>
        /// Сохранение тела пенальти
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> setPenalty(int id, decimal SummaPenalty, decimal PercentPenalty)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(SummaPenalty);
            ap.Add(PercentPenalty);
            ap.Add(UserSettings.User.Id);

            DataTable dtResult = executeProcedure("[Arenda].[spg_setPenalty]",
                 new string[4] { "@id", "@SummaPenalty", "@PercentPenalty","@id_user" },
                 new DbType[4] { DbType.Int32, DbType.Decimal, DbType.Decimal, DbType.Int32 }, ap);

            return dtResult;
        }

        /// <summary>
        /// Подтверждение пений Д
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> setFines(int id_Agreements, int id, decimal SummaPenalty, string period)
        {
            ap.Clear();
            ap.Add(id_Agreements);
            ap.Add(id);
            ap.Add(SummaPenalty);
            ap.Add(period);
            ap.Add(UserSettings.User.Id);

            DataTable dtResult = executeProcedure("[Arenda].[spg_setFines]",
                 new string[5] { "@id_Agreements", "@id", "@summPenalty", "@period", "@id_user" },
                 new DbType[5] { DbType.Int32, DbType.Int32, DbType.Decimal, DbType.String, DbType.Int32 }, ap);

            return dtResult;
        }
    }
}
