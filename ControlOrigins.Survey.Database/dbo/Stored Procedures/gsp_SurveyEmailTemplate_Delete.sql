CREATE PROC [dbo].[gsp_SurveyEmailTemplate_Delete] 
    @SurveyEmailTemplateID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[SurveyEmailTemplate]
	WHERE  [SurveyEmailTemplateID] = @SurveyEmailTemplateID

	COMMIT



