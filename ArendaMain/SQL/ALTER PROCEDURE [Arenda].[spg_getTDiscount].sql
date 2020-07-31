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
	@id_Agreements int
AS
BEGIN
	SET NOCOUNT ON;

select 
	d.id,
	d.DateStart,
	d.DateEnd, 
	d.id_TypeDiscount,
	d.id_StatusDiscount,
	td.cName as nameTypeDiscount,
	d.Discount,
	sd.cName as nameStatusDiscount
from 
	[Arenda].[j_tDiscount] d
		left join Arenda.s_TypeDiscount td on td.id = d.id_TypeDiscount
		left join Arenda.s_StatusDiscount sd on sd.id = d.id_StatusDiscount
where 
	id_Agreements = @id_Agreements


END
