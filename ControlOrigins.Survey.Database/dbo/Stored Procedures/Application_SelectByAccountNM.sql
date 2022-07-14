
CREATE PROC [dbo].[Application_SelectByAccountNM] 
    @AccountNM nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN


SELECT     Application.ApplicationID, Application.ApplicationNM, Application.ApplicationCD, Application.ApplicationShortNM, Application.ApplicationTypeID, 
                      Application.ApplicationDS, Application.MenuOrder, Role.ReadFL, Role.UpdateFL, ApplicationUser.ApplicationUserID, ApplicationUser.AccountNM, 
                      ApplicationUser.FirstNM, ApplicationUser.LastNM, ApplicationUser.eMailAddress, ApplicationUser.CommentDS, ApplicationUser.LastLoginDT, 
                      ApplicationUser.LastLoginLocation, ApplicationUserRole.ApplicationUserRoleID, Role.RoleID, Role.RoleCD, Role.RoleNM, Role.RoleDS, Role.ReviewLevel
FROM         Application INNER JOIN
                      ApplicationUserRole ON Application.ApplicationID = ApplicationUserRole.ApplicationID INNER JOIN
                      ApplicationUser ON ApplicationUserRole.ApplicationUserID = ApplicationUser.ApplicationUserID INNER JOIN
                      Role ON ApplicationUserRole.RoleID = Role.RoleID
     WHERE AccountNM = @AccountNM 
     ORDER BY dbo.Application.MenuOrder ASC

	COMMIT




