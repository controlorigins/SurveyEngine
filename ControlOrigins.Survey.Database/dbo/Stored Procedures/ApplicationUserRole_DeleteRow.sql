

-- ==========================================================================================
-- Entity Name:	ApplicationUserRole_DeleteRow
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:43:41 AM
-- Description:	This stored procedure is intended for deleting a specific row from ApplicationUserRole table
-- ==========================================================================================
CREATE Procedure [dbo].[ApplicationUserRole_DeleteRow]
	@ApplicationUserRoleID int
As
Begin
	Delete ApplicationUserRole
	Where
		[ApplicationUserRoleID] = @ApplicationUserRoleID

End

