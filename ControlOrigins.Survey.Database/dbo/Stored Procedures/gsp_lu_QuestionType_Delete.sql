CREATE PROC [dbo].[gsp_lu_QuestionType_Delete] 
    @QuestionTypeID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[lu_QuestionType]
	WHERE  [QuestionTypeID] = @QuestionTypeID

	COMMIT



