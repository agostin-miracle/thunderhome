IF OBJECT_ID ( 'dbo.PRTABGERINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTABGERINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 23/03/2022 14:58:11
 Objetivo : Inserção de Registros na Tabela TBTABGER
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTABGERINS
        (        @NUMTAB int, 
        @USECOD tinyint = 1, 
        @KEYCOD int = 0, 
        @KEYTXT char(10) = null, 
        @NUMREF int = 0, 
        @DSCTAB varchar(100), 
        @USRDSP tinyint = 1, 
        @IDEPRE tinyint = 0, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    IF(@DSCTAB!='')
      BEGIN
        IF(EXISTS(SELECT 1 FROM TBTABGER (NOLOCK) WHERE KEYCOD=@KEYCOD AND KEYTXT=@KEYTXT AND DSCTAB=@DSCTAB))
           SET @RETURN_VALUE=-2
      END

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBTABGER(NUMTAB, USECOD, KEYCOD, KEYTXT, NUMREF, DSCTAB, USRDSP, IDEPRE, STAREC, UPDUSU)
                        VALUES (@NUMTAB, @USECOD, @KEYCOD, @KEYTXT, @NUMREF, @DSCTAB, @USRDSP, @IDEPRE, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = @@IDENTITY
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRTABGERSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTABGERSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 23/03/2022 14:58:11
 Objetivo : Seleciona o registro de acordo com o id de registro da tabela geral
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTABGERSEL
(
    @KEYTAB Integer
)
AS
    SET NOCOUNT ON
    SELECT A.*, DSCREC = B.DSCTAB, LGNUSU  = ISNULL(C.LGNUSU,'')
      FROM TBTABGER (NOLOCK) A
     INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB=7 AND B.KEYCOD=A.STAREC)
      LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV=1)

    WHERE     (A.KEYTAB=@KEYTAB)

GO

IF OBJECT_ID ( 'dbo.PRTABGERUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTABGERUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 23/03/2022 14:58:11
 Objetivo : Altera um registro da tabela TBTABGER ()  de acordo com a chave identity
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTABGERUPD
        (@KEYTAB int, 
        @NUMTAB int, 
        @USECOD tinyint = 1, 
        @KEYCOD int = 0, 
        @KEYTXT char(10) = null, 
        @NUMREF int = 0, 
        @DSCTAB varchar(100), 
        @USRDSP tinyint = 1, 
        @IDEPRE tinyint = 0, 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBTABGER
               SET NUMTAB=@NUMTAB
                  ,USECOD=@USECOD
                  ,KEYCOD=@KEYCOD
                  ,KEYTXT=@KEYTXT
                  ,NUMREF=@NUMREF
                  ,DSCTAB=@DSCTAB
                  ,USRDSP=@USRDSP
                  ,IDEPRE=@IDEPRE
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (KEYTAB=@KEYTAB)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @KEYTAB
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRTABGERCOD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTABGERCOD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 23/03/2022 14:58:11
 Objetivo : Seleciona todos os registros de um Tipo de tabela informado
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTABGERCOD
(
    @NUMTAB Integer=NULL
)
AS
    SET NOCOUNT ON
    SELECT A.*, DSCREC = B.DSCTAB, LGNUSU  = ISNULL(C.LGNUSU,'')
      FROM TBTABGER (NOLOCK) A
     INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB=7 AND B.KEYCOD=A.STAREC)
      LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV=1)          
    WHERE A.STAREC NOT IN (0,13)
 AND       (@NUMTAB IS NULL OR A.NUMTAB=@NUMTAB)
     ORDER BY A.KEYCOD



GO

IF OBJECT_ID ( 'dbo.PRTABGERFNDKEY', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTABGERFNDKEY;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 23/03/2022 14:58:11
 Objetivo : Obtêm o Id de Registro de uma Tabela Geral Baseada no Código Chave da Tabela
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTABGERFNDKEY
(
    @NUMTAB Integer,
    @KEYCOD Integer
)
AS
    SET NOCOUNT ON

    SELECT COALESCE(ISNULL(KEYTAB,0),0) AS KEYTAB FROM TBTABGER (NOLOCK) A WHERE STAREC NOT IN (0,13)
 AND     (A.NUMTAB=@NUMTAB)
     AND (A.KEYCOD=@KEYCOD)

GO

IF OBJECT_ID ( 'dbo.PRTABGERFNDTXT', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTABGERFNDTXT;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 23/03/2022 14:58:11
 Objetivo : Obtêm o Id de Registro de uma Tabela Geral Baseada no Código Chave Texto da Tabela
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTABGERFNDTXT
(
    @NUMTAB Integer,
    @KEYTXT varchar
)
AS
    SET NOCOUNT ON

    SELECT COALESCE(ISNULL(KEYTAB,0),0) AS KEYTAB FROM TBTABGER (NOLOCK) A WHERE STAREC NOT IN (0,13)
 AND     (A.NUMTAB=@NUMTAB)
     AND (A.KEYTXT=@KEYTXT)

GO

