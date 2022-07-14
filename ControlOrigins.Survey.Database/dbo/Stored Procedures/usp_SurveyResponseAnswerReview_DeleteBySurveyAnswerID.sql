CREATE PROC [dbo].[usp_SurveyResponseAnswerReview_DeleteBySurveyAnswerID] 
    @SurveyAnswerID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[SurveyResponseAnswerReview]
	WHERE  [SurveyAnswerID] = @SurveyAnswerID

	COMMIT



