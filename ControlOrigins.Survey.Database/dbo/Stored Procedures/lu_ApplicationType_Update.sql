

-- ==========================================================================================
-- Entity Name:	lu_ApplicationType_Update
-- Create date:	10/7/2015 10:26:11 PM
-- Description:	This stored procedure is intended for updating lu_ApplicationType table
-- ==========================================================================================
CREATE Procedure [dbo].[lu_ApplicationType_Update]
	@ApplicationTypeID int,
	@ApplicationTypeNM nvarchar(50),
	@ApplicationTypeDS nvarchar(MAX),
	@ModifiedID int,
	@ModifiedDT datetime
As
Begin
	Update lu_ApplicationType
	Set
		[ApplicationTypeNM] = @ApplicationTypeNM,
		[ApplicationTypeDS] = @ApplicationTypeDS,
		[ModifiedID] = @ModifiedID,
		[ModifiedDT] = @ModifiedDT
	Where		
		[ApplicationTypeID] = @ApplicationTypeID
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

