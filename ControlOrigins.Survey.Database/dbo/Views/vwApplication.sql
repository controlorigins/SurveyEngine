CREATE VIEW [dbo].[vwApplication]
AS
SELECT        dbo.Application.ApplicationID, dbo.Application.ApplicationNM, dbo.Application.ApplicationCD, dbo.Application.ApplicationShortNM, dbo.Application.ApplicationDS, dbo.Application.MenuOrder, 
                         dbo.lu_ApplicationType.ApplicationTypeID, dbo.lu_ApplicationType.ApplicationTypeNM, dbo.lu_ApplicationType.ApplicationTypeDS, COUNT(DISTINCT dbo.SurveyResponse.SurveyID) AS SurveyCount, 
                         COUNT(DISTINCT dbo.SurveyResponse.SurveyResponseID) AS SurveyResponseCount, COUNT(DISTINCT dbo.ApplicationUserRole.ApplicationUserRoleID) AS UserCount
FROM            dbo.Application LEFT OUTER JOIN
                         dbo.lu_ApplicationType ON dbo.Application.ApplicationTypeID = dbo.lu_ApplicationType.ApplicationTypeID LEFT OUTER JOIN
                         dbo.SurveyResponse ON dbo.Application.ApplicationID = dbo.SurveyResponse.ApplicationID LEFT OUTER JOIN
                         dbo.ApplicationUserRole ON dbo.Application.ApplicationID = dbo.ApplicationUserRole.ApplicationID
GROUP BY dbo.Application.ApplicationID, dbo.Application.ApplicationNM, dbo.Application.ApplicationCD, dbo.Application.ApplicationShortNM, dbo.Application.ApplicationDS, dbo.Application.MenuOrder, 
                         dbo.lu_ApplicationType.ApplicationTypeID, dbo.lu_ApplicationType.ApplicationTypeNM, dbo.lu_ApplicationType.ApplicationTypeDS
