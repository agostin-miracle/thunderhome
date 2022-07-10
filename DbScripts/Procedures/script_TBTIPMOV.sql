IF OBJECT_ID ( 'dbo.PRTIPMOVINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPMOVINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 20/03/2022 16:58:28
 Objetivo : Inserção de Registros na Tabela TBTIPMOV
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPMOVINS
        (@CODMOV smallint, 
        @SUBSYS tinyint, 
        @DSCMOV varchar(50), 
        @DSCEXT varchar(100) = '', 
        @SIGOPE smallint = 0, 
        @CNDBLO tinyint = 1, 
        @NUMDIA smallint = 0, 
        @CODEST smallint = 0, 
        @CODCAN smallint = 0, 
        @DSCEXC varchar(100) = '', 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    IF(@DSCEXT IS NULL)
       SET @DSCEXT='';
    IF(@DSCEXC IS NULL)
       SET @DSCEXC='';
            
    IF(EXISTS (SELECT 1 FROM TBTIPMOV (NOLOCK) WHERE DSCMOV= @DSCMOV))
       SET @RETURN_VALUE=-3;

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBTIPMOV(CODMOV, SUBSYS, DSCMOV, DSCEXT, SIGOPE, CNDBLO, NUMDIA, CODEST, CODCAN, DSCEXC, STAREC, UPDUSU)
                        VALUES (@CODMOV, @SUBSYS, @DSCMOV, @DSCEXT, @SIGOPE, @CNDBLO, @NUMDIA, @CODEST, @CODCAN, @DSCEXC, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = @CODMOV
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRTIPMOVUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPMOVUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 20/03/2022 16:58:28
 Objetivo : Altera um registro da tabela TBTIPMOV (Operations)  de acordo com a chave primaria
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPMOVUPD
        (@CODMOV smallint, 
        @SUBSYS tinyint, 
        @DSCMOV varchar(50), 
        @DSCEXT varchar(100) = '', 
        @SIGOPE smallint = 0, 
        @CNDBLO tinyint = 1, 
        @NUMDIA smallint = 0, 
        @CODEST smallint = 0, 
        @CODCAN smallint = 0, 
        @DSCEXC varchar(100) = '', 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD=GETDATE();
    
    IF(@DSCEXT IS NULL)
       SET @DSCEXT='';
    IF(@DSCEXC IS NULL)
       SET @DSCEXC='';
    
    IF(EXISTS (SELECT 1 FROM TBTIPMOV (NOLOCK) WHERE DSCMOV = @DSCMOV AND CODMOV <>@CODMOV))
        SET @RETURN_VALUE=-3;

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBTIPMOV
               SET SUBSYS=@SUBSYS
                  ,DSCMOV=@DSCMOV
                  ,DSCEXT=@DSCEXT
                  ,SIGOPE=@SIGOPE
                  ,CNDBLO=@CNDBLO
                  ,NUMDIA=@NUMDIA
                  ,CODEST=@CODEST
                  ,CODCAN=@CODCAN
                  ,DSCEXC=@DSCEXC
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (CODMOV=@CODMOV)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @CODMOV
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
