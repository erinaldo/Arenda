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
-- Description:	Запись договора 1С
-- =============================================
ALTER PROCEDURE [Arenda].[SetAgreement1CForAgreement]	
	@id_Agreements int,
	@NumberAccount varchar(max),
	@DateAccount datetime,
	@NumberAgreement varchar(max),
	@TypePayment varchar(max),
	@isAdd bit,
	@id_Scan int,
	@id_user int,
	@isNewData bit = false
AS
BEGIN
	
	IF(@isAdd=1)
		BEGIN
			UPDATE Arenda.j_Agreements set Agreement1C = @NumberAgreement where id = @id_Agreements
		END
	
	if not exists(select id from Arenda.j_LoadAccount1C where id_Agreements = @id_Agreements and id_Scan = @id_Scan) or @isNewData = 1
		BEGIN
			INSERT INTO Arenda.j_LoadAccount1C (id_Agreements,NumberAccount,NumberAgreement,TypePayment,DateAccount,DateLoad,DateSendMail,id_Loader,id_Scan)
			VALUES (@id_Agreements,@NumberAccount,@NumberAgreement,@TypePayment,@DateAccount,GETDATE(),null,@id_user,@id_Scan)		
		END
	ELSE
		BEGIN
			UPDATE  
				Arenda.j_LoadAccount1C 
			SET 
				NumberAccount = @NumberAccount,
				NumberAgreement = @NumberAgreement,
				TypePayment = @TypePayment,
				DateAccount = @DateAccount,
				DateLoad =GETDATE(),
				DateSendMail = null,
				id_Loader = @id_user,
				id_Scan = @id_Scan
			WHERE
				id_Agreements = @id_Agreements
		END
END

