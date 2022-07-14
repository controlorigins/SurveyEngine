CREATE PROC [dbo].[gsp_QuestionGroupMember_Insert] 
    @QuestionGroupID int,
    @QuestionID int,
    @QuestionWeight decimal(18, 4),
    @DisplayOrder int,
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[QuestionGroupMember] ([QuestionGroupID], [QuestionID], [QuestionWeight], [DisplayOrder], [ModifiedID], [ModifiedDT])
	SELECT @QuestionGroupID, @QuestionID, @QuestionWeight, @DisplayOrder, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [QuestionGroupMemberID], [QuestionGroupID], [QuestionID], [QuestionWeight], [DisplayOrder], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[QuestionGroupMember]
	WHERE  [QuestionGroupMemberID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



