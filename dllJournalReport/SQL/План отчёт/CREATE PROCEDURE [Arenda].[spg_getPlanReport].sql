SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-17
-- Description:	Получение тела план отчёта
-- =============================================
ALTER PROCEDURE [Arenda].[spg_getPlanReport]		 	
	@date date,
	@id_ObjectLease int,
	@id_tPlanReport int  = 0
AS
BEGIN
	SET NOCOUNT ON;

	

select id_Agreements,sum(case when isToTenant = 1 then -1 else 1 end * Summa) as summa INTO #tmpPayContr from Arenda.j_PaymentContract where id_PayType = 1 group by id_Agreements
select id_Agreements,sum(case when isToTenant = 1 then -1 else 1 end * Summa) as summa INTO #tmpPayContrOver from Arenda.j_PaymentContract where id_PayType in (2,3) and PlaneDate = @date group by id_Agreements


select f.id_Agreements,sum(f.Summa) as summa INTO #tmpFines from Arenda.j_Fines f inner join Arenda.s_AddPayment a on a.id = f.id_АddPayment where f.PlanDate = @date and a.cName = 'Пени' group by f.id_Agreements
select f.id_Agreements,sum(f.Summa) as summa INTO #tmpFinesOver from Arenda.j_Fines f inner join Arenda.s_AddPayment a on a.id = f.id_АddPayment where f.PlanDate = @date and a.cName not like 'Пени' group by f.id_Agreements


DECLARE @idPretPlanReport int,@idPreMonthPlane int
select  @idPretPlanReport  = id from Arenda.j_tPlanReport where PeriodMonthPlan = dateadd(month,-1,@date)
select  @idPreMonthPlane  = id from Arenda.j_tMonthPlan where PeriodMonthPlan = @date -- dateadd(month,-1,@date)






select 
	a.id,
	a.id_Landlord,
	torg.Abbreviation+' ' + lt.cName as nameLandLord,
	torgt.Abbreviation+' ' + ltt.cName as nameTenant,
	a.Agreement,
	'' as timeLimit,
	ol.cName as nameObject,	
	--tc.TypeContract,	
	--a.id_TypeContract,
	b.Abbreviation as Build,
	f.Abbreviation as [Floor],
	case 
		when a.id_TypeContract = 1 then s.cName
		when a.id_TypeContract = 2 then rp.NumberPlace
		when a.id_TypeContract = 3 then lp.NumberPlot
	end as namePlace,
	case when a.id_TypeContract = 2 then null else a.Total_Area end as Total_Area,
	case when a.id_TypeContract = 2 then null else a.Cost_of_Meter end as Cost_of_Meter, --a.Cost_of_Meter,
	a.Total_Sum,
	0 as Discount,
	isnull(tpc.summa,0) as sumPayCont,
	isnull(prePR.Credit,0) as preCredit,
	isnull(prePR.OverPayment,0) as preOverPayment,
	isnull(preMP.[Plan],0) as prePlan,
	isnull(pr.EndPlan,0) as EndPlan,
	isnull(case when @id_tPlanReport = 0 then tf.summa else pr.Penalty end,0) as Penalty,
	isnull(case when @id_tPlanReport = 0 then tfo.summa else pr.OtherPayments end,0) as OtherPayments,	
	0 as ultraResult,
	isnull(case when @id_tPlanReport = 0 then tpco.summa else pr.Included end,0) as Included,	
	isnull(case when @id_tPlanReport = 0 then 0 else pr.Credit end,0) as  Credit,
	isnull(case when @id_tPlanReport = 0 then 0 else pr.OverPayment end,0) as OverPayment,
	dateadd(day,isnull(aa.RentalVacation,0),isnull(ad.DateDocument,a.Start_Date)) as Start_Date,
	--isnull(ad.DateDocument,a.Start_Date) as Start_Date,
	--isnull(ad.Date_of_Departure,a.Stop_Date) as Stop_Date
	[Arenda].[fGetDateEndAgreements](a.id) as Stop_Date,
	isnull(a.Phone,0) as Phone
INTO 
	#tmpTable
