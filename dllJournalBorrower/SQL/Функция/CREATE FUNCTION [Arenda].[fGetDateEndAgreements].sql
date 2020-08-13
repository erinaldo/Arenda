USE [dbase2]
GO
/****** Object:  UserDefinedFunction [Arenda].[ConvertDateToString]    Script Date: 13.08.2020 15:39:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S G Y
-- Create date: 2020-08-13
-- Description:	Получение даты окончания договора
-- =============================================
CREATE FUNCTION [Arenda].[fGetDateEndAgreements]
(
	@id_Agreements int
)
RETURNS date
AS
BEGIN
	

DECLARE @dateDoc date,@dateAddiDoc date,@dateOut date

select 
	@dateDoc = Stop_Date 
from 
	Arenda.j_Agreements where id = @id_Agreements

select 
	@dateAddiDoc = ad.DateDocument
from 
	Arenda.j_AdditionalDocuments ad
		inner join Arenda.s_TypeDoc td on td.id = ad.id_TypeDoc
where
	ad.id_Agreements = @id_Agreements 
	and ad.isActive = 1 
	and td.Rus_Name = 'Акт приёма-передачи (возврат)' 

select 
	@dateOut = ad.DateDocument
from 
	Arenda.j_AdditionalDocuments ad
		inner join Arenda.s_TypeDoc td on td.id = ad.id_TypeDoc
	--inner join Arenda.j_AddDocConfirmed ac on ac.id_AdditionalDocuments = ad.id

		left join Arenda.j_AdditionalDocuments ad2 on ad2.id_PetitionLeave = ad.id and ad2.id_Agreements = @id_Agreements
		left join Arenda.s_TypeDoc td2 on td2.id = ad2.id_TypeDoc and 	td2.Rus_Name = 'Аннуляция заявления на съезд'
		left join Arenda.j_AddDocConfirmed ac2 on ac2.id_AdditionalDocuments = ad2.id				
where 
	ad.id_Agreements = @id_Agreements 
	and ad.isActive = 1 
	and td.Rus_Name = 'Заявление на съезд' 
	and isnull(ac2.isConfirmed,0) = 0
--and ac2.isConfirmed = 1
	
DECLARE @reDate date
IF(@dateAddiDoc is not null)
	BEGIN set @reDate = @dateAddiDoc END
ELSE IF @dateOut is not null AND @dateOut<cast(GETDATE() as date)
	BEGIN set @reDate = @dateOut END
ELSE 
	BEGIN set @reDate = @dateDoc END

return @reDate

END
