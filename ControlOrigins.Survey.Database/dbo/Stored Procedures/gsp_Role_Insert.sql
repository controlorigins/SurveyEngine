CREATE PROC [dbo].[gsp_Role_Insert] 
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
	
	INSERT INTO [dbo].[Role] ([RoleCD], [RoleNM], [RoleDS], [ReviewLevel], [ReadFL], [UpdateFL], [ModifiedID], [ModifiedDT])
	SELECT @RoleCD, @RoleNM, @RoleDS, @ReviewLevel, @ReadFL, @UpdateFL, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [RoleID], [RoleCD], [RoleNM], [RoleDS], [ReviewLevel], [ReadFL], [UpdateFL], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[Role]
	WHERE  [RoleID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



