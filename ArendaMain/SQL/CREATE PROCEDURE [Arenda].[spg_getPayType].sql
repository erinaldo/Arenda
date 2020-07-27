SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-27
-- Description:	Получение Типов оплаты
-- =============================================
CREATE PROCEDURE [Arenda].[spg_getPayType]		
AS
BEGIN
	SET NOCOUNT ON;

	select 
		p.id,p.cName,p.isActive
	from 
		[Arenda].[s_PayType] p
	
	
	
END
