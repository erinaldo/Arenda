SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-14
-- Description:	Подтверждение пений Д
-- =============================================
CREATE PROCEDURE [Arenda].[spg_setFines]		 
	@id_Agreements int,
	@id int, 
	@summPenalty numeric(12,2),
	@period varchar(7),
	@id_user int
AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRY 
		UPDATE Arenda.j_tPenalty 
		SET id_StatusPenalty = 3,id_Editor = @id_user, DateEdit = GETDATE()
		WHERE id = @id


		DECLARE @id_addPayment  int
		SELECT @id_addPayment = id FROM Arenda.s_AddPayment WHERE cName like 'Пени'


		IF @id_addPayment is null
			BEGIN
				select -1 as id
				return
			END

		 INSERT INTO Arenda.j_Fines (id_Agreements,DateFines,Summa,Comment,id_АddPayment,id_Editor,DateEdit)
		 VALUES (@id_Agreements,GETDATE(),@summPenalty,@period,@id_addPayment,@id_user,GETDATE())

		select cast(SCOPE_IDENTITY() as int) as id
END TRY 
BEGIN CATCH 
	SELECT -9999 as id
	return;
END CATCH
	
END
