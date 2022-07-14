CREATE TABLE [dbo].[SiteRole] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [RoleName] NVARCHAR (150) NOT NULL,
    [Active]   BIT            NOT NULL,
    CONSTRAINT [PK_SiteRole] PRIMARY KEY CLUSTERED ([Id] ASC)
);

