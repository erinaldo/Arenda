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
-- Description:	ƒобавление ссылки на банк арендодател€ и договора
-- =============================================
CREATE PROCEDURE [Arenda].[AddAgreementsBank]	
		@id_Agreements int,
		@id_LandlordTenantBank int 
		
AS
BEGIN
	IF exists (select id from [Arenda].[j_AgreementsBank] where id_Agreements = @id_Agreements)
		BEGIN
			UPDATE [Arenda].[j_AgreementsBank]
			SET id_LandlordTenantBank = @id_LandlordTenantBank
			WHERE id_Agreements = @id_Agreements
		END
	ELSE
		BEGIN
			INSERT INTO [Arenda].[j_AgreementsBank](id_Agreements,id_LandlordTenantBank)
			VALUES (@id_Agreements,@id_LandlordTenantBank)
		END
END
