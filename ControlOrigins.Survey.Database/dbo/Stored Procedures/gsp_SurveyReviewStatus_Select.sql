CREATE PROC [dbo].[gsp_SurveyReviewStatus_Select] 
    @SurveyReviewStatusID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [SurveyReviewStatusID], [SurveyID], [ReviewStatusID], [ReviewStatusNM], [ReviewStatusDS], [ApprovedFL], [CommentFL], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[SurveyReviewStatus] 
	WHERE  ([SurveyReviewStatusID] = @SurveyReviewStatusID OR @SurveyReviewStatusID IS NULL) 

	COMMIT



