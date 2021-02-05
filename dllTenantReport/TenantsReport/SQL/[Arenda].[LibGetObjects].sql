USE [dbase2]
GO

/****** Object:  StoredProcedure [Arenda].[LibGetObjects]    Script Date: 13.07.2020 15:55:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		АНС
-- Create date: 13.07.2020
-- Description:	Получает список объектов
-- =============================================
CREATE PROCEDURE [Arenda].[LibGetObjects] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT	0 as id,
			' Все объекты' AS cName
	UNION
	SELECT	SOL.id,
			SOL.cName
	FROM Arenda.s_ObjectLease SOL
	WHERE SOL.isActive = 1
	ORDER BY cName ASC
END
GO

