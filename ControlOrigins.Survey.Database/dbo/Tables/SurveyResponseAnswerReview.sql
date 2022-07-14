CREATE TABLE [dbo].[SurveyResponseAnswerReview] (
    [SurveyResponseAnswerReviewID] INT            IDENTITY (1, 1) NOT NULL,
    [SurveyAnswerID]               INT            NOT NULL,
    [ApplicationUserRoleID]        INT            NOT NULL,
    [ReviewLevel]                  INT            CONSTRAINT [DF_SurveyResponseAnswerReview_ReviewLevel] DEFAULT ((1)) NOT NULL,
    [ReviewStatusID]               INT            NOT NULL,
    [ModifiedID]                   INT            CONSTRAINT [DF_SurveyResponseAnswerReview_ModifiedID] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]                   DATETIME       CONSTRAINT [DF_SurveyResponseAnswerReview_ModifiedDT] DEFAULT (getdate()) NOT NULL,
    [ModifiedComment]              NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_SurveyResponseAnswerReview] PRIMARY KEY CLUSTERED ([SurveyResponseAnswerReviewID] ASC),
    CONSTRAINT [FK_SurveyResponseAnswerReview_ApplicationUserRole] FOREIGN KEY ([ApplicationUserRoleID]) REFERENCES [dbo].[ApplicationUserRole] ([ApplicationUserRoleID]),
    CONSTRAINT [FK_SurveyResponseAnswerReview_SurveyResponseAnswer] FOREIGN KEY ([SurveyAnswerID]) REFERENCES [dbo].[SurveyResponseAnswer] ([SurveyAnswerID])
);

