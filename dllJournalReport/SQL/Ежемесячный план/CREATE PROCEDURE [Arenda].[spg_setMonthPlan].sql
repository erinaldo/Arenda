SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-14
-- Description:	—оздание или удалени€ тела ежемес€чного плана
-- =============================================
CREATE PROCEDURE [Arenda].[spg_setMonthPlan]		 
	@id_tMonthPlan int,	
	@id_Agreements int, 
	@SummaContract numeric(16,2),
	@Discount numeric(16,2),
	@Plan numeric(16,2),
	@isDel bit

AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRY 

IF @isDel = 0
	BEGIN
		INSERT INTO [Arenda].[j_MonthPlan]
			   ([id_tMonthPlan]
			   ,[id_Agreements]
			   ,[SummaContract]
			   ,[Discount]
			   ,[Plan])
		 VALUES
			   (@id_tMonthPlan
			   ,@id_Agreements
			   ,@SummaContract
			   ,@Discount
			   ,@Plan)

		select cast(SCOPE_IDENTITY() as int) as id
	END
ELSE
	BEGIN
		DELETE FROM Arenda.j_MonthPlan where id_tMonthPlan = @id_tMonthPlan
		select 0 as id
	END
	
END TRY 
BEGIN CATCH 
	SELECT -9999 as id
	return;
END CATCH
	
END
