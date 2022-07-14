CREATE TABLE [dbo].[lu_QuestionType] (
    [QuestionTypeID] INT            IDENTITY (1, 1) NOT NULL,
    [QuestionTypeCD] NVARCHAR (255) NOT NULL,
    [QuestionTypeDS] NVARCHAR (MAX) NOT NULL,
    [ControlName]    NVARCHAR (255) NOT NULL,
    [AnswerDataType] NVARCHAR (255) NOT NULL,
    [ModifiedID]     INT            CONSTRAINT [DF__QuestionT__Modif__47DBAE45] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]     DATETIME       CONSTRAINT [DF__QuestionT__Modif__48CFD27E] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [aaaaaQuestionType_PK] PRIMARY KEY NONCLUSTERED ([QuestionTypeID] ASC)
);

