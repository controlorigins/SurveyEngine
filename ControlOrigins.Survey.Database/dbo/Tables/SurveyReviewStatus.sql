CREATE TABLE [dbo].[SurveyReviewStatus] (
    [SurveyReviewStatusID] INT            IDENTITY (1, 1) NOT NULL,
    [SurveyID]             INT            NOT NULL,
    [ReviewStatusID]       INT            NOT NULL,
    [ReviewStatusNM]       NVARCHAR (50)  NOT NULL,
    [ReviewStatusDS]       NVARCHAR (MAX) NOT NULL,
    [ApprovedFL]           BIT            CONSTRAINT [DF_SurveyReviewStatus_ApprovedFL] DEFAULT ((0)) NOT NULL,
    [CommentFL]            BIT            NOT NULL,
    [ModifiedID]           INT            CONSTRAINT [DF_SurveyReviewStatus_ModifiedID] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]           DATETIME       CONSTRAINT [DF_SurveyReviewStatus_ModifiedDT] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_SurveyReviewStatus] PRIMARY KEY CLUSTERED ([SurveyReviewStatusID] ASC),
    CONSTRAINT [FK_SurveyReviewStatus_Survey] FOREIGN KEY ([SurveyID]) REFERENCES [dbo].[Survey] ([SurveyID]),
    CONSTRAINT [UK_SurveyReviewStatus_SurveyStatus] UNIQUE NONCLUSTERED ([SurveyID] ASC, [ReviewStatusID] ASC)
);

