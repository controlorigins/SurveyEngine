CREATE VIEW [dbo].[vwApplicationPermissison]
AS
SELECT     dbo.Application.ApplicationID, dbo.Application.ApplicationNM, dbo.Application.ApplicationCD, dbo.Application.ApplicationShortNM, dbo.Application.ApplicationTypeID, 
                      dbo.Application.ApplicationDS, dbo.Application.MenuOrder, dbo.ApplicationUserRole.ApplicationUserRoleID, dbo.ApplicationUserRole.ModifiedID, 
                      dbo.ApplicationUserRole.ModifiedDT, dbo.ApplicationUser.ApplicationUserID, dbo.ApplicationUser.FirstNM, dbo.ApplicationUser.LastNM, 
                      dbo.ApplicationUser.eMailAddress, dbo.ApplicationUser.CommentDS, dbo.ApplicationUser.AccountNM, dbo.ApplicationUser.LastLoginDT, 
                      dbo.ApplicationUser.LastLoginLocation, dbo.Role.RoleID, dbo.Role.RoleCD, dbo.Role.RoleNM, dbo.Role.ReviewLevel, dbo.Role.ReadFL, dbo.Role.UpdateFL, 
                      dbo.Role.RoleDS
FROM         dbo.ApplicationUser INNER JOIN
                      dbo.ApplicationUserRole ON dbo.ApplicationUser.ApplicationUserID = dbo.ApplicationUserRole.ApplicationUserID INNER JOIN
                      dbo.Role ON dbo.ApplicationUserRole.RoleID = dbo.Role.RoleID INNER JOIN
                      dbo.Application ON dbo.ApplicationUserRole.ApplicationID = dbo.Application.ApplicationID

