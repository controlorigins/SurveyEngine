CREATE PROC [dbo].[gsp_ImportHistory_Select] 
    @ImportHistoryID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ImportHistoryID], [FileName], [ImportType], [NumberOfRows], [ImportLog], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[ImportHistory] 
	WHERE  ([ImportHistoryID] = @ImportHistoryID OR @ImportHistoryID IS NULL) 

	COMMIT



