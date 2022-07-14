CREATE PROC [dbo].[gsp_SurveyStatus_Insert] 
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
	
	INSERT INTO [dbo].[SurveyStatus] ([SurveyID], [StatusID], [StatusNM], [StatusDS], [EmailTemplate], [EmailSubjectTemplate], [PreviousStatusID], [NextStatusID], [ModifiedID], [ModifiedDT])
	SELECT @SurveyID, @StatusID, @StatusNM, @StatusDS, @EmailTemplate, @EmailSubjectTemplate, @PreviousStatusID, @NextStatusID, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyStatusID], [SurveyID], [StatusID], [StatusNM], [StatusDS], [EmailTemplate], [EmailSubjectTemplate], [PreviousStatusID], [NextStatusID], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[SurveyStatus]
	WHERE  [SurveyStatusID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



