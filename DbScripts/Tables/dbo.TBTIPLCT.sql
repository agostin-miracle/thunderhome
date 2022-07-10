CREATE TABLE [dbo].[TBTIPLCT] (
    [TIPLCT] SMALLINT     DEFAULT ((0)) NOT NULL,
    [DSCLCT] VARCHAR (50) DEFAULT ('') NOT NULL,
    [INDLCT] SMALLINT     DEFAULT ((0)) NOT NULL,
    [INDDEB] SMALLINT     DEFAULT ((0)) NOT NULL,
    [ORGDEB] TINYINT      DEFAULT ((0)) NOT NULL,
    [INDCRD] SMALLINT     DEFAULT ((0)) NOT NULL,
    [ORGCRD] TINYINT      DEFAULT ((0)) NOT NULL,
    [CODTAR] SMALLINT     DEFAULT ((0)) NOT NULL,
    [USETRF] TINYINT      DEFAULT ((0)) NOT NULL,
    [STAREC] TINYINT      DEFAULT ((1)) NOT NULL,
    [DATCAD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [DATUPD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [UPDUSU] INT          DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TBTIPLCT] PRIMARY KEY CLUSTERED ([TIPLCT] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Account Entry Type', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TBTIPLCT';

