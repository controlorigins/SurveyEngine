CREATE PROC [dbo].[gsp_SurveyResponseState_Update] 
    @SurveyResponseStateID int,
    @SurveyResponseID int,
    @StatusID int,
    @AssignedUserID int,
    @Active bit,
    @EmailSent bit,
    @EmailBody nvarchar(MAX),
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[SurveyResponseState]
	SET    [SurveyResponseID] = @SurveyResponseID, [StatusID] = @StatusID, [AssignedUserID] = @AssignedUserID, [Active] = @Active, [EmailSent] = @EmailSent, [EmailBody] = @EmailBody, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [SurveyResponseStateID] = @SurveyResponseStateID
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyResponseStateID], [SurveyResponseID], [StatusID], [AssignedUserID], [Active], [EmailSent], [EmailBody], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[SurveyResponseState]
	WHERE  [SurveyResponseStateID] = @SurveyResponseStateID	
	-- End Return Select <- do not remove

	COMMIT



