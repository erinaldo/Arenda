SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-14
-- Description:	Сохранение тела пенальти
-- =============================================
CREATE PROCEDURE [Arenda].[spg_setPenalty]		 
	@id int,	
	@SummaPenalty numeric(12,2),
	@PercentPenalty numeric(4,2),
	@id_user int
AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRY 
		UPDATE Arenda.j_Penalty 
		SET SummaPenalty  = @SummaPenalty, PercentPenalty = @PercentPenalty,id_Editor = @id_user, DateEdit = GETDATE()
		WHERE id = @id

		select 0 as id
END TRY 
BEGIN CATCH 
	SELECT -9999 as id
	return;
END CATCH
	
END
