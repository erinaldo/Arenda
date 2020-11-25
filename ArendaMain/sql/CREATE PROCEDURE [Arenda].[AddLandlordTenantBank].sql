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
-- Description:	ƒобавление банка на арендател€/арендодател€
-- =============================================
ALTER PROCEDURE [Arenda].[AddLandlordTenantBank]	
		@id int,
		@id_bank int,
		@PaymentAccount varchar(20),
		@id_LandlordTenant int,
		@isActive bit,
		@id_User int,
		@isDel bit
AS
BEGIN
	
IF EXISTS (select TOP(1) id from Arenda.s_LandlordTenantBank where id = @id)
	BEGIN
	
		IF @isDel = 1
			BEGIN
				IF EXISTS (select * from Arenda.[j_AgreementsBank] where id_LandlordTenantBank = @id)
					BEGIN
						select -1 as id;
						return;

					END

				DELETE FROM Arenda.s_LandlordTenantBank WHERE id = @id
				select @id as id;
				return;
			END
		ELSE
			BEGIN
				UPDATE 
					Arenda.s_LandlordTenantBank
				SET
					id_Bank = @id_bank,
					id_LandlordTenant =@id_LandlordTenant,
					PaymentAccount = @PaymentAccount,
					isActive = @isActive,
					id_Editor = @id_User,
					DateEdit = GETDATE()
				WHERE
					id =@id

				select @id as id;
				return; 
			END
	END
ELSE
	BEGIN
		INSERT INTO Arenda.s_LandlordTenantBank (id_LandlordTenant,id_Bank,PaymentAccount,isActive,id_Creator,id_Editor,DateCreate,DateEdit)
		VALUES (@id_LandlordTenant,@id_bank,@PaymentAccount,@isActive,@id_User,@id_User,GETDATE(),GETDATE())

		select cast( SCOPE_IDENTITY() as int)  as id;
		return;
	END
		
END
