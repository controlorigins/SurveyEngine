
CREATE PROC [dbo].[usp_ApplicationSurveyResponseDetail_BySurveyResponseID] 
    @SurveyResponseID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

SELECT     SurveyAnswerID, AnswerType, AnswerQuantity, AnswerDate, AnswerComment, ModifiedComment, QuestionID, QuestionNM, QuestionDS, QuestionSort, 
                      ReviewRoleLevel, QuestionCommentFL, QuestionValue, QuestionTypeID, QuestionTypeCD, QuestionTypeDS, AnswerDataType, QuestionAnswerID, 
                      QuestionAnswerSort, QuestionAnswerShortNM, QuestionAnswerNM, QuestionAnswerValue, QuestionAnswerDS, QuestionAnswerCommentFL, ActiveFL, 
                      SurveyResponseID, SurveyResponseNM, DataSource, SurveyID, UseQuestionGroupsFL, SurveyNM, SurveyShortNM, SurveyDS, CompletionMessage, 
                      ResponseNMTemplate, ReviewerAccountNM, AutoAssignFilter, StartDT, EndDT, ApplicationID, ApplicationNM, ApplicationCD, ApplicationShortNM, ApplicationDS, 
                      MenuOrder, SurveyTypeID, SurveyTypeShortNM, SurveyTypeNM, SurveyTypeDS, SurveyTypeComment, MutiSequenceFL, ApplicationTypeID, ApplicationTypeNM, 
                      ApplicationTypeDS, QuestionShortNM, StatusNM, StatusDS, EmailTemplate, EmailSubjectTemplate, FirstNM, LastNM, eMailAddress, CommentDS, AccountNM, 
                      SupervisorAccountNM, LastLoginDT, LastLoginLocation, SurveyResponseSequenceID, SequenceNumber, SequenceText, QuestionGroupMemberID, QuestionWeight, 
                      DisplayOrder, QuestionGroupID, GroupOrder, QuestionGroupShortNM, QuestionGroupNM, QuestionGroupDS, QuestionGroupWeight, GroupHeader, GroupFooter, 
                      DependentQuestionGroupID, DependentMinScore, DependentMaxScore
FROM         vwApplicationSurveyResponseDetail
WHERE     (SurveyResponseID = @SurveyResponseID)

COMMIT




