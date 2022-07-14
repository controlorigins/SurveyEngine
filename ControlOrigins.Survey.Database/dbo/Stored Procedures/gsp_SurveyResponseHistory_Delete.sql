CREATE PROC [dbo].[gsp_SurveyResponseHistory_Delete] 
    @SurveyResponseHistoryID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[SurveyResponseHistory]
	WHERE  [SurveyResponseHistoryID] = @SurveyResponseHistoryID

	COMMIT



