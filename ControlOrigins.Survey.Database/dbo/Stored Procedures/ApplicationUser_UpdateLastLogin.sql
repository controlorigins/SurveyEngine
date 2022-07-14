
CREATE PROC [dbo].[ApplicationUser_UpdateLastLogin] 
    @ApplicationUserID int,
    @LastLoginDT datetime,
    @LastLoginLocation ntext,
    @ModifiedID int,
    @ModifiedDT datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[ApplicationUser]
	SET    [LastLoginDT] = @LastLoginDT, [LastLoginLocation] = @LastLoginLocation, [ModifiedID] = @ModifiedID, [ModifiedDT] = @ModifiedDT
	WHERE  [ApplicationUserID] = @ApplicationUserID

	COMMIT




