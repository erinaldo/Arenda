

select id_Agreements,max(DateSeal) as DateSeal INTO #tableDateSeal from 
Arenda.j_SealSections where DateOpen is null
group by id_Agreements

select 
	a.id,
	too.Abbreviation + ' ' + lt.cName as nameTenant,
	a.Agreement,
	ol.cName as nameObjectLease,
	a.id_TypeContract,
	isnull(case 
		when a.id_TypeContract = 1 then b.cName+', '+f.cName+', '+s.cName
		when a.id_TypeContract = 2 then b2.cName+', номер рекламного места:'+rp.NumberPlace
		when a.id_TypeContract = 3 then 'Ќомер земельного участка:'+lp.NumberPlot
	end,'ƒа вот, что-то ничего не нашлось!') as namePlace,
	a.Cost_of_Meter,
	a.Total_Sum,
	ds.DateSeal
from
	Arenda.j_Agreements a 
		inner join Arenda.s_Landlord_Tenant lt on lt.id = a.id_Tenant
		inner join Arenda.s_Type_of_Organization too on too.id = lt.id_Type_Of_Organization
		inner join Arenda.s_ObjectLease ol on ol.id = a.id_ObjectLease
		left join #tableDateSeal ds on ds.id_Agreements  = a.id


		left join Arenda.s_Building b  on b.id = a.id_Buildings and a.id_TypeContract = 1
		left join Arenda.s_Floors f on f.id = a.id_Floor
		left join Arenda.s_Sections s on s.id = a.id_Section

		left join Arenda.s_ReclamaPlace rp on rp.id = a.id_Section and a.id_TypeContract = 2
		left join Arenda.s_Building b2 on b2.id = rp.id_Building

		left join Arenda.s_LandPlot lp on lp.id = a.id_Section and a.id_TypeContract = 3


DROP TABLE #tableDateSeal