USE [dbase2]
GO

/****** Object:  StoredProcedure [Arenda].[LibGetTenantReport]    Script Date: 21.07.2020 12:00:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		АНС
-- Create date: 13.07.2020
-- Description:	Получает данные для отчета об арендаторах
-- =============================================
CREATE PROCEDURE [Arenda].[LibGetTenantReport] 
	@IdObject int,
	@IdLandlord int,
	@IdActivities int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	

	WITH _Agreements AS
	(
		SELECT	JA.id,
				JA.id_TypeContract,
				JA.id_ObjectLease,
				JA.id_TypeActivities,
				JA.id_Landlord,
				JA.id_Tenant,
				JA.id_Buildings,
				JA.id_Floor,
				JA.id_Section,
				JA.isConfirmed,
				CASE 
					WHEN ad.DateDocument is not null and ad.id_TypeDoc = 6 THEN ad.DateDocument
					WHEN ad.DateDocument is null and AD.Date_of_Departure is not null and AD1003.comfId is null THEN AD.Date_of_Departure
					WHEN (ad.DateDocument is null and AD1003.comfId is not null) THEN JA.Stop_Date
					ELSE JA.Stop_Date
				END AS Stop_Date,
				ad.DateDocument as dateDocument1,
				ad.id_TypeDoc as typeDoc1,
				AD.Date_of_Departure as dateDerapture2,
				AD1003.comfId
		FROM Arenda.j_Agreements JA
		LEFT JOIN Arenda.j_AdditionalDocuments ad on JA.id = ad.id_Agreements and ad.id_TypeDoc = 6 
		LEFT JOIN (	SELECT	ad.id_Agreements,
							ad.DateDocument, 
							ad.Date_of_Departure,
							ad.id_TypeDoc, ad.id,
							ad.id_PetitionLeave,
							adoc.id as comfId
					FROM Arenda.j_AdditionalDocuments  ad
					LEFT JOIN Arenda.j_AddDocConfirmed adoc on ad.id = adoc.id_AdditionalDocuments and adoc.isConfirmed = 1
					WHERE ad.id_TypeDoc = 8) AD ON	JA.id = AD.id_Agreements
													AND AD.DateDocument = (	SELECT max(DateDocument)
																			FROM Arenda.j_AdditionalDocuments ad1
																			WHERE ad1.id_Agreements = JA.id
																			AND ad1.id_TypeDoc = 8)
		LEFT JOIN (	SELECT	ad.id_Agreements,
							ad.DateDocument, 
							ad.Date_of_Departure,
							ad.id_TypeDoc, ad.id,
							ad.id_PetitionLeave,
							adoc.id AS comfId
					FROM Arenda.j_AdditionalDocuments ad 
					LEFT JOIN Arenda.j_AddDocConfirmed adoc ON ad.id = adoc.id_AdditionalDocuments AND adoc.isConfirmed = 1
					WHERE id_TypeDoc = 9) AD1003 ON JA.id = AD1003.id_Agreements AND AD1003.id_PetitionLeave = AD.id 
		WHERE JA.isConfirmed = 1
		AND CONVERT(date,GETDATE()) >= JA.Start_Date AND CONVERT(date,GETDATE()) <= JA.Stop_Date
	)
	SELECT	JA.id,
			SOL.id AS IdObject,
			SOL.cName AS Object,
			SLTL.id AS IdLandlord,
			STOOL.Abbreviation + ' ' + SLTL.cName AS Landlord,
			STA.id AS IdActivities,
			STA.cName AS Activities,
			SLTT.id AS IdTenant,
			STOOT.Abbreviation + ' ' + SLTT.cName AS Tenant,
			STC.TypeContract,
			SB.cName AS Building,
			SF.cName AS "Floor",
			CASE JA.id_TypeContract
				WHEN 1 THEN SS.cName--(SELECT SS.cName FROM Arenda.s_Sections SS WHERE SS.id = JA.id_Section)
				WHEN 2 THEN SRP.NumberPlace--(SELECT SRP.NumberPlace FROM Arenda.s_ReclamaPlace SRP WHERE SRP.id = JA.id_Section)
				WHEN 3 THEN SLP.NumberPlot--(SELECT SLP.NumberPlot FROM Arenda.s_LandPlot SLP WHERE SLP.id = JA.id_Section)
			END AS Section,
			SLTT.Work_phone,
			SLTT.email
	FROM _Agreements JA
	LEFT JOIN Arenda.s_ObjectLease SOL ON SOL.id = JA.id_ObjectLease 
	LEFT JOIN Arenda.s_TypeActivities STA ON STA.id = JA.id_TypeActivities
	LEFT JOIN Arenda.s_Landlord_Tenant SLTL ON SLTL.id = JA.id_Landlord
	LEFT JOIN Arenda.s_Type_of_Organization STOOL ON STOOL.id = SLTL.id_Type_Of_Organization
	LEFT JOIN Arenda.s_Landlord_Tenant SLTT ON SLTT.id = JA.id_Tenant
	LEFT JOIN Arenda.s_Type_of_Organization STOOT ON STOOT.id = SLTT.id_Type_Of_Organization
	LEFT JOIN Arenda.s_Building SB ON SB.id = JA.id_Buildings
	LEFT JOIN Arenda.s_Floors SF ON SF.id = JA.id_Floor
	LEFT JOIN Arenda.s_Sections SS ON SS.id = JA.id_Section
	LEFT JOIN Arenda.s_ReclamaPlace SRP ON SRP.id = JA.id_Section
	LEFT JOIN Arenda.s_LandPlot SLP ON SLP.id = JA.id_Section
	LEFT JOIN Arenda.s_TypeContract STC ON STC.id = JA.id_TypeContract
	WHERE	JA.isConfirmed = 1
			AND (@IdObject = 0 OR SOL.id = @IdObject)
			AND (@IdLandlord = 0 OR SLTL.id = @IdLandlord)
			AND (@IdActivities = 0 OR STA.id = @IdActivities)
			AND JA.Stop_Date >= GETDATE()
			AND STA.id IS NOT NULL
	ORDER BY SOL.id, SLTL.id, STA.id
END
GO

