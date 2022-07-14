CREATE PROC [dbo].[gsp_SurveyResponseSequence_Update] 
    @SurveyResponseSequenceID int,
    @SurveyResponseID int,
    @SequenceNumber int,
    @SequenceText nvarchar(255),
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[SurveyResponseSequence]
	SET    [SurveyResponseID] = @SurveyResponseID, [SequenceNumber] = @SequenceNumber, [SequenceText] = @SequenceText, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [SurveyResponseSequenceID] = @SurveyResponseSequenceID
	
	-- Begin Return Select <- do not remove
	SELECT [SurveyResponseSequenceID], [SurveyResponseID], [SequenceNumber], [SequenceText], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[SurveyResponseSequence]
	WHERE  [SurveyResponseSequenceID] = @SurveyResponseSequenceID	
	-- End Return Select <- do not remove

	COMMIT



