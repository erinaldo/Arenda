SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-17
-- Description:	Получение списка план-отчётов 
-- =============================================
CREATE PROCEDURE [Arenda].[spg_getTPlanReport]		 	
	@dateStart date,
	@dateEnd date,
	@id_objectLease int  = 0
AS
BEGIN
	SET NOCOUNT ON;

select 
	p.id,
	p.isСonfirmed,
	p.id_ObjectLease,
	p.PeriodMonthPlan,
	p.DateEdit,
	isnull(l.FIO,'') as FIO,
	isnull(lc.FIO,'') as FIOConfirmed,
	p.DateСonfirmed,
	o.cName as nameObject
from 
	Arenda.j_tPlanReport p
		left join dbo.ListUsers lc on lc.id = p.id_Сonfirmed
		left join dbo.ListUsers l on l.id = p.id_Editor
		left join Arenda.s_ObjectLease o on o.id = p.id_ObjectLease
where  
	@dateStart<=p.PeriodMonthPlan and p.PeriodMonthPlan<=@dateEnd and (@id_objectLease = 0 or p.id_ObjectLease = @id_objectLease)
	
END
