CREATE PROC [dbo].[gsp_lu_SurveyResponseStatus_Select] 
    @StatusID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [StatusID], [StatusNM], [StatusDS], [EmailTemplate], [PreviousStatusID], [NextStatusID], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[lu_SurveyResponseStatus] 
	WHERE  ([StatusID] = @StatusID OR @StatusID IS NULL) 

	COMMIT



