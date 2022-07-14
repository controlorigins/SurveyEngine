CREATE TABLE [dbo].[AppProperty] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [SiteAppID] INT            NOT NULL,
    [Key]       NVARCHAR (50)  NOT NULL,
    [Value]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_AppProperty] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AppProperty_Application] FOREIGN KEY ([SiteAppID]) REFERENCES [dbo].[Application] ([ApplicationID])
);

