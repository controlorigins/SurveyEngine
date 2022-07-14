

-- ==========================================================================================
-- Entity Name:	ApplicationUserRole_SelectRow
-- Author:	Mark Hazleton
-- Modified date:	9/30/2015 11:33:41 AM
-- Description:	This stored procedure is intended for selecting a specific row from ApplicationUserRole table
-- ==========================================================================================
CREATE Procedure [dbo].[ApplicationUserRole_SelectRow]
	@ApplicationUserRoleID int
As
Begin
	SELECT        ApplicationUserRole.ApplicationUserRoleID, ApplicationUserRole.ApplicationID, ApplicationUserRole.ApplicationUserID, ApplicationUserRole.RoleID, ApplicationUserRole.ModifiedID, 
                         ApplicationUserRole.ModifiedDT, ApplicationUserRole.IsDemo, ApplicationUserRole.StartUpDate, ApplicationUserRole.IsMonthlyPrice, ApplicationUserRole.Price, ApplicationUserRole.UserInRolled, 
                         ApplicationUserRole.IsUserAdmin, Application.ApplicationNM, Application.ApplicationShortNM, Application.MenuOrder, ApplicationUser.FirstNM, ApplicationUser.LastNM, ApplicationUser.eMailAddress, 
                         ApplicationUser.AccountNM, Role.RoleCD, Role.RoleNM
FROM            ApplicationUserRole LEFT OUTER JOIN
                         Role ON ApplicationUserRole.RoleID = Role.RoleID LEFT OUTER JOIN
                         ApplicationUser ON ApplicationUserRole.ApplicationUserID = ApplicationUser.ApplicationUserID LEFT OUTER JOIN
                         Application ON ApplicationUserRole.ApplicationID = Application.ApplicationID
	Where
		[ApplicationUserRoleID] = @ApplicationUserRoleID
End

