CREATE PROC [dbo].[gsp_SurveyStatus_Select] 
    @SurveyStatusID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [SurveyStatusID], [SurveyID], [StatusID], [StatusNM], [StatusDS], [EmailTemplate], [EmailSubjectTemplate], [PreviousStatusID], [NextStatusID], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[SurveyStatus] 
	WHERE  ([SurveyStatusID] = @SurveyStatusID OR @SurveyStatusID IS NULL) 

	COMMIT



