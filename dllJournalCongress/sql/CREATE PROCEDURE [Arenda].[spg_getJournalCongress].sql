SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-17
-- Description:	Получение Журнала съездов
-- =============================================
ALTER PROCEDURE [Arenda].[spg_getJournalCongress]		 	
	@dateStart date,
	@dateEnd date
AS
BEGIN
	SET NOCOUNT ON;

select 
	ad.id,
	ddd.id as id_LinkPetitionLeave,
	torg.Abbreviation+' ' + lt.cName as nameLandLord,
	torgt.Abbreviation+' ' + ltt.cName as nameTenant,
	ol.cName as nameObject,
	a.Agreement,	
	--
	case 
		when a.id_TypeContract = 1 then b.Abbreviation+', '+f.Abbreviation+', номер секции: ' +s.cName
		when a.id_TypeContract = 2 then bp.Abbreviation+', номер рекламного места: '+rp.NumberPlace
		when a.id_TypeContract = 3 then 'Номер земельного участка: '+lp.NumberPlot
	end as namePlace,
	--
	a.Cost_of_Meter,
	ad.DateDocument,
	ad.Date_of_Departure,	
	a.failComment,
	a.id_ObjectLease,
	adc.id as typeAdc,
	isnull(adc.isConfirmed,0) as isConfirmed,
	cast(case when ddd.id is null then 0 else 1 end as bit) as isLinkPetitionLeave,
	(select id from Arenda.j_AdditionalDocuments sssss where sssss.id_Agreements = ad.id_Agreements and sssss.id_TypeDoc = 6) as isCancelAgreements,
	isnull(adcddd.isConfirmed,0) as isConfirmed_LinkPetitionLeave
from 
	Arenda.j_Agreements a
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

		left join Arenda.j_AddDocConfirmed adc on adc.id_AdditionalDocuments = ad.id
		left join Arenda.j_AdditionalDocuments ddd  on ddd.id_PetitionLeave = ad.id
		left join Arenda.j_AddDocConfirmed adcddd on adcddd.id_AdditionalDocuments = ddd.id
where 
	a.isConfirmed = 1 and td.Rus_Name = 'Заявление на съезд' and @dateStart<= ad.Date_of_Departure and ad.Date_of_Departure <= @dateEnd and ad.isActive = 1
	
	
	
END
