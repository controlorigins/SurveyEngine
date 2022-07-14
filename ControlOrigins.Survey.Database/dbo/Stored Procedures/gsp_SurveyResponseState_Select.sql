CREATE PROC [dbo].[gsp_SurveyResponseState_Select] 
    @SurveyResponseStateID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [SurveyResponseStateID], [SurveyResponseID], [StatusID], [AssignedUserID], [Active], [EmailSent], [EmailBody], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[SurveyResponseState] 
	WHERE  ([SurveyResponseStateID] = @SurveyResponseStateID OR @SurveyResponseStateID IS NULL) 

	COMMIT



