CREATE PROC [dbo].[gsp_ApplicationSurvey_Delete] 
    @ApplicationSurveyID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[ApplicationSurvey]
	WHERE  [ApplicationSurveyID] = @ApplicationSurveyID

	COMMIT



