CREATE PROC [dbo].[gsp_QuestionGroupMember_Delete] 
    @QuestionGroupMemberID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[QuestionGroupMember]
	WHERE  [QuestionGroupMemberID] = @QuestionGroupMemberID

	COMMIT



