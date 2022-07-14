CREATE PROC [dbo].[gsp_lu_UnitOfMeasure_Update] 
    @UnitOfMeasureID int,
    @UnitOfMeasureNM nvarchar(50),
    @UnitOfMeasureDS nvarchar(MAX),
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[lu_UnitOfMeasure]
	SET    [UnitOfMeasureNM] = @UnitOfMeasureNM, [UnitOfMeasureDS] = @UnitOfMeasureDS, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [UnitOfMeasureID] = @UnitOfMeasureID
	
	-- Begin Return Select <- do not remove
	SELECT [UnitOfMeasureID], [UnitOfMeasureNM], [UnitOfMeasureDS], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[lu_UnitOfMeasure]
	WHERE  [UnitOfMeasureID] = @UnitOfMeasureID	
	-- End Return Select <- do not remove

	COMMIT



