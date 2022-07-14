CREATE VIEW [dbo].[vwSurveyResponseSummary]
AS
SELECT        dbo.SurveyResponse.SurveyResponseID, dbo.SurveyResponse.SurveyResponseNM, dbo.SurveyResponse.SurveyID, dbo.SurveyResponse.ApplicationID, dbo.SurveyResponse.AssignedUserID, 
                         dbo.SurveyResponse.AssignedUserID AS ApplicationUserID, dbo.SurveyResponse.StatusID, dbo.SurveyResponse.DataSource, dbo.Application.ApplicationNM, dbo.Application.ApplicationCD, 
                         dbo.Application.ApplicationShortNM, dbo.SurveyStatus.StatusNM, dbo.Survey.SurveyNM, dbo.Survey.SurveyShortNM, dbo.ApplicationUser.FirstNM, dbo.ApplicationUser.LastNM, 
                         dbo.ApplicationUser.eMailAddress, dbo.ApplicationUser.AccountNM, dbo.ApplicationUser.LastLoginDT, dbo.ApplicationUser.LastLoginLocation, dbo.SurveyResponse.ModifiedID, 
                         dbo.SurveyResponse.ModifiedDT, COUNT(DISTINCT dbo.SurveyResponseAnswer.SurveyAnswerID) AS AnswerCount, COUNT(DISTINCT dbo.QuestionGroupMember.QuestionGroupMemberID) AS QuestionCount, 
                         SUM(CASE QuestionAnswer.CommentFl WHEN 'True' THEN 1 ELSE 0 END) AS CommentCount, DATEDIFF(DAY, dbo.SurveyResponse.ModifiedDT, GETDATE()) AS DaySinceModified, 
                         SUM(dbo.Question.QuestionValue * dbo.QuestionAnswer.QuestionAnswerValue * dbo.QuestionGroupMember.QuestionWeight * dbo.QuestionGroup.QuestionGroupWeight) AS SurveyResponseScore, 
                         AVG(dbo.Question.QuestionValue * dbo.QuestionAnswer.QuestionAnswerValue) AS AverageQuestionScore
FROM            dbo.SurveyResponse INNER JOIN
                         dbo.Application ON dbo.SurveyResponse.ApplicationID = dbo.Application.ApplicationID LEFT OUTER JOIN
                         dbo.Survey LEFT OUTER JOIN
                         dbo.QuestionGroup LEFT OUTER JOIN
                         dbo.SurveyResponseAnswer LEFT OUTER JOIN
                         dbo.Question ON dbo.SurveyResponseAnswer.QuestionID = dbo.Question.QuestionID LEFT OUTER JOIN
                         dbo.QuestionAnswer INNER JOIN
                         dbo.QuestionGroupMember ON dbo.QuestionAnswer.QuestionID = dbo.QuestionGroupMember.QuestionID ON dbo.SurveyResponseAnswer.QuestionAnswerID = dbo.QuestionAnswer.QuestionAnswerID ON 
                         dbo.QuestionGroup.QuestionGroupID = dbo.QuestionGroupMember.QuestionGroupID ON dbo.Survey.SurveyID = dbo.QuestionGroup.SurveyID ON dbo.SurveyResponse.SurveyID = dbo.Survey.SurveyID AND 
                         dbo.SurveyResponse.SurveyResponseID = dbo.SurveyResponseAnswer.SurveyResponseID LEFT OUTER JOIN
                         dbo.ApplicationUser ON dbo.SurveyResponse.AssignedUserID = dbo.ApplicationUser.ApplicationUserID LEFT OUTER JOIN
                         dbo.SurveyStatus ON dbo.SurveyResponse.StatusID = dbo.SurveyStatus.StatusID AND dbo.SurveyResponse.SurveyID = dbo.SurveyStatus.SurveyID
GROUP BY dbo.SurveyResponse.SurveyResponseID, dbo.SurveyResponse.SurveyResponseNM, dbo.SurveyResponse.SurveyID, dbo.SurveyResponse.ApplicationID, dbo.SurveyResponse.AssignedUserID, 
                         dbo.SurveyResponse.StatusID, dbo.SurveyResponse.DataSource, dbo.Application.ApplicationNM, dbo.Application.ApplicationCD, dbo.Application.ApplicationShortNM, dbo.SurveyStatus.StatusNM, 
                         dbo.Survey.SurveyNM, dbo.Survey.SurveyShortNM, dbo.ApplicationUser.FirstNM, dbo.ApplicationUser.LastNM, dbo.ApplicationUser.eMailAddress, dbo.ApplicationUser.AccountNM, 
                         dbo.ApplicationUser.LastLoginDT, dbo.ApplicationUser.LastLoginLocation, dbo.SurveyResponse.ModifiedID, dbo.SurveyResponse.ModifiedDT
