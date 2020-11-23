USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[GetPayments]    Script Date: 20.11.2020 14:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 14-11-2014
-- Description:	Получение списка типов доп. оплат
-- =============================================
CREATE PROCEDURE [Arenda].[GetAddPayment]	
	
AS
BEGIN
	
	select 
		id,cName,isActive 
	from 
		[Arenda].s_AddPayment		
END
