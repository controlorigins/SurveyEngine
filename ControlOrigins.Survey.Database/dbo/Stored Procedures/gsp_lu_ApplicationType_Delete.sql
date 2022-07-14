CREATE PROC [dbo].[gsp_lu_ApplicationType_Delete] 
    @ApplicationTypeID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[lu_ApplicationType]
	WHERE  [ApplicationTypeID] = @ApplicationTypeID

	COMMIT



