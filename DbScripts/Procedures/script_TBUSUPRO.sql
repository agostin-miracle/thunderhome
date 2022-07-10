IF OBJECT_ID ( 'dbo.PRUSUPROINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRUSUPROINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 29/03/2022 16:33:47
 Objetivo : Inserção de Registros na Tabela TBUSUPRO
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRUSUPROINS
        (@USUPRO int, 
        @CODUSU int, 
        @CODPRO smallint, 
        @APLINT bit = 0, 
        @VLRMIN money = 0, 
        @VLRMAX money = 0, 
        @APLRVC bit = 0, 
        @REGVCT tinyint = 10, 
        @APLCES tinyint = 0, 
        @APLTAD tinyint = 0, 
        @APLMEN tinyint = 0, 
        @VLRMEN money = 0, 
        @INSBC1 varchar(30) = '', 
        @INSBC2 varchar(30) = '', 
        @INSBC3 varchar(30) = '', 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
/* Pega o proximo numero de ID */
    EXEC @USUPRO =  PRGETNXTNUM 42

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBUSUPRO(USUPRO, CODUSU, CODPRO, APLINT, VLRMIN, VLRMAX, APLRVC, REGVCT, APLCES, APLTAD, APLMEN, VLRMEN, INSBC1, INSBC2, INSBC3, STAREC, UPDUSU)
                        VALUES (@USUPRO, @CODUSU, @CODPRO, @APLINT, @VLRMIN, @VLRMAX, @APLRVC, @REGVCT, @APLCES, @APLTAD, @APLMEN, @VLRMEN, @INSBC1, @INSBC2, @INSBC3, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = @USUPRO
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRUSUPROSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRUSUPROSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 29/03/2022 16:33:47
 Objetivo : Obtêm o registro de Gerencia de Produto de acordo com ocódigo do gestor
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRUSUPROSEL
(
    @USUPRO Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBUSUPRO (nolock) A

    WHERE     (A.USUPRO=@USUPRO)

GO

IF OBJECT_ID ( 'dbo.PRUSUPROLOC', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRUSUPROLOC;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 29/03/2022 16:33:47
 Objetivo : Obtêm o Código do Gestor de Produto com base no produto e usuário
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRUSUPROLOC
(
    @CODUSU Integer,
    @CODPRO smallint
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBUSUPRO (NOLOCK) A

    WHERE     (A.CODUSU=@CODUSU)
     AND (A.CODPRO=@CODPRO)

GO

IF OBJECT_ID ( 'dbo.PRUSUPROUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRUSUPROUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 29/03/2022 16:33:47
 Objetivo : Altera um registro da tabela TBUSUPRO ()  de acordo com o campo USUPRO
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRUSUPROUPD
        (@USUPRO int, 
        @CODUSU int, 
        @CODPRO smallint, 
        @APLINT bit = 0, 
        @VLRMIN money = 0, 
        @VLRMAX money = 0, 
        @APLRVC bit = 0, 
        @REGVCT tinyint = 10, 
        @APLCES tinyint = 0, 
        @APLTAD tinyint = 0, 
        @APLMEN tinyint = 0, 
        @VLRMEN money = 0, 
        @INSBC1 varchar(30) = '', 
        @INSBC2 varchar(30) = '', 
        @INSBC3 varchar(30) = '', 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBUSUPRO
               SET APLINT=@APLINT
                  ,VLRMIN=@VLRMIN
                  ,VLRMAX=@VLRMAX
                  ,APLRVC=@APLRVC
                  ,REGVCT=@REGVCT
                  ,APLCES=@APLCES
                  ,APLTAD=@APLTAD
                  ,APLMEN=@APLMEN
                  ,VLRMEN=@VLRMEN
                  ,INSBC1=@INSBC1
                  ,INSBC2=@INSBC2
                  ,INSBC3=@INSBC3
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (USUPRO=@USUPRO)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @USUPRO
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
