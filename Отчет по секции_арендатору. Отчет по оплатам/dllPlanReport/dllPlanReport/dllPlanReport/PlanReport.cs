using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nwuram.Framework.ToExcelNew;

namespace dllPlanReport
{
    class PlanReport
    {
        private DataTable dtResult;
        private Dictionary<int, DataTable> listPay = new Dictionary<int, DataTable>();
        private Dictionary<int, DataTable> listLeave = new Dictionary<int, DataTable>();
        DataTable dtLeaves;
        public DataTable dtReport;
        public int cAgrCount;
        public PlanReport(int id_Object, int id_Building, int id_Floor, int id_Section, int id_Tenant)
        {
            Task<DataTable> task = Config.hCntMain.getDataReport(id_Object, id_Building, id_Floor, id_Section, id_Tenant);
            task.Wait();
            dtReport = task.Result;
        }

        public PlanReport()
        {
          
        }

        public bool createTable()
        {
            Task<DataTable> task = Config.hCntMain.getPlanAgreement();
            task.Wait();
            DataTable dtAgreements = task.Result;
            if (dtAgreements == null)
                return false;
            task = Config.hCntMain.getDiscounts();
            task.Wait();
            DataTable dtDiscounts = task.Result;
            if (dtDiscounts == null)
                return false;
            task = Config.hCntMain.getLeaves();
            task.Wait();
            dtLeaves = task.Result;
            if (dtLeaves == null)
                return false;
            int i = 0;
            // по каждому договору
            foreach (DataRow dr in dtAgreements.Rows)
            {
                cAgrCount = i/dtAgreements.Rows.Count;
                i++;
                // таблица - расчет стоимости по дням с учетом скидок
                int id_Agreemets = (int)dr["id"];
                string name_Agreements = dr["Agreement"].ToString();

                DataTable dtPriceDays = new DataTable();
                dtPriceDays.Columns.Add("day", typeof(DateTime));
                dtPriceDays.Columns.Add("price", typeof(decimal));
                DateTime dateStart = (DateTime)dr["dateStart"];
                DateTime dateEnd = (DateTime)dr["dateEnd"];
                decimal price = decimal.Parse(dr["Total_Sum"].ToString());
                decimal area = decimal.Parse(dr["Total_Area"].ToString());
                decimal phone = decimal.Parse(dr["Phone"].ToString());

                DateTime dateIterator = dateStart;
                while (dateIterator <= dateEnd)
                {
                    dtPriceDays.Rows.Add(dateIterator, Math.Round(price / DateTime.DaysInMonth(dateIterator.Year, dateIterator.Month), 2));
                    dateIterator = dateIterator.AddDays(1);
                }

                EnumerableRowCollection<DataRow> rDiscounts = dtDiscounts.AsEnumerable().Where(r => r.Field<int>("id_Agreements") == id_Agreemets);
                if (rDiscounts.Count() > 0)
                {
                    foreach (DataRow drDays in dtPriceDays.Rows)
                    {
                        DateTime discountArea = new DateTime(1900, 1, 1);
                        //скидки на кв.м.
                        EnumerableRowCollection<DataRow> rowcoll = rDiscounts
                            .Where(r => r.Field<DateTime>("DateStart") <= (DateTime)drDays["day"] && (r.Field<DateTime?>("DateEnd") is null || r.Field<DateTime?>("DateEnd") >= (DateTime)drDays["day"])
                            && r.Field<int>("id_TypeDiscount") == 2).OrderByDescending(o => o.Field<DateTime>("DateStart"));
                        if (rowcoll.Count() > 0)
                        {
                            discountArea = (DateTime)rowcoll.First()["DateStart"];
                            drDays["price"] = Math.Round((decimal.Parse(rowcoll.First()["Discount"].ToString()) * area + phone)
                                    / DateTime.DaysInMonth(DateTime.Parse(drDays["day"].ToString()).Year, DateTime.Parse(drDays["day"].ToString()).Month), 2);
                        }
                        //процент

                        rowcoll = rDiscounts
                            .Where(r => r.Field<DateTime>("DateStart") <= (DateTime)drDays["day"] && (r.Field<DateTime?>("DateEnd") is null || r.Field<DateTime?>("DateEnd") >= (DateTime)drDays["day"])
                            && r.Field<int>("id_TypeDiscount") == 1).OrderByDescending(o => o.Field<DateTime>("DateStart"));
                        if (rowcoll.Count() > 0)
                        {
                            if ((DateTime)rowcoll.First()["DateStart"] > discountArea)
                                drDays["price"] = Math.Round((100 - decimal.Parse(rowcoll.First()["Discount"].ToString())) / 100 * decimal.Parse(drDays["price"].ToString()), 2);
                        }
                    }
                }

                DataTable resultTable = new DataTable();
                resultTable.Columns.Add("month", typeof(int)); //месяц цифрой
                resultTable.Columns.Add("year", typeof(int)); // год цифврой
                resultTable.Columns.Add("price", typeof(decimal)); // оплата за месяц
                resultTable.Columns.Add("date", typeof(DateTime)); // дата датой  с первым числом
                //resultTable.Columns.Add("sumPrice", typeof(decimal));// сумма по месяцам до месяца
                var temp = dtPriceDays.AsEnumerable()
                    .GroupBy(t => new
                    {
                        month = t.Field<DateTime>("day").Month,
                        year = t.Field<DateTime>("day").Year
                    })
                    .Select(s =>
                    {
                        DataRow drow = resultTable.NewRow();
                        drow["month"] = s.Key.month;
                        drow["year"] = s.Key.year;
                        drow["price"] = decimal.Round(s.Sum(r => r.Field<decimal>("price")));
                        drow["date"] = new DateTime(s.Key.year, s.Key.month, 1);
                        //drow["sumPrice"] = 0;
                        return drow;

                    });
                if (temp.Count() > 0)
                    resultTable = temp.CopyToDataTable();
                /*foreach (DataRow dr in resultTable.Rows)
                {
                    dr["sumPrice"] = resultTable.AsEnumerable().Where(r => r.Field<DateTime>("date") <= (DateTime)dr["date"]).Sum(r => r.Field<decimal>("price"));
                }*/

                //список договоров для второй таблицы (с заявкой на съезд)
                EnumerableRowCollection<DataRow> rowsLeave = dtLeaves.AsEnumerable().Where(r => r.Field<int>("id") == id_Agreemets);
                if (rowsLeave.Count() > 0)
                {
                    DataTable dtLastMonth = new DataTable();
                    dtLastMonth.Columns.Add("price", typeof(decimal));
                    dtLastMonth.Columns.Add("date", typeof(DateTime));
                    dtLastMonth = dtPriceDays.AsEnumerable()
                        .Where(r => r.Field<DateTime>("day").Month == DateTime.Parse(rowsLeave.First()["Date_of_Departure"].ToString()).Month &&
                        r.Field<DateTime>("day").Year == DateTime.Parse(rowsLeave.First()["Date_of_Departure"].ToString()).Year &&
                        r.Field<DateTime>("day") <= DateTime.Parse(rowsLeave.First()["Date_of_Departure"].ToString()))
                        .GroupBy(g => new { month = g.Field<DateTime>("day").Month, year = g.Field<DateTime>("day").Year })
                        .Select(s =>
                        {
                            DataRow drow = dtLastMonth.NewRow();
                            drow["date"] = new DateTime(s.Key.year, s.Key.month, 1);
                            drow["price"] = decimal.Round(s.Sum(r => r.Field<decimal>("price")));
                            return drow;
                        }).CopyToDataTable();
                    listLeave.Add(id_Agreemets, dtLastMonth);
                }

                listPay.Add(id_Agreemets, resultTable);
            }
            insertData();
            return true;
        }

