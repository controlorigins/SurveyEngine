CREATE TABLE [dbo].[Survey] (
    [SurveyID]            INT            IDENTITY (1, 1) NOT NULL,
    [SurveyTypeID]        INT            NOT NULL,
    [UseQuestionGroupsFL] BIT            CONSTRAINT [DF__Survey__UseSurve__245D67DE] DEFAULT ((0)) NOT NULL,
    [SurveyNM]            NVARCHAR (50)  NOT NULL,
    [SurveyShortNM]       NVARCHAR (50)  NOT NULL,
    [SurveyDS]            NVARCHAR (MAX) NOT NULL,
    [CompletionMessage]   NVARCHAR (MAX) NOT NULL,
    [ResponseNMTemplate]  NVARCHAR (100) NULL,
    [ReviewerAccountNM]   NVARCHAR (50)  NULL,
    [AutoAssignFilter]    NVARCHAR (MAX) NULL,
    [StartDT]             DATE           NULL,
    [EndDT]               DATE           NULL,
    [ParentSurveyID]      INT            NULL,
    [ModifiedID]          INT            CONSTRAINT [DF__Survey__Modified__25518C17] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]          DATETIME       CONSTRAINT [DF__Survey__Modified__2645B050] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [Survey_PK] PRIMARY KEY NONCLUSTERED ([SurveyID] ASC),
    CONSTRAINT [FK_Survey_SurveyType] FOREIGN KEY ([SurveyTypeID]) REFERENCES [dbo].[lu_SurveyType] ([SurveyTypeID])
);

