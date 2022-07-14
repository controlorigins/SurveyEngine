CREATE PROC [dbo].[gsp_lu_UnitOfMeasure_Delete] 
    @UnitOfMeasureID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[lu_UnitOfMeasure]
	WHERE  [UnitOfMeasureID] = @UnitOfMeasureID

	COMMIT