        public void showReport()
        {
            ExcelUnLoad rep = new ExcelUnLoad();
            int crow = 1;
            rep.AddSingleValue("Стоимости договоров по месяцам", crow, 1);
            crow+=2;
            foreach (var a in listPay)
            {
                DataTable dtData = a.Value;
                int col = 2;
                rep.AddSingleValue($"Договор: {a.Key}", crow, 1);
                foreach (DataRow dr in dtData.Rows)
                {
                    rep.AddSingleValue(dr["date"].ToString(), crow, col);
                    rep.AddSingleValue(dr["price"].ToString(), crow + 1, col);
                    col++;
                }
                crow += 3;

            }
            rep.GoToNextSheet("С аннуляциями");
            crow = 1;
            rep.AddSingleValue("Договора с аннуляциями", crow, 1);
            crow += 2;
            foreach (var a in listLeave)
            {
                rep.AddSingleValue($"Договор id: {a.Key}", crow, 1);
                int col = 2;
                foreach (DataRow dr in a.Value.Rows)
                {
                    rep.AddSingleValue(dr["date"].ToString(), crow, col);
                    rep.AddSingleValue(dr["price"].ToString(), crow + 1, col);
                    col++;
                }
                crow += 3;
            }
            
            rep.Show();
        }

        ExcelUnLoad rep;
        private Dictionary<int, string> listMonth = new Dictionary<int, string>() { { 1, "январь" }, { 2, "февраль" }, { 3, "март"}, { 4, "апрель" }, { 5, "май"}, { 6, "июнь"}, { 7, "июль"},
            { 8, "август"}, { 9, "сентябрь"}, { 10, "октябрь"}, { 11, "ноябрь"}, {12, "декабрь" } };

