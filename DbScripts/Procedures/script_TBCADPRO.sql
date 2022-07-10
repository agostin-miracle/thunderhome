IF OBJECT_ID ( 'dbo.PRCADPROINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADPROINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 27/03/2022 20:00:19
 Objetivo : Inserção de Registros na Tabela TBCADPRO
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADPROINS
        (@CODPRO smallint, 
        @DSCPRO varchar(50), 
        @LINPRO smallint, 
        @NOMFAN varchar(50), 
        @ATVCDT bit = 0, 
        @ATVGPA bit = 0, 
        @INDBNF bit = 0, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
/* Pega o proximo numero de ID */
    EXEC @CODPRO =  PRGETNXTNUM 3

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBCADPRO(CODPRO, DSCPRO, LINPRO, NOMFAN, ATVCDT, ATVGPA, INDBNF, STAREC, UPDUSU)
                        VALUES (@CODPRO, @DSCPRO, @LINPRO, @NOMFAN, @ATVCDT, @ATVGPA, @INDBNF, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = @CODPRO
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRCADPROSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADPROSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 27/03/2022 20:00:19
 Objetivo : Obtêm o registro do produto de acorco o código de produto informado
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADPROSEL
(
    @CODPRO Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBCADPRO (nolock) A

    WHERE     (A.CODPRO=@CODPRO)

GO

IF OBJECT_ID ( 'dbo.PRCADPROUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADPROUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 27/03/2022 20:00:19
 Objetivo : Altera um registro da tabela TBCADPRO (Products)  de acordo com a chave primaria
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADPROUPD
        (@CODPRO smallint, 
        @DSCPRO varchar(50), 
        @LINPRO smallint, 
        @NOMFAN varchar(50), 
        @ATVCDT bit = 0, 
        @ATVGPA bit = 0, 
        @INDBNF bit = 0, 
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
            UPDATE TBCADPRO
               SET DSCPRO=@DSCPRO
                  ,LINPRO=@LINPRO
                  ,NOMFAN=@NOMFAN
                  ,ATVCDT=@ATVCDT
                  ,ATVGPA=@ATVGPA
                  ,INDBNF=@INDBNF
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (CODPRO=@CODPRO)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @CODPRO
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    IF(@STAREC=0 AND @RETURN_VALUE>0)
  UPDATE TBUSUPRO SET STAREC=0,DATUPD=GETDATE() WHERE CODPRO=@CODPRO
    RETURN @RETURN_VALUE
GO
