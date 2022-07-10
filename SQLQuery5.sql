﻿

SELECT NIDTAR, NIVTAR, USUPRO,APLEXP,CODTAR,STAREC,CODMOE,DATCAD,DATUPD,UPDUSU,FMTCOB,pcttar,vlrtar,tarbas,vlrinf, vlrmax, codbnd, tiplin, modcrt, tarmax, rsptar, codexp, 


FROM tbcadtar (nolock) WHERE NIDTAR=-1



SELECT A.*, PARINI = TIPLIN, PARFIN = TIPLIN, EXTVBS = VLRMAX, EXTVLR = VLRMAX, NIDCAL = NIDTAR FROM TBCADTAR (NOLOCK) A


SELECT A.CODUSU, 
		   A.CODMOV,
		   YEAR(DATCAD) NUMANO,
		   MONTH(DATCAD) NUMMES,
		   ABS(SUM(VLRMOV*SIGOPE)) AS VLRMOV,	
		   COUNT(*) QTDMOV
  FROM TBREGCCR (NOLOCK) A 
 WHERE A.STAREC=1
   AND A.INDLCT=2
   AND YEAR(DATCAD)=YEAR(GETDATE())
 GROUP BY A.CODUSU, A.CODMOV, YEAR(DATCAD), MONTH(DATCAD)          


 SELECT ISNULL(NIDTAR ,0)
  FROM TBCADTAR (NOLOCK) 

  DECLARE @RETURN_VALUE INT
 SELECT @RETURN_VALUE = COALESCE(ISNULL(NIDTAR ,0),0)
  FROM TBCADTAR (NOLOCK) 
 SELECT COALESCE(@RETURN_VALUE,0) AS RETURN_VALUE


 WHERE USUCFG =@USUPRO 
   AND CODTAR =@CODTAR 
   AND NIVCFG =@NIVTAR        
   AND CODBND =@CODBND
   AND TIPLIN =@TIPLIN
   AND MODCRT =@MODCRT



   CREATE TABLE [dbo].[TBEXPTAR](
	[NIDTAR] [int] NOT NULL,
	[NIDEXP] [int] IDENTITY(1,1) NOT NULL,
	[CODEXP] [int] NOT NULL CONSTRAINT [DF_TBEXPTAR_CODEXP]  DEFAULT ((0)),
	[LVLAPL] [tinyint] NOT NULL,
	[STAREC] [tinyint] NOT NULL CONSTRAINT [DF_TBEXPTAR_STAREC]  DEFAULT ((1)),
	[DATCAD] [datetime] NOT NULL CONSTRAINT [DF_TBEXPTAR_DATCAD]  DEFAULT (getdate()),
	[DATUPD] [datetime] NOT NULL CONSTRAINT [DF_TBEXPTAR_DATUPD]  DEFAULT (getdate()),
	[UPDUSU] [int] NOT NULL CONSTRAINT [DF_TBEXPTAR_UPDUSU]  DEFAULT ((0)),
	[LVLREG] [tinyint] NOT NULL CONSTRAINT [DF_TBEXPTAR_LVLREG]  DEFAULT ((0)),
	[QTDMIN] [smallint] NOT NULL CONSTRAINT [DF_TBEXPTAR_QTDMIN]  DEFAULT ((0)),
	[QTDMAX] [smallint] NOT NULL CONSTRAINT [DF_TBEXPTAR_QTDMAX]  DEFAULT ((0)),
	[VLRTAR] [money] NOT NULL CONSTRAINT [DF_TBEXPTAR_VLRTAR]  DEFAULT ((0)),
	[VLRMIN] [money] NOT NULL CONSTRAINT [DF__TBEXPTAR__VLRMIN__7398D00E]  DEFAULT ((0)),
	[VLRMAX] [money] NOT NULL CONSTRAINT [DF__TBEXPTAR__VLRMAX__748CF447]  DEFAULT ((0)),
 CONSTRAINT [PK_TBEXPTAR] PRIMARY KEY CLUSTERED 
(
	[CODEXP] ASC,
	[LVLAPL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SELECT A.*, DSCREC='', LGNUSU='' FROM dbo.TBEXPTAR (NOLOCK) A WHERE CODEXP=-1