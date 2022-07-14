CREATE TABLE [dbo].[tblFiles] (
    [id]          INT             IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50)    NOT NULL,
    [ContentType] VARCHAR (50)    NOT NULL,
    [Data]        VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_tblFiles] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [UK_tblFiles_Type_Name] UNIQUE NONCLUSTERED ([Name] ASC, [ContentType] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name must be unique for each file type', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tblFiles', @level2type = N'CONSTRAINT', @level2name = N'UK_tblFiles_Type_Name';

