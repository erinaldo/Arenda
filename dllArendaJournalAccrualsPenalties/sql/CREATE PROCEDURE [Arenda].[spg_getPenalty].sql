USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[spg_getTypeDiscount]    Script Date: 20.07.2020 15:46:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-20
-- Description:	Получение списка пений за период
-- =============================================
ALTER PROCEDURE [Arenda].[spg_getPenalty]	
	@period varchar(7)
AS
BEGIN
	SET NOCOUNT ON;

select
	tp.id,
	tp.id_Agreements,
	a.id_TypeContract,
	a.id_ObjectLease,
	tp.id_StatusPenalty,
	p.id as id_payment,
	too.Abbreviation+' '+lt.cName as nameTenant,
	tc.TypeContract,
	a.Agreement,
	substring(CONVERT(varchar(10),cast(tp.PeriodCredit as date),104),4,7) as PeriodCredit,
	p.SummaCredit,
	pc.Date as datePayContract,
	pc.Summa as sumPayContract,
	p.CountDaysCredit,
	p.PercentPenalty,
	p.SummaPenalty
from
	Arenda.j_tPenalty tp
		INNER JOIN Arenda.j_Agreements a on a.id = tp.id_Agreements
		LEFT JOIN Arenda.s_Landlord_Tenant lt on lt.id = a.id_Tenant
		LEFT JOIN Arenda.s_Type_of_Organization too on too.id = lt.id_Type_Of_Organization

		LEFT JOIN Arenda.s_TypeContract tc on tc.id = a.id_TypeContract
		LEFT JOIN Arenda.j_Penalty p on p.id_tPenalty = tp.id
		LEFT JOIN Arenda.j_PaymentContract pc on pc.id = p.id_PaymentContract
WHERE
	substring(CONVERT(varchar(10),cast(tp.PeriodCredit as date),104),4,7)  = @period
ORDER BY 
	tp.id_Agreements asc


END
