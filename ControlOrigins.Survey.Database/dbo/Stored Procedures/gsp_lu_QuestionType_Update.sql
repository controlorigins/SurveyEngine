CREATE PROC [dbo].[gsp_lu_QuestionType_Update] 
    @QuestionTypeID int,
    @QuestionTypeCD nvarchar(255),
    @QuestionTypeDS nvarchar(MAX),
    @ControlName nvarchar(255),
    @AnswerDataType nvarchar(255),
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[lu_QuestionType]
	SET    [QuestionTypeCD] = @QuestionTypeCD, [QuestionTypeDS] = @QuestionTypeDS, [ControlName] = @ControlName, [AnswerDataType] = @AnswerDataType, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [QuestionTypeID] = @QuestionTypeID
	
	-- Begin Return Select <- do not remove
	SELECT [QuestionTypeID], [QuestionTypeCD], [QuestionTypeDS], [ControlName], [AnswerDataType], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[lu_QuestionType]
	WHERE  [QuestionTypeID] = @QuestionTypeID	
	-- End Return Select <- do not remove

	COMMIT



