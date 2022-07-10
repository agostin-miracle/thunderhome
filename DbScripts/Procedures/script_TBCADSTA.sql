IF OBJECT_ID ( 'dbo.PRCADSTAINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADSTAINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 19/03/2022 14:29:11
 Objetivo : Inserção de Registros na Tabela TBCADSTA
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADSTAINS
        (@CODSTA smallint, 
        @DSCSTA varchar(70), 
        @CODMOD tinyint, 
        @SIGOPE smallint = 1, 
        @NXTSTA int, 
        @CANCHG tinyint = 0, 
        @DELMEN tinyint = 0, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SELECT @CODSTA = ISNULL(MAX(CODSTA),0)+1 from TBCADSTA WITH (NOLOCK)

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBCADSTA(CODSTA, DSCSTA, CODMOD, SIGOPE, NXTSTA, CANCHG, DELMEN, STAREC, UPDUSU)
                        VALUES (@CODSTA, @DSCSTA, @CODMOD, @SIGOPE, @NXTSTA, @CANCHG, @DELMEN, @STAREC, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PRCADSTASEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADSTASEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 19/03/2022 14:29:11
 Objetivo : Seleciona o registro de acordo com o codigo do status fornecido
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADSTASEL
(
    @CODSTA Integer
)
AS
    SET NOCOUNT ON
    SELECT A.*
    ,DSCCHG = CASE WHEN (A.CANCHG=1) THEN 'Sim' ELSE 'Não' END
    ,DSCDEL = CASE WHEN (A.DELMEN=1) THEN 'Sim' ELSE 'Não' END
    ,DSCREC = ISNULL(B.DSCTAB,'')
    ,DSCMOD = ISNULL(C.DSCTAB,'')
    ,LGNUSU = ISNULL(D.LGNUSU,'')
      FROM TBCADSTA (NOLOCK) A
     INNER JOIN TBTABGER B (NOLOCK) ON (B.NUMTAB =  7 AND B.KEYCOD = A.STAREC)
     INNER JOIN TBTABGER C (NOLOCK) ON (C.NUMTAB = 14 AND C.KEYCOD = A.CODMOD)
      LEFT JOIN TBLGNUSU D (NOLOCK) ON (D.CODUSU = A.UPDUSU  AND D.REGATV = 1)

    WHERE     (A.CODSTA=@CODSTA)

GO

IF OBJECT_ID ( 'dbo.PRCADSTAUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADSTAUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 19/03/2022 14:29:11
 Objetivo : Altera um registro da tabela TBCADSTA (Transaction Status)  de acordo com a chave primaria
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADSTAUPD
        (@CODSTA smallint, 
        @DSCSTA varchar(70), 
        @CODMOD tinyint, 
        @SIGOPE smallint = 1, 
        @NXTSTA int, 
        @CANCHG tinyint = 0, 
        @DELMEN tinyint = 0, 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBCADSTA
               SET DSCSTA=@DSCSTA
                  ,CODMOD=@CODMOD
                  ,SIGOPE=@SIGOPE
                  ,NXTSTA=@NXTSTA
                  ,CANCHG=@CANCHG
                  ,DELMEN=@DELMEN
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (CODSTA=@CODSTA)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @CODSTA
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
