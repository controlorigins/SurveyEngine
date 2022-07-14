

CREATE PROC [dbo].[usp_Question_SelectByQuestionShortNM] 
    @QuestionShortNM nvarchar(75)
    
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [QuestionID], 
	       [SurveyTypeID], 
	       [QuestionShortNM], 
	       [QuestionNM], 
	       [QuestionDS], 
	       [QuestionSort], 
	       [ReviewRoleLevel], 
	       [QuestionTypeID], 
	       [CommentFL], 
	       [QuestionValue], 
	       [UnitOfMeasureID], 
	       [ModifiedID], 
	       [ModifiedDT] 
	FROM   [dbo].[Question] 
	WHERE  ([QuestionShortNM] = @QuestionShortNM) 

	COMMIT





