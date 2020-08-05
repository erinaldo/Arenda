SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Запись журнала объекта скидки 
-- =============================================
ALTER PROCEDURE [Arenda].[spg_setDiscountObject]		 
	@id int,
	@id_tDiscount int,
	@id_ObjectLease int,
	@id_Buildings int = null,
	@id_Floor int = null,
	@id_rentalObject int,
	@typeRentalObject int,
	--@id_Sections int = null,
	--@id_LandPlot int = null,
	--@id_ReclamaPlace int = null,
	@isException bit,
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
					--INSERT INTO [Arenda].[j_DiscountObject]  (id_tDiscount,id_ObjectLease,id_Buildings,id_Floor,id_Sections,id_LandPlot,id_ReclamaPlace,isException,id_Editor,DateEdit)
					--VALUES (@id_tDiscount,@id_ObjectLease,@id_Buildings,@id_Floor,@id_Sections,@id_LandPlot,@id_ReclamaPlace,@isException,@id_user,GETDATE())
					INSERT INTO [Arenda].[j_DiscountObject]  (id_tDiscount,id_ObjectLease,id_Buildings,id_Floor,id_rentalObject,typeRentalObject,isException,id_Editor,DateEdit)
					VALUES (@id_tDiscount,@id_ObjectLease,@id_Buildings,@id_Floor,@id_rentalObject,@typeRentalObject,@isException,@id_user,GETDATE())

					SELECT  cast(SCOPE_IDENTITY() as int) as id
					return;
				END
			ELSE
				BEGIN
					UPDATE [Arenda].[j_DiscountObject] 
					set		id_ObjectLease = @id_ObjectLease,
							id_Buildings = @id_Buildings,
							id_Floor = @id_Floor,
							id_rentalObject= @id_rentalObject,
							typeRentalObject=@typeRentalObject,
							--id_Sections=@id_Sections,
							--id_LandPlot=@id_LandPlot,
							--id_ReclamaPlace = @id_ReclamaPlace,
							isException = @isException,
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
					
					IF NOT EXISTS(select TOP(1) id from [Arenda].[j_DiscountObject]  where (@id = 0 or id = @id) and id_tDiscount = @id_tDiscount)
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
					--DELETE FROM [Arenda].[j_DiscountValue]  where id = @id and id_tDiscount = @id_tDiscount
					DELETE FROM [Arenda].[j_DiscountObject]  where  (@id = 0 OR id = @id) and id_tDiscount = @id_tDiscount
					RETURN
				END
		END
END TRY 
BEGIN CATCH 
	SELECT -9999 as id
	return;
END CATCH
	
END
