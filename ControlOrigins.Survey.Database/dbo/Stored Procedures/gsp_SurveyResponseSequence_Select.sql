CREATE PROC [dbo].[gsp_SurveyResponseSequence_Select] 
    @SurveyResponseSequenceID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [SurveyResponseSequenceID], [SurveyResponseID], [SequenceNumber], [SequenceText], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[SurveyResponseSequence] 
	WHERE  ([SurveyResponseSequenceID] = @SurveyResponseSequenceID OR @SurveyResponseSequenceID IS NULL) 

	COMMIT



