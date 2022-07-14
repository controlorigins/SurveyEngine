
CREATE PROC [dbo].[usp_SurveyReviewStatus_SelectBySurveyID] 
    @SurveyID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [SurveyReviewStatusID], [SurveyID], [ReviewStatusID], [ReviewStatusNM], [ReviewStatusDS], [ApprovedFL], [CommentFL], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[SurveyReviewStatus] 
	WHERE  ([SurveyID] = @SurveyID) 

	COMMIT




