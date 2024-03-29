USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[AddEditLD]    Script Date: 02.03.2021 13:31:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,> @id = 0 - добавить , @id <> 0 - изменить
-- Editor:		Molotkova_IS
-- Edit date:	07-12-2019
-- Description:	Добавлены поля id_TypeContract, id_ObjectLease, CadastralNumber
-- =============================================
ALTER PROCEDURE [Arenda].[AddEditLD]
	@id int,
	@ten int,
	@lord int,
	@agreement varchar(10),
	@Date_of_Conclusion datetime,
	@Start_Date datetime,
	@Stop_Date datetime , 
	@build int = null,--varchar(20),
	@Floor int = null,--varchar(50),
	@Section int,-- varchar(50),
	@Type_of_Premises int=null,-- varchar(50),
	@Total_Area numeric(12, 2),
	@Area_of_Trading_Hall numeric(12, 2),
	@Cost_of_Meter numeric(12, 4),
	@Phone numeric(12, 2),
	@Total_Sum numeric(12, 2),
	@Payment_Type bit,
	@Remark varchar(255),
	@Reklama numeric(12, 2),
	@ReklLength numeric(12,2),
	@ReklWidth numeric(12,2),
	@ReklArea numeric(12,2),
	@ReklNumber int,
	@failComment varchar(max),
	@id_TypeDog int,
	@KadNum varchar(max),
	@id_obg int,
	@RentalVacation int,
	@id_SavePayment int = null,
	@id_TypeActivities int,
	@id_user int,
	@Agreement1C varchar(50),
	@numSection varchar(50) = null
AS
BEGIN
	SET NOCOUNT ON;

declare @id_Agreement int = 0
declare @id_Rekl int = 0
declare @ReklComment varchar(max) = ''
	
if (@id = 0)
begin
--if(@id_TypeDog <> 3)
--begin
	insert into Arenda.j_Agreements(
	   [id_Tenant], 
	   [id_Landlord], 
	   [Agreement],
	   [Date_of_Conclusion], 
	   [Start_Date], 
	   [Stop_Date],
	   [id_Buildings], 
	   [id_Floor], 
	   [id_Section],
	   [id_Type_of_Premises], 
	   [Total_Area], 
	   [Area_of_Trading_Hall],
	   [Cost_of_Meter], 
	   [Phone], 
	   [Total_Sum],
	   [Payment_Type], 
	   [Remark], 
	   [Reklama], 
	   [failComment],
	   [id_TypeContract], 
	   [CadastralNumber], 
	   [id_ObjectLease],
	   [id_TypeActivities],
	   [id_Creator],
	   [DateCreate],
	   [id_Editor],
	   [DateEdit],
	   [Agreement1C])
	Values ( 
			@ten, --
			@lord, --
			@agreement,--
			@Date_of_Conclusion,--
			@Start_Date,--
			@Stop_Date,--
			@build,--
			@Floor,
			@Section, 
			@Type_of_Premises,
			@Total_Area,
			@Area_of_Trading_Hall,
			@Cost_of_Meter, 
			@Phone, 
			@Total_Sum,
			@Payment_Type, 
			@Remark, 
			@Reklama, 
			@failComment,
			@id_TypeDog, 
			@KadNum, 
			@id_obg,
			@id_TypeActivities,
			@id_user,
			GETDATE(),
			@id_user,
			GETDATE(),
			@Agreement1C)
			
	set @id_Agreement = SCOPE_IDENTITY()	
	
	if (@Reklama>0)
	begin
		INSERT INTO [Arenda].[j_OptionsAdvert]
			   ([id_Agreements]
			   ,[Length]
			   ,[Width]
			   ,[Area]
			   ,[Number])
		 VALUES
			   (@id_Agreement
			   ,@ReklLength
			   ,@ReklWidth
			   ,@ReklArea
			   ,@ReklNumber)
		
		set @id_Rekl = SCOPE_IDENTITY()	
		set @ReklComment = 'add'
	end
--end
--else
--begin
--	insert into Arenda.j_Agreements(
--	   [id_Tenant], [id_Landlord], [Agreement],
--	   [Date_of_Conclusion], [Start_Date], [Stop_Date],
--	   [Total_Area], [Area_of_Trading_Hall],
--	   [Cost_of_Meter], [Phone], [Total_Sum],
--	   [Payment_Type], [Remark], [Reklama], [failComment],
--	   [id_TypeContract], [CadastralNumber], [id_ObjectLease])
--	Values ( @ten, @lord, @agreement,
--			@Date_of_Conclusion, @Start_Date, @Stop_Date,
--			@Total_Area, @Area_of_Trading_Hall,
--			@Cost_of_Meter, @Phone, @Total_Sum,
--			@Payment_Type, @Remark, @Reklama, @failComment,
--			@id_TypeDog, @KadNum, @id_obg)
			
--	set @id_Agreement = SCOPE_IDENTITY()
--end


--Начало Добавление в [Arenda].[j_AdditionalAgreements]

IF exists (select id from [Arenda].[j_AdditionalAgreements] where id_Agreements = @id_Agreement)
	BEGIN
		UPDATE [Arenda].[j_AdditionalAgreements]  set id_SavePayment =@id_SavePayment, RentalVacation = @RentalVacation,id_Editor = @id_user,DateEdit =  GETDATE(),NumSection = @numSection where id_Agreements =  @id_Agreement 
	END
