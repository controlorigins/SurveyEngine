CREATE PROC [dbo].[gsp_SurveyResponseAnswer_Insert] 
    @SurveyResponseID int,
    @SequenceNumber int,
    @QuestionID int,
    @QuestionAnswerID int,
    @AnswerType nvarchar(20),
    @AnswerQuantity float,
    @AnswerDate datetime,
    @AnswerComment nvarchar(MAX),
    @ModifiedComment nvarchar(MAX),
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[SurveyResponseAnswer] ([SurveyResponseID], [SequenceNumber], [QuestionID], [QuestionAnswerID], [AnswerType], [AnswerQuantity], [AnswerDate], [AnswerComment], [ModifiedComment], [ModifiedID], [ModifiedDT])
	SELECT @SurveyResponseID, @SequenceNumber, @QuestionID, @QuestionAnswerID, @AnswerType, @AnswerQuantity, @AnswerDate, @AnswerComment, @ModifiedComment, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyAnswerID], [SurveyResponseID], [SequenceNumber], [QuestionID], [QuestionAnswerID], [AnswerType], [AnswerQuantity], [AnswerDate], [AnswerComment], [ModifiedComment], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[SurveyResponseAnswer]
	WHERE  [SurveyAnswerID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



