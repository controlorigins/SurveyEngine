CREATE PROC [dbo].[gsp_WebPortal_Delete] 
    @WebPortalID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[WebPortal]
	WHERE  [WebPortalID] = @WebPortalID

	COMMIT

