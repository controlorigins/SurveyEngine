

-- ==========================================================================================
-- Entity Name:	UserAppProperty_DeleteRow
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:43:21 AM
-- Description:	This stored procedure is intended for deleting a specific row from UserAppProperty table
-- ==========================================================================================
CREATE Procedure [dbo].[UserAppProperty_DeleteRow]
	@Id int
As
Begin
	Delete UserAppProperty
	Where
		[Id] = @Id

End

