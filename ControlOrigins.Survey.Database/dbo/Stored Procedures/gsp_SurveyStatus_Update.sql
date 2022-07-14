CREATE PROC [dbo].[gsp_SurveyStatus_Update] 
    @SurveyStatusID int,
    @SurveyID int,
    @StatusID int,
    @StatusNM nvarchar(50),
    @StatusDS nvarchar(MAX),
    @EmailTemplate nvarchar(MAX),
    @EmailSubjectTemplate nvarchar(MAX),
    @PreviousStatusID int,
    @NextStatusID int,
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[SurveyStatus]
	SET    [SurveyID] = @SurveyID, [StatusID] = @StatusID, [StatusNM] = @StatusNM, [StatusDS] = @StatusDS, [EmailTemplate] = @EmailTemplate, [EmailSubjectTemplate] = @EmailSubjectTemplate, [PreviousStatusID] = @PreviousStatusID, [NextStatusID] = @NextStatusID, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [SurveyStatusID] = @SurveyStatusID
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyStatusID], [SurveyID], [StatusID], [StatusNM], [StatusDS], [EmailTemplate], [EmailSubjectTemplate], [PreviousStatusID], [NextStatusID], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[SurveyStatus]
	WHERE  [SurveyStatusID] = @SurveyStatusID	
	-- End Return Select <- do not remove

	COMMIT



