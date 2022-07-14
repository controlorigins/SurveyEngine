

-- ==========================================================================================
-- Entity Name:	AppProperty_SelectRow
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:11:54 AM
-- Description:	This stored procedure is intended for selecting a specific row from AppProperty table
-- ==========================================================================================
CREATE Procedure [dbo].[AppProperty_SelectRow]
	@Id int
As
Begin
	Select 
		[Id],
		[SiteAppID],
		[Key],
		[Value]
	From AppProperty
	Where
		[Id] = @Id
End

