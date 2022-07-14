CREATE PROC [dbo].[gsp_lu_SurveyResponseStatus_Update] 
    @StatusID int,
    @StatusNM nvarchar(50),
    @StatusDS nvarchar(MAX),
    @EmailTemplate nvarchar(MAX),
    @PreviousStatusID int,
    @NextStatusID int,
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[lu_SurveyResponseStatus]
	SET    [StatusNM] = @StatusNM, [StatusDS] = @StatusDS, [EmailTemplate] = @EmailTemplate, [PreviousStatusID] = @PreviousStatusID, [NextStatusID] = @NextStatusID, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [StatusID] = @StatusID
	
	-- Begin Return Select <- do not remove
	SELECT [StatusID], [StatusNM], [StatusDS], [EmailTemplate], [PreviousStatusID], [NextStatusID], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[lu_SurveyResponseStatus]
	WHERE  [StatusID] = @StatusID	
	-- End Return Select <- do not remove

	COMMIT



