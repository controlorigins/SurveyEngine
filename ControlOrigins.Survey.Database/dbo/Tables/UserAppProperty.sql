CREATE TABLE [dbo].[UserAppProperty] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [UserID] INT            NOT NULL,
    [AppID]  INT            NOT NULL,
    [Key]    NVARCHAR (50)  NOT NULL,
    [Value]  NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_UserAppProperty] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserAppProperty_Application] FOREIGN KEY ([AppID]) REFERENCES [dbo].[Application] ([ApplicationID]),
    CONSTRAINT [FK_UserAppProperty_ApplicationUser] FOREIGN KEY ([UserID]) REFERENCES [dbo].[ApplicationUser] ([ApplicationUserID]),
    CONSTRAINT [UK_UserAppProperty] UNIQUE NONCLUSTERED ([AppID] ASC, [UserID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Application ID and User ID must be unique', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserAppProperty', @level2type = N'CONSTRAINT', @level2name = N'UK_UserAppProperty';

