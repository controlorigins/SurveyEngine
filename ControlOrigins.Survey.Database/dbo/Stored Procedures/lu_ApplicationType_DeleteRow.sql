

-- ==========================================================================================
-- Entity Name:	lu_ApplicationType_DeleteRow
-- Create date:	10/7/2015 10:26:11 PM
-- Description:	This stored procedure is intended for deleting a specific row from lu_ApplicationType table
-- ==========================================================================================
CREATE Procedure [dbo].[lu_ApplicationType_DeleteRow]
	@ApplicationTypeID int
As
Begin
	Delete lu_ApplicationType
	Where
		[ApplicationTypeID] = @ApplicationTypeID

End

