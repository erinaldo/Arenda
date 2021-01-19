USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[setScan]    Script Date: 19.01.2021 13:47:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Molotkova_IS
-- Create date: 2019-03-22
-- Description:	Запись сканов
-- Editor:		Molotkova_IS
-- Edit date:	2019-12-15
-- Description: Добавлено поле Path
-- =============================================
ALTER PROCEDURE [Arenda].[setScan]
	@id_Doc int,
	@nameFile varchar(max),
	@idUser int,
	@Extension varchar(50),
	@id_DocType int,
	@DateDocument date,
	@Path varchar(max)
	
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	INSERT INTO 
		[Arenda].j_Scan(id_Doc, cName, Scan, DateInsert, id_Insert, Extension, id_DocType, DateDocument, [Path])
	VALUES
		(@id_Doc, @nameFile, null, GETDATE(), @idUser, @Extension, @id_DocType, @DateDocument,@Path)

	SELECT cast(SCOPE_IDENTITY() as int) as id

END
