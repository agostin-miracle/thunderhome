IF OBJECT_ID ( 'dbo.PRCADGERSELCTA', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADGERSELCTA;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm uma lista de usuários com contas virtuais ativas
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADGERSELCTA
(
    @CODUSU Integer=NULL
)
AS
    IF(@CODUSU<=0)
       SET @CODUSU=NULL;
    
    SELECT A.CODUSU, NOMUSU, REFCOD =ISNULL(B.NIDCTA,0)
      FROM TBCADGER (NOLOCK) A
      LEFT JOIN TBCADCTA (NOLOCK) B ON (B.CODUSU= A.CODUSU)
     WHERE CODATR IN (SELECT CODATR
                       FROM TBTIPATR
    				   WHERE USEACT=1) AND A.STAREC=1

 AND     (@CODUSU IS NULL OR A.CODUSU=@CODUSU)
     ORDER BY A.NOMUSU, A.CODCMF    

GO

IF OBJECT_ID ( 'dbo.PRCADGERSELUSRPRO', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADGERSELUSRPRO;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm uma lista de usuários vinculados ao gerenciamento de Produto
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADGERSELUSRPRO
(
    @CODPRO smallint=NULL
)
AS
    SELECT DISTINCT A.CODUSU, B.NOMUSU, REFCOD=A.CODPRO FROM TBUSUPRO (NOLOCK) A INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.CODUSU)

    WHERE     (@CODPRO IS NULL OR A.CODPRO=@CODPRO)
    

GO

IF OBJECT_ID ( 'dbo.PRCADGERSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADGERSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm uma lista de registros do cadastro geral conforme parâmetros informados
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADGERSELALL
(
    @CODATR smallint=NULL,
    @STAUSU smallint=NULL,
    @SRCUSU Integer=NULL,
    @NOMUSU varchar(70)=NULL,
    @STAREC Tinyint=NULL
)
AS
    IF(@NOMUSU='')
                 SET @NOMUSU=NULL
              IF(@STAREC<0)
                 SET @STAREC=NULL
              IF(@SRCUSU<0)
                 SET @SRCUSU=NULL
              IF(@CODATR<0)
                 SET @CODATR=NULL
              IF(@STAUSU<0)
                 SET @STAUSU=NULL
              SELECT * FROM VWCADGER A

    WHERE     (@CODATR IS NULL OR A.CODATR=@CODATR)
     AND (@STAUSU IS NULL OR A.STAUSU=@STAUSU)
     AND (@SRCUSU IS NULL OR A.SRCUSU=@SRCUSU)
     AND (@NOMUSU IS NULL OR A.NOMUSU LIKE '%'+@NOMUSU+'%')
     AND (@STAREC IS NULL OR A.STAREC=@STAREC)
     ORDER BY A.NOMUSU, A.CODCMF    

GO

IF OBJECT_ID ( 'dbo.PRCADGERSELRED', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADGERSELRED;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm uma lista de registros do cadastro geral simplificada por codio e descrição
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADGERSELRED
(
    @CODUSU Integer=NULL,
    @SRCUSU Integer=NULL
)
AS
    IF(@SRCUSU<=0)
                 SET @SRCUSU=NULL
              IF(@CODUSU<=0)
                 SET @CODUSU=NULL
              SELECT CODUSU, NOMUSU, REFCOD = SRCUSU FROM VWCADGER A

    WHERE     (@CODUSU IS NULL OR A.CODUSU=@CODUSU)
     AND (@SRCUSU IS NULL OR A.SRCUSU=@SRCUSU)
     ORDER BY A.NOMUSU, A.CODCMF    

GO

IF OBJECT_ID ( 'dbo.PRTIPMOVSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPMOVSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm a Lista de Operacoes
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPMOVSELALL
(
    @SUBSYS Tinyint=NULL
)
AS
    SELECT A.*, DSCREC= B.DSCTAB, DSCBLO = C.DSCTAB, DSCOPE = D.DSCTAB, LGNUSU = ISNULL(E.LGNUSU,''), DSCSYS = F.DSCTAB
      FROM TBTIPMOV (NOLOCK) A
     INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB= 7 AND B.KEYCOD=A.STAREC)
     INNER JOIN TBTABGER (NOLOCK) C ON (C.NUMTAB=29 AND C.KEYCOD=A.CNDBLO)
     INNER JOIN TBTABGER (NOLOCK) D ON (D.NUMTAB=10 AND D.KEYCOD=A.SIGOPE)
      LEFT JOIN TBLGNUSU (NOLOCK) E ON (E.CODUSU = A.UPDUSU AND E.REGATV=1)
     INNER JOIN TBTABGER (NOLOCK) F ON (F.NUMTAB=93 AND F.KEYCOD=A.SUBSYS)

    WHERE     (@SUBSYS IS NULL OR A.SUBSYS=@SUBSYS)
    

GO

IF OBJECT_ID ( 'dbo.PRCADSTASELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADSTASELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Lista Status das Transações de acordo com o módulo informado
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADSTASELALL
(
    @CODMOD smallint=NULL
)
AS
    SELECT A.*
    ,DSCCHG = CASE WHEN (A.CANCHG=1) THEN 'Sim' ELSE 'Não' END
    ,DSCDEL = CASE WHEN (A.DELMEN=1) THEN 'Sim' ELSE 'Não' END
    ,DSCREC = ISNULL(B.DSCTAB,'')
    ,DSCMOD = ISNULL(C.DSCTAB,'')
    ,LGNUSU = ISNULL(D.LGNUSU,'')
      FROM TBCADSTA (NOLOCK) A
     INNER JOIN TBTABGER B (NOLOCK) ON (B.NUMTAB =  7 AND B.KEYCOD = A.STAREC)
     INNER JOIN TBTABGER C (NOLOCK) ON (C.NUMTAB = 14 AND C.KEYCOD = A.CODMOD)
      LEFT JOIN TBLGNUSU D (NOLOCK) ON (D.CODUSU = A.UPDUSU  AND D.REGATV = 1)

    WHERE     (@CODMOD IS NULL OR A.CODMOD=@CODMOD)
    

GO

IF OBJECT_ID ( 'dbo.PRCADENDSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADENDSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm uma lista de todos os endereços de um usuário
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADENDSELALL
(
    @CODUSU Integer,
    @TIPEND smallint=NULL,
    @REGATV smallint=NULL,
    @STAREC smallint=NULL
)
AS
    IF(@TIPEND<0)
       SET @TIPEND=NULL
    IF(@REGATV<0)
       SET @REGATV=NULL
    IF(@STAREC<0)
       SET @STAREC=NULL
    
    SELECT CODEND,
        A.REGATV,
        DSCATV = CASE WHEN (A.REGATV=1) THEN 'Sim' ELSE 'Não' END,
        A.CODUSU,
    	B.NOMUSU,
        A.TIPEND,
    	C.DSCTEN,
      C.REFCTO,
        TIPLOG,
    	DSCLOG = ISNULL(D.DSCTAB,''),
        CODUFE,
    	DSCUFE = ISNULL(E.DSCTAB,''),
        DSCEND,
    	FULEND =  CASE WHEN A.TIPEND IN (3,4) THEN DSCEND ELSE
    	(ISNULL(D.DSCTAB,'') + ' ' + DSCEND +  CASE WHEN NUMEND > 0 THEN ',' + CONVERT(VARCHAR,NUMEND) ELSE '' END
    	+ ' ' + ISNULL(DSCCID,'') + ' ' + ISNULL(DSCBAI,'') +  CASE WHEN CODCEP<>'00000000' THEN ' - ' + dbo.FormatCEP(CODCEP) ELSE '' END) END,
    	DSCCPL,
        NUMEND,
        DSCCID,
        DSCBAI,
        CODCEP,
        CODPAI,
    	DSCPAI = ISNULL(F.DSCTAB,''),
        LATITU,
        LONGIT,
        A.STAREC,
        DSCREC = ISNULL(G.DSCTAB,''),
        DATCAD = FORMAT(A.DATCAD, 'dd/MM/yyyy HH:mm'),
        DATUPD = FORMAT(A.DATUPD, 'dd/MM/yyyy HH:mm'),
        A.UPDUSU,
    	LGNUSU = ISNULL(H.LGNUSU,'')
      FROM TBCADEND (NOLOCK) A
     INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.CODUSU)
     INNER JOIN TBTIPEND (NOLOCK) C ON (C.TIPEND = A.TIPEND)
      LEFT JOIN TBTABGER (NOLOCK) D ON (D.NUMTAB = 81 AND D.KEYCOD = A.TIPLOG)
     INNER JOIN TBTABGER (NOLOCK) E ON (E.NUMTAB = 2 AND E.KEYTXT = A.CODUFE)
      LEFT JOIN TBTABGER (NOLOCK) F ON (F.NUMTAB = 1 AND F.KEYCOD = A.CODPAI)
     INNER JOIN TBTABGER (NOLOCK) G ON (G.NUMTAB = 7 AND G.KEYCOD = A.STAREC)
      LEFT JOIN TBLGNUSU (NOLOCK) H ON (H.CODUSU = A.UPDUSU AND H.REGATV=1)

    WHERE     (A.CODUSU=@CODUSU)
     AND (@TIPEND IS NULL OR A.TIPEND=@TIPEND)
     AND (@REGATV IS NULL OR A.REGATV=@REGATV)
     AND (@STAREC IS NULL OR A.STAREC=@STAREC)
    

GO

IF OBJECT_ID ( 'dbo.PRCADCTOSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTOSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Seleciona todos os registros de contato do usuário fornecido
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCTOSELALL
(
    @CODUSU Integer=NULL
)
AS
    SELECT
        CODCTO
        ,A.CODUSU
    	,B.NOMUSU
        ,A.CODEND
    	,C.DSCEND
        ,A.TIPCTO
    	,D.DSCCTO
        ,A.REGATV
    	,DSCATV = CASE WHEN (A.REGATV=1) THEN 'Sim' ELSE 'Não' END
        ,A.CODPAI
    	,DSCPAI = ISNULL(E.DSCTAB,'')
        ,CODOPR
        ,DSCOPR = ISNULL(F.DSCTAB,'')
        ,NUMDDD
        ,ISWHAT
        ,NUMTEL
        ,DSCOBS
        ,A.STAREC
        ,DSCREC = ISNULL(G.DSCTAB,'')
        ,DATCAD = FORMAT(A.DATCAD, 'dd/MM/yyyy HH:mm')
        ,DATUPD = FORMAT(A.DATUPD, 'dd/MM/yyyy HH:mm')
        ,A.UPDUSU
    	,LGNUSU  = ISNULL(H.LGNUSU,'')
    
      FROM TBCADCTO (NOLOCK) A
     INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.CODUSU)
      LEFT JOIN TBCADEND (NOLOCK) C ON (C.CODEND = A.CODEND)
     INNER JOIN TBTIPCTO (NOLOCK) D ON (D.TIPCTO = A.TIPCTO)
     INNER JOIN TBTABGER (NOLOCK) E ON (E.NUMTAB= 1 AND E.KEYCOD = A.CODPAI)
      LEFT JOIN TBTABGER (NOLOCK) F ON (F.NUMTAB= 45 AND F.KEYCOD = A.CODOPR)
     INNER JOIN TBTABGER (NOLOCK) G ON (G.NUMTAB= 7 AND G.KEYCOD = A.STAREC)
      LEFT JOIN TBLGNUSU (NOLOCK) H ON (H.CODUSU = A.UPDUSU AND H.REGATV=1)

    WHERE     (@CODUSU IS NULL OR A.CODUSU=@CODUSU)
    

GO

IF OBJECT_ID ( 'dbo.PRREGCRTSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGCRTSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm uma lista de todos os cartões da base ativa conforme parâmetros de pesquisa informados
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGCRTSELALL
(
    @USUPRO Integer,
    @STACRT smallint=NULL,
    @NUMCRT varchar(16)=NULL,
    @NOMPRT varchar(100)=NULL
)
AS
    SELECT A.CODCRT
          ,A.NIDCTA
          ,A.USUPRO
          ,A.TIPPRO
          ,A.ORGATV
          ,A.ASSUSU
          ,ISNULL(B.NOMUSU,'') AS NOMUSU
          ,A.SECLVL
          ,A.DATATV
          ,CNVATV = ISNULL(FORMAT(A.DATATV, 'dd/MM/yyyy'),'')
          ,A.DATCAN
          ,CNVCAN = ISNULL(FORMAT(A.DATCAN, 'dd/MM/yyyy'),'')
          ,A.CALMEN
          ,A.USUMEN
          ,ISNULL(C.NOMUSU,'') AS DSCTOM
          ,A.NOMPRT
          ,NUMCRT = dbo.FormatCard(A.NUMCRT)
          ,A.VALCRT
          ,A.PSWCRT
          ,A.NUMCVC
          ,A.STAPRO
          ,A.STACRT
          ,ISNULL(D.DSCSTA,'') AS DSCSTA
          ,A.NOMCR1
          ,A.NOMCR2
          ,A.NOMCR3
          ,A.OPRCRT
          ,A.DATEXT
          ,A.CODBLO
          ,A.TIPPRT
          ,A.DSCMOT
          ,A.APLCON
          ,A.APLSAQ
          ,A.LOTMIG
          ,A.STAREC
          ,ISNULL(E.DSCTAB,'') AS DSCREC
          ,A.DATCAD
          ,CNVCAD = FORMAT(A.DATCAD, 'dd/MM/yyyy HH:mm')
          ,A.DATUPD
          ,CNVUPD = FORMAT(A.DATUPD, 'dd/MM/yyyy HH:mm')
          ,A.UPDUSU
          ,LGNUSU = ISNULL(F.LGNUSU,'')
          ,DSCPRO = ISNULL(I.DSCPRO,'')
    	    ,QTDMEN = ISNULL(J.QTDMEN,0)
      FROM TBREGCRT (NOLOCK) A
     INNER JOIN TBCADGER (NOLOCK) B ON (A.ASSUSU = B.CODUSU)
      LEFT JOIN TBCADGER (NOLOCK) C ON (A.USUMEN = C.CODUSU)
     INNER JOIN TBCADSTA (NOLOCK) D ON (A.STACRT = D.CODSTA)
     INNER JOIN TBTABGER (NOLOCK) E ON (E.NUMTAB = 7 AND E.KEYCOD = A.STAREC)
      LEFT JOIN TBLGNUSU (NOLOCK) F ON (F.CODUSU = A.UPDUSU AND F.REGATV=1)
     INNER JOIN TBUSUPRO (NOLOCK) G ON (G.USUPRO = A.USUPRO)
     INNER JOIN TBCADGER (NOLOCK) H ON (H.CODUSU = G.CODUSU)
     INNER JOIN TBCADPRO (NOLOCK) I ON (I.CODPRO = G.CODPRO)
      LEFT JOIN (SELECT USUPRO, CODREF, QTDMEN=COUNT(*) FROM TBREGMEN (NOLOCK) WHERE STAMEN=261 AND TIPMEN=1 AND MODREG=1 AND REGBXA=0 GROUP BY USUPRO, CODREF )J ON (J.USUPRO = A.USUPRO AND J.CODREF=A.CODCRT)
     WHERE A.CODCRT>0

 AND     (A.USUPRO=@USUPRO)
     AND (@STACRT IS NULL OR A.STACRT=@STACRT)
     AND (@NUMCRT IS NULL OR A.NUMCRT LIKE '%'+@NUMCRT+'%')
     AND (@NOMPRT IS NULL OR A.NOMPRT LIKE '%'+@NOMPRT+'%')
     ORDER BY A.NUMCRT    

GO

IF OBJECT_ID ( 'dbo.PRREGMENSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGMENSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm os registros de mensalidade conforme os parâmetros informados
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGMENSELALL
(
    @USUPRO Integer,
    @TIPMEN Tinyint,
    @STAMEN Integer=NULL,
    @CODUSU Integer=NULL
)
AS
    IF(@USUPRO<=0)
       SET @USUPRO=NULL
    
       SELECT
             A.NIDMEN
            ,A.USUPRO
        	,NOMGST = C.NOMUSU
            ,A.CODUSU
        	,D.NOMUSU
            ,A.USRREF
            ,A.TIPMEN
        	  ,DSCMEN = ISNULL(E.DSCTAB,'')
            ,A.CODREF
    		    ,REFMEN = CASE WHEN A.TIPMEN = 1 THEN ISNULL(DBO.FormatCard(I.NUMCRT),'')
    		          END
            ,A.MODREG
        	  ,DSCMOD = CASE WHEN (A.MODREG=1) THEN 'PROVISAO' ELSE 'BAIXA' END
            ,A.REGBXA
            ,A.STAMEN
          	,F.DSCSTA
            ,A.NUMMES
            ,A.NUMANO
            ,A.NUMPCL
            ,A.DATMEN
            ,A.DATVCT
            ,CNVMEN = FORMAT(A.DATMEN, 'dd/MM/yyyy')
            ,CNVVCT = FORMAT(A.DATVCT, 'dd/MM/yyyy')
            ,A.SIGOPE
            ,A.VLRCOB
        	  ,VLRMOV = (A.SIGOPE*A.VLRCOB)
            ,A.LOTINS
            ,A.NUMTEN
            ,A.STAREC
        	  ,DSCREC= ISNULL(G.DSCTAB,'')
            ,DATCAD = FORMAT(A.DATCAD, 'dd/MM/yyyy HH:mm')
            ,DATUPD = FORMAT(A.DATUPD, 'dd/MM/yyyy HH:mm')
            ,A.UPDUSU
        	,LGNUSU = ISNULL(H.LGNUSU,'')
         FROM TBREGMEN (NOLOCK) A
        INNER JOIN TBUSUPRO (NOLOCK) B ON  (B.USUPRO = A.USUPRO)
        INNER JOIN TBCADGER (NOLOCK) C ON  (C.CODUSU = A.CODUSU)
        INNER JOIN TBCADGER (NOLOCK) D ON  (D.CODUSU = A.USRREF)
        INNER JOIN TBTABGER (NOLOCK) E ON  (E.NUMTAB = 50 AND E.KEYCOD = A.TIPMEN)
        INNER JOIN TBCADSTA (NOLOCK) F ON  (F.CODSTA = A.STAMEN)
        INNER JOIN TBTABGER (NOLOCK) G ON  (G.NUMTAB =  7 AND G.KEYCOD = A.STAREC)
         LEFT JOIN TBLGNUSU (NOLOCK) H ON  (H.CODUSU = A.UPDUSU AND H.REGATV = 1)
         LEFT JOIN TBREGCRT (NOLOCK) I ON  (I.CODCRT = A.CODREF AND 1 = A.TIPMEN)

    WHERE     (A.USUPRO=@USUPRO)
     AND (A.TIPMEN=@TIPMEN)
     AND (@STAMEN IS NULL OR A.STAMEN=@STAMEN)
     AND (@CODUSU IS NULL OR A.CODUSU=@CODUSU)
    

GO

IF OBJECT_ID ( 'dbo.PRREGMENRESALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGMENRESALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm os registros de resumo de mensalidade conforme os parâmetros informados
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGMENRESALL
(
    @USUPRO Integer,
    @TIPMEN Tinyint,
    @STAMEN smallint=NULL,
    @CODUSU Integer=NULL
)
AS
    IF(@USUPRO<=0)
       SET @USUPRO=NULL
    
       SELECT
            A.USUPRO
        	,NOMGST = MAX(C.NOMUSU)
            ,A.CODUSU
        	,NOMUSU = MAX(D.NOMUSU)
            ,A.TIPMEN
        	,DSCMEN = MAX(ISNULL(E.DSCTAB,''))
            ,A.STAMEN
          	,DSCSTA = MAX(F.DSCSTA)
            ,NUMPCL  = MAX(A.NUMPCL)
            ,DATMEN  = MAX(A.DATMEN)
            ,DATVCT = MAX(A.DATVCT)
            ,CNVMEN = FORMAT(MAX(A.DATMEN), 'dd/MM/yyyy')
            ,CNVVCT = FORMAT(MAX(A.DATVCT), 'dd/MM/yyyy')
        	,VLRMOV = SUM((A.SIGOPE*A.VLRCOB))
       	    ,DSCREC= ISNULL(MAX(G.DSCTAB),'')
         FROM TBREGMEN (NOLOCK) A
        INNER JOIN TBUSUPRO (NOLOCK) B ON  (B.USUPRO = A.USUPRO)
        INNER JOIN TBCADGER (NOLOCK) C ON  (C.CODUSU = A.CODUSU)
        INNER JOIN TBCADGER (NOLOCK) D ON  (D.CODUSU = A.USRREF)
        INNER JOIN TBTABGER (NOLOCK) E ON  (E.NUMTAB = 50 AND E.KEYCOD = A.TIPMEN)
        INNER JOIN TBCADSTA (NOLOCK) F ON  (F.CODSTA = A.STAMEN)
        INNER JOIN TBTABGER (NOLOCK) G ON  (G.NUMTAB =  7 AND G.KEYCOD = A.STAREC)
         LEFT JOIN TBLGNUSU (NOLOCK) H ON  (H.CODUSU = A.UPDUSU AND H.REGATV = 1)
         LEFT JOIN TBREGCRT (NOLOCK) I ON  (I.CODCRT = A.CODREF AND 1 = A.TIPMEN)

    WHERE     (A.USUPRO=@USUPRO)
     AND (A.TIPMEN=@TIPMEN)
     AND (@STAMEN IS NULL OR A.STAMEN=@STAMEN)
     AND (@CODUSU IS NULL OR A.CODUSU=@CODUSU)
     GROUP BY A.USUPRO, A.CODUSU, A.TIPMEN, A.STAMEN, A.STAREC    

GO

IF OBJECT_ID ( 'dbo.PRCADPROSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADPROSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm uma lista de todos os produtos
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADPROSELALL
(
    @LINPRO smallint=NULL
)
AS
    SELECT A.*
          ,DSCLIN
          ,DSCREC = ISNULL(B.DSCTAB,'')
          ,LGNUSU = ISNULL(C.LGNUSU,'')
          ,DSCCDT = CASE WHEN (A.ATVCDT=1) THEN 'Sim' else 'Não' END
          ,DSCGPA = CASE WHEN (A.ATVGPA=1) THEN 'Sim' else 'Não' END
          ,DSCBNF = CASE WHEN (A.INDBNF=1) THEN 'Sim' else 'Não' END
      FROM TBCADPRO A WITH (NOLOCK)
      LEFT JOIN TBTABGER B (NOLOCK)  ON (7 = B.NUMTAB AND A.STAREC = B.KEYCOD)
      LEFT JOIN TBLGNUSU C (NOLOCK)  ON (C.CODUSU = A.UPDUSU AND C.REGATV=1)
      LEFT JOIN TBLINPRO D (NOLOCK)  ON (A.LINPRO = D.LINPRO)

    WHERE     (@LINPRO IS NULL OR A.LINPRO=@LINPRO)
     ORDER BY A.DSCPRO    

GO

IF OBJECT_ID ( 'dbo.PRCADCTASELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTASELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm todos os registros de contas virtuais registradas conforme parametro fornecido
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCTASELALL
(
    @STACTA smallint=NULL,
    @NOMUSU varchar(70)=NULL
)
AS
    IF(@STACTA<=0)
       SET @STACTA=NULL;
    IF(@NOMUSU='')
       SET @NOMUSU=NULL;
    
    SELECT
        A.NIDCTA
       ,A.CODUSU
    	 ,B.NOMUSU
       ,A.NUMAGE
       ,A.NUMBCO
    	 ,DSCBCO = ISNULL(C.DSCTAB,'')
       ,A.NUMCTA
       ,A.NUMDVE
       ,A.ORGCTA
    	 ,DSCORG = ISNULL(D.DSCTAB,'')
       ,A.TIPCTA
    	 ,E.DSCCTA
    	 ,E.TIPEXT
       ,A.STACTA
    	 ,F.DSCSTA
       ,A.DATVAL
       ,A.APLLIM
       ,A.VLRLIM
       ,A.TIPBNF
       ,DSCBNF = ISNULL(G.DSCTAB,'')
       ,CODCMF = dbo.FormatCMF(A.CODCMF)
       ,A.NOMBNF
       ,A.STAREC
       ,DSCREC = ISNULL(H.DSCTAB,'')
       ,A.UPDUSU
    	 ,LGNUSU = ISNULL(I.LGNUSU,'')
      FROM TBCADCTA (NOLOCK) A
     INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.CODUSU)
      LEFT JOIN TBTABGER (NOLOCK) C ON (C.NUMTAB = 12 AND C.KEYTXT = A.NUMBCO)
     INNER JOIN TBTABGER (NOLOCK) D ON (D.NUMTAB = 23 AND D.KEYCOD = A.ORGCTA)
     INNER JOIN TBTIPCTA (NOLOCK) E ON (E.TIPCTA = A.TIPCTA)
     INNER JOIN TBCADSTA (NOLOCK) F ON (F.CODSTA = A.STACTA)
     INNER JOIN TBTABGER (NOLOCK) G ON (G.NUMTAB = 20 AND G.KEYCOD = A.TIPBNF)
     INNER JOIN TBTABGER (NOLOCK) H ON (H.NUMTAB =  7 AND H.KEYCOD = A.STAREC)
      LEFT JOIN TBLGNUSU (NOLOCK) I ON (I.CODUSU = A.UPDUSU AND I.REGATV = 1)

    WHERE     (@STACTA IS NULL OR A.STACTA=@STACTA)
     AND  ((@NOMUSU IS NULL OR B.NOMUSU LIKE '%'+@NOMUSU+'%')
    OR(@NOMUSU IS NULL OR A.NOMBNF LIKE '%'+@NOMUSU+'%')
)    

GO

IF OBJECT_ID ( 'dbo.PRCADCVLSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCVLSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Lista de Contas vinculadas por usuário
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCVLSELALL
(
    @NOMUSU varchar(70)=NULL
)
AS
    SELECT A.*
           ,B.NUMCTA
    	   ,B.NUMAGE
    	   ,B.NUMDVE
    	   ,C.NOMUSU
    	   ,DSCBCO = NUMBCO + ' - ' + ISNULL(D.DSCTAB,''),DSCSTA
    	   ,CODCMF = DBO.FormatCMF(B.CODCMF)
    	   ,NOMBNF = CASE WHEN B.ORGCTA IN (1,3,4)  THEN 'O PROPRIO' ELSE B.NOMBNF END
         ,LGNUSU = ISNULL(F.LGNUSU,'')
         ,G.DSCCTA
      FROM TBCADCVL (NOLOCK) A
     INNER JOIN TBCADCTA (NOLOCK) B ON (B.NIDCTA = A.NIDCTA)
     INNER JOIN TBCADGER (NOLOCK) C ON (C.CODUSU = B.CODUSU)
     INNER JOIN TBTABGER (NOLOCK) D ON (D.NUMTAB=12 AND D.KEYTXT = B.NUMBCO)
     INNER JOIN TBCADSTA (NOLOCK) E ON (E.CODSTA = B.STACTA)
      LEFT JOIN TBLGNUSU (NOLOCK) F ON (F.CODUSU = A.UPDUSU AND F.REGATV = 1)
     INNER JOIN TBTIPCTA (NOLOCK) G ON (G.TIPCTA = B.TIPCTA)

    WHERE     (@NOMUSU IS NULL OR C.NOMUSU LIKE '%'+@NOMUSU+'%')
    

GO

IF OBJECT_ID ( 'dbo.PRUSUPROSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRUSUPROSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Listagem de Gestores de Produtos
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRUSUPROSELALL
(
    @CODUSU Integer=NULL,
    @CODPRO smallint=NULL
)
AS
    if(@CODUSU<=0) SET @CODUSU=NULL
    if(@CODPRO<=0) SET @CODPRO=NULL
    SELECT A.*
          , B.NOMUSU
          , C.DSCPRO
          , DSCAPL = CASE WHEN (APLINT=1 ) THEN 'Sim' ELSE 'Nao' END
          , DSCRVC = CASE WHEN (APLRVC=1 ) THEN 'Sim' ELSE 'Nao' END
          , DSCCES = CASE WHEN (APLRVC=1 ) THEN 'Sim' ELSE 'Nao' END
          , DSCTAD = CASE WHEN (APLRVC=1 ) THEN 'Sim' ELSE 'Nao' END
          , DSCREC = ISNULL(D.DSCTAB, '')
          , LGNUSU = ISNULL(E.LGNUSU,'')
          , HASCFG = CASE WHEN F.NIDCFG IS NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END
          , CNTTAR = ISNULL(G.CNTTAR,0)
          , DSCTAD = ISNULL(H.DSCTAB, '')
    
     FROM TBUSUPRO (NOLOCK) A
    INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.CODUSU)
    INNER JOIN TBCADPRO (NOLOCK) C ON (C.CODPRO = A.CODPRO)
    INNER JOIN TBTABGER (NOLOCK) D ON (D.NUMTAB = 7 AND D.KEYCOD = A.STAREC)
     LEFT JOIN TBLGNUSU (NOLOCK) E ON (E.CODUSU = A.UPDUSU AND E.REGATV=1 AND E.STAREC=1)
     LEFT JOIN TBUSUCFG (NOLOCK) F ON (F.USUCFG  = A.USUPRO AND F.NIVCFG = 1)
     LEFT JOIN (SELECT USUCFG, NIVCFG, CNTTAR =COUNT(*)  FROM TBCADTAR (NOLOCK) GROUP BY USUCFG, NIVCFG) G ON (G.USUCFG = A.USUPRO AND G.NIVCFG = 1)
    INNER JOIN TBTABGER (NOLOCK) H ON (H.NUMTAB = 912 AND H.KEYCOD = A.TIPTAD)

    WHERE     (@CODUSU IS NULL OR A.CODUSU=@CODUSU)
     AND (@CODPRO IS NULL OR A.CODPRO=@CODPRO)
    

GO

IF OBJECT_ID ( 'dbo.PRCFGBOLSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCFGBOLSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm todos os registro de configuração de boleto de acordo com os parâmetros fornecidos
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCFGBOLSELALL
(
    @NIVCFG Tinyint,
    @USUCFG Integer=NULL
)
AS
    IF(@USUCFG <0)
      SET @USUCFG=NULL
    SELECT A.*
          ,DSCTAR =ISNULL(DSCTAR,'N/A')
      	  ,NOMUSU =CASE
           WHEN A.NIVCFG=1 THEN G.NOMUSU
     		   WHEN A.NIVCFG =2 THEN F.NOMUSU
     		   ELSE
    		       'PADRÃO'
    	     END
          ,LGNUSU = ISNULL(D.LGNUSU,'')
          ,DSCREC = ISNULL(C.DSCTAB,'')
       	  ,DSCTBL = ISNULL(E.DSCTAB,'')
          ,DSCTDP = ISNULL(H.DSCTAB,'')
      FROM TBCFGBOL (NOLOCK) A
      LEFT JOIN TBTIPTAR (NOLOCK) B ON (B.CODTAR = A.CODTAR)
     INNER JOIN TBTABGER (NOLOCK) C ON (C.NUMTAB =  7 AND C.KEYCOD = A.STAREC)
      LEFT JOIN TBLGNUSU (NOLOCK) D ON (D.CODUSU = A.UPDUSU AND D.REGATV=1)
     INNER JOIN TBTABGER (NOLOCK) E ON (E.NUMTAB = 16 AND E.KEYCOD = A.TIPBOL)
      LEFT JOIN TBCADGER (NOLOCK) F ON (F.CODUSU = A.USUCFG AND 2 = A.NIVCFG )
     INNER JOIN TBTABGER (NOLOCK) H ON (H.NUMTAB = 912 AND H.KEYCOD = A.APLTDP)
      LEFT JOIN (SELECT CODUSU = USUPRO, NOMUSU = A2.NOMUSU + ' - ' + A3.DSCPRO
                   FROM TBUSUPRO (NOLOCK) A1
     INNER JOIN TBCADGER (NOLOCK) A2 ON (A2.CODUSU = A1.CODUSU)
     INNER JOIN TBCADPRO (NOLOCK) A3 ON (A3.CODPRO = A1.CODPRO)) G ON (G.CODUSU = A.USUCFG AND 1 = A.NIVCFG)

    WHERE     (A.NIVCFG=@NIVCFG)
     AND (@USUCFG IS NULL OR A.USUCFG=@USUCFG)
     ORDER BY A.USUCFG, A.NIVCFG, A.TIPBOL    

GO

IF OBJECT_ID ( 'dbo.PRREGBOLSELTCK', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGBOLSELTCK;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm informações do boleto para fatura
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGBOLSELTCK
(
    @NIDBOL Integer
)
AS
    SELECT A.*
          ,CEDNOM = B.NOMUSU
    	    ,CEDCMF = dbo.FormatCMF(B.CODCMF)
          ,SACNOM = C.NOMUSU
    	    ,SACCMF = dbo.FormatCMF(C.CODCMF)
    	    ,SACMAL = F.DSCEND
          ,AVANOM = D.NOMUSU
    	    ,AVACMF = dbo.FormatCMF(D.CODCMF)
    	    ,DSCBLT = G.DSCTAB
    	    ,H.DSCSTA
    	    ,I.VLRLIQ
    	    ,NOMEMP = J.NOMUSU
      FROM TBREGBOL (NOLOCK) A
     INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.USUCED)
     INNER JOIN TBCADGER (NOLOCK) C ON (C.CODUSU = A.USUSAC)
     INNER JOIN TBCADGER (NOLOCK) D ON (D.CODUSU = A.CODAVA)
      LEFT JOIN TBCADEND (NOLOCK) E ON (E.CODEND = A.CODEND)
     INNER JOIN (SELECT DSCEND, CODUSU FROM TBCADEND (NOLOCK) WHERE TIPEND = 3 AND STAREC=1 AND REGATV=1)  F ON (F.CODUSU = A.USUSAC)
     INNER JOIN TBTABGER (NOLOCK) G ON (G.NUMTAB=16 AND G.KEYCOD = A.TIPBOL)
     INNER JOIN TBCADSTA (NOLOCK) H ON (H.CODSTA = A.STABOL)
     INNER JOIN (SELECT NIDREF, VLRLIQ = ISNULL(SUM(VLROPE*SIGOPE),0) FROM TBREGOPE (NOLOCK) WHERE SUBSYS =2 AND STAREC=1 GROUP BY NIDREF) I ON (I.NIDREF= A.NIDBOL)
     INNER JOIN TBCADGER (NOLOCK) J ON (J.CODUSU  = A.CODEMP)
     WHERE A.STAREC=1

 AND     (A.NIDBOL=@NIDBOL)
    

GO

IF OBJECT_ID ( 'dbo.PRREGBOLSELREC', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGBOLSELREC;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm informações do boleto para obtenção de registro
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGBOLSELREC
(
    @NIDBOL Integer
)
AS
    SELECT A.NIDBOL,
        A.CODEMP,
        A.KEYBOL,
        A.SUBSYS,
        A.NIDCBL,
        A.USUPRO,
        A.USUCED,
        CEDNOM = B.NOMUSU,
        CEDCMF = B.CODCMF,
        A.USUSAC,
        SACNOM = C.NOMUSU,
        SACCMF = C.CODCMF,
        SACMAL = F.DSCEND,
        A.CODAVA,
        AVANOM = D.NOMUSU,
        AVACMF = D.CODCMF,
        A.CODMOE,
        A.TIPBOL,
    	  DATEMI = FORMAT(A.DATEMI,'dd/MM/yyyy'),
    	  DATVCT = FORMAT(A.DATVCT,'dd/MM/yyyy'),
        A.CODCED,
        A.NIDLIM,
        A.INSBC1,
        A.INSBC2,
        A.INSBC3,
        A.DSCBOL,
        A.LINDIG,
        A.CODESP,
        A.NUMCTR,
        A.NOSNUM,
        A.IMGSAV,
        A.RSPTAR,
        A.VLRBOL,
        A.VLRTAR,
        A.VLRTDP,
        A.STAREC,
        DSCLOG = ISNULL(G.DSCTAB,''),
    	  DSCEND = ISNULL(E.DSCEND,''),
    	  DSCCPL = ISNULL(E.DSCCPL,''),
    	  NUMEND = ISNULL(E.NUMEND,''),
    	  DSCCID = ISNULL(E.DSCCID,''),
    	  DSCBAI = ISNULL(E.DSCBAI,''),
    	  CODCEP=  dbo.FormatCep(E.CODCEP)
    	  ,VLRLIQ = ISNULL(H.VLRLIQ,0)
    	  ,J.DSCSTA
      FROM TBREGBOL (NOLOCK) A
     INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.USUCED)
     INNER JOIN TBCADGER (NOLOCK) C ON (C.CODUSU = A.USUSAC)
      LEFT JOIN TBCADGER (NOLOCK) D ON (D.CODUSU = A.CODAVA)
      LEFT JOIN TBCADEND (NOLOCK) E ON (E.CODEND = A.CODEND)
     INNER JOIN TBTABGER (NOLOCK) G ON (G.NUMTAB=81 AND G.KEYCOD = E.TIPLOG)
     INNER JOIN (SELECT DSCEND, CODUSU FROM TBCADEND (NOLOCK) WHERE TIPEND = 3 AND STAREC=1 AND REGATV=1)  F ON (F.CODUSU = A.USUSAC)
     INNER JOIN (SELECT NIDREF, VLRLIQ = SUM(VLROPE*SIGOPE) FROM TBREGOPE (NOLOCK) WHERE SUBSYS =2 AND STAREC=1 GROUP BY NIDREF) H ON (H.NIDREF= A.NIDBOL)
     INNER JOIN TBCADGER (NOLOCK) I ON (I.CODUSU  = A.CODEMP)
     INNER JOIN TBCADSTA (NOLOCK) J ON (J.CODSTA  = A.STABOL)
    	 WHERE A.CODVER=3  AND A.STAREC=1 AND E.CODCEP <>'00000000'

 AND     (A.NIDBOL=@NIDBOL)
    

GO

IF OBJECT_ID ( 'dbo.PRREGBOLSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGBOLSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Lista de Boletos conforme parâmetros
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGBOLSELALL
(
    @USUPRO Integer=NULL,
    @USUCED Integer=NULL,
    @USUSAC Integer=NULL,
    @CODAVA Integer=NULL,
    @STABOL smallint=NULL,
    @TIPBOL smallint=NULL,
    @NIDBOL Integer=NULL,
    @DATEMI1 varchar(10)=NULL,
    @DATEMI2 varchar(25)=NULL,
    @DATVCT1 varchar(10)=NULL,
    @DATVCT2 varchar(25)=NULL,
    @DATPGT1 varchar(10)=NULL,
    @DATPGT2 varchar(25)=NULL
)
AS
    IF(@USUPRO <=0) SET @USUPRO=NULL
    IF(@USUCED <=0) SET @USUCED=NULL
    IF(@USUSAC <=0) SET @USUSAC=NULL
    IF(@CODAVA <=0) SET @CODAVA=NULL
    IF(@STABOL <=0) SET @STABOL=NULL
    IF(@TIPBOL <=0) SET @TIPBOL=NULL
    IF(@NIDBOL <=0) SET @NIDBOL=NULL
    IF(@DATEMI1 = '') SET @DATEMI1=NULL
    IF(@DATEMI2 = '') SET @DATEMI2=NULL
    IF(@DATVCT1 = '') SET @DATVCT1=NULL
    IF(@DATVCT2 = '') SET @DATVCT2=NULL
    IF(@DATPGT1 = '') SET @DATPGT1=NULL
    IF(@DATPGT2 = '') SET @DATPGT2=NULL
    
    
    SELECT A.*,
           EMPNOM = I.NOMUSU,
           CEDNOM = B.NOMUSU,
           CEDCMF = B.CODCMF,
           SACNOM = C.NOMUSU,
           SACCMF = C.CODCMF,
           SACMAL = ISNULL(F.DSCEND,''),
           AVANOM = D.NOMUSU,
           AVACMF = D.CODCMF,
    	     VLRLIQ = ISNULL(H.VLRLIQ,0),
    	     J.DSCSTA,
    	     LGNUSU = ISNULL(K.LGNUSU,''),
           DSCBLT = L.DSCTAB -- TIPO DE BOLETO
      FROM TBREGBOL (NOLOCK) A
     INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.USUCED)
     INNER JOIN TBCADGER (NOLOCK) C ON (C.CODUSU = A.USUSAC)
      LEFT JOIN TBCADGER (NOLOCK) D ON (D.CODUSU = A.CODAVA)
      LEFT JOIN (SELECT DSCEND, CODUSU FROM TBCADEND (NOLOCK) WHERE TIPEND = 3 AND STAREC=1 AND REGATV=1)  F ON (F.CODUSU = A.USUSAC)
     INNER JOIN (SELECT NIDREF, VLRLIQ = SUM(VLROPE*SIGOPE) FROM TBREGOPE (NOLOCK) WHERE SUBSYS =2 AND STAREC=1 GROUP BY NIDREF) H ON (H.NIDREF= A.NIDBOL)
     INNER JOIN TBCADGER (NOLOCK) I ON (I.CODUSU  = A.CODEMP)
     INNER JOIN TBCADSTA (NOLOCK) J ON (J.CODSTA  = A.STABOL)
      LEFT JOIN TBLGNUSU (NOLOCK) K ON (K.CODUSU = A.UPDUSU AND K.REGATV=1)
     INNER JOIN TBTABGER (NOLOCK) L ON  (L.NUMTAB= 16 AND L.KEYCOD = A.TIPBOL)
     WHERE A.STAREC = 1
       AND ( (@DATEMI1 IS  NULL AND @DATEMI2 IS NULL) OR (A.DATEMI BETWEEN  @DATEMI1 AND @DATEMI2))
       AND ( (@DATVCT1 IS  NULL AND @DATVCT2 IS NULL) OR (A.DATVCT BETWEEN @DATVCT1 AND @DATVCT2))
       AND ( (@DATPGT1 IS  NULL AND @DATPGT2 IS NULL) OR (A.DATPGT BETWEEN @DATPGT1 AND @DATPGT2))

 AND     (@USUPRO IS NULL OR A.USUPRO=@USUPRO)
     AND (@USUCED IS NULL OR A.USUCED=@USUCED)
     AND (@USUSAC IS NULL OR A.USUSAC=@USUSAC)
     AND (@CODAVA IS NULL OR A.CODAVA=@CODAVA)
     AND (@STABOL IS NULL OR A.STABOL=@STABOL)
     AND (@TIPBOL IS NULL OR A.TIPBOL=@TIPBOL)
     AND (@NIDBOL IS NULL OR A.NIDBOL=@NIDBOL)
    

GO

IF OBJECT_ID ( 'dbo.PRREGOPESELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGOPESELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm todos os registros de operações de um susbsistema e ID de referencia específico
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGOPESELALL
(
    @SUBSYS Tinyint,
    @NIDREF Integer
)
AS
    SELECT A.*,
               B.DSCMOV,
               DSCOPE= ISNULL(C.DSCTAB,''),
               DSCREC= ISNULL(D.DSCTAB,''),
               LGNUSU = ISNULL(E.LGNUSU,''),
    		   VLRTOT = ISNULL(F.VLRTOT,0)
        FROM TBREGOPE (NOLOCK) A
        INNER JOIN TBTIPMOV (NOLOCK) B ON (B.CODMOV = A.CODMOV)
        INNER JOIN TBTABGER (NOLOCK) C ON (C.NUMTAB= 10 AND C.KEYCOD = A.SIGOPE)
        INNER JOIN TBTABGER (NOLOCK) D ON (D.NUMTAB=  7 AND D.KEYCOD = A.STAREC)
        LEFT JOIN TBLGNUSU (NOLOCK) E ON (E.CODUSU = A.UPDUSU AND E.REGATV=1)
        LEFT JOIN (SELECT SUBSYS, NIDREF, VLRTOT = COALESCE(SUM(SIGOPE * VLROPE),0)
    	             FROM TBREGOPE (NOLOCK) WHERE STAREC IN(1,2)
    				 GROUP BY SUBSYS, NIDREF) F ON (F.SUBSYS = A.SUBSYS AND F.NIDREF = A.NIDREF)

 AND     (A.SUBSYS=@SUBSYS)
     AND (A.NIDREF=@NIDREF)
     ORDER BY A.DATOPE, A.CODMOV    

GO

IF OBJECT_ID ( 'dbo.PRTIPLCTSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPLCTSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Lista de Tipos de Lançamento
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPLCTSELALL
(
    @USETRF Tinyint=NULL
)
AS
    SELECT A.*, DSCREC = B.DSCTAB, LGNUSU  = ISNULL(C.LGNUSU,'')
    	  ,DSCIBS = ISNULL(D.DSCTAB,'')
    	  ,DSCIDB = ISNULL(E.DSCTAB,'')
    	  ,DSCICR = ISNULL(F.DSCTAB,'')
    	  ,DSCTAR = ISNULL(G.DSCTAR,'')
    	  ,DSCADB = ISNULL(H.DSCTAB,'')
    	  ,DSCACR = ISNULL(I.DSCTAB,'')
          FROM TBTIPLCT (NOLOCK) A
          INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB=7 AND B.KEYCOD=A.STAREC)
          INNER JOIN TBTABGER (NOLOCK) D ON (D.NUMTAB=39 AND D.KEYCOD=A.INDLCT)
          INNER JOIN TBTABGER (NOLOCK) E ON (E.NUMTAB=39 AND E.KEYCOD=A.INDDEB)
          INNER JOIN TBTABGER (NOLOCK) F ON (F.NUMTAB=39 AND F.KEYCOD=A.INDCRD)
          INNER JOIN TBTABGER (NOLOCK) H ON (H.NUMTAB=106 AND H.KEYCOD=A.ORGDEB)
          INNER JOIN TBTABGER (NOLOCK) I ON (I.NUMTAB=106 AND I.KEYCOD=A.ORGCRD)
          LEFT JOIN TBTIPTAR (NOLOCK) G ON (G.CODTAR =A.CODTAR)
          LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV=1)

    WHERE     (@USETRF IS NULL OR A.USETRF=@USETRF)
     ORDER BY TIPLCT    

GO

IF OBJECT_ID ( 'dbo.PRCADLCTSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADLCTSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Lista de Cadastro de Lançamentos
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADLCTSELALL
(
    @TIPLCT smallint=NULL
)
AS
    SELECT A.*, DSCREC = B.DSCTAB, LGNUSU  = ISNULL(C.LGNUSU,'')
    	  ,DSCLCT
    	  ,DSCIDB = ISNULL(E.DSCTAB,'')
    	  ,DSCICR = ISNULL(F.DSCTAB,'')
    	  ,DSCMDB = ISNULL(H.DSCMOV,'')
    	  ,DSCMCR = ISNULL(I.DSCMOV,'')
    	  ,DSCTRM = ISNULL(J.DSCTAB,'')
    	  ,DSCSTA = ISNULL(K.DSCSTA,'')
     FROM TBCADLCT (NOLOCK) A
     INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB=7 AND B.KEYCOD=A.STAREC)
     INNER JOIN TBTIPLCT (NOLOCK) D ON (D.TIPLCT = A.TIPLCT)
     INNER JOIN TBTABGER (NOLOCK) E ON (E.NUMTAB=39 AND E.KEYCOD=A.INDDEB)
     INNER JOIN TBTABGER (NOLOCK) F ON (F.NUMTAB=39 AND F.KEYCOD=A.INDCRD)
      LEFT JOIN TBTIPTAR (NOLOCK) G ON (G.CODTAR =A.CODTAR)
      LEFT JOIN TBTIPMOV (NOLOCK) H ON (H.CODMOV =A.MOVDEB)
      LEFT JOIN TBTIPMOV (NOLOCK) I ON (I.CODMOV =A.MOVCRD)
     INNER JOIN TBTABGER (NOLOCK) J ON (J.NUMTAB=59 AND J.KEYCOD=A.CODTRM)
      LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV=1)
      LEFT JOIN TBCADSTA (NOLOCK) K ON (K.CODSTA = A.STAREG)

    WHERE     (@TIPLCT IS NULL OR A.TIPLCT=@TIPLCT)
     ORDER BY A.TIPLCT, A.LINLCT, A.CODTRM, A.NUMORD    

GO

IF OBJECT_ID ( 'dbo.PRREGMOVSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGMOVSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm todos os registros de transferências de acordo com os parâmetros informados
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGMOVSELALL
(
    @TIPLCT smallint=NULL,
    @CODUSU Integer=NULL,
    @NIDTRA varchar(13)=NULL,
    @STAREC Tinyint=NULL
)
AS
    IF(@CODUSU<0)
       SET @CODUSU =NULL
    IF(@NIDTRA='')
       SET @NIDTRA = NULL
    
    SELECT  A.*
       ,NUMDEB = 'LANCAMENTO : ' + ISNULL(D.DSCTAB,'N/D') + CHAR(13) + ' BANCO: ' + B.NUMBCO + ' AGÊNCIA: ' + B.NUMAGE + ' C/C :' + LTRIM(RTRIM(B.NUMCTA)) + '-' + B.NUMDVE + CHAR(13) + ' CORRENTISTA: ' + ISNULL(B1.NOMUSU,'')
       ,NUMCRD = 'LANCAMENTO : ' + ISNULL(E.DSCTAB,'N/D') + CHAR(13) + ' BANCO: ' + C.NUMBCO + ' AGÊNCIA: ' + C.NUMAGE + ' C/C :' + LTRIM(RTRIM(C.NUMCTA)) + '-' + C.NUMDVE + CHAR(13) + ' CORRENTISTA: ' + ISNULL(C1.NOMUSU,'')
       ,DSCREC = G.DSCTAB
       ,LGNUSU  = ISNULL(H.LGNUSU,'')
       ,I.DSCSTA
       ,F.DSCLCT
    FROM TBREGMOV (NOLOCK) A
    INNER JOIN TBCADCTA (NOLOCK) B ON (B.NIDCTA = A.CTADEB)
    LEFT JOIN TBCADGER (NOLOCK) B1 ON (B1.CODUSU = B.CODUSU)
    INNER JOIN TBCADCTA (NOLOCK) C ON (C.NIDCTA = A.CTACRD)
    LEFT JOIN TBCADGER (NOLOCK) C1 ON (C1.CODUSU = C.CODUSU)
    LEFT JOIN TBTABGER (NOLOCK) D ON (D.NUMTAB=39 AND D.KEYCOD = A.INDDEB)
    LEFT JOIN TBTABGER (NOLOCK) E ON (E.NUMTAB=39 AND E.KEYCOD = A.INDCRD)
    INNER JOIN TBTIPLCT (NOLOCK) F ON (F.TIPLCT = A.TIPLCT)
    INNER JOIN TBTABGER (NOLOCK) G ON (G.NUMTAB=7 AND G.KEYCOD=A.STAREC)
    LEFT JOIN TBLGNUSU (NOLOCK) H ON (C.CODUSU = A.UPDUSU AND C.REGATV=1)
    INNER JOIN TBCADSTA (NOLOCK) I ON (I.CODSTA = A.STAMOV)

    WHERE     (@TIPLCT IS NULL OR A.TIPLCT=@TIPLCT)
     AND (@CODUSU IS NULL OR A.CODUSU=@CODUSU)
     AND (@NIDTRA IS NULL OR A.NIDTRA=@NIDTRA)
     AND (@STAREC IS NULL OR A.STAREC=@STAREC)
     ORDER BY A.DATMOV DESC    

GO

IF OBJECT_ID ( 'dbo.PRCADCTASELACTCND', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTASELACTCND;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm uma lista de usuários com contas virtuais ativas
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCTASELACTCND
(
    @FUNLCT Tinyint,
    @NOMUSU varchar(70)
)
AS
    IF ((@FUNLCT = 2))
       BEGIN
          SELECT CODUSU = CASE WHEN A.NIDCTA>0 THEN A.NIDCTA ELSE B.NIDCTA END
                ,NOMUSU = dbo.FormatCard(A.NUMCRT ) + ' ' + A.NOMPRT
                ,REFCOD = A.USUPRO
                ,REFSEL = A.CODCRT
            FROM TBREGCRT (NOLOCK) A
           INNER JOIN TBCADCTA (NOLOCK) B  ON (B.CODUSU = A.ASSUSU)
           WHERE B.STAREC= 1 AND B.STACTA=250 AND B.ORGCTA=1
    	     AND A.STAREC =1
    		 AND A.STACRT=109
             AND (A.NOMPRT LIKE '%' + @NOMUSU + '%' OR B.NOMBNF LIKE '%' + @NOMUSU + '%')
       END
    ELSE IF ((@FUNLCT = 3))
       BEGIN
          SELECT NIDCTA
                ,NOMUSU ='[' + CONVERT(VARCHAR(1),ORGCTA) + '] ' + NUMCTA + ' - ' + CASE WHEN ORGCTA>1 THEN NOMBNF ELSE NOMUSU END
    		    ,REFCOD=A.CODUSU
    			,REFSEL=0
            FROM TBCADCTA (NOLOCK) A
           INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.CODUSU)
           WHERE A.STACTA=250 AND A.STAREC=1 AND A.ORGCTA = 2
    	     AND B.STAREC=1
    		 AND B.STAUSU=61
             AND (A.NOMBNF LIKE '%' + @NOMUSU + '%' OR B.NOMUSU LIKE '%' + @NOMUSU + '%')
        END
    ELSE
        BEGIN
          SELECT NIDCTA
                ,NOMUSU ='[' + CONVERT(VARCHAR(1),ORGCTA) + '] ' + NUMCTA + ' - ' + CASE WHEN ORGCTA>1 THEN NOMBNF ELSE NOMUSU END
    		    ,REFCOD=A.CODUSU
    			,REFSEL=0
            FROM TBCADCTA (NOLOCK) A
           INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.CODUSU)
           WHERE A.STACTA=250
    	     AND A.STAREC=1
    	     AND B.STAREC=1
    		 AND B.STAUSU=61
    		 AND A.ORGCTA = 1
             AND (A.NOMBNF LIKE '%' + @NOMUSU + '%' OR B.NOMUSU LIKE '%' + @NOMUSU + '%')
           ORDER BY B.NOMUSU, A.NOMBNF
        END

    

GO

