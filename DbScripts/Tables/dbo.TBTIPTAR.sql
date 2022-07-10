CREATE TABLE [dbo].[TBTIPTAR] (
    [CODTAR] TINYINT    NOT NULL,
    [DSCTAR] VARCHAR(50) NOT NULL,
    [CODMOV] SMALLINT   NOT NULL,
    [STAREC] TINYINT    DEFAULT ((1)) NOT NULL,
    [DATCAD] DATETIME   DEFAULT (getdate()) NOT NULL,
    [DATUPD] DATETIME   DEFAULT (getdate()) NOT NULL,
    [UPDUSU] INT        DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([CODTAR] ASC), 
    CONSTRAINT [FK_TBTIPTAR_TPTIPMOV] FOREIGN KEY ([CODMOV]) REFERENCES [TBTIPMOV]([CODMOV])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tariff Type', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TBTIPTAR';

