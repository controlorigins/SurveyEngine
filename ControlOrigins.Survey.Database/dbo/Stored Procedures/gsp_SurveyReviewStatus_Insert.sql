CREATE PROC [dbo].[gsp_SurveyReviewStatus_Insert] 
    @SurveyID int,
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
	
	INSERT INTO [dbo].[SurveyReviewStatus] ([SurveyID], [ReviewStatusID], [ReviewStatusNM], [ReviewStatusDS], [ApprovedFL], [CommentFL], [ModifiedID], [ModifiedDT])
	SELECT @SurveyID, @ReviewStatusID, @ReviewStatusNM, @ReviewStatusDS, @ApprovedFL, @CommentFL, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyReviewStatusID], [SurveyID], [ReviewStatusID], [ReviewStatusNM], [ReviewStatusDS], [ApprovedFL], [CommentFL], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[SurveyReviewStatus]
	WHERE  [SurveyReviewStatusID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



