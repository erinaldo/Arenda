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
ALTER PROCEDURE [Arenda].[spg_GetPaymentsForAgreemetns]
	@id_Agreements int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select 
		SUM(case when isToTenant = 1 then -1 else 1 end * p.Summa) as Summa
	from 
		Arenda.j_PaymentContract p
	where 
		id_Agreements = @id_Agreements and p.id_PayType = 2
	group by id_Agreements

END

