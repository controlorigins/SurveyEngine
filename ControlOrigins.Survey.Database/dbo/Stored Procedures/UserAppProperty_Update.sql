

-- ==========================================================================================
-- Entity Name:	UserAppProperty_Update
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:43:21 AM
-- Description:	This stored procedure is intended for updating UserAppProperty table
-- ==========================================================================================
CREATE Procedure [dbo].[UserAppProperty_Update]
	@Id int,
	@UserID int,
	@AppID int,
	@Key nvarchar(50),
	@Value nvarchar(MAX)
As
Begin
	Update UserAppProperty
	Set
		[UserID] = @UserID,
		[AppID] = @AppID,
		[Key] = @Key,
		[Value] = @Value
	Where		
		[Id] = @Id
	Select 
		[Id],
		[UserID],
		[AppID],
		[Key],
		[Value]
	From UserAppProperty
	Where
		[Id] = @Id
End

