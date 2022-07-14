CREATE PROC [dbo].[gsp_ApplicationSurvey_Update] 
    @ApplicationSurveyID int,
    @ApplicationID int,
    @SurveyID int,
    @DefaultRoleID int,
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[ApplicationSurvey]
	SET    [ApplicationID] = @ApplicationID, [SurveyID] = @SurveyID, [DefaultRoleID] = @DefaultRoleID, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [ApplicationSurveyID] = @ApplicationSurveyID
	
	-- Begin Return Select <- do not remove
	SELECT [ApplicationSurveyID], [ApplicationID], [SurveyID], [DefaultRoleID], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[ApplicationSurvey]
	WHERE  [ApplicationSurveyID] = @ApplicationSurveyID	
	-- End Return Select <- do not remove

	COMMIT



