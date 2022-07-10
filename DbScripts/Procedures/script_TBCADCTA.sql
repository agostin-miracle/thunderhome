IF OBJECT_ID ( 'dbo.PRCADCTAINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTAINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 11/04/2022 17:46:12
 Objetivo : Inserção de Registros na Tabela TBCADCTA
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCTAINS
        (        @CODUSU int, 
        @NUMAGE varchar(15) = ' ', 
        @NUMBCO char(3) = '000', 
        @NUMCTA char(15), 
        @NUMDVE char(1), 
        @ORGCTA tinyint = 1, 
        @REGATV tinyint = 0, 
        @TIPCTA tinyint, 
        @STACTA smallint, 
        @DATVAL datetime, 
        @APLLIM tinyint = 0, 
        @VLRLIM money = 0, 
        @TIPBNF int = 0, 
        @CODCMF varchar(15) = '000000000000000', 
        @NOMBNF varchar(70) = ' ', 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    IF(@NUMBCO='' OR @NUMBCO IS NULL)
       SET @NUMBCO='000';
    
    IF(@ORGCTA IN (1,3,4))
        BEGIN
            IF(EXISTS(SELECT 1 
                        FROM TBCADCTA (NOLOCK) 
                       WHERE CODUSU =@CODUSU AND ORGCTA=@ORGCTA))              
                BEGIN
                    SET @RETURN_VALUE=-2
                END
        END
            
    IF(@ORGCTA IN (1,3,4) AND @RETURN_VALUE=0)
        BEGIN
            DECLARE @RETORNO INT
            DECLARE @ACCOUNTBASE VARCHAR(11)
            EXEC PRGETNXTNUM 4, @RETORNO OUTPUT
            SET @ACCOUNTBASE =  dbo.CreateAccountBase(@RETORNO)
            SET @NUMCTA = @ACCOUNTBASE
            SET @NUMDVE = dbo.Modulo11(@accountbase)
            SET @NUMAGE = '0001';
            SET @NUMBCO = '000'
            SET @TIPBNF = 0
        END
    
    SELECT @NUMBCO = LTRIM(RTRIM(RIGHT('000' + ISNULL(@NUMBCO, '0'),3)))      
    
    IF(@ORGCTA IN (1,3,4) AND (@CODCMF='' OR @CODCMF IS NULL) )
       BEGIN
           SELECT @CODCMF = CODCMF FROM TBCADGER (NOLOCK) WHERE CODUSU=@CODUSU
       END
    IF(@ORGCTA IN (1,3,4) AND (@NOMBNF='' OR @NOMBNF IS NULL) )
       BEGIN
           SELECT @NOMBNF = NOMUSU FROM TBCADGER (NOLOCK) WHERE CODUSU=@CODUSU
       END

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBCADCTA(CODUSU, NUMAGE, NUMBCO, NUMCTA, NUMDVE, ORGCTA, REGATV, TIPCTA, STACTA, DATVAL, APLLIM, VLRLIM, TIPBNF, CODCMF, NOMBNF, STAREC, UPDUSU)
                        VALUES (@CODUSU, @NUMAGE, @NUMBCO, @NUMCTA, @NUMDVE, @ORGCTA, @REGATV, @TIPCTA, @STACTA, @DATVAL, @APLLIM, @VLRLIM, @TIPBNF, @CODCMF, @NOMBNF, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = @@IDENTITY
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
    IF(@RETURN_VALUE>0 AND @ORGCTA=2)
          UPDATE TBCADCTA 
             SET REGATV=0 
            WHERE CODUSU=@CODUSU 
              AND ORGCTA=2 
              AND NIDCTA<>@RETURN_VALUE
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRCADCTASEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTASEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 11/04/2022 17:46:13
 Objetivo : Seleciona o registro de conta virtual de acordo com o id da conta
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCTASEL
(
    @NIDCTA Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBCADCTA (nolock) A

    WHERE     (A.NIDCTA=@NIDCTA)

GO

IF OBJECT_ID ( 'dbo.PRCADCTASELCTA', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTASELCTA;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 11/04/2022 17:46:13
 Objetivo : Seleciona o registro de conta virtual de acordo com os parâmetros fornecidos
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCTASELCTA
(
    @NUMBCO varchar(3),
    @NUMAGE varchar(15),
    @NUMCTA varchar(11),
    @NUMDVE varchar(1),
    @CODUSU Integer,
    @ORGCTA Tinyint = 2,
    @TIPCTA Integer = 2
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBCADCTA (NOLOCK) A

    WHERE     (A.NUMBCO=@NUMBCO)
     AND (A.NUMAGE=@NUMAGE)
     AND (A.NUMCTA=@NUMCTA)
     AND (A.NUMDVE=@NUMDVE)
     AND (A.CODUSU=@CODUSU)
     AND (A.ORGCTA=@ORGCTA)
     AND (A.TIPCTA=@TIPCTA)

GO

IF OBJECT_ID ( 'dbo.PRCADCTASELCMF', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTASELCMF;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 11/04/2022 17:46:13
 Objetivo : Obtêm o registro de conta virtual de acordo com o cpf/cnpj informado
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCTASELCMF
(
    @CODCMF varchar(15),
    @ORGCTA Tinyint=NULL
)
AS
    SET NOCOUNT ON
    SELECT TOP 1 * FROM TBCADCTA (NOLOCK) A WHERE A.CODUSU IN(SELECT CODUSU FROM TBCADGER (NOLOCK) WHERE STAREC=1 AND STAUSU=61 AND CODCMF=@CODCMF) AND (@ORGCTA IS NULL OR ORGCTA =@ORGCTA)


GO

IF OBJECT_ID ( 'dbo.PRCADCTAUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTAUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 11/04/2022 17:46:13
 Objetivo : Altera um registro da tabela TBCADCTA ()  de acordo com a chave identity
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCTAUPD
        (@NIDCTA int, 
        @CODUSU int, 
        @NUMAGE varchar(15) = ' ', 
        @NUMBCO char(3) = '000', 
        @NUMCTA char(15), 
        @NUMDVE char(1), 
        @ORGCTA tinyint = 1, 
        @REGATV tinyint = 0, 
        @TIPCTA tinyint, 
        @STACTA smallint, 
        @DATVAL datetime, 
        @APLLIM tinyint = 0, 
        @VLRLIM money = 0, 
        @TIPBNF int = 0, 
        @CODCMF varchar(15) = '000000000000000', 
        @NOMBNF varchar(70) = ' ', 
        @STAREC tinyint = 1, 
        @DATUPD date, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBCADCTA
               SET CODUSU=@CODUSU
                  ,NUMAGE=@NUMAGE
                  ,NUMBCO=@NUMBCO
                  ,NUMCTA=@NUMCTA
                  ,NUMDVE=@NUMDVE
                  ,ORGCTA=@ORGCTA
                  ,REGATV=@REGATV
                  ,TIPCTA=@TIPCTA
                  ,STACTA=@STACTA
                  ,DATVAL=@DATVAL
                  ,APLLIM=@APLLIM
                  ,VLRLIM=@VLRLIM
                  ,TIPBNF=@TIPBNF
                  ,CODCMF=@CODCMF
                  ,NOMBNF=@NOMBNF
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (NIDCTA=@NIDCTA)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @NIDCTA
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRCADCTADELNID', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTADELNID;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 11/04/2022 17:46:14
 Objetivo : Exclui logicamente um registro de conta
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCTADELNID
(
    @NIDCTA Integer,
    @UPDUSU Integer
)
AS
    SET NOCOUNT ON

    DECLARE @RETURN_VALUE INT =0          
    IF(@UPDUSU<=0)
       SET @RETURN_VALUE=-2
    IF(EXISTS(SELECT 1 FROM TBCADCTA (NOLOCK) WHERE NIDCTA=@NIDCTA) AND @RETURN_VALUE=0)         
       BEGIN
          UPDATE TBCADCTA SET STAREC=1, STACTA=253, UPDUSU=@UPDUSU, DATUPD=GETDATE() WHERE NIDCTA = @NIDCTA
          SET @RETURN_VALUE=1
       END
    ELSE
       SET @RETURN_VALUE=-1
    RETURN @RETURN_VALUE

GO

IF OBJECT_ID ( 'dbo.PRCADCTACHGSTAACC', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTACHGSTAACC;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 11/04/2022 17:46:14
 Objetivo : Executa a aprovação ou rejeição de uma conta digital
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCTACHGSTAACC
(
    @NIDCTA Integer,
    @CODOPE Tinyint,
    @UPDUSU Integer
    ,@RETURN_VALUE INT OUTPUT
)
AS
    SET NOCOUNT ON

    SET @RETURN_VALUE=0
    DECLARE @NOMUSU VARCHAR(70)=N'';
    DECLARE @CODACE INT=0;
    DECLARE @CODUSU INT=0;
    DECLARE @LGNUSU VARCHAR(50)=N'';
    DECLARE @STACTA SMALLINT;
    IF(@NIDCTA=0)
      SET @RETURN_VALUE = -1
      
    IF(@RETURN_VALUE=0)
       BEGIN
          IF(@UPDUSU=0)
              SET @RETURN_VALUE = -2
       END
    
    IF(@RETURN_VALUE=0)
       BEGIN
          IF(@CODOPE NOT IN (1,3))
            SET @RETURN_VALUE = -4
       END        
    
    IF(@UPDUSU>0)
      SELECT @LGNUSU = LGNUSU FROM TBLGNUSU (NOLOCK) WHERE CODUSU=@UPDUSU AND REGATV=1 
    
    SELECT @STACTA = STACTA
      FROM TBCACSTA (NOLOCK)
     WHERE NIDCTA = @NIDCTA  
     
    IF(@STACTA <> 253)
       SET @RETURN_VALUE=-6;
    
    
    IF(@RETURN_VALUE=0)
       BEGIN
          IF( NOT (EXISTS (SELECT 1 
                      FROM TBCADGER (NOLOCK) 
                     WHERE CODUSU IN (SELECT CODUSU 
                                        FROM TBCADCTA (NOLOCK) 
                                       WHERE NIDCTA=@NIDCTA) 
                       AND STAREC=1 AND STAUSU=61)))
              BEGIN
                  SET @RETURN_VALUE=-5;
              END          
       END
    
    IF (@CODUSU>0 AND @RETURN_VALUE=0)
    	BEGIN
    		UPDATE TBCADCTA
    		   SET STACTA = CASE WHEN (@CODOPE=1) THEN 250 ELSE 252 END
              ,STAREC = CASE WHEN (@CODOPE=1) THEN 1 ELSE 0 END
              ,UPDUSU = @UPDUSU
              ,DATUPD = GETDATE()
             WHERE NIDCTA = @NIDCTA
         IF(@@ERROR=0)
    	      BEGIN
    				        UPDATE TBCADGER SET UPDUSU = @UPDUSU
                          ,DATUPD = GETDATE()
                          ,DSCOCO = LTRIM(RTRIM(DSCOCO)) + CHAR(13)+ 'REGISTRO '+ CASE WHEN (@CODOPE=1) THEN 'APROVADO' ELSE 'CANCELADO' END  + ' POR ' + LTRIM(RTRIM(@LGNUSU))  + ' COM STATUS DE CONTA ORIGINAL: ' + CONVERT(VARCHAR(3),@STACTA)
    				         WHERE CODUSU IN (SELECT CODUSU FROM TBCADCTA WHERE NIDCTA = @NIDCTA)
                     IF(@@ERROR=0)
                        BEGIN
                            SET @RETURN_VALUE = 1;
      			            END
    	      END               
      END
      
    RETURN @RETURN_VALUE

GO

IF OBJECT_ID ( 'dbo.PRCADCTALOC', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTALOC;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 11/04/2022 17:46:14
 Objetivo : Localiza uma conta virtual a partir do código de usuário
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCTALOC
(
    @CODUSU Integer,
    @ORGCTA Tinyint = 1,
    @TIPCTA Integer = 1
)
AS
    SET NOCOUNT ON

    SELECT ISNULL(NIDCTA,0) AS NIDCTA FROM TBCADCTA (NOLOCK) WHERE CODUSU =@CODUSU AND ORGCTA = @ORGCTA AND TIPCTA=@TIPCTA

GO

