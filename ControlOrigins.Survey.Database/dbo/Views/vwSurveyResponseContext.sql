CREATE VIEW [dbo].[vwSurveyResponseContext]
AS
SELECT        dbo.SurveyResponse.SurveyResponseID, dbo.SurveyResponse.SurveyResponseNM, dbo.SurveyResponse.SurveyID, dbo.SurveyResponse.ApplicationID, dbo.SurveyResponse.AssignedUserID, 
                         dbo.SurveyResponse.StatusID, dbo.SurveyResponse.DataSource, dbo.SurveyResponse.ModifiedID, dbo.SurveyResponse.ModifiedDT, dbo.Survey.SurveyNM, dbo.Survey.SurveyShortNM, dbo.Survey.SurveyDS, 
                         dbo.Application.ApplicationNM, dbo.Application.ApplicationCD, dbo.Application.ApplicationShortNM, dbo.ApplicationUser.eMailAddress, dbo.ApplicationUser.FirstNM, dbo.ApplicationUser.LastNM, 
                         dbo.ApplicationUser.AccountNM, dbo.ApplicationUser.CommentDS, dbo.ApplicationUser.SupervisorAccountNM, dbo.vwContextQuestionAnswer.QuestionID, dbo.vwContextQuestionAnswer.QuestionAnswerID, 
                         dbo.vwContextQuestionAnswer.QuestionNM, dbo.vwContextQuestionAnswer.QuestionAnswerNM, dbo.ApplicationUser.ApplicationUserID
FROM            dbo.SurveyResponse INNER JOIN
                         dbo.Survey ON dbo.SurveyResponse.SurveyID = dbo.Survey.SurveyID INNER JOIN
                         dbo.Application ON dbo.SurveyResponse.ApplicationID = dbo.Application.ApplicationID LEFT OUTER JOIN
                         dbo.vwContextQuestionAnswer ON dbo.SurveyResponse.SurveyResponseID = dbo.vwContextQuestionAnswer.SurveyResponseID LEFT OUTER JOIN
                         dbo.ApplicationUser ON dbo.SurveyResponse.AssignedUserID = dbo.ApplicationUser.ApplicationUserID
