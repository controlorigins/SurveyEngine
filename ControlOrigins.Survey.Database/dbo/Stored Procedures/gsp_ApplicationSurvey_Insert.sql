CREATE PROC [dbo].[gsp_ApplicationSurvey_Insert] 
    @ApplicationID int,
    @SurveyID int,
    @DefaultRoleID int,
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[ApplicationSurvey] ([ApplicationID], [SurveyID], [DefaultRoleID], [ModifiedID], [ModifiedDT])
	SELECT @ApplicationID, @SurveyID, @DefaultRoleID, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [ApplicationSurveyID], [ApplicationID], [SurveyID], [DefaultRoleID], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[ApplicationSurvey]
	WHERE  [ApplicationSurveyID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



