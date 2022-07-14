

-- ==========================================================================================
-- Entity Name:	lu_SurveyType_DeleteRow
-- Create date:	10/5/2015 12:13:50 PM
-- Description:	This stored procedure is intended for deleting a specific row from lu_SurveyType table
-- ==========================================================================================
CREATE Procedure [dbo].[lu_SurveyType_DeleteRow]
	@SurveyTypeID int
As
Begin
	Delete lu_SurveyType
	Where
		[SurveyTypeID] = @SurveyTypeID

End

