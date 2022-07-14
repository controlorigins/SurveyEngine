CREATE PROC [dbo].[gsp_SurveyResponse_Select] 
    @SurveyResponseID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [SurveyResponseID], [SurveyResponseNM], [SurveyID], [ApplicationID], [AssignedUserID], [StatusID], [DataSource], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[SurveyResponse] 
	WHERE  ([SurveyResponseID] = @SurveyResponseID OR @SurveyResponseID IS NULL) 

	COMMIT



