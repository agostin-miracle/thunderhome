CREATE TABLE [dbo].[TBCADFER] (
    [DATSCO] TINYINT  DEFAULT ((1)) NOT NULL,
    [CODUFE] CHAR (2) DEFAULT ('SP') NOT NULL,
    [DATMOV] DATETIME NOT NULL,
    [STAREC] TINYINT  DEFAULT ((1)) NOT NULL,
    [DATCAD] DATETIME DEFAULT (getdate()) NOT NULL,
    [DATUPD] DATETIME DEFAULT (getdate()) NOT NULL,
    [UPDUSU] INT      DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TBCADFER] PRIMARY KEY CLUSTERED ([CODUFE] ASC, [DATMOV] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'HolyDays', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TBCADFER';

