CREATE PROC [dbo].[gsp_SurveyResponseState_Delete] 
    @SurveyResponseStateID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[SurveyResponseState]
	WHERE  [SurveyResponseStateID] = @SurveyResponseStateID

	COMMIT