        string monthPlan = "";
        string datePlan = "";
        public void createReportSection()
        {
            /*Task<DataTable> task = Config.hCntMain.getDataReport();
            task.Wait();
            dtReport = task.Result;*/
            if (dtReport == null)
                return;
            monthPlan = listMonth[DateTime.Parse(dtReport.Rows[0]["monthX"].ToString()).Month];
            DateTime dayX = DateTime.Parse(dtReport.Rows[0]["dayX"].ToString());
            datePlan = $"{dayX.Day}.{dayX.Month}";
            rep = new ExcelUnLoad();
            int crow = 1;
            int startRow = 1;
            AddHeaderSection(ref crow);
            var objects = dtReport.AsEnumerable().GroupBy(g => g.Field<string>("objectName")).Select(s=>s.Key);
            //индекс секции для подкраски строчки
            int indexSection = 0;
            foreach (string obj in objects)
            {
                rep.AddSingleValue(obj, crow, 1);
                int startObject = crow;
                //дататейбл по объекту
                EnumerableRowCollection<DataRow> rObject = dtReport.AsEnumerable().Where(r => r.Field<string>("objectName") == obj);
                //секциии
                var sections = rObject.GroupBy(g => g.Field<string>("nameSection")).Select(s => s.Key);
                foreach (string sect in sections)
                {
                    rep.AddSingleValue(sect, crow, 2);
                    int startSection = crow;
                    
                    //дататейбл по обхекту и секции
                    EnumerableRowCollection<DataRow> rSection = rObject.Where(r => r.Field<string>("nameSection") == sect);
                    //арендаторы
                    var tenants = rSection.GroupBy(g => new { id_tenant = g.Field<int>("tenant_id"), name = g.Field<string>("tenant_Name") }).Select(r => new{ r.Key.name, r.Key.id_tenant});
                    foreach (var tenant in tenants)
                    {
                        rep.AddSingleValue(tenant.name, crow, 3);
                        int startTenant = crow;
                        //договоры
                        EnumerableRowCollection<DataRow> rAgrements = rSection.Where(r => r.Field<int>("tenant_id") == tenant.id_tenant);
                        foreach (DataRow dr in rAgrements)
                        {
                            rep.AddSingleValueObject(dr["Agreement"], crow, 4);
                            rep.AddSingleValueObject(dr["Total_Sum"], crow, 5);
                            rep.AddSingleValueObject(dr["SummaPlanView"], crow, 6);
                            decimal before25day = (dr["payments"] == DBNull.Value ? 0 : (decimal)dr["payments"]);
                            if (before25day < 0)
                                rep.AddSingleValueObject((-1*before25day), crow, 7);
                            if (before25day > 0)
                                rep.AddSingleValueObject(before25day, crow, 8);
                            decimal pay = (dr["paymentsNew"] == DBNull.Value ? 0 : (decimal)dr["paymentsNew"]);
                            if (pay > 0)
                                rep.AddSingleValueObject(pay, crow, 11);
                            decimal curDebt = before25day + pay;
                            if (curDebt < 0)
                                rep.AddSingleValueObject((-curDebt), crow, 13);
                            if (curDebt > 0)
                                rep.AddSingleValueObject(curDebt, crow, 15);


                            decimal currDown = (decimal)dr["percDebt"];
                            if (currDown!=0 && curDebt<0)
                            {
                                rep.AddSingleValue($"{((curDebt / -currDown) * 100).ToString("0.00")}%", crow, 14);
                            }

                            rep.AddSingleValue(dr["landLord_name"].ToString(), crow, 18);
                            crow++;
                        }
                        rep.Merge(startTenant, 3, crow - 1, 3);
                        rep.SetCellAlignmentToJustify(startTenant, 3, crow - 1, 3);
                    }
                    //итого долг на 25 число
                    EnumerableRowCollection<DataRow> rCol = rSection
                        .Where(r => r.Field<object>("payments")!=null);
                    // переписано, теперь тупо все суммируется и если >0 то переплата, если <0 то долг
                    decimal rowSumm = rCol.Sum(s => s.Field<decimal>("payments"));
                    if (rowSumm>0)
                        rep.AddSingleValueObject(rowSumm, startSection, 10);
                    if (rowSumm<0)
                        rep.AddSingleValueObject(-rowSumm, startSection, 9);
                    /*decimal rowSumm = rCol
                        .Where(r => r.Field<decimal>("payments") < 0)
                        .Sum(s => s.Field<decimal>("payments"));
                    if (rowSumm!=0)
                        rep.AddSingleValueObject(-rowSumm, startSection, 9);
                    //переплата на 25 число
                    rowSumm = rCol
                        .Where(r=> r.Field<decimal>("payments") > 0)
                        .Sum(s => s.Field<decimal>("payments"));
                    if (rowSumm!=0)
                        rep.AddSingleValueObject(rowSumm, startSection, 10);
                    */
                    //сумма оплаты
                    rCol = rSection
                        .Where(r => r.Field<object>("paymentsNew") != null);
                    rowSumm = rCol
                        .Sum(s => s.Field<decimal>("paymentsNew"));
                    if (rowSumm!=0)
                        rep.AddSingleValueObject(rowSumm, startSection, 12);
                    //итого долг - считается сумма текущего по секции - если в -, то долг, если в +, то переплата... для - считается переплата
                    
                    rowSumm = rSection.Sum(s => s.Field<decimal>("payments") + s.Field<decimal>("paymentsNew"));
                    if ( rowSumm<0)
                        rep.AddSingleValueObject((-rowSumm), startSection, 16);
                    if (rowSumm > 0)
                        rep.AddSingleValueObject(rowSumm, startSection, 17);
                    // итого % долга  - при минусовом rowSumm, делим вот на то число
                    // закомментить проценты!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    //decimal total_sum = rCol.Where(r=>r.Field<decimal>("percDebt")>0).Sum(s => s.Field<decimal>("percDebt"));
                    //if (rowSumm < 0 && total_sum!=0)
                    //    rep.AddSingleValue((-rowSumm * 100 / total_sum).ToString("0.00") + "%", startSection, 17);
                    ////////////////////////////////////////////////////////////////////////
                    // итого переплата

                    rep.Merge(startSection, 2, crow - 1, 2);
                    rep.Merge(startSection, 9, crow - 1, 9);
                    rep.Merge(startSection, 10, crow - 1, 10);
                    rep.Merge(startSection, 12, crow - 1, 12);
                    rep.Merge(startSection, 16, crow - 1, 16);
                    //rep.Merge(startSection, 17, crow - 1, 17);
                    rep.Merge(startSection, 17, crow - 1, 17);

                    rep.SetCellAlignmentToJustify(startSection, 2, crow - 1, 2);
                    rep.SetCellAlignmentToJustify(startSection, 9, crow - 1, 9);
                    rep.SetCellAlignmentToJustify(startSection, 10, crow - 1, 10);
                    rep.SetCellAlignmentToJustify(startSection, 12, crow - 1, 12);
                    rep.SetCellAlignmentToJustify(startSection, 16, crow - 1, 16);
                    //rep.SetCellAlignmentToJustify(startSection, 17, crow - 1, 17);
                    rep.SetCellAlignmentToJustify(startSection, 17, crow - 1, 17);

                    //цвет до со 2 до 18 ячейки
                    if (indexSection % 2 == 1)
                        rep.SetCellColor(startSection, 2, crow - 1, 18, Color.LightGray);
                    indexSection++;
                }
                rep.Merge(startObject, 1, crow-1, 1);
                rep.SetCellAlignmentToJustify(startObject, 1, crow-1, 1);
            }

            rep.SetPageOrientationToLandscape();
            rep.SetPageSetup(1, 9999, true);
            rep.SetBorders(1, 1, crow - 1, 18);
            rep.SetPrintRepeatHead(1, 2);
            rep.SetBottomMargin(0);
            rep.SetFooterMargin(0);
            rep.SetHeaderMargin(0);
            rep.SetLeftMargin(0);
            rep.SetRightMargin(0);
            rep.SetTopMargin(0);
            rep.Show();

        }

