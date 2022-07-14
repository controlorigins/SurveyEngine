CREATE PROC [dbo].[gsp_lu_ReviewStatus_Update] 
    @ReviewStatusID int,
    @ReviewStatusNM nvarchar(50),
    @ReviewStatusDS nvarchar(MAX),
    @ApprovedFL bit,
    @CommentFL bit,
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[lu_ReviewStatus]
	SET    [ReviewStatusNM] = @ReviewStatusNM, [ReviewStatusDS] = @ReviewStatusDS, [ApprovedFL] = @ApprovedFL, [CommentFL] = @CommentFL, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [ReviewStatusID] = @ReviewStatusID
	
	-- Begin Return Select <- do not remove
	SELECT [ReviewStatusID], [ReviewStatusNM], [ReviewStatusDS], [ApprovedFL], [CommentFL], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[lu_ReviewStatus]
	WHERE  [ReviewStatusID] = @ReviewStatusID	
	-- End Return Select <- do not remove

	COMMIT



