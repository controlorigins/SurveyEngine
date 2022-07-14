CREATE PROC [dbo].[gsp_SurveyResponseAnswerReview_Delete] 
    @SurveyResponseAnswerReviewID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[SurveyResponseAnswerReview]
	WHERE  [SurveyResponseAnswerReviewID] = @SurveyResponseAnswerReviewID

	COMMIT



