CREATE TABLE [dbo].[TBCADEND] (
    [CODEND] INT             IDENTITY (1, 1) NOT NULL,
    [REGATV] TINYINT         DEFAULT ((0)) NOT NULL,
    [CODUSU] INT             NOT NULL,
    [TIPEND] TINYINT         NOT NULL,
    [TIPLOG] SMALLINT        DEFAULT ((0)) NOT NULL,
    [CODUFE] CHAR (2)        DEFAULT ('  ') NOT NULL,
    [DSCEND] VARCHAR (70)    DEFAULT ('') NOT NULL,
    [DSCCPL] VARCHAR (70)    DEFAULT ('') NOT NULL,
    [NUMEND] INT             DEFAULT ((0)) NOT NULL,
    [DSCCID] VARCHAR (40)    DEFAULT ('') NOT NULL,
    [DSCBAI] VARCHAR (40)    DEFAULT ('') NOT NULL,
    [CODCEP] VARCHAR (8)     DEFAULT ('00000000') NOT NULL,
    [CODPAI] INT             DEFAULT ((55)) NOT NULL,
    [LATITU] DECIMAL (12, 9) NULL,
    [LONGIT] DECIMAL (12, 9) NULL,
    [STAREC] TINYINT         DEFAULT ((1)) NOT NULL,
    [DATCAD] DATETIME        DEFAULT (getdate()) NOT NULL,
    [DATUPD] DATETIME        DEFAULT (getdate()) NOT NULL,
    [UPDUSU] INT             DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([CODEND] ASC),
    CONSTRAINT [FK_TBCADEND_TBTIPEND] FOREIGN KEY ([TIPEND]) REFERENCES [dbo].[TBTIPEND] ([TIPEND]),
    CONSTRAINT [FK_TBCADEND_TBCADGER] FOREIGN KEY ([CODUSU]) REFERENCES [dbo].[TBCADGER] ([CODUSU])
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Address Book',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TBCADEND',
    @level2type = NULL,
    @level2name = NULL