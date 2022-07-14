CREATE PROC [dbo].[gsp_SurveyResponseHistory_Insert] 
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
	
	INSERT INTO [dbo].[SurveyResponseHistory] ([ApplicationUserID], [SurveyResponseID], [SurveyResponseNM], [StatusID], [QuestionGroupID], [UserNM], [Answers], [ModifiedID], [ModifiedDT])
	SELECT @ApplicationUserID, @SurveyResponseID, @SurveyResponseNM, @StatusID, @QuestionGroupID, @UserNM, @Answers, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyResponseHistoryID], [ApplicationUserID], [SurveyResponseID], [SurveyResponseNM], [StatusID], [QuestionGroupID], [UserNM], [Answers], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[SurveyResponseHistory]
	WHERE  [SurveyResponseHistoryID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



