CREATE TABLE [dbo].[lu_SurveyResponseStatus] (
    [StatusID]         INT            IDENTITY (1, 1) NOT NULL,
    [StatusNM]         NVARCHAR (50)  NOT NULL,
    [StatusDS]         NVARCHAR (MAX) NOT NULL,
    [EmailTemplate]    NVARCHAR (MAX) NULL,
    [PreviousStatusID] INT            NOT NULL,
    [NextStatusID]     INT            NOT NULL,
    [ModifiedID]       INT            CONSTRAINT [DF_lu_SurveyResponseStatus_ModifiedID] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]       DATETIME       CONSTRAINT [DF_lu_SurveyResponseStatus_ModifiedDT] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_lu_SurveyResponseStatus] PRIMARY KEY CLUSTERED ([StatusID] ASC)
);

