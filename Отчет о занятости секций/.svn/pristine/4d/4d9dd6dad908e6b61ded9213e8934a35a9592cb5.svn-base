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

namespace ArendaViewSection
{
    class Procedures : SqlProvider
    {
        public Procedures(string server, string database, string username, string password, string appName)
             : base(server, database, username, password, appName)
        {
        }
        ArrayList ap = new ArrayList();

        #region Combobox's
        /// <summary>
        /// получение зданий
        /// </summary>
        /// <returns></returns>
        public async Task<DataTable> getBuildings()
        {
            ap.Clear();
            DataTable dtTemp = executeProcedure("[Arenda].[spg_getBuilding]",
                new string[] { }, new DbType[] { }, ap);
            if (dtTemp!= null)
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
        /// <summary>
        /// получение объектов
        /// </summary>
        /// <returns></returns>
        public async Task<DataTable> getObjectsLease()
        {
            ap.Clear();
            DataTable dtTemp = executeProcedure("[Arenda].[spg_getObjectLease]",
                new string[] { }, new DbType[] { }, ap);
            if (dtTemp != null)
            {
                DataRow dr = dtTemp.NewRow();
                dr["id"] = 0;
                dr["cName"] = "Все объекты";
                dr["isActive"] = 1;
                dtTemp.Rows.InsertAt(dr, 0);
                dtTemp.AcceptChanges();
                dtTemp.DefaultView.RowFilter = "isActive = 1";
            }
            return dtTemp.DefaultView.ToTable().Copy();
        }

        #endregion

        public async Task<DataTable> getData()
        {
            ap.Clear();
            return executeProcedure("[Arenda].[kav_getCurrentSections]",
                new string[] { },
                new DbType[] { }, ap);
        }

    }
}
