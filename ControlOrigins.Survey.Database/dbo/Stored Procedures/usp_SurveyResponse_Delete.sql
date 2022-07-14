CREATE PROC [dbo].[usp_SurveyResponse_Delete] 
    @SurveyResponseID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[SurveyResponseHistory]
	WHERE  [SurveyResponseID] = @SurveyResponseID

	DELETE
	FROM   [dbo].[SurveyResponseState]
	WHERE  [SurveyResponseID] = @SurveyResponseID
 
 	DELETE
	FROM   [dbo].[SurveyResponseAnswerReview]
	WHERE  [SurveyAnswerID] in (select SurveyAnswerID from SurveyResponseAnswer where SurveyResponseID =  @SurveyResponseID) 

	DELETE
	FROM   [dbo].[SurveyResponseAnswer_Error]
	WHERE  [SurveyResponseID] = @SurveyResponseID

	DELETE
	FROM   [dbo].[SurveyResponseAnswer]
	WHERE  [SurveyResponseID] = @SurveyResponseID

	DELETE
	FROM   [dbo].[SurveyResponseSequence]
	WHERE  [SurveyResponseID] = @SurveyResponseID

	DELETE
	FROM   [dbo].[SurveyResponse]
	WHERE  [SurveyResponseID] = @SurveyResponseID


	COMMIT



