SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-06-23
-- Description:	Получение списка зданий
-- =============================================
CREATE PROCEDURE [Arenda].[spg_getTypeDiscount]		 
AS
BEGIN
	SET NOCOUNT ON;

SELECT 
	p.id,p.cName,p.isActive
FROM 
	Arenda.s_TypeDiscount p


END
