--select 
--	 a.id,
--	 a.Total_Sum+ a.Phone as totalSum,
--	 a.Start_Date,
--	 dateadd(day,ad.RentalVacation,a.Start_Date) as newDateStart,
--	 a.Stop_Date,
--	 ad.*
--from 
--	Arenda.j_Agreements a
--		inner join Arenda.j_AdditionalAgreements ad on ad.id_Agreements = a.id
--where 
--	isConfirmed = 1

select a.id,sum(a.Summa) as Summa,sum(a.sumPay) as sumPay,sum(a.Summa) - sum(a.sumPay) as resultSumma,round(((sum(a.Summa) - sum(a.sumPay))/sum(a.Summa))*100,2) as prcOwe from (
select 
	a.id,f.Summa,cast(0 as numeric (16,2)) as sumPay

	--sum(f.Summa) as summa
from	
	Arenda.j_Agreements a
		inner join Arenda.j_Fines f on f.id_Agreements = a.id
		inner join Arenda.s_AddPayment ad on ad.id = f.id_ÀddPayment
where 
	a.isConfirmed = 1 and ad.isActive = 1

UNION ALL

select a.id,cast(0 as numeric (16,2)) as Summa,f.Summa  as sumPay
from Arenda.j_Agreements a 
		inner join Arenda.j_Fines f on f.id_Agreements = a.id
		inner join Arenda.j_PaymentContract pc on pc.id_Fines = f.id
where 
	a.isConfirmed = 1
) as a
GROUP BY 
	a.id



--select * from Arenda.j_tDiscount