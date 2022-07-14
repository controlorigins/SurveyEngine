

-- ==========================================================================================
-- Entity Name:	ApplicationUser_Update
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:43:47 AM
-- Description:	This stored procedure is intended for updating ApplicationUser table
-- ==========================================================================================
CREATE Procedure [dbo].[ApplicationUser_Update]
	@ApplicationUserID int,
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
	@RoleID int,
	@UserLogin nvarchar(150),
	@EmailVerified bit,
	@VerifyCode nvarchar(50)
As
Begin
	Update ApplicationUser
	Set
		[FirstNM] = @FirstNM,
		[LastNM] = @LastNM,
		[eMailAddress] = @eMailAddress,
		[CommentDS] = @CommentDS,
		[AccountNM] = @AccountNM,
		[SupervisorAccountNM] = @SupervisorAccountNM,
		[CompanyID] = @CompanyID,
		[ModifiedID] = @ModifiedID,
		[ModifiedDT] = @ModifiedDT,
		[DisplayName] = @DisplayName,
		[RoleID] = @RoleID,
		[UserLogin] = @UserLogin,
		[EmailVerified] = @EmailVerified,
		[VerifyCode] = @VerifyCode
	Where		
		[ApplicationUserID] = @ApplicationUserID


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

