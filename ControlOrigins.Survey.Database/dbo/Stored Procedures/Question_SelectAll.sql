

-- ==========================================================================================
-- Entity Name:	Question_SelectAll
-- Create date:	10/16/2015 12:12:44 PM
-- Description:	This stored procedure is intended for selecting all rows from Question table
-- ==========================================================================================
CREATE Procedure [dbo].[Question_SelectAll]
As
Begin

SELECT        Question.QuestionID, Question.QuestionNM, Question.QuestionShortNM, Question.QuestionDS, Question.QuestionSort, Question.UnitOfMeasureID, Question.ReviewRoleLevel, Question.QuestionTypeID, 
                         Question.QuestionValue, Question.CommentFL, Question.FileData, Question.Keywords, Question.SurveyTypeID, lu_QuestionType.QuestionTypeCD, lu_QuestionType.QuestionTypeDS, 
                         lu_QuestionType.ControlName, lu_QuestionType.AnswerDataType, lu_SurveyType.SurveyTypeShortNM, lu_SurveyType.SurveyTypeNM, lu_UnitOfMeasure.UnitOfMeasureNM, Question.ModifiedID, 
                         Question.ModifiedDT, ApplicationUser.AccountNM
FROM            Question LEFT OUTER JOIN
                         ApplicationUser ON Question.ModifiedID = ApplicationUser.ApplicationUserID LEFT OUTER JOIN
                         lu_SurveyType ON Question.SurveyTypeID = lu_SurveyType.SurveyTypeID LEFT OUTER JOIN
                         lu_UnitOfMeasure ON Question.UnitOfMeasureID = lu_UnitOfMeasure.UnitOfMeasureID LEFT OUTER JOIN
                         lu_QuestionType ON Question.QuestionTypeID = lu_QuestionType.QuestionTypeID
ORDER BY Question.QuestionSort, Question.QuestionShortNM


End

