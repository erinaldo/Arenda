SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	«апись справочника земельных участков
-- =============================================
ALTER PROCEDURE [Arenda].[spg_setLandPlot]		 
	@id int,
	@NumberPlot varchar(max),	
	@id_ObjectLease int,	
	@AreaPlot bigint,
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

			IF EXISTS (select TOP(1) id from [Arenda].[s_LandPlot] where id <>@id and LTRIM(RTRIM(LOWER(NumberPlot))) = LTRIM(RTRIM(LOWER(@NumberPlot))) AND id_ObjectLease = @id_ObjectLease)
				BEGIN
					SELECT -1 as id;
					return;
				END

			IF @id = 0
				BEGIN
					INSERT INTO [Arenda].[s_LandPlot]  (NumberPlot,id_ObjectLease,AreaPlot,isActive,id_Editor,DateEdit)
					VALUES (@NumberPlot,@id_ObjectLease,@AreaPlot,1,@id_user,GETDATE())

					SELECT  cast(SCOPE_IDENTITY() as int) as id
					return;
				END
			ELSE
				BEGIN
					UPDATE [Arenda].[s_LandPlot] 
					set		NumberPlot = @NumberPlot,
							id_ObjectLease = @id_ObjectLease,
							AreaPlot=@AreaPlot,
							isActive=@isActive,
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
					
					IF NOT EXISTS(select TOP(1) id from [Arenda].[s_LandPlot] where id = @id)
						BEGIN
							select -1 as id
							return;
						END

					
					IF EXISTS(select TOP(1) id from [Arenda].[j_DiscountObject] where id_rentalObject = @id and typeRentalObject = 3)
						BEGIN
							select -2 as id
							return;
						END
					
					select 0 as id
					return;
				END
			ELSE
				BEGIN
					DELETE FROM [Arenda].[s_LandPlot]  where id = @id
					RETURN
				END
		END
END TRY 
BEGIN CATCH 
	SELECT -9999 as id
	return;
END CATCH
	
END
