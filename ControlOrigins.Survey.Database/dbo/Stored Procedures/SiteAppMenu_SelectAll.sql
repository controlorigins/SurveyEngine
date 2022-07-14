

-- ==========================================================================================
-- Entity Name:	SiteAppMenu_SelectAll
-- Author:	Mark Hazleton
-- Create date:	9/23/2015 2:43:25 PM
-- Description:	This stored procedure is intended for selecting all rows from SiteAppMenu table
-- ==========================================================================================
CREATE Procedure [dbo].[SiteAppMenu_SelectAll]
As
Begin
	Select 
		[Id],
		[SiteAppID],
		[MenuText],
		[TartgetPage],
		[GlyphName],
		[MenuOrder],
		[SiteRoleID],
		[ViewInMenu]
	From SiteAppMenu
End

