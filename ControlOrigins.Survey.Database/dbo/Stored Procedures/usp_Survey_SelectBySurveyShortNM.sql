
CREATE PROC [dbo].[usp_Survey_SelectBySurveyShortNM] 
    @SurveyShortNM NVARCHAR(40)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

SELECT     SurveyID, SurveyTypeID, UseQuestionGroupsFL, SurveyNM, SurveyShortNM, SurveyDS, CompletionMessage, ResponseNMTemplate, ReviewerAccountNM, 
                      AutoAssignFilter, StartDT, EndDT, ParentSurveyID, ModifiedID, ModifiedDT
FROM         Survey
	WHERE  Survey.SurveyShortNM = @SurveyShortNM

	COMMIT




