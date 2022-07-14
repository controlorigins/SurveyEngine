CREATE PROC [dbo].[gsp_lu_ApplicationType_Update] 
    @ApplicationTypeID int,
    @ApplicationTypeNM nvarchar(50),
    @ApplicationTypeDS nvarchar(MAX),
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[lu_ApplicationType]
	SET    [ApplicationTypeNM] = @ApplicationTypeNM, [ApplicationTypeDS] = @ApplicationTypeDS, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [ApplicationTypeID] = @ApplicationTypeID
	
	-- Begin Return Select <- do not remove
	SELECT [ApplicationTypeID], [ApplicationTypeNM], [ApplicationTypeDS], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[lu_ApplicationType]
	WHERE  [ApplicationTypeID] = @ApplicationTypeID	
	-- End Return Select <- do not remove

	COMMIT



