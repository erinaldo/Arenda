USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[GetPayments]    Script Date: 20.11.2020 14:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 23-11-2020
-- Description:	Получение списка доп оплат за период
-- =============================================
ALTER PROCEDURE [Arenda].[GetReportFinesPay]	
	@date date	
AS
BEGIN
	
	select 
	ob.cName as nameObject,
	too.Abbreviation + ' ' + lt.cName as nameTenant,
	a.Agreement,
	sad.cName,
	jf.Summa,
	jf.DateFines,
	jf.MetersData,
	jf.Comment,
	isnull(pf.Summa,0.00) as sumPay,
	jf.Summa - isnull(pf.Summa,0.00)  as resultSum
from 
	[Arenda].j_Fines jf
		--left join Arenda.j_PaymentFines pf on pf.id_Fines = jf.id
		left join Arenda.j_PaymentContract pf on pf.id_Fines = jf.id
		left join [Arenda].s_AddPayment sad on jf.id_АddPayment = sad.id 
		left join Arenda.j_Agreements a on a.id = jf.id_Agreements
		left join Arenda.s_ObjectLease ob on ob.id = a.id_ObjectLease

		inner join Arenda.s_Landlord_Tenant lt on lt.id = a.id_Tenant
		inner join Arenda.s_Type_of_Organization too on too.id = lt.id_Type_Of_Organization
where
	YEAR(jf.DateFines) = YEAR(@date) and MONTH(jf.DateFines) = MONTH(@date) 

		
END
