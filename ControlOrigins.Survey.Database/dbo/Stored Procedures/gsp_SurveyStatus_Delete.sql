CREATE PROC [dbo].[gsp_SurveyStatus_Delete] 
    @SurveyStatusID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[SurveyStatus]
	WHERE  [SurveyStatusID] = @SurveyStatusID

	COMMIT



