CREATE TABLE [dbo].[TBCADPRO] (
    [CODPRO] SMALLINT     NOT NULL,
    [DSCPRO] VARCHAR (50) NOT NULL,
    [LINPRO] SMALLINT     NOT NULL,
    [NOMFAN] VARCHAR (50) NOT NULL,
    [ATVCDT] BIT          DEFAULT ((0)) NOT NULL,
    [ATVGPA] BIT          DEFAULT ((0)) NOT NULL,
    [INDBNF] BIT          DEFAULT ((0)) NOT NULL,
    [STAREC] TINYINT      DEFAULT ((1)) NOT NULL,
    [DATCAD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [DATUPD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [UPDUSU] INT          DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TBCADPRO] PRIMARY KEY CLUSTERED ([CODPRO] ASC),
    CONSTRAINT [FK_TBCADPRO_TBLINPRO] FOREIGN KEY ([LINPRO]) REFERENCES [dbo].[TBLINPRO] ([LINPRO])
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Products',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TBCADPRO',
    @level2type = NULL,
    @level2name = NULL