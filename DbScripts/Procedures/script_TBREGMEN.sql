IF OBJECT_ID ( 'dbo.PRREGMENINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGMENINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 29/03/2022 14:14:55
 Objetivo : Inserção de Registros na Tabela TBREGMEN
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGMENINS
        (        @SUBSYS tinyint, 
        @USUPRO int, 
        @CODUSU int, 
        @USRREF int = 0, 
        @TIPMEN tinyint, 
        @CODREF int, 
        @MODREG tinyint = 1, 
        @REGBXA tinyint = 0, 
        @STAMEN smallint, 
        @SRCCAN tinyint = 0, 
        @NUMMES tinyint = 0, 
        @NUMANO smallint = 0, 
        @NUMPCL smallint, 
        @DATMEN date, 
        @DATVCT date, 
        @SIGOPE smallint = 1, 
        @VLRCOB money = 0, 
        @LOTINS int = 0, 
        @NUMTEN int = 0, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBREGMEN(SUBSYS, USUPRO, CODUSU, USRREF, TIPMEN, CODREF, MODREG, REGBXA, STAMEN, SRCCAN, NUMMES, NUMANO, NUMPCL, DATMEN, DATVCT, SIGOPE, VLRCOB, LOTINS, NUMTEN, STAREC, UPDUSU)
                        VALUES (@SUBSYS, @USUPRO, @CODUSU, @USRREF, @TIPMEN, @CODREF, @MODREG, @REGBXA, @STAMEN, @SRCCAN, @NUMMES, @NUMANO, @NUMPCL, @DATMEN, @DATVCT, @SIGOPE, @VLRCOB, @LOTINS, @NUMTEN, @STAREC, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PRREGMENSELMEN', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGMENSELMEN;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 29/03/2022 14:14:55
 Objetivo : Localiza um registro de mensalidae conforme os parâmetros abaixo
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGMENSELMEN
(
    @TIPMEN Integer,
    @CODREF Integer,
    @CODUSU Integer,
    @REGBXA Tinyint = 0
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBREGMEN (NOLOCK) A WHERE STAREC=1

 AND     (A.TIPMEN=@TIPMEN)
     AND (A.CODREF=@CODREF)
     AND (A.CODUSU=@CODUSU)
     AND (A.REGBXA=@REGBXA)
     ORDER BY A.DATMEN DESC
GO

IF OBJECT_ID ( 'dbo.PRREGMENUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGMENUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 29/03/2022 14:14:55
 Objetivo : Altera um registro da tabela TBREGMEN (Monthly Payment)  de acordo com a chave identity
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGMENUPD
        (@NIDMEN int, 
        @SUBSYS tinyint, 
        @USUPRO int, 
        @CODUSU int, 
        @USRREF int = 0, 
        @TIPMEN tinyint, 
        @CODREF int, 
        @MODREG tinyint = 1, 
        @REGBXA tinyint = 0, 
        @STAMEN smallint, 
        @SRCCAN tinyint = 0, 
        @NUMMES tinyint = 0, 
        @NUMANO smallint = 0, 
        @NUMPCL smallint, 
        @DATMEN date, 
        @DATVCT date, 
        @SIGOPE smallint = 1, 
        @VLRCOB money = 0, 
        @LOTINS int = 0, 
        @NUMTEN int = 0, 
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
            UPDATE TBREGMEN
               SET SUBSYS=@SUBSYS
                  ,USUPRO=@USUPRO
                  ,CODUSU=@CODUSU
                  ,USRREF=@USRREF
                  ,TIPMEN=@TIPMEN
                  ,CODREF=@CODREF
                  ,MODREG=@MODREG
                  ,REGBXA=@REGBXA
                  ,STAMEN=@STAMEN
                  ,SRCCAN=@SRCCAN
                  ,NUMMES=@NUMMES
                  ,NUMANO=@NUMANO
                  ,NUMPCL=@NUMPCL
                  ,DATMEN=@DATMEN
                  ,DATVCT=@DATVCT
                  ,SIGOPE=@SIGOPE
                  ,VLRCOB=@VLRCOB
                  ,LOTINS=@LOTINS
                  ,NUMTEN=@NUMTEN
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (NIDMEN=@NIDMEN)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @NIDMEN
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
