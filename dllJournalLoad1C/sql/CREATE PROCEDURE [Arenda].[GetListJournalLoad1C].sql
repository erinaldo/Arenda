USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[GetListDoc]    Script Date: 24.07.2020 10:49:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-27
-- Description:	ѕолучение списка оплат по договору
-- =============================================
ALTER PROCEDURE [Arenda].[GetListJournalLoad1C]	
	@id_object int = 0,
	@dateStart date, 
	@dateEnd date
AS
BEGIN


select 
	a.id,
	lac.DateLoad,
	ol.cName as nameObjectLease,	
	too.Abbreviation + ' ' + lt.cName as nameTenant,
	lloo.Abbreviation + ' ' + ll.cName as nameLandLord,
	a.Agreement,
	tc.TypeContract,
	a.id_TypeContract,
	isnull(case 
		when a.id_TypeContract = 1 then isnull(b.cName+', ','')+isnull(f.cName+' ','')+isnull(s.cName,'')
		when a.id_TypeContract = 2 then isnull(b2.cName,'')+', номер рекламного места:'+isnull(rp.NumberPlace,'')
		when a.id_TypeContract = 3 then 'Ќомер земельного участка:'+isnull(lp.NumberPlot,'')
	end,'') as namePlace,

	isnull(case 
		when a.id_TypeContract = 1 then isnull(b.cName+' ','')
		when a.id_TypeContract = 2 then isnull(b2.cName,'')
		when a.id_TypeContract = 3 then ''
	end,'') as buildName,
	isnull(case 
		when a.id_TypeContract = 1 then isnull(f.cName+' ','')
		when a.id_TypeContract = 2 then ''
		when a.id_TypeContract = 3 then ''
	end,'') as floorName,
	isnull(case 
		when a.id_TypeContract = 1 then isnull(s.cName,'')
		when a.id_TypeContract = 2 then isnull(rp.NumberPlace,'')
		when a.id_TypeContract = 3 then isnull(lp.NumberPlot,'')
	end,'') as sectionName,
	a.id_Landlord,
	lac.NumberAccount,
	lac.DateAccount,
	lac.TypePayment,
	DateSendMail,
	lt.email as emailSend,
	ll.email as emailSender
from
	[Arenda].[j_LoadAccount1C] lac
		inner join Arenda.j_Agreements a on a.id = lac.id_Agreements
		inner join Arenda.s_Landlord_Tenant lt on lt.id = a.id_Tenant
		inner join Arenda.s_Type_of_Organization too on too.id = lt.id_Type_Of_Organization
		inner join Arenda.s_ObjectLease ol on ol.id = a.id_ObjectLease

		inner join Arenda.s_Landlord_Tenant ll on ll.id = a.id_Landlord
		inner join Arenda.s_Type_of_Organization lloo on lloo.id = ll.id_Type_Of_Organization

		left join Arenda.s_TypeContract tc on tc.id = a.id_TypeContract


		left join Arenda.s_Building b  on b.id = a.id_Buildings and a.id_TypeContract = 1
		left join Arenda.s_Floors f on f.id = a.id_Floor
		left join Arenda.s_Sections s on s.id = a.id_Section

		left join Arenda.s_ReclamaPlace rp on rp.id = a.id_Section and a.id_TypeContract = 2
		left join Arenda.s_Building b2 on b2.id = rp.id_Building

		left join Arenda.s_LandPlot lp on lp.id = a.id_Section and a.id_TypeContract = 3
where 
	@dateStart<=lac.DateLoad and lac.DateLoad<=@dateEnd
	and (@id_object = 0 or a.id_ObjectLease = @id_object)

END

