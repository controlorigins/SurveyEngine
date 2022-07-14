CREATE PROC [dbo].[gsp_QuestionGroup_Update] 
    @QuestionGroupID int,
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

	UPDATE [dbo].[QuestionGroup]
	SET    [SurveyID] = @SurveyID, [GroupOrder] = @GroupOrder, [QuestionGroupShortNM] = @QuestionGroupShortNM, [QuestionGroupNM] = @QuestionGroupNM, [QuestionGroupDS] = @QuestionGroupDS, [QuestionGroupWeight] = @QuestionGroupWeight, [GroupHeader] = @GroupHeader, [GroupFooter] = @GroupFooter, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT, [DependentQuestionGroupID] = @DependentQuestionGroupID, [DependentMinScore] = @DependentMinScore, [DependentMaxScore] = @DependentMaxScore
	WHERE  [QuestionGroupID] = @QuestionGroupID
	
	-- Begin Return Select <- do not remove
	SELECT [QuestionGroupID], [SurveyID], [GroupOrder], [QuestionGroupShortNM], [QuestionGroupNM], [QuestionGroupDS], [QuestionGroupWeight], [GroupHeader], [GroupFooter], [ModifiedID], [ModifiedDT], [DependentQuestionGroupID], [DependentMinScore], [DependentMaxScore]
	FROM   [dbo].[QuestionGroup]
	WHERE  [QuestionGroupID] = @QuestionGroupID	
	-- End Return Select <- do not remove

	COMMIT



