DECLARE @id int = 1050, @date date = '2020-06-01'

exec [Arenda].[spg_getPlaneReportDopInfoPay] @id,@date
return;


select
	a.id,
	'π'+a.Agreement+' Ò "'+	torgt.Abbreviation+' ' + ltt.cName+'"' as nameTenant,
	tpr.PeriodMonthPlan,
	ol.cName as nameObject,
	f.DateFines,
	ap.cName as namePayment,
	f.Summa as summaFines,
	pc.Date,
	pc.Summa as summaPayments,
	case 
		when pc.isCash is null then ''
		when pc.isCash = 1 then 'Õ‡Î.' else '¡ÂÁÌ‡Î.'
	end as typeCash
from
		Arenda.j_Agreements a	
		left join Arenda.j_PlanReport pr on pr.id_Agreements = a.id
		left join Arenda.j_tPlanReport tpr on pr.id_tPlanReport = tpr.id
		
		--left join Arenda.j_Agreements a	on a.id = pr.id_Agreements	
		
		left join Arenda.s_Landlord_Tenant ltt on ltt.id = a.id_Tenant
		left join Arenda.s_Type_of_Organization torgt on torgt.id = ltt.id_Type_Of_Organization
		
		left join Arenda.s_ObjectLease ol on ol.id = tpr.id_ObjectLease

		left join Arenda.j_Fines f on f.id_Agreements = a.id
		left join Arenda.s_AddPayment ap on ap.id = f.id_¿ddPayment
		left join Arenda.j_PaymentContract pc on pc.id_Agreements = a.id and pc.id_Fines = f.id
where	
	a.id = @id and f.PlanDate = @date
	--and f.id  is not null