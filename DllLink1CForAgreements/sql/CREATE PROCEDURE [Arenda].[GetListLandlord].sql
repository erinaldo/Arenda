USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[GetListDoc]    Script Date: 24.07.2020 10:49:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-27
-- Description:	Получение списка оплат по договору
-- =============================================
ALTER PROCEDURE [Arenda].[GetListLandlord]	
AS
BEGIN

select 
	l.id,
	isnull(t.Abbreviation+' ','')+l.cName as cName,
	l.id_ObjectLease,
	l.isActive
from 
	Arenda.s_Landlord_Tenant l 
		left join Arenda.s_Type_of_Organization t on t.id =  l.id_Type_Of_Organization
where l.isActive = 1 and l.Sign_Landlord_Tenant = 0

END

