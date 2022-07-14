CREATE TABLE [dbo].[SurveyEmailTemplate] (
    [SurveyEmailTemplateID] INT            IDENTITY (1, 1) NOT NULL,
    [SurveyEmailTemplateNM] NVARCHAR (250) NOT NULL,
    [SurveyID]              INT            NOT NULL,
    [StatusID]              INT            NOT NULL,
    [SubjectTemplate]       NVARCHAR (MAX) NOT NULL,
    [EmailTemplate]         NVARCHAR (MAX) NOT NULL,
    [FromEmailAddress]      NVARCHAR (150) NOT NULL,
    [FilterCriteria]        NVARCHAR (MAX) NULL,
    [StartDT]               DATETIME       NULL,
    [EndDT]                 DATETIME       NULL,
    [Active]                BIT            CONSTRAINT [DF_SurveyEmailTemplate_Active] DEFAULT ((1)) NOT NULL,
    [SendToSupervisor]      BIT            CONSTRAINT [DF_SurveyEmailTemplate_SendToSupervisor] DEFAULT ((0)) NOT NULL,
    [ModifiedID]            INT            NOT NULL,
    [ModifiedDT]            DATETIME       NOT NULL,
    CONSTRAINT [PK_SurveyEmailTemplate] PRIMARY KEY CLUSTERED ([SurveyEmailTemplateID] ASC),
    CONSTRAINT [FK_SurveyEmailTemplate_Survey] FOREIGN KEY ([SurveyID]) REFERENCES [dbo].[Survey] ([SurveyID])
);

