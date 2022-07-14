CREATE PROC [dbo].[gsp_SurveyEmailTemplate_Insert] 
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
	
	INSERT INTO [dbo].[SurveyEmailTemplate] ([SurveyEmailTemplateNM], [SurveyID], [StatusID], [SubjectTemplate], [EmailTemplate], [FromEmailAddress], [FilterCriteria], [StartDT], [EndDT], [Active], [SendToSupervisor], [ModifiedID], [ModifiedDT])
	SELECT @SurveyEmailTemplateNM, @SurveyID, @StatusID, @SubjectTemplate, @EmailTemplate, @FromEmailAddress, @FilterCriteria, @StartDT, @EndDT, @Active, @SendToSupervisor, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyEmailTemplateID], [SurveyEmailTemplateNM], [SurveyID], [StatusID], [SubjectTemplate], [EmailTemplate], [FromEmailAddress], [FilterCriteria], [StartDT], [EndDT], [Active], [SendToSupervisor], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[SurveyEmailTemplate]
	WHERE  [SurveyEmailTemplateID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