ELSE
	BEGIN
		INSERT INTO [Arenda].[j_AdditionalAgreements]   (id_Agreements,RentalVacation,id_SavePayment,id_Editor,DateEdit,NumSection)
		values (@id_Agreement,@RentalVacation,@id_SavePayment,@id_user,GETDATE(),@numSection)		
	END
--Окончание Добавление в [Arenda].[j_AdditionalAgreements]

	select @id_Agreement as id_Agreement, @id_Rekl as id_Rekl, @ReklComment as ReklComment
end 

if (@id != 0)
begin
	set @id_Agreement = @id


--if (@id_TypeDog <> 3)
--begin
	Update Arenda.j_Agreements
	Set 
		[id_Tenant]= @ten ,
		[id_Landlord]= @lord ,
		[Agreement]= @agreement,
		[Date_of_Conclusion]= @Date_of_Conclusion ,
		[Start_Date] = @Start_Date ,
        [Stop_Date] = @Stop_Date ,
		[id_Buildings] = @build,
		[id_Floor] = @Floor,
		[id_Section] = @Section,
		[id_Type_of_Premises] = @Type_of_Premises,
		[Total_Area] = @Total_Area,
		[Area_of_Trading_Hall] = @Area_of_Trading_Hall,
		[Cost_of_Meter] = @Cost_of_Meter,
		[Phone] = @Phone,
		[Total_Sum] = @Total_Sum,
		[Payment_Type] = @Payment_Type,
		[Remark] = @Remark,
		[Reklama] = @Reklama,
		[failComment] = @failComment,
		[CadastralNumber] = @KadNum,
		[id_ObjectLease] = @id_obg,
		[id_TypeContract] = @id_TypeDog,
		[id_TypeActivities] = @id_TypeActivities,
		[id_Editor] = @id_user,
	    [DateEdit] = GETDATE(),
		[Agreement1C] = @Agreement1C
	WHERE 
		id = @id_Agreement
--end
--else
--begin
--	Update Arenda.j_Agreements
--	Set 
--		[id_Tenant]= @ten ,
--		[id_Landlord]= @lord ,
--		[Agreement]= @agreement,
--		[Date_of_Conclusion]= @Date_of_Conclusion ,
--		[Start_Date] = @Start_Date ,
--        [Stop_Date] = @Stop_Date ,
--		[Total_Area] = @Total_Area,
--		[Area_of_Trading_Hall] = @Area_of_Trading_Hall,
--		[Cost_of_Meter] = @Cost_of_Meter,
--		[Phone] = @Phone,
--		[Total_Sum] = @Total_Sum,
--		[Payment_Type] = @Payment_Type,
--		[Remark] = @Remark,
--		[Reklama] = @Reklama,
--		[failComment] = @failComment,
--		[id_TypeContract] = @id_TypeDog,
--		[CadastralNumber] = @KadNum,
--		[id_ObjectLease] = @id_obg
--	where id = @id_Agreement
--end
	if (@Reklama=0)
	begin		
		
		set @id_Rekl = (select top 1 id from [Arenda].[j_OptionsAdvert] where id_Agreements = @id_Agreement order by id desc)
		
		delete from [Arenda].[j_OptionsAdvert] where id = @id_Rekl
		
		set @ReklComment = 'del'
	end
	else
	begin
		if exists (select * from [Arenda].[j_OptionsAdvert] where id_Agreements = @id_Agreement)
		begin
			
			set @id_Rekl = (select top 1 id from [Arenda].[j_OptionsAdvert] where id_Agreements = @id_Agreement order by id desc)
			
			UPDATE [Arenda].[j_OptionsAdvert]
			   SET 
				  [Length] = @ReklLength
				  ,[Width] = @ReklWidth
				  ,[Area] = @ReklArea
				  ,[Number] = @ReklNumber
			 WHERE id = @id_Rekl
			 
			 set @ReklComment = 'edit'
		end
		else
		begin
			INSERT INTO [Arenda].[j_OptionsAdvert]
				   ([id_Agreements]
				   ,[Length]
				   ,[Width]
				   ,[Area]
				   ,[Number])
			 VALUES
				   (@id_Agreement
				   ,@ReklLength
				   ,@ReklWidth
				   ,@ReklArea
				   ,@ReklNumber)
			
			set @id_Rekl = SCOPE_IDENTITY()
			set @ReklComment = 'add'
		end
	end


--Начало Добавление в [Arenda].[j_AdditionalAgreements]

IF exists (select id from [Arenda].[j_AdditionalAgreements] where id_Agreements = @id_Agreement)
	BEGIN
		UPDATE [Arenda].[j_AdditionalAgreements]  set id_SavePayment =@id_SavePayment, RentalVacation = @RentalVacation,id_Editor = @id_user,DateEdit =  GETDATE(),NumSection = @numSection where id_Agreements =  @id_Agreement 
	END
ELSE
	BEGIN
		INSERT INTO [Arenda].[j_AdditionalAgreements]   (id_Agreements,RentalVacation,id_SavePayment,id_Editor,DateEdit,NumSection)
		values (@id_Agreement,@RentalVacation,@id_SavePayment,@id_user,GETDATE(),@numSection)		
	END
--Окончание Добавление в [Arenda].[j_AdditionalAgreements]

	select @id_Agreement as id_Agreement, @id_Rekl as id_Rekl, @ReklComment as ReklComment
end
    
END
