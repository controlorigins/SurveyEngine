CREATE PROC [dbo].[gsp_lu_ReviewStatus_Delete] 
    @ReviewStatusID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[lu_ReviewStatus]
	WHERE  [ReviewStatusID] = @ReviewStatusID

	COMMIT



