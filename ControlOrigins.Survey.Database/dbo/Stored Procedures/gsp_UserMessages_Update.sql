CREATE PROC [dbo].[gsp_UserMessages_Update] 
    @Id int,
    @ToUserID int = NULL,
    @FromUserID int = NULL,
    @Message nvarchar(MAX) = NULL,
    @Opened bit = NULL,
    @CratedDateTime datetime = NULL,
    @Subject nvarchar(50) = NULL,
    @Deleted bit = NULL,
    @AppID int = NULL,
    @ShowonPage int = NULL,
    @FromApp bit = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[UserMessages]
	SET    [ToUserID] = @ToUserID, [FromUserID] = @FromUserID, [Message] = @Message, [Opened] = @Opened, [CratedDateTime] = @CratedDateTime, [Subject] = @Subject, [Deleted] = @Deleted, [AppID] = @AppID, [ShowonPage] = @ShowonPage, [FromApp] = @FromApp
	WHERE  [Id] = @Id
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [ToUserID], [FromUserID], [Message], [Opened], [CratedDateTime], [Subject], [Deleted], [AppID], [ShowonPage], [FromApp]
	FROM   [dbo].[UserMessages]
	WHERE  [Id] = @Id	
	-- End Return Select <- do not remove

	COMMIT
