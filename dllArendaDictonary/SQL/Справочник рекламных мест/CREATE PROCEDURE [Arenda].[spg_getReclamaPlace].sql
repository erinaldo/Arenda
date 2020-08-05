SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Получение списка рекламных мест
-- =============================================
ALTER PROCEDURE [Arenda].[spg_getReclamaPlace]		 	
AS
BEGIN
	SET NOCOUNT ON;

	select 
		r.id,
		r.id_Building,
		r.id_ObjectLease,
		r.isActive,
		r.Length,
		r.NumberPlace,
		r.Width,
		o.cName as nameObject,
		b.cName as nameBuild,
		cast(r.Length as varchar(1000))+' x '+cast(r.Width as varchar(1000)) as nameSize,
		cast(0 as bit) as isSelect,
		cast(0 as bit) as isException
	from 
		[Arenda].[s_ReclamaPlace] r
			left join [Arenda].[s_ObjectLease] o on o.id = r.id_ObjectLease
			left join [Arenda].[s_Building] b on b.id = r.id_Building
	
	
	
END
