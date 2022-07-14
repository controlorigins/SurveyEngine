
CREATE PROC [dbo].[usp_Survey_SelectByQuestionID] 
    @QuestionID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

SELECT     Survey.SurveyID, 
           Survey.SurveyTypeID, 
		   Survey.UseQuestionGroupsFL, 
		   Survey.SurveyNM, 
		   Survey.SurveyShortNM, 
		   Survey.SurveyDS, 
		   Survey.CompletionMessage, 
		   Survey.ResponseNMTemplate, 
		   Survey.ReviewerAccountNM, 
		   Survey.AutoAssignFilter, 
		   Survey.StartDT, 
		   Survey.EndDT, 
		   Survey.ParentSurveyID, 
		   Survey.ModifiedID, 
		   Survey.ModifiedDT 
FROM   Survey
where Survey.SurveyID in (Select SurveyID 
                            from QuestionGroup 
						where QuestionGroupID in (Select QuestionGroupID 
						                            from QuestionGroupMember 
												where QuestionID = @QuestionID))


	COMMIT




