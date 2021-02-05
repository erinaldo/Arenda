USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[GetListDoc]    Script Date: 24.07.2020 10:49:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-27
-- Description:	Поиск договора 1С
-- =============================================
ALTER PROCEDURE [Arenda].[FindAgreement1CForAgreement]	
	@inAgreement varchar(max)
AS
BEGIN

if exists(select id from Arenda.j_Agreements where Agreement1C = @inAgreement)
	begin
		select 
			a.id,
			cast(0 as bit) as isAdd,
			o.Abbreviation+' '+ll.cName as nameLandLord,
			a.id_Landlord,
			ob.cName as nameObject
		from 
			Arenda.j_Agreements a
				left join Arenda.s_Landlord_Tenant ll on ll.id = a.id_Landlord
				left join Arenda.s_Type_of_Organization o on o.id = ll.id_Type_Of_Organization
				left join Arenda.s_ObjectLease ob on ob.id = a.id_ObjectLease
		where 
			lower(Agreement1C) = lower(@inAgreement)

		return
	end
ELSE
	BEGIN
		select 
			a.id,
			cast(1 as bit) as isAdd,
			o.Abbreviation+' '+ll.cName as nameLandLord,
			a.id_Landlord,
			ob.cName as nameObject
		from 
			Arenda.j_Agreements a
			left join Arenda.s_Landlord_Tenant ll on ll.id = a.id_Landlord
			left join Arenda.s_Type_of_Organization o on o.id = ll.id_Type_Of_Organization
			left join Arenda.s_ObjectLease ob on ob.id = a.id_ObjectLease
		where 
			lower(a.Agreement) = lower(@inAgreement) and a.Agreement1C is null
		return
	END

END

