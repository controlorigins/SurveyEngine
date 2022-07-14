

-- ==========================================================================================
-- Entity Name:	Company_DeleteRow
-- Create date:	10/9/2015 2:14:07 PM
-- Description:	This stored procedure is intended for deleting a specific row from Company table
-- ==========================================================================================
CREATE Procedure [dbo].[Company_DeleteRow]
	@CompanyID int
As
Begin
	Delete Company
	Where
		[CompanyID] = @CompanyID

End

