CREATE PROC [dbo].[gsp_WebPortal_Select] 
    @WebPortalID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [WebPortalID], [WebPortalNM], [WebPortalDS], [WebPortalURL], [WebServiceURL], [ActiveFL], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[WebPortal] 
	WHERE  ([WebPortalID] = @WebPortalID OR @WebPortalID IS NULL) 

	COMMIT

