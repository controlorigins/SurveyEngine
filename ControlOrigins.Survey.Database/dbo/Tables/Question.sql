CREATE TABLE [dbo].[Question] (
    [QuestionID]      INT             IDENTITY (1, 1) NOT NULL,
    [SurveyTypeID]    INT             NOT NULL,
    [QuestionShortNM] NVARCHAR (75)   NOT NULL,
    [QuestionNM]      NVARCHAR (MAX)  NOT NULL,
    [QuestionDS]      NVARCHAR (MAX)  NOT NULL,
    [Keywords]        NVARCHAR (255)  NULL,
    [QuestionSort]    INT             CONSTRAINT [DF__Question__Questi__1ED998B2] DEFAULT ((0)) NOT NULL,
    [ReviewRoleLevel] INT             NOT NULL,
    [QuestionTypeID]  INT             NOT NULL,
    [CommentFL]       BIT             CONSTRAINT [DF__Question__Commen__1FCDBCEB] DEFAULT ((0)) NOT NULL,
    [QuestionValue]   INT             CONSTRAINT [DF_Question_QuestionValue] DEFAULT ((0)) NOT NULL,
    [UnitOfMeasureID] INT             NOT NULL,
    [ModifiedID]      INT             CONSTRAINT [DF__Question__Modifi__20C1E124] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]      DATETIME        CONSTRAINT [DF__Question__Modifi__21B6055D] DEFAULT (getdate()) NOT NULL,
    [FileData]        VARBINARY (MAX) NULL,
    CONSTRAINT [PK_Question] PRIMARY KEY NONCLUSTERED ([QuestionID] ASC),
    CONSTRAINT [FK_Question_lu_UnitOfMeasure] FOREIGN KEY ([UnitOfMeasureID]) REFERENCES [dbo].[lu_UnitOfMeasure] ([UnitOfMeasureID]),
    CONSTRAINT [FK_Question_QuestionType] FOREIGN KEY ([QuestionTypeID]) REFERENCES [dbo].[lu_QuestionType] ([QuestionTypeID]),
    CONSTRAINT [FK_Question_SurveyType] FOREIGN KEY ([SurveyTypeID]) REFERENCES [dbo].[lu_SurveyType] ([SurveyTypeID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_QuestionShortName]
    ON [dbo].[Question]([QuestionShortNM] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Question Short Name must be Unique', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Question', @level2type = N'INDEX', @level2name = N'UK_QuestionShortName';

