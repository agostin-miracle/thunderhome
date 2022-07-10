IF OBJECT_ID ( 'dbo.PRTIPLCTINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPLCTINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 22/04/2022 18:33:55
 Objetivo : Inserção de Registros na Tabela TBTIPLCT
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPLCTINS
        (@TIPLCT smallint = 0, 
        @DSCLCT varchar(50) = '', 
        @INDLCT smallint = 0, 
        @INDDEB smallint = 0, 
        @ORGDEB tinyint = 0, 
        @INDCRD smallint = 0, 
        @ORGCRD tinyint = 0, 
        @CODTAR smallint = 0, 
        @USETRF tinyint, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    IF(EXISTS (SELECT 1 FROM TBTIPLCT (NOLOCK) WHERE DSCLCT = @DSCLCT))
       SET @RETURN_VALUE=-3;

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBTIPLCT(TIPLCT, DSCLCT, INDLCT, INDDEB, ORGDEB, INDCRD, ORGCRD, CODTAR, USETRF, STAREC, UPDUSU)
                        VALUES (@TIPLCT, @DSCLCT, @INDLCT, @INDDEB, @ORGDEB, @INDCRD, @ORGCRD, @CODTAR, @USETRF, @STAREC, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PRTIPLCTUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPLCTUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 22/04/2022 18:33:56
 Objetivo : Altera um registro da tabela TBTIPLCT (Account Entry Type)  de acordo com a chave primaria
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPLCTUPD
        (@TIPLCT smallint = 0, 
        @DSCLCT varchar(50) = '', 
        @INDLCT smallint = 0, 
        @INDDEB smallint = 0, 
        @ORGDEB tinyint = 0, 
        @INDCRD smallint = 0, 
        @ORGCRD tinyint = 0, 
        @CODTAR smallint = 0, 
        @USETRF tinyint, 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD=GETDATE();
    IF(EXISTS (SELECT 1 FROM TBTIPLCT (NOLOCK) WHERE DSCLCT = @DSCLCT AND TIPLCT <>@TIPLCT))
        SET @RETURN_VALUE=-3;

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBTIPLCT
               SET DSCLCT=@DSCLCT
                  ,INDLCT=@INDLCT
                  ,INDDEB=@INDDEB
                  ,ORGDEB=@ORGDEB
                  ,INDCRD=@INDCRD
                  ,ORGCRD=@ORGCRD
                  ,CODTAR=@CODTAR
                  ,USETRF=@USETRF
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (TIPLCT=@TIPLCT)

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
