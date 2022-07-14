CREATE PROC [dbo].[usp_SurveyResponse_Reset] 
    @SurveyResponseID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[SurveyResponseHistory]
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

   
	UPDATE [dbo].[SurveyResponse] set DataSource = 'RESET by usp_SurveyResponse_Reset', StatusID = 1, ModifiedDT = GETDATE(), ModifiedID=1 
	WHERE  [SurveyResponseID] = @SurveyResponseID


	COMMIT



