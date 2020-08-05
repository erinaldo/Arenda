SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-06-23
-- Description:	Получение списка секций
-- =============================================
ALTER PROCEDURE [Arenda].[spg_getSections]		 
AS
BEGIN
	SET NOCOUNT ON;

select 
	s.id,
	s.id_Building,
	s.id_ObjectLease,
	s.id_Floor,
	s.isActive,
	s.cName,
	f.cName as nameFloor,
	b.cName as nameBuilding,
	cast(0 as bit) as isSelect,
	cast(0 as bit) as isException,
	s.Total_Area

from 
	Arenda.s_Sections s
		left join Arenda.s_Floors f on f.id = s.id_Floor
		left join Arenda.s_Building b on b.id  = s.id_Building

END
