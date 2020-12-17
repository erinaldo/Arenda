select id,id_Bank_TMP,PaymentAccount_TMP,isActive,427,GETDATE(),427,GETDATE() from Arenda.s_Landlord_Tenant 
where 
	id_Bank_TMP is not null and id_Bank_TMP <>0 and
	Sign_Landlord_Tenant =0 --тут нужна связка на договор Landlord

select * from Arenda.[s_LandlordTenantBank]

select * from Arenda.[j_AgreementsBank]


--select id_Landlord from Arenda.j_Agreements




select id_Bank_TMP,PaymentAccount_TMP from Arenda.s_Landlord_Tenant where Sign_Landlord_Tenant = 0 and id_Bank is not null and id_Bank <>0 

--select * from Arenda.s_Landlord_Tenant where Sign_Landlord_Tenant =1 --тут нужна связка на договор Tenant


select * from Arenda.s_Components