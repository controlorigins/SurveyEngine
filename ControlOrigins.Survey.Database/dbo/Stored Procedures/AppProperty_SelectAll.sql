

-- ==========================================================================================
-- Entity Name:	AppProperty_SelectAll
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:11:54 AM
-- Description:	This stored procedure is intended for selecting all rows from AppProperty table
-- ==========================================================================================
CREATE Procedure [dbo].[AppProperty_SelectAll]
As
Begin
	Select 
		[Id],
		[SiteAppID],
		[Key],
		[Value]
	From AppProperty
End

