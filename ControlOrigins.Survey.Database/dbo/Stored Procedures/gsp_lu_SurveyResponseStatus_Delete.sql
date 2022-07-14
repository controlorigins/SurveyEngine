CREATE PROC [dbo].[gsp_lu_SurveyResponseStatus_Delete] 
    @StatusID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[lu_SurveyResponseStatus]
	WHERE  [StatusID] = @StatusID

	COMMIT



