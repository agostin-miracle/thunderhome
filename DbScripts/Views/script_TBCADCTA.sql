IF OBJECT_ID ( 'dbo.VWCADCTA', 'V' ) IS NOT NULL
    DROP VIEW dbo.VWCADCTA;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 11/04/2022 17:46:14
 Objetivo : View de Pesquisa de Contas
 ==================================================================================================== */
CREATE VIEW dbo.VWCADCTA
AS
    SELECT NIDCTA,A.ORGCTA
     ,A.STACTA,A.CODUSU
     ,A.TIPCTA,UPPER(CASE WHEN ORGCTA IN (1,3,4) THEN B.NOMUSU ELSE A.NOMBNF END) NOMUSU
     , CONVERT(CHAR(1),ORGCTA) + '-' + (REPLACE(LTRIM(RTRIM(NUMCTA)),'-','') + '-' + CAST(NUMDVE AS VARCHAR(1))
    + ' - ' + ISNULL(CASE WHEN ORGCTA IN (1,3,4) THEN B.NOMUSU ELSE A.NOMBNF END ,'')
    + ' - ' + ISNULL(C.DSCTAB,'')) AS DSC001
     ,(LEFT(ISNULL(CASE WHEN ORGCTA IN (1,3,4) THEN B.NOMUSU ELSE A.NOMBNF END,'') + ' - ' + ISNULL(C.DSCTAB,'')
         + replicate('_',100),100)
     + CONVERT(CHAR(1),ORGCTA) + '-' + REPLACE(LTRIM(RTRIM(NUMCTA)),'-','') + '-' + CAST(NUMDVE AS VARCHAR(1))) AS DSC002
    
      FROM TBCADCTA (NOLOCK) A
     INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.CODUSU)
      LEFT JOIN TBTABGER (NOLOCK) C ON (C.NUMTAB = 12 AND C.KEYTXT = A.NUMBCO)
     WHERE A.STAREC = 1
       AND A.STACTA = 250
       AND B.STAUSU = 61
       AND B.STAREC = 1
       AND LTRIM(RTRIM(A.NUMCTA))<>''
       AND (CASE WHEN ORGCTA IN (1,3,4) THEN B.NOMUSU ELSE A.NOMBNF END) <> ''


GO

