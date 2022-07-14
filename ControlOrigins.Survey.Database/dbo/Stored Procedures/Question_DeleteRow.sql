

-- ==========================================================================================
-- Entity Name:	Question_DeleteRow
-- Create date:	10/16/2015 12:12:44 PM
-- Description:	This stored procedure is intended for deleting a specific row from Question table
-- ==========================================================================================
CREATE Procedure [dbo].[Question_DeleteRow]
	@QuestionID int
As
Begin
	Delete Question
	Where
		[QuestionID] = @QuestionID

End

