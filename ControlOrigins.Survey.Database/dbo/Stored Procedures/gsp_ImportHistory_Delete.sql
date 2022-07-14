CREATE PROC [dbo].[gsp_ImportHistory_Delete] 
    @ImportHistoryID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[ImportHistory]
	WHERE  [ImportHistoryID] = @ImportHistoryID

	COMMIT



