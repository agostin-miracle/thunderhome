IF OBJECT_ID ( 'dbo.PRCFGBOLINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCFGBOLINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/04/2022 13:33:56
 Objetivo : Inserção de Registros na Tabela TBCFGBOL
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCFGBOLINS
        (        @USUCFG int, 
        @NIVCFG tinyint, 
        @TIPBOL tinyint, 
        @APLTAR tinyint = 0, 
        @CODTAR smallint = 0, 
        @APLTDP tinyint = 0, 
        @TARTDP smallint = 0, 
        @TIPEXT char(2) = '', 
        @INSBC1 varchar(30) = '', 
        @INSBC2 varchar(30) = '', 
        @INSBC3 varchar(30) = '', 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBCFGBOL(USUCFG, NIVCFG, TIPBOL, APLTAR, CODTAR, APLTDP, TARTDP, TIPEXT, INSBC1, INSBC2, INSBC3, STAREC, UPDUSU)
                        VALUES (@USUCFG, @NIVCFG, @TIPBOL, @APLTAR, @CODTAR, @APLTDP, @TARTDP, @TIPEXT, @INSBC1, @INSBC2, @INSBC3, @STAREC, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PRCFGBOLSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCFGBOLSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/04/2022 13:33:56
 Objetivo : Obtêm o registro de configuração de boleto conforme o id informado
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCFGBOLSEL
(
    @NIDCBL Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBCFGBOL (NOLOCK) A

    WHERE     (A.NIDCBL=@NIDCBL)

GO

IF OBJECT_ID ( 'dbo.PRCFGBOLUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCFGBOLUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/04/2022 13:33:56
 Objetivo : Altera um registro da tabela TBCFGBOL (Ticket Configuration)  de acordo com a chave identity
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCFGBOLUPD
        (@NIDCBL int, 
        @USUCFG int, 
        @NIVCFG tinyint, 
        @TIPBOL tinyint, 
        @APLTAR tinyint = 0, 
        @CODTAR smallint = 0, 
        @APLTDP tinyint = 0, 
        @TARTDP smallint = 0, 
        @TIPEXT char(2) = '', 
        @INSBC1 varchar(30) = '', 
        @INSBC2 varchar(30) = '', 
        @INSBC3 varchar(30) = '', 
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
            UPDATE TBCFGBOL
               SET APLTAR=@APLTAR
                  ,CODTAR=@CODTAR
                  ,APLTDP=@APLTDP
                  ,TARTDP=@TARTDP
                  ,TIPEXT=@TIPEXT
                  ,INSBC1=@INSBC1
                  ,INSBC2=@INSBC2
                  ,INSBC3=@INSBC3
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (NIDCBL=@NIDCBL)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @NIDCBL
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRCFGBOLOCREC', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCFGBOLOCREC;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/04/2022 13:33:57
 Objetivo : Localiza um id de registro de configuração de boleto de acordo com os parâmetros fornecidos
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCFGBOLOCREC
(
    @USUPRO Integer,
    @USUCED Integer,
    @TIPBOL Tinyint
)
AS
    SET NOCOUNT ON

    DECLARE @RETURN_VALUE INT=0
    
    /* Cedente - nivel usuário */
    IF(EXISTS(SELECT 1 FROM TBCFGBOL (NOLOCK) WHERE USUCFG=@USUCED AND NIVCFG=2 AND TIPBOL=@TIPBOL))
    	SELECT @RETURN_VALUE = ISNULL(NIDCBL,0) FROM TBCFGBOL (NOLOCK) WHERE USUCFG=@USUCED AND NIVCFG=1 AND STAREC IN (1,2) AND TIPBOL=@TIPBOL
    
    SET @RETURN_VALUE = ISNULL(@RETURN_VALUE,0)
    /* GESTOR - nivel gestão */
    IF(@RETURN_VALUE=0)
       BEGIN
    		IF(EXISTS(SELECT 1 FROM TBCFGBOL (NOLOCK) WHERE USUCFG=@USUPRO AND NIVCFG=1 AND TIPBOL=@TIPBOL))
    			SELECT @RETURN_VALUE = ISNULL(NIDCBL,0) FROM TBCFGBOL (NOLOCK) WHERE USUCFG=@USUPRO AND NIVCFG=1 AND STAREC IN (1,2) AND TIPBOL=@TIPBOL
    	END
    
    SET @RETURN_VALUE = ISNULL(@RETURN_VALUE,0)
    /* Não existe nenhuma configuração, acesso padrão */
    IF(@RETURN_VALUE=0)
        BEGIN
    		IF(EXISTS(SELECT 1 FROM TBCFGBOL (NOLOCK) WHERE USUCFG=0 AND NIVCFG=0 AND TIPBOL=@TIPBOL))
    			SELECT @RETURN_VALUE = NIDCBL FROM TBCFGBOL (NOLOCK) WHERE USUCFG=0 AND NIVCFG=0 AND STAREC IN (1,2) AND TIPBOL=@TIPBOL
    	END
    SET @RETURN_VALUE = ISNULL(@RETURN_VALUE,0)
    RETURN @RETURN_VALUE

GO

