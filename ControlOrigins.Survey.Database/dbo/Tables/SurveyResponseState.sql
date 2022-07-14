CREATE TABLE [dbo].[SurveyResponseState] (
    [SurveyResponseStateID] INT            IDENTITY (1, 1) NOT NULL,
    [SurveyResponseID]      INT            NOT NULL,
    [StatusID]              INT            NOT NULL,
    [AssignedUserID]        INT            NOT NULL,
    [Active]                BIT            CONSTRAINT [DF_SurveyResponseState_Active] DEFAULT ((0)) NOT NULL,
    [EmailSent]             BIT            CONSTRAINT [DF_SurveyResponseState_EmailSent] DEFAULT ((0)) NOT NULL,
    [EmailBody]             NVARCHAR (MAX) NULL,
    [ModifiedID]            INT            NOT NULL,
    [ModifiedDT]            DATETIME       CONSTRAINT [DF_SurveyResponseState_ModifiedDT] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_SurveyResponseState] PRIMARY KEY CLUSTERED ([SurveyResponseStateID] ASC),
    CONSTRAINT [FK_SurveyResponseState_ApplicationUser] FOREIGN KEY ([AssignedUserID]) REFERENCES [dbo].[ApplicationUser] ([ApplicationUserID]),
    CONSTRAINT [FK_SurveyResponseState_lu_SurveyResponseStatus] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[lu_SurveyResponseStatus] ([StatusID]),
    CONSTRAINT [FK_SurveyResponseState_SurveyResponse] FOREIGN KEY ([SurveyResponseID]) REFERENCES [dbo].[SurveyResponse] ([SurveyResponseID])
);

