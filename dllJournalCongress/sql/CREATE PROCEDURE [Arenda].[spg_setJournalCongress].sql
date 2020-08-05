SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-14
-- Description:	Подтверждение или аннуляция съезда
-- =============================================
CREATE PROCEDURE [Arenda].[spg_setJournalCongress]		 
	@id int,	
	@id_user int
AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRY 
		IF EXISTS (select id from Arenda.j_AddDocConfirmed where id_AdditionalDocuments = @id)
			UPDATE Arenda.j_AddDocConfirmed SET isConfirmed = 1, DateCreate = GETDATE(),id_Creator = @id_user where id_AdditionalDocuments = @id
		ELSE
			INSERT INTO Arenda.j_AddDocConfirmed (id_AdditionalDocuments,isConfirmed,id_Creator,DateCreate) values (@id,1,@id_user,GETDATE())

		select 0 as id
END TRY 
BEGIN CATCH 
	SELECT -9999 as id
	return;
END CATCH
	
END
