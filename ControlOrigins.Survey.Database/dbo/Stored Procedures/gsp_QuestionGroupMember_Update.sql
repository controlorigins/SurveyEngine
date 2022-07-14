CREATE PROC [dbo].[gsp_QuestionGroupMember_Update] 
    @QuestionGroupMemberID int,
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

	UPDATE [dbo].[QuestionGroupMember]
	SET    [QuestionGroupID] = @QuestionGroupID, [QuestionID] = @QuestionID, [QuestionWeight] = @QuestionWeight, [DisplayOrder] = @DisplayOrder, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [QuestionGroupMemberID] = @QuestionGroupMemberID
	
	-- Begin Return Select <- do not remove
	SELECT [QuestionGroupMemberID], [QuestionGroupID], [QuestionID], [QuestionWeight], [DisplayOrder], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[QuestionGroupMember]
	WHERE  [QuestionGroupMemberID] = @QuestionGroupMemberID	
	-- End Return Select <- do not remove

	COMMIT



