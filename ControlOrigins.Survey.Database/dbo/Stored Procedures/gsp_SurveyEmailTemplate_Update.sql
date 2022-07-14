CREATE PROC [dbo].[gsp_SurveyEmailTemplate_Update] 
    @SurveyEmailTemplateID int,
    @SurveyEmailTemplateNM nvarchar(250),
    @SurveyID int,
    @StatusID int,
    @SubjectTemplate nvarchar(MAX),
    @EmailTemplate nvarchar(MAX),
    @FromEmailAddress nvarchar(150),
    @FilterCriteria nvarchar(MAX),
    @StartDT datetime,
    @EndDT datetime,
    @Active bit,
    @SendToSupervisor bit,
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[SurveyEmailTemplate]
	SET    [SurveyEmailTemplateNM] = @SurveyEmailTemplateNM, [SurveyID] = @SurveyID, [StatusID] = @StatusID, [SubjectTemplate] = @SubjectTemplate, [EmailTemplate] = @EmailTemplate, [FromEmailAddress] = @FromEmailAddress, [FilterCriteria] = @FilterCriteria, [StartDT] = @StartDT, [EndDT] = @EndDT, [Active] = @Active, [SendToSupervisor] = @SendToSupervisor, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [SurveyEmailTemplateID] = @SurveyEmailTemplateID
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyEmailTemplateID], [SurveyEmailTemplateNM], [SurveyID], [StatusID], [SubjectTemplate], [EmailTemplate], [FromEmailAddress], [FilterCriteria], [StartDT], [EndDT], [Active], [SendToSupervisor], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[SurveyEmailTemplate]
	WHERE  [SurveyEmailTemplateID] = @SurveyEmailTemplateID	
	-- End Return Select <- do not remove

	COMMIT



