IF OBJECT_ID ( 'dbo.PRCADCRTINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCRTINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/03/2022 15:21:18
 Objetivo : Inserção de Registros na Tabela TBCADCRT
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCRTINS
        (@CODCRT int, 
        @USUGST int, 
        @KEYACC varchar(40) = '', 
        @KEYCRT varchar(40) = '', 
        @NIDLIM int = 0, 
        @NUMLOT int, 
        @NUMCRT varchar(19), 
        @VALCRT varchar(5) = '00/00', 
        @NUMCVC smallint = 0, 
        @STACRT smallint, 
        @PSWCRT varchar(10), 
        @FILNAM varchar(255) = '', 
        @CODBLO tinyint = 0, 
        @CODFOR int, 
        @LOTMIG int = 0, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
/* Pega o proximo numero de ID */
    EXEC @CODCRT =  PRGETNXTNUM 5

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBCADCRT(CODCRT, USUGST, KEYACC, KEYCRT, NIDLIM, NUMLOT, NUMCRT, VALCRT, NUMCVC, STACRT, PSWCRT, FILNAM, CODBLO, CODFOR, LOTMIG, STAREC, UPDUSU)
                        VALUES (@CODCRT, @USUGST, @KEYACC, @KEYCRT, @NIDLIM, @NUMLOT, @NUMCRT, @VALCRT, @NUMCVC, @STACRT, @PSWCRT, @FILNAM, @CODBLO, @CODFOR, @LOTMIG, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = @CODCRT
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRADMCRTSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRADMCRTSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/03/2022 15:21:18
 Objetivo : Seleciona o registro de acordo com o Código do Cartão
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRADMCRTSEL
(
    @CODCRT Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBREGCRT (nolock) A

    WHERE     (A.CODCRT=@CODCRT)

GO

IF OBJECT_ID ( 'dbo.PRADMCRTSELCRT', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRADMCRTSELCRT;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/03/2022 15:21:18
 Objetivo : Seleciona o registro de acordo com o Código Extendido do Cartão
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRADMCRTSELCRT
(
    @NUMCRT varchar(16)
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBREGCRT (nolock) A

    WHERE     (A.NUMCRT=@NUMCRT)

GO

IF OBJECT_ID ( 'dbo.PRCADCRTUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCRTUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/03/2022 15:21:18
 Objetivo : Altera um registro da tabela TBCADCRT ()  de acordo com a chave primaria
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCRTUPD
        (@CODCRT int, 
        @USUGST int, 
        @KEYACC varchar(40) = '', 
        @KEYCRT varchar(40) = '', 
        @NIDLIM int = 0, 
        @NUMLOT int, 
        @NUMCRT varchar(19), 
        @VALCRT varchar(5) = '00/00', 
        @NUMCVC smallint = 0, 
        @STACRT smallint, 
        @PSWCRT varchar(10), 
        @FILNAM varchar(255) = '', 
        @CODBLO tinyint = 0, 
        @CODFOR int, 
        @LOTMIG int = 0, 
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
            UPDATE TBCADCRT
               SET USUGST=@USUGST
                  ,KEYACC=@KEYACC
                  ,KEYCRT=@KEYCRT
                  ,NIDLIM=@NIDLIM
                  ,NUMLOT=@NUMLOT
                  ,NUMCRT=@NUMCRT
                  ,VALCRT=@VALCRT
                  ,NUMCVC=@NUMCVC
                  ,STACRT=@STACRT
                  ,PSWCRT=@PSWCRT
                  ,FILNAM=@FILNAM
                  ,CODBLO=@CODBLO
                  ,CODFOR=@CODFOR
                  ,LOTMIG=@LOTMIG
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (CODCRT=@CODCRT)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @CODCRT
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRADMCRTGETATV', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRADMCRTGETATV;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/03/2022 15:21:19
 Objetivo : Verifica se o cartão já foi alocado para um portador
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRADMCRTGETATV
(
    @CODCRT Integer
    ,@RETURN_VALUE INT OUTPUT
)
AS
    SET NOCOUNT ON

    SET @RETURN_VALUE=0
    IF(EXISTS (SELECT 1 FROM TBADMCRT (NOLOCK) WHERE CODCRT = @CODCRT AND STACRT=131))
       SET @RETURN_VALUE=1
    ELSE
       SET @RETURN_VALUE=-1
    RETURN @RETURN_VALUE;

GO

IF OBJECT_ID ( 'dbo.PRADMCRTCHGSTA', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRADMCRTCHGSTA;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/03/2022 15:21:19
 Objetivo : ADM : Altera o Status de um Cartão
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRADMCRTCHGSTA
(
    @CODCRT Integer,
    @STACRT smallint,
    @UPDUSU Integer,
    @CODBLO smallint = 0,
    @DSCMOT varchar(50) = ''''
    ,@RETURN_VALUE INT OUTPUT
)
AS
    SET NOCOUNT ON

    SET @RETURN_VALUE=0
    DECLARE @OLDSTA VARCHAR(100) = N''
    DECLARE @STAANT SMALLINT
    DECLARE @CODUSU INT
    DECLARE @DELMEN TINYINT =0
    
    IF(NOT EXISTS(SELECT 1 FROM TBCADSTA (NOLOCK) WHERE CODSTA = @STACRT AND STAREC=1))
    		SET @RETURN_VALUE=-3
    
    IF(@RETURN_VALUE=0)
      BEGIN
        IF(NOT EXISTS(SELECT 1 FROM TBADMCRT (NOLOCK) WHERE CODCRT = @CODCRT))
    		    SET @RETURN_VALUE=-2
      END
    
    
    SELECT @OLDSTA = CONVERT(VARCHAR(10),ISNULL(STACRT,0))
          ,@STAANT = ISNULL(STAPRO,0)
          ,@CODUSU=ASSUSU 
      FROM TBADMCRT (NOLOCK) 
     WHERE CODCRT =@CODCRT  
            
    IF(@RETURN_VALUE=0)
    		BEGIN         
        	  UPDATE TBADMCRT 
        		   SET STAPRO = STACRT,
        			     STACRT = @STACRT,
        			     UPDUSU = @UPDUSU,
        			     DATUPD = GETDATE(),
    					     CODBLO = @CODBLO,
                   DSCMOT = @DSCMOT,
      		         DATCAN = CASE WHEN @MENDEL=1 THEN GETDATE() ELSE DATCAN END
        	   WHERE CODCRT = @CODCRT AND STACRT<>@STACRT
               
            IF(@@ERROR=0)
                BEGIN
                    SET @RETURN_VALUE=1        
                    DECLARE @AUDSRC VARCHAR(100) = N'STATUS DO CARTAO ' + @OLDSTA
                    DECLARE @AUDCHG VARCHAR(100) = N'STATUS DO CARTAO ' + CONVERT(VARCHAR(10),@STACRT) 
                    INSERT INTO TBAUDDAT ( UPDUSU,    AUDDAT, AUDKEY, AUDIDR,   AUDIMS,                AUDOBJ, AUDSRC, AUDCHG) 
                                  VALUES (@UPDUSU, GETDATE(),     18, @CODCRT,     104, OBJECT_NAME(@@PROCID),@AUDSRC, @AUDCHG)        
                    SET @RETURN_VALUE=1                                          
                END
          END        
    RETURN @RETURN_VALUE

GO

IF OBJECT_ID ( 'dbo.PRADMCRTCHGPSW', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRADMCRTCHGPSW;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/03/2022 15:21:19
 Objetivo : Efetua a alteração da senha do cartão
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRADMCRTCHGPSW
(
    @CODCRT Integer,
    @PSWCRT varchar(10),
    @UPDUSU Integer,
    @NUMIPA varchar(50)
    ,@RETURN_VALUE INT OUTPUT
)
AS
    SET NOCOUNT ON

    SET @RETURN_VALUE=0
    DECLARE @OLDPSW VARCHAR(100) = N''
    IF(EXISTS (SELECT 1 FROM TBADMCRT (NOLOCK) WHERE CODCRT =@CODCRT AND STAREC=1))
      BEGIN
        SELECT @OLDPSW = ISNULL(PSWCRT,'') FROM TBADMCRT (NOLOCK) WHERE CODCRT =@CODCRT  
    	  UPDATE TBADMCRT 
    		   SET PSWCRT = @PSWCRT,
    			     UPDUSU = @UPDUSU,
    			     DATUPD = GETDATE()
    	   WHERE CODCRT = @CODCRT
           AND STAREC = 1
        IF(@@ERROR=0)
            BEGIN
                SET @RETURN_VALUE=1        
                DECLARE @AUDSRC VARCHAR(100) = N'SENHA DO CARTAO ' + @OLDPSW
                DECLARE @AUDCHG VARCHAR(100) = N'SENHA DO CARTAO ' + @PSWCRT  
                INSERT INTO TBAUDDAT ( UPDUSU,    AUDDAT, AUDKEY, AUDIDR,   AUDIMS,                AUDOBJ, AUDSRC, AUDCHG) 
                              VALUES (@UPDUSU, GETDATE(),     18, @CODCRT,     105, OBJECT_NAME(@@PROCID),@AUDSRC, @AUDCHG)        
    
            END
      END  
    ELSE
        SET @RETURN_VALUE=-2
    END TRY
    RETURN @RETURN_VALUE

GO

IF OBJECT_ID ( 'dbo.PRADMCRTCHGCVC', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRADMCRTCHGCVC;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/03/2022 15:21:19
 Objetivo : Efetua a alteração do número o CVC do Cartão
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRADMCRTCHGCVC
(
    @CODCRT Integer,
    @NUMCVC smallint,
    @UPDUSU Integer
    ,@RETURN_VALUE INT OUTPUT
)
AS
    SET NOCOUNT ON

    SET @RETURN_VALUE=0
    DECLARE @OLDCVC VARCHAR(10) = N''
    BEGIN TRANSACTION
    BEGIN TRY
    IF(EXISTS (SELECT 1 FROM TBCADCRT WHERE CODCRT =@CODCRT))
      BEGIN
        SELECT @OLDCVC = CONVERT(VARCHAR(10),ISNULL(NUMCVC,0)) 
         FROM TBCADCRT WHERE CODCRT =@CODCRT
    	  UPDATE TBREGCRT 
    		   SET NUMCVC = @NUMCVC,
    			     UPDUSU = @UPDUSU,
    			     DATUPD = GETDATE()
    	   WHERE CODCRT = @CODCRT
           AND STAREC = 1
        IF(@@ERROR=0)
            BEGIN
                DECLARE @AUDSRC VARCHAR(100) = N'NUMERO DO CVC ' + @OLDCVC          
                DECLARE @AUDCHG VARCHAR(100) = N'NUMERO DO CVC ' + CONVERT(VARCHAR(10), @NUMCVC)  
                INSERT INTO TBAUDDAT ( UPDUSU,    AUDDAT, AUDKEY, AUDIDR,   AUDIMS,                AUDOBJ, AUDSRC, AUDCHG) 
                              VALUES (@UPDUSU, GETDATE(),     14, @CODCRT,     106, OBJECT_NAME(@@PROCID),@AUDSRC, @AUDCHG)        
            END
      END        
    END TRY
    BEGIN CATCH
      SET @RETURN_VALUE=-1
    END CATCH
    
    IF(@RETURN_VALUE=1)
      COMMIT TRAN
    ELSE
       ROLLBACK TRAN
    RETURN @RETURN_VALUE

GO

IF OBJECT_ID ( 'dbo.PRREGCRTCHGASS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGCRTCHGASS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 01/03/2022 15:21:19
 Objetivo : Efetua o cancelamento da associação de um cartão
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGCRTCHGASS
(
    @CODCRT Integer,
    @UPDUSU Integer
    ,@RETURN_VALUE INT OUTPUT
)
AS
    SET NOCOUNT ON

    SET @RETURN_VALUE=0
    IF(EXISTS (SELECT 1 FROM TBREGCRT WHERE CODCRT =@CODCRT AND ASSUSU>0 AND ASSPRT>0))
      BEGIN
    	  UPDATE TBREGCRT 
    		   SET NOMPRT='',
    		       NOMCR1='',
    			     NOMCR2='',
    			     NOMCR3='',
               STAPRO = STACRT,           
    			     STACRT = 101,
    			     UPDUSU = @UPDUSU,
    			     DATUPD = GETDATE(),
               ASSPRT = 0,
               ASSUSU = 0
    	   WHERE CODCRT = @CODCRT
        IF(@@ERROR=0)
            BEGIN
                DECLARE @AUDSRC VARCHAR(100) = N'ALTEROU O STATUS DO CARTAO PARA 101 - EM ESTOQUE
                DECLARE @AUDCHG VARCHAR(100) = N'ALTEROU O STATUS DO CARTAO PARA 101 - EM ESTOQUE  
                INSERT INTO TBAUDDAT ( UPDUSU,    AUDDAT, AUDKEY, AUDIDR,   AUDIMS,                AUDOBJ, AUDSRC, AUDCHG) 
                              VALUES (@UPDUSU, GETDATE(),     14, @CODCRT,     104, OBJECT_NAME(@@PROCID),@AUDSRC, @AUDCHG)        
            END
      END        
    RETURN @RETURN_VALUE

GO

