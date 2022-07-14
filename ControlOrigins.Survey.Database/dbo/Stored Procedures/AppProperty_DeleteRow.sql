

-- ==========================================================================================
-- Entity Name:	AppProperty_DeleteRow
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:11:54 AM
-- Description:	This stored procedure is intended for deleting a specific row from AppProperty table
-- ==========================================================================================
CREATE Procedure [dbo].[AppProperty_DeleteRow]
	@Id int
As
Begin
	Delete AppProperty
	Where
		[Id] = @Id

End

