SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-14
-- Description:	—ÓÁ‰‡ÌËÂ ËÎË Û‰‡ÎÂÌËˇ Á‡„ÓÎÓ‚Í‡ ÔÎ‡Ì-ÓÚ˜∏Ú‡
-- =============================================
ALTER PROCEDURE [Arenda].[spg_setTPlanReport]		 
	@id int,	
	@PeriodMonthPlan date,
	@id_ObjectLease int,
	@is—onfirmed bit,
	--@id_—onfirmed int = null,
	--@Date—onfirmed datetime = null,
	@isDel bit,
	@result int,
	@id_user int
AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRY 

IF @id = 0
	BEGIN
		INSERT INTO [Arenda].[j_tPlanReport]
			   ([PeriodMonthPlan]
			   ,[id_ObjectLease]
			   ,[is—onfirmed]
			   ,[id_—onfirmed]
			   ,[Date—onfirmed]
			   ,[id_Editor]
			   ,[DateEdit])
		 VALUES
			   (@PeriodMonthPlan
			   ,@id_ObjectLease
			   ,0
			   ,null
			   ,null
			   ,@id_user
			   ,GETDATE())

		select cast(SCOPE_IDENTITY() as int) as id
	END
ELSE
	BEGIN
		IF @isDel = 0
			BEGIN

				UPDATE 
					[Arenda].[j_tPlanReport] 
				SET
				    [PeriodMonthPlan] = @PeriodMonthPlan
				   ,[id_ObjectLease] = @id_ObjectLease
				   ,[is—onfirmed] = @is—onfirmed
				   ,[id_—onfirmed] = case when @is—onfirmed = 1 then  @id_user else null end
				   ,[Date—onfirmed] = case when @is—onfirmed = 1 then  GETDATE() else null end
				   ,[id_Editor]  = @id_user
				   ,[DateEdit] = GETDATE()
				WHERE 
					id = @id

				Select @id as id
			END
		ELSE
			BEGIN
				IF @result = 0
					BEGIN

						if not exists (select top(1) id from Arenda.j_tPlanReport where id = @id )
							BEGIN								
								select -1 as id
								return
							END

						if exists (select top(1) id from Arenda.j_tPlanReport where id = @id and is—onfirmed = 1)
							BEGIN								
								select -2 as id
								return
							END

						Select 0 as id
					END
				ELSE
					BEGIN
						DELETE FROM Arenda.j_PlanReport where id_tPlanReport = @id
						DELETE FROM Arenda.j_tPlanReport where id = @id

						Select @id as id
					END
			END
	END
		
END TRY 
BEGIN CATCH 
	SELECT -9999 as id,ERROR_MESSAGE() as msg
	return;
END CATCH
	
END
