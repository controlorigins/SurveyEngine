CREATE PROC [dbo].[gsp_lu_UnitOfMeasure_Insert] 
    @UnitOfMeasureNM nvarchar(50),
    @UnitOfMeasureDS nvarchar(MAX),
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[lu_UnitOfMeasure] ([UnitOfMeasureNM], [UnitOfMeasureDS], [ModifiedID], [ModifiedDT])
	SELECT @UnitOfMeasureNM, @UnitOfMeasureDS, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [UnitOfMeasureID], [UnitOfMeasureNM], [UnitOfMeasureDS], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[lu_UnitOfMeasure]
	WHERE  [UnitOfMeasureID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



