CREATE PROC [dbo].[gsp_lu_QuestionType_Select] 
    @QuestionTypeID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [QuestionTypeID], [QuestionTypeCD], [QuestionTypeDS], [ControlName], [AnswerDataType], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[lu_QuestionType] 
	WHERE  ([QuestionTypeID] = @QuestionTypeID OR @QuestionTypeID IS NULL) 

	COMMIT



