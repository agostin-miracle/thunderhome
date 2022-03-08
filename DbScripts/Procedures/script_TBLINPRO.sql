IF OBJECT_ID ( 'dbo.PRLINPROINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRLINPROINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/03/2022 16:58:02
 Objetivo : Inserção de Registros na Tabela TBLINPRO
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRLINPROINS
        (@LINPRO smallint, 
        @DSCLIN varchar(50), 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SELECT @LINPRO = ISNULL(MAX(LINPRO),0)+1 from TBLINPRO WITH (NOLOCK)

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBLINPRO(LINPRO, DSCLIN, STAREC, DATUPD, UPDUSU)
                        VALUES (@LINPRO, @DSCLIN, @STAREC, @DATUPD, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PRLINPROSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRLINPROSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/03/2022 16:58:03
 Objetivo : Seleciona o registro de acordo com o Código da Linha
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRLINPROSEL
(
    @LINPRO Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBLINPRO (NOLOCK) A

    WHERE     (A.LINPRO=@LINPRO)

GO

IF OBJECT_ID ( 'dbo.PRLINPROUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRLINPROUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/03/2022 16:58:03
 Objetivo : Altera um registro da tabela TBLINPRO ()  de acordo com a chave primaria
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRLINPROUPD
        (@LINPRO smallint, 
        @DSCLIN varchar(50), 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBLINPRO
               SET DSCLIN=@DSCLIN
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (LINPRO=@LINPRO)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @LINPRO
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRLINPROSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRLINPROSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/03/2022 16:58:03
 Objetivo : Obtêm todos os registros de linha de produtos existentes
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRLINPROSELALL
AS
    SET NOCOUNT ON
    SELECT *  FROM TBLINPRO (NOLOCK)
     ORDER BY LINPRO



GO

