CREATE PROC [dbo].[gsp_SurveyResponseAnswerReview_Select] 
    @SurveyResponseAnswerReviewID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [SurveyResponseAnswerReviewID], [SurveyAnswerID], [ApplicationUserRoleID], [ReviewLevel], [ReviewStatusID], [ModifiedID], [ModifiedDT], [ModifiedComment] 
	FROM   [dbo].[SurveyResponseAnswerReview] 
	WHERE  ([SurveyResponseAnswerReviewID] = @SurveyResponseAnswerReviewID OR @SurveyResponseAnswerReviewID IS NULL) 

	COMMIT



