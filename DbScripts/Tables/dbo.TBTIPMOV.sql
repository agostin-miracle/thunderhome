CREATE TABLE [dbo].[TBTIPMOV] (
    [CODMOV] SMALLINT      NOT NULL,
    [DSCMOV] VARCHAR (50)  NOT NULL,
    [DSCEXT] VARCHAR (100) DEFAULT ('') NOT NULL,
    [SIGOPE] SMALLINT      DEFAULT ((0)) NOT NULL,
    [TIPVAL] INT           DEFAULT ((1)) NOT NULL,
    [NUMDIA] SMALLINT      DEFAULT ((0)) NOT NULL,
    [CODEST] SMALLINT      DEFAULT ((0)) NOT NULL,
    [CODCAN] SMALLINT      DEFAULT ((0)) NOT NULL,
    [DSCEXC] VARCHAR (100) NULL,
    [STAREC] TINYINT       DEFAULT ((1)) NOT NULL,
    [DATCAD] DATETIME      DEFAULT (getdate()) NOT NULL,
    [DATUPD] DATETIME      DEFAULT (getdate()) NOT NULL,
    [UPDUSU] INT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TBTIPMOV] PRIMARY KEY CLUSTERED ([CODMOV] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Operations', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TBTIPMOV';

