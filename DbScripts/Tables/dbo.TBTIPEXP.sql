CREATE TABLE [dbo].[TBTIPEXP] (
    [TIPEXP] SMALLINT     NOT NULL,
    [DSCEXP] VARCHAR (70) NOT NULL,
    [LVLREG] TINYINT      DEFAULT ((0)) NOT NULL,
    [STAREC] TINYINT      DEFAULT ((1)) NOT NULL,
    [DATCAD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [DATUPD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [UPDUSU] INT          DEFAULT ((0)) NOT NULL, 
    CONSTRAINT [PK_TBTIPEXP] PRIMARY KEY ([TIPEXP])
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Expanded Types',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TBTIPEXP',
    @level2type = NULL,
    @level2name = NULL