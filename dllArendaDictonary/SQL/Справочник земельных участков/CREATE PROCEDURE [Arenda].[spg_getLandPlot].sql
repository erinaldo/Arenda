SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	ѕолучение списка земельных участков
-- =============================================
ALTER PROCEDURE [Arenda].[spg_getLandPlot]		 	
AS
BEGIN
	SET NOCOUNT ON;

	select 
		r.id,
		r.id_ObjectLease,
		r.isActive,
		r.NumberPlot,
		r.AreaPlot,
		o.cName as nameObject,
		cast(0 as bit) as isSelect,
		cast(0 as bit) as isException
	from 
		[Arenda].[s_LandPlot] r
			left join [Arenda].[s_ObjectLease] o on o.id = r.id_ObjectLease
			
	
	
	
END
