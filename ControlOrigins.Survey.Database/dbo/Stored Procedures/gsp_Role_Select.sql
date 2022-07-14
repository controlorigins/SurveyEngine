CREATE PROC [dbo].[gsp_Role_Select] 
    @RoleID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [RoleID], [RoleCD], [RoleNM], [RoleDS], [ReviewLevel], [ReadFL], [UpdateFL], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[Role] 
	WHERE  ([RoleID] = @RoleID OR @RoleID IS NULL) 

	COMMIT



