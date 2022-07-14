CREATE PROC [dbo].[usp_QuestionAnswer_SelectByQuestionID] 
    @QuestionID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [QuestionAnswerID], 
	       [QuestionID], 
	       [QuestionAnswerShortNM], 
	       [QuestionAnswerNM], 
	       [QuestionAnswerValue], 
	       [QuestionAnswerDS], 
	       [QuestionAnswerSort], 
	       [CommentFL], 
	       [ActiveFL], 
	       [ModifiedID], 
	       [ModifiedDT] 
	FROM   [dbo].[QuestionAnswer] 
	WHERE  ([QuestionID] = @QuestionID ) 
	ORDER BY [QuestionAnswerSort]

	COMMIT



