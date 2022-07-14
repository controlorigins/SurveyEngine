CREATE TABLE [dbo].[SurveyResponseSequence] (
    [SurveyResponseSequenceID] INT            IDENTITY (1, 1) NOT NULL,
    [SurveyResponseID]         INT            NOT NULL,
    [SequenceNumber]           INT            DEFAULT ((1)) NOT NULL,
    [SequenceText]             NVARCHAR (255) NULL,
    [ModifiedID]               INT            DEFAULT ((1)) NOT NULL,
    [ModifiedDT]               DATETIME       DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [aaaaaSurveyResponseSequence_PK] PRIMARY KEY NONCLUSTERED ([SurveyResponseSequenceID] ASC),
    CONSTRAINT [SurveyResponseSequence_FK00] FOREIGN KEY ([SurveyResponseID]) REFERENCES [dbo].[SurveyResponse] ([SurveyResponseID]),
    CONSTRAINT [UK_SurveyResponseSequence] UNIQUE NONCLUSTERED ([SurveyResponseID] ASC, [SequenceNumber] ASC)
);

