IF OBJECT_ID ( 'dbo.PRVALBOLINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRVALBOLINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 16/03/2022 17:19:19
 Objetivo : Inserção de Registros na Tabela TBVALBOL
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRVALBOLINS
        (        @NIDBOL int, 
        @TIPVAL tinyint, 
        @VLRBAS money = 0, 
        @VLRPCT money, 
        @SIGOPE smallint = 0, 
        @VLROPE money = 0, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBVALBOL(NIDBOL, TIPVAL, VLRBAS, VLRPCT, SIGOPE, VLROPE, STAREC, UPDUSU)
                        VALUES (@NIDBOL, @TIPVAL, @VLRBAS, @VLRPCT, @SIGOPE, @VLROPE, @STAREC, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PRVALBOLSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRVALBOLSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 16/03/2022 17:19:20
 Objetivo : Seleciona o registro de acordo com o codigo do produto
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRVALBOLSEL
(
    @NIDCBL Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBVALBOL (NOLOCK) A

    WHERE     (A.NIDCBL=@NIDCBL)

GO

IF OBJECT_ID ( 'dbo.PRCFGBOLUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCFGBOLUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 16/03/2022 17:19:20
 Objetivo : Altera um registro da tabela TBVALBOL ()  de acordo com a chave identity
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCFGBOLUPD
        (@NIDVBL int, 
        @NIDBOL int, 
        @TIPVAL tinyint, 
        @VLRBAS money = 0, 
        @VLRPCT money, 
        @SIGOPE smallint = 0, 
        @VLROPE money = 0, 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD=GETDATE()

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBVALBOL
               SET VLRBAS=@VLRBAS
                  ,VLRPCT=@VLRPCT
                  ,SIGOPE=@SIGOPE
                  ,VLROPE=@VLROPE
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (NIDVBL=@NIDVBL)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @NIDVBL
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRVALBOLSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRVALBOLSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 16/03/2022 17:19:20
 Objetivo : Seleciona todos os registros de um determinado boleto
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRVALBOLSELALL
AS
    SET NOCOUNT ON
    SELECT A.*
            ,DSCVAL = ISNULL(B.DSCTAB,'')
            ,DSCREC = ISNULL(C.DSCTAB,'')
            ,LGNUSU = ISNULL(D.LGNUSU,'')
    		,DSCOPE = ISNULL(E.DSCTAB,'')
      FROM TBVALBOL (NOLOCK) A
      INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB=7 AND B.KEYCOD=A.TIPVAL)
      INNER JOIN TBTABGER (NOLOCK) C ON (C.NUMTAB =  7 AND C.KEYCOD = A.STAREC)
       LEFT JOIN TBLGNUSU (NOLOCK) D ON (D.CODUSU = A.UPDUSU AND D.REGATV=1)
      INNER JOIN TBTABGER (NOLOCK) E ON (E.NUMTAB = 10 AND E.KEYCOD = A.SIGOPE)
     ORDER BY A.USUCFG, A.NIVCFG, A.TIPBOL



GO

