CREATE PROC [dbo].[gsp_QuestionAnswer_Select] 
    @QuestionAnswerID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [QuestionAnswerID], [QuestionID], [QuestionAnswerShortNM], [QuestionAnswerNM], [QuestionAnswerValue], [QuestionAnswerDS], [QuestionAnswerSort], [CommentFL], [ActiveFL], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[QuestionAnswer] 
	WHERE  ([QuestionAnswerID] = @QuestionAnswerID OR @QuestionAnswerID IS NULL) 

	COMMIT



