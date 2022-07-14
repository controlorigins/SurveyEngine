CREATE PROC [dbo].[gsp_Survey_Select] 
    @SurveyID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [SurveyID], [SurveyTypeID], [UseQuestionGroupsFL], [SurveyNM], [SurveyShortNM], [SurveyDS], [CompletionMessage], [ResponseNMTemplate], [ReviewerAccountNM], [AutoAssignFilter], [StartDT], [EndDT], [ParentSurveyID], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[Survey] 
	WHERE  ([SurveyID] = @SurveyID OR @SurveyID IS NULL) 

	COMMIT



