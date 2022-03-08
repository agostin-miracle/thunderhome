IF OBJECT_ID ( 'dbo.PRTIPUSUINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPUSUINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 21/02/2022 18:21:16
 Objetivo : Inserção de Registros na Tabela TBTIPUSU
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPUSUINS
        (@TIPUSU tinyint, 
        @DSCTUS varchar(40), 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SELECT @TIPUSU = ISNULL(MAX(TIPUSU),0)+1 from TBTIPUSU WITH (NOLOCK)

    IF(ISNULL(@DSCTUS,'')='')
            SET @RETURN_VALUE=-3;

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBTIPUSU(TIPUSU, DSCTUS, STAREC, UPDUSU)
                        VALUES (@TIPUSU, @DSCTUS, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = 1
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRTIPUSUSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPUSUSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 21/02/2022 18:21:16
 Objetivo : Obtêm o registro do Tipo de Usuário fornecido
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPUSUSEL
(
    @TIPUSU Tinyint
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBTIPUSU (NOLOCK) A

    WHERE     (A.TIPUSU=@TIPUSU)

GO

IF OBJECT_ID ( 'dbo.PRTIPUSUSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPUSUSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 21/02/2022 18:21:16
 Objetivo : Obtêm todos os registros de Tipo de Usuário
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPUSUSELALL
AS
    SET NOCOUNT ON
    SELECT * FROM TBTIPUSU (NOLOCK) A

     ORDER BY A.TIPUSU
GO

IF OBJECT_ID ( 'dbo.PRTIPUSUUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPUSUUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 21/02/2022 18:21:16
 Objetivo : Altera um registro da tabela TBTIPUSU ()  de acordo com a chave primaria
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPUSUUPD
        (@TIPUSU tinyint, 
        @DSCTUS varchar(40), 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD=GETDATE();
           
            IF(ISNULL(@DSCTUS,'')='')
               SET @RETURN_VALUE=-3;

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBTIPUSU
               SET DSCTUS=@DSCTUS
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (TIPUSU=@TIPUSU)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = 1
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
