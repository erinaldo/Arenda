SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	ѕолучение списка видов дейстельности
-- =============================================
CREATE PROCEDURE [Arenda].[spg_getTypeActivities]		 	
AS
BEGIN
	SET NOCOUNT ON;

	select 
		r.id,
		r.isActive,
		r.cName
	from 
		[Arenda].[s_TypeActivities] r	
END
