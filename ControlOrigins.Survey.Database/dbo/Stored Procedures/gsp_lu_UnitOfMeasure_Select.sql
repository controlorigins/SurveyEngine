CREATE PROC [dbo].[gsp_lu_UnitOfMeasure_Select] 
    @UnitOfMeasureID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [UnitOfMeasureID], [UnitOfMeasureNM], [UnitOfMeasureDS], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[lu_UnitOfMeasure] 
	WHERE  ([UnitOfMeasureID] = @UnitOfMeasureID OR @UnitOfMeasureID IS NULL) 

	COMMIT



