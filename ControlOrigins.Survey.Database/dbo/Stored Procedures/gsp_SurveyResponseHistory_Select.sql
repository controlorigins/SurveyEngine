CREATE PROC [dbo].[gsp_SurveyResponseHistory_Select] 
    @SurveyResponseHistoryID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [SurveyResponseHistoryID], [ApplicationUserID], [SurveyResponseID], [SurveyResponseNM], [StatusID], [QuestionGroupID], [UserNM], [Answers], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[SurveyResponseHistory] 
	WHERE  ([SurveyResponseHistoryID] = @SurveyResponseHistoryID OR @SurveyResponseHistoryID IS NULL) 

	COMMIT



