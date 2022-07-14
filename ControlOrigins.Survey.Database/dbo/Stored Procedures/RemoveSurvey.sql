


CREATE PROC [dbo].[RemoveSurvey] 
    @SurveyID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

delete [dbo].[SurveyResponseAnswer_Error] where SurveyResponseID in (select SurveyResponseID from SurveyResponse where SurveyID = @SurveyID)

delete [dbo].[SurveyResponseHistory] where SurveyResponseID in (select SurveyResponseID from SurveyResponse where SurveyID = @SurveyID)

delete [dbo].SurveyResponseAnswer where SurveyResponseID in (select SurveyResponseID from SurveyResponse where SurveyID = @SurveyID)

delete dbo.SurveyResponseSequence where SurveyResponseID in (select SurveyResponseID from SurveyResponse where SurveyID = @SurveyID)

delete dbo.SurveyResponse where SurveyResponseID in (select SurveyResponseID from SurveyResponse where SurveyID = @SurveyID)

delete dbo.QuestionAnswer where QuestionID in (select QuestionID from QuestionGroupMember where QuestionGroupID in (select QuestionGroupID from QuestionGroup where SurveyID = @SurveyID))

delete dbo.QuestionGroupMember where QuestionGroupID in (select QuestionGroupID from QuestionGroup where SurveyID = @SurveyID)

delete dbo.QuestionGroup where QuestionGroupID in (select QuestionGroupID from QuestionGroup where SurveyID = @SurveyID)

delete dbo.ApplicationSurvey where SurveyID = @SurveyID

delete dbo.Survey where SurveyID =@SurveyID 

COMMIT






