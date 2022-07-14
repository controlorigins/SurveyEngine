

-- ==========================================================================================
-- Entity Name:	tblFiles_SelectRow
-- Create date:	10/16/2015 3:40:12 PM
-- Description:	This stored procedure is intended for selecting a specific row from tblFiles table
-- ==========================================================================================
CREATE Procedure [dbo].[tblFiles_SelectRow]
	@id int
As
Begin
	Select 
		[id],
		[Name],
		[ContentType],
		[Data]
	From tblFiles
	Where
		[id] = @id
End

