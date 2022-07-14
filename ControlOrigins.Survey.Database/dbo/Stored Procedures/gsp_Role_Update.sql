CREATE PROC [dbo].[gsp_Role_Update] 
    @RoleID int,
    @RoleCD nvarchar(50),
    @RoleNM nvarchar(50),
    @RoleDS nvarchar(MAX),
    @ReviewLevel int,
    @ReadFL bit,
    @UpdateFL bit,
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Role]
	SET    [RoleCD] = @RoleCD, [RoleNM] = @RoleNM, [RoleDS] = @RoleDS, [ReviewLevel] = @ReviewLevel, [ReadFL] = @ReadFL, [UpdateFL] = @UpdateFL, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [RoleID] = @RoleID
	
	-- Begin Return Select <- do not remove
	SELECT [RoleID], [RoleCD], [RoleNM], [RoleDS], [ReviewLevel], [ReadFL], [UpdateFL], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[Role]
	WHERE  [RoleID] = @RoleID	
	-- End Return Select <- do not remove

	COMMIT



