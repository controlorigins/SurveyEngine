CREATE PROC [dbo].[gsp_UserMessages_Select] 
    @Id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [Id], [ToUserID], [FromUserID], [Message], [Opened], [CratedDateTime], [Subject], [Deleted], [AppID], [ShowonPage], [FromApp] 
	FROM   [dbo].[UserMessages] 
	WHERE  ([Id] = @Id OR @Id IS NULL) 

	COMMIT
