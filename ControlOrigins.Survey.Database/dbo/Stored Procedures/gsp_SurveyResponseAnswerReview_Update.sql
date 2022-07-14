CREATE PROC [dbo].[gsp_SurveyResponseAnswerReview_Update] 
    @SurveyResponseAnswerReviewID int,
    @SurveyAnswerID int,
    @ApplicationUserRoleID int,
    @ReviewLevel int,
    @ReviewStatusID int,
    @ModifiedID int,
    @ModifiedDT datetime,
    @ModifiedComment nvarchar(MAX)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[SurveyResponseAnswerReview]
	SET    [SurveyAnswerID] = @SurveyAnswerID, [ApplicationUserRoleID] = @ApplicationUserRoleID, [ReviewLevel] = @ReviewLevel, [ReviewStatusID] = @ReviewStatusID, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT, [ModifiedComment] = @ModifiedComment
	WHERE  [SurveyResponseAnswerReviewID] = @SurveyResponseAnswerReviewID
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyResponseAnswerReviewID], [SurveyAnswerID], [ApplicationUserRoleID], [ReviewLevel], [ReviewStatusID], [ModifiedID], [ModifiedDT], [ModifiedComment]
	FROM   [dbo].[SurveyResponseAnswerReview]
	WHERE  [SurveyResponseAnswerReviewID] = @SurveyResponseAnswerReviewID	
	-- End Return Select <- do not remove

	COMMIT



