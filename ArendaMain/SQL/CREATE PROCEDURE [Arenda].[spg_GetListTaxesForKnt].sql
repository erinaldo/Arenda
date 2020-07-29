USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[GetTaxes]    Script Date: 29.07.2020 11:37:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G Y
-- Create date: 2020-07-29
-- Description:	Получение списка доп оплат для КНТ
-- =============================================
ALTER PROCEDURE [Arenda].[spg_GetListTaxesForKnt]
	@DatePlane date
AS
BEGIN


select 
	jf.id,
	a.Agreement,
	jf.DateFines,
	sad.cName,
	jf.Summa,
	isnull(jf.isConfirmed,0) as isConfirmed,
	cast(0 as bit) as isSelect
from 
	[Arenda].[j_Fines] jf
		left join [Arenda].s_AddPayment sad on jf.id_АddPayment = sad.id 
		left join Arenda.j_Agreements a on a.id = jf.id_Agreements
where
	jf.PlanDate = @DatePlane
order by 
	jf.id_Agreements asc
	
		
END
