using System.Text;
using System.Collections;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.Connection;
using System.Data;
using System;
using Nwuram.Framework.Settings.User;
using System.Threading.Tasks;
using System.Threading;

namespace dllJournalPlaneReport
{
    class Procedures : SqlProvider
    {
        public Procedures(string server, string database, string username, string password, string appName)
              : base(server, database, username, password, appName)
        {
        }
        ArrayList ap = new ArrayList();

        /// <summary>
        /// Получение списка журнала ежемесячных планов
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getPlaneReportDopInfoPay(int id, DateTime date)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(date);


            return executeProcedure("[Arenda].[spg_getPlaneReportDopInfoPay]",
                 new string[2] { "@id", "@date" },
                 new DbType[2] { DbType.Int32, DbType.Date }, ap);
        }

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
        /// Получение списка журнала ежемесячных планов
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getTPlanReport(DateTime dateStart, DateTime dateEnd)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(dateEnd);


            return executeProcedure("[Arenda].[spg_getTPlanReport]",
                 new string[2] { "@dateStart", "@dateEnd" },
                 new DbType[2] { DbType.Date, DbType.Date }, ap);
        }

        /// <summary>
        /// Получение списка журнала ежемесячных планов
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getTPlanReport(DateTime dateStart, DateTime dateEnd, int id_objectLease)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(dateEnd);
            ap.Add(id_objectLease);

            return executeProcedure("[Arenda].[spg_getTPlanReport]",
                 new string[3] { "@dateStart", "@dateEnd", "@id_objectLease" },
                 new DbType[3] { DbType.Date, DbType.Date, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Создание или удаления заголовка ежемесячного плана
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> setTPlanReport(int id, DateTime PeriodMonthPlan, int id_ObjectLease, bool? isСonfirmed, bool isDel, int result)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(PeriodMonthPlan);
            ap.Add(id_ObjectLease);
            ap.Add(isСonfirmed);
            ap.Add(isDel);
            ap.Add(result);
            ap.Add(UserSettings.User.Id);


            return executeProcedure("[Arenda].[spg_setTPlanReport]",
                 new string[7] { "@id", "@PeriodMonthPlan", "@id_ObjectLease", "@isСonfirmed", "@isDel", "@result", "@id_user" },
                 new DbType[7] { DbType.Int32, DbType.Date, DbType.Int32, DbType.Boolean, DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        ////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Получение тела план отчёта
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getTDiscount(int id_Agreements)
        {
            ap.Clear();
            ap.Add(id_Agreements);

            return executeProcedure("[Arenda].[spg_getTDiscount]",
                 new string[1] { "@id_Agreements" },
                 new DbType[1] { DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение тела план отчёта
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getPlanReport(DateTime dateStart,int id_ObjectLease)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(id_ObjectLease);

            return executeProcedure("[Arenda].[spg_getPlanReport]",
                 new string[2] { "@date", "@id_ObjectLease" },
                 new DbType[2] { DbType.Date,DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение тела журнала ежемесячных планов
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getPlanReport(DateTime dateStart, int id_ObjectLease,int id_tMonthPlane)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(id_ObjectLease);
            ap.Add(id_tMonthPlane);

            return executeProcedure("[Arenda].[spg_getPlanReport]",
                 new string[3] { "@date", "@id_ObjectLease", "@id_tPlanReport" },
                 new DbType[3] { DbType.Date, DbType.Int32, DbType.Int32 }, ap);
        }



        /// <summary>
        /// Создание или удаления тела ежемесячного плана
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> spg_setPlanReport(int id_tPlanReport, int id_Agreements, decimal SummaContract, decimal Discount, decimal SecurityPayment, decimal EndPlan,
            decimal Penalty, decimal OtherPayments, decimal TotalPaid, decimal Included, decimal Credit, decimal OverPayment, bool isDel)
        {
            ap.Clear();
            ap.Add(id_tPlanReport);
            ap.Add(id_Agreements);
            ap.Add(SummaContract);
            ap.Add(Discount);
            ap.Add(SecurityPayment);

            ap.Add(EndPlan);
            ap.Add(Penalty);
            ap.Add(OtherPayments);
            ap.Add(TotalPaid);
            ap.Add(Included);
            ap.Add(Credit);
            ap.Add(OverPayment);

            ap.Add(isDel);

            return executeProcedure("[Arenda].[spg_setPlanReport]",
                 new string[13] { "@id_tPlanReport", "@id_Agreements", "@SummaContract", "@Discount", "@SecurityPayment","@EndPlan","@Penalty", "@OtherPayments", "@TotalPaid", "@Included",
                     "@Credit","@OverPayment","@isDel" },
                 new DbType[13] { DbType.Int32, DbType.Int32, DbType.Decimal, DbType.Decimal, DbType.Decimal, DbType.Decimal, DbType.Decimal, DbType.Decimal, DbType.Decimal,
                     DbType.Decimal, DbType.Decimal, DbType.Decimal, DbType.Boolean }, ap);
        }

    }
}
