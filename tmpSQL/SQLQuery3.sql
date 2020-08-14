----select * from Arenda.j_Agreements
----select * from Arenda.s_TypeActivities
--select* from Arenda.j_tPlanReport

--select * from Arenda.j_AdditionalDocuments where id_Agreements = 1051 and id_TypeDoc in (6)
--select * from Arenda.j_AdditionalDocuments where id_Agreements = 1051 and id_TypeDoc in (8)
--select * from Arenda.j_AdditionalDocuments where id_Agreements = 1051 and id_TypeDoc in (9)

--select * from Arenda.s_TypeDoc
--select * from Arenda.s_AddPayment




DECLARE @dateStart date = '2020-04-02',@dateEnd date =  '2020-04-12', @id_Agreements int = 3059

select * from Arenda.j_tDiscount where id_Agreements = 3059 and id_StatusDiscount in (1,2)

if( @dateStart is not null and @dateEnd is not null)
	BEGIN
		if exists(select top(1) id from Arenda.j_tDiscount where id_Agreements = @id_Agreements and id_StatusDiscount in (1,2) and DateStart<=@dateStart and @dateStart<=DateEnd and DateEnd is not null)
			begin select -2 as id;return; end

		if exists(select top(1) id from Arenda.j_tDiscount where id_Agreements = @id_Agreements and id_StatusDiscount in (1,2) and DateStart<=@dateEnd and @dateEnd<=DateEnd and DateEnd is not null)
			begin select -2 as id;return; end

		if exists(select top(1) id from Arenda.j_tDiscount where id_Agreements = @id_Agreements and id_StatusDiscount in (1,2) and @dateStart<= DateStart and DateStart<=@dateEnd and DateEnd is not null)
			begin select -2 as id;return; end

		if exists(select top(1) id from Arenda.j_tDiscount where id_Agreements = @id_Agreements and id_StatusDiscount in (1,2) and @dateStart<= DateEnd and DateEnd<=@dateEnd and DateEnd is not null)
			begin select -2 as id;return; end
	END
ELSE IF @dateStart is not null and @dateEnd is null
	BEGIN
		if exists(select top(1) id from Arenda.j_tDiscount where id_Agreements = @id_Agreements and id_StatusDiscount in (1,2) and DateStart = @dateStart  and DateEnd is null)
			begin select -2 as id;return; end
	END