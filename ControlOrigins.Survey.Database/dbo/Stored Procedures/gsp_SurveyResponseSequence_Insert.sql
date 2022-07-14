CREATE PROC [dbo].[gsp_SurveyResponseSequence_Insert] 
    @SurveyResponseID int,
    @SequenceNumber int,
    @SequenceText nvarchar(255),
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[SurveyResponseSequence] ([SurveyResponseID], [SequenceNumber], [SequenceText], [ModifiedID], [ModifiedDT])
	SELECT @SurveyResponseID, @SequenceNumber, @SequenceText, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyResponseSequenceID], [SurveyResponseID], [SequenceNumber], [SequenceText], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[SurveyResponseSequence]
	WHERE  [SurveyResponseSequenceID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT



