CREATE PROC [dbo].[gsp_lu_ReviewStatus_Select] 
    @ReviewStatusID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ReviewStatusID], [ReviewStatusNM], [ReviewStatusDS], [ApprovedFL], [CommentFL], [ModifiedID], [ModifiedDT] 
	FROM   [dbo].[lu_ReviewStatus] 
	WHERE  ([ReviewStatusID] = @ReviewStatusID OR @ReviewStatusID IS NULL) 

	COMMIT



