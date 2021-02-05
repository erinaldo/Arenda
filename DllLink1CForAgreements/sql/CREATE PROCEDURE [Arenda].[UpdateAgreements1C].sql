USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[GetListDoc]    Script Date: 24.07.2020 10:49:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-27
-- Description:	Запись ссылки на номер 1С
-- =============================================
CREATE PROCEDURE [Arenda].[UpdateAgreements1C]	
	@id_Agreements int,
	@NumberAgreement varchar(max)
AS
BEGIN	
	UPDATE Arenda.j_Agreements set Agreement1C = @NumberAgreement where id = @id_Agreements
END

