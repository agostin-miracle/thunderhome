IF OBJECT_ID ( 'dbo.PRTIPTARINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPTARINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 13/03/2022 10:27:45
 Objetivo : Inserção de Registros na Tabela TBTIPTAR
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPTARINS
        (@CODTAR tinyint, 
        @DSCTAR varchar(50), 
        @CODMOV smallint, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SELECT @CODTAR = ISNULL(MAX(CODTAR),0)+1 from TBTIPTAR WITH (NOLOCK)

    IF(EXISTS (SELECT 1 FROM TBTIPTAR (NOLOCK) WHERE DSCTAR= @DSCTAR))
       SET @RETURN_VALUE=-3;

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBTIPTAR(CODTAR, DSCTAR, CODMOV, STAREC, UPDUSU)
                        VALUES (@CODTAR, @DSCTAR, @CODMOV, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = @CODTAR
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRTIPTARUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPTARUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 13/03/2022 10:27:45
 Objetivo : Altera um registro da tabela TBTIPTAR (Tariff Type)  de acordo com a chave primaria
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPTARUPD
        (@CODTAR tinyint, 
        @DSCTAR varchar(50), 
        @CODMOV smallint, 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD=GETDATE();
    IF(EXISTS (SELECT 1 FROM TBTIPTAR (NOLOCK) WHERE DSCTAR = @DSCTAR AND CODTAR <>@CODTAR))
        SET @RETURN_VALUE=-3;

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBTIPTAR
               SET DSCTAR=@DSCTAR
                  ,CODMOV=@CODMOV
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (CODTAR=@CODTAR)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @CODTAR
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRTIPTARSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPTARSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 13/03/2022 10:27:45
 Objetivo : Obtêm uma Lista dos Tipos de Tarifa
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPTARSELALL
AS
    SET NOCOUNT ON
    SELECT A.*, DSCREC= ISNULL(B.DSCTAB,''), 
    LGNUSU = ISNULL(C.LGNUSU,''), D.DSCMOV 
    FROM TBTIPTAR (NOLOCK) A
    INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB =7 AND B.KEYCOD=A.STAREC)
     LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV =1)
    INNER JOIN TBTIPMOV (NOLOCK) D ON (D.CODMOV = A.CODMOV)
     ORDER BY A.CODTAR



GO

