CREATE TABLE [dbo].[SurveyResponseAnswer_Error] (
    [SurveyAnswer_ErrorID] INT            IDENTITY (1, 1) NOT NULL,
    [SurveyResponseID]     INT            NOT NULL,
    [SequenceNumber]       INT            CONSTRAINT [DF__SurveyRes__Seque__1332DBDC] DEFAULT ((1)) NOT NULL,
    [QuestionID]           INT            NOT NULL,
    [QuestionAnswerID]     INT            NULL,
    [AnswerType]           NVARCHAR (MAX) NULL,
    [AnswerQuantity]       NVARCHAR (MAX) NULL,
    [AnswerDate]           NVARCHAR (MAX) NULL,
    [AnswerComment]        NVARCHAR (MAX) NULL,
    [ErrorCode]            NVARCHAR (MAX) NULL,
    [ErrorMessage]         NVARCHAR (MAX) NULL,
    [ProgramName]          NVARCHAR (MAX) NOT NULL,
    [ModifiedID]           INT            CONSTRAINT [DF__SurveyRes__Modif__14270015] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]           DATETIME       CONSTRAINT [DF__SurveyRes__Modif__151B244E] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [aaaaaSurveyResponseAnswer_Error_PK] PRIMARY KEY NONCLUSTERED ([SurveyAnswer_ErrorID] ASC)
);

