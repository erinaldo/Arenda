DECLARE 
	@dateStart date = '2020-01-01', @dateEnd date = '2020-08-01'

select 
	torg.Abbreviation+' ' + torg.cName as nameLandLord,
	torgt.Abbreviation+' ' + torgt.cName as nameTenant,
	ol.cName as nameObject,
	a.Agreement,	
	--
	case 
		when a.id_TypeContract = 1 then b.Abbreviation+', '+f.Abbreviation+', номер секции: ' +s.cName
		when a.id_TypeContract = 2 then bp.Abbreviation+', номер рекламного места: '+rp.NumberPlace
		when a.id_TypeContract = 3 then 'Номер земельного участка: '+lp.NumberPlot
	end as namePlace,
	--
	a.Cost_of_Meter,
	ad.DateDocument,
	ad.Date_of_Departure,	
	a.failComment

from 
	Arenda.j_Agreements a
		inner join Arenda.j_AdditionalDocuments ad on ad.id_Agreements = a.id
		inner join Arenda.s_TypeDoc td on td.id = ad.id_TypeDoc

		left join Arenda.s_Landlord_Tenant lt on lt.id = a.id_Landlord
		left join Arenda.s_Type_of_Organization torg on torg.id = lt.id_Type_Of_Organization

		left join Arenda.s_Landlord_Tenant ltt on ltt.id = a.id_Tenant
		left join Arenda.s_Type_of_Organization torgt on torgt.id = ltt.id_Type_Of_Organization

		left join Arenda.s_ObjectLease ol on ol.id = a.id_ObjectLease

		left join Arenda.s_Building b on b.id = a.id_Buildings and a.id_TypeContract = 1
		left join Arenda.s_Floors f on f.id = a.id_Floor
		left join Arenda.s_Sections s on s.id = a.id_Section

		left join Arenda.s_ReclamaPlace rp on rp.id = a.id_Section and a.id_TypeContract = 2
		left join Arenda.s_Building bp on bp.id = rp.id_Building

		left join Arenda.s_LandPlot lp on lp.id = a.id_Section and a.id_TypeContract = 3


where 
	a.isConfirmed = 1 and td.Rus_Name = 'Заявление на съезд' and @dateStart<= ad.Date_of_Departure and ad.Date_of_Departure <= @dateEnd