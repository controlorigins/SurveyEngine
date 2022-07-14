


CREATE PROC [dbo].[Question_SelectByQuestionShortNM]
    @QuestionShortNM nvarchar(75)
    
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN


SELECT        Question.QuestionID, Question.QuestionNM, Question.QuestionShortNM, Question.QuestionDS, Question.QuestionSort, Question.UnitOfMeasureID, Question.ReviewRoleLevel, Question.QuestionTypeID, 
                         Question.QuestionValue, Question.CommentFL, Question.FileData, Question.Keywords, Question.SurveyTypeID, lu_QuestionType.QuestionTypeCD, lu_QuestionType.QuestionTypeDS, 
                         lu_QuestionType.ControlName, lu_QuestionType.AnswerDataType, lu_SurveyType.SurveyTypeShortNM, lu_SurveyType.SurveyTypeNM, lu_UnitOfMeasure.UnitOfMeasureNM, Question.ModifiedID, 
                         Question.ModifiedDT, ApplicationUser.AccountNM
FROM            Question LEFT OUTER JOIN
                         ApplicationUser ON Question.ModifiedID = ApplicationUser.ApplicationUserID LEFT OUTER JOIN
                         lu_SurveyType ON Question.SurveyTypeID = lu_SurveyType.SurveyTypeID LEFT OUTER JOIN
                         lu_UnitOfMeasure ON Question.UnitOfMeasureID = lu_UnitOfMeasure.UnitOfMeasureID LEFT OUTER JOIN
                         lu_QuestionType ON Question.QuestionTypeID = lu_QuestionType.QuestionTypeID
	WHERE  (QuestionShortNM = @QuestionShortNM) 
ORDER BY Question.QuestionSort, Question.QuestionShortNM






	COMMIT






