CREATE TABLE [dbo].[SurveyStatus] (
    [SurveyStatusID]       INT            IDENTITY (1, 1) NOT NULL,
    [SurveyID]             INT            NOT NULL,
    [StatusID]             INT            NOT NULL,
    [StatusNM]             NVARCHAR (50)  NOT NULL,
    [StatusDS]             NVARCHAR (MAX) NOT NULL,
    [EmailTemplate]        NVARCHAR (MAX) NOT NULL,
    [EmailSubjectTemplate] NVARCHAR (MAX) NOT NULL,
    [PreviousStatusID]     INT            NOT NULL,
    [NextStatusID]         INT            NOT NULL,
    [ModifiedID]           INT            NOT NULL,
    [ModifiedDT]           DATETIME       NOT NULL,
    CONSTRAINT [PK_SurveyStatus] PRIMARY KEY CLUSTERED ([SurveyStatusID] ASC),
    CONSTRAINT [FK_SurveyStatus_Survey] FOREIGN KEY ([SurveyID]) REFERENCES [dbo].[Survey] ([SurveyID]),
    CONSTRAINT [UK_SurveyStatus_SurveyStatus] UNIQUE NONCLUSTERED ([SurveyID] ASC, [StatusID] ASC)
);

