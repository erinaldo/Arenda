using Nwuram.Framework.Settings.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arenda
{
  
    class Penni
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        private DataTable dtPaymentMonth;
       /// <summary>
       /// договор
       /// </summary>
        private int id_agreement;
        /// <summary>
        /// дата оплаты
        /// </summary>
        private DateTime datePayment;
        /// <summary>
        /// оплата до выбранной даты
        /// </summary>
        private decimal payAtDate;
        /// <summary>
        /// день оплаты из прог_конфиг
        /// </summary>
        private int dayToPay;
        /// <summary>
        /// сумма оплаты
        /// </summary>
        private decimal sumPay;
        /// <summary>
        /// 0- есть подтвержденные пени, не добавлено 1-добавлена оплата без пени, 2-добавлена оплата с неполным пени, 3- добавлена оплата с полным пени 
        /// </summary>
        /// 
        private string Description;
        public int statusPenni { get; private set; } = 0;
        public int id_PaymentContract { get; private set; } = 0;
        public DataTable dtPaymentContract { get; private set; }
        /// <summary>
        /// сообщение пользователю
        /// </summary>
        public string message { 
            get 
            {
                string mes = "";
                switch(statusPenni)
                {
                    case 0:
                        mes = "Добавление оплаты выбранным\nднем невозможно, т.к. присутсутствуют\nподтвержденные пени за оплачиваемый\nпериод или далее";
                        break;
                    case 1: mes =  "Оплата добавлена";
                        break;
                    case 2: mes = "Оплата добавлена";
                        break;
                    case 3: mes =  $"За период\n{messageMonth}\nначислены пени.\nТребуется подтверждение Д";
                        break;
                    
                }
                return mes;
            }
            set 
            { 
                message = value; 
            }
        }
        /// <summary>
        /// месяца, за которые вводится оплата
        /// </summary>
        private string messageMonth = "";
        /// <summary>
        /// погнали считать пени
        /// </summary>
        /// <param name="id_agreement">договор</param>
        /// <param name="datePayment">дата оплаты</param>
        public Penni(int id_agreement, DateTime datePayment, decimal sumPay,string Description)
        {
            this.id_agreement = id_agreement;
            this.datePayment = datePayment;
            this.sumPay = sumPay;
            this.Description = Description;
            getTablePayment();
        }


        private void getTablePayment()
        {
            DataTable dtArendaPeriod = _proc.getArendaPeriod(id_agreement,datePayment);
            if (dtArendaPeriod == null || dtArendaPeriod.Rows.Count == 0)
                return;
            DateTime startArenda = DateTime.Parse(dtArendaPeriod.Rows[0]["dateStart"].ToString());
            DateTime endArenda = DateTime.Parse(dtArendaPeriod.Rows[0]["dateEnd"].ToString());
            decimal price = decimal.Parse(dtArendaPeriod.Rows[0]["Total_Sum"].ToString());
            decimal area = decimal.Parse(dtArendaPeriod.Rows[0]["Total_Area"].ToString());
            decimal phone = decimal.Parse(dtArendaPeriod.Rows[0]["Phone"].ToString());
            payAtDate = decimal.Parse(dtArendaPeriod.Rows[0]["pay"].ToString());
            DataTable dtPriceDays = new DataTable();


            dtPriceDays.Columns.Add("day", typeof(DateTime));
            dtPriceDays.Columns.Add("price", typeof(decimal));

            DateTime dateIterator = startArenda;
            while (dateIterator <= endArenda)
            {
                dtPriceDays.Rows.Add(dateIterator, Math.Round(price / DateTime.DaysInMonth(dateIterator.Year, dateIterator.Month), 2));
                dateIterator = dateIterator.AddDays(1);
            }

            DataTable dtDiscounts = _proc.getDiscounts(id_agreement);
            if (dtDiscounts == null)
                return;

            foreach (DataRow dr in dtPriceDays.Rows)
            {
                DateTime discountArea = new DateTime(1900, 1, 1);
                //скидки на кв.м.
                EnumerableRowCollection<DataRow> rowcoll = dtDiscounts.AsEnumerable()
                    .Where(r => r.Field<DateTime>("DateStart") <= (DateTime)dr["day"] && (r.Field<DateTime?>("DateEnd") is null || r.Field<DateTime?>("DateEnd") >= (DateTime)dr["day"])
                    && r.Field<int>("id_TypeDiscount") == 2).OrderByDescending(o=>o.Field<DateTime>("DateStart"));
                if (rowcoll.Count() > 0)
                {
                    discountArea = (DateTime)rowcoll.First()["DateStart"];
                    dr["price"] =  Math.Round( (decimal.Parse(rowcoll.First()["Discount"].ToString()) * area + phone)
                            / DateTime.DaysInMonth(DateTime.Parse(dr["day"].ToString()).Year, DateTime.Parse(dr["day"].ToString()).Month), 2);
                }
                //процент
               
                rowcoll = dtDiscounts.AsEnumerable()
                    .Where(r => r.Field<DateTime>("DateStart") <= (DateTime)dr["day"] && (r.Field<DateTime?>("DateEnd") is null || r.Field<DateTime?>("DateEnd") >= (DateTime)dr["day"])
                    && r.Field<int>("id_TypeDiscount") == 1).OrderByDescending(o => o.Field<DateTime>("DateStart"));
                if (rowcoll.Count() > 0)
                {
                    if ((DateTime)rowcoll.First()["DateStart"] > discountArea)
                     dr["price"] = Math.Round((100 - decimal.Parse(rowcoll.First()["Discount"].ToString())) / 100 * decimal.Parse(dr["price"].ToString()), 2);
                }
            }
            //и сейчас суммируем по месяцам.. вот(

            DataTable resultTable = new DataTable();
            resultTable.Columns.Add("month", typeof(int)); //месяц цифрой
            resultTable.Columns.Add("year", typeof(int)); // год цифврой
            resultTable.Columns.Add("price", typeof(decimal)); // оплата за месяц
            resultTable.Columns.Add("date", typeof(DateTime)); // дата датой  с первым числом
            resultTable.Columns.Add("sumPrice", typeof(decimal));// сумма по месяцам до месяца
            resultTable = dtPriceDays.AsEnumerable()
                .GroupBy(t => new
                {
                    month = t.Field<DateTime>("day").Month,
                    year = t.Field<DateTime>("day").Year                   
                })
                .Select(s =>
                {
                    DataRow dr = resultTable.NewRow();
                    dr["month"] = s.Key.month;
                    dr["year"] = s.Key.year;
                    dr["price"] = decimal.Round(s.Sum(r => r.Field<decimal>("price")));
                    dr["date"] = new DateTime(s.Key.year, s.Key.month, 1);
                    dr["sumPrice"] = 0;
                    return dr;

                }).CopyToDataTable();
            foreach (DataRow dr in resultTable.Rows)
            {
                dr["sumPrice"] = resultTable.AsEnumerable().Where(r => r.Field<DateTime>("date") <= (DateTime)dr["date"]).Sum(r => r.Field<decimal>("price"));
            }

            dtPaymentMonth = resultTable;
            /*
            frmPenniTest frm = new frmPenniTest(resultTable);
            frm.Show();*/
        }

        public bool PenniCalculate()
        {
            DataTable dtSettings = _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "", "");
            dayToPay = int.Parse(dtSettings.Select("id_value = 'chsl'").First()["value"].ToString());

            //выбираем, до какого месяца рассчитываем, до 25 июня заплатить за июль, после 25 июня заплатить за август 
            DateTime dateCalculate = new DateTime();
            if (datePayment.Day > dayToPay)
                dateCalculate = datePayment.AddMonths(1);
            else
                dateCalculate = datePayment;

            // текущая оплата
            decimal pay = sumPay;
            // оплата до текущего дня
            decimal paidAtDate = payAtDate;

            decimal needPayment = 0.00M;
            if (dateCalculate >=(DateTime)dtPaymentMonth.Rows[0]["date"])
                needPayment = decimal.Parse(dtPaymentMonth
                    .AsEnumerable()
                    .Where(r => r.Field<DateTime>("date") <= dateCalculate)
                    .OrderByDescending(o => o.Field<DateTime>("date"))
                    .First()["sumPrice"].ToString());

            //это все хорошо прям супер как хорошо
            if ((needPayment <= paidAtDate) || payAtDate == 0)
            {
                // вводим оплату
                DataTable dt = new DataTable();
                dt = _proc.AddEditPayment(0,
                         id_agreement,
                         datePayment,
                         sumPay,
                         id_PayType,
                         planeDate,
                         isRealMoney,
                         isSendMoney,
                         id_Fine,
                         Description
                         );
                id_PaymentContract = int.Parse(dt.Rows[0]["id"].ToString());
                statusPenni = 1;
                dtPaymentContract = dt;
            }
            //а тут плоховато вот
            else
            {
                DateTime monthToPay = DateTime.Parse(dtPaymentMonth.AsEnumerable()
                    .Where(r => r.Field<decimal>("sumPrice") > paidAtDate)
                    .OrderBy(o => o.Field<DateTime>("date"))
                    .First()["date"].ToString());

                bool acceptedPeni = _proc.getAcceptedPeni(id_agreement, monthToPay);
                //есть подтвержденная пени, то сообщение и не вводим оплату
                if (acceptedPeni)
                {
                    statusPenni = 0;
                    return false;
                }
                //идем дальше... добавляем оплату и пеню (условие на наличие шапки в процедуре будет)
                else
                {
                    // вводим оплату
                    DataTable dt = new DataTable();
                    dt = _proc.AddEditPayment(0,
                             id_agreement,
                             datePayment,
                             sumPay,
                             id_PayType,
                             planeDate,
                             isRealMoney,
                             isSendMoney,
                             id_Fine,
                             Description
                             );
                    id_PaymentContract = int.Parse(dt.Rows[0]["id"].ToString());
                    dtPaymentContract = dt;
                    // от месяца оплаты до месяца, за который надо оплатить

                    decimal iteratorPay = sumPay;
                    bool firstMonth = true;//первый месяц - сумма долга - вычитание сколько надо заплатить минус оплата
                    while (monthToPay<=dateCalculate && iteratorPay > 0)
                    {
                        //считаем сколько долг
                        decimal credit;
                        //для первого месяца с долгом берем сколько надо было заплатить всего минус сколько оплачено
                        if (firstMonth)

                        {
                            credit = decimal.Parse(dtPaymentMonth.AsEnumerable().Where(r => r.Field<DateTime>("date") == monthToPay).First()["sumPrice"].ToString())
                                - paidAtDate;
                            firstMonth = false;
                        }
                        //для второго и следующих месяцев - долг - просто сумма за месяц
                        else
                        {
                            credit = decimal.Parse(dtPaymentMonth.AsEnumerable().Where(r => r.Field<DateTime>("date") == monthToPay).First()["sumPrice"].ToString());
                        }
                        //кол-во дней просрочки
                        int dayCountCredit = (datePayment - monthToPay.AddMonths(-1).AddDays(dayToPay)).Days + 1;
                        // статус для оплаты в tPenalty (2 если оплата покрыла долг, 1 если оплата не покрыла долг)
                        int status = decimal.Parse(dtPaymentMonth.AsEnumerable().Where(r => r.Field<DateTime>("date") == monthToPay).First()["sumPrice"].ToString())
                                - paidAtDate - sumPay > 0 ? 1 : 2;
                        if (status == 2)
                        {
                            statusPenni = 3;
                            messageMonth += intToMonth[monthToPay.Month] + " " + monthToPay.Year.ToString() + " ";
                            
                        }
                        if (messageMonth.Length == 0) statusPenni = 2;
                        //сохранаем пени
                        DataTable dtPeni = _proc.setPeni(id_agreement, Math.Round(credit), dayCountCredit, id_PaymentContract, monthToPay, status);
                        //вычитаем из оплаты то, сколько в пени оплата пошли
                        iteratorPay -= credit;
                        //и смотрим следующий месяц
                        monthToPay = monthToPay.AddMonths(1);
                    }
                }

            }

            return true;
        }

        private Dictionary<int, string> intToMonth = new Dictionary<int, string>()
        {   {1,"Январь" },
            {2, "Февраль" },
            {3, "Март" },
            {4, "Апрель" },
            {5, "Май" },
            {6, "Июнь" },
            {7, "Июль" },
            {8, "Август" },
            {9, "Сентябрь" },
            {10, "Октябрь" },
            {11, "Ноябрь" },
            {12, "Декабрь" } };

        private int id_PayType;
        private DateTime planeDate;
        private bool isRealMoney;
        private bool isSendMoney;
        private int? id_Fine;

        public void setDataToAddPay(int id_PayType, DateTime planeDate, bool isRealMoney, bool isSendMoney, int? id_Fine)
        {
            this.id_PayType = id_PayType;
            this.planeDate = planeDate;
            this.isRealMoney = isRealMoney;
            this.isSendMoney = isSendMoney;
            this.id_Fine = id_Fine;
        }

    }

    

}
