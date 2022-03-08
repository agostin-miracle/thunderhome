CREATE TABLE [dbo].[TBTIPEND] (
    [TIPEND] TINYINT      NOT NULL,
    [DSCTEN] VARCHAR (30) NOT NULL,
	REFCTO BIT DEFAULT 0 NOT NULL,
    [STAREC] TINYINT      DEFAULT ((1)) NOT NULL,
    [DATCAD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [DATUPD] DATETIME     DEFAULT (getdate()) NOT NULL,
    [UPDUSU] INT          DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TBTIPEND] PRIMARY KEY CLUSTERED ([TIPEND] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Address Type', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TBTIPEND';

