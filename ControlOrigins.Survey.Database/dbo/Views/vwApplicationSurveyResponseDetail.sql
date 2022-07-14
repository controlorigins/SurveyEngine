CREATE VIEW [dbo].[vwApplicationSurveyResponseDetail]
AS
SELECT     dbo.SurveyResponseAnswer.SurveyAnswerID, dbo.SurveyResponseAnswer.AnswerType, dbo.SurveyResponseAnswer.AnswerQuantity, 
                      dbo.SurveyResponseAnswer.AnswerDate, dbo.SurveyResponseAnswer.AnswerComment, dbo.SurveyResponseAnswer.ModifiedComment, dbo.Question.QuestionID, 
                      dbo.Question.QuestionNM, dbo.Question.QuestionDS, dbo.Question.QuestionSort, dbo.Question.ReviewRoleLevel, dbo.Question.CommentFL AS QuestionCommentFL, 
                      dbo.Question.QuestionValue, dbo.lu_QuestionType.QuestionTypeID, dbo.lu_QuestionType.QuestionTypeCD, dbo.lu_QuestionType.QuestionTypeDS, 
                      dbo.lu_QuestionType.AnswerDataType, dbo.QuestionAnswer.QuestionAnswerID, dbo.QuestionAnswer.QuestionAnswerSort, 
                      dbo.QuestionAnswer.QuestionAnswerShortNM, dbo.QuestionAnswer.QuestionAnswerNM, dbo.QuestionAnswer.QuestionAnswerValue, 
                      dbo.QuestionAnswer.QuestionAnswerDS, dbo.QuestionAnswer.CommentFL AS QuestionAnswerCommentFL, dbo.QuestionAnswer.ActiveFL, 
                      dbo.SurveyResponse.SurveyResponseID, dbo.SurveyResponse.SurveyResponseNM, dbo.SurveyResponse.DataSource, dbo.Survey.SurveyID, 
                      dbo.Survey.UseQuestionGroupsFL, dbo.Survey.SurveyNM, dbo.Survey.SurveyShortNM, dbo.Survey.SurveyDS, dbo.Survey.CompletionMessage, 
                      dbo.Survey.ResponseNMTemplate, dbo.Survey.ReviewerAccountNM, dbo.Survey.AutoAssignFilter, dbo.Survey.StartDT, dbo.Survey.EndDT, 
                      dbo.Application.ApplicationID, dbo.Application.ApplicationNM, dbo.Application.ApplicationCD, dbo.Application.ApplicationShortNM, dbo.Application.ApplicationDS, 
                      dbo.Application.MenuOrder, dbo.lu_SurveyType.SurveyTypeID, dbo.lu_SurveyType.SurveyTypeShortNM, dbo.lu_SurveyType.SurveyTypeNM, 
                      dbo.lu_SurveyType.SurveyTypeDS, dbo.lu_SurveyType.SurveyTypeComment, dbo.lu_SurveyType.MutiSequenceFL, dbo.lu_ApplicationType.ApplicationTypeID, 
                      dbo.lu_ApplicationType.ApplicationTypeNM, dbo.lu_ApplicationType.ApplicationTypeDS, dbo.Question.QuestionShortNM, dbo.SurveyStatus.StatusNM, 
                      dbo.SurveyStatus.StatusDS, dbo.SurveyStatus.EmailTemplate, dbo.SurveyStatus.EmailSubjectTemplate, dbo.ApplicationUser.FirstNM, dbo.ApplicationUser.LastNM, 
                      dbo.ApplicationUser.eMailAddress, dbo.ApplicationUser.CommentDS, dbo.ApplicationUser.AccountNM, dbo.ApplicationUser.SupervisorAccountNM, 
                      dbo.ApplicationUser.LastLoginDT, dbo.ApplicationUser.LastLoginLocation, dbo.SurveyResponseSequence.SurveyResponseSequenceID, 
                      dbo.SurveyResponseSequence.SequenceNumber, dbo.SurveyResponseSequence.SequenceText, dbo.QuestionGroupMember.QuestionGroupMemberID, 
                      dbo.QuestionGroupMember.QuestionWeight, dbo.QuestionGroupMember.DisplayOrder, dbo.QuestionGroup.QuestionGroupID, dbo.QuestionGroup.GroupOrder, 
                      dbo.QuestionGroup.QuestionGroupShortNM, dbo.QuestionGroup.QuestionGroupNM, dbo.QuestionGroup.QuestionGroupDS, dbo.QuestionGroup.QuestionGroupWeight, 
                      dbo.QuestionGroup.GroupHeader, dbo.QuestionGroup.GroupFooter, dbo.QuestionGroup.DependentQuestionGroupID, dbo.QuestionGroup.DependentMinScore, 
                      dbo.QuestionGroup.DependentMaxScore
FROM         dbo.SurveyResponseAnswer INNER JOIN
                      dbo.Question ON dbo.SurveyResponseAnswer.QuestionID = dbo.Question.QuestionID INNER JOIN
                      dbo.lu_QuestionType ON dbo.Question.QuestionTypeID = dbo.lu_QuestionType.QuestionTypeID INNER JOIN
                      dbo.QuestionAnswer ON dbo.SurveyResponseAnswer.QuestionAnswerID = dbo.QuestionAnswer.QuestionAnswerID INNER JOIN
                      dbo.SurveyResponse ON dbo.SurveyResponseAnswer.SurveyResponseID = dbo.SurveyResponse.SurveyResponseID INNER JOIN
                      dbo.Survey ON dbo.SurveyResponse.SurveyID = dbo.Survey.SurveyID INNER JOIN
                      dbo.Application ON dbo.SurveyResponse.ApplicationID = dbo.Application.ApplicationID INNER JOIN
                      dbo.lu_SurveyType ON dbo.Survey.SurveyTypeID = dbo.lu_SurveyType.SurveyTypeID INNER JOIN
                      dbo.lu_ApplicationType ON dbo.Application.ApplicationTypeID = dbo.lu_ApplicationType.ApplicationTypeID INNER JOIN
                      dbo.SurveyStatus ON dbo.Survey.SurveyID = dbo.SurveyStatus.SurveyID AND dbo.SurveyResponse.StatusID = dbo.SurveyStatus.StatusID INNER JOIN
                      dbo.SurveyResponseSequence ON dbo.SurveyResponseAnswer.SurveyResponseID = dbo.SurveyResponseSequence.SurveyResponseID AND 
                      dbo.SurveyResponseAnswer.SequenceNumber = dbo.SurveyResponseSequence.SequenceNumber AND 
                      dbo.SurveyResponse.SurveyResponseID = dbo.SurveyResponseSequence.SurveyResponseID INNER JOIN
                      dbo.QuestionGroup ON dbo.Survey.SurveyID = dbo.QuestionGroup.SurveyID INNER JOIN
                      dbo.QuestionGroupMember ON dbo.Question.QuestionID = dbo.QuestionGroupMember.QuestionID AND 
                      dbo.QuestionGroup.QuestionGroupID = dbo.QuestionGroupMember.QuestionGroupID LEFT OUTER JOIN
                      dbo.ApplicationUser ON dbo.SurveyResponse.AssignedUserID = dbo.ApplicationUser.ApplicationUserID

