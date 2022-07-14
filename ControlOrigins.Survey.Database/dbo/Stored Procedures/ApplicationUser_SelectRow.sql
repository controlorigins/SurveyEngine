

-- ==========================================================================================
-- Entity Name:	ApplicationUser_SelectRow
-- Author:	Mark Hazleton
-- Modified date:	9/30/2015 11:43:47 AM
-- Description:	This stored procedure is intended for selecting a specific row from ApplicationUser table
-- ==========================================================================================
CREATE Procedure [dbo].[ApplicationUser_SelectRow]
	@ApplicationUserID int
As
Begin


SELECT        ApplicationUser.ApplicationUserID, ApplicationUser.FirstNM, ApplicationUser.LastNM, ApplicationUser.eMailAddress, ApplicationUser.CommentDS, ApplicationUser.AccountNM, 
                         ApplicationUser.SupervisorAccountNM, ApplicationUser.LastLoginDT, ApplicationUser.LastLoginLocation, 
						 CAST(MAX(CAST(ApplicationUser.EmailVerified as INT)) AS BIT)  as EmailVerified,
						 ApplicationUser.ModifiedID, ApplicationUser.ModifiedDT, 
                         COUNT(DISTINCT SurveyResponse.SurveyResponseID) AS SRCount, COUNT(DISTINCT ApplicationUserRole.ApplicationUserRoleID) AS URCount, ApplicationUser.DisplayName, ApplicationUser.RoleID, 
                         ApplicationUser.UserKey, ApplicationUser.UserLogin, ApplicationUser.VerifyCode, SiteRole.RoleName, Company.CompanyID, Company.CompanyNM, Company.CompanyCD
FROM            ApplicationUser LEFT OUTER JOIN
                         Company ON ApplicationUser.CompanyID = Company.CompanyID LEFT OUTER JOIN
                         SiteRole ON ApplicationUser.RoleID = SiteRole.Id LEFT OUTER JOIN
                         SurveyResponse ON ApplicationUser.ApplicationUserID = SurveyResponse.AssignedUserID LEFT OUTER JOIN
                         ApplicationUserRole ON ApplicationUser.ApplicationUserID = ApplicationUserRole.ApplicationUserID
Where ApplicationUser.ApplicationUserID = @ApplicationUserID
GROUP BY ApplicationUser.ApplicationUserID, ApplicationUser.FirstNM, ApplicationUser.LastNM, ApplicationUser.eMailAddress, ApplicationUser.CommentDS, ApplicationUser.AccountNM, 
                         ApplicationUser.SupervisorAccountNM, ApplicationUser.LastLoginDT, ApplicationUser.LastLoginLocation, ApplicationUser.ModifiedID, ApplicationUser.ModifiedDT, ApplicationUser.DisplayName, 
                         ApplicationUser.RoleID, ApplicationUser.UserKey, ApplicationUser.UserLogin, ApplicationUser.VerifyCode, SiteRole.RoleName, Company.CompanyID, Company.CompanyNM, Company.CompanyCD	





End

