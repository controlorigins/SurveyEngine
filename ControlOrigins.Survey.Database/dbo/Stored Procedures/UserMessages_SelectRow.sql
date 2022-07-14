

-- ==========================================================================================
-- Entity Name:	UserMessages_SelectRow
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:43:15 AM
-- Description:	This stored procedure is intended for selecting a specific row from UserMessages table
-- ==========================================================================================
CREATE Procedure [dbo].[UserMessages_SelectRow]
	@Id int
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
	Where
		[Id] = @Id
End

