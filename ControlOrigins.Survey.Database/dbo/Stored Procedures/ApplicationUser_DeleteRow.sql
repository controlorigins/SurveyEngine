

-- ==========================================================================================
-- Entity Name:	ApplicationUser_DeleteRow
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:43:47 AM
-- Description:	This stored procedure is intended for deleting a specific row from ApplicationUser table
-- ==========================================================================================
CREATE Procedure [dbo].[ApplicationUser_DeleteRow]
	@ApplicationUserID int
As
Begin
	Delete ApplicationUser
	Where
		[ApplicationUserID] = @ApplicationUserID

End

