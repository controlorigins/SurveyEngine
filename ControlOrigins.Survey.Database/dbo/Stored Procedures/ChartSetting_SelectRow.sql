

-- ==========================================================================================
-- Entity Name:	ChartSetting_SelectRow
-- Create date:	12/3/2015 6:19:38 PM
-- Description:	This stored procedure is intended for selecting a specific row from ChartSetting table
-- ==========================================================================================
CREATE Procedure [dbo].[ChartSetting_SelectRow]
	@Id int
As
Begin
	Select 
		[Id],
		[SiteUserID],
		[SiteAppID],
		[SettingType],
		[SettingName],
		[SettingValue],
		[SettingValueEnhanced],
		[DateCreated],
		[LastUpdated]
	From ChartSetting
	Where
		[Id] = @Id
End

