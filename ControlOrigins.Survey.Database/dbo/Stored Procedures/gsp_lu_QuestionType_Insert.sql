CREATE PROC [dbo].[gsp_lu_QuestionType_Insert] 
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
	
	INSERT INTO [dbo].[lu_QuestionType] ([QuestionTypeCD], [QuestionTypeDS], [ControlName], [AnswerDataType], [ModifiedID], [ModifiedDT])
	SELECT @QuestionTypeCD, @QuestionTypeDS, @ControlName, @AnswerDataType, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [QuestionTypeID], [QuestionTypeCD], [QuestionTypeDS], [ControlName], [AnswerDataType], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[lu_QuestionType]
	WHERE  [QuestionTypeID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



