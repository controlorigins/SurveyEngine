CREATE PROC [dbo].[gsp_lu_ApplicationType_Select] 
    @ApplicationTypeID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ApplicationTypeID], [ApplicationTypeNM], [ApplicationTypeDS], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[lu_ApplicationType] 
	WHERE  ([ApplicationTypeID] = @ApplicationTypeID OR @ApplicationTypeID IS NULL) 

	COMMIT



