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
	@dateStart date
AS
BEGIN
	SET NOCOUNT ON;


select 
	ad.id,
	torg.Abbreviation+' ' + lt.cName as nameLandLord,
	torgt.Abbreviation+' ' + ltt.cName as nameTenant,
	ol.cName as nameObject,
	tc.TypeContract,
	a.Agreement,	

	b.Abbreviation as Build,
	bp.Abbreviation as [Floor],
	case 
		when a.id_TypeContract = 1 then s.cName
		when a.id_TypeContract = 2 then rp.NumberPlace
		when a.id_TypeContract = 3 then lp.NumberPlot
	end as namePlace,
	a.Total_Area,
	a.Cost_of_Meter,
	a.Total_Sum,
	ad.DateDocument as Start_Date,
	isnull(ad.Date_of_Departure,a.Stop_Date) as Stop_Date

from 
	Arenda.j_Agreements a
		inner join Arenda.s_TypeContract tc on tc.id = a.id_TypeContract

		inner join Arenda.j_AdditionalDocuments ad on ad.id_Agreements = a.id
		inner join Arenda.s_TypeDoc td on td.id = ad.id_TypeDoc

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
where 
	a.isConfirmed = 1 and a.Start_Date<= @dateStart and @dateStart<=a.Stop_Date and td.Rus_Name = 'Акт приёма-передачи'
	
END
