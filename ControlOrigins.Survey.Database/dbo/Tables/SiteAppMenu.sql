CREATE TABLE [dbo].[SiteAppMenu] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [SiteAppID]   INT            NOT NULL,
    [MenuText]    NVARCHAR (50)  NOT NULL,
    [TartgetPage] NVARCHAR (MAX) NOT NULL,
    [GlyphName]   NVARCHAR (50)  NOT NULL,
    [MenuOrder]   INT            NOT NULL,
    [SiteRoleID]  INT            NOT NULL,
    [ViewInMenu]  BIT            NOT NULL,
    CONSTRAINT [PK_SiteAppMenu] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SiteAppMenu_Application] FOREIGN KEY ([SiteAppID]) REFERENCES [dbo].[Application] ([ApplicationID]),
    CONSTRAINT [UK_SiteAppMenu] UNIQUE NONCLUSTERED ([MenuText] ASC, [SiteAppID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Menu Item must be unique for Application and Role', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SiteAppMenu', @level2type = N'CONSTRAINT', @level2name = N'UK_SiteAppMenu';

