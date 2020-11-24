USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[GetPayments]    Script Date: 20.11.2020 14:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G. Y.
-- Create date: 24-11-2020
-- Description:	Получение списка банков привязанных к арендатору\арендодателю
-- =============================================
ALTER PROCEDURE [Arenda].[GetLandlordTenantBank]	
		@id_LandLord int 
AS
BEGIN
	
	


select 
	l.id,
	l.id_Bank,
	l.PaymentAccount,
	b.cName,
	b.BIC,
	b.CorrespondentAccount,
	l.isActive
from 
	[Arenda].[s_LandlordTenantBank] l
		left join Arenda.s_Banks b on b.id = l.id_Bank
where 
	l.id_LandlordTenant = @id_LandLord
		
END
