USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[DelDTL]    Script Date: 10.08.2020 9:12:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [Arenda].[DelDTL] 
	-- Add the parameters for the stored procedure here
	@id int,
	@prz varchar(20)
AS
BEGIN
SET NOCOUNT ON

	if @prz = 'land_tenant'
	begin
	delete from Arenda.other_Landlord_Tenant
	where id_Landlord_Tenant = @id
	
	delete from Arenda.s_Landlord_Tenant
	Where		
		[id] = @id
	end
	
	if @prz = 'listdoc'
	begin
	
	delete from Arenda.j_AdditionalDocuments
	where [id_Agreements] = @id

	delete from Arenda.j_AdditionalAgreements
	where [id_Agreements] = @id

	delete from Arenda.j_tDiscount
	where [id_Agreements] = @id

	delete from Arenda.j_Agreements
	Where		
		[id] = @id
		
	
		
	end 
	
		
END