        private void AddHeaderSection(ref int crow)
        {
            int maxCol = 18;
            rep.AddSingleValue("Объект", crow, 1);
            rep.AddSingleValue("Секция", crow, 2);
            rep.AddSingleValue("Арендатор", crow, 3);
            rep.AddSingleValue("Номер договора", crow, 4);
            rep.AddSingleValue("Сумма по договору", crow, 5);
            rep.AddSingleValue($"План {monthPlan}", crow, 6);
            rep.AddSingleValue($"Долг на {datePlan}", crow, 7);
            rep.AddSingleValue($"Переплата на {datePlan}", crow, 8);
            rep.AddSingleValue($"Итого долг на {datePlan}", crow, 9);
            rep.AddSingleValue($"Итого переплата на {datePlan}", crow, 10);
            rep.AddSingleValue("Сумма оплаты", crow, 11);
            rep.Merge(crow, 11, crow+1, 12);
            rep.AddSingleValue("Долг тек.", crow, 13);
            rep.AddSingleValue("Переплата тек.", crow, 15);
            rep.AddSingleValue("Тек. % долга", crow, 14);
            rep.AddSingleValue("Итого долг", crow, 16);
            //rep.AddSingleValue("Итого % долга", crow, 17);
            rep.AddSingleValue("Итого переплата", crow, 17);
            rep.AddSingleValue("Арендодатель", crow, 18);
            for (int i = 1; i<=19;i++)
            {
                if (i == 11 || i == 12)
                    continue;
                rep.Merge(crow, i, crow + 1, i);
            }
            rep.SetCellAlignmentToCenter(crow, 1, crow, maxCol);
            rep.SetCellAlignmentToJustify(crow, 1, crow, maxCol);
            rep.SetFontBold(crow, 1, crow + 1, maxCol);
            rep.SetWrapText(crow, 1, crow + 1, maxCol);
            crow+=2;
        }

