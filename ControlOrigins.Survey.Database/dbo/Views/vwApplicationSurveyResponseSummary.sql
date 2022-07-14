CREATE VIEW [dbo].[vwApplicationSurveyResponseSummary]
AS
SELECT        TOP (100) PERCENT SR.SurveyResponseNM, SS.StatusNM, SR.StatusID, SR.DataSource, S.SurveyShortNM, COUNT(SRA.SurveyAnswerID) AS AnswerCount, COUNT(QGM.QuestionGroupMemberID) 
                         AS QuestionCount, SUM(CASE QA.CommentFl WHEN 'True' THEN 1 ELSE 0 END) AS CommentCount, SUM(CASE WHEN Q.ReviewRoleLevel = 2 AND QA.CommentFL = 'True' THEN 1 ELSE 0 END) 
                         AS PendingReviewCount, CASE WHEN COUNT(SRA.SurveyAnswerID) = 0 THEN 0 WHEN COUNT(QGM.QuestionGroupMemberID) = 0 THEN 0 ELSE COUNT(SRA.SurveyAnswerID) 
                         * 100 / COUNT(QGM.QuestionGroupMemberID) * 100 / 100 END AS PercentComplete, S.SurveyNM, SR.ModifiedDT, DATEDIFF(DAY, SR.ModifiedDT, GETDATE()) AS DaySinceModified, SR.AssignedUserID, 
                         SR.SurveyResponseID, SR.SurveyID, SR.ApplicationID, SR.ModifiedID, AU.FirstNM, AU.LastNM, AU.eMailAddress, AU.ApplicationUserID, 
                         SUM(Q.QuestionValue * QA.QuestionAnswerValue * QGM.QuestionWeight * QG.QuestionGroupWeight) AS SurveyResponseScore, A.ApplicationNM, A.ApplicationCD, A.ApplicationShortNM, AU.AccountNM, 
                         AU.SupervisorAccountNM
FROM            dbo.QuestionGroup AS QG LEFT OUTER JOIN
                         dbo.QuestionGroupMember AS QGM ON QG.QuestionGroupID = QGM.QuestionGroupID RIGHT OUTER JOIN
                         dbo.Application AS A INNER JOIN
                         dbo.SurveyStatus AS SS INNER JOIN
                         dbo.SurveyResponse AS SR INNER JOIN
                         dbo.Survey AS S ON SR.SurveyID = S.SurveyID ON SS.SurveyID = S.SurveyID AND SS.StatusID = SR.StatusID ON A.ApplicationID = SR.ApplicationID ON QG.SurveyID = S.SurveyID LEFT OUTER JOIN
                         dbo.ApplicationUser AS AU ON SR.AssignedUserID = AU.ApplicationUserID LEFT OUTER JOIN
                         dbo.QuestionAnswer AS QA RIGHT OUTER JOIN
                         dbo.SurveyResponseAnswer AS SRA ON QA.QuestionAnswerID = SRA.QuestionAnswerID LEFT OUTER JOIN
                         dbo.Question AS Q ON SRA.QuestionID = Q.QuestionID ON QGM.QuestionID = SRA.QuestionID AND SR.SurveyResponseID = SRA.SurveyResponseID
GROUP BY S.SurveyNM, SR.SurveyResponseID, SR.SurveyResponseNM, SR.ModifiedID, SR.SurveyID, SR.AssignedUserID, SR.StatusID, SR.DataSource, S.SurveyShortNM, SR.ApplicationID, SR.ModifiedDT, SS.StatusNM, 
                         AU.FirstNM, AU.LastNM, AU.eMailAddress, AU.ApplicationUserID, A.ApplicationNM, A.ApplicationCD, A.ApplicationShortNM, AU.AccountNM, AU.SupervisorAccountNM
