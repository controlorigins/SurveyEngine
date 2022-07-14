
CREATE PROC [dbo].[usp_SurveyEmailTemplate_SelectBySurveyID] 
    @SurveyID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [SurveyEmailTemplateID], 
	       [SurveyEmailTemplateNM], 
	       [SurveyID], 
	       [StatusID], 
	       [SubjectTemplate], 
	       [EmailTemplate], 
	       [FromEmailAddress],
	       [FilterCriteria],
	       [StartDT], 
	       [EndDT], 
	       [Active],
	       [SendToSupervisor],
	       [ModifiedID], 
	       [ModifiedDT] 
	FROM   [dbo].[SurveyEmailTemplate] 
	WHERE  (SurveyID = @SurveyID ) 

	COMMIT




