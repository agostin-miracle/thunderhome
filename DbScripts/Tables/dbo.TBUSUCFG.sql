CREATE TABLE [dbo].[TBUSUCFG] (
    [NIDCFG] INT          IDENTITY (1, 1) NOT NULL,
    [USUCFG] INT          NOT NULL,
    [NIVCFG] TINYINT      DEFAULT ((1)) NOT NULL,
    [APLTAT] BIT          DEFAULT ((0)) NOT NULL,
    [VLRTAT] MONEY        DEFAULT ((0)) NOT NULL,
    [APLMEN] BIT          DEFAULT ((0)) NOT NULL,
    [VLRMEN] MONEY        DEFAULT ((0)) NOT NULL,
    [VLRTAR] MONEY        DEFAULT ((0)) NOT NULL,
    [INSBC1] VARCHAR (50) DEFAULT ('') NULL,
    [INSBC2] VARCHAR (50) DEFAULT ('') NULL,
    [INSBC3] VARCHAR (50) DEFAULT ('') NULL,
    [STAREC] TINYINT      DEFAULT ((1)) NOT NULL,
    [DATCAD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [DATUPD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [UPDUSU] INT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([USUCFG] ASC, [NIVCFG] ASC)
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User Configuration',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TBUSUCFG',
    @level2type = NULL,
    @level2name = NULL