CREATE TABLE [dbo].[lu_ApplicationType] (
    [ApplicationTypeID] INT            IDENTITY (1, 1) NOT NULL,
    [ApplicationTypeNM] NVARCHAR (50)  NOT NULL,
    [ApplicationTypeDS] NVARCHAR (MAX) NULL,
    [ModifiedID]        INT            CONSTRAINT [DF__lu_Applic__Modif__114A936A] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]        DATETIME       CONSTRAINT [DF__lu_Applic__Modif__123EB7A3] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [ApplicationType_PK] PRIMARY KEY NONCLUSTERED ([ApplicationTypeID] ASC),
    CONSTRAINT [UK_lu_ApplicationType_ApplicationTypeNM] UNIQUE NONCLUSTERED ([ApplicationTypeNM] ASC)
);

