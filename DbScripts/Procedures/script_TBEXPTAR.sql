IF OBJECT_ID ( 'dbo.PREXPTARINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PREXPTARINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 13/03/2022 11:39:53
 Objetivo : Inserção de Registros na Tabela TBEXPTAR
 ==================================================================================================== */
CREATE PROCEDURE dbo.PREXPTARINS
        (        @TIPEXP smallint, 
        @LVLAPL tinyint, 
        @VLRTAR money = 0, 
        @VLRMIN money = 0, 
        @VLRMAX money = 0, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    SET @LVLAPL = 0;
    IF(EXISTS(SELECT 1 FROM TBEXPTAR (NOLOCK) WHERE TIPEXP=@TIPEXP))
       SELECT @LVLAPL = MAX(ISNULL(LVLAPL,0)) + 1  FROM TBEXPTAR (NOLOCK) WHERE TIPEXP = @TIPEXP
    ELSE
       SET @LVLAPL=1

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBEXPTAR(TIPEXP, LVLAPL, VLRTAR, VLRMIN, VLRMAX, STAREC, UPDUSU)
                        VALUES (@TIPEXP, @LVLAPL, @VLRTAR, @VLRMIN, @VLRMAX, @STAREC, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PREXPTARUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PREXPTARUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 13/03/2022 11:39:53
 Objetivo : Altera um registro da tabela TBEXPTAR (Expanded Tariff)  de acordo com a chave identity
 ==================================================================================================== */
CREATE PROCEDURE dbo.PREXPTARUPD
        (@NIDEXP int, 
        @TIPEXP smallint, 
        @LVLAPL tinyint, 
        @VLRTAR money = 0, 
        @VLRMIN money = 0, 
        @VLRMAX money = 0, 
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
            UPDATE TBEXPTAR
               SET VLRTAR=@VLRTAR
                  ,VLRMIN=@VLRMIN
                  ,VLRMAX=@VLRMAX
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (NIDEXP=@NIDEXP)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @NIDEXP
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PREXPTARSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PREXPTARSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 13/03/2022 11:39:53
 Objetivo : Seleciona todos os registros de expansão de tarifa do id de tarifação fornecido
 ==================================================================================================== */
CREATE PROCEDURE dbo.PREXPTARSELALL
(
    @TIPEXP smallint=NULL
)
AS
    SET NOCOUNT ON
    IF(@TIPEXP<=0)          
       SET @TIPEXP=NULL;
    SELECT A.*
          ,B.DSCEXP
          ,ISNULL(E.LGNUSU,'') LGNUSU 
        	,ISNULL(C.DSCTAB,'') DSCREC
      FROM TBEXPTAR (NOLOCK) A 
     INNER JOIN TBTIPEXP (NOLOCK) B ON (B.TIPEXP = A.TIPEXP)
     INNER JOIN TBTABGER (NOLOCK) C ON (C.NUMTAB = 7 AND C.KEYCOD = A.STAREC)
      LEFT JOIN TBLGNUSU (NOLOCK) E ON (E.CODUSU = B.UPDUSU AND E.REGATV=1)
     WHERE       (@TIPEXP IS NULL OR A.TIPEXP=@TIPEXP)
     ORDER BY A.TIPEXP, A.LVLAPL



GO

