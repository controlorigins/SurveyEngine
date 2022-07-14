

-- ==========================================================================================
-- Entity Name:	UserMessages_DeleteRow
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:43:15 AM
-- Description:	This stored procedure is intended for deleting a specific row from UserMessages table
-- ==========================================================================================
CREATE Procedure [dbo].[UserMessages_DeleteRow]
	@Id int
As
Begin
	Delete UserMessages
	Where
		[Id] = @Id

End

