CREATE PROC [dbo].[gsp_ImportHistory_Update] 
    @ImportHistoryID int,
    @FileName nvarchar(150),
    @ImportType nvarchar(50),
    @NumberOfRows int,
    @ImportLog nvarchar(MAX),
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[ImportHistory]
	SET    [FileName] = @FileName, [ImportType] = @ImportType, [NumberOfRows] = @NumberOfRows, [ImportLog] = @ImportLog, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [ImportHistoryID] = @ImportHistoryID
	
	-- Begin Return Select <- do not remove
	SELECT [ImportHistoryID], [FileName], [ImportType], [NumberOfRows], [ImportLog], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[ImportHistory]
	WHERE  [ImportHistoryID] = @ImportHistoryID	
	-- End Return Select <- do not remove

	COMMIT



