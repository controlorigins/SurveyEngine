

-- ==========================================================================================
-- Entity Name:	AppProperty_Update
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:11:54 AM
-- Description:	This stored procedure is intended for updating AppProperty table
-- ==========================================================================================
CREATE Procedure [dbo].[AppProperty_Update]
	@Id int,
	@SiteAppID int,
	@Key nvarchar(50),
	@Value nvarchar(500)
As
Begin
	Update AppProperty
	Set
		[SiteAppID] = @SiteAppID,
		[Key] = @Key,
		[Value] = @Value
	Where		
		[Id] = @Id
	Select 
		[Id],
		[SiteAppID],
		[Key],
		[Value]
	From AppProperty
	Where
		[Id] = @Id
End

