USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[spg_getPlaneReportDopInfoPay]    Script Date: 22.09.2020 16:41:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-17
-- Description:	Получение списка доп оплат по договору на период
-- =============================================
ALTER PROCEDURE [Arenda].[spg_getPlaneReportDopInfoPay]		 	
	@id int, 
	@date date
AS
BEGIN
	SET NOCOUNT ON;

select
	a.id,
	'№'+a.Agreement+' с "'+	torgt.Abbreviation+' ' + ltt.cName+'"' as nameTenant,
	@date as PeriodMonthPlan,
	ol.cName as nameObject,
	f.DateFines,
	ap.cName as namePayment,
	f.Summa as summaFines,
	pc.Date,
	pc.Summa as summaPayments,
	case 
		when pc.isCash is null then ''
		when pc.isCash = 1 then 'Нал.' else 'Безнал.'
	end as typeCash,
	f.id as id_fines
from
		Arenda.j_Agreements a	
		--left join Arenda.j_PlanReport pr on pr.id_Agreements = a.id
		--left join Arenda.j_tPlanReport tpr on pr.id_tPlanReport = tpr.id
		
		--left join Arenda.j_Agreements a	on a.id = pr.id_Agreements	
		
		left join Arenda.s_Landlord_Tenant ltt on ltt.id = a.id_Tenant
		left join Arenda.s_Type_of_Organization torgt on torgt.id = ltt.id_Type_Of_Organization
		
		left join Arenda.s_ObjectLease ol on ol.id = a.id_ObjectLease

		left join Arenda.j_Fines f on f.id_Agreements = a.id
		left join Arenda.s_AddPayment ap on ap.id = f.id_АddPayment
		left join Arenda.j_PaymentContract pc on pc.id_Agreements = a.id and pc.id_Fines = f.id
where	
	a.id = @id and f.PlanDate = @date and (a.fullPayed = 0 or (a.fullPayed = 1 and GETDATE()<a.Stop_Date ))
	
END
