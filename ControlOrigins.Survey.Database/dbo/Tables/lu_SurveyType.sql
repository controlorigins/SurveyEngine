CREATE TABLE [dbo].[lu_SurveyType] (
    [SurveyTypeID]       INT            IDENTITY (1, 1) NOT NULL,
    [SurveyTypeShortNM]  NVARCHAR (255) NOT NULL,
    [SurveyTypeNM]       NVARCHAR (50)  NOT NULL,
    [SurveyTypeDS]       NVARCHAR (MAX) NULL,
    [SurveyTypeComment]  NVARCHAR (MAX) NULL,
    [ApplicationTypeID]  INT            NOT NULL,
    [ParentSurveyTypeID] INT            NULL,
    [MutiSequenceFL]     BIT            CONSTRAINT [DF__lu_Survey__MutiS__0C85DE4D] DEFAULT ((0)) NOT NULL,
    [ModifiedID]         INT            CONSTRAINT [DF__lu_Survey__Modif__0D7A0286] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]         DATETIME       CONSTRAINT [DF__lu_Survey__Modif__0E6E26BF] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [SurveyType_PK] PRIMARY KEY NONCLUSTERED ([SurveyTypeID] ASC),
    CONSTRAINT [UK_lu_SurveyType_SurveyTypeNM] UNIQUE NONCLUSTERED ([SurveyTypeNM] ASC),
    CONSTRAINT [UK_lu_SurveyType_SurveyTypeShortNM] UNIQUE NONCLUSTERED ([SurveyTypeShortNM] ASC)
);

