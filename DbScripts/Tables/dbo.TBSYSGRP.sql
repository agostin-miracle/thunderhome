CREATE TABLE [dbo].[TBSYSGRP] (
    [SYSGRP] INT          NOT NULL,
    [DSCGRP] VARCHAR (30) NOT NULL,
    [STAREC] TINYINT      DEFAULT ((1)) NOT NULL,
    [DATCAD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [DATUPD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [UPDUSU] INT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([SYSGRP] ASC)
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Groups',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TBSYSGRP',
    @level2type = NULL,
    @level2name = NULL