CREATE TABLE [dbo].[TBTIPATR] (
    [CODATR] SMALLINT     NOT NULL,
    [DSCATR] VARCHAR (40) NOT NULL,
    [USELGN] BIT          DEFAULT ((1)) NOT NULL,
    [USEACT] BIT          DEFAULT ((0)) NOT NULL,
    [STAREC] TINYINT      DEFAULT ((1)) NOT NULL,
    [DATCAD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [DATUPD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [UPDUSU] INT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([CODATR] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Atributo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TBTIPATR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Status do Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TBTIPATR', @level2type = N'COLUMN', @level2name = N'STAREC';


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Define se o atributo permite a criação automática de conta virtual',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TBTIPATR',
    @level2type = N'COLUMN',
    @level2name = N'USEACT'