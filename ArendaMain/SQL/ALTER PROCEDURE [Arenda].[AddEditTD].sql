USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[AddEditTD]    Script Date: 31.07.2020 16:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kirill Cvetkov
-- Create date: Unknown
-- Description:	Добавление доп документа
-- Modified by: Butakov I.A.
-- Midify date: 20-11-2013
-- Description:	Добавлено поле Total_Area
-- Midify date: 21-04-2015
-- Description:	Функционал изменения признака действия перенесен в процедуру [Arenda].[DelTD]
-- =============================================
ALTER PROCEDURE [Arenda].[AddEditTD]
	@id int,
	@id_agr int,
	@datedoc datetime,
	@id_type_doc int,
	@number int = null,	
	@daterenewal datetime = null,
	@id_user int,		
	@Total_Area decimal(12, 2) = null,
	@DepartureDate datetime = null,
	@comment varchar(1024) = null,
	@id_PetitionLeave int = null
AS
BEGIN
	SET NOCOUNT ON;

		INSERT INTO [Arenda].[j_AdditionalDocuments] 
			   ([id_Agreements], 
			   [DateDocument], 
			   [id_TypeDoc],
			   [Number],
			   [isActive],
			   [DateRenewal],
			   [id_Creator], 
			   [DateCreate], 
			   [Total_Area],
			   [Date_of_Departure],
			   [Comment],
			   id_PetitionLeave
			   )
		 VALUES
			   (
			   @id_agr , 
			   @datedoc, 
			   @id_type_doc, 
			   @number, 
			   1, 
			   @daterenewal, 
			   @id_user,  
			   GETDATE(), 
			   @Total_Area,
			   @DepartureDate,
			   @comment,
			   @id_PetitionLeave
			   )	

	-- При добавлении доп документа 
	-- с типом Соглашение о продлении договора (id_type_doc = 1) 
	-- устанавливаем fullpayed по договору в 0
	
	if (@id_type_doc = 1)
	begin		
		update Arenda.j_Agreements 
		set fullPayed = 0
		where id = @id_agr
	end
END
