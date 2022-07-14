CREATE PROC [dbo].[gsp_SurveyResponseAnswer_Select] 
    @SurveyAnswerID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [SurveyAnswerID], [SurveyResponseID], [SequenceNumber], [QuestionID], [QuestionAnswerID], [AnswerType], [AnswerQuantity], [AnswerDate], [AnswerComment], [ModifiedComment], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[SurveyResponseAnswer] 
	WHERE  ([SurveyAnswerID] = @SurveyAnswerID OR @SurveyAnswerID IS NULL) 

	COMMIT



