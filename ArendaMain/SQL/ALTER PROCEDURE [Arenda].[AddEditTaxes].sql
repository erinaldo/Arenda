USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[AddEditTaxes]    Script Date: 29.07.2020 10:44:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Butakov I.
-- Create date: 17-11-2014
-- Description:	Добавление / редактирование штрафа по договору аренды
-- Modify date: 27-02-2015
-- Description:	добавлено поле [id_АddPayment]
-- =============================================
ALTER PROCEDURE [Arenda].[AddEditTaxes]
	@id int,
	@id_Agreements int,
	@Date datetime,
	@Summa decimal(12,2),
	@Comment varchar(255),
	@id_Editor int,
	@id_АddPayment int,
	@datePlane date,
	@meters numeric(16,2) = null	
	AS
BEGIN

if (@id = 0)
	begin

		INSERT INTO [Arenda].[j_Fines]
				   ([id_Agreements]
				   ,[DateFines]
				   ,[Summa]
				   ,[Comment]
				   ,[DateEdit]
				   ,[id_Editor]
				   ,[id_АddPayment]
				   ,[PlanDate]
				   ,[MetersData]
				   ,[isConfirmed]
				   )
			 VALUES
				   (@id_Agreements
				   ,@Date
				   ,@Summa
				   ,@Comment
				   ,GETDATE()
				   ,@id_Editor
				   ,@id_АddPayment
				   ,@datePlane
				   ,@meters
				   ,0
				   )

		select SCOPE_IDENTITY()			   
	
	end
else
	begin
		UPDATE [Arenda].[j_Fines]
			SET 
				[DateFines] = @Date
				,[Summa] = @Summa
				,[Comment] = @Comment
				,[DateEdit] = GETDATE()
				,[id_Editor] = @id_Editor
				,[id_АddPayment] = @id_АddPayment
				,[PlanDate] = @datePlane
				,[MetersData] = @meters
			WHERE id = @id
 
		select @id
	end
END
