CREATE TABLE [dbo].[TBCADSTA] (
    [CODSTA] SMALLINT     NOT NULL,
    [DSCSTA] VARCHAR (70) NOT NULL,
    [CODMOD] TINYINT      NOT NULL,
    [NXTSTA] INT          DEFAULT ((0)) NOT NULL,
    [CANCHG] TINYINT      DEFAULT ((0)) NOT NULL,
    [DELMEN] TINYINT      DEFAULT ((0)) NOT NULL,
    [STAREC] TINYINT      DEFAULT ((1)) NOT NULL,
    [DATCAD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [DATUPD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [UPDUSU] INT          DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TBCADSTA] PRIMARY KEY CLUSTERED ([CODSTA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Transaction Status', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TBCADSTA';

