CREATE TABLE [dbo].[UserMessages] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [ToUserID]       INT            NULL,
    [FromUserID]     INT            NULL,
    [Message]        NVARCHAR (MAX) NULL,
    [Opened]         BIT            NULL,
    [CratedDateTime] DATETIME       NULL,
    [Subject]        NVARCHAR (50)  NULL,
    [Deleted]        BIT            NULL,
    [AppID]          INT            NULL,
    [ShowonPage]     INT            NULL,
    [FromApp]        BIT            NULL,
    CONSTRAINT [PK_UserMessages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserMessages_FROM_ApplicationUser] FOREIGN KEY ([FromUserID]) REFERENCES [dbo].[ApplicationUser] ([ApplicationUserID]),
    CONSTRAINT [FK_UserMessages_TO_ApplicationUser] FOREIGN KEY ([ToUserID]) REFERENCES [dbo].[ApplicationUser] ([ApplicationUserID])
);

