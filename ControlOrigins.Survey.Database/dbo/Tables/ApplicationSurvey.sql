CREATE TABLE [dbo].[ApplicationSurvey] (
    [ApplicationSurveyID] INT      IDENTITY (1, 1) NOT NULL,
    [ApplicationID]       INT      NOT NULL,
    [SurveyID]            INT      NOT NULL,
    [DefaultRoleID]       INT      NOT NULL,
    [ModifiedID]          INT      NOT NULL,
    [ModifiedDT]          DATETIME CONSTRAINT [DF__Applicati__Modif__2180FB33] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [ApplicationSurvey_PK] PRIMARY KEY NONCLUSTERED ([ApplicationSurveyID] ASC),
    CONSTRAINT [FK_ApplicationSurvey_Role] FOREIGN KEY ([DefaultRoleID]) REFERENCES [dbo].[Role] ([RoleID]),
    CONSTRAINT [SystemSurvey_FK00] FOREIGN KEY ([SurveyID]) REFERENCES [dbo].[Survey] ([SurveyID]),
    CONSTRAINT [SystemSurvey_FK01] FOREIGN KEY ([ApplicationID]) REFERENCES [dbo].[Application] ([ApplicationID]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_ApplicationSurvey]
    ON [dbo].[ApplicationSurvey]([ApplicationID] ASC, [SurveyID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Appliation and Survey must be unique', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ApplicationSurvey', @level2type = N'INDEX', @level2name = N'UK_ApplicationSurvey';

