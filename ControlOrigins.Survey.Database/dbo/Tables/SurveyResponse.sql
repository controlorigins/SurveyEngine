CREATE TABLE [dbo].[SurveyResponse] (
    [SurveyResponseID] INT            IDENTITY (1, 1) NOT NULL,
    [SurveyResponseNM] NVARCHAR (250) NOT NULL,
    [SurveyID]         INT            NOT NULL,
    [ApplicationID]    INT            NOT NULL,
    [AssignedUserID]   INT            NULL,
    [StatusID]         INT            NOT NULL,
    [DataSource]       NVARCHAR (250) NOT NULL,
    [ModifiedID]       INT            CONSTRAINT [DF__SurveyRes__Modif__5AEE82B9] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]       DATETIME       CONSTRAINT [DF__SurveyRes__Modif__5BE2A6F2] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [SurveyResponse_PK] PRIMARY KEY NONCLUSTERED ([SurveyResponseID] ASC),
    CONSTRAINT [FK_SurveyResponse_Application] FOREIGN KEY ([ApplicationID]) REFERENCES [dbo].[Application] ([ApplicationID]),
    CONSTRAINT [SurveyResponse_FK00] FOREIGN KEY ([SurveyID]) REFERENCES [dbo].[Survey] ([SurveyID]),
    CONSTRAINT [SurveyResponse_FK02] FOREIGN KEY ([AssignedUserID]) REFERENCES [dbo].[ApplicationUser] ([ApplicationUserID]),
    CONSTRAINT [SurveyResponse_UK] UNIQUE NONCLUSTERED ([SurveyResponseNM] ASC, [AssignedUserID] ASC)
);

