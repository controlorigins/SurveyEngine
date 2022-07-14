CREATE TABLE [dbo].[QuestionGroup] (
    [QuestionGroupID]          INT             IDENTITY (1, 1) NOT NULL,
    [SurveyID]                 INT             NOT NULL,
    [GroupOrder]               INT             CONSTRAINT [DF__QuestionG__Group__3A81B327] DEFAULT ((0)) NOT NULL,
    [QuestionGroupShortNM]     NVARCHAR (50)   NOT NULL,
    [QuestionGroupNM]          NVARCHAR (50)   NOT NULL,
    [QuestionGroupDS]          NVARCHAR (MAX)  NULL,
    [QuestionGroupWeight]      DECIMAL (18, 4) CONSTRAINT [DF__QuestionG__Quest__3B75D760] DEFAULT ((1)) NOT NULL,
    [GroupHeader]              NVARCHAR (MAX)  NULL,
    [GroupFooter]              NVARCHAR (MAX)  NULL,
    [ModifiedID]               INT             CONSTRAINT [DF__QuestionG__Modif__3C69FB99] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]               DATETIME        CONSTRAINT [DF__QuestionG__Modif__3D5E1FD2] DEFAULT (getdate()) NOT NULL,
    [DependentQuestionGroupID] INT             NULL,
    [DependentMinScore]        DECIMAL (18, 4) NULL,
    [DependentMaxScore]        DECIMAL (18, 4) NULL,
    CONSTRAINT [PK_QuestionGroup] PRIMARY KEY NONCLUSTERED ([QuestionGroupID] ASC),
    CONSTRAINT [FK_QuestionGroup_Survey] FOREIGN KEY ([SurveyID]) REFERENCES [dbo].[Survey] ([SurveyID])
);

