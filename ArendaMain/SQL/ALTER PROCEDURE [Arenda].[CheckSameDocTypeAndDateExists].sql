USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[CheckSameDocTypeAndDateExists]    Script Date: 03.08.2020 11:43:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Butakov I.
-- Create date: 20-03-2015
-- Description:	Проверка при сохранении доп документов
-- на наличие доп документов такого же типа и на ту же дату
-- =============================================
ALTER PROCEDURE [Arenda].[CheckSameDocTypeAndDateExists]
	@id_agreements int,
	@id_TypeDoc int,	
	@DateDocument datetime
	AS
BEGIN

DECLARE @idTmp int = null;
IF(@id_TypeDoc = 8)
	BEGIN
		
		select TOP(1)
			@idTmp = ad.id
		from 
			Arenda.j_AdditionalDocuments ad
				inner join Arenda.s_TypeDoc td on td.id = ad.id_TypeDoc
				inner join Arenda.j_AddDocConfirmed ac on ac.id_AdditionalDocuments = ad.id

				left join Arenda.j_AdditionalDocuments ad2 on ad2.id_PetitionLeave = ad.id and ad2.id_Agreements = @id_agreements
				left join Arenda.s_TypeDoc td2 on td2.id = ad2.id_TypeDoc and 	td2.Rus_Name = 'Аннуляция заявления на съезд'
				left join Arenda.j_AddDocConfirmed ac2 on ac2.id_AdditionalDocuments = ad2.id
		where 
			ad.id_Agreements = @id_agreements 
			and ad.isActive = 1 
			and td.Rus_Name = 'Заявление на съезд' 
			and  isnull(ac2.isConfirmed,0) = 0 
			and ac.isConfirmed = 1
	
		IF @idTmp is not null 
			BEGIN
				select @idTmp as id,'У договора присутствует заявление на съезд.\nСоздать заявление на съезд невозможно.\n' as msg
				return;
			END
	END
ELSE
IF(@id_TypeDoc = 9)
	BEGIN
		select @idTmp = ad.id 
		from 
			Arenda.j_AdditionalDocuments ad
				inner join Arenda.s_TypeDoc td on td.id = ad.id_TypeDoc
		where	
			td.Rus_Name = 'Соглашение о расторжении договора'
			and ad.id_Agreements = @id_agreements 
			and ad.isActive = 1  

		IF @idTmp is not null 
			BEGIN
				select @idTmp as id,'У договора присутсвует соглашение о расторжении договора.\nСоздать аннуляцию заявления на съезд невозможно.\n' as msg
				return;
			END
	END
ELSE IF (@id_TypeDoc = 7 OR @id_TypeDoc = 6 OR @id_TypeDoc = 3)
	BEGIN
		IF EXISTS (select top(1) id from Arenda.j_AdditionalDocuments where		id_TypeDoc  = @id_TypeDoc and id_Agreements = @id_agreements and isActive = 1)
			BEGIN
				select 0 as id,'Выбранный тип дополнительного документа\n уже присутствует у договора.\nСоздать второй такой доп. документ нельзя.\n' as msg
				return;
			END
	END


select 
	id 
from 
	Arenda.j_AdditionalDocuments 
where 
	id_Agreements = @id_agreements
	and
	id_TypeDoc = @id_TypeDoc 
	and 
	CONVERT(date,DateDocument) = CONVERT(date,@DateDocument)
	and 
	isActive = 1
		
END
