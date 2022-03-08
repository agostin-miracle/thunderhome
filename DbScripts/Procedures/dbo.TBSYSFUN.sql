CREATE TABLE [dbo].[TBSYSFUN] (
    [SYSFUN] BIGINT        NOT NULL,
    [SYSMTH] VARCHAR (100) NOT NULL,
    [SYSPRC] VARCHAR (100) DEFAULT ('') NOT NULL,
    [SYSDSC] VARCHAR (255) NOT NULL,
    [STAREC] TINYINT       DEFAULT ((1)) NOT NULL,
    [DATCAD] DATETIME      DEFAULT (getdate()) NOT NULL,
    [DATUPD] DATETIME      DEFAULT (getdate()) NOT NULL,
    [UPDUSU] INT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([SYSFUN] ASC)
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'System Features',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TBSYSFUN',
    @level2type = NULL,
    @level2name = NULL