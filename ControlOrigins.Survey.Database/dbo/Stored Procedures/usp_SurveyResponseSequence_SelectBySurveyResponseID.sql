CREATE PROC [dbo].[usp_SurveyResponseSequence_SelectBySurveyResponseID] 
    @SurveyResponseID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [SurveyResponseSequenceID], 
	       [SurveyResponseID], 
	       [SequenceNumber], 
	       [SequenceText], 
	       [ModifiedID], 
	       [ModifiedDT] 
	FROM   [dbo].[SurveyResponseSequence] 
	WHERE  ([SurveyResponseID] = @SurveyResponseID) 

	COMMIT



