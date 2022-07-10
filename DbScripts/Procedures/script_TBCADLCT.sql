IF OBJECT_ID ( 'dbo.PRCADLCTINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADLCTINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 17/04/2022 15:10:16
 Objetivo : Inserção de Registros na Tabela TBCADLCT
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADLCTINS
        (        @TIPLCT smallint = 0, 
        @LINLCT smallint, 
        @CODTRM tinyint = 1, 
        @NUMORD smallint, 
        @INDDEB int = 0, 
        @INDCRD int = 0, 
        @MOVDEB int = 0, 
        @MOVCRD int = 0, 
        @STAREG int = 0, 
        @IDEPRE tinyint = 0, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    SELECT @NUMORD = (MAX( ISNULL(NUMORD,0) ) + 1) FROM TBCADLCT (NOLOCK) WHERE TIPLCT =@TIPLCT AND LINLCT = @LINLCT GROUP BY TIPLCT, LINLCT

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBCADLCT(TIPLCT, LINLCT, CODTRM, NUMORD, INDDEB, INDCRD, MOVDEB, MOVCRD, STAREG, IDEPRE, STAREC, UPDUSU)
                        VALUES (@TIPLCT, @LINLCT, @CODTRM, @NUMORD, @INDDEB, @INDCRD, @MOVDEB, @MOVCRD, @STAREG, @IDEPRE, @STAREC, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PRCADLCTUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADLCTUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 17/04/2022 15:10:16
 Objetivo : Altera um registro da tabela TBCADLCT ()  de acordo com a chave identity
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADLCTUPD
        (@NIDLCT int, 
        @TIPLCT smallint = 0, 
        @LINLCT smallint, 
        @CODTRM tinyint = 1, 
        @NUMORD smallint, 
        @INDDEB int = 0, 
        @INDCRD int = 0, 
        @MOVDEB int = 0, 
        @MOVCRD int = 0, 
        @STAREG int = 0, 
        @IDEPRE tinyint = 0, 
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
            UPDATE TBCADLCT
               SET TIPLCT=@TIPLCT
                  ,LINLCT=@LINLCT
                  ,CODTRM=@CODTRM
                  ,NUMORD=@NUMORD
                  ,INDDEB=@INDDEB
                  ,INDCRD=@INDCRD
                  ,MOVDEB=@MOVDEB
                  ,MOVCRD=@MOVCRD
                  ,STAREG=@STAREG
                  ,IDEPRE=@IDEPRE
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (NIDLCT=@NIDLCT)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @NIDLCT
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRCADLCTCPY', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADLCTCPY;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 17/04/2022 15:10:16
 Objetivo : Copia um conjunto de cadastro de lançamentos para outros
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADLCTCPY
(
    @TIPLCT smallint,
    @NEWTIP smallint,
    @UPDUSU Integer
)
AS
    SET NOCOUNT ON

    DECLARE @RETURN_VALUE INT =0
    IF(@UPDUSU<=0)
       SET @RETURN_VALUE=-1
           
    IF(@RETURN_VALUE=0 AND @TIPLCT>0 )
       BEGIN
          INSERT INTO TBCADLCT (TIPLCT, LINLCT, CODTRM, NUMORD,
                  INDDEB, INDCRD, MOVDEB, MOVCRD,
                  STAREG, IDEPRE, CODVER, STAREC, UPDUSU)
              SELECT
                  @NEWTIP, LINLCT, CODTRM, NUMORD,
                  INDDEB, INDCRD, MOVDEB, MOVCRD,
                  STAREG, IDEPRE, CODVER, 1, @UPDUSU
              FROM TBCADLCT (NOLOCK) A WHERE A.STAREC=1 AND TIPLCT=@TIPLCT
              
    		  SELECT @RETURN_VALUE = COUNT(*) FROM TBCADLCT WHERE TIPLCT = @NEWTIP AND STAREC=1          
        END
    RETURN @RETURN_VALUE

GO

IF OBJECT_ID ( 'dbo.PRCADLCTDEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADLCTDEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 17/04/2022 15:10:16
 Objetivo : Exclui um conjunto de cadastro de lançamentos
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADLCTDEL
(
    @TIPLCT smallint,
    @UPDUSU Integer
)
AS
    SET NOCOUNT ON

    DECLARE @RETURN_VALUE INT =0
    IF(@UPDUSU<=0)
       SET @RETURN_VALUE=-1
           
    IF(@RETURN_VALUE=0 AND @TIPLCT > 0)
       BEGIN
          DECLARE @RECORDS INT
          SELECT @RECORDS=COUNT(*) FROM TBCADLCT WHERE TIPLCT=@TIPLCT
    			IF(@RECORDS>0)
    			     BEGIN
    						  DELETE FROM TBCADLCT WHERE TIPLCT=@TIPLCT
    						  SET @RETURN_VALUE=@RECORDS
    				   END
       END
    RETURN @RETURN_VALUE

GO

