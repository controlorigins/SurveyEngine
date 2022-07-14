
CREATE PROC [dbo].[usp_SurveyResponse_CloneAnswers] 
    @SourceSurveyResponseID int ,
    @DestinationSurveyResponseID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[SurveyResponseHistory]
	WHERE  [SurveyResponseID] = @DestinationSurveyResponseID

	DELETE
	FROM   [dbo].[SurveyResponseAnswerReview]
	WHERE  [SurveyAnswerID] in (select SurveyAnswerID from SurveyResponseAnswer where SurveyResponseID =  @DestinationSurveyResponseID) 

	DELETE
	FROM   [dbo].[SurveyResponseAnswer_Error]
	WHERE  [SurveyResponseID] = @DestinationSurveyResponseID

	DELETE
	FROM   [dbo].[SurveyResponseAnswer]
	WHERE  [SurveyResponseID] = @DestinationSurveyResponseID
  
	UPDATE [dbo].[SurveyResponse] set DataSource = 'RESET by usp_SurveyResponse_CloneAnswers', StatusID = 1, ModifiedDT = GETDATE(), ModifiedID=1 
	WHERE  [SurveyResponseID] = @DestinationSurveyResponseID

    INSERT INTO [dbo].[SurveyResponseAnswer] ( SurveyResponseID, SequenceNumber, QuestionID, QuestionAnswerID, AnswerType, AnswerQuantity, AnswerDate, AnswerComment, ModifiedComment, ModifiedID, ModifiedDT )
    SELECT @DestinationSurveyResponseID AS SurveyResponseID, SequenceNumber, QuestionID, QuestionAnswerID, AnswerType, AnswerQuantity, AnswerDate, AnswerComment, ModifiedComment, 1 AS ModifiedID, GETDATE() AS ModifiedDT
    FROM [dbo].[SurveyResponseAnswer]
    WHERE (((SurveyResponseID)=@SourceSurveyResponseID));

	COMMIT



