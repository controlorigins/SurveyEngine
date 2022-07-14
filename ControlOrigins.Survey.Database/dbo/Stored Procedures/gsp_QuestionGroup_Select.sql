CREATE PROC [dbo].[gsp_QuestionGroup_Select] 
    @QuestionGroupID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [QuestionGroupID], [SurveyID], [GroupOrder], [QuestionGroupShortNM], [QuestionGroupNM], [QuestionGroupDS], [QuestionGroupWeight], [GroupHeader], [GroupFooter], [ModifiedID], [ModifiedDT], [DependentQuestionGroupID], [DependentMinScore], [DependentMaxScore] 
	FROM   [dbo].[QuestionGroup] 
	WHERE  ([QuestionGroupID] = @QuestionGroupID OR @QuestionGroupID IS NULL) 

	COMMIT



