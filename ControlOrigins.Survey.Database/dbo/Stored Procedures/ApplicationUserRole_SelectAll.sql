

-- ==========================================================================================
-- Entity Name:	ApplicationUserRole_SelectAll
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:43:41 AM
-- Description:	This stored procedure is intended for selecting all rows from ApplicationUserRole table
-- ==========================================================================================
CREATE Procedure [dbo].[ApplicationUserRole_SelectAll]
As
Begin
	Select 
		[ApplicationUserRoleID],
		[ApplicationID],
		[ApplicationUserID],
		[RoleID],
		[ModifiedID],
		[ModifiedDT],
		[IsDemo],
		[StartUpDate],
		[IsMonthlyPrice],
		[Price],
		[UserInRolled],
		[IsUserAdmin]
	From ApplicationUserRole
End

