using System.Text;
using System.Collections;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.Connection;
using System.Data;
using System;
using Nwuram.Framework.Settings.User;
using System.Threading.Tasks;
using System.Threading;

namespace dllArendaDictonary
{
    class Procedures : SqlProvider
    {
        public Procedures(string server, string database, string username, string password, string appName)
              : base(server, database, username, password, appName)
        {
        }
        ArrayList ap = new ArrayList();

        #region "Справочник рекламных мест"

        /// <summary>
        /// Запись справочника типов отзывов
        /// </summary>
        /// <param name="id">Код записи</param>
        /// <param name="cName">Наименование </param>
        /// <param name="Abbreviation">Аббревиатура</param>
        /// <param name="isActive">признак активности записи</param>  
        /// <param name="isDel">Признак удаления записи</param>
        /// <param name="result">Результирующая для проверки</param>
        /// <returns>Таблица с данными</returns>
        /// <param name="id">код созданной записи</param>
        public async Task<DataTable> setReclamaPlace(int id, string NumberPlace,int id_ObjectLease,int id_Building,Int64 Length,Int64 Width,  bool isActive, bool isDel, int result)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(NumberPlace);
            ap.Add(id_ObjectLease);
            ap.Add(id_Building);
            ap.Add(Length);
            ap.Add(Width);
            ap.Add(isActive);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(result);
            ap.Add(isDel);

            DataTable dtResult = executeProcedure("[Arenda].[spg_setReclamaPlace]",
                 new string[10] { "@id","@NumberPlace", "@id_ObjectLease", "@id_Building", "@Length", "@Width", "@isActive", "@id_user", "@result", "@isDel" },
                 new DbType[10] { DbType.Int32, DbType.String,DbType.Int32,DbType.Int32,DbType.Int64,DbType.Int64,DbType.Boolean, DbType.Int32, DbType.Int32, DbType.Boolean }, ap);

            return dtResult;
        }
           
        /// <summary>
        /// Получение справочника отделов
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getBuilding(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Arenda].[spg_getBuilding]",
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

