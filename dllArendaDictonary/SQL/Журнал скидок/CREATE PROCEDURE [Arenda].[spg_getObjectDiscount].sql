SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-06-23
-- Description:	��������� ������ �������� ������
-- =============================================
CREATE PROCEDURE [Arenda].[spg_getObjectDiscount]		 
AS
BEGIN
	SET NOCOUNT ON;

select 1 as id,'������' as cName
union
select 2 as id,'��������� �����' as cName
union
select 3 as id,'��������� ������� ' as cName


END
