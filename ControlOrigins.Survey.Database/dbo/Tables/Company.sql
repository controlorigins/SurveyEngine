CREATE TABLE [dbo].[Company] (
    [CompanyID]                 INT            IDENTITY (1, 1) NOT NULL,
    [CompanyNM]                 NVARCHAR (50)  NOT NULL,
    [CompanyCD]                 NVARCHAR (10)  NOT NULL,
    [CompanyDS]                 NVARCHAR (255) NULL,
    [Title]                     NVARCHAR (255) NOT NULL,
    [Theme]                     NVARCHAR (10)  NOT NULL,
    [DefaultTheme]              NVARCHAR (10)  NOT NULL,
    [GalleryFolder]             NVARCHAR (50)  NOT NULL,
    [SiteURL]                   NVARCHAR (255) NOT NULL,
    [Address1]                  NVARCHAR (100) NOT NULL,
    [Address2]                  NVARCHAR (100) NULL,
    [City]                      NVARCHAR (50)  NOT NULL,
    [State]                     NVARCHAR (20)  NOT NULL,
    [Country]                   NVARCHAR (50)  NOT NULL,
    [PostalCode]                NVARCHAR (20)  NOT NULL,
    [FaxNumber]                 NVARCHAR (30)  NULL,
    [PhoneNumber]               NVARCHAR (30)  NULL,
    [DefaultPaymentTerms]       NVARCHAR (255) NULL,
    [DefaultInvoiceDescription] NVARCHAR (MAX) NULL,
    [ActiveFL]                  BIT            NOT NULL,
    [Component]                 NVARCHAR (50)  NULL,
    [FromEmail]                 NVARCHAR (50)  NULL,
    [SMTP]                      NVARCHAR (50)  NULL,
    [ModifiedDT]                DATETIME       CONSTRAINT [DF_Company_ModifiedDT] DEFAULT (getdate()) NOT NULL,
    [ModifiedID]                INT            CONSTRAINT [DF_Company_ModifiedID] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED ([CompanyID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_CompanyCD]
    ON [dbo].[Company]([CompanyCD] ASC);

