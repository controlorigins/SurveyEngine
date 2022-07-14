

-- ==========================================================================================
-- Entity Name:	SiteAppMenu_DeleteRow
-- Author:	Mark Hazleton
-- Create date:	9/23/2015 2:43:25 PM
-- Description:	This stored procedure is intended for deleting a specific row from SiteAppMenu table
-- ==========================================================================================
CREATE Procedure [dbo].[SiteAppMenu_DeleteRow]
	@Id int
As
Begin
	Delete SiteAppMenu
	Where
		[Id] = @Id

End

