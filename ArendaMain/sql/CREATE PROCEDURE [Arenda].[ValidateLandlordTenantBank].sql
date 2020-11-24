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
-- Description:	Проверка банка на дублики
-- =============================================
CREATE PROCEDURE [Arenda].[ValidateLandlordTenantBank]	
		@id int,
		@id_bank int,
		@PaymentAccount varchar(20)
AS
BEGIN
	
	


select 
	l.id
from 
	[Arenda].[s_LandlordTenantBank] l
where 
	l.id <>@id and l.id_Bank =@id_bank and l.PaymentAccount = @PaymentAccount 
		
END
