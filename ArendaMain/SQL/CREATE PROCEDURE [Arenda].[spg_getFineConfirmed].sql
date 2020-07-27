SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-27
-- Description:	Получение списка доп оплат для выбора в оплате
-- =============================================
ALTER PROCEDURE [Arenda].[spg_getFineConfirmed]		
	@id_Agreements int,
	@dateStart date,
	@dateEnd date

AS
BEGIN
	SET NOCOUNT ON;

select 
	f.id,
	f.Summa,
	f.DateFines,
	p.cName,
	isnull(pf.Summa,0) as pfSumma,
	f.Summa - isnull(pf.Summa,0) as resDolg,
	f.PlanDate
from 
	[Arenda].[j_Fines] f
		inner join Arenda.s_AddPayment p on p.id = f.id_АddPayment
		left join Arenda.j_PaymentFines pf on pf.id_Fines = f.id
where 
	f.id_Agreements = @id_Agreements and f.isConfirmed = 1 and @dateStart<= f.DateFines and f.DateFines<=@dateEnd
	
	
	
END
