SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Запись журнала данных по скидкам 
-- =============================================
CREATE PROCEDURE [Arenda].[spg_setDiscountValue]		 
	@id int,
	@id_tDiscount int,
	@PercentDiscount numeric(10,2) = null,
	@DiscountPrice numeric(16,2) =  null,
	@Price numeric(16,2) =  null,
	@TotalPrice numeric(16,2) =  null,
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
					INSERT INTO [Arenda].[j_DiscountValue]  (id_tDiscount,PercentDiscount,DiscountPrice,Price,TotalPrice,id_Editor,DateEdit)
					VALUES (@id_tDiscount,@PercentDiscount,@DiscountPrice,@Price,@TotalPrice,@id_user,GETDATE())

					SELECT  cast(SCOPE_IDENTITY() as int) as id
					return;
				END
			ELSE
				BEGIN
					UPDATE [Arenda].[j_DiscountValue] 
					set		PercentDiscount = @PercentDiscount,
							DiscountPrice=@DiscountPrice,
							Price=@Price,
							TotalPrice = @TotalPrice,
							id_Editor=@id_user,
							DateEdit=GETDATE()
					where id = @id and id_tDiscount = @id_tDiscount
										
					SELECT @id as id
					return;
				END
		END
	ELSE
		BEGIN
			IF @result = 0
				BEGIN
					
					IF NOT EXISTS(select TOP(1) id from [Arenda].[j_DiscountValue]  where id = @id and id_tDiscount = @id_tDiscount)
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
					DELETE FROM [Arenda].[j_DiscountValue]  where id = @id and id_tDiscount = @id_tDiscount
					--DELETE FROM [Arenda].[j_DiscountObject]  where  id = @id and id_tDiscount = @id_tDiscount
					RETURN
				END
		END
END TRY 
BEGIN CATCH 
	SELECT -9999 as id
	return;
END CATCH
	
END
