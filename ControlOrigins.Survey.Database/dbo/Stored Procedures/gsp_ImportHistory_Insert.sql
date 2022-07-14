CREATE PROC [dbo].[gsp_ImportHistory_Insert] 
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
	
	INSERT INTO [dbo].[ImportHistory] ([FileName], [ImportType], [NumberOfRows], [ImportLog], [ModifiedID], [ModifiedDT])
	SELECT @FileName, @ImportType, @NumberOfRows, @ImportLog, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [ImportHistoryID], [FileName], [ImportType], [NumberOfRows], [ImportLog], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[ImportHistory]
	WHERE  [ImportHistoryID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