                    row["cName"] = "Все Здания";
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
                    dtResult.Rows.Add(row);
                    dtResult.AcceptChanges();
                    dtResult.DefaultView.Sort = "isMain asc, cName asc";
                    dtResult = dtResult.DefaultView.ToTable().Copy();
                }
            }

            return dtResult;
        }

        /// <summary>
        /// Получение списка рекламных мест
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getReclamaPlace()
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Arenda].[spg_getReclamaPlace]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            return dtResult;
        }

        #endregion

        #region "Справочник земельных участков"

        /// <summary>
        /// Запись справочника типов отзывов
        /// </summary>
        /// <param name="id">Код записи</param>
        /// <param name="cName">Наименование </param>
        /// <param name="Abbreviation">Аббревиатура</param>
        /// <param name="isActive">признак активности записи</param>  
        /// <param name="isDel">Признак удаления записи</param>
        /// <param name="result">Результирующая для проверки</param>
        /// <returns>Таблица с данными</returns>
        /// <param name="id">код созданной записи</param>
        public async Task<DataTable> setLandPlot(int id, string NumberPlot, int id_ObjectLease,  Int64 AreaPlot, bool isActive, bool isDel, int result)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(NumberPlot);
            ap.Add(id_ObjectLease);
            ap.Add(AreaPlot);
            ap.Add(isActive);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(result);
            ap.Add(isDel);

            DataTable dtResult = executeProcedure("[Arenda].[spg_setLandPlot]",
                 new string[8] { "@id", "@NumberPlot", "@id_ObjectLease", "@AreaPlot", "@isActive", "@id_user", "@result", "@isDel" },
                 new DbType[8] { DbType.Int32, DbType.String, DbType.Int32, DbType.Int64, DbType.Boolean, DbType.Int32, DbType.Int32, DbType.Boolean }, ap);

            return dtResult;
        }

        /// <summary>
        /// Получение списка рекламных мест
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getLandPlot()
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Arenda].[spg_getLandPlot]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            return dtResult;
        }

        #endregion

        #region "Справочник земельных участков"

        /// <summary>
        /// Запись справочника типов отзывов
        /// </summary>
        /// <param name="id">Код записи</param>
        /// <param name="cName">Наименование </param>
        /// <param name="Abbreviation">Аббревиатура</param>
        /// <param name="isActive">признак активности записи</param>  
        /// <param name="isDel">Признак удаления записи</param>
        /// <param name="result">Результирующая для проверки</param>
        /// <returns>Таблица с данными</returns>
        /// <param name="id">код созданной записи</param>
        public async Task<DataTable> setTypeActivities(int id, string cName, bool isActive, bool isDel, int result)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(cName);
            ap.Add(isActive);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(result);
            ap.Add(isDel);

            DataTable dtResult = executeProcedure("[Arenda].[spg_setTypeActivities]",
                 new string[6] { "@id", "@cName", "@isActive", "@id_user", "@result", "@isDel" },
                 new DbType[6] { DbType.Int32, DbType.String, DbType.Boolean, DbType.Int32, DbType.Int32, DbType.Boolean }, ap);

            return dtResult;
        }

        /// <summary>
        /// Получение списка видов дейстельности
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getTypeActivities()
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Arenda].[spg_getTypeActivities]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            return dtResult;
        }

        #endregion

        #region "Журнал скидок"

        /// <summary>
        /// Получение справочника отделов
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getTypeAgreements(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Arenda].[spg_getTypeAgreements]",
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

                    row["cName"] = "Все Здания";
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

        public async Task<DataTable> getTypeDiscount(bool withAllDeps = false)
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

                    row["cName"] = "Все Здания";
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

        public async Task<DataTable> getTypeTenant(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Arenda].[spg_getTypeTenant]",
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

                    row["cName"] = "Все Здания";
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

        public async Task<DataTable> getFloors(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Arenda].[spg_getFloors]",
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

                    row["cName"] = "Все Этажи";
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

        public async Task<DataTable> getObjectDiscount(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Arenda].[spg_getObjectDiscount]",
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
                    dtResult.Rows.Add(row);
                    dtResult.AcceptChanges();
                    dtResult.DefaultView.Sort = "isMain asc, cName asc";
                    dtResult = dtResult.DefaultView.ToTable().Copy();
                }
            }

            return dtResult;
        }

        public async Task<DataTable> getSections(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Arenda].[spg_getSections]",
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

                    row["cName"] = "Все Секции";
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

        public async Task<DataTable> setTDiscount(int id, DateTime dateStart, DateTime? dateEnd,int id_typeDiscount,int id_TypeTenant,int id_TypeAgreements,int id_StatusDiscount, bool isActive, bool isDel, int result)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(dateStart);
            ap.Add(dateEnd);
            ap.Add(id_typeDiscount);
            ap.Add(id_TypeTenant);
            ap.Add(id_TypeAgreements);
            ap.Add(id_StatusDiscount);

            ap.Add(isActive);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(result);
            ap.Add(isDel);

            DataTable dtResult = executeProcedure("[Arenda].[spg_setTDiscount]",
                 new string[11] { "@id", "@dateStart", "@dateEnd", "@id_typeDiscount", "@id_TypeTenant", "@id_TypeAgreements", "@id_StatusDiscount", "@isActive", "@id_user", "@result", "@isDel" },
                 new DbType[11] { DbType.Int32, DbType.Date,DbType.Date,DbType.Int32,DbType.Int32,DbType.Int32,DbType.Int32, DbType.Boolean, DbType.Int32, DbType.Int32, DbType.Boolean }, ap);

            return dtResult;
        }

        public async Task<DataTable> setDiscountValue(int id, int id_tDiscount, decimal? PercentDiscount, decimal? DiscountPrice, decimal? Price, decimal? TotalPrice, bool isActive, bool isDel, int result)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_tDiscount);
            ap.Add(PercentDiscount);
            ap.Add(DiscountPrice);
            ap.Add(Price);
            ap.Add(TotalPrice);
            ap.Add(isActive);
            ap.Add(UserSettings.User.Id);
            ap.Add(result);
            ap.Add(isDel);

            DataTable dtResult = executeProcedure("[Arenda].[spg_setDiscountValue]",
                 new string[10] { "@id", "@id_tDiscount", "@PercentDiscount", "@DiscountPrice", "@Price", "@TotalPrice", "@isActive", "@id_user", "@result", "@isDel" },
                 new DbType[10] { DbType.Int32, DbType.Int32, DbType.Decimal, DbType.Decimal, DbType.Decimal, DbType.Decimal, DbType.Boolean, DbType.Int32, DbType.Int32, DbType.Boolean }, ap);

            return dtResult;
        }

        /*public async Task<DataTable> setDiscountObject(int id, int id_tDiscount, int? id_ObjectLease, int? id_Buildings,int? id_Floor,int? id_Sections,int? id_LandPlot,int? id_ReclamaPlace,bool isException, bool isActive, bool isDel, int result)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_tDiscount);
            ap.Add(id_ObjectLease);
            ap.Add(id_Buildings);
            ap.Add(id_Floor);
            ap.Add(id_Sections);
            ap.Add(id_LandPlot);
            ap.Add(id_ReclamaPlace);
            ap.Add(isException);
            ap.Add(isActive);
            ap.Add(UserSettings.User.Id);
            ap.Add(result);
            ap.Add(isDel);

            DataTable dtResult = executeProcedure("[Arenda].[spg_setDiscountObject]",
                 new string[13] { "@id", "@id_tDiscount", "@id_ObjectLease", "@id_Buildings", "@id_Floor", "@id_Sections", "@id_LandPlot", "@id_ReclamaPlace","@isException", "@isActive", "@id_user", "@result", "@isDel" },
                 new DbType[13] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Boolean, DbType.Boolean, DbType.Int32, DbType.Int32, DbType.Boolean }, ap);

            return dtResult;
        }*/

        public async Task<DataTable> setDiscountObject(int id, int id_tDiscount, int id_ObjectLease, int? id_Buildings, int? id_Floor, int id_rentalObject, int typeRentalObject, bool isException, bool isActive, bool isDel, int result)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_tDiscount);
            ap.Add(id_ObjectLease);
            ap.Add(id_Buildings);
            ap.Add(id_Floor);            
            ap.Add(id_rentalObject);
            ap.Add(typeRentalObject);
            ap.Add(isException);
            ap.Add(isActive);
            ap.Add(UserSettings.User.Id);
            ap.Add(result);
            ap.Add(isDel);

            DataTable dtResult = executeProcedure("[Arenda].[spg_setDiscountObject]",
                 new string[12] { "@id", "@id_tDiscount", "@id_ObjectLease", "@id_Buildings", "@id_Floor", "@id_rentalObject", "@typeRentalObject", "@isException", "@isActive", "@id_user", "@result", "@isDel" },
                 new DbType[12] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Boolean, DbType.Boolean, DbType.Int32, DbType.Int32, DbType.Boolean }, ap);

            return dtResult;
        }


        public async Task<DataTable> getTDiscount()
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Arenda].[spg_getTDiscount]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            return dtResult;
        }

        public async Task<DataTable> getDiscountValue(int id_tDiscount)
        {
            ap.Clear();
            ap.Add(id_tDiscount);

            DataTable dtResult = executeProcedure("[Arenda].[spg_getDiscountValue]",
                 new string[1] { "@id_tDiscount" },
                 new DbType[1] {DbType.Int32 }, ap);

            return dtResult;
        }

        public async Task<DataTable> getDiscountObject(int id_tDiscount)
        {
            ap.Clear();
            ap.Add(id_tDiscount);

            DataTable dtResult = executeProcedure("[Arenda].[spg_getDiscountObject]",
                 new string[1] { "@id_tDiscount" },
                 new DbType[1] { DbType.Int32 }, ap);

            return dtResult;
        }

        #endregion
    }
}
