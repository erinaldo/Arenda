SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Запись заголовка журнала скидок
-- =============================================
ALTER PROCEDURE [Arenda].[spg_setTDiscount]		 
	@id int,
	@dateStart date,
	@dateEnd date = null,
	@id_typeDiscount int,
	@id_TypeTenant int,
	@id_TypeAgreements int,
	@id_StatusDiscount int,
	@isActive bit,
	@id_user int,
	@result int = 0,
	@isDel int
AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRY 
	IF @isDel = 0
		BEGIN

			--IF EXISTS (select TOP(1) id from [Arenda].[j_tDiscount] where id <>@id and id_TypeDiscount = @id_typeDiscount AND id_TypeTenant = @id_TypeTenant AND id_TypeAgreements = @id_TypeAgreements)
			--	BEGIN
			--		SELECT -1 as id;
			--		return;
			--	END

			IF @id = 0
				BEGIN
					INSERT INTO [Arenda].[j_tDiscount]  (DateStart,DateEnd,id_TypeDiscount,id_TypeTenant,id_TypeAgreements,id_StatusDiscount,id_Editor,DateEdit)
					VALUES (@dateStart,@dateEnd,@id_typeDiscount,@id_TypeTenant,@id_TypeAgreements,@id_StatusDiscount,@id_user,GETDATE())

					SELECT  cast(SCOPE_IDENTITY() as int) as id
					return;
				END
			ELSE
				BEGIN
					UPDATE [Arenda].[j_tDiscount] 
					set		DateStart = @dateStart,
							DateEnd = @dateEnd,
							id_TypeDiscount=@id_typeDiscount,
							id_TypeTenant=@id_TypeTenant,
							id_TypeAgreements=@id_TypeAgreements,
							id_StatusDiscount = @id_StatusDiscount,
							id_Editor=@id_user,
							DateEdit=GETDATE()
					where id = @id
										
					SELECT @id as id
					return;
				END
		END
	ELSE
		BEGIN
			IF @result = 0
				BEGIN
					
					IF NOT EXISTS(select TOP(1) id from [Arenda].[j_tDiscount] where id = @id)
						BEGIN
							select -1 as id
							return;
						END
											
					--IF EXISTS(select TOP(1) id from [Arenda].[j_DiscountObject] where id_LandPlot = @id)
					--	BEGIN
					--		select -2 as id
					--		return;
					--	END
					
					select 0 as id
					return;
				END
			ELSE
				BEGIN					
					DELETE FROM [Arenda].[j_DiscountValue]  where id_tDiscount = @id
					DELETE FROM [Arenda].[j_DiscountObject]  where id_tDiscount = @id
					DELETE FROM [Arenda].[j_tDiscount]  where id = @id
					RETURN
				END
		END
END TRY 
BEGIN CATCH 
	SELECT -9999 as id
	return;
END CATCH
	
END
