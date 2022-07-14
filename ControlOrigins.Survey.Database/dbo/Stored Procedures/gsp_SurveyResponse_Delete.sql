CREATE PROC [dbo].[gsp_SurveyResponse_Delete] 
    @SurveyResponseID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[SurveyResponse]
	WHERE  [SurveyResponseID] = @SurveyResponseID

	COMMIT



