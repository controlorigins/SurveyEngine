CREATE PROC [dbo].[gsp_SurveyResponseAnswer_Error_Insert] 
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
	
	INSERT INTO [dbo].[SurveyResponseAnswer_Error] ([SurveyResponseID], [SequenceNumber], [QuestionID], [QuestionAnswerID], [AnswerType], [AnswerQuantity], [AnswerDate], [AnswerComment], [ErrorCode], [ErrorMessage], [ProgramName], [ModifiedID], [ModifiedDT])
	SELECT @SurveyResponseID, @SequenceNumber, @QuestionID, @QuestionAnswerID, @AnswerType, @AnswerQuantity, @AnswerDate, @AnswerComment, @ErrorCode, @ErrorMessage, @ProgramName, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyAnswer_ErrorID], [SurveyResponseID], [SequenceNumber], [QuestionID], [QuestionAnswerID], [AnswerType], [AnswerQuantity], [AnswerDate], [AnswerComment], [ErrorCode], [ErrorMessage], [ProgramName], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[SurveyResponseAnswer_Error]
	WHERE  [SurveyAnswer_ErrorID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



