USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[GetListDoc]    Script Date: 24.07.2020 10:49:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2021-01-05
-- Description:	Обновление даты отправки письма
-- =============================================
CREATE PROCEDURE [Arenda].[UpdateDateSendLoadAccount1C]	
	@id int
AS
BEGIN
	
	UPDATE 
		Arenda.j_LoadAccount1C
	SET 
		DateSendMail = GETDATE()
	WHERE
		id = @id

END