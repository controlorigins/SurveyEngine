

-- ==========================================================================================
-- Entity Name:	UserAppProperty_Insert
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:43:21 AM
-- Description:	This stored procedure is intended for inserting values to UserAppProperty table
-- ==========================================================================================
CREATE Procedure [dbo].[UserAppProperty_Insert]
	@UserID int,
	@AppID int,
	@Key nvarchar(50),
	@Value nvarchar(MAX)
As
Begin
	Insert Into UserAppProperty
		([UserID],[AppID],[Key],[Value])
	Values
		(@UserID,@AppID,@Key,@Value)

	Declare @Id int
	Select @Id = @@IDENTITY
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

