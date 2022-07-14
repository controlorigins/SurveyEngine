
CREATE PROC [dbo].[usp_SurveyStatus_SelectBySurveyID] 
    @SurveyID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [SurveyStatusID], [SurveyID], [StatusID], [StatusNM], [StatusDS], [EmailTemplate],[EmailSubjectTemplate], [PreviousStatusID], [NextStatusID], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[SurveyStatus] 
	WHERE  ([SurveyID] = @SurveyID ) 

	COMMIT




