CREATE PROC [dbo].[gsp_QuestionAnswer_Update] 
    @QuestionAnswerID int,
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

	UPDATE [dbo].[QuestionAnswer]
	SET    [QuestionID] = @QuestionID, [QuestionAnswerShortNM] = @QuestionAnswerShortNM, [QuestionAnswerNM] = @QuestionAnswerNM, [QuestionAnswerValue] = @QuestionAnswerValue, [QuestionAnswerDS] = @QuestionAnswerDS, [QuestionAnswerSort] = @QuestionAnswerSort, [CommentFL] = @CommentFL, [ActiveFL] = @ActiveFL, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [QuestionAnswerID] = @QuestionAnswerID
	
	-- Begin Return Select <- do not remove
	SELECT [QuestionAnswerID], [QuestionID], [QuestionAnswerShortNM], [QuestionAnswerNM], [QuestionAnswerValue], [QuestionAnswerDS], [QuestionAnswerSort], [CommentFL], [ActiveFL], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[QuestionAnswer]
	WHERE  [QuestionAnswerID] = @QuestionAnswerID	
	-- End Return Select <- do not remove

	COMMIT



