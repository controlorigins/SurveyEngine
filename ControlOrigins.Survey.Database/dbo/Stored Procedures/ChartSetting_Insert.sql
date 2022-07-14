

-- ==========================================================================================
-- Entity Name:	ChartSetting_Insert
-- Create date:	12/3/2015 6:19:38 PM
-- Description:	This stored procedure is intended for inserting values to ChartSetting table
-- ==========================================================================================
CREATE Procedure [dbo].[ChartSetting_Insert]
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
	Insert Into ChartSetting
		([SiteUserID],[SiteAppID],[SettingType],[SettingName],[SettingValue],[SettingValueEnhanced],[DateCreated],[LastUpdated])
	Values
		(@SiteUserID,@SiteAppID,@SettingType,@SettingName,@SettingValue,@SettingValueEnhanced,@DateCreated,@LastUpdated)

	Declare @Id int
	Select @Id = @@IDENTITY
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

