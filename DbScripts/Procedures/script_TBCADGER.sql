IF OBJECT_ID ( 'dbo.PRCADGERINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADGERINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 15:35:42
 Objetivo : Inserção de Registros na Tabela TBCADGER
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADGERINS
        (@CODUSU int, 
        @CODATR smallint = 0, 
        @SRCUSU int, 
        @NIVCFA tinyint = 3, 
        @STAUSU smallint = 2, 
        @TIPUSU tinyint, 
        @TIPPES char(1) = 'F', 
        @CODPJU char(1) = 'I', 
        @NUMIRG char(20) = '', 
        @CODCMF char(15) = '', 
        @NOMUSU varchar(70), 
        @NOMMAE varchar(70) = '', 
        @DATNAC datetime = null, 
        @ATRPPE bit = 0, 
        @DSCOCO varchar(1024) = '', 
        @CODNAC smallint = 55, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    SET @CODUSU =0
    EXEC @CODUSU = PRGETNXTNUM 1
    
    IF(@CODUSU>0)
       BEGIN
         IF(EXISTS (SELECT 1 FROM TBCADGER (NOLOCK) WHERE CODUSU <> @CODUSU AND CODCMF=@CODCMF AND CODATR = @CODATR AND SRCUSU=@SRCUSU AND STAREC=1))
            SET @RETURN_VALUE = -3
       END
    ELSE
       SET @RETURN_VALUE=-4

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBCADGER(CODUSU, CODATR, SRCUSU, NIVCFA, STAUSU, TIPUSU, TIPPES, CODPJU, NUMIRG, CODCMF, NOMUSU, NOMMAE, DATNAC, ATRPPE, DSCOCO, CODNAC, STAREC, UPDUSU)
                        VALUES (@CODUSU, @CODATR, @SRCUSU, @NIVCFA, @STAUSU, @TIPUSU, @TIPPES, @CODPJU, @NUMIRG, @CODCMF, @NOMUSU, @NOMMAE, @DATNAC, @ATRPPE, @DSCOCO, @CODNAC, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = @CODUSU
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRCADGERSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADGERSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 15:35:42
 Objetivo : Seleciona o registro de acordo com o Código do Usuário
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADGERSEL
(
    @CODUSU Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBCADGER (NOLOCK) A

    WHERE     (A.CODUSU=@CODUSU)

GO

IF OBJECT_ID ( 'dbo.PRCADGERUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADGERUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 15:35:42
 Objetivo : Altera um registro da tabela TBCADGER ()  de acordo com a chave primaria
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADGERUPD
        (@CODUSU int, 
        @CODATR smallint = 0, 
        @SRCUSU int, 
        @NIVCFA tinyint = 3, 
        @STAUSU smallint = 2, 
        @TIPUSU tinyint, 
        @TIPPES char(1) = 'F', 
        @CODPJU char(1) = 'I', 
        @NUMIRG char(20) = '', 
        @CODCMF char(15) = '', 
        @NOMUSU varchar(70), 
        @NOMMAE varchar(70) = '', 
        @DATNAC datetime = null, 
        @ATRPPE bit = 0, 
        @DSCOCO varchar(1024) = '', 
        @CODNAC smallint = 55, 
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
            UPDATE TBCADGER
               SET CODATR=@CODATR
                  ,SRCUSU=@SRCUSU
                  ,NIVCFA=@NIVCFA
                  ,STAUSU=@STAUSU
                  ,TIPUSU=@TIPUSU
                  ,TIPPES=@TIPPES
                  ,CODPJU=@CODPJU
                  ,NUMIRG=@NUMIRG
                  ,CODCMF=@CODCMF
                  ,NOMUSU=@NOMUSU
                  ,NOMMAE=@NOMMAE
                  ,DATNAC=@DATNAC
                  ,ATRPPE=@ATRPPE
                  ,DSCOCO=@DSCOCO
                  ,CODNAC=@CODNAC
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (CODUSU=@CODUSU)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @CODUSU
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRCADGERSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADGERSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 15:35:42
 Objetivo : Obtêm uma lista de registros do cadastro geral conforme parâmetros informados
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADGERSELALL
(
    @CODATR smallint=NULL,
    @STAUSU smallint=NULL,
    @SRCUSU Integer=NULL,
    @NOMUSU varchar=NULL,
    @STAREC Tinyint=NULL
)
AS
    SET NOCOUNT ON
    IF(@NOMUSU = '')
        SET @NOMUSU = NULL
    IF(@NOMUSU IS NOT NULL)
        SET @NOMUSU = '%' +  RTRIM(@NOMUSU) +'%';
    IF(@NOMUSU='')
                 SET @NOMUSU=NULL
              IF(@STAREC<0)
                 SET @STAREC=NULL
              IF(@SRCUSU<0)
                 SET @SRCUSU=NULL
              IF(@CODATR<0)
                 SET @CODATR=NULL
              IF(@STAUSU<0)
                 SET @STAUSU=NULL
                  SELECT * FROM VWCADGER A
     WHERE       (@CODATR IS NULL OR A.CODATR=@CODATR)
       AND (@STAUSU IS NULL OR A.STAUSU=@STAUSU)
       AND (@SRCUSU IS NULL OR A.SRCUSU=@SRCUSU)
       AND (@NOMUSU IS NULL OR A.NOMUSU LIKE @NOMUSU)
       AND (@STAREC IS NULL OR A.STAREC=@STAREC)
     ORDER BY A.NOMUSU, A.CODCMF



GO

IF OBJECT_ID ( 'dbo.PRCADGERSELCTA', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADGERSELCTA;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 15:35:42
 Objetivo : Obtêm uma lista de usuários com contas virtuais ativas
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADGERSELCTA
AS
    SET NOCOUNT ON
    SELECT * FROM TBCADGER (NOLOCK) A WHERE CODUSU IN (SELECT CODUSU FROM TBCADCTA (NOLOCK) WHERE STACTA=250 AND ORGCTA IN (1,3,4) AND STAREC=1)
     ORDER BY A.NOMUSU, A.CODCMF



GO

IF OBJECT_ID ( 'dbo.PRCADGERSELUSRPRO', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADGERSELUSRPRO;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 15:35:42
 Objetivo : Obtêm uma lista de usuários vinculados ao gerenciamento de Produto
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADGERSELUSRPRO
(
    @CODPRO smallint=NULL
)
AS
    SET NOCOUNT ON
    SELECT DISTINCT A.CODUSU, B.NOMUSU, REFCOD=A.CODPRO FROM TBUSUPRO (NOLOCK) A INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.CODUSU)
     WHERE       (@CODPRO IS NULL OR A.CODPRO=@CODPRO)



GO

IF OBJECT_ID ( 'dbo.PRCADGERSELTIP', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADGERSELTIP;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 15:35:42
 Objetivo : Obtêm uma lista de usuários por tipo de usuário
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADGERSELTIP
(
    @TIPUSU Tinyint,
    @CODUSU Integer=NULL
)
AS
    SET NOCOUNT ON
    SELECT CODUSU, NOMUSU, REFCOD = TIPUSU FROM TBCADGER (NOLOCK) A
     WHERE       (A.TIPUSU=@TIPUSU)
       AND (@CODUSU IS NULL OR A.CODUSU=@CODUSU)



GO

IF OBJECT_ID ( 'dbo.PRCADGERVALENTRCMF', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADGERVALENTRCMF;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 15:35:42
 Objetivo : Valida um CPF/CNPJ já existente para o mesmo atributo na base de cadastro geral
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADGERVALENTRCMF
(
    @CODATR smallint,
    @CODUSU Integer,
    @CODCMF varchar(15),
    @SRCUSU Integer
)
AS
    SET NOCOUNT ON

    DECLARE @RETURN_VALUE INT =0;
    SELECT @RETURN_VALUE = ISNULL(CODUSU,0) 
      FROM TBCADGER (NOLOCK)
     WHERE CODUSU <> @CODUSU
    	 AND LTRIM(RTRIM(CODCMF)) = @CODCMF
    	 AND CODATR = @CODATR
    	 AND SRCUSU = @SRCUSU   
    	 AND STAREC = 1
    RETURN @RETURN_VALUE;

GO

IF OBJECT_ID ( 'dbo.PRCADUSUPSQCMF', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADUSUPSQCMF;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 15:35:42
 Objetivo : Verifica se já existe um cadastro com o CPF/CNPJ cadastrado, e retorna o id localizado
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADUSUPSQCMF
(
    @CODATR smallint,
    @CODCMF varchar
)
AS
    SET NOCOUNT ON

    DECLARE @RETURN_VALUE INT =0;
    SELECT @RETURN_VALUE = ISNULL(CODUSU,0) FROM TBCADGER (NOLOCK) 
     WHERE CODCMF = @CODCMF
    	 AND CODATR = @CODATR
    RETURN @RETURN_VALUE;

GO

