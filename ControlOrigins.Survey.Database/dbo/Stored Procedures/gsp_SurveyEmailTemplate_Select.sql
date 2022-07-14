CREATE PROC [dbo].[gsp_SurveyEmailTemplate_Select] 
    @SurveyEmailTemplateID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [SurveyEmailTemplateID], [SurveyEmailTemplateNM], [SurveyID], [StatusID], [SubjectTemplate], [EmailTemplate], [FromEmailAddress], [FilterCriteria], [StartDT], [EndDT], [Active], [SendToSupervisor], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[SurveyEmailTemplate] 
	WHERE  ([SurveyEmailTemplateID] = @SurveyEmailTemplateID OR @SurveyEmailTemplateID IS NULL) 

	COMMIT



