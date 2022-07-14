CREATE PROC [dbo].[gsp_WebPortal_Insert] 
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
	
	INSERT INTO [dbo].[WebPortal] ([WebPortalNM], [WebPortalDS], [WebPortalURL], [WebServiceURL], [ActiveFL], [ModifiedID], [ModifiedDT])
	SELECT @WebPortalNM, @WebPortalDS, @WebPortalURL, @WebServiceURL, @ActiveFL, @ModifiedID, @ModifiedDT
	
	-- Begin Return Select <- do not remove
	SELECT [WebPortalID], [WebPortalNM], [WebPortalDS], [WebPortalURL], [WebServiceURL], [ActiveFL], [ModifiedID], [ModifiedDT]
	FROM   [dbo].[WebPortal]
	WHERE  [WebPortalID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT

