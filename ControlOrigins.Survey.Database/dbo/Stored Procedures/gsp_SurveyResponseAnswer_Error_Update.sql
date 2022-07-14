CREATE PROC [dbo].[gsp_SurveyResponseAnswer_Error_Update] 
    @SurveyAnswer_ErrorID int,
    @SurveyResponseID int,
    @SequenceNumber int,
    @QuestionID int,
    @QuestionAnswerID int,
    @AnswerType nvarchar(MAX),
    @AnswerQuantity nvarchar(MAX),
    @AnswerDate nvarchar(MAX),
    @AnswerComment nvarchar(MAX),
    @ErrorCode nvarchar(MAX),
    @ErrorMessage nvarchar(MAX),
    @ProgramName nvarchar(MAX),
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[SurveyResponseAnswer_Error]
	SET    [SurveyResponseID] = @SurveyResponseID, [SequenceNumber] = @SequenceNumber, [QuestionID] = @QuestionID, [QuestionAnswerID] = @QuestionAnswerID, [AnswerType] = @AnswerType, [AnswerQuantity] = @AnswerQuantity, [AnswerDate] = @AnswerDate, [AnswerComment] = @AnswerComment, [ErrorCode] = @ErrorCode, [ErrorMessage] = @ErrorMessage, [ProgramName] = @ProgramName, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [SurveyAnswer_ErrorID] = @SurveyAnswer_ErrorID
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyAnswer_ErrorID], [SurveyResponseID], [SequenceNumber], [QuestionID], [QuestionAnswerID], [AnswerType], [AnswerQuantity], [AnswerDate], [AnswerComment], [ErrorCode], [ErrorMessage], [ProgramName], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[SurveyResponseAnswer_Error]
	WHERE  [SurveyAnswer_ErrorID] = @SurveyAnswer_ErrorID	
	-- End Return Select <- do not remove

	COMMIT



