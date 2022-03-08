IF OBJECT_ID ( 'dbo.PRUSUPROINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRUSUPROINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 08/03/2022 11:54:36
 Objetivo : Inserção de Registros na Tabela TBUSUPRO
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRUSUPROINS
        (@USUPRO int, 
        @CODUSU int, 
        @CODPRO smallint, 
        @APLINT bit = 0, 
        @VLRMIN money = 0, 
        @VLRMAX money = 0, 
        @APLRVC bit = 0, 
        @REGVCT tinyint, 
        @APLCES tinyint = 0, 
        @APLTAD tinyint = 0, 
        @APLMEN tinyint = 0, 
        @VLRMEN money = 0, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
/* Pega o proximo numero de ID */
    EXEC @USUPRO =  PRGETNXTNUM 42

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBUSUPRO(USUPRO, CODUSU, CODPRO, APLINT, VLRMIN, VLRMAX, APLRVC, REGVCT, APLCES, APLTAD, APLMEN, VLRMEN, STAREC, UPDUSU)
                        VALUES (@USUPRO, @CODUSU, @CODPRO, @APLINT, @VLRMIN, @VLRMAX, @APLRVC, @REGVCT, @APLCES, @APLTAD, @APLMEN, @VLRMEN, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = @USUPRO
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRUSUPROSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRUSUPROSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 08/03/2022 11:54:37
 Objetivo : Obtêm o registro de Gerencia de Produto de acordo com ocódigo do gestor
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRUSUPROSEL
(
    @USUPRO Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBUSUPRO (nolock) A

    WHERE     (A.USUPRO=@USUPRO)

GO

IF OBJECT_ID ( 'dbo.PRUSUPROLOC', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRUSUPROLOC;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 08/03/2022 11:54:37
 Objetivo : Obtêm o Código do Gestor de Produto com base no produto e usuário
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRUSUPROLOC
(
    @CODUSU Integer,
    @CODPRO smallint
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBUSUPRO (NOLOCK) A

    WHERE     (A.CODUSU=@CODUSU)
     AND (A.CODPRO=@CODPRO)

GO

IF OBJECT_ID ( 'dbo.PRUSUPROUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRUSUPROUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 08/03/2022 11:54:37
 Objetivo : Altera um registro da tabela TBUSUPRO ()  de acordo com o campo USUPRO
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRUSUPROUPD
        (@USUPRO int, 
        @CODUSU int, 
        @CODPRO smallint, 
        @APLINT bit = 0, 
        @VLRMIN money = 0, 
        @VLRMAX money = 0, 
        @APLRVC bit = 0, 
        @REGVCT tinyint, 
        @APLCES tinyint = 0, 
        @APLTAD tinyint = 0, 
        @APLMEN tinyint = 0, 
        @VLRMEN money = 0, 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBUSUPRO
               SET APLINT=@APLINT
                  ,VLRMIN=@VLRMIN
                  ,VLRMAX=@VLRMAX
                  ,APLRVC=@APLRVC
                  ,REGVCT=@REGVCT
                  ,APLCES=@APLCES
                  ,APLTAD=@APLTAD
                  ,APLMEN=@APLMEN
                  ,VLRMEN=@VLRMEN
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (USUPRO=@USUPRO)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @USUPRO
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRUSUPROSELGSTLIN', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRUSUPROSELGSTLIN;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 08/03/2022 11:54:37
 Objetivo : Obtêm uma lista de gestores, se informada a linha de produto, extrai somente os gestores ligados à linha
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRUSUPROSELGSTLIN
(
    @LINPRO smallint=NULL
)
AS
    SET NOCOUNT ON
    SELECT CODUSU = A.USUPRO, NOMUSU = LTRIM(RTRIM(NOMUSU)) + ' - ' + C.DSCPRO, REFCOD= A.CODUSU
      FROM TBUSUPRO (NOLOCK) A
     INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.CODUSU)
     INNER JOIN TBCADPRO (NOLOCK) C ON (C.CODPRO = A.CODPRO)
     WHERE       (@LINPRO IS NULL OR C.LINPRO=@LINPRO)
     ORDER BY C.DSCPRO



GO

IF OBJECT_ID ( 'dbo.PRUSUPROSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRUSUPROSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 08/03/2022 11:54:37
 Objetivo : Obtêm uma lista de todos os registros de Gestores de Produtos de acordo com o usuario e/ou  o produto informado
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRUSUPROSELALL
(
    @CODUSU Integer=NULL,
    @CODPRO smallint=NULL
)
AS
    SET NOCOUNT ON
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
     FROM TBUSUPRO (NOLOCK) A 
    INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.CODUSU)
    INNER JOIN TBCADPRO (NOLOCK) C ON (C.CODPRO = A.CODPRO)
    INNER JOIN TBTABGER (NOLOCK) D ON (D.NUMTAB = 7 AND D.KEYCOD = A.STAREC)
     LEFT JOIN TBLGNUSU (NOLOCK) E ON (E.CODUSU = A.UPDUSU AND E.REGATV=1 AND E.STAREC=1)          
     LEFT JOIN TBUSUCFG (NOLOCK) F ON (F.USUCFG  = A.USUPRO AND F.NIVCFG = 1)
     LEFT JOIN (SELECT USUPRO, NIVTAR, CNTTAR =COUNT(*)  FROM TBCADTAR (NOLOCK) GROUP BY USUPRO, NIVTAR) G ON (G.USUPRO = A.USUPRO AND G.NIVTAR = 1)
     WHERE       (@CODUSU IS NULL OR A.CODUSU=@CODUSU)
       AND (@CODPRO IS NULL OR A.CODPRO=@CODPRO)



GO

