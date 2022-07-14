CREATE PROC [dbo].[gsp_SurveyReviewStatus_Delete] 
    @SurveyReviewStatusID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[SurveyReviewStatus]
	WHERE  [SurveyReviewStatusID] = @SurveyReviewStatusID

	COMMIT



