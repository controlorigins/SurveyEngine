CREATE VIEW [dbo].[vwSurveyQuestions]
AS
SELECT        TOP (100) PERCENT dbo.QuestionGroupMember.QuestionWeight, dbo.QuestionGroupMember.DisplayOrder, dbo.Question.QuestionID, dbo.Question.QuestionNM, dbo.Question.QuestionShortNM, 
                         dbo.Question.QuestionDS, dbo.Question.QuestionSort, dbo.Question.UnitOfMeasureID, dbo.Question.ReviewRoleLevel, dbo.Question.QuestionTypeID, dbo.Question.QuestionValue, dbo.Question.CommentFL, 
                         dbo.lu_QuestionType.QuestionTypeCD, dbo.lu_QuestionType.QuestionTypeDS, dbo.lu_QuestionType.ControlName, dbo.lu_QuestionType.AnswerDataType, dbo.Survey.SurveyID, 
                         dbo.Survey.UseQuestionGroupsFL, dbo.Survey.SurveyNM, dbo.Survey.SurveyShortNM, dbo.Survey.SurveyDS, dbo.Survey.CompletionMessage, dbo.Survey.ResponseNMTemplate, 
                         dbo.Survey.ReviewerAccountNM, dbo.Survey.AutoAssignFilter, dbo.Survey.StartDT, dbo.Survey.EndDT, dbo.QuestionGroup.QuestionGroupID, dbo.QuestionGroup.GroupOrder, 
                         dbo.QuestionGroup.QuestionGroupShortNM, dbo.QuestionGroup.QuestionGroupNM, dbo.QuestionGroup.QuestionGroupDS, dbo.QuestionGroup.QuestionGroupWeight, dbo.QuestionGroup.GroupHeader, 
                         dbo.QuestionGroup.GroupFooter, dbo.QuestionAnswer.QuestionAnswerID, dbo.QuestionAnswer.QuestionAnswerShortNM, dbo.QuestionAnswer.QuestionAnswerNM, dbo.QuestionAnswer.QuestionAnswerValue, 
                         dbo.QuestionAnswer.QuestionAnswerDS, dbo.QuestionAnswer.QuestionAnswerSort, dbo.QuestionAnswer.CommentFL AS QuestionAnswerCommentFL, dbo.QuestionAnswer.ActiveFL
FROM            dbo.Question INNER JOIN
                         dbo.QuestionGroupMember ON dbo.Question.QuestionID = dbo.QuestionGroupMember.QuestionID INNER JOIN
                         dbo.QuestionGroup ON dbo.QuestionGroupMember.QuestionGroupID = dbo.QuestionGroup.QuestionGroupID INNER JOIN
                         dbo.lu_QuestionType ON dbo.Question.QuestionTypeID = dbo.lu_QuestionType.QuestionTypeID INNER JOIN
                         dbo.Survey ON dbo.QuestionGroup.SurveyID = dbo.Survey.SurveyID INNER JOIN
                         dbo.QuestionAnswer ON dbo.Question.QuestionID = dbo.QuestionAnswer.QuestionID
WHERE        (dbo.Survey.SurveyID IN
                             (SELECT        SurveyID
                               FROM            dbo.ApplicationSurvey))
