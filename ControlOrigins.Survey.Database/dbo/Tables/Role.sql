CREATE TABLE [dbo].[Role] (
    [RoleID]      INT            IDENTITY (1, 1) NOT NULL,
    [RoleCD]      NVARCHAR (50)  NOT NULL,
    [RoleNM]      NVARCHAR (50)  NOT NULL,
    [RoleDS]      NVARCHAR (MAX) NOT NULL,
    [ReviewLevel] INT            CONSTRAINT [DF_Role_ReviewLevel] DEFAULT ((1)) NOT NULL,
    [ReadFL]      BIT            NOT NULL,
    [UpdateFL]    BIT            NOT NULL,
    [ModifiedID]  INT            CONSTRAINT [DF__Role__ModifiedID__160F4887] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]  DATETIME       CONSTRAINT [DF__Role__ModifiedDT__17036CC0] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [aaaaaRole_PK] PRIMARY KEY NONCLUSTERED ([RoleID] ASC)
);

