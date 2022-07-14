CREATE PROC [dbo].[gsp_SurveyResponse_Insert] 
    @SurveyResponseNM nvarchar(250),
    @SurveyID int,
    @ApplicationID int,
    @AssignedUserID int,
    @StatusID int,
    @DataSource nvarchar(250),
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[SurveyResponse] ([SurveyResponseNM], [SurveyID], [ApplicationID], [AssignedUserID], [StatusID], [DataSource], [ModifiedID], [ModifiedDT])
	SELECT @SurveyResponseNM, @SurveyID, @ApplicationID, @AssignedUserID, @StatusID, @DataSource, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyResponseID], [SurveyResponseNM], [SurveyID], [ApplicationID], [AssignedUserID], [StatusID], [DataSource], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[SurveyResponse]
	WHERE  [SurveyResponseID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



