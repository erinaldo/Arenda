USE [dbase2]
GO

/****** Object:  StoredProcedure [Arenda].[kav_getPrintData]    Script Date: 25.08.2020 12:01:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Kamaev A.V.
-- Create date: 2020-08-24
-- Description:	Процедура для печати документов
-- exec [Arenda].[kav_getPrintData] 1002,2048
-- exec [Arenda].[kav_getPrintData] 1002,1002
-- exec [Arenda].[kav_getPrintData] 1002,1002
-- exec Arenda.kav_getPrintData 1042,0
-- exec Arenda.kav_getPrintData 1051,0
-- exec Arenda.kav_getPrintData 3066,0
-- =============================================
CREATE PROCEDURE [Arenda].[kav_getPrintData]
	@id int,
	@id_act int = 0
AS
BEGIN
	SET NOCOUNT ON;

    select
			rtrim(ltrim(a.Agreement)) as num_dog,
			Arenda.ConvertDateToString(a.Date_of_Conclusion) as date_dog,
			Arenda.ConvertDateToString(ad.DateDocument) as date_sogl,
			rtrim(ltrim(tl.cName + ' ' + l.cName)) as arendodatel,
			rtrim(ltrim(tt.cName + ' ' + t.cName)) as arendator,
			rtrim(ltrim(tl.Abbreviation + ' ' + l.cName)) as arendodatel_short,
			rtrim(ltrim(tt.Abbreviation + ' ' + t.cName)) as arendator_short,
			pl.cName + ' ' + l.Contact_Lastname_Par + ' ' + substring(l.Contact_Firstname, 1, 1) + '. ' + substring(l.Contact_Middlename, 1, 1) + '.' as predstavitel,
			t.Contact_Lastname_Par + ' ' + substring(t.Contact_Firstname, 1, 1) + '. ' + substring(t.Contact_Middlename, 1, 1) + '.' as arendator_predst,
			
			--убираем ", действующего..." из темплейта
			case when b.cName is not null then 
				', действующ' + case when l.Sex = 1 then 'его' else 'ей' end + ' на основании ' + 
							
							b.cName + isnull(' ' + l.Number_basement, '') + case when b.needDate = 1 then isnull(' от ' + Arenda.ConvertDateToString(l.Date_basement), '') else '' end 
				else '' end	
						as osnovanie_arendodatel,
			case when bt.cName is not null then 
				', действующ' + case when other.Sex = 1 then 'его' else 'ей' end + ' на основании ' +
						bt.cName + isnull(' ' + other.OGRN, '') + case when bt.needDate = 1 then isnull(' от ' + Arenda.ConvertDateToString(t.Date_basement), '') else '' end 						
				else '' end		
						as osnovanie_arendator,
			--case when other.Sex = 1 then 'его' else 'ей' end as okonchanie_1,
			case when other.Sex = 1 then 'ый' else 'ая' end as okonchanie_2,
			--case when l.Sex = 1 then 'его' else 'ей' end as okonchanie_a,
			s.cName as section,
			fl.cName as [floor],
			a.Total_Area as area,
			
			rtrim(ltrim(tl.Abbreviation + ' ' + l.cName)) as name_1,
			rtrim(ltrim(tt.Abbreviation + ' ' + t.cName)) as name_2,
			rtrim(ltrim(l.Address)) as address_1,
			rtrim(ltrim(t.Address)) as address_2,
			l.PaymentAccount as rs_1,
			t.PaymentAccount as rs_2,
			l.INN as inn_1,
			t.INN as inn_2,
			isnull(rtrim(ltrim(bnl.cName)), '') as bank_1,
			isnull(rtrim(ltrim(bnt.cName)), '') as bank_2,
			isnull(bnl.BIC, '') as bik_1,
			isnull(bnt.BIC, '') as bik_2,
			isnull(bnl.CorrespondentAccount, '') as ks_1,
			isnull(bnt.CorrespondentAccount, '') as ks_2,
			l.KPP as kpp_1,
			t.KPP as kpp_2,
			other.OGRN as ogrn_1,
			other_2.OGRN as ogrn_2,
			l.Contact_Lastname + ' ' + substring(l.Contact_Firstname, 1, 1) + '. ' + substring(l.Contact_Middlename, 1, 1) + '.' as predst_1,
			t.Contact_Lastname + ' ' + substring(t.Contact_Firstname, 1, 1) + '. ' + substring(t.Contact_Middlename, 1, 1) + '.' as predst_2,
			l.Mobile_phone as tel_mail_1,
			l.email as mail_1,
			work_phone_arendodatel = isnull([l].Work_phone, ''),
			work_phone_arendator = isnull([t].Work_phone, ''),
			home_phone_arendodatel = isnull([l].Home_phone, ''),
			home_phone_arendator = isnull([t].Home_phone, ''),
			mobile_phone_arendodatel = isnull([l].Mobile_phone, ''),
			mobile_phone_arendator = isnull([t].Mobile_phone, ''),
			landlord_address = t.Address_trade_premises,
			building = build.cName,
			date_start =  Arenda.ConvertDateToString(a.[Start_Date]),
			date_end = Arenda.ConvertDateToString(a.[Stop_Date]),
			convert(varchar(50),convert(int,a.Total_Sum)) + ' руб. ('  + convert(varchar(200),Arenda.ConvertNumericToString(a.Total_Sum)) + ' 00 коп.)' as sum_arenda,
			isnull(tp.cName, '') as dest,
			isnull(a.CadastralNumber,'') as kadnum,
			rekl.Length as lengthRekl,
			rekl.Width as widthRekl,
			convert(numeric(16,4),round((rekl.Length*rekl.Width)/1000000.0,4)) as areaRekl,
			rekl.NumberPlace as nameRekl
    from
			Arenda.j_Agreements a left join Arenda.j_AdditionalDocuments ad on a.id = ad.id_Agreements -- 
								   left join Arenda.s_Landlord_Tenant l on a.id_Landlord = l.id -- 
			                       left join Arenda.s_Type_of_Organization tl on l.id_Type_Of_Organization = tl.id -- 
			                       left join Arenda.s_Posts pl on l.id_Posts = pl.id --
			                       left join Arenda.s_Landlord_Tenant t on a.id_Tenant = t.id -- 
			                       left join Arenda.s_Type_of_Organization tt on t.id_Type_Of_Organization = tt.id -- 
			                       left join Arenda.s_Basement b on l.id_Basement = b.id
			                       left join Arenda.s_Basement bt on t.id_Basement = bt.id
			                       left join Arenda.other_Landlord_Tenant other on other.id_Landlord_Tenant = t.id --
								   left join Arenda.other_Landlord_Tenant other_2 on other_2.id_Landlord_Tenant = a.id_Tenant
			                       left join Arenda.s_Banks bnl on l.id_Bank = bnl.id
			                       left join Arenda.s_Banks bnt on t.id_Bank = bnt.id
			                       left join Arenda.s_Sections s on a.id_Section = s.id -- 
			                       left join Arenda.s_Floors fl on s.id_Floor = fl.id--
								   left join Arenda.s_Building build on build.id = a.id_Buildings
								   left join Arenda.s_Type_of_Premises tp on a.id_Type_of_Premises = tp.id
								   left join Arenda.s_ReclamaPlace rekl on rekl.id = a.id_Section and a.id_TypeContract = 2
    where
			a.id = @id and
			(@id_act = 0 or (ad.id = @id_act and
			ad.isActive = 1))
END
GO

