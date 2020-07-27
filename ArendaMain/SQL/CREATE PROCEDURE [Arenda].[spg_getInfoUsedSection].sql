SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Получение информации о использование секций другими
-- =============================================
CREATE PROCEDURE [Arenda].[spg_getInfoUsedSection]	
	@id int,
	@dateStart date,
	@dateEnd date,
	@id_section int,
	@id_TypeContract int
AS
BEGIN
	SET NOCOUNT ON;

IF EXISTS(
select 
	id 
from 
	Arenda.j_Agreements
where
	id_TypeContract = @id_TypeContract and id_Section = @id_section  and id <> @id
	and 
	(
	(Start_Date<= @dateStart and @dateStart <=Stop_Date) 
	or (Start_Date<= @dateEnd and @dateEnd <=Stop_Date) 
	or (@dateStart<= Start_Date and Start_Date <=@dateEnd) 
	or (@dateStart<= Stop_Date and Stop_Date <=@dateEnd) 
	)) select 1 as id ELSE select 0 as id
	
	
	
END
