SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-06-23
-- Description:	Получение списка секций
-- =============================================
ALTER PROCEDURE [Arenda].[spg_getDiscountObject]		 
	@id_tDiscount int
AS
BEGIN
	SET NOCOUNT ON;

select 
	d.id,
	d.id_Buildings,
	d.id_Floor,
	d.id_ObjectLease,
	d.id_rentalObject,
	d.typeRentalObject,
	--d.id_LandPlot,	
	--d.id_ReclamaPlace,
	--d.id_Sections,
	d.isException
from 
	[Arenda].[j_DiscountObject] d
where 
	d.id_tDiscount = @id_tDiscount


END
