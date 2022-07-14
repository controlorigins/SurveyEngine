

-- ==========================================================================================
-- Entity Name:	ApplicationUserRole_Update
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:43:41 AM
-- Description:	This stored procedure is intended for updating ApplicationUserRole table
-- ==========================================================================================
CREATE Procedure [dbo].[ApplicationUserRole_Update]
	@ApplicationUserRoleID int,
	@ApplicationID int,
	@ApplicationUserID int,
	@RoleID int,
	@ModifiedID int,
	@ModifiedDT datetime,
	@IsDemo bit,
	@StartUpDate date,
	@IsMonthlyPrice bit,
	@Price money,
	@UserInRolled bit,
	@IsUserAdmin bit
As
Begin
	Update ApplicationUserRole
	Set
		[ApplicationID] = @ApplicationID,
		[ApplicationUserID] = @ApplicationUserID,
		[RoleID] = @RoleID,
		[ModifiedID] = @ModifiedID,
		[ModifiedDT] = @ModifiedDT,
		[IsDemo] = @IsDemo,
		[StartUpDate] = @StartUpDate,
		[IsMonthlyPrice] = @IsMonthlyPrice,
		[Price] = @Price,
		[UserInRolled] = @UserInRolled,
		[IsUserAdmin] = @IsUserAdmin
	Where		
		[ApplicationUserRoleID] = @ApplicationUserRoleID
	Select 
		[ApplicationUserRoleID],
		[ApplicationID],
		[ApplicationUserID],
		[RoleID],
		[ModifiedID],
		[ModifiedDT],
		[IsDemo],
		[StartUpDate],
		[IsMonthlyPrice],
		[Price],
		[UserInRolled],
		[IsUserAdmin]
	From ApplicationUserRole
	Where
		[ApplicationUserRoleID] = @ApplicationUserRoleID
End

