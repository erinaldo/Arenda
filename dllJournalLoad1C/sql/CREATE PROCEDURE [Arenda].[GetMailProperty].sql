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
-- Description:	ѕолучение списка настроек почтовых серверов
-- =============================================
CREATE PROCEDURE [Arenda].[GetMailProperty]	
AS
BEGIN

select 
	cName,
	host,
	port
from 
	[Arenda].[s_mail_property]



END