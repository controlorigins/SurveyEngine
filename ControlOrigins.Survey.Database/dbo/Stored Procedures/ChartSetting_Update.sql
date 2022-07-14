

-- ==========================================================================================
-- Entity Name:	ChartSetting_Update
-- Create date:	12/3/2015 6:19:38 PM
-- Description:	This stored procedure is intended for updating ChartSetting table
-- ==========================================================================================
CREATE Procedure [dbo].[ChartSetting_Update]
	@Id int,
	@SiteUserID int,
	@SiteAppID int,
	@SettingType nvarchar(50),
	@SettingName nvarchar(50),
	@SettingValue nvarchar(MAX),
	@SettingValueEnhanced nvarchar(MAX),
	@DateCreated datetime,
	@LastUpdated datetime
As
Begin
	Update ChartSetting
	Set
		[SiteUserID] = @SiteUserID,
		[SiteAppID] = @SiteAppID,
		[SettingType] = @SettingType,
		[SettingName] = @SettingName,
		[SettingValue] = @SettingValue,
		[SettingValueEnhanced] = @SettingValueEnhanced,
		[DateCreated] = @DateCreated,
		[LastUpdated] = @LastUpdated
	Where		
		[Id] = @Id
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

