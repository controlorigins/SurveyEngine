CREATE PROC [dbo].[gsp_QuestionGroup_Insert] 
    @SurveyID int,
    @GroupOrder int,
    @QuestionGroupShortNM nvarchar(50),
    @QuestionGroupNM nvarchar(50),
    @QuestionGroupDS nvarchar(MAX),
    @QuestionGroupWeight decimal(18, 4),
    @GroupHeader nvarchar(MAX),
    @GroupFooter nvarchar(MAX),
    @ModifiedID int,
    @ModifiedDT datetime,
    @DependentQuestionGroupID int,
    @DependentMinScore decimal(18, 4),
    @DependentMaxScore decimal(18, 4)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[QuestionGroup] ([SurveyID], [GroupOrder], [QuestionGroupShortNM], [QuestionGroupNM], [QuestionGroupDS], [QuestionGroupWeight], [GroupHeader], [GroupFooter], [ModifiedID], [ModifiedDT], [DependentQuestionGroupID], [DependentMinScore], [DependentMaxScore])
	SELECT @SurveyID, @GroupOrder, @QuestionGroupShortNM, @QuestionGroupNM, @QuestionGroupDS, @QuestionGroupWeight, @GroupHeader, @GroupFooter, @ModifiedID, @ModifiedDT, @DependentQuestionGroupID, @DependentMinScore, @DependentMaxScore
	
	-- Begin Return Select <- do not remove
	SELECT [QuestionGroupID], [SurveyID], [GroupOrder], [QuestionGroupShortNM], [QuestionGroupNM], [QuestionGroupDS], [QuestionGroupWeight], [GroupHeader], [GroupFooter], [ModifiedID], [ModifiedDT], [DependentQuestionGroupID], [DependentMinScore], [DependentMaxScore]
	FROM   [dbo].[QuestionGroup]
	WHERE  [QuestionGroupID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



