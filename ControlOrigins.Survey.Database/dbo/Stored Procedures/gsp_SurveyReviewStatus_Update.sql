CREATE PROC [dbo].[gsp_SurveyReviewStatus_Update] 
    @SurveyReviewStatusID int,
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

	UPDATE [dbo].[SurveyReviewStatus]
	SET    [SurveyID] = @SurveyID, [ReviewStatusID] = @ReviewStatusID, [ReviewStatusNM] = @ReviewStatusNM, [ReviewStatusDS] = @ReviewStatusDS, [ApprovedFL] = @ApprovedFL, [CommentFL] = @CommentFL, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [SurveyReviewStatusID] = @SurveyReviewStatusID
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyReviewStatusID], [SurveyID], [ReviewStatusID], [ReviewStatusNM], [ReviewStatusDS], [ApprovedFL], [CommentFL], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[SurveyReviewStatus]
	WHERE  [SurveyReviewStatusID] = @SurveyReviewStatusID	
	-- End Return Select <- do not remove

	COMMIT



