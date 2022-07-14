CREATE PROC [dbo].[gsp_QuestionAnswer_Delete] 
    @QuestionAnswerID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[QuestionAnswer]
	WHERE  [QuestionAnswerID] = @QuestionAnswerID

	COMMIT



