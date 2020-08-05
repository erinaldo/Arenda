SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-06-23
-- Description:	Получение списка секций
-- =============================================
CREATE PROCEDURE [Arenda].[spg_getDiscountValue]		 
	@id_tDiscount int
AS
BEGIN
	SET NOCOUNT ON;

select 
	d.id,d.DiscountPrice,d.PercentDiscount,d.Price,d.TotalPrice
from 
	[Arenda].[j_DiscountValue] d
where 
	d.id_tDiscount = @id_tDiscount


END
