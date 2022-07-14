

-- ==========================================================================================
-- Entity Name:	UserMessages_Update
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:43:15 AM
-- Description:	This stored procedure is intended for updating UserMessages table
-- ==========================================================================================
CREATE Procedure [dbo].[UserMessages_Update]
	@Id int,
	@ToUserID int,
	@FromUserID int,
	@Message nvarchar(MAX),
	@Opened bit,
	@CratedDateTime datetime,
	@Subject nvarchar(50),
	@Deleted bit,
	@AppID int,
	@ShowonPage int,
	@FromApp bit
As
Begin
	Update UserMessages
	Set
		[ToUserID] = @ToUserID,
		[FromUserID] = @FromUserID,
		[Message] = @Message,
		[Opened] = @Opened,
		[CratedDateTime] = @CratedDateTime,
		[Subject] = @Subject,
		[Deleted] = @Deleted,
		[AppID] = @AppID,
		[ShowonPage] = @ShowonPage,
		[FromApp] = @FromApp
	Where		
		[Id] = @Id
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

