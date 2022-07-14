CREATE PROC [dbo].[gsp_SurveyResponseAnswer_Update] 
    @SurveyAnswerID int,
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

	UPDATE [dbo].[SurveyResponseAnswer]
	SET    [SurveyResponseID] = @SurveyResponseID, [SequenceNumber] = @SequenceNumber, [QuestionID] = @QuestionID, [QuestionAnswerID] = @QuestionAnswerID, [AnswerType] = @AnswerType, [AnswerQuantity] = @AnswerQuantity, [AnswerDate] = @AnswerDate, [AnswerComment] = @AnswerComment, [ModifiedComment] = @ModifiedComment, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [SurveyAnswerID] = @SurveyAnswerID
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyAnswerID], [SurveyResponseID], [SequenceNumber], [QuestionID], [QuestionAnswerID], [AnswerType], [AnswerQuantity], [AnswerDate], [AnswerComment], [ModifiedComment], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[SurveyResponseAnswer]
	WHERE  [SurveyAnswerID] = @SurveyAnswerID	
	-- End Return Select <- do not remove

	COMMIT



