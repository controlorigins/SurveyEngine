CREATE PROC [dbo].[gsp_SurveyResponseAnswerReview_Insert] 
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
	
	INSERT INTO [dbo].[SurveyResponseAnswerReview] ([SurveyAnswerID], [ApplicationUserRoleID], [ReviewLevel], [ReviewStatusID], [ModifiedID], [ModifiedDT], [ModifiedComment])
	SELECT @SurveyAnswerID, @ApplicationUserRoleID, @ReviewLevel, @ReviewStatusID, @ModifiedID, @ModifiedDT, @ModifiedComment
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyResponseAnswerReviewID], [SurveyAnswerID], [ApplicationUserRoleID], [ReviewLevel], [ReviewStatusID], [ModifiedID], [ModifiedDT], [ModifiedComment]
	FROM   [dbo].[SurveyResponseAnswerReview]
	WHERE  [SurveyResponseAnswerReviewID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