        public void createReportTenant()
        {
           
            if (dtReport == null)
                return;
            monthPlan = listMonth[DateTime.Parse(dtReport.Rows[0]["monthX"].ToString()).Month];
            DateTime dayX = DateTime.Parse(dtReport.Rows[0]["dayX"].ToString());
            datePlan = $"{dayX.Day}.{dayX.Month}";
            rep = new ExcelUnLoad();
            int crow = 1;
            int startRow = 1;
            AddHeaderTenant(ref crow);
            var tenants = dtReport.AsEnumerable().GroupBy(g => g.Field<string>("new_tenant_Name")).Select(s => s.Key);
            int indexTenant = 0;
            foreach (string tenant in tenants)
            {
                rep.AddSingleValue(tenant, crow, 1);
                int startTenant = crow;
                // таблица по арендатору
                EnumerableRowCollection<DataRow> rTenant = dtReport.AsEnumerable().Where(r => r.Field<string>("new_tenant_Name") == tenant);
                // объекты
                var objects = rTenant.GroupBy(g => g.Field<string>("objectName")).Select(s => s.Key);
                foreach (string obj in objects)
                {
                    rep.AddSingleValue(obj, crow, 2);
                    int startObject = crow;

                    // таблица по объекту
                    EnumerableRowCollection<DataRow> rObject = rTenant.Where(r => r.Field<string>("objectName") == obj);
                    // секции
                    var sections = rObject.GroupBy(g => g.Field<string>("nameSection")).Select(s=>s.Key);
                    foreach (var section in sections)
                    {
                        rep.AddSingleValue(section, crow, 3);
                        int startSection = crow;
                        //договоры
                        EnumerableRowCollection<DataRow> rAgrements = rObject.Where(r => r.Field<string>("nameSection") == section);
                        foreach (DataRow dr in rAgrements)
                        {
                            rep.AddSingleValue(dr["Agreement"].ToString(), crow, 4);
                            rep.AddSingleValue(DateTime.Parse(dr["Start_Date"].ToString()).ToShortDateString(), crow, 5);
                            rep.AddSingleValue(DateTime.Parse(dr["End_Date"].ToString()).ToShortDateString(), crow, 6);
                            rep.AddSingleValueObject(dr["Total_Sum"], crow, 7);
                            rep.AddSingleValueObject(dr["SummaPlanView"], crow, 8);
                            decimal before25day = (dr["payments"] == DBNull.Value ? 0 : (decimal)dr["payments"]);
                            if (before25day < 0)
                                rep.AddSingleValueObject((-1 * before25day), crow, 9);
                            if (before25day > 0)
                                rep.AddSingleValueObject(before25day, crow, 10);
                            decimal pay = (dr["paymentsNew"] == DBNull.Value ? 0 : (decimal)dr["paymentsNew"]);
                            if (pay!=0)
                                rep.AddSingleValueObject(pay, crow, 13);
                           /* decimal curDebt = before25day + pay;
                            if (curDebt < 0)
                                rep.AddSingleValue((-curDebt).ToString("0.00"), crow, 13);
                            if (curDebt > 0)
                                rep.AddSingleValue(curDebt.ToString("0.00"), crow, 14);


                            decimal currDown = (decimal)dr["percDebt"];
                            if (currDown != 0 && curDebt < 0)
                            {
                                rep.AddSingleValue($"{((curDebt / -currDown) * 100).ToString("0.00")}%", crow, 15);
                            }
                            */
                            rep.AddSingleValue(dr["landLord_name"].ToString(), crow, 21);
                            crow++;
                        }
                        rep.Merge(startSection, 3, crow - 1, 3);
                        rep.SetCellAlignmentToJustify(startSection, 3, crow - 1, 3);
                    }
                    //итого долг на 25 число
                    EnumerableRowCollection<DataRow> rCol = rObject
                        .Where(r => r.Field<object>("payments") != null);
                    // тут будет у нас или в одну или в другую считаться

                    decimal rowSumm = rCol.Sum(s => s.Field<decimal>("payments"));

                   // decimal rowSumm = rCol
                    //    .Where(r => r.Field<decimal>("payments") < 0)
                     //   .Sum(s => s.Field<decimal>("payments"));
                    if (rowSumm<0)
                        rep.AddSingleValueObject((-rowSumm), startObject, 11);
                    //переплата на 25 число
                   // rowSumm = rCol
                    //    .Where(r => r.Field<decimal>("payments") > 0)
                    //    .Sum(s => s.Field<decimal>("payments"));
                    if (rowSumm>0)
                        rep.AddSingleValueObject(rowSumm, startObject, 12);
                    
                    
                    
                    //сумма оплаты
                    rCol = rObject
                        .Where(r => r.Field<object>("paymentsNew") != null);
                    rowSumm = rCol
                        .Sum(s => s.Field<decimal>("paymentsNew"));
                    if (rowSumm!=0)
                        rep.AddSingleValueObject(rowSumm, startObject, 14);
                    //итого долг - считается сумма текущего по секции - если в -, то долг, если в +, то переплата... для - считается переплата

                    //это как в предыдущем, только по другому идет сортировочка 
                    /*rowSumm = rTenant.Sum(s => s.Field<decimal>("payments") + s.Field<decimal>("paymentsNew"));
                    if (rowSumm < 0)
                        rep.AddSingleValue((-rowSumm).ToString("0.00"), startObject, 16);
                    if (rowSumm > 0)
                        rep.AddSingleValue(rowSumm.ToString("0.00"), startObject, 18);
                    // итого % долга  - при минусовом rowSumm, делим вот на то число
                    decimal total_sum = rCol.Where(r => r.Field<decimal>("percDebt") > 0).Sum(s => s.Field<decimal>("percDebt"));
                    if (rowSumm < 0 && total_sum != 0)
                        rep.AddSingleValue((-rowSumm * 100 / total_sum).ToString("0.00") + "%", startObject, 17);*/
                    // итого переплата

                    decimal currentSum = rObject.Sum(s => (s.Field<decimal>("payments") + s.Field<decimal>("paymentsNew")));

                    
                    if (currentSum < 0)
                        rep.AddSingleValueObject((-currentSum), startObject, 15);
                    if (currentSum > 0)
                        rep.AddSingleValueObject(currentSum, startObject, 16);


                    decimal currDown = rObject.Sum(s => s.Field<decimal>("percDebt"));
                    if (currDown != 0 && currentSum < 0)
                    {
                        rep.AddSingleValue($"{((currentSum / -currDown) * 100).ToString("0.00")}%", startObject, 17);
                    }



                    rep.Merge(startObject, 2, crow - 1, 2);
                    rep.Merge(startObject, 11, crow - 1, 11);
                    rep.Merge(startObject, 12, crow - 1, 12);
                    rep.Merge(startObject, 14, crow - 1, 14);
                    rep.Merge(startObject, 15, crow - 1, 15);
                    rep.Merge(startObject, 16, crow - 1, 16);
                    rep.Merge(startObject, 17, crow - 1, 17);

                    rep.SetCellAlignmentToJustify(startObject, 2, crow - 1, 2);
                    rep.SetCellAlignmentToJustify(startObject, 11, crow - 1, 11);
                    rep.SetCellAlignmentToJustify(startObject, 12, crow - 1, 12);
                    rep.SetCellAlignmentToJustify(startObject, 14, crow - 1, 14);
                    rep.SetCellAlignmentToJustify(startObject, 15, crow - 1, 15);
                    rep.SetCellAlignmentToJustify(startObject, 16, crow - 1, 16);
                    rep.SetCellAlignmentToJustify(startObject, 17, crow - 1, 17);
                }

                decimal summ = rTenant.Sum(s => s.Field<decimal>("payments") + s.Field<decimal>("paymentsNew"));
                if (summ < 0)
                    rep.AddSingleValueObject((-summ), startTenant, 18);
                if (summ > 0)
                    rep.AddSingleValueObject(summ, startTenant, 20);
                // итого % долга  - при минусовом rowSumm, делим вот на то число
                decimal sum_total = rTenant.Where(r => r.Field<decimal>("percDebt") > 0).Sum(s => s.Field<decimal>("percDebt"));
                if (summ < 0 && sum_total != 0)
                    rep.AddSingleValue((-summ * 100 / sum_total).ToString("0.00") + "%", startTenant, 19);

                rep.Merge(startTenant, 18, crow - 1, 18);
                rep.Merge(startTenant, 19, crow - 1, 19);
                rep.Merge(startTenant, 20, crow - 1, 20);
                rep.SetCellAlignmentToJustify(startTenant, 18, crow - 1, 18);
                rep.SetCellAlignmentToJustify(startTenant, 19, crow - 1, 19);
                rep.SetCellAlignmentToJustify(startTenant, 20, crow - 1, 20);

                rep.Merge(startTenant, 1, crow - 1, 1);
                rep.SetCellAlignmentToJustify(startTenant, 1, crow - 1, 1);

                //подстветка каждого второго арендатора
                //цвет до со 1 по 21 ячейки
                if (indexTenant % 2 == 1)
                    rep.SetCellColor(startTenant, 1, crow - 1, 21, Color.LightGray);
                indexTenant++;
            }

            rep.SetPageOrientationToLandscape();
            rep.SetPageSetup(1, 9999, true);
            rep.SetBorders(1, 1, crow - 1, 21);
            rep.SetPrintRepeatHead(1, 2);

            rep.SetBottomMargin(0);
            rep.SetFooterMargin(0);
            rep.SetHeaderMargin(0);
            rep.SetLeftMargin(0);
            rep.SetRightMargin(0);
            rep.SetTopMargin(0);

            rep.Show();
        }

