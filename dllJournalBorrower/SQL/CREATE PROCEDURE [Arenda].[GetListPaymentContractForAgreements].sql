USE [dbase2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-11-20
-- Description:	Получение списка оплат по договору
-- =============================================
ALTER PROCEDURE [Arenda].[GetListPaymentContractForAgreements]
	@id_Agreements int 
AS
BEGIN	
	SET NOCOUNT ON;

select 
	--p.Summa,
	case when p.isToTenant = 1 then -1 else 1 end * p.Summa as Summa,
	p.Date,
	t.cName,
	case when p.isCash = 0 then 'Нал.' else 'Без нал.' end as nameTypeCash
from 
	Arenda.j_PaymentContract p
		left join Arenda.s_PayType t on t.id = p.id_PayType
where 
	p.id_Agreements = @id_Agreements


END

