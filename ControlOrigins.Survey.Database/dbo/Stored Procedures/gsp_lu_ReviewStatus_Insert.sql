CREATE PROC [dbo].[gsp_lu_ReviewStatus_Insert] 
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
	
	INSERT INTO [dbo].[lu_ReviewStatus] ([ReviewStatusNM], [ReviewStatusDS], [ApprovedFL], [CommentFL], [ModifiedID], [ModifiedDT])
	SELECT @ReviewStatusNM, @ReviewStatusDS, @ApprovedFL, @CommentFL, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [ReviewStatusID], [ReviewStatusNM], [ReviewStatusDS], [ApprovedFL], [CommentFL], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[lu_ReviewStatus]
	WHERE  [ReviewStatusID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



