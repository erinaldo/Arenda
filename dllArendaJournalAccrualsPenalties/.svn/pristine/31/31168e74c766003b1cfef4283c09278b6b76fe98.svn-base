USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[spg_getTypeDiscount]    Script Date: 20.07.2020 15:46:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-20
-- Description:	Получение списка преиода кредитования
-- =============================================
ALTER PROCEDURE [Arenda].[spg_getListPeriodCredit]		 
AS
BEGIN
	SET NOCOUNT ON;

select a.PeriodCredit from(
select
	substring(CONVERT(varchar(10),cast(PeriodCredit as date),104),4,7) as PeriodCredit
FROM
	Arenda.j_tPenalty where PeriodCredit is not null ) as a
GROUP BY
	a.PeriodCredit

END
