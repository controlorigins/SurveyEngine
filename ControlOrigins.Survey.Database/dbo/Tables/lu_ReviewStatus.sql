CREATE TABLE [dbo].[lu_ReviewStatus] (
    [ReviewStatusID] INT            IDENTITY (1, 1) NOT NULL,
    [ReviewStatusNM] NVARCHAR (50)  NOT NULL,
    [ReviewStatusDS] NVARCHAR (MAX) NOT NULL,
    [ApprovedFL]     BIT            CONSTRAINT [DF_lu_ReviewStatus_ApprovedFL] DEFAULT ((0)) NOT NULL,
    [CommentFL]      BIT            NOT NULL,
    [ModifiedID]     INT            CONSTRAINT [DF_lu_ReviewStatus_ModifiedID] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]     DATETIME       CONSTRAINT [DF_lu_ReviewStatus_ModifiedDT] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_lu_ReviewStatus] PRIMARY KEY CLUSTERED ([ReviewStatusID] ASC)
);

