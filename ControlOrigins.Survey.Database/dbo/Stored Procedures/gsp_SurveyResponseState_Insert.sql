CREATE PROC [dbo].[gsp_SurveyResponseState_Insert] 
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
	
	INSERT INTO [dbo].[SurveyResponseState] ([SurveyResponseID], [StatusID], [AssignedUserID], [Active], [EmailSent], [EmailBody], [ModifiedID], [ModifiedDT])
	SELECT @SurveyResponseID, @StatusID, @AssignedUserID, @Active, @EmailSent, @EmailBody, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyResponseStateID], [SurveyResponseID], [StatusID], [AssignedUserID], [Active], [EmailSent], [EmailBody], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[SurveyResponseState]
	WHERE  [SurveyResponseStateID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



