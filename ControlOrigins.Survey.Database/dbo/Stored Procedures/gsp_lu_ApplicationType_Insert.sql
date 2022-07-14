CREATE PROC [dbo].[gsp_lu_ApplicationType_Insert] 
    @ApplicationTypeNM nvarchar(50),
    @ApplicationTypeDS nvarchar(MAX),
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[lu_ApplicationType] ([ApplicationTypeNM], [ApplicationTypeDS], [ModifiedID], [ModifiedDT])
	SELECT @ApplicationTypeNM, @ApplicationTypeDS, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [ApplicationTypeID], [ApplicationTypeNM], [ApplicationTypeDS], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[lu_ApplicationType]
	WHERE  [ApplicationTypeID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



