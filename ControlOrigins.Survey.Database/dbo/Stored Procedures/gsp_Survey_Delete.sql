CREATE PROC [dbo].[gsp_Survey_Delete] 
    @SurveyID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Survey]
	WHERE  [SurveyID] = @SurveyID

	COMMIT



