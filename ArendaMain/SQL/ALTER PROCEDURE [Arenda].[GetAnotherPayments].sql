USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[GetAnotherPayments]    Script Date: 29.07.2020 10:52:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Butakov I.
-- Create date: 26-02-2015
-- Description:	Получение данных для справочника дополнительных оплат
-- =============================================
ALTER PROCEDURE [Arenda].[GetAnotherPayments]
	@All bit
AS
BEGIN

	select 
		addP.id,
		addP.cName,
		addP.isActive,
		addP.isMeter
	from 
		Arenda.s_AddPayment addP
	where 
		(@All = 1)
		or
		(@All <> 1 and addP.isActive=1)		
	order by 
		addP.cName
		
END
