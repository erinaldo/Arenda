SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-06-23
-- Description:	Получение списка секций
-- =============================================
ALTER PROCEDURE [Arenda].[spg_getTDiscount]		 
AS
BEGIN
	SET NOCOUNT ON;

select 
	d.id,
	d.DateStart,
	d.DateEnd, 
	d.id_TypeAgreements,
	d.id_TypeDiscount,
	d.id_TypeTenant,
	d.id_StatusDiscount,
	td.cName as nameTypeDiscount,
	ta.cName as nameTypeAgreements,
	tt.cName as nameTypeTenant,
	dv.DiscountPrice,
	dv.PercentDiscount,
	dv.Price,
	dv.TotalPrice
from 
	[Arenda].[j_tDiscount] d
		left join Arenda.s_TypeDiscount td on td.id = d.id_TypeDiscount
		left join Arenda.s_TypeAgreements ta on ta.id = d.id_TypeAgreements
		left join Arenda.s_TypeTenant tt on tt.id = d.id_TypeTenant
		left join Arenda.j_DiscountValue dv on dv.id_tDiscount = d.id



END
