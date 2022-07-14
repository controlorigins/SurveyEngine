CREATE TABLE [dbo].[ApplicationUserRole] (
    [ApplicationUserRoleID] INT      IDENTITY (1, 1) NOT NULL,
    [ApplicationID]         INT      NOT NULL,
    [ApplicationUserID]     INT      NOT NULL,
    [RoleID]                INT      NOT NULL,
    [ModifiedID]            INT      CONSTRAINT [DF__Applicati__Modif__3A4CA8FD] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]            DATETIME CONSTRAINT [DF__Applicati__Modif__3B40CD36] DEFAULT (getdate()) NOT NULL,
    [IsDemo]                BIT      NULL,
    [StartUpDate]           DATE     NULL,
    [IsMonthlyPrice]        BIT      NULL,
    [Price]                 MONEY    NULL,
    [UserInRolled]          BIT      NULL,
    [IsUserAdmin]           BIT      NULL,
    CONSTRAINT [ApplicationUserRole_PK] PRIMARY KEY NONCLUSTERED ([ApplicationUserRoleID] ASC),
    CONSTRAINT [FK_ApplicationUserRole_Application] FOREIGN KEY ([ApplicationID]) REFERENCES [dbo].[Application] ([ApplicationID]),
    CONSTRAINT [FK_ApplicationUserRole_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Role] ([RoleID]),
    CONSTRAINT [UserRole_FK01] FOREIGN KEY ([ApplicationUserID]) REFERENCES [dbo].[ApplicationUser] ([ApplicationUserID]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_ApplicationUserRole]
    ON [dbo].[ApplicationUserRole]([ApplicationID] ASC, [ApplicationUserID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Application and User must be unique', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ApplicationUserRole', @level2type = N'INDEX', @level2name = N'UK_ApplicationUserRole';

