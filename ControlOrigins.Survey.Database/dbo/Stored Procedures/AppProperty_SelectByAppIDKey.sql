

-- ==========================================================================================
-- Entity Name:	AppProperty_SelectByAppIDKey
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:11:54 AM
-- Description:	This stored procedure is intended for selecting a specific row from AppProperty table
-- ==========================================================================================
CREATE Procedure [dbo].[AppProperty_SelectByAppIDKey]
	@SiteAppId int,
	@Key nvarchar(50)
As
Begin
	Select 
		[Id],
		[SiteAppID],
		[Key],
		[Value]
	From AppProperty
	Where
		[SiteAppID] = @SiteAppId 
		and [Key] = @Key

End


