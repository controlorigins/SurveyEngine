CREATE PROC [dbo].[gsp_SurveyResponseHistory_Update] 
    @SurveyResponseHistoryID int,
    @ApplicationUserID int,
    @SurveyResponseID int,
    @SurveyResponseNM nvarchar(100),
    @StatusID int,
    @QuestionGroupID int,
    @UserNM nvarchar(50),
    @Answers ntext,
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[SurveyResponseHistory]
	SET    [ApplicationUserID] = @ApplicationUserID, [SurveyResponseID] = @SurveyResponseID, [SurveyResponseNM] = @SurveyResponseNM, [StatusID] = @StatusID, [QuestionGroupID] = @QuestionGroupID, [UserNM] = @UserNM, [Answers] = @Answers, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [SurveyResponseHistoryID] = @SurveyResponseHistoryID
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyResponseHistoryID], [ApplicationUserID], [SurveyResponseID], [SurveyResponseNM], [StatusID], [QuestionGroupID], [UserNM], [Answers], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[SurveyResponseHistory]
	WHERE  [SurveyResponseHistoryID] = @SurveyResponseHistoryID	
	-- End Return Select <- do not remove

	COMMIT



