USE [dbase2]
GO

/****** Object:  StoredProcedure [Arenda].[LibGetActivities]    Script Date: 13.07.2020 16:07:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		АНС
-- Create date: 13.07.2020
-- Description:	Получает список 
-- =============================================
CREATE PROCEDURE [Arenda].[LibGetActivities] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT	0 AS id,
			' Все виды' AS cName
	UNION
	SELECT	STA.id,
			STA.cName
	FROM Arenda.s_TypeActivities STA
	WHERE STA.isActive = 1
	ORDER BY cName ASC
END
GO

