

-- ==========================================================================================
-- Entity Name:	Application_DeleteRow
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:43:54 AM
-- Description:	This stored procedure is intended for deleting a specific row from Application table
-- ==========================================================================================
CREATE Procedure [dbo].[Application_DeleteRow]
	@ApplicationID int
As
Begin
	Delete Application
	Where
		[ApplicationID] = @ApplicationID

End

