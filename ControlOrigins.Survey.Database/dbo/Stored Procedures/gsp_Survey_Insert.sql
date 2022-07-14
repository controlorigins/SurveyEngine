CREATE PROC [dbo].[gsp_Survey_Insert] 
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
	
	INSERT INTO [dbo].[Survey] ([SurveyTypeID], [UseQuestionGroupsFL], [SurveyNM], [SurveyShortNM], [SurveyDS], [CompletionMessage], [ResponseNMTemplate], [ReviewerAccountNM], [AutoAssignFilter], [StartDT], [EndDT], [ParentSurveyID], [ModifiedID], [ModifiedDT])
	SELECT @SurveyTypeID, @UseQuestionGroupsFL, @SurveyNM, @SurveyShortNM, @SurveyDS, @CompletionMessage, @ResponseNMTemplate, @ReviewerAccountNM, @AutoAssignFilter, @StartDT, @EndDT, @ParentSurveyID, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyID], [SurveyTypeID], [UseQuestionGroupsFL], [SurveyNM], [SurveyShortNM], [SurveyDS], [CompletionMessage], [ResponseNMTemplate], [ReviewerAccountNM], [AutoAssignFilter], [StartDT], [EndDT], [ParentSurveyID], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[Survey]
	WHERE  [SurveyID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



