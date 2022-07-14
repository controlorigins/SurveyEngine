CREATE TABLE [dbo].[QuestionGroupMember] (
    [QuestionGroupMemberID] INT             IDENTITY (1, 1) NOT NULL,
    [QuestionGroupID]       INT             NOT NULL,
    [QuestionID]            INT             NOT NULL,
    [QuestionWeight]        DECIMAL (18, 4) CONSTRAINT [DF_QuestionGroupMember_QuestionWeight] DEFAULT ((0)) NOT NULL,
    [DisplayOrder]          INT             CONSTRAINT [DF_QuestionGroupMember_DisplayOrder] DEFAULT ((0)) NOT NULL,
    [ModifiedID]            INT             CONSTRAINT [DF__QuestionG__Modif__4222D4EF] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]            DATETIME        CONSTRAINT [DF__QuestionG__Modif__4316F928] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [QuestionGroupMember_PK] PRIMARY KEY NONCLUSTERED ([QuestionGroupMemberID] ASC),
    CONSTRAINT [QuestionGroupMember_FK01] FOREIGN KEY ([QuestionGroupID]) REFERENCES [dbo].[QuestionGroup] ([QuestionGroupID]),
    CONSTRAINT [QuestionGroupMember_FK02] FOREIGN KEY ([QuestionID]) REFERENCES [dbo].[Question] ([QuestionID])
);

