CREATE PROC [dbo].[gsp_WebPortal_Update] 
    @WebPortalID int,
    @WebPortalNM nvarchar(50),
    @WebPortalDS nvarchar(MAX) = NULL,
    @WebPortalURL nvarchar(250),
    @WebServiceURL nvarchar(250),
    @ActiveFL bit,
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[WebPortal]
	SET    [WebPortalNM] = @WebPortalNM, [WebPortalDS] = @WebPortalDS, [WebPortalURL] = @WebPortalURL, [WebServiceURL] = @WebServiceURL, [ActiveFL] = @ActiveFL, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [WebPortalID] = @WebPortalID
	
	-- Begin Return Select <- do not remove
	SELECT [WebPortalID], [WebPortalNM], [WebPortalDS], [WebPortalURL], [WebServiceURL], [ActiveFL], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[WebPortal]
	WHERE  [WebPortalID] = @WebPortalID	
	-- End Return Select <- do not remove

	COMMIT

