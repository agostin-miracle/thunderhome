IF OBJECT_ID ( 'dbo.PRTIPCTOINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPCTOINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 19/03/2022 10:01:31
 Objetivo : Inserção de Registros na Tabela TBTIPCTO
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPCTOINS
        (@TIPCTO tinyint, 
        @DSCCTO varchar(30), 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SELECT @TIPCTO = ISNULL(MAX(TIPCTO),0)+1 from TBTIPCTO WITH (NOLOCK)

    IF(EXISTS (SELECT 1 FROM TBTIPCTO (NOLOCK) WHERE DSCCTO = @DSCCTO))
        SET @RETURN_VALUE=-2;

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBTIPCTO(TIPCTO, DSCCTO, STAREC, UPDUSU)
                        VALUES (@TIPCTO, @DSCCTO, @STAREC, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PRTIPCTOUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPCTOUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 19/03/2022 10:01:31
 Objetivo : Altera um registro da tabela TBTIPCTO ()  de acordo com a chave primaria
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPCTOUPD
        (@TIPCTO tinyint, 
        @DSCCTO varchar(30), 
        @STAREC tinyint = 1, 
        @DATUPD datetime = null, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD = GETDATE();
            IF(EXISTS (SELECT 1 FROM TBTIPCTO (NOLOCK) WHERE DSCCTO = @DSCCTO AND TIPCTO!=@TIPCTO))
            SET @RETURN_VALUE=-2;

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBTIPCTO
               SET DSCCTO=@DSCCTO
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (TIPCTO=@TIPCTO)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @TIPCTO
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
