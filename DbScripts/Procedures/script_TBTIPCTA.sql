IF OBJECT_ID ( 'dbo.PRTIPCTAINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPCTAINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 19/03/2022 09:50:22
 Objetivo : Inserção de Registros na Tabela TBTIPCTA
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPCTAINS
        (@TIPCTA tinyint, 
        @DSCCTA varchar(30), 
        @TIPEXT char(2) = '', 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SELECT @TIPCTA = ISNULL(MAX(TIPCTA),0)+1 from TBTIPCTA WITH (NOLOCK)

    IF(EXISTS (SELECT 1 FROM TBTIPCTA (NOLOCK) WHERE DSCCTA = @DSCCTA))
        SET @RETURN_VALUE=-2;

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBTIPCTA(TIPCTA, DSCCTA, TIPEXT, STAREC, UPDUSU)
                        VALUES (@TIPCTA, @DSCCTA, @TIPEXT, @STAREC, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PRTIPCTASEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPCTASEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 19/03/2022 09:50:22
 Objetivo : Obtêm o registro do tipo de conta informado
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPCTASEL
(
    @TIPCTA Tinyint
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBTIPCTA (nolock) A

    WHERE     (A.TIPCTA=@TIPCTA)

GO

IF OBJECT_ID ( 'dbo.PRTIPCTAUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPCTAUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 19/03/2022 09:50:22
 Objetivo : Altera um registro da tabela TBTIPCTA (Tipos de Conta)  de acordo com a chave primaria
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPCTAUPD
        (@TIPCTA tinyint, 
        @DSCCTA varchar(30), 
        @TIPEXT char(2) = '', 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD = GETDATE();
    IF(EXISTS (SELECT 1 FROM TBTIPCTA (NOLOCK) WHERE DSCCTA = @DSCCTA AND TIPCTA!=@TIPCTA))
       SET @RETURN_VALUE=-2;

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBTIPCTA
               SET DSCCTA=@DSCCTA
                  ,TIPEXT=@TIPEXT
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (TIPCTA=@TIPCTA)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @TIPCTA
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
