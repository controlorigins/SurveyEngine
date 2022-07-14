
CREATE PROC [dbo].[usp_Survey_DeleteRelated] 
    @SurveyID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE FROM [dbo].[SurveyStatus] WHERE [SurveyID] = @SurveyID
	DELETE FROM [dbo].[SurveyReviewStatus] WHERE [SurveyID] = @SurveyID
	DELETE FROM [dbo].[ApplicationSurvey] WHERE [SurveyID] = @SurveyID
	DELETE FROM [dbo].[Survey] WHERE [SurveyID] = @SurveyID

	COMMIT




