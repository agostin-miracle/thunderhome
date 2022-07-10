﻿CREATE TABLE [dbo].[TBREGCRT] (
    [CODCRT] INT          NOT NULL,
    [NIDCTA] INT          DEFAULT ((0)) NOT NULL,
    [USUPRO] INT          DEFAULT ((0)) NOT NULL,
    [TIPPRO] TINYINT      DEFAULT ((0)) NOT NULL,
    [ORGATV] TINYINT      DEFAULT ((0)) NOT NULL,
    [ASSUSU] INT          DEFAULT ((0)) NOT NULL,
    [SECLVL] TINYINT      DEFAULT ((3)) NOT NULL,
    [DATATV] DATETIME         NULL,
    [DATCAN] DATETIME         NULL,
    [CALMEN] TINYINT      DEFAULT ((0)) NOT NULL,
    [USUMEN] INT          DEFAULT ((0)) NOT NULL,
    [NOMPRT] VARCHAR (70) NOT NULL,
    [NUMCRT] VARCHAR (25) NOT NULL,
    [VALCRT] VARCHAR (5)  DEFAULT ('00/00') NOT NULL,
    [PSWCRT] VARCHAR (6)  DEFAULT ('0000') NOT NULL,
    [NUMCVC] SMALLINT     DEFAULT ((0)) NOT NULL,
    [STAPRO] SMALLINT     DEFAULT ((0)) NOT NULL,
    [STACRT] SMALLINT     NOT NULL,
    [NOMCR1] VARCHAR (30) DEFAULT ('') NOT NULL,
    [NOMCR2] VARCHAR (30) DEFAULT ('') NOT NULL,
    [NOMCR3] VARCHAR (30) DEFAULT ('') NOT NULL,
    [OPRCRT] TINYINT      DEFAULT ((0)) NOT NULL,
    [DATEXT] DATE         NULL,
    [CODBLO] TINYINT      DEFAULT ((0)) NOT NULL,
    [TIPPRT] TINYINT      DEFAULT ((1)) NOT NULL,
    [DSCMOT] VARCHAR (50) DEFAULT ('') NOT NULL,
    [APLCON] TINYINT      DEFAULT ((1)) NOT NULL,
    [APLSAQ] TINYINT      DEFAULT ((1)) NOT NULL,
    [LOTMIG] INT          DEFAULT ((0)) NOT NULL,
    [STAREC] TINYINT      DEFAULT ((1)) NOT NULL,
    [DATCAD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [DATUPD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [UPDUSU] INT          DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TBREGCRT] PRIMARY KEY CLUSTERED ([CODCRT] ASC),
    CONSTRAINT [FK_TBREGCRT_TBCADGER] FOREIGN KEY ([ASSUSU]) REFERENCES [dbo].[TBCADGER] ([CODUSU]),
    CONSTRAINT [FK_TBREGCRT_TBCADCRT] FOREIGN KEY ([CODCRT]) REFERENCES [dbo].[TBCADCRT] ([CODCRT]),
    CONSTRAINT [FK_TBREGCRT_TBCADSTA] FOREIGN KEY ([STACRT]) REFERENCES [dbo].[TBCADSTA] ([CODSTA])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Active Cards', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TBREGCRT';

