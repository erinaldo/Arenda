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

namespace ArendaDiscount
{
    class Procedures : SqlProvider
    {
        ArrayList ap = new ArrayList();

        public Procedures(string server, string database, string username, string password, string appName)
            : base(server, database, username, password, appName)
        {           

        }

        public async Task<DataTable> getTypeDiscount()
        {
            ap.Clear();
            return executeProcedure("[Arenda].[spg_getTypeDiscount]",
                new string[] { },
                new DbType[] { }, ap);
        }

        public async Task<DataTable> GetContractTypes()
        {
            ap.Clear();
            return executeProcedure("Arenda.GetContractTypes", new string[] { }, new DbType[] { }, ap);
        }

        /// <summary>
        /// получение зданий
        /// </summary>
        /// <returns></returns>
        public async Task<DataTable> getBuildings()
        {
            ap.Clear();
            DataTable dtTemp = executeProcedure("[Arenda].[spg_getBuilding]",
                new string[] { }, new DbType[] { }, ap);
            if (dtTemp != null)
            {
                DataRow dr = dtTemp.NewRow();
                dr["id"] = 0;
                dr["cName"] = "Все здания";
                dr["isActive"] = 1;
                dtTemp.Rows.InsertAt(dr, 0);
                dtTemp.AcceptChanges();
                dtTemp.DefaultView.RowFilter = "isActive = 1";
            }
            return dtTemp.DefaultView.ToTable().Copy();
        }

        /// <summary>
        /// получение этажей
        /// </summary>
        /// <returns></returns>
        public async Task<DataTable> getFloors()
        {
            ap.Clear();
            DataTable dtTemp = executeProcedure("[Arenda].[spg_getFloors]",
                new string[] { }, new DbType[] { }, ap);
            if (dtTemp != null)
            {
                DataRow dr = dtTemp.NewRow();
                dr["id"] = 0;
                dr["cName"] = "Все этажи";
                dr["isActive"] = 1;
                dtTemp.Rows.InsertAt(dr, 0);
                dtTemp.AcceptChanges();
                dtTemp.DefaultView.RowFilter = "isActive = 1";
            }
            return dtTemp.DefaultView.ToTable().Copy();
        }

        public async Task<DataTable> getObjectsLease()
        {
            ap.Clear();
            DataTable dtTemp = executeProcedure("[Arenda].[spg_getObjectLease]",
                new string[] { }, new DbType[] { }, ap); 
            dtTemp.DefaultView.RowFilter = "isActive = 1";
            return dtTemp.DefaultView.ToTable().Copy();
        }

        public async Task<DataTable> getSectionsDiscount(DateTime dateStart, DateTime dateEnd)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(dateEnd);
            return executeProcedure("[Arenda].[kav_getSectionsDiscount]",
                new string[] {"@dateStart", "@dateEnd" },
                new DbType[] { DbType.DateTime, DbType.DateTime }, ap);
        }

        public async Task<DataTable>validateDiscount(string id_agreements, DateTime dateStart, DateTime dateEnd, bool permanentDiscount)
        {
            ap.Clear();
            ap.Add(id_agreements);
            ap.Add(dateStart.Date);
            ap.Add(dateEnd.Date);
            ap.Add(permanentDiscount);
            return executeProcedure("[Arenda].[kav_validateDiscounts]",
                new string[] { "@id_agreements", "@date_start", "@date_end", "@permanent" },
                new DbType[] { DbType.String, DbType.DateTime, DbType.DateTime, DbType.Boolean }, ap);
        }

        public async Task<DataTable> setDiscounts(string id_agreements, DateTime dateStart, DateTime dateEnd, int id_typeDiscount, decimal discount, bool permanent)
        {
            ap.Clear();
            ap.Add(id_agreements);
            ap.Add(dateStart);
            ap.Add(dateEnd);
            ap.Add(id_typeDiscount);
            ap.Add(discount);
            ap.Add(UserSettings.User.Id);
            ap.Add(permanent);
            return executeProcedure("[Arenda].[kav_setDiscounts]",
                new string[] { "@id_agreements", "@date_start", "@date_end", "@id_TypeDiscount", "@discount", "@id_user", "@permanent" },
                new DbType[] { DbType.String, DbType.DateTime, DbType.DateTime, DbType.Int32, DbType.Decimal, DbType.Int32, DbType.Boolean }, ap);
        }
    }

    enum logEnum
    {
        Добавление_объекта_аренды = 1592,
        Редактирование_объекта_аренды = 1593,
        Удаление_объекта_аренды = 1594,
        Добавление_план_отчета = 1595,
        Редактирование_план_отчета = 1596,
        Удалить_план_отчет = 1597,
        Создание_массовой_скидки = 1598,
        Создание_скидки = 1599,
        Удаленеие_скидки = 1600,
        Подтверждение_договора = 1601,
        Подтверждение_скидки = 1602,
        Отклонение_скидки = 1603,
        Подтверждение_пени = 1604,
        Подтверждение_съезда = 1605,
        Отмена_подтверждения_договора = 1606,
        Подтвержение_счета = 1607,
        Подтверждение_план_отчета = 1608,
        Подтверждение_ежемесячного_плана = 1609,
        Создание_ежемесячного_плана = 1610,
        Добавление_ежемесячного_плана = 1611,
        Редактирование_ежемесячного_плана = 1612,
        Удаление_ежемесячного_плана = 1613,
        Добавление_арендодателя = 1614,
        Редактирование_арендодателя = 1615,
        Отмена_подтверждения_счета = 1616
    }
}
