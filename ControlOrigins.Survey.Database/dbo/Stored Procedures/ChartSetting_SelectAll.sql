

-- ==========================================================================================
-- Entity Name:	ChartSetting_SelectAll
-- Create date:	12/3/2015 6:19:38 PM
-- Description:	This stored procedure is intended for selecting all rows from ChartSetting table
-- ==========================================================================================
CREATE Procedure [dbo].[ChartSetting_SelectAll]
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
End

