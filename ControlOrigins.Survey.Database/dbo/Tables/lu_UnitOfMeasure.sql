CREATE TABLE [dbo].[lu_UnitOfMeasure] (
    [UnitOfMeasureID] INT            IDENTITY (1, 1) NOT NULL,
    [UnitOfMeasureNM] NVARCHAR (50)  NOT NULL,
    [UnitOfMeasureDS] NVARCHAR (MAX) NULL,
    [ModifiedID]      INT            CONSTRAINT [DF__lu_UnitOf__Modif__0A9D95DB] DEFAULT ((1)) NOT NULL,
    [ModifiedDT]      DATETIME       CONSTRAINT [DF__lu_UnitOf__Modif__0B91BA14] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [aaaaaUnitOfMeasure_PK] PRIMARY KEY NONCLUSTERED ([UnitOfMeasureID] ASC)
);

