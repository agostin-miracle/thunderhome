IF OBJECT_ID ( 'dbo.PRTIPATRINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPATRINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 03/03/2022 18:35:07
 Objetivo : Inserção de Registros na Tabela TBTIPATR
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPATRINS
        (@CODATR smallint, 
        @DSCATR varchar(40), 
        @USELGN bit = 1, 
        @USEACT bit, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SELECT @CODATR = ISNULL(MAX(CODATR),0)+1 from TBTIPATR WITH (NOLOCK)

    IF((@DSCATR IS NULL) OR (@DSCATR = ''))
               SET @RETURN_VALUE=-3;
    
            IF(@RETURN_VALUE=0)
                BEGIN
                    IF(EXISTS( SELECT 1 FROM TBTIPATR (NOLOCK) WHERE DSCATR=@DSCATR))
                        SET @RETURN_VALUE=-2;
                END

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBTIPATR(CODATR, DSCATR, USELGN, USEACT, STAREC, UPDUSU)
                        VALUES (@CODATR, @DSCATR, @USELGN, @USEACT, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = @CODATR
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRTIPATRSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPATRSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 03/03/2022 18:35:07
 Objetivo : Obtêm o registro do tipo de atributo informado
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPATRSEL
(
    @CODATR smallint
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBTIPATR (NOLOCK) A

    WHERE     (A.CODATR=@CODATR)

GO

IF OBJECT_ID ( 'dbo.PRTIPATRLST', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPATRLST;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 03/03/2022 18:35:07
 Objetivo : Obtêm todos os registros de tipo de atributo cadastrados
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPATRLST
AS
    SET NOCOUNT ON
    SELECT A.*, DSCTAB AS DSCREC, ISNULL(LGNUSU,'') LGNUSU FROM TBTIPATR (NOLOCK) A
    INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB=7 AND B.KEYCOD = A.STAREC)
    LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV=1 AND C.STAREC=1)

     ORDER BY A.CODATR
GO

IF OBJECT_ID ( 'dbo.PRTIPATRUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPATRUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 03/03/2022 18:35:07
 Objetivo : Altera um registro da tabela TBTIPATR (Tipo de Atributo)  de acordo com a chave primaria
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPATRUPD
        (@CODATR smallint, 
        @DSCATR varchar(40), 
        @USELGN bit = 1, 
        @USEACT bit, 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD=GETDATE();
            
            IF((@DSCATR IS NULL) OR (@DSCATR = ''))
               SET @RETURN_VALUE=-3;
    
            IF(@RETURN_VALUE=0)
                BEGIN
                    IF(EXISTS( SELECT 1 FROM TBTIPATR (NOLOCK) WHERE DSCATR=@DSCATR AND CODATR<>@CODATR) )
                        SET @RETURN_VALUE=-2;
                END

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBTIPATR
               SET DSCATR=@DSCATR
                  ,USELGN=@USELGN
                  ,USEACT=@USEACT
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (CODATR=@CODATR)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @CODATR
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
