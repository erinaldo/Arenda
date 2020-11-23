USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[GetPayments]    Script Date: 20.11.2020 14:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 14-11-2014
-- Description:	Получение списка оплат по договору
-- =============================================
ALTER PROCEDURE [Arenda].[GetReportPayAgreement]	
	@dateStart date,
	@dateEnd date	
AS
BEGIN
	
	select 
	too.Abbreviation + ' ' + lt.cName as nameTenant,
	tool.Abbreviation + ' ' + ltl.cName as nameLandLord,
	a.Agreement,
	isnull(case 
		when a.id_TypeContract = 1 then isnull(b.cName+', ','')+isnull(f.cName+', ','')+isnull(s.cName,'')
		when a.id_TypeContract = 2 then isnull(b2.cName,'')+', номер рекламного места:'+isnull(rp.NumberPlace,'')
		when a.id_TypeContract = 3 then 'Номер земельного участка:'+isnull(lp.NumberPlot,'')
	end,'') as namePlace,

	p.Date,
	p.Summa,
	p.isCash,
	p.id_PayType,
	pt.cName as namePayType,
	p.isToTenant -- 0 оренд, 1 возврат
	,case when p.isToTenant = 0 then 'Плата' else 'Возврат средств' end as nameToTenant
	,p.PlaneDate
	,sp.cName as nameSavePayment
	,ap.cName as nameAddPayment
	,fn.DateFines
from 
	Arenda.j_PaymentContract p 
		left join Arenda.s_PayType pt on pt.id = p.id_PayType
		left join Arenda.j_Agreements a on a.id = p.id_Agreements
		inner join Arenda.s_Landlord_Tenant lt on lt.id = a.id_Tenant
		inner join Arenda.s_Type_of_Organization too on too.id = lt.id_Type_Of_Organization

		left join  Arenda.j_AdditionalAgreements aa on aa.id_Agreements = a.id
		left join  Arenda.s_SavePayment sp on  sp.id = aa.id_SavePayment

		inner join Arenda.s_Landlord_Tenant ltl on ltl.id = a.id_Landlord
		inner join Arenda.s_Type_of_Organization tool on tool.id = ltl.id_Type_Of_Organization

		left join Arenda.s_Building b  on b.id = a.id_Buildings and a.id_TypeContract = 1
		left join Arenda.s_Floors f on f.id = a.id_Floor
		left join Arenda.s_Sections s on s.id = a.id_Section

		left join Arenda.s_ReclamaPlace rp on rp.id = a.id_Section and a.id_TypeContract = 2
		left join Arenda.s_Building b2 on b2.id = rp.id_Building

		left join Arenda.s_LandPlot lp on lp.id = a.id_Section and a.id_TypeContract = 3

		left join Arenda.j_Fines fn on fn.id = p.id_Fines 
		left join Arenda.s_AddPayment ap on ap.id = fn.id_АddPayment
where
	@dateStart<=p.Date and p.Date<=@dateEnd
		
END
