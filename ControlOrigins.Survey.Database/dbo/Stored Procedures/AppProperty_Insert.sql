

-- ==========================================================================================
-- Entity Name:	AppProperty_Insert
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:11:54 AM
-- Description:	This stored procedure is intended for inserting values to AppProperty table
-- ==========================================================================================
CREATE Procedure [dbo].[AppProperty_Insert]
	@SiteAppID int,
	@Key nvarchar(50),
	@Value nvarchar(500)
As
Begin
	Insert Into AppProperty
		([SiteAppID],[Key],[Value])
	Values
		(@SiteAppID,@Key,@Value)

	Declare @Id int
	Select @Id = @@IDENTITY
	Select 
		[Id],
		[SiteAppID],
		[Key],
		[Value]
	From AppProperty
	Where
		[Id] = @Id
End

