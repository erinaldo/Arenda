USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[spg_getTypeDiscount]    Script Date: 20.07.2020 15:46:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-20
-- Description:	Получение типов договора
-- =============================================
ALTER PROCEDURE [Arenda].[spg_getTypeContract]		 
AS
BEGIN
	SET NOCOUNT ON;

SELECT 
	p.id
	,p.TypeContract as cName
	,p.isActive
	,p.Comment
FROM 
	Arenda.s_TypeContract p


END
