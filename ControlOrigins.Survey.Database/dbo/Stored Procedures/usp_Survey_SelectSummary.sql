
CREATE PROC [dbo].[usp_Survey_SelectSummary] 

AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

SELECT        Survey.SurveyID, Survey.SurveyTypeID, Survey.SurveyNM, Survey.SurveyShortNM, Survey.SurveyDS, Survey.CompletionMessage, Survey.ResponseNMTemplate, Survey.ReviewerAccountNM, 
                         Survey.AutoAssignFilter, Survey.StartDT, Survey.EndDT, Survey.ParentSurveyID, COUNT(DISTINCT ApplicationSurvey.ApplicationID) AS ApplicationCount, COUNT(DISTINCT SurveyResponse.SurveyResponseID) 
                         AS SurveyResponseCount, COUNT(DISTINCT QuestionGroup.QuestionGroupID) AS QuestionGroupCount, COUNT(DISTINCT QuestionGroupMember.QuestionID) AS QuestionCount, 
                         lu_SurveyType.SurveyTypeShortNM, lu_SurveyType.SurveyTypeNM, ParentSurvey.SurveyNM AS ParentSurveyNM, ParentSurvey.SurveyShortNM AS ParentSurveyShortNM
FROM            ApplicationSurvey RIGHT OUTER JOIN
                         Survey AS ParentSurvey RIGHT OUTER JOIN
                         Survey ON ParentSurvey.ParentSurveyID = Survey.SurveyID LEFT OUTER JOIN
                         lu_SurveyType ON Survey.SurveyTypeID = lu_SurveyType.SurveyTypeID LEFT OUTER JOIN
                         QuestionGroup ON Survey.SurveyID = QuestionGroup.SurveyID ON ApplicationSurvey.SurveyID = Survey.SurveyID LEFT OUTER JOIN
                         SurveyResponse ON Survey.SurveyID = SurveyResponse.SurveyID LEFT OUTER JOIN
                         QuestionGroupMember ON QuestionGroup.QuestionGroupID = QuestionGroupMember.QuestionGroupID
GROUP BY Survey.SurveyID, Survey.SurveyTypeID, Survey.SurveyNM, Survey.SurveyShortNM, Survey.SurveyDS, Survey.CompletionMessage, Survey.ResponseNMTemplate, Survey.ReviewerAccountNM, 
                         Survey.AutoAssignFilter, Survey.StartDT, Survey.EndDT, Survey.ParentSurveyID, lu_SurveyType.SurveyTypeShortNM, lu_SurveyType.SurveyTypeNM, ParentSurvey.SurveyNM, ParentSurvey.SurveyShortNM






	COMMIT




