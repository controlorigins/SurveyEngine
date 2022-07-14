

CREATE PROC [dbo].[Application_SelectSummary] 
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN


SELECT        Application.ApplicationID, Application.ApplicationNM, Application.ApplicationCD, Application.ApplicationShortNM, Application.ApplicationTypeID, Application.ApplicationDS, Application.MenuOrder, 
                         Application.ModifiedID, Application.ModifiedDT, Application.ApplicationFolder, Application.DefaultPageID, Company.CompanyID, Company.CompanyNM, Company.CompanyCD, 
                         lu_ApplicationType.ApplicationTypeNM, lu_ApplicationType.ApplicationTypeDS, COUNT(DISTINCT ApplicationUserRole.ApplicationUserID) AS UserCount, COUNT(DISTINCT ApplicationSurvey.SurveyID) 
                         AS SurveyCount, COUNT(DISTINCT SiteAppMenu.Id) AS MenuCount, count(distinct SurveyResponse.SurveyResponseID) as SurveyResponseCount
FROM            Application LEFT OUTER JOIN
                         SurveyResponse ON Application.ApplicationID = SurveyResponse.ApplicationID LEFT OUTER JOIN
                         SiteAppMenu ON Application.ApplicationID = SiteAppMenu.SiteAppID LEFT OUTER JOIN
                         ApplicationUserRole ON Application.ApplicationID = ApplicationUserRole.ApplicationID LEFT OUTER JOIN
                         ApplicationSurvey ON Application.ApplicationID = ApplicationSurvey.ApplicationID LEFT OUTER JOIN
                         lu_ApplicationType ON Application.ApplicationTypeID = lu_ApplicationType.ApplicationTypeID LEFT OUTER JOIN
                         Company ON Application.CompanyID = Company.CompanyID
GROUP BY Application.ApplicationID, Application.ApplicationNM, Application.ApplicationCD, Application.ApplicationShortNM, Application.ApplicationTypeID, Application.ApplicationDS, Application.MenuOrder, 
                         Application.ModifiedID, Application.ModifiedDT, Application.ApplicationFolder, Application.DefaultPageID, Company.CompanyID, Company.CompanyNM, Company.CompanyCD, 
                         lu_ApplicationType.ApplicationTypeNM, lu_ApplicationType.ApplicationTypeDS
ORDER BY Application.ApplicationNM

	COMMIT





