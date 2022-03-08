IF OBJECT_ID ( 'dbo.PRREGCRTINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGCRTINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 18:52:54
 Objetivo : Inserção de Registros na Tabela TBREGCRT
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGCRTINS
        (@CODCRT int, 
        @NIDCTA int = 0, 
        @USUPRO int = 0, 
        @TIPPRO tinyint = 0, 
        @ORGATV tinyint = 0, 
        @ASSUSU int = 0, 
        @SECLVL tinyint = 3, 
        @DATATV date = null, 
        @DATCAN date = null, 
        @CALMEN tinyint = 0, 
        @USUMEN int = 0, 
        @NOMPRT varchar(70), 
        @NUMCRT varchar(25), 
        @VALCRT varchar(5) = '00/00', 
        @PSWCRT varchar(6) = '0000', 
        @NUMCVC smallint = 0, 
        @STAPRO smallint = 0, 
        @STACRT smallint, 
        @NOMCR1 varchar(30) = '', 
        @NOMCR2 varchar(30) = '', 
        @NOMCR3 varchar(30) = '', 
        @OPRCRT tinyint = 0, 
        @DATEXT date = null, 
        @CODBLO tinyint = 0, 
        @TIPPRT tinyint = 1, 
        @DSCMOT varchar(50) = '', 
        @APLCON tinyint = 1, 
        @APLSAQ tinyint = 1, 
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
            INSERT INTO TBREGCRT(CODCRT, NIDCTA, USUPRO, TIPPRO, ORGATV, ASSUSU, SECLVL, DATATV, DATCAN, CALMEN, USUMEN, NOMPRT, NUMCRT, VALCRT, PSWCRT, NUMCVC, STAPRO, STACRT, NOMCR1, NOMCR2, NOMCR3, OPRCRT, DATEXT, CODBLO, TIPPRT, DSCMOT, APLCON, APLSAQ, LOTMIG, STAREC, UPDUSU)
                        VALUES (@CODCRT, @NIDCTA, @USUPRO, @TIPPRO, @ORGATV, @ASSUSU, @SECLVL, @DATATV, @DATCAN, @CALMEN, @USUMEN, @NOMPRT, @NUMCRT, @VALCRT, @PSWCRT, @NUMCVC, @STAPRO, @STACRT, @NOMCR1, @NOMCR2, @NOMCR3, @OPRCRT, @DATEXT, @CODBLO, @TIPPRT, @DSCMOT, @APLCON, @APLSAQ, @LOTMIG, @STAREC, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PRREGCRTSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGCRTSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 18:52:55
 Objetivo : Seleciona o registro de acordo com o Código do Cartão
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGCRTSEL
(
    @CODCRT Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBREGCRT (nolock) A

    WHERE     (A.CODCRT=@CODCRT)

GO

IF OBJECT_ID ( 'dbo.PRREGCRTSELCRT', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGCRTSELCRT;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 18:52:55
 Objetivo : Seleciona o registro de acordo com o Código Extendido do Cartão
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGCRTSELCRT
(
    @NUMCRT varchar(16)
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBREGCRT (nolock) A

    WHERE     (A.NUMCRT=@NUMCRT)

GO

IF OBJECT_ID ( 'dbo.PRREGCRTUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGCRTUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 18:52:55
 Objetivo : Altera um registro da tabela TBREGCRT (Active Cards)  de acordo com a chave primaria
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGCRTUPD
        (@CODCRT int, 
        @NIDCTA int = 0, 
        @USUPRO int = 0, 
        @TIPPRO tinyint = 0, 
        @ORGATV tinyint = 0, 
        @ASSUSU int = 0, 
        @SECLVL tinyint = 3, 
        @DATATV date = null, 
        @DATCAN date = null, 
        @CALMEN tinyint = 0, 
        @USUMEN int = 0, 
        @NOMPRT varchar(70), 
        @NUMCRT varchar(25), 
        @VALCRT varchar(5) = '00/00', 
        @PSWCRT varchar(6) = '0000', 
        @NUMCVC smallint = 0, 
        @STAPRO smallint = 0, 
        @STACRT smallint, 
        @NOMCR1 varchar(30) = '', 
        @NOMCR2 varchar(30) = '', 
        @NOMCR3 varchar(30) = '', 
        @OPRCRT tinyint = 0, 
        @DATEXT date = null, 
        @CODBLO tinyint = 0, 
        @TIPPRT tinyint = 1, 
        @DSCMOT varchar(50) = '', 
        @APLCON tinyint = 1, 
        @APLSAQ tinyint = 1, 
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
            UPDATE TBREGCRT
               SET NIDCTA=@NIDCTA
                  ,USUPRO=@USUPRO
                  ,TIPPRO=@TIPPRO
                  ,ORGATV=@ORGATV
                  ,ASSUSU=@ASSUSU
                  ,SECLVL=@SECLVL
                  ,DATATV=@DATATV
                  ,DATCAN=@DATCAN
                  ,CALMEN=@CALMEN
                  ,USUMEN=@USUMEN
                  ,NOMPRT=@NOMPRT
                  ,NUMCRT=@NUMCRT
                  ,VALCRT=@VALCRT
                  ,PSWCRT=@PSWCRT
                  ,NUMCVC=@NUMCVC
                  ,STAPRO=@STAPRO
                  ,STACRT=@STACRT
                  ,NOMCR1=@NOMCR1
                  ,NOMCR2=@NOMCR2
                  ,NOMCR3=@NOMCR3
                  ,OPRCRT=@OPRCRT
                  ,DATEXT=@DATEXT
                  ,CODBLO=@CODBLO
                  ,TIPPRT=@TIPPRT
                  ,DSCMOT=@DSCMOT
                  ,APLCON=@APLCON
                  ,APLSAQ=@APLSAQ
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
IF OBJECT_ID ( 'dbo.PRREGCRTSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGCRTSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 18:52:55
 Objetivo : Obtêm uma lista de todos os cartões da base ativa conforme parâmetros de pesquisa informados
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGCRTSELALL
(
    @USUPRO Integer=NULL,
    @STACRT smallint=NULL,
    @NUMCRT varchar(16)=NULL,
    @NOMPRT varchar(100)=NULL
)
AS
    SET NOCOUNT ON
    IF(@NUMCRT = '')
        SET @NUMCRT = NULL
    IF(@NUMCRT IS NOT NULL)
        SET @NUMCRT = '%' +  RTRIM(@NUMCRT) +'%';
    IF(@NOMPRT = '')
        SET @NOMPRT = NULL
    IF(@NOMPRT IS NOT NULL)
        SET @NOMPRT = '%' +  RTRIM(@NOMPRT) +'%';
    SELECT A.* 
          ,CNVCAD = FORMAT(A.DATCAD, 'dd/MM/yyyy HH:mm')
          ,CNVUPD = FORMAT(A.DATUPD, 'dd/MM/yyyy HH:mm')    
          ,CNVATV = ISNULL(FORMAT(A.DATATV, 'dd/MM/yyyy'),'')    
          ,CNVCAN = ISNULL(FORMAT(A.DATCAN, 'dd/MM/yyyy'),'')          
          ,ISNULL(B.NOMUSU,'') AS NOMUSU
          ,ISNULL(C.NOMUSU,'') AS DSCTOM
          ,ISNULL(E.DSCTAB,'') AS DSCREC
          ,ISNULL(D.DSCSTA,'') AS DSCSTA
          ,LGNUSU = ISNULL(F.LGNUSU,'')
          ,DSCPRO = ISNULL(I.DSCPRO,'')
      FROM TBREGCRT (NOLOCK) A
     INNER JOIN TBCADGER (NOLOCK) B ON (A.ASSUSU = B.CODUSU)
      LEFT JOIN TBCADGER (NOLOCK) C ON (A.USUMEN = C.CODUSU)
     INNER JOIN TBCADSTA (NOLOCK) D ON (A.STACRT = D.CODSTA)
     INNER JOIN TBTABGER (NOLOCK) E ON (E.NUMTAB = 7 AND E.KEYCOD = A.STAREC)
      LEFT JOIN TBLGNUSU (NOLOCK) F ON (F.CODUSU = A.UPDUSU AND F.REGATV=1)
     INNER JOIN TBUSUPRO (NOLOCK) G ON (G.USUPRO = A.USUPRO)  
     INNER JOIN TBCADGER (NOLOCK) H ON (H.CODUSU = G.CODUSU)   
     INNER JOIN TBCADPRO (NOLOCK) I ON (I.CODPRO = G.CODPRO)    
     WHERE A.CODCRT>0
 AND       (@USUPRO IS NULL OR A.USUPRO=@USUPRO)
       AND (@STACRT IS NULL OR A.STACRT=@STACRT)
       AND (@NUMCRT IS NULL OR A.NUMCRT LIKE @NUMCRT)
       AND (@NOMPRT IS NULL OR A.NOMPRT LIKE @NOMPRT)
     ORDER BY A.NUMCRT



GO

IF OBJECT_ID ( 'dbo.PRREGCRTCHGONLPAR', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGCRTCHGONLPAR;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 18:52:55
 Objetivo : Altera o campo de permissão de compra on-line
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGCRTCHGONLPAR
(
    @CODCRT Integer,
    @UPDUSU Integer,
    @APLCON Tinyint
)
AS
    SET NOCOUNT ON

    DECLARE @RETURN_VALUE INT =0
    DECLARE @AUDCHG VARCHAR(512)=N'';
    IF(@CODCRT<=0)
       SET @RETURN_VALUE=-1
    IF(@UPDUSU<=0)
       SET @RETURN_VALUE=-2
       
       
    IF(EXISTS(SELECT 1 FROM TBREGCRT (NOLOCK) WHERE CODCRT = @CODCRT AND @RETURN_VALUE=0))
       BEGIN
          SET @AUDCHG = N'DESATIVOU O CARTAO PARA COMPRA ONLINE'    
          IF(@APLCON = 1)
            SET @AUDCHG = N'ATIVOU O CARTAO PARA COMPRA ONLINE' 
          UPDATE TBREGCRT SET APLCON = @APLCON, DATUPD = GETDATE(), UPDUSU = @UPDUSU WHERE CODCRT = @CODCRT
          IF(@@ERROR=0)
              BEGIN
                  INSERT INTO TBAUDDAT ( UPDUSU,    AUDDAT, AUDKEY, AUDIDR,   AUDIMS,                AUDOBJ, AUDSRC,  AUDCHG) 
                                VALUES (@UPDUSU, GETDATE(),     14, @CODCRT,       0, OBJECT_NAME(@@PROCID),     '', @AUDCHG)        
              END
    	END
    ELSE
        SET @RETURN_VALUE=-3;
    RETURN @RETURN_VALUE

GO

IF OBJECT_ID ( 'dbo.PRREGCRTCHGSAQPAR', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGCRTCHGSAQPAR;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 18:52:55
 Objetivo : Altera o campo de permissão de saque
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGCRTCHGSAQPAR
(
    @CODCRT Integer,
    @UPDUSU Integer,
    @APLSAQ Tinyint
)
AS
    SET NOCOUNT ON

    DECLARE @RETURN_VALUE INT =0
    DECLARE @AUDCHG VARCHAR(512)=N'';
    IF(@CODCRT<=0)
       SET @RETURN_VALUE=-1
    IF(@UPDUSU<=0)
       SET @RETURN_VALUE=-2
       
    IF(EXISTS(SELECT 1 FROM TBREGCRT (NOLOCK) WHERE CODCRT = @CODCRT AND @RETURN_VALUE=0))
       BEGIN
          SET @AUDCHG = N'DESATIVOU O CARTAO PARA SAQUE'    
          IF(@APLCON = 1)
            SET @AUDCHG = N'ATIVOU O CARTAO PARA SAQUE' 
          UPDATE TBREGCRT SET APLSAQ = @APLSAQ, DATUPD = GETDATE(), UPDUSU = @UPDUSU WHERE CODCRT = @CODCRT
          IF(@@ERROR=0)
              BEGIN
                  INSERT INTO TBAUDDAT ( UPDUSU,    AUDDAT, AUDKEY, AUDIDR,   AUDIMS,                AUDOBJ, AUDSRC,  AUDCHG) 
                                VALUES (@UPDUSU, GETDATE(),     14, @CODCRT,       0, OBJECT_NAME(@@PROCID),     '', @AUDCHG)        
              END
    	END
    ELSE
        SET @RETURN_VALUE=-3;
    RETURN @RETURN_VALUE

GO

IF OBJECT_ID ( 'dbo.PRREGCRTCHGOWN', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGCRTCHGOWN;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 18:52:55
 Objetivo : Altera o Gestor do Cartão em uso
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGCRTCHGOWN
(
    @CODCRT Integer,
    @UPDUSU Integer,
    @USUPRO Integer = 13
)
AS
    SET NOCOUNT ON

    DECLARE @RETURN_VALUE INT =0
    DECLARE @NUMCRT VARCHAR(20)=N'';
    DECLARE @AUDDSC VARCHAR(512)=N'';
    DECLARE @NIDCRT VARCHAR(12)=N'';
    
    IF(@USUPRO <=0)
        SET @RETURN_VALUE=-6   
    
    IF(@RETURN_VALUE=0)
    		BEGIN
    			IF(@CODCRT<=0)
    				SET @RETURN_VALUE=-1
    	    END
    IF(@RETURN_VALUE=0)
    		BEGIN
    			IF(@UPDUSU<=0)
    				SET @RETURN_VALUE=-2
    		END
    
    IF(@RETURN_VALUE=0)
    		BEGIN
    			IF(EXISTS(SELECT 1 FROM TBREGCRT (NOLOCK) WHERE CODCRT = @CODCRT AND USUPRO=@USUPRO)) 
    				SET @RETURN_VALUE = -3
    		END
    
    IF(@RETURN_VALUE = 0)
       BEGIN
    		   SELECT @NUMCRT = ISNULL(NUMCRT,'') FROM TBREGCRT (NOLOCK) WHERE CODCRT = @CODCRT
           IF(@NUMCRT <>'')
              BEGIN
                IF(LEN(@NUMCRT)=16)
                    BEGIN
    		                 UPDATE TBREGCRT 
                            SET USUPRO = @USUPRO, 
                                DATUPD = GETDATE(), 
                                UPDUSU = @UPDUSU WHERE CODCRT = @CODCRT
    		                 IF(@@ERROR=0)
                            BEGIN
                                SET @RETURN_VALUE=1                  
                            END
                    END
              ELSE
                  SET @RETURN_VALUE = -4
            END
          ELSE
            SET @RETURN_VALUE = -5
    	END
    RETURN @RETURN_VALUE

GO

IF OBJECT_ID ( 'dbo.PRREGCRTCHGMEN', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGCRTCHGMEN;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 18:52:55
 Objetivo : Executa Ações de Atualização pela manutenção do Cartão
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGCRTCHGMEN
(
    @CODCRT Integer,
    @UPDUSU Integer,
    @CODACT Tinyint
    ,@RETURN_VALUE INT OUTPUT
)
AS
    SET NOCOUNT ON

    SET @RETURN_VALUE = 0
    IF(@UPDUSU<=0)
       SET @RETURN_VALUE=-1
     	  BEGIN
    			UPDATE TBCTRMEN 
             SET STAREC = CASE WHEN (@CODACT=1) THEN 1 ELSE 13 END, 
                 STAMEN = CASE WHEN (@CODACT=1) THEN 261 ELSE 267 END,  
                 UPDUSU = @UPDUSU,
                 DATUPD = GETDATE() 
            WHERE CODCRT=@CODCRT AND STAREC NOT IN (0,9) AND STAMEN IN (SELECT CODSTA FROM TBCADSTA WHERE STAREC=1 AND CANCHG=1)
    			SET @RETURN_VALUE = @@ROWCOUNT
    		END
    	RETURN @RETURN_VALUE

GO

IF OBJECT_ID ( 'dbo.PRREGCRTGETATV', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGCRTGETATV;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 18:52:55
 Objetivo : Verifica se o cartão e o CPF/CNPJ informado são passíveis de ativação
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGCRTGETATV
(
    @NUMCRT varchar(16),
    @CODCMF varchar(15)
    ,@RETURN_VALUE INT OUTPUT
)
AS
    SET NOCOUNT ON

    SET @RETURN_VALUE=0
    DECLARE @CODCRT INT
    DECLARE @STACRT SMALLINT
    DECLARE @CMFASS VARCHAR(15)
    SELECT @CODCRT = A.CODCRT, @STACRT = A.STACRT, @CMFASS = ISNULL(B.CODCMF,'') 
      FROM TBREGCRT (NOLOCK) A  
      LEFT JOIN TBCADGER (NOLOCK) B ON (A.ASSUSU = B.CODUSU) 
     WHERE A.NUMCRT=@NUMCRT
    
     IF(@@ROWCOUNT>0)
    	BEGIN
    		IF(@STACRT = 103)
    			BEGIN
    				IF(@CODCMF = @CMFASS)
    					SET @RETURN_VALUE = 1
    			END 
    		ELSE
    			IF(@STACRT = 109)
    				BEGIN
    					IF(@CODCMF = @CMFASS)
    						SET @RETURN_VALUE = -1					
    					ELSE
    						SET @RETURN_VALUE = -2					
    				END
    			IF(@STACRT = 107)
    				SET @RETURN_VALUE = -3					
    	END
    ELSE
    	SET @RETURN_VALUE = -4					
    RETURN @RETURN_VALUE;

GO

IF OBJECT_ID ( 'dbo.PRREGCRTLCKCRT', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGCRTLCKCRT;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 18:52:55
 Objetivo : Operação@1 -Bloqueia o Cartão@2 - Reverte o Bloqueio Anterior@3 -Fixa o cartão como aguardando desbloqueio
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGCRTLCKCRT
(
    @CODCRT Integer,
    @UPDUSU Integer,
    @TIPOPE Tinyint
    ,@RETURN_VALUE INT OUTPUT
)
AS
    SET NOCOUNT ON

    SET @RETURN_VALUE = 0
    DECLARE @AUDCHG VARCHAR(100)=N'';
    DECLARE @AUDSRC VARCHAR(100) = N''
    DECLARE @STAPRO SMALLINT
    DECLARE @STACRT SMALLINT
    SELECT @STAPRO = STAPRO, @STACRT = STACRT FROM TBRGCRT WHERE CODCRT =@CODCRT
    IF(@TIPOPE=1)
    	BEGIN
    		 UPDATE TBREGCRT SET STAPRO = STACRT, STACRT = 127, DATUPD=GETDATE(), UPDUSU=@UPDUSU WHERE CODCRT =@CODCRT
    			IF(@@ERROR=0)
               BEGIN
                  SET @RETURN_VALUE=1           
                  SET @AUDSRC= 'STAPRO: ' + CONVERT(VARCHAR, @STAPRO) + ',STACRT: ' + CONVERT(VARCHAR, @STACRT)
                  SET @AUDCHG= 'STAPRO: ' + CONVERT(VARCHAR, @STACRT) + ',STACRT: 127'
               END
    					
          ELSE
              SET @RETURN_VALUE=-1
    	END	  
    	  
    IF(@TIPOPE=2)
    	BEGIN
    		 UPDATE TBREGCRT SET STACRT = STAPRO, DATUPD=GETDATE(), UPDUSU=@UPDUSU WHERE CODCRT =@CODCRT  AND STAPRO<>0
     		IF(@@ERROR=0)
              BEGIN
    					SET @RETURN_VALUE=1
                  SET @AUDSRC= 'STACRT: ' + CONVERT(VARCHAR, @STACRT)
                  SET @AUDCHG= 'STACRT: ' + CONVERT(VARCHAR, @STAPRO)
              END
          ELSE
              SET @RETURN_VALUE=-2
    	END	  
    
    IF(@TIPOPE=3)
    	BEGIN
    		 UPDATE TBREGCRT SET STACRT = 128 WHERE CODCRT = @CODCRT AND STAPRO <> 0
    			IF(@@ERROR=0)
              BEGIN
    					SET @RETURN_VALUE=1
                  SET @AUDSRC= 'STACRT: ' + CONVERT(VARCHAR, @STACRT)
                  SET @AUDCHG= 'STACRT: 128'
              END
          ELSE
              SET @RETURN_VALUE=-2
    	END	  
    
      IF(@RETURN_VALUE=1)
        BEGIN
      INSERT INTO TBAUDDAT ( UPDUSU,    AUDDAT, AUDKEY, AUDIDR,   AUDIMS,                AUDOBJ, AUDSRC, AUDCHG) 
                    VALUES (@UPDUSU, GETDATE(),     14, @CODCRT,      0, OBJECT_NAME(@@PROCID),@AUDSRC, @AUDCHG)        
        END
    RETURN @RETURN_VALUE

GO

IF OBJECT_ID ( 'dbo.PRREGCRTCHGSTA', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGCRTCHGSTA;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 18:52:55
 Objetivo : Altera o Status de um Cartão
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGCRTCHGSTA
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
        IF(NOT EXISTS(SELECT 1 FROM TBREGCRT (NOLOCK) WHERE CODCRT = @CODCRT))
    		    SET @RETURN_VALUE=-2
      END
    
    IF(@RETURN_VALUE=0)
      BEGIN
          SELECT @DELMEN = ISNULL(DELMEN,-1) FROM TBCADSTA (NOLOCK) WHERE CODSTA = @STACRT AND STAREC=1  
      END
    
    SELECT @OLDSTA = CONVERT(VARCHAR(10),ISNULL(STACRT,0))
          ,@STAANT = ISNULL(STAPRO,0)
          ,@CODUSU=ASSUSU 
      FROM TBREGCRT (NOLOCK) 
     WHERE CODCRT =@CODCRT  
            
    IF(@RETURN_VALUE=0)
    		BEGIN         
        	  UPDATE TBREGCRT 
        		   SET STAPRO = STACRT,
        			     STACRT = @STACRT,
        			     UPDUSU = @UPDUSU,
        			     DATUPD = GETDATE(),
    					     CODBLO = @CODBLO,
                   DSCMOT = @DSCMOT,
      		         DATCAN = CASE WHEN @DELMEN=1 THEN GETDATE() ELSE DATCAN END
        	   WHERE CODCRT = @CODCRT AND STAREC = 1 AND STACRT<>@STACRT
               
            IF(@@ERROR=0)
                BEGIN
                
                    IF(@CODUSU>0 AND @STACRT=109)
                        BEGIN
                            UPDATE TBCADUSU SET STAUSU = 61, STAREC=1, UPDUSU = @UPDUSU, DATUPD = GETDATE() WHERE CODUSU = @CODUSU AND STAUSU <>61
                            UPDATE TBCADCTA SET STACTA = 250,STAREC=1,UPDUSU=@UPDUSU,DATUPD=GETDATE() WHERE CODUSU=@CODUSU AND ORGCTA=1 AND STACTA<>250                   
                        END
                    IF(@DELMEN=1)
                       UPDATE TBCTRMEN SET STAMEN =269, STAREC =13, UPDUSU=@UPDUSU, DATUPD=GETDATE() WHERE STAMEN IN (SELECT CODSTA FROM TBCADSTA WHERE CANCHG=1)
    
    
                SET @RETURN_VALUE=1        
                DECLARE @AUDSRC VARCHAR(100) = N'STATUS DO CARTAO ' + @OLDSTA
                DECLARE @AUDCHG VARCHAR(100) = N'STATUS DO CARTAO ' + CONVERT(VARCHAR(10),@STACRT) 
                INSERT INTO TBAUDDAT ( UPDUSU,    AUDDAT, AUDKEY, AUDIDR,   AUDIMS,                AUDOBJ, AUDSRC, AUDCHG) 
                              VALUES (@UPDUSU, GETDATE(),     14, @CODCRT,     104, OBJECT_NAME(@@PROCID),@AUDSRC, @AUDCHG)        
    
                    SET @RETURN_VALUE=1                                          
                END
          END        
    RETURN @RETURN_VALUE

GO

IF OBJECT_ID ( 'dbo.PRREGCRTCHGPSW', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGCRTCHGPSW;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 18:52:55
 Objetivo : Efetua a alteração da senha do cartão
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGCRTCHGPSW
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
    IF(EXISTS (SELECT 1 FROM TBREGCRT (NOLOCK) WHERE CODCRT =@CODCRT AND STAREC=1))
      BEGIN
        SELECT @OLDPSW = ISNULL(PSWCRT,'') FROM TBREGCRT (NOLOCK) WHERE CODCRT =@CODCRT  
    	  UPDATE TBREGCRT 
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
                              VALUES (@UPDUSU, GETDATE(),     14, @CODCRT,     105, OBJECT_NAME(@@PROCID),@AUDSRC, @AUDCHG)        
    
            END
      END  
    ELSE
        SET @RETURN_VALUE=-2
    RETURN @RETURN_VALUE

GO

IF OBJECT_ID ( 'dbo.PRREGCRTCHGCVC', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGCRTCHGCVC;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 18:52:55
 Objetivo : Efetua a alteração do número o CVC do Cartão
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGCRTCHGCVC
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
     Date : 07/03/2022 18:52:55
 Objetivo : Cancela a associação de um cartão
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
    DECLARE @STAOLD SMALINT
    IF(EXISTS (SELECT 1 FROM TBREGCRT WHERE CODCRT =@CODCRT AND ASSUSU>0 AND STACRT<>101))
      BEGIN
        SELECT @STAOLD = STACRT FROM TBREGCRT WHERE CODCRT=@CODCRT
        UPDATE TBREGCRT 
           SET NOMPRT='',
               NOMCR1='',
               NOMCR2='',
    	         NOMCR3='',
               STAPRO = @STAOLD,           
    	         STACRT = 101,
    	         UPDUSU = @UPDUSU,
    	         DATUPD = GETDATE(),
               ASSUSU = 0
    	   WHERE CODCRT = @CODCRT
            IF(@@ERROR=0)
                BEGIN
                    DECLARE @AUDSRC VARCHAR(100) = N'STATUS ANTERIOR DO CARTAO : ' + CONVERT(VARCHAR, @STAOLD)
                    DECLARE @AUDCHG VARCHAR(100) = N'ALTEROU O STATUS DO CARTAO PARA 101 - EM ESTOQUE' 
                    INSERT INTO TBAUDDAT ( UPDUSU,    AUDDAT, AUDKEY, AUDIDR,   AUDIMS,                AUDOBJ, AUDSRC, AUDCHG) 
                                  VALUES (@UPDUSU, GETDATE(),     14, @CODCRT,     104, OBJECT_NAME(@@PROCID),@AUDSRC, @AUDCHG)        
                END
      END        
    RETURN @RETURN_VALUE

GO

