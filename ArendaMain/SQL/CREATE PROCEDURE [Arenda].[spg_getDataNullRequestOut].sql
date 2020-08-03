SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-08-03
-- Description:	Получение информации об аннуляции съезда
-- =============================================
ALTER PROCEDURE [Arenda].[spg_getDataNullRequestOut]
	@id_Agreements int
AS
BEGIN
	SET NOCOUNT ON;

--if exists(
	select ad.id,ad.DateDocument from Arenda.j_AdditionalDocuments ad
		inner join Arenda.s_TypeDoc td on td.id = ad.id_TypeDoc
		inner join Arenda.j_AddDocConfirmed ac on ac.id_AdditionalDocuments = ad.id
		left join Arenda.j_AdditionalDocuments ad2 on ad2.id_PetitionLeave = ad.id and ad2.id_Agreements = @id_Agreements
		left join Arenda.s_TypeDoc td2 on td2.id = ad2.id_TypeDoc and 	td2.Rus_Name = 'Аннуляция заявления на съезд'
	where	
		td.Rus_Name = 'Заявление на съезд'
		and ad.id_Agreements = @id_Agreements 
		and ad.isActive = 1  
		and ac.isConfirmed = 1
		and ad2.id_PetitionLeave is null
		--)
--BEGIN
--	select 1 as id
--END
--ELSE 
--BEGIN
--	select 0 as id
--END	
	
	
	
END
