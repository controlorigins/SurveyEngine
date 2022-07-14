CREATE PROC [dbo].[usp_SurveyResponseCurrentAnswers_SelectBySurveyResponseID] 
    @SurveyResponseID INT,
    @RoleReviewLevel  INT,
    @CommentFL BIT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN


SELECT     dbo.SurveyResponseSequence.SequenceText, 
           dbo.SurveyResponseSequence.SequenceNumber, 
           dbo.SurveyResponseAnswer.SurveyAnswerID, 
           dbo.SurveyResponseAnswer.SurveyResponseID, 
           dbo.SurveyResponseAnswer.AnswerType, 
           dbo.SurveyResponseAnswer.AnswerQuantity, 
           dbo.SurveyResponseAnswer.AnswerDate, 
           dbo.SurveyResponseAnswer.AnswerComment, 
           dbo.SurveyResponseAnswer.ModifiedComment, 
           dbo.SurveyResponseAnswer.ModifiedID, 
           dbo.SurveyResponseAnswer.ModifiedDT, 
           dbo.Question.QuestionID, 
           dbo.Question.QuestionShortNM, 
           dbo.Question.QuestionNM, 
           dbo.Question.QuestionDS, 
           dbo.Question.QuestionSort, 
           dbo.Question.ReviewRoleLevel, 
           dbo.Question.CommentFL AS QuestionCommentFL, 
           dbo.Question.QuestionValue, 
           dbo.Question.UnitOfMeasureID, 
           dbo.QuestionAnswer.QuestionAnswerShortNM,
           dbo.QuestionAnswer.QuestionAnswerNM, 
           dbo.QuestionAnswer.QuestionAnswerValue, 
           dbo.QuestionAnswer.QuestionAnswerDS, 
           dbo.QuestionAnswer.QuestionAnswerSort, 
           dbo.QuestionAnswer.CommentFL AS QuestionAnswerCommentFL, 
           dbo.QuestionAnswer.ActiveFL, 
           dbo.lu_QuestionType.QuestionTypeID, 
           dbo.lu_QuestionType.QuestionTypeCD, 
           dbo.lu_QuestionType.QuestionTypeDS, 
           dbo.lu_QuestionType.ControlName, 
           dbo.lu_QuestionType.AnswerDataType, 
           dbo.QuestionAnswer.QuestionAnswerID
FROM       dbo.Question INNER JOIN
           dbo.QuestionAnswer ON dbo.Question.QuestionID = dbo.QuestionAnswer.QuestionID INNER JOIN
           dbo.lu_QuestionType ON dbo.Question.QuestionTypeID = dbo.lu_QuestionType.QuestionTypeID INNER JOIN
           dbo.SurveyResponseAnswer ON dbo.Question.QuestionID = dbo.SurveyResponseAnswer.QuestionID AND 
           dbo.QuestionAnswer.QuestionAnswerID = dbo.SurveyResponseAnswer.QuestionAnswerID INNER JOIN
           dbo.SurveyResponseSequence ON dbo.SurveyResponseAnswer.SurveyResponseID = dbo.SurveyResponseSequence.SurveyResponseID AND 
           dbo.SurveyResponseAnswer.SequenceNumber = dbo.SurveyResponseSequence.SequenceNumber	  
 where     dbo.SurveyResponseSequence.SurveyResponseID = @SurveyResponseID  and 
           dbo.Question.ReviewRoleLevel <= @RoleReviewLevel and
           ( dbo.QuestionAnswer.CommentFL = @CommentFL or @CommentFL is null) 


COMMIT



