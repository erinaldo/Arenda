USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[GetTaxes]    Script Date: 29.07.2020 11:37:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Butakov I.
-- Create date: 14-11-2014
-- Description:	Получение списка штрафов по договору
-- =============================================
ALTER PROCEDURE [Arenda].[GetTaxes]
	@id_Agreements int	
AS
BEGIN

select 
	t.id,
	t.id_Agreements,
	t.TaxDate,
	t.penalty,
	t.PaymentSum,
	t.Comment,
	t.DateEdit,
	t.id_Editor,
	t.Editor,
	t.PaymentId,
	t.PaymentName,
	t.scan,
	Convert(varchar(max),(t.penaltyDec - t.PaymentSumDec)) as Debt,
	t.MetersData,
	isnull(t.isConfirmed,0) as isConfirmed,
	t.PlanDate
from 
(SELECT jf.[id]
      ,jf.[id_Agreements]
      ,jf.[DateFines] as TaxDate
      ,Convert(varchar(max),jf.[Summa]) as penalty
      ,Convert(varchar(max),
      (select isnull(sum(jpf.Summa),0) from Arenda.j_PaymentFines jpf where jpf.id_Fines = jf.id) )
			as PaymentSum
	  ,jf.[Summa] as penaltyDec
	  ,(select isnull(sum(jpf.Summa),0) from Arenda.j_PaymentFines jpf where jpf.id_Fines = jf.id) 
			as PaymentSumDec
      ,jf.[Comment]
      ,jf.[DateEdit]
      ,jf.[id_Editor]
      ,(select isnull(ltrim(rtrim(lu.FIO)),'') from ListUsers lu where lu.id = jf.[id_Editor]) as Editor
	  ,isnull(ltrim(rtrim(sad.cName)),'') as PaymentName
      ,isnull(ltrim(rtrim(sad.id)),'') as PaymentId
      ,case 
			when (select COUNT(*) from Arenda.j_CheckScan chec where chec.id_Fines = jf.id)>0
			then '+'
			else ''
		end as scan
	 ,jf.MetersData
	 ,jf.isConfirmed
	 ,jf.PlanDate
FROM 
	[Arenda].[j_Fines] jf
	left join [Arenda].s_AddPayment sad
		on jf.id_АddPayment = sad.id 
where 
	jf.[id_Agreements] = @id_Agreements	) t
order by t.TaxDate	
	
		
END
