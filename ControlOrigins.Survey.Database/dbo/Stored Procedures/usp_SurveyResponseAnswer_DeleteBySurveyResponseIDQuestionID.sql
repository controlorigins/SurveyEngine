CREATE PROC [dbo].[usp_SurveyResponseAnswer_DeleteBySurveyResponseIDQuestionID] 
    @SurveyResponseID int,
    @SequenceNumber int,
    @QuestionID int 
    
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[SurveyResponseAnswer]
	WHERE  [SurveyResponseID] = @SurveyResponseID 
	AND    [SequenceNumber] = @SequenceNumber
	AND    [QuestionID] = @QuestionID

	COMMIT



