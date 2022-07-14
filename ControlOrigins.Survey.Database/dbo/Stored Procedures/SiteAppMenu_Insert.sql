

-- ==========================================================================================
-- Entity Name:	SiteAppMenu_Insert
-- Author:	Mark Hazleton
-- Create date:	9/23/2015 2:43:25 PM
-- Description:	This stored procedure is intended for inserting values to SiteAppMenu table
-- ==========================================================================================
CREATE Procedure [dbo].[SiteAppMenu_Insert]
	@SiteAppID int,
	@MenuText nvarchar(50),
	@TartgetPage nvarchar(MAX),
	@GlyphName nvarchar(50),
	@MenuOrder int,
	@SiteRoleID int,
	@ViewInMenu bit
As
Begin
	Insert Into SiteAppMenu
		([SiteAppID],[MenuText],[TartgetPage],[GlyphName],[MenuOrder],[SiteRoleID],[ViewInMenu])
	Values
		(@SiteAppID,@MenuText,@TartgetPage,@GlyphName,@MenuOrder,@SiteRoleID,@ViewInMenu)

	Declare @Id int
	Select @Id = @@IDENTITY
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

