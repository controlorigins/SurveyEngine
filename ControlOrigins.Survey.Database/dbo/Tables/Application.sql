CREATE TABLE [dbo].[Application] (
    [ApplicationID]      INT            IDENTITY (1, 1) NOT NULL,
    [ApplicationNM]      NVARCHAR (250) NOT NULL,
    [ApplicationCD]      NVARCHAR (50)  NOT NULL,
    [ApplicationShortNM] NVARCHAR (50)  NOT NULL,
    [ApplicationTypeID]  INT            NOT NULL,
    [ApplicationDS]      NVARCHAR (MAX) NULL,
    [MenuOrder]          INT            CONSTRAINT [DF__System__MenuOrde__00200768] DEFAULT ((0)) NOT NULL,
    [ApplicationFolder]  NVARCHAR (150) CONSTRAINT [DF_Application_ApplicationFolder] DEFAULT (N'SurveyAdmin') NOT NULL,
    [DefaultPageID]      INT            CONSTRAINT [DF_Application_DefaultPageID] DEFAULT ((63)) NOT NULL,
    [CompanyID]          INT            NULL,
    [ModifiedID]         INT            CONSTRAINT [DF__System__Modified__01142BA1] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]         DATETIME       CONSTRAINT [DF__System__Modified__02084FDA] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [Application_PK] PRIMARY KEY NONCLUSTERED ([ApplicationID] ASC),
    CONSTRAINT [FK_Application_Company] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[Company] ([CompanyID]),
    CONSTRAINT [System_FK01] FOREIGN KEY ([ApplicationTypeID]) REFERENCES [dbo].[lu_ApplicationType] ([ApplicationTypeID]) ON DELETE CASCADE ON UPDATE CASCADE
);

