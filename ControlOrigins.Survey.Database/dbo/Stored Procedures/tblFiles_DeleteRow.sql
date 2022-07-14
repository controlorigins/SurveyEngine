

-- ==========================================================================================
-- Entity Name:	tblFiles_DeleteRow
-- Create date:	10/16/2015 3:40:12 PM
-- Description:	This stored procedure is intended for deleting a specific row from tblFiles table
-- ==========================================================================================
CREATE Procedure [dbo].[tblFiles_DeleteRow]
	@id int
As
Begin
	Delete tblFiles
	Where
		[id] = @id

End

