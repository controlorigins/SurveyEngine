CREATE PROC [dbo].[gsp_SurveyResponseSequence_Delete] 
    @SurveyResponseSequenceID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[SurveyResponseSequence]
	WHERE  [SurveyResponseSequenceID] = @SurveyResponseSequenceID

	COMMIT



