USE [dbase2]
GO
/****** Object:  StoredProcedure [Arenda].[CheckPosts]    Script Date: 28.01.2021 12:13:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Butakov I.A.>
-- Create date: <2013-11-02>
-- Description:	<Проверка на существование должности>
-- Editor:		Molotkova_IS
-- Edit date:	2020-02-28
-- Description:	Проверка производится по имени и id, а не имени в разных падежах
-- =============================================
ALTER PROCEDURE [Arenda].[CheckPosts]
	@id INT,
	@cName VARCHAR(MAX)

AS
BEGIN
SET NOCOUNT ON
	SELECT
		id,
		cName,
		p.Dative_case,
		isActive
	FROM [Arenda].[s_Posts] p
	WHERE LOWER(RTRIM(LTRIM(cName))) = LOWER(RTRIM(LTRIM(@cName))) AND id != @id
END
