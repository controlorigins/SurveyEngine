CREATE PROC [dbo].[gsp_SurveyResponse_Update] 
    @SurveyResponseID int,
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

	UPDATE [dbo].[SurveyResponse]
	SET    [SurveyResponseNM] = @SurveyResponseNM, [SurveyID] = @SurveyID, [ApplicationID] = @ApplicationID, [AssignedUserID] = @AssignedUserID, [StatusID] = @StatusID, [DataSource] = @DataSource, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [SurveyResponseID] = @SurveyResponseID
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyResponseID], [SurveyResponseNM], [SurveyID], [ApplicationID], [AssignedUserID], [StatusID], [DataSource], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[SurveyResponse]
	WHERE  [SurveyResponseID] = @SurveyResponseID	
	-- End Return Select <- do not remove

	COMMIT



