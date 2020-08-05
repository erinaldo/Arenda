select
	a.id,
	a.Total_Sum+a.Phone as total_summ,
	dateadd(day,isnull(aa.RentalVacation,0),isnull(ad.DateDocument, a.Start_Date)) as Start_Date,
	a.Stop_Date,
	aa.RentalVacation,
	ad.DateDocument,
	a.Start_Date,
	p.Date,
	p.Summa,
	p.isPayment
from 
	Arenda.j_Agreements a 
		left JOIN Arenda.j_AdditionalAgreements aa on aa.id_Agreements = a.id
		LEFT  JOIN Arenda.j_AdditionalDocuments ad on ad.id_Agreements = a.id and ad.id_TypeDoc = (select TOP(1) id from Arenda.s_TypeDoc where Rus_Name = 'Акт приёма-передачи')
		left join Arenda.j_PaymentContract p on p.id_Agreements = a.id --and p.isPayment = 2
where 
	a.isConfirmed  = 1


