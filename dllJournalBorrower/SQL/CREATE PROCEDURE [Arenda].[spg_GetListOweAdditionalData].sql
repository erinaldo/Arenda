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
			select 
				d.id_Agreements,
				d.DateStart,
				d.DateEnd,
				d.id_TypeDiscount,
				d.Discount,
				a.Total_Sum,
				a.Total_Area,
				a.Start_Date,
				a.Stop_Date
			from 
				Arenda.j_tDiscount d
					inner join Arenda.j_Agreements a on a.id = d.id_Agreements
			where 
				a.isConfirmed = 1 
				--and a.id = 1053 
				and d.id_StatusDiscount = 2
			order by 
				d.DateStart asc
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

