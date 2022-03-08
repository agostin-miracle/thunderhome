IF OBJECT_ID ( 'dbo.PRTIPENDINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPENDINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/03/2022 09:42:48
 Objetivo : Inserção de Registros na Tabela TBTIPEND
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPENDINS
        (@TIPEND tinyint, 
        @DSCTEN varchar(30), 
        @REFCTO bit = 0, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SELECT @TIPEND = ISNULL(MAX(TIPEND),0)+1 from TBTIPEND WITH (NOLOCK)

    IF(EXISTS (SELECT 1 FROM TBTIPEND (NOLOCK) WHERE DSCTEN = @DSCTEN))
       SET @RETURN_VALUE=-3;

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBTIPEND(TIPEND, DSCTEN, REFCTO, STAREC, UPDUSU)
                        VALUES (@TIPEND, @DSCTEN, @REFCTO, @STAREC, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PRTIPENDSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPENDSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/03/2022 09:42:48
 Objetivo : Obtêm o registro de Tipo de Endereço com base no id informado
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPENDSEL
(
    @TIPEND Tinyint
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBTIPEND (NOLOCK) A

    WHERE     (A.TIPEND=@TIPEND)

GO

IF OBJECT_ID ( 'dbo.PRTIPENDSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPENDSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/03/2022 09:42:48
 Objetivo : Obtêm uma Lista dos Tipos de Endereços
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPENDSELALL
AS
    SET NOCOUNT ON
    SELECT * FROM TBTIPEND (NOLOCK) A

     ORDER BY A.TIPEND
GO

IF OBJECT_ID ( 'dbo.PRTIPENDUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPENDUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/03/2022 09:42:48
 Objetivo : Altera um registro da tabela TBTIPEND (Address Type)  de acordo com a chave primaria
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPENDUPD
        (@TIPEND tinyint, 
        @DSCTEN varchar(30), 
        @REFCTO bit = 0, 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD=GETDATE();
    IF(EXISTS (SELECT 1 FROM TBTIPEND (NOLOCK) WHERE DSCTEN = @DSCTEN AND TIPEND <>@TIPEND))
        SET @RETURN_VALUE=-3;

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBTIPEND
               SET DSCTEN=@DSCTEN
                  ,REFCTO=@REFCTO
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (TIPEND=@TIPEND)

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
