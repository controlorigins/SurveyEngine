

-- ==========================================================================================
-- Entity Name:	UserAppProperty_SelectAll
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:43:21 AM
-- Description:	This stored procedure is intended for selecting all rows from UserAppProperty table
-- ==========================================================================================
CREATE Procedure [dbo].[UserAppProperty_SelectAll]
As
Begin
	Select 
		[Id],
		[UserID],
		[AppID],
		[Key],
		[Value]
	From UserAppProperty
End

