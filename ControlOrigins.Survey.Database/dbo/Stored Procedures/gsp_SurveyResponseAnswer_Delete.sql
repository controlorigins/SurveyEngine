CREATE PROC [dbo].[gsp_SurveyResponseAnswer_Delete] 
    @SurveyAnswerID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[SurveyResponseAnswer]
	WHERE  [SurveyAnswerID] = @SurveyAnswerID

	COMMIT



