select 
	d.*,
	a.Total_Sum+a.Phone as Total_Sum
from 
	Arenda.j_tDiscount d
		inner join Arenda.j_Agreements a on a.id = d.id_Agreements