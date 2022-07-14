
CREATE PROC [dbo].[usp_QuestionGroup_SelectBySurveyID] 
    @SurveyID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [QuestionGroupID], 
	       [SurveyID], 
	       [GroupOrder], 
	       [QuestionGroupShortNM], 
	       [QuestionGroupNM], 
	       [QuestionGroupDS], 
	       [QuestionGroupWeight], 
	       [GroupHeader], 
	       [GroupFooter], 
	       [ModifiedID], 
	       [ModifiedDT],
	       [DependentQuestionGroupID],
	       [DependentMinScore],
	       [DependentMaxScore] 
	FROM   [dbo].[QuestionGroup] 
	WHERE  ([SurveyID] = @SurveyID ) 
	ORDER BY [GroupOrder]
	

	COMMIT




