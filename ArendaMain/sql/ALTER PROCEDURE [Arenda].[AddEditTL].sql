USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[AddEditTL]    Script Date: 16.12.2020 14:32:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Editor:		Molotkova_IS
-- Edit date:	02-12-2019, 12-12-2019
-- Description:	Добавлено поле [id_ObjectLease], [Path], [email]
-- update:		kav 2020-09-10 поле id_ObjectLease сделано null без ключа, null для арендатора (isTenant = 1)
-- =============================================
ALTER PROCEDURE [Arenda].[AddEditTL]
	@id int,
	@type varchar(30),
	@cName varchar(100), 
	@name varchar(30),
	@otc varchar(30), 
	@fam varchar(30), 
	@fam_par varchar(50),
	@sex bit, 
	@wphone varchar(20),
	@hphone varchar(20),
	@mphone varchar(20),
	@adress varchar(1024), 
	@id_bank int, 
	@pa varchar(20),
	@okpo varchar(8),
	@kpp varchar(9), 
	@inn varchar(12),
	@id_basment int = null, 
	@WiS varchar(50), 
	@dateReg date = null,
	@regNum varchar(10),
	@numCert varchar(10), 
	@serCer varchar (10),
	@WPON varchar(50),
	@numAcc varchar(10),
	@serAcc varchar(10), 
	@nds int, 
	@slt int, 
	@remark varchar(255), 
	@mode int,
	@numofbas varchar(50) = null,
	@datebas datetime = null,
	@id_Posts int,
	@adress_trade varchar(255),
	@outReport int,
	@id_obj int,
	@path varchar(max),
	@email varchar(max),
	@factAdress varchar(max) = '',
	@isTenant bit = 0 -- признак арендатора
AS
BEGIN
	SET NOCOUNT ON;

	IF @id_obj = 0
		select @id_obj = id from Arenda.s_ObjectLease
		
	if @mode = 1 
	Begin 
	
	INSERT INTO [Arenda].[s_Landlord_Tenant]
       ([id_Type_Of_Organization], 
       [cName], 
       [Contact_Firstname],
       [Contact_Middlename],
       [Contact_Lastname],
       [Contact_Lastname_Par],
       [Sex], 
       [Work_phone],
       [Home_phone],
       [Mobile_phone],
       [Address],
       --[id_Bank_TMP],
       --[PaymentAccount_TMP],
       [OKPO],
       [KPP],
       [INN],
       [id_Basement], 
       [Who_is_Registered], 
       [DateRegistration], 
       [RegistrationNumber],
       [Number_of_Certificate],
       [Series_od_Certificate],
       [Who_put_on_Account],
       [Number_Accounting],
       [Series_of_Accounting], 
       [Vat_Nds],
       [Sign_Landlord_Tenant], 
       [Remark], 
       [isActive], 
       [Number_basement], 
       [Date_basement], 
       [id_Posts], 
       [Address_trade_premises],
       [outReport],
	   [id_ObjectLease],
	   [Path],
	   [email],FactAdress)
     VALUES
           (
				(Select 
					top 1 s.id 
				from Arenda.s_Type_of_Organization s  
				where s.Abbreviation = @type),
				@cName, 
				@name,
				@otc, 
				@fam , 
				@fam_par,
				@sex, 
				@wphone,
				@hphone,
				@mphone,
				@adress, 
				--@id_bank,  
				--@pa, 
				@okpo,
				@kpp, 
				@inn,
				@id_basment, 
				@WiS, 
				@dateReg ,
				@regNum,
				@numCert, 
				@serCer, 
				@WPON,
				@numAcc,
				@serAcc, 
				@nds, 
				@slt, 
				@remark, 
				1,
				@numofbas,
				@datebas,
				@id_Posts,
				@adress_trade,
				@outReport,
				case when @isTenant = 0 then @id_obj else null end,
				@path,
				@email,
				@factAdress
			)	
			
			set @id = SCOPE_IDENTITY()
	End
	
	if @mode = 0 
	begin
		Update Arenda.[s_Landlord_Tenant]
		Set	
			id_Type_Of_Organization =(Select top 1 s.id from Arenda.s_Type_of_Organization s  where s.Abbreviation = @type ),
			cName=	@cName, 
			Contact_Firstname=	@name,
			Contact_Lastname=	@fam, 
			Contact_Lastname_Par=	@fam_par,
			Contact_Middlename=	@otc , 
			Sex=	@sex, 
			Work_phone=	@wphone,
			Home_phone=	@hphone,
			Mobile_phone=	@mphone,
			Address=	@adress, 
			--id_Bank =	@id_bank,  
			--PaymentAccount =	@pa, 
			OKPO =	@okpo,
			KPP=	@kpp, 
			INN=	@inn,
			id_Basement= @id_basment, 
			Who_is_Registered =	@WiS, 
			DateRegistration=	@dateReg ,
			RegistrationNumber=	@regNum,
			Number_of_Certificate=	@numCert, 
			Series_od_Certificate=		@serCer, 
			Who_put_on_Account=	@WPON,
			Number_Accounting=	@numAcc,
			Series_of_Accounting =	@serAcc, 
			Vat_Nds =	@nds, 
			Sign_Landlord_Tenant=	@slt, 
			Remark=	@remark, 		
			Number_basement = @numofbas,
			Date_basement = @datebas,
			id_Posts = @id_Posts,
			Address_trade_premises = @adress_trade,
			outReport = @outReport,
			id_ObjectLease = case when @isTenant = 0 then @id_obj else null end,
			[Path] = @path,
			email = @email,
			FactAdress = @factAdress
		Where		
			id = @id
	end
	
	select @id as id
	
END

