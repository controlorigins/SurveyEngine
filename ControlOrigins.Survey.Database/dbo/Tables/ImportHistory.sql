CREATE TABLE [dbo].[ImportHistory] (
    [ImportHistoryID] INT            IDENTITY (1, 1) NOT NULL,
    [FileName]        NVARCHAR (150) NOT NULL,
    [ImportType]      NVARCHAR (50)  NOT NULL,
    [NumberOfRows]    INT            NOT NULL,
    [ImportLog]       NVARCHAR (MAX) NULL,
    [ModifiedID]      INT            CONSTRAINT [DF_ImportHistory_ModifiedID] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]      DATETIME       CONSTRAINT [DF_ImportHistory_ModifiedDT] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ImportHistory] PRIMARY KEY CLUSTERED ([ImportHistoryID] ASC)
);

