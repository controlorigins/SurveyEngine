CREATE PROC [dbo].[gsp_QuestionGroup_Delete] 
    @QuestionGroupID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[QuestionGroup]
	WHERE  [QuestionGroupID] = @QuestionGroupID

	COMMIT



