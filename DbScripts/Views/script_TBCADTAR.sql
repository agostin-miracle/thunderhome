IF OBJECT_ID ( 'dbo.VWREGCCRTAR', 'V' ) IS NOT NULL
    DROP VIEW dbo.VWREGCCRTAR;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 30/03/2022 13:56:18
 Objetivo : View de Informações consolidados de Tipo de Movimento por Usuário
 ==================================================================================================== */
CREATE VIEW dbo.VWREGCCRTAR
AS
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


GO

IF OBJECT_ID ( 'dbo.VWCADTAR', 'V' ) IS NOT NULL
    DROP VIEW dbo.VWCADTAR;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 30/03/2022 13:56:18
 Objetivo : View de Informações consolidadas de informações do cadastro de tarifas
 ==================================================================================================== */
CREATE VIEW dbo.VWCADTAR
AS
    SELECT A.*
      ,NOMUSU = CASE WHEN A.NIVCFG=2 THEN ISNULL(B.NOMUSU,'') ELSE ISNULL(D.NOMUSU,'' ) + ' - ' + ISNULL(E.DSCPRO,'') END
      ,DSCCFG = ISNULL(F.DSCTAB,'')
      ,DSCRSP = ISNULL(G.DSCTAB,'')
      ,H.DSCTAR
      ,DSCLIN = ISNULL(J.DSCTAB,'')
      ,DSCBND = ISNULL(I.DSCTAB,'')
      ,K.DSCMOD
      ,L.DSCEXP
      ,DSCMOE = ISNULL(M.DSCTAB,'')
      ,DSCCOB = ISNULL(N.DSCTAB,'')
      ,DSCREC = ISNULL(O.DSCTAB,'')
      ,LGNUSU = ISNULL(P.LGNUSU,'')
      FROM TBCADTAR (NOLOCK) A
      LEFT JOIN TBCADGER B (NOLOCK) ON (B.CODUSU = A.USUCFG AND 2 = A.NIVCFG)
      LEFT JOIN TBUSUPRO C (NOLOCK) ON (C.USUPRO = A.USUCFG AND 1 = A.NIVCFG)
      LEFT JOIN TBCADGER D (NOLOCK) ON (D.CODUSU = C.CODUSU)
      LEFT JOIN TBCADPRO E (NOLOCK) ON (E.CODPRO = C.CODPRO)
      INNER JOIN TBTABGER F (NOLOCK) ON (F.NUMTAB=947 AND F.KEYCOD=A.NIVCFG)
      INNER JOIN TBTABGER G (NOLOCK) ON (G.NUMTAB=105 AND G.KEYCOD=A.RSPTAR)
      INNER JOIN TBTIPTAR H (NOLOCK) ON (H.CODTAR = A.CODTAR)
      INNER JOIN TBTABGER I (NOLOCK) ON (I.NUMTAB=63 AND I.KEYCOD=A.CODBND)
      INNER JOIN TBTABGER J (NOLOCK) ON (J.NUMTAB=44 AND J.KEYCOD=A.TIPLIN)
      INNER JOIN TBMODTAR K (NOLOCK) ON (K.MODCRT = A.MODCRT)
      INNER JOIN TBTIPEXP L (NOLOCK) ON (L.TIPEXP = A.CODEXP)
       LEFT JOIN TBTABGER M (NOLOCK) ON (M.NUMTAB=298 AND M.KEYCOD=A.CODMOE)
      INNER JOIN TBTABGER N (NOLOCK) ON (N.NUMTAB=30 AND N.KEYCOD=A.FMTCOB)
      INNER JOIN TBTABGER O (NOLOCK) ON (O.NUMTAB=7 AND O.KEYCOD=A.STAREC)
       LEFT JOIN TBLGNUSU P (NOLOCK) ON (P.CODUSU = A.UPDUSU AND P.REGATV = 1)


GO

