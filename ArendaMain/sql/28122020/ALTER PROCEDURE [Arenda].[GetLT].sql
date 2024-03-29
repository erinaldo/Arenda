USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[GetLT]    Script Date: 12.01.2021 15:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [Arenda].[GetLT]
	@tid int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
	   lt.[id]
      ,lt.[id_Type_Of_Organization]
      ,lt.[cName]
      ,lt.[Contact_Firstname]
      ,lt.[Contact_Middlename]
      ,lt.[Contact_Lastname]
      ,lt.[Sex]
      ,lt.[Work_phone]
      ,lt.[Home_phone]
      ,lt.[Mobile_phone]
      ,lt.[Remark]
      --,lt.[id_Bank]
      --,lt.[PaymentAccount]
      ,lt.[OKPO]
      ,lt.[KPP]
      ,lt.[INN]
      ,lt.[id_Basement]
      ,lt.[Who_is_Registered]
      ,lt.[DateRegistration]
      ,lt.[RegistrationNumber]
      ,lt.[Number_of_Certificate]
      ,lt.[Series_od_Certificate]
      ,lt.[Who_put_on_Account]
      ,lt.[Number_Accounting]
      ,lt.[Series_of_Accounting]
      ,lt.[Vat_Nds]
      ,lt.[Sign_Landlord_Tenant]
      ,lt.[isActive]
      ,lt.[Number_basement]
      ,lt.[Date_basement]
      ,lt.[id_Posts]
      ,lt.[Contact_Lastname_Par]
      ,lt.[Address_trade_premises]
      ,lt.[outReport]
      ,lt.[id_ObjectLease]
      ,lt.[Path]
      ,lt.[email]
      ,lt.[FactAdress]
      ,lt.[Address], 
		--(Select Abbreviation 
		--from Arenda.s_Type_of_Organization 
		--where lt.id_Type_Of_Organization = id) as type_abb,
		(Select cName as Bas 
		from Arenda.s_Basement 
		where lt.id_Basement = id) as base_name 

		,t.cName as nameTypeOrg
		,t.Abbreviation as  type_abb
		,p.cName as namePost
		,obl.cName as nameObjectLease,
		lt.EmailPassword
	from 
		Arenda.s_Landlord_Tenant lt
			left join Arenda.s_Type_of_Organization t on t.id = lt.id_Type_Of_Organization
			left join Arenda.s_Posts p on p.id = lt.id_Posts
			left join Arenda.s_ObjectLease obl on obl.id = lt.id_ObjectLease



	where 
		lt.id = @tid
END
