CREATE PROC [dbo].[gsp_ApplicationSurvey_Select] 
    @ApplicationSurveyID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ApplicationSurveyID], [ApplicationID], [SurveyID], [DefaultRoleID], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[ApplicationSurvey] 
	WHERE  ([ApplicationSurveyID] = @ApplicationSurveyID OR @ApplicationSurveyID IS NULL) 

	COMMIT



