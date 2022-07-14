

-- ==========================================================================================
-- Entity Name:	SiteAppMenu_Update
-- Author:	Mark Hazleton
-- Create date:	9/23/2015 2:43:25 PM
-- Description:	This stored procedure is intended for updating SiteAppMenu table
-- ==========================================================================================
CREATE Procedure [dbo].[SiteAppMenu_Update]
	@Id int,
	@SiteAppID int,
	@MenuText nvarchar(50),
	@TartgetPage nvarchar(MAX),
	@GlyphName nvarchar(50),
	@MenuOrder int,
	@SiteRoleID int,
	@ViewInMenu bit
As
Begin
	Update SiteAppMenu
	Set
		[SiteAppID] = @SiteAppID,
		[MenuText] = @MenuText,
		[TartgetPage] = @TartgetPage,
		[GlyphName] = @GlyphName,
		[MenuOrder] = @MenuOrder,
		[SiteRoleID] = @SiteRoleID,
		[ViewInMenu] = @ViewInMenu
	Where		
		[Id] = @Id
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