        private void AddHeaderTenant(ref int crow)
        {
            rep.AddSingleValue("Арендатор", crow, 1);
            rep.AddSingleValue("Объект", crow, 2);
            rep.AddSingleValue("Секция", crow, 3);
            rep.AddSingleValue("Номер договора", crow, 4);
            rep.AddSingleValue("Начало договора", crow, 5);
            rep.AddSingleValue("Конец договора", crow, 6);
            rep.AddSingleValue("Сумма по договору", crow, 7);
            rep.AddSingleValue($"План {monthPlan}", crow, 8);
            rep.AddSingleValue($"Долг на {datePlan}", crow, 9);
            rep.AddSingleValue($"Переплата на {datePlan}", crow, 10);
            rep.AddSingleValue($"Итого долг на {datePlan}", crow, 11);
            rep.AddSingleValue($"Итого переплата на {datePlan}", crow, 12);
            rep.AddSingleValue("Сумма оплаты", crow, 13);
            rep.Merge(crow, 13, crow + 1, 14);
            rep.AddSingleValue("Долг тек.", crow, 15);
            rep.AddSingleValue("Переплата тек.", crow, 16);
            rep.AddSingleValue("Тек. % долга", crow, 17);
            rep.AddSingleValue("Итого долг", crow, 18);
            rep.AddSingleValue("Итого % долга", crow, 19);
            rep.AddSingleValue("Итого переплата", crow, 20);
            rep.AddSingleValue("Арендодатель", crow, 21);
            for (int i = 1; i <= 21; i++)
            {
                if (i == 13 || i == 14)
                    continue;
                rep.Merge(crow, i, crow + 1, i);
            }
            rep.SetCellAlignmentToCenter(crow, 1, crow, 21);
            rep.SetCellAlignmentToJustify(crow, 1, crow, 21);
            rep.SetFontBold(crow, 1, crow + 1, 21);
            rep.SetWrapText(crow, 1, crow + 1, 21);
            crow += 2;
        }


