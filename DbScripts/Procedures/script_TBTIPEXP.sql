IF OBJECT_ID ( 'dbo.PRTIPEXPINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPEXPINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 14/03/2022 20:25:16
 Objetivo : Inserção de Registros na Tabela TBTIPEXP
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPEXPINS
        (@TIPEXP smallint, 
        @DSCEXP varchar(70), 
        @LVLREG tinyint = 0, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SELECT @TIPEXP = ISNULL(MAX(TIPEXP),0)+1 from TBTIPEXP WITH (NOLOCK)

    IF(@LVLREG<0)
       SET @RETURN_VALUE=-4;
           
    IF(EXISTS (SELECT 1 FROM TBTIPEXP (NOLOCK) WHERE DSCEXP= @DSCEXP))
       SET @RETURN_VALUE=-3;

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBTIPEXP(TIPEXP, DSCEXP, LVLREG, STAREC, UPDUSU)
                        VALUES (@TIPEXP, @DSCEXP, @LVLREG, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = @TIPEXP
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRTIPEXPUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPEXPUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 14/03/2022 20:25:16
 Objetivo : Altera um registro da tabela TBTIPEXP ()  de acordo com a chave primaria
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPEXPUPD
        (@TIPEXP smallint, 
        @DSCEXP varchar(70), 
        @LVLREG tinyint = 0, 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD=GETDATE();
    
    IF(@LVLREG<0)
       SET @RETURN_VALUE=-4;
    
    IF(EXISTS (SELECT 1 FROM TBTIPEXP (NOLOCK) WHERE DSCEXP = @DSCEXP AND TIPEXP <>@TIPEXP))
        SET @RETURN_VALUE=-3;

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBTIPEXP
               SET DSCEXP=@DSCEXP
                  ,LVLREG=@LVLREG
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (TIPEXP=@TIPEXP)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @TIPEXP
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRTIPEXPSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPEXPSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 14/03/2022 20:25:16
 Objetivo : Obtêm uma Lista dos Tipos de Expansão
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPEXPSELALL
AS
    SET NOCOUNT ON
    SELECT A.*, DSCREC= ISNULL(B.DSCTAB,''), LGNUSU = ISNULL(C.LGNUSU,''), DSCREG = D.DSCTAB
    FROM TBTIPEXP (NOLOCK) A
    INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB =7 AND B.KEYCOD=A.STAREC)
     LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV =1)
    INNER JOIN TBTABGER (NOLOCK) D ON (D.NUMTAB =94 AND D.KEYCOD=A.LVLREG)
     ORDER BY A.TIPEXP



GO

