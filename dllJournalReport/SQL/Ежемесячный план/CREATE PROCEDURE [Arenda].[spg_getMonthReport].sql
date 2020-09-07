SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-17
-- Description:	Получение тела журнала ежемесячных планов
-- =============================================
ALTER PROCEDURE [Arenda].[spg_getMonthReport]		 	
	@dateStart date,
	@id_ObjectLease int,
	@id_tMonthPlane int  = 0
AS
BEGIN
	SET NOCOUNT ON;

select * from (
select 
	a.id,
	torg.Abbreviation+' ' + lt.cName as nameLandLord,
	torgt.Abbreviation+' ' + ltt.cName as nameTenant,
	ol.cName as nameObject,
	'' as timeLimit,
	tc.TypeContract,
	a.Agreement,	
	a.id_TypeContract,
	b.Abbreviation as Build,
	f.Abbreviation as [Floor],
	case 
		when a.id_TypeContract = 1 then s.cName
		when a.id_TypeContract = 2 then rp.NumberPlace
		when a.id_TypeContract = 3 then lp.NumberPlot
	end as namePlace,
	case when a.id_TypeContract = 2 then null else a.Total_Area end as Total_Area,--a.Total_Area,
	case when a.id_TypeContract = 2 then null else a.Cost_of_Meter end as Cost_of_Meter, --a.Cost_of_Meter,
	isnull(mp.SummaContract,a.Total_Sum) as Total_Sum,
	dateadd(day,isnull(aa.RentalVacation,0),isnull(ad.DateDocument,a.Start_Date)) as Start_Date,
	--isnull(ad.DateDocument,a.Start_Date) as Start_Date,--ad.DateDocument as Start_Date,
	--isnull(ad.Date_of_Departure,a.Stop_Date) as Stop_Date,
	[Arenda].[fGetDateEndAgreements](a.id) as Stop_Date,
	mp.Discount as discount,
	mp.[Plan] as plane,
	a.id_Landlord,
	isnull(a.Phone,0) as Phone
	
from 
	Arenda.j_Agreements a
		inner join Arenda.s_TypeContract tc on tc.id = a.id_TypeContract
		left join Arenda.j_AdditionalAgreements aa on aa.id_Agreements = a.id

		left join Arenda.j_AdditionalDocuments ad on ad.id_Agreements = a.id and ad.id_TypeDoc = (select top(1) td.id from Arenda.s_TypeDoc td  where td.Rus_Name = 'Акт приёма-передачи')
		left join Arenda.s_TypeDoc td on td.id = ad.id_TypeDoc and td.Rus_Name = 'Акт приёма-передачи' 

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

		left join Arenda.j_MonthPlan mp on mp.id_Agreements = a.id and mp.id_tMonthPlan = @id_tMonthPlane
where 
	a.isConfirmed = 1 
	--and a.Start_Date<= @dateStart and @dateStart<=a.Stop_Date 
	and a.id_ObjectLease = @id_ObjectLease and a.fullPayed = 0 and (@id_tMonthPlane = 0 or mp.id is not null)
	and (@id_tMonthPlane = 0 or mp.id is not null)
) as t
where
		--YEAR(t.Start_Date)<= YEAR(@dateStart) and MONTH(t.Start_Date)<= MONTH(@dateStart)  and YEAR(t.Stop_Date)>= YEAR(@dateStart) and MONTH(t.Stop_Date)>= MONTH(@dateStart)
		(t.Stop_Date>=@dateStart and DATEADD(day,-1, DATEADD(month,1,@dateStart))>=t.Start_Date) or @id_tMonthPlane <>0

END
