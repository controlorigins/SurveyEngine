

-- ==========================================================================================
-- Entity Name:	tblFiles_SelectAll
-- Create date:	10/16/2015 3:40:12 PM
-- Description:	This stored procedure is intended for selecting all rows from tblFiles table
-- ==========================================================================================
CREATE Procedure [dbo].[tblFiles_SelectAll]
As
Begin
	Select 
		[id],
		[Name],
		[ContentType],
		[Data]
	From tblFiles
End

