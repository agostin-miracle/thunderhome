IF OBJECT_ID ( 'dbo.PRCADENDINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADENDINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 18/03/2022 17:51:35
 Objetivo : Inserção de Registros na Tabela TBCADEND
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADENDINS
        (        @REGATV tinyint = 0, 
        @CODUSU int, 
        @TIPEND tinyint, 
        @TIPLOG smallint = 0, 
        @CODUFE char(2) = '  ', 
        @DSCEND varchar(70) = '', 
        @DSCCPL varchar(70) = '', 
        @NUMEND int = 0, 
        @DSCCID varchar(40) = '', 
        @DSCBAI varchar(40) = '', 
        @CODCEP varchar(8) = '00000000', 
        @CODPAI int = 55, 
        @LATITU decimal = null, 
        @LONGIT decimal = null, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    DECLARE @DATCAD DATETIME
        SET @DATCAD = GETDATE()
        SET @RETURN_VALUE=0        
        IF(@DSCEND IS NULL OR @DSCEND='')
           SET @RETURN_VALUE=-3
           
        IF(@RETURN_VALUE = 0)
          EXEC @RETURN_VALUE = PRCADENDFND @CODUSU, @DSCEND, @TIPEND
        
        IF(@RETURN_VALUE = 0)
           BEGIN
              IF(@TIPEND NOT IN (3,4,0))
                BEGIN
                  IF((@CODCEP='00000000') OR (@CODUFE=''))
                    SET @STAREC=13
                END
          END

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBCADEND(REGATV, CODUSU, TIPEND, TIPLOG, CODUFE, DSCEND, DSCCPL, NUMEND, DSCCID, DSCBAI, CODCEP, CODPAI, LATITU, LONGIT, STAREC, UPDUSU)
                        VALUES (@REGATV, @CODUSU, @TIPEND, @TIPLOG, @CODUFE, @DSCEND, @DSCCPL, @NUMEND, @DSCCID, @DSCBAI, @CODCEP, @CODPAI, @LATITU, @LONGIT, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = @@IDENTITY
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
    IF(@RETURN_VALUE>0 AND @REGATV=1 AND @STAREC=1)
              BEGIN
                UPDATE TBCADEND 
                   SET REGATV = 0
                 WHERE CODEND <> @RETURN_VALUE
                   AND CODUSU = @CODUSU
                   AND TIPEND = @TIPEND
                   AND STAREC = 1
                   AND @REGATV = 1
              END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRCADENDSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADENDSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 18/03/2022 17:51:35
 Objetivo : Obtêm o registro de Endereço
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADENDSEL
(
    @CODEND Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBCADEND (NOLOCK) A

    WHERE     (A.CODEND=@CODEND)

GO

IF OBJECT_ID ( 'dbo.PRCADENDUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADENDUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 18/03/2022 17:51:35
 Objetivo : Altera um registro da tabela TBCADEND ()  de acordo com a chave primaria
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADENDUPD
        (@CODEND int, 
        @REGATV tinyint = 0, 
        @CODUSU int, 
        @TIPEND tinyint, 
        @TIPLOG smallint = 0, 
        @CODUFE char(2) = '  ', 
        @DSCEND varchar(70) = '', 
        @DSCCPL varchar(70) = '', 
        @NUMEND int = 0, 
        @DSCCID varchar(40) = '', 
        @DSCBAI varchar(40) = '', 
        @CODCEP varchar(8) = '00000000', 
        @CODPAI int = 55, 
        @LATITU decimal = null, 
        @LONGIT decimal = null, 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD=GETDATE();
        SET @RETURN_VALUE=0        
        IF(@DSCEND IS NULL OR @DSCEND='')
           SET @RETURN_VALUE=-3
        
        IF(@RETURN_VALUE = 0)
           BEGIN
              IF(@TIPEND NOT IN (3,4,0))
                BEGIN
                  IF((@CODCEP='00000000') OR (@CODUFE=''))
                    SET @STAREC=13
                END
          END

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBCADEND
               SET REGATV=@REGATV
                  ,CODUSU=@CODUSU
                  ,TIPEND=@TIPEND
                  ,TIPLOG=@TIPLOG
                  ,CODUFE=@CODUFE
                  ,DSCEND=@DSCEND
                  ,DSCCPL=@DSCCPL
                  ,NUMEND=@NUMEND
                  ,DSCCID=@DSCCID
                  ,DSCBAI=@DSCBAI
                  ,CODCEP=@CODCEP
                  ,CODPAI=@CODPAI
                  ,LATITU=@LATITU
                  ,LONGIT=@LONGIT
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (CODEND=@CODEND)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @CODEND
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    IF(@RETURN_VALUE>0 AND @REGATV=1 AND @STAREC=1)
              BEGIN
                UPDATE TBCADEND 
                   SET REGATV = 0
                 WHERE CODEND <> @RETURN_VALUE
                   AND CODUSU = @CODUSU
                   AND TIPEND = @TIPEND
                   AND STAREC = 1
                   AND @REGATV = 1
              END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRCADENDFND', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADENDFND;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 18/03/2022 17:51:35
 Objetivo : Localiza o ID de um endereço com base nos parametros fornecidos
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADENDFND
(
    @CODUSU Integer,
    @DSCEND varchar(70)=NULL,
    @TIPEND Tinyint=NULL,
    @REGATV Tinyint=NULL
)
AS
    SET NOCOUNT ON

    DECLARE @RETURN_VALUE INT = 0
    SELECT TOP 1 @RETURN_VALUE = COALESCE(CODEND,0) 
      FROM TBCADEND (NOLOCK) 
     WHERE STAREC = 1
       AND CODUSU = @CODUSU  
       AND DSCEND = @DSCEND
    	 AND TIPEND = @TIPEND    
    	 AND (@REGATV IS NULL OR REGATV = @REGATV)       
    RETURN @RETURN_VALUE

GO

