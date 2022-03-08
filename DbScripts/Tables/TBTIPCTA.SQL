CREATE TABLE [dbo].[TBTIPCTA] (
    [TIPCTA] TINYINT      NOT NULL,
    [DSCCTA] VARCHAR (30) NOT NULL,
    [TIPEXT] CHAR (2)     DEFAULT ('') NOT NULL,
    [STAREC] TINYINT      DEFAULT ((1)) NOT NULL,
    [DATCAD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [DATUPD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [UPDUSU] INT          DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TBTIPCTA] PRIMARY KEY CLUSTERED ([TIPCTA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipos de Conta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TBTIPCTA';

