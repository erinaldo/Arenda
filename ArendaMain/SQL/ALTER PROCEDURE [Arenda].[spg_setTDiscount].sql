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
	@id_Agreements int,
	@dateStart date,
	@dateEnd date = null,
	@id_TypeDiscount int,	
	@id_StatusDiscount int,
	@Discount decimal(16,2),
	@id_user int,
	@result int = 0,
	@isDel int= 0
AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRY 
	IF @isDel = 0
		BEGIN

			if( @dateStart is not null and @dateEnd is not null)
				BEGIN
					if exists(select top(1) id from Arenda.j_tDiscount where id_Agreements = @id_Agreements and id_StatusDiscount in (1,2) and DateStart<=@dateStart and @dateStart<=DateEnd and DateEnd is not null and id  <> @id)
						begin select -2 as id;return; end

					if exists(select top(1) id from Arenda.j_tDiscount where id_Agreements = @id_Agreements and id_StatusDiscount in (1,2) and DateStart<=@dateEnd and @dateEnd<=DateEnd and DateEnd is not null and id  <> @id)
						begin select -2 as id;return; end

					if exists(select top(1) id from Arenda.j_tDiscount where id_Agreements = @id_Agreements and id_StatusDiscount in (1,2) and @dateStart<= DateStart and DateStart<=@dateEnd and DateEnd is not null and id  <> @id)
						begin select -2 as id;return; end

					if exists(select top(1) id from Arenda.j_tDiscount where id_Agreements = @id_Agreements and id_StatusDiscount in (1,2) and @dateStart<= DateEnd and DateEnd<=@dateEnd and DateEnd is not null and id  <> @id)
						begin select -2 as id;return; end
				END
			ELSE IF @dateStart is not null and @dateEnd is null
				BEGIN
					if exists(select top(1) id from Arenda.j_tDiscount where id_Agreements = @id_Agreements and id_StatusDiscount in (1,2) and DateStart = @dateStart  and DateEnd is null and id  <> @id )
						begin select -2 as id;return; end
				END

			IF @id = 0
				BEGIN
					INSERT INTO [Arenda].[j_tDiscount]  (id_Agreements,DateStart,DateEnd,id_TypeDiscount,id_StatusDiscount,Discount,id_Editor,DateEdit)
					VALUES (@id_Agreements,@dateStart,@dateEnd,@id_TypeDiscount,@id_StatusDiscount,@Discount,@id_user,GETDATE())

					SELECT  cast(SCOPE_IDENTITY() as int) as id
					return;
				END
			ELSE
				BEGIN
					UPDATE [Arenda].[j_tDiscount] 
					set		
							id_Agreements = @id_Agreements,
							DateStart = @dateStart,
							DateEnd = @dateEnd,
							id_TypeDiscount=@id_TypeDiscount,							
							id_StatusDiscount = @id_StatusDiscount,
							Discount = @Discount,
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
