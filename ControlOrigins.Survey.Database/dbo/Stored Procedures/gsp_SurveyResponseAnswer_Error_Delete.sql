CREATE PROC [dbo].[gsp_SurveyResponseAnswer_Error_Delete] 
    @SurveyAnswer_ErrorID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[SurveyResponseAnswer_Error]
	WHERE  [SurveyAnswer_ErrorID] = @SurveyAnswer_ErrorID

	COMMIT



