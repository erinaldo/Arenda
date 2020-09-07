USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[GetDocuments]    Script Date: 07.09.2020 11:39:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		SPorykhin G Y
-- Create date: 2020-09-07
-- Description:	Получение документов
-- =============================================
ALTER PROCEDURE [Arenda].[spg_GetDocuments]
	
AS
BEGIN
SET NOCOUNT ON

	select
		id,
		id_Doc,
		--Scan,
		cName,
		Extension
	from Arenda.j_Scan 
	where [Path] is null or [Path] = ''

END