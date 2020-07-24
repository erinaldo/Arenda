USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[FillCbTen]    Script Date: 24.07.2020 15:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Editor:		Molotkova_IS
-- Edit date:	07-12-2019, 12-12-2019
-- Description:	К имени Арендодателя добавлено имя объекта, добавлены id родителя и ребенка
-- =============================================
ALTER PROCEDURE [Arenda].[FillCbTen]
	-- Add the parameters for the stored procedure here
@prz int	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
select
	lt.id,
	case when @prz <> 1
	then too.Abbreviation + ' ' + lt.cName + '/' + isnull(ol.cName, '')
	else too.Abbreviation + ' ' + lt.cName end as aren,
	lt.Contact_Lastname,
	lt.Contact_Firstname,
	lt.Contact_Middlename,
	lt.INN,
	lt.Address_trade_premises,
	lt.id_ObjectLease,
	pt.id_TenantParent,
	ct.id_TenantChild,
	ol.CadastralNumber
from Arenda.s_Landlord_Tenant lt 
left join Arenda.s_Type_of_Organization too on lt.id_Type_Of_Organization = too.id
left join Arenda.s_ObjectLease ol on ol.id = lt.id_ObjectLease
left join Arenda.j_ParentChildTenant pt on pt.id_TenantChild = lt.id
left join Arenda.j_ParentChildTenant ct on ct.id_TenantParent = lt.id
where lt.Sign_Landlord_Tenant = @prz and lt.isActive = 1
Order by  lt.isActive DESC
END
