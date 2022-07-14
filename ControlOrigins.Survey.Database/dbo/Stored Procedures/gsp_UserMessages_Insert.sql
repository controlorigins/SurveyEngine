CREATE PROC [dbo].[gsp_UserMessages_Insert] 
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
	
	INSERT INTO [dbo].[UserMessages] ([ToUserID], [FromUserID], [Message], [Opened], [CratedDateTime], [Subject], [Deleted], [AppID], [ShowonPage], [FromApp])
	SELECT @ToUserID, @FromUserID, @Message, @Opened, @CratedDateTime, @Subject, @Deleted, @AppID, @ShowonPage, @FromApp
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [ToUserID], [FromUserID], [Message], [Opened], [CratedDateTime], [Subject], [Deleted], [AppID], [ShowonPage], [FromApp]
	FROM   [dbo].[UserMessages]
	WHERE  [Id] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
