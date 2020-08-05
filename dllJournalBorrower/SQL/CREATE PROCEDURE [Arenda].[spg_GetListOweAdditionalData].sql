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
-- Description:	Получение дополнительной информации для должников
-- =============================================
ALTER PROCEDURE [Arenda].[spg_GetListOweAdditionalData]
	@typeData int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @typeData = 1
		BEGIN
			select 0 as id
		END
	ELSE
		BEGIN		
			select a.id,sum(a.Summa) as SummaPaymentFine,sum(a.sumPay) as SummaFine,sum(a.Summa) - sum(a.sumPay) as SummaPenny,round(((sum(a.Summa) - sum(a.sumPay))/sum(a.Summa))*100,2) as PrcPenny from (
			select 
				a.id,f.Summa,cast(0 as numeric (16,2)) as sumPay				
			from	
				Arenda.j_Agreements a
					inner join Arenda.j_Fines f on f.id_Agreements = a.id
					inner join Arenda.s_AddPayment ad on ad.id = f.id_АddPayment
			where 
				a.isConfirmed = 1 and ad.isActive = 1

			UNION ALL

			select a.id,cast(0 as numeric (16,2)) as Summa,f.Summa  as sumPay
			from Arenda.j_Agreements a 
					inner join Arenda.j_Fines f on f.id_Agreements = a.id
					inner join Arenda.j_PaymentContract pc on pc.id_Fines = f.id
			where 
				a.isConfirmed = 1
			) as a
			GROUP BY 
				a.id
		END



END

