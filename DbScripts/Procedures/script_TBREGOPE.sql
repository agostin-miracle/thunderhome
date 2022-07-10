IF OBJECT_ID ( 'dbo.PRVALBOLINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRVALBOLINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/04/2002 08:43:54
 Objetivo : Inserção de Registros na Tabela TBREGOPE
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRVALBOLINS
        (        @SUBSYS tinyint, 
        @NIDREF int, 
        @NIDCAL int, 
        @IDPROC int, 
        @DATOPE datetime, 
        @CODMOV smallint, 
        @VLRBAS money = 0, 
        @VLRPCT money = 0, 
        @SIGOPE smallint = 0, 
        @VLRPCP tinyint = 0, 
        @VLROPE money = 0, 
        @USELCT smallint, 
        @LOTFIN int = 0, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBREGOPE(SUBSYS, NIDREF, NIDCAL, IDPROC, DATOPE, CODMOV, VLRBAS, VLRPCT, SIGOPE, VLRPCP, VLROPE, USELCT, LOTFIN, STAREC, UPDUSU)
                        VALUES (@SUBSYS, @NIDREF, @NIDCAL, @IDPROC, @DATOPE, @CODMOV, @VLRBAS, @VLRPCT, @SIGOPE, @VLRPCP, @VLROPE, @USELCT, @LOTFIN, @STAREC, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PRREGOPESEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGOPESEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/04/2002 08:43:54
 Objetivo : Obtêm o registro de operação selecionado
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGOPESEL
(
    @NIDOPE Integer
)
AS
    SET NOCOUNT ON
    SELECT  A.*, B.DSCMOV, DSCOPE= ISNULL(C.DSCTAB,''),  DSCREC= ISNULL(D.DSCTAB,''), LGNUSU = ISNULL(E.LGNUSU,'')
    FROM TBREGOPE (NOLOCK) A
    INNER JOIN TBTIPMOV (NOLOCK) B ON (B.CODMOV = A.CODMOV)
    INNER JOIN TBTABGER (NOLOCK) C ON (C.NUMTAB= 10 AND C.KEYCOD = A.SIGOPE)
    INNER JOIN TBTABGER (NOLOCK) D ON (D.NUMTAB=  7 AND D.KEYCOD = A.STAREC)
    LEFT JOIN TBLGNUSU (NOLOCK) E ON (E.CODUSU = A.UPDUSU AND E.REGATV=1)

    WHERE     (A.NIDOPE=@NIDOPE)

GO

IF OBJECT_ID ( 'dbo.PRCFGBOLUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCFGBOLUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/04/2002 08:43:54
 Objetivo : Altera um registro da tabela TBREGOPE (Registration of Operations)  de acordo com a chave identity
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCFGBOLUPD
        (@NIDOPE int, 
        @SUBSYS tinyint, 
        @NIDREF int, 
        @NIDCAL int, 
        @IDPROC int, 
        @DATOPE datetime, 
        @CODMOV smallint, 
        @VLRBAS money = 0, 
        @VLRPCT money = 0, 
        @SIGOPE smallint = 0, 
        @VLRPCP tinyint = 0, 
        @VLROPE money = 0, 
        @USELCT smallint, 
        @LOTFIN int = 0, 
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
            UPDATE TBREGOPE
               SET SUBSYS=@SUBSYS
                  ,NIDREF=@NIDREF
                  ,NIDCAL=@NIDCAL
                  ,IDPROC=@IDPROC
                  ,DATOPE=@DATOPE
                  ,CODMOV=@CODMOV
                  ,VLRBAS=@VLRBAS
                  ,VLRPCT=@VLRPCT
                  ,SIGOPE=@SIGOPE
                  ,VLRPCP=@VLRPCP
                  ,VLROPE=@VLROPE
                  ,USELCT=@USELCT
                  ,LOTFIN=@LOTFIN
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (NIDOPE=@NIDOPE)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @NIDOPE
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
