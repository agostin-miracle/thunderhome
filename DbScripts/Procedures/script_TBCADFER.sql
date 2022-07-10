IF OBJECT_ID ( 'dbo.PRCADFERINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADFERINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 11/04/2022 14:27:22
 Objetivo : Inserção de Registros na Tabela TBCADFER
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADFERINS
        (        @CODUFE char(2), 
        @DATMOV datetime, 
        @DSCFER varchar(50), 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBCADFER(CODUFE, DATMOV, DSCFER, STAREC, UPDUSU)
                        VALUES (@CODUFE, @DATMOV, @DSCFER, @STAREC, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PRCADFERUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADFERUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 11/04/2022 14:27:22
 Objetivo : Altera um registro da tabela TBCADFER (HolyDays)  de acordo com a chave identity
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADFERUPD
        (@NIDHOL int, 
        @CODUFE char(2), 
        @DATMOV datetime, 
        @DSCFER varchar(50), 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD=GETDATE();

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBCADFER
               SET DSCFER=@DSCFER
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (NIDHOL=@NIDHOL)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @NIDHOL
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
