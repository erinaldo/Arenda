SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-14
-- Description:	Создание или удаления тела План отчёта
-- =============================================
CREATE PROCEDURE [Arenda].[spg_setPlanReport]		 
	@id_tPlanReport int,
	@id_Agreements int,
	@SummaContract numeric(16,2),
	@Discount numeric(16,2),
	@SecurityPayment numeric(16,2),
	@EndPlan numeric(16,2),
	@Penalty numeric(16,2),
	@OtherPayments numeric(16,2),
	@TotalPaid numeric(16,2),
	@Included numeric(16,2),
	@Credit numeric(16,2),
	@OverPayment numeric(16,2),
	@isDel bit

AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRY 

IF @isDel = 0
	BEGIN
		INSERT INTO [Arenda].[j_PlanReport]
			   (	[id_tPlanReport],
					[id_Agreements],
					[SummaContract],
					[Discount],
					[SecurityPayment],
					[EndPlan],
					[Penalty],
					[OtherPayments],
					[TotalPaid],
					[Included],
					[Credit],
					[OverPayment])
		 VALUES
			   (	@id_tPlanReport,
					@id_Agreements,
					@SummaContract,
					@Discount,
					@SecurityPayment,
					@EndPlan,
					@Penalty,
					@OtherPayments,
					@TotalPaid,
					@Included,
					@Credit,
					@OverPayment)

		select cast(SCOPE_IDENTITY() as int) as id
	END
ELSE
	BEGIN
		DELETE FROM Arenda.j_PlanReport where id_tPlanReport = @id_tPlanReport
		select 0 as id
	END
	
END TRY 
BEGIN CATCH 
	SELECT -9999 as id
	return;
END CATCH
	
END
