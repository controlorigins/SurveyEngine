

-- ==========================================================================================
-- Entity Name:	ApplicationUser_Insert
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:43:47 AM
-- Description:	This stored procedure is intended for inserting values to ApplicationUser table
-- ==========================================================================================
CREATE Procedure [dbo].[ApplicationUser_Insert]
	@FirstNM nvarchar(100),
	@LastNM nvarchar(100),
	@eMailAddress nvarchar(100),
	@CommentDS nvarchar(MAX),
	@AccountNM nvarchar(50),
	@SupervisorAccountNM nvarchar(50),
	@CompanyID int,
	@ModifiedID int,
	@ModifiedDT datetime,
	@DisplayName nvarchar(150),
	@Password nvarchar(150),
	@RoleID int,
	@UserKey uniqueidentifier,
	@UserLogin nvarchar(150),
	@EmailVerified bit,
	@VerifyCode nvarchar(50)
As
Begin
	Insert Into ApplicationUser
		([FirstNM],[LastNM],[eMailAddress],[CommentDS],[AccountNM],[SupervisorAccountNM],[CompanyID],[ModifiedID],[ModifiedDT],[DisplayName],[Password],[RoleID],[UserKey],[UserLogin],[EmailVerified],[VerifyCode])
	Values
		(@FirstNM,@LastNM,@eMailAddress,@CommentDS,@AccountNM,@SupervisorAccountNM,@CompanyID, @ModifiedID,@ModifiedDT,@DisplayName,@Password,@RoleID,@UserKey,@UserLogin,@EmailVerified,@VerifyCode)

	Declare @ApplicationUserID int
	Select @ApplicationUserID = @@IDENTITY


SELECT        ApplicationUser.ApplicationUserID, ApplicationUser.FirstNM, ApplicationUser.LastNM, ApplicationUser.eMailAddress, ApplicationUser.CommentDS, ApplicationUser.AccountNM, 
                         ApplicationUser.SupervisorAccountNM, ApplicationUser.LastLoginDT, ApplicationUser.LastLoginLocation, ApplicationUser.ModifiedID, ApplicationUser.ModifiedDT, 
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