        private void insertData()
        {
            string xmlMain = "";//первая таблица
            foreach (var a in listPay)
            {
                int id_Agreement = a.Key;
                foreach (DataRow dr in a.Value.Rows)
                {
                    xmlMain += $"<agreement id=\"{id_Agreement}\" period=\"{toSql(DateTime.Parse(dr["date"].ToString()))}\" summa=\"{dr["price"]}\" />";
                }
            }
            string xmlLeave = ""; // вторая таблица, проверить в проце, что эта хрень Length>0
            foreach (var a in listLeave)
            {
                int id_Agreement = a.Key;
                DateTime dateLeave =(DateTime)dtLeaves.AsEnumerable().Where(r => r.Field<int>("id") == id_Agreement).First()["Date_Of_Departure"];
                foreach (DataRow dr in a.Value.Rows)
                {
                    xmlLeave += $"<agreement id=\"{id_Agreement}\" period=\"{toSql(DateTime.Parse(dr["date"].ToString()))}\" summa=\"{dr["price"]}\" dateLeave=\"{toSql(dateLeave)}\" />";
                }
            }
            Task<DataTable> task = Config.hCntMain.setPlanData(xmlMain, xmlLeave);
            task.Wait();
            DataTable dtResult = task.Result;

        }

        private string toSql(DateTime date)
        {
            return $"{date.Year}-{date.Month}-{date.Day}";
        }
    }
}
