CREATE PROC [dbo].[gsp_Survey_Update] 
    @SurveyID int,
    @SurveyTypeID int,
    @UseQuestionGroupsFL bit,
    @SurveyNM nvarchar(50),
    @SurveyShortNM nvarchar(50),
    @SurveyDS nvarchar(MAX),
    @CompletionMessage nvarchar(MAX),
    @ResponseNMTemplate nvarchar(100),
    @ReviewerAccountNM nvarchar(50),
    @AutoAssignFilter nvarchar(MAX),
    @StartDT date,
    @EndDT date,
    @ParentSurveyID int,
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Survey]
	SET    [SurveyTypeID] = @SurveyTypeID, [UseQuestionGroupsFL] = @UseQuestionGroupsFL, [SurveyNM] = @SurveyNM, [SurveyShortNM] = @SurveyShortNM, [SurveyDS] = @SurveyDS, [CompletionMessage] = @CompletionMessage, [ResponseNMTemplate] = @ResponseNMTemplate, [ReviewerAccountNM] = @ReviewerAccountNM, [AutoAssignFilter] = @AutoAssignFilter, [StartDT] = @StartDT, [EndDT] = @EndDT, [ParentSurveyID] = @ParentSurveyID, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [SurveyID] = @SurveyID
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyID], [SurveyTypeID], [UseQuestionGroupsFL], [SurveyNM], [SurveyShortNM], [SurveyDS], [CompletionMessage], [ResponseNMTemplate], [ReviewerAccountNM], [AutoAssignFilter], [StartDT], [EndDT], [ParentSurveyID], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[Survey]
	WHERE  [SurveyID] = @SurveyID	
	-- End Return Select <- do not remove

	COMMIT



