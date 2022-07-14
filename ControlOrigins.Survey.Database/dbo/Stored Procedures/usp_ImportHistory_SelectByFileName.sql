
CREATE PROC [dbo].[usp_ImportHistory_SelectByFileName] 
    @FileName nvarchar(150)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ImportHistoryID], [FileName], [ImportType], [NumberOfRows], [ImportLog], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[ImportHistory] 
	WHERE  ([FileName] = @FileName ) 

	COMMIT




