CREATE TABLE [dbo].[QuestionAnswer] (
    [QuestionAnswerID]      INT            IDENTITY (1, 1) NOT NULL,
    [QuestionID]            INT            NOT NULL,
    [QuestionAnswerSort]    INT            CONSTRAINT [DF_QuestionAnswer_QuestionAnswerSort] DEFAULT ((0)) NOT NULL,
    [QuestionAnswerShortNM] NVARCHAR (50)  NOT NULL,
    [QuestionAnswerNM]      NVARCHAR (MAX) NOT NULL,
    [QuestionAnswerValue]   INT            CONSTRAINT [DF_QuestionAnswer_QuestionAnswerValue] DEFAULT ((0)) NOT NULL,
    [QuestionAnswerDS]      NVARCHAR (MAX) NOT NULL,
    [CommentFL]             BIT            CONSTRAINT [DF__QuestionA__Comme__267ABA7A] DEFAULT ((0)) NOT NULL,
    [ActiveFL]              BIT            CONSTRAINT [DF__QuestionA__Activ__276EDEB3] DEFAULT ((0)) NOT NULL,
    [ModifiedID]            INT            CONSTRAINT [DF__QuestionA__Modif__286302EC] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]            DATETIME       CONSTRAINT [DF__QuestionA__Modif__29572725] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [QuestionAnswer_PK] PRIMARY KEY NONCLUSTERED ([QuestionAnswerID] ASC),
    CONSTRAINT [FK_QuestionAnswer_Question] FOREIGN KEY ([QuestionID]) REFERENCES [dbo].[Question] ([QuestionID]),
    CONSTRAINT [QuestionAnswer_FK01] FOREIGN KEY ([QuestionID]) REFERENCES [dbo].[Question] ([QuestionID]),
    CONSTRAINT [UK_QuestionAnswer_ShortNMQuestionID] UNIQUE NONCLUSTERED ([QuestionID] ASC, [QuestionAnswerShortNM] ASC)
);

