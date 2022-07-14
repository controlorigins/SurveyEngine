

-- ==========================================================================================
-- Entity Name:	UserAppProperty_SelectRow
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:43:21 AM
-- Description:	This stored procedure is intended for selecting a specific row from UserAppProperty table
-- ==========================================================================================
CREATE Procedure [dbo].[UserAppProperty_SelectRow]
	@Id int
As
Begin
	Select 
		[Id],
		[UserID],
		[AppID],
		[Key],
		[Value]
	From UserAppProperty
	Where
		[Id] = @Id
End

