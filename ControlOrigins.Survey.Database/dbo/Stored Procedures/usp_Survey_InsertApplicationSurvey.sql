

CREATE PROC [dbo].[usp_Survey_InsertApplicationSurvey] 
    @ApplicationID int, 
    @DefaultRoleID int, 
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
	
	INSERT INTO [dbo].[Survey] ([SurveyTypeID], [UseQuestionGroupsFL], [SurveyNM], [SurveyShortNM], [SurveyDS], [CompletionMessage], [ResponseNMTemplate], [ReviewerAccountNM], [AutoAssignFilter], [StartDT], [EndDT], [ParentSurveyID], [ModifiedID], [ModifiedDT])
	SELECT @SurveyTypeID, @UseQuestionGroupsFL, @SurveyNM, @SurveyShortNM, @SurveyDS, @CompletionMessage, @ResponseNMTemplate, @ReviewerAccountNM, @AutoAssignFilter, @StartDT, @EndDT, @ParentSurveyID, @ModifiedID, @ModifiedDT
	
	Select @SurveyID = SCOPE_IDENTITY()
	
	exec gsp_ApplicationSurvey_Insert @ApplicationID, @SurveyID, @DefaultRoleID, @ModifiedID, @ModifiedDT
	
	INSERT INTO [dbo].[SurveyStatus] ([SurveyID], [StatusID], [StatusNM], [StatusDS], [EmailTemplate], [PreviousStatusID], [NextStatusID], [ModifiedID], [ModifiedDT])
    select @SurveyID as SurveyID, 
           StatusID, 
           StatusNM, 
           StatusDS, 
           isnull(EmailTemplate, 'NO EMAIL'),
           PreviousStatusID, 
           NextStatusID, 
           @ModifiedID  as ModifiedID, 
           GETDATE() 
      from lu_SurveyResponseStatus

    INSERT INTO [dbo].[SurveyReviewStatus]([SurveyID], [ReviewStatusID], [ReviewStatusNM], [ReviewStatusDS], [ApprovedFL], [CommentFL], [ModifiedID], [ModifiedDT])
    SELECT @SurveyID as SurveyID, 
           [ReviewStatusID],
           [ReviewStatusNM],
           [ReviewStatusDS],
           [ApprovedFL],
           [CommentFL], 
           @ModifiedID  as ModifiedID, 
           GETDATE()  
      FROM [dbo].[lu_ReviewStatus]
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyID], [SurveyTypeID], [UseQuestionGroupsFL], [SurveyNM], [SurveyShortNM], [SurveyDS], [CompletionMessage], [ResponseNMTemplate], [ReviewerAccountNM], [AutoAssignFilter], [StartDT], [EndDT], [ParentSurveyID],  [ModifiedID], [ModifiedDT]
	FROM   [dbo].[Survey]
	WHERE  [SurveyID] = @SurveyID
	-- End Return Select <- do not remove
               
	COMMIT





