

-- ==========================================================================================
-- Entity Name:	ApplicationUserRole_Insert
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:43:41 AM
-- Description:	This stored procedure is intended for inserting values to ApplicationUserRole table
-- ==========================================================================================
CREATE Procedure [dbo].[ApplicationUserRole_Insert]
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
	Insert Into ApplicationUserRole
		([ApplicationID],[ApplicationUserID],[RoleID],[ModifiedID],[ModifiedDT],[IsDemo],[StartUpDate],[IsMonthlyPrice],[Price],[UserInRolled],[IsUserAdmin])
	Values
		(@ApplicationID,@ApplicationUserID,@RoleID,@ModifiedID,@ModifiedDT,@IsDemo,@StartUpDate,@IsMonthlyPrice,@Price,@UserInRolled,@IsUserAdmin)

	Declare @ApplicationUserRoleID int
	Select @ApplicationUserRoleID = @@IDENTITY
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

