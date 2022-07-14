

CREATE PROC [dbo].[RemoveSurveyResponse] 
    @SurveyResponseID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

delete [dbo].[SurveyResponseAnswer_Error] where SurveyResponseID = @SurveyResponseID

delete [dbo].[SurveyResponseHistory] where SurveyResponseID = @SurveyResponseID

delete [dbo].SurveyResponseAnswer where SurveyResponseID = @SurveyResponseID

delete [dbo].SurveyResponseSequence where SurveyResponseID = @SurveyResponseID

delete [dbo].SurveyResponse where SurveyResponseID = @SurveyResponseID

COMMIT





