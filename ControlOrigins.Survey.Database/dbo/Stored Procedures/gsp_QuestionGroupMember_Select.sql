CREATE PROC [dbo].[gsp_QuestionGroupMember_Select] 
    @QuestionGroupMemberID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [QuestionGroupMemberID], [QuestionGroupID], [QuestionID], [QuestionWeight], [DisplayOrder], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[QuestionGroupMember] 
	WHERE  ([QuestionGroupMemberID] = @QuestionGroupMemberID OR @QuestionGroupMemberID IS NULL) 

	COMMIT



