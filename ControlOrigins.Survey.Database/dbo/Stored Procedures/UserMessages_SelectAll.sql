

-- ==========================================================================================
-- Entity Name:	UserMessages_SelectAll
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:43:15 AM
-- Description:	This stored procedure is intended for selecting all rows from UserMessages table
-- ==========================================================================================
CREATE Procedure [dbo].[UserMessages_SelectAll]
As
Begin
	Select 
		[Id],
		[ToUserID],
		[FromUserID],
		[Message],
		[Opened],
		[CratedDateTime],
		[Subject],
		[Deleted],
		[AppID],
		[ShowonPage],
		[FromApp]
	From UserMessages
End

