CREATE TABLE [dbo].[ChartSetting] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [SiteUserID]           INT            NOT NULL,
    [SiteAppID]            INT            NOT NULL,
    [SettingType]          NVARCHAR (50)  NOT NULL,
    [SettingName]          NVARCHAR (50)  NOT NULL,
    [SettingValue]         NVARCHAR (MAX) NOT NULL,
    [SettingValueEnhanced] NVARCHAR (MAX) NULL,
    [DateCreated]          DATETIME       NOT NULL,
    [LastUpdated]          DATETIME       NOT NULL,
    CONSTRAINT [PK_ChartSetting] PRIMARY KEY CLUSTERED ([Id] ASC)
);

