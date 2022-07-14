CREATE PROC [dbo].[gsp_QuestionAnswer_Insert] 
    @QuestionID int,
    @QuestionAnswerShortNM nvarchar(50),
    @QuestionAnswerNM nvarchar(MAX),
    @QuestionAnswerValue int,
    @QuestionAnswerDS nvarchar(MAX),
    @QuestionAnswerSort int,
    @CommentFL bit,
    @ActiveFL bit,
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[QuestionAnswer] ([QuestionID], [QuestionAnswerShortNM], [QuestionAnswerNM], [QuestionAnswerValue], [QuestionAnswerDS], [QuestionAnswerSort], [CommentFL], [ActiveFL], [ModifiedID], [ModifiedDT])
	SELECT @QuestionID, @QuestionAnswerShortNM, @QuestionAnswerNM, @QuestionAnswerValue, @QuestionAnswerDS, @QuestionAnswerSort, @CommentFL, @ActiveFL, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [QuestionAnswerID], [QuestionID], [QuestionAnswerShortNM], [QuestionAnswerNM], [QuestionAnswerValue], [QuestionAnswerDS], [QuestionAnswerSort], [CommentFL], [ActiveFL], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[QuestionAnswer]
	WHERE  [QuestionAnswerID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



