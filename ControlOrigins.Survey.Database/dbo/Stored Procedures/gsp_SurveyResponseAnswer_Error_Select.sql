CREATE PROC [dbo].[gsp_SurveyResponseAnswer_Error_Select] 
    @SurveyAnswer_ErrorID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [SurveyAnswer_ErrorID], [SurveyResponseID], [SequenceNumber], [QuestionID], [QuestionAnswerID], [AnswerType], [AnswerQuantity], [AnswerDate], [AnswerComment], [ErrorCode], [ErrorMessage], [ProgramName], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[SurveyResponseAnswer_Error] 
	WHERE  ([SurveyAnswer_ErrorID] = @SurveyAnswer_ErrorID OR @SurveyAnswer_ErrorID IS NULL) 

	COMMIT



