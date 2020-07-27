USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[AddEditPayment]    Script Date: 27.07.2020 14:58:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Butakov I.
-- Create date: 14-11-2014
-- Description:	Добавление / редактирование оплаты по договору аренды
-- Modified:    18-03-2015
-- ModifiedBy:  Butakov I.
-- Description:	Удалено поле isPayment
-- =============================================
ALTER PROCEDURE [Arenda].[AddEditPayment]
	@id int,
	@id_Agreements int,
	@Date datetime,
	@Summa decimal(12,2),	
	@id_PayType int,
	@PlaneDate date,
	@isRealMoney bit,
	@isSendMoney bit,
	@id_Fines int = null,
	@id_Editor int
	AS
BEGIN

declare @days int = 0				-- просрочка в днях
declare @peni numeric (12,2) = 0	-- величина начисленного пени
declare @payed numeric (12,2) = 0	-- величина, принятая к оплате
declare @debt numeric (12,2) = 0	-- оставшийся долг по оплате

if (@id = 0)
begin

	INSERT INTO [Arenda].[j_PaymentContract]
			   ([id_Agreements]
			   ,[Date]
			   ,[Summa]		
			   ,[id_PayType]
			   ,[PlaneDate]			   
			   ,[isToTenant]
			   ,[isCash]
			   ,[id_Fines]
			   ,[DateEdit]
			   ,[id_Editor])
		 VALUES
			   (@id_Agreements
			   ,@Date
			   ,@Summa		
			   ,@id_PayType
			   ,@PlaneDate
			   ,@isRealMoney
			   ,@isSendMoney
			   ,@id_Fines
			   ,GETDATE()
			   ,@id_Editor)
			   
	set @id = SCOPE_IDENTITY()
			   
end

select 
	@id as id, 
	@days as [days], 	
	@peni as peni,
	@payed as payed,
	@debt as debt

END
