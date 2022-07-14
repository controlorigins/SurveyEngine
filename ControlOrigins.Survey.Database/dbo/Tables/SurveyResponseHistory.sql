CREATE TABLE [dbo].[SurveyResponseHistory] (
    [SurveyResponseHistoryID] INT            IDENTITY (1, 1) NOT NULL,
    [ApplicationUserID]       INT            NOT NULL,
    [SurveyResponseID]        INT            NOT NULL,
    [SurveyResponseNM]        NVARCHAR (100) NOT NULL,
    [StatusID]                INT            CONSTRAINT [DF_SurveyResponseHistory_StatusID] DEFAULT ((1)) NOT NULL,
    [QuestionGroupID]         INT            NULL,
    [UserNM]                  NVARCHAR (50)  NULL,
    [Answers]                 NVARCHAR (MAX) NULL,
    [ModifiedID]              INT            CONSTRAINT [DF__SurveyRes__Modif__3493CFA7] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]              DATETIME       CONSTRAINT [DF__SurveyRes__Modif__3587F3E0] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [aaaaaSurveyResponseHistory_PK] PRIMARY KEY NONCLUSTERED ([SurveyResponseHistoryID] ASC),
    CONSTRAINT [FK_SurveyResponseHistory_SurveyResponse] FOREIGN KEY ([SurveyResponseID]) REFERENCES [dbo].[SurveyResponse] ([SurveyResponseID])
);

