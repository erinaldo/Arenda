DECLARE @id_Agreements int  = 3063


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
	
IF(@dateAddiDoc is not null)
	BEGIN select @dateAddiDoc; return;END
ELSE IF @dateOut is not null AND @dateOut<cast(GETDATE() as date)
	BEGIN select @dateOut; return;END
ELSE BEGIN select @dateDoc; return;END
