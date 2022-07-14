CREATE TABLE [dbo].[SurveyResponseAnswer] (
    [SurveyAnswerID]   INT            IDENTITY (1, 1) NOT NULL,
    [SurveyResponseID] INT            NOT NULL,
    [SequenceNumber]   INT            CONSTRAINT [DF__SurveyRes__Seque__0CBAE877] DEFAULT ((1)) NOT NULL,
    [QuestionID]       INT            NOT NULL,
    [QuestionAnswerID] INT            NOT NULL,
    [AnswerType]       NVARCHAR (20)  NOT NULL,
    [AnswerQuantity]   FLOAT (53)     NULL,
    [AnswerDate]       DATETIME       NULL,
    [AnswerComment]    NVARCHAR (MAX) NULL,
    [ModifiedComment]  NVARCHAR (MAX) NULL,
    [ModifiedID]       INT            CONSTRAINT [DF__SurveyRes__Modif__0DAF0CB0] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]       DATETIME       CONSTRAINT [DF__SurveyRes__Modif__0EA330E9] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [SurveyResponseAnswer_PK] PRIMARY KEY NONCLUSTERED ([SurveyAnswerID] ASC),
    CONSTRAINT [FK_SurveyResponseAnswer_Question] FOREIGN KEY ([QuestionID]) REFERENCES [dbo].[Question] ([QuestionID]),
    CONSTRAINT [FK_SurveyResponseAnswer_QuestionAnswer] FOREIGN KEY ([QuestionAnswerID]) REFERENCES [dbo].[QuestionAnswer] ([QuestionAnswerID]),
    CONSTRAINT [FK_SurveyResponseAnswer_SurveyResponseSequence] FOREIGN KEY ([SurveyResponseID], [SequenceNumber]) REFERENCES [dbo].[SurveyResponseSequence] ([SurveyResponseID], [SequenceNumber])
);

