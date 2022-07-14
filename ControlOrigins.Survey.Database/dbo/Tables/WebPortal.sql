CREATE TABLE [dbo].[WebPortal] (
    [WebPortalID]   INT            IDENTITY (1, 1) NOT NULL,
    [WebPortalNM]   NVARCHAR (50)  NOT NULL,
    [WebPortalDS]   NVARCHAR (MAX) NULL,
    [WebPortalURL]  NVARCHAR (250) NOT NULL,
    [WebServiceURL] NVARCHAR (250) NOT NULL,
    [ActiveFL]      BIT            CONSTRAINT [DF_WebPortal_ActiveFL] DEFAULT ((1)) NOT NULL,
    [ModifiedID]    INT            CONSTRAINT [DF_WebPortal_ModifiedID] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]    DATETIME       CONSTRAINT [DF_WebPortal_ModifiedDT] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_WebPortal] PRIMARY KEY CLUSTERED ([WebPortalID] ASC)
);

