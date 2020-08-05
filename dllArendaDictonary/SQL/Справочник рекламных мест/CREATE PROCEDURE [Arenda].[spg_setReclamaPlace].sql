SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Запись справочника рекламных мест
-- =============================================
ALTER PROCEDURE [Arenda].[spg_setReclamaPlace]		 
	@id int,
	@NumberPlace varchar(max),	
	@id_ObjectLease int,
	@id_Building int,
	@Length bigint,
	@Width bigint,
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

			IF EXISTS (select TOP(1) id from [Arenda].[s_ReclamaPlace] where id <>@id and LTRIM(RTRIM(LOWER(NumberPlace))) = LTRIM(RTRIM(LOWER(@NumberPlace))) AND id_ObjectLease = @id_ObjectLease AND id_Building = @id_Building)
				BEGIN
					SELECT -1 as id;
					return;
				END

			IF @id = 0
				BEGIN
					INSERT INTO [Arenda].[s_ReclamaPlace]  (NumberPlace,id_ObjectLease,id_Building,Length,Width,isActive,id_Editor,DateEdit)
					VALUES (@NumberPlace,@id_ObjectLease,@id_Building,@Length,@Width,1,@id_user,GETDATE())

					SELECT  cast(SCOPE_IDENTITY() as int) as id
					return;
				END
			ELSE
				BEGIN
					UPDATE [Arenda].[s_ReclamaPlace] 
					set		NumberPlace = @NumberPlace,
							id_ObjectLease = @id_ObjectLease,
							id_Building = @id_Building,
							[Length] = @Length,
							Width=@Width,
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
					
					IF NOT EXISTS(select TOP(1) id from [Arenda].[s_ReclamaPlace] where id = @id)
						BEGIN
							select -1 as id
							return;
						END

					
					IF EXISTS(select TOP(1) id from [Arenda].[j_DiscountObject] where id_rentalObject = @id and typeRentalObject = 2)
						BEGIN
							select -2 as id
							return;
						END
					
					select 0 as id
					return;
				END
			ELSE
				BEGIN
					DELETE FROM [Arenda].[s_ReclamaPlace]  where id = @id
					RETURN
				END
		END
END TRY 
BEGIN CATCH 
	SELECT -9999 as id
	return;
END CATCH
	
END
