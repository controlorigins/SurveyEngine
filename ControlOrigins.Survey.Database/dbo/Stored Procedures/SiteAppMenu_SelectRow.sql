

-- ==========================================================================================
-- Entity Name:	SiteAppMenu_SelectRow
-- Author:	Mark Hazleton
-- Create date:	9/23/2015 2:43:25 PM
-- Description:	This stored procedure is intended for selecting a specific row from SiteAppMenu table
-- ==========================================================================================
CREATE Procedure [dbo].[SiteAppMenu_SelectRow]
	@Id int
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
	Where
		[Id] = @Id
End

