CREATE PROC [dbo].[gsp_lu_SurveyResponseStatus_Insert] 
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
	
	INSERT INTO [dbo].[lu_SurveyResponseStatus] ([StatusNM], [StatusDS], [EmailTemplate], [PreviousStatusID], [NextStatusID], [ModifiedID], [ModifiedDT])
	SELECT @StatusNM, @StatusDS, @EmailTemplate, @PreviousStatusID, @NextStatusID, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [StatusID], [StatusNM], [StatusDS], [EmailTemplate], [PreviousStatusID], [NextStatusID], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[lu_SurveyResponseStatus]
	WHERE  [StatusID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



