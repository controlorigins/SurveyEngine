

-- ==========================================================================================
-- Entity Name:	ChartSetting_DeleteRow
-- Create date:	12/3/2015 6:19:38 PM
-- Description:	This stored procedure is intended for deleting a specific row from ChartSetting table
-- ==========================================================================================
CREATE Procedure [dbo].[ChartSetting_DeleteRow]
	@Id int
As
Begin
	Delete ChartSetting
	Where
		[Id] = @Id

End

