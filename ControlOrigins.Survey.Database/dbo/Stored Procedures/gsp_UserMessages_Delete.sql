CREATE PROC [dbo].[gsp_UserMessages_Delete] 
    @Id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[UserMessages]
	WHERE  [Id] = @Id

	COMMIT
