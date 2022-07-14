CREATE PROC [dbo].[gsp_Role_Delete] 
    @RoleID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Role]
	WHERE  [RoleID] = @RoleID

	COMMIT



