

-- ==========================================================================================
-- Entity Name:	UserMessages_Insert
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:43:15 AM
-- Description:	This stored procedure is intended for inserting values to UserMessages table
-- ==========================================================================================
CREATE Procedure [dbo].[UserMessages_Insert]
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
	Insert Into UserMessages
		([ToUserID],[FromUserID],[Message],[Opened],[CratedDateTime],[Subject],[Deleted],[AppID],[ShowonPage],[FromApp])
	Values
		(@ToUserID,@FromUserID,@Message,@Opened,@CratedDateTime,@Subject,@Deleted,@AppID,@ShowonPage,@FromApp)

	Declare @Id int
	Select @Id = @@IDENTITY
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