from 
	Arenda.j_Agreements a
		inner join Arenda.s_TypeContract tc on tc.id = a.id_TypeContract
		left join Arenda.j_AdditionalAgreements aa on aa.id_Agreements = a.id


		left join Arenda.j_AdditionalDocuments ad on ad.id_Agreements = a.id and ad.id_TypeDoc = (select top(1) td.id from Arenda.s_TypeDoc td  where td.Rus_Name = 'Акт приёма-передачи') and ad.isActive = 1
		--left join Arenda.j_AdditionalDocuments ad on ad.id_Agreements = a.id
		--left join Arenda.s_TypeDoc td on td.id = ad.id_TypeDoc and td.Rus_Name = 'Акт приёма-передачи' 
		 
		left join Arenda.s_Landlord_Tenant lt on lt.id = a.id_Landlord
		left join Arenda.s_Type_of_Organization torg on torg.id = lt.id_Type_Of_Organization

		left join Arenda.s_Landlord_Tenant ltt on ltt.id = a.id_Tenant
		left join Arenda.s_Type_of_Organization torgt on torgt.id = ltt.id_Type_Of_Organization

		left join Arenda.s_ObjectLease ol on ol.id = a.id_ObjectLease

		left join Arenda.s_Building b on b.id = a.id_Buildings and a.id_TypeContract = 1
		left join Arenda.s_Floors f on f.id = a.id_Floor
		left join Arenda.s_Sections s on s.id = a.id_Section

		left join Arenda.s_ReclamaPlace rp on rp.id = a.id_Section and a.id_TypeContract = 2
		left join Arenda.s_Building bp on bp.id = rp.id_Building

		left join Arenda.s_LandPlot lp on lp.id = a.id_Section and a.id_TypeContract = 3

		left join #tmpPayContr tpc on tpc.id_Agreements = a.id
		left join #tmpPayContrOver tpco on tpco.id_Agreements = a.id

		left join Arenda.j_PlanReport prePR on prePR.id_Agreements = a.id and prePR.id_tPlanReport =  @idPretPlanReport 
		left join Arenda.j_MonthPlan preMP on preMP.id_Agreements = a.id and preMP.id_tMonthPlan =  @idPreMonthPlane 
		left join #tmpFines tf on tf.id_Agreements = a.id
		left join #tmpFinesOver tfo on tfo.id_Agreements = a.id

		left join Arenda.j_PlanReport pr on pr.id_Agreements = a.id and pr.id_tPlanReport = @id_tPlanReport
where 
	a.isConfirmed = 1 --and a.Start_Date<= @date and @date<=a.Stop_Date 
	and a.fullPayed = 0 --and td.Rus_Name = 'Акт приёма-передачи' 
	and a.id_ObjectLease = @id_ObjectLease
	and (@id_tPlanReport = 0 or pr.id is not null)

SELECT distinct
	t.id,
	t.id_Landlord,
	t.nameLandLord,
	t.nameTenant,
	t.Agreement,
	t.timeLimit,
	t.nameObject,
	t.Build,
	t.[Floor],
	t.namePlace,
	t.Total_Area,
	t.Cost_of_Meter,
	t.Total_Sum,
	isnull(t.Total_Sum,0) - isnull(t.EndPlan,0) as Discount,
	t.sumPayCont,
	t.preCredit,
	t.preOverPayment,
	t.prePlan,
	t.EndPlan,
	t.Penalty,
	t.OtherPayments,
	isnull(t.EndPlan,0) + isnull(t.preCredit,0) - isnull(t.preOverPayment,0) + t.Penalty - t.OtherPayments as ultraResult,
	t.Included,
	case 
	when @id_tPlanReport = 0 then (isnull(t.EndPlan,0) + isnull(t.preCredit,0) - isnull(t.preOverPayment,0) + t.Penalty - t.OtherPayments) - isnull(t.Included,0)
	else t.Credit end as Credit,
	case 
	when @id_tPlanReport = 0 then (isnull(t.EndPlan,0) + isnull(t.preCredit,0) - isnull(t.preOverPayment,0) + t.Penalty - t.OtherPayments) - isnull(t.Included, 0)
	else t.OverPayment end as OverPayment,
	t.Start_Date,
	t.Stop_Date,
	t.Phone
FROM 
	#tmpTable t
where
		--YEAR(t.Start_Date)<= YEAR(@date) and MONTH(t.Start_Date)<= MONTH(@date)  and YEAR(t.Stop_Date)>= YEAR(@date) and MONTH(t.Stop_Date)>= MONTH(@date)
		(t.Stop_Date>=@date and DATEADD(day,-1, DATEADD(month,1,@date))>=t.Start_Date) or @id_tPlanReport <>0


DROP TABLE #tmpPayContr,#tmpPayContrOver,#tmpFines,#tmpFinesOver,#tmpTable

END
