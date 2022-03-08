IF OBJECT_ID ( 'dbo.PRCADCTAINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTAINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/03/2022 21:23:11
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

    IF(@ORGCTA IN (1,3,4))
        BEGIN
            IF(EXISTS(SELECT 1 FROM TBCADCTA (NOLOCK) WHERE CODUSU =@CODUSU AND ORGCTA=@ORGCTA))              
                BEGIN
                    SELECT @RETURN_VALUE = ISNULL(NIDCTA,0) FROM TBCADCTA (NOLOCK) WHERE CODUSU =@CODUSU AND ORGCTA=@ORGCTA AND NUMCTA<>''
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
            SET @CODCMF = '000000000000000'
            SET @NOMBNF = ' '
        END
    SELECT @NUMBCO = LTRIM(RTRIM(RIGHT('000' + ISNULL(@NUMBCO, '0'),3)))

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
          UPDATE TBCADCTA SET REGATV=0 WHERE CODUSU=@CODUSU AND ORGCTA=2 AND NIDCTA<>@RETURN_VALUE
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRCADCTASEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTASEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/03/2022 21:23:11
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
     Date : 01/03/2022 21:23:11
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
     Date : 01/03/2022 21:23:11
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
     Date : 01/03/2022 21:23:11
 Objetivo : Altera um registro da tabela TBCADCTA (Virtual Account)  de acordo com a chave identity
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
               SET REGATV=@REGATV
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
IF OBJECT_ID ( 'dbo.PRCADCTASELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTASELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/03/2022 21:23:11
 Objetivo : Obtêm todos os registros de contas virtuais registradas conforme parametro fornecido
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCTASELALL
(
    @CODUSU Integer=NULL
)
AS
    SET NOCOUNT ON
    SELECT
        A.NIDCTA
        ,A.CODUSU
    	,B.NOMUSU
        ,A.NUMAGE
        ,A.NUMBCO
    	,DSCBCO = ISNULL(C.DSCTAB,'')
        ,A.NUMCTA
        ,A.NUMDVE
        ,A.ORGCTA
    	,DSCORG = ISNULL(D.DSCTAB,'')
        ,A.TIPCTA
    	,E.DSCCTA
    	,E.TIPEXT
        ,A.STACTA
    	,F.DSCSTA
        ,A.DATVAL
        ,A.APLLIM
        ,A.VLRLIM
        ,A.TIPBNF
        ,DSCBNF = ISNULL(G.DSCTAB,'')
        ,CODCMF = dbo.FormatCMF(A.CODCMF)
        ,A.NOMBNF
        ,A.STAREC
        ,DSCREC = ISNULL(H.DSCTAB,'')
        ,A.UPDUSU
    	,LGNUSU = ISNULL(I.LGNUSU,'')
      ,DATTRF = CONVERT(VARCHAR,DATVAL,112)
     FROM TBCADCTA (NOLOCK) A 
     INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.CODUSU)
      LEFT JOIN TBTABGER (NOLOCK) C ON (C.NUMTAB = 12 AND C.KEYTXT = A.NUMBCO)
     INNER JOIN TBTABGER (NOLOCK) D ON (D.NUMTAB = 23 AND D.KEYCOD = A.ORGCTA)
     INNER JOIN TBTIPCTA (NOLOCK) E ON (E.TIPCTA = A.TIPCTA)
     INNER JOIN TBCADSTA (NOLOCK) F ON (F.CODSTA = A.STACTA)
     INNER JOIN TBTABGER (NOLOCK) G ON (G.NUMTAB = 20 AND G.KEYCOD = A.TIPBNF)
     INNER JOIN TBTABGER (NOLOCK) H ON (H.NUMTAB =  7 AND H.KEYCOD = A.STAREC)
      LEFT JOIN TBLGNUSU (NOLOCK) I ON (I.CODUSU = A.UPDUSU AND I.REGATV = 1)
     WHERE       (@CODUSU IS NULL OR A.CODUSU=@CODUSU)



GO

IF OBJECT_ID ( 'dbo.PRCADCTADELNID', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTADELNID;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/03/2022 21:23:11
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
          UPDATE TBCADCTA SET STAREC=0, STACTA=253, UPDUSU=@UPDUSU, DATUPD=GETDATE() WHERE NIDCTA = @NIDCTA
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
     Date : 01/03/2022 21:23:11
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
    DECLARE @LGNUSU  VARCHAR(50)=N'';
    
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
      
    BEGIN TRAN
    
    IF (@CODUSU>0 AND @RETURN_VALUE=0 AND @STAREC>=0)
    	BEGIN
    		UPDATE TBCADCTA
    		   SET STACTA = CASE WHEN (@CODOPE=1) THEN 250 ELSE 251 END
              ,STAREC = CASE WHEN (@CODOPE=1) THEN 1 ELSE 0 END
              ,UPDUSU = @UPDUSU
              ,DATUPD = GETDATE()
             WHERE NIDCTA = @NIDCTA
             IF(@@ERROR=0)
    			      BEGIN
    				        UPDATE TBCADGER
    				           SET STAUSU = CASE WHEN (@CODOPE=1) THEN 61 ELSE 3 END
                          ,STAREC = CASE WHEN (@CODOPE=1) THEN 1 ELSE 13 END
                          ,UPDUSU = @UPDUSU
                          ,DATUPD = GETDATE()
                          ,DSCOCO = LTRIM(RTRIM(DSCOCO)) + CHAR(13)+ 'REGISTRO '+ CASE WHEN (@CODOPE=1) THEN 'APROVADO' ELSE 'CANCELADO' END  + ' POR ' + LTRIM(RTRIM(@LGNUSU))  + ' COM STATUS DE REGISTRO : ' + CONVERT(VARCHAR(3),@STAREC)
    				         WHERE CODUSU IN (SELECT CODUSU FROM TBCADCTA WHERE NIDCTA = @NIDCTA)
                     IF(@@ERROR=0)
                        BEGIN
                            SET @RETURN_VALUE = 1;
      			      END
    	END               
      
    IF(@RETURN_VALUE=1)
       COMMIT TRANSACTION
    ELSE
       ROLLBACK TRANSACTION
    RETURN @RETURN_VALUE

GO

IF OBJECT_ID ( 'dbo.PRCADCTALOC', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTALOC;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/03/2022 21:23:11
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

