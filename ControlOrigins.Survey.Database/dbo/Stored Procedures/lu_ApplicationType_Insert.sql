

-- ==========================================================================================
-- Entity Name:	lu_ApplicationType_Insert
-- Create date:	10/7/2015 10:26:11 PM
-- Description:	This stored procedure is intended for inserting values to lu_ApplicationType table
-- ==========================================================================================
CREATE Procedure [dbo].[lu_ApplicationType_Insert]
	@ApplicationTypeNM nvarchar(50),
	@ApplicationTypeDS nvarchar(MAX),
	@ModifiedID int,
	@ModifiedDT datetime
As
Begin
	Insert Into lu_ApplicationType
		([ApplicationTypeNM],[ApplicationTypeDS],[ModifiedID],[ModifiedDT])
	Values
		(@ApplicationTypeNM,@ApplicationTypeDS,@ModifiedID,@ModifiedDT)

	Declare @ApplicationTypeID int
	Select @ApplicationTypeID = @@IDENTITY
	Select 
		[ApplicationTypeID],
		[ApplicationTypeNM],
		[ApplicationTypeDS],
		[ModifiedID],
		[ModifiedDT]
	From lu_ApplicationType
	Where
		[ApplicationTypeID] = @ApplicationTypeID
End

