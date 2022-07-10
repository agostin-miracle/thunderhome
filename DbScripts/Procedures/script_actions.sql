IF OBJECT_ID ( 'dbo.PRCADGERVALENTRCMF', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADGERVALENTRCMF;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
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

 AND     (@CODATR IS NULL OR A.CODATR=@CODATR)
     AND (@CODUSU IS NULL OR A.CODUSU=@CODUSU)
     AND (@CODCMF IS NULL OR A.CODCMF=@CODCMF)
     AND (@SRCUSU IS NULL OR A.SRCUSU=@SRCUSU)
    

GO

IF OBJECT_ID ( 'dbo.PRCADUSUPSQCMF', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADUSUPSQCMF;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
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
    	 AND CODATR = @CODATR
    RETURN @RETURN_VALUE;

 AND     (@CODATR IS NULL OR A.CODATR=@CODATR)
     AND (@CODCMF IS NULL OR A.CODCMF=@CODCMF)
    

GO

IF OBJECT_ID ( 'dbo.PRCADTARFNDTAR', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADTARFNDTAR;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Localiza um registro de tarifação com base no gestor ou usuário cedente, caso não encontra localiza com base no gestor padrão
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADTARFNDTAR
(
    @USUCFG Integer,
    @CODUSU Integer,
    @CODTAR smallint,
    @CODBND smallint = 0,
    @TIPLIN Tinyint = 0,
    @MODCRT Tinyint = 0
)
AS
    SET NOCOUNT ON
    DECLARE @GSTPAD INT=0
    DECLARE @RETURN_VALUE INT = 0
    /* USUARIO */
    EXEC @RETURN_VALUE = PRCADTARFND @CODUSU, 2, @CODTAR, @CODBND, @TIPLIN, @MODCRT
    IF(@RETURN_VALUE=0)
    		BEGIN
    		    /* GESTOR */
         		EXEC @RETURN_VALUE = PRCADTARFND @USUCFG, 1, @CODTAR, @CODBND, @TIPLIN, @MODCRT
        END
    IF(@RETURN_VALUE=0)
    		BEGIN
    		    /* PADRAO*/
    			 SELECT @GSTPAD = ISNULL(GSTPAD,0) FROM TBSYSCFG (NOLOCK);
         	IF(@GSTPAD>0)
         	   BEGIN
       					EXEC @RETURN_VALUE = PRCADTARFND @GSTPAD, 1, @CODTAR, @CODBND, @TIPLIN, @MODCRT
         	   END
        END
    RETURN @RETURN_VALUE;

    WHERE     (@USUCFG IS NULL OR A.USUCFG=@USUCFG)
     AND (@CODUSU IS NULL OR A.CODUSU=@CODUSU)
     AND (@CODTAR IS NULL OR A.CODTAR=@CODTAR)
     AND (@CODBND IS NULL OR A.CODBND=@CODBND)
     AND (@TIPLIN IS NULL OR A.TIPLIN=@TIPLIN)
     AND (@MODCRT IS NULL OR A.MODCRT=@MODCRT)
    

GO

IF OBJECT_ID ( 'dbo.PRCADTARFND', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADTARFND;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Procura por um registro de tarifação de acordo com os parâmetros fornecidos
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADTARFND
(
    @USUCFG Integer,
    @NIVCFG Tinyint,
    @CODTAR smallint,
    @CODBND smallint = 0,
    @TIPLIN Tinyint = 0,
    @MODCRT Tinyint = 0
)
AS
    SET NOCOUNT ON
    DECLARE @RETURN_VALUE INT=0;
    SELECT @RETURN_VALUE = ISNULL(NIDTAR ,0)
      FROM TBCADTAR (NOLOCK)
     WHERE USUCFG =@USUCFG
       AND CODTAR =@CODTAR
       AND NIVCFG =@NIVCFG
       AND CODBND =@CODBND
       AND TIPLIN =@TIPLIN
       AND MODCRT =@MODCRT
    IF(@RETURN_VALUE IS NULL)
       SET @RETURN_VALUE=0;
       RETURN @RETURN_VALUE

 AND     (@USUCFG IS NULL OR A.USUCFG=@USUCFG)
     AND (@NIVCFG IS NULL OR A.NIVCFG=@NIVCFG)
     AND (@CODTAR IS NULL OR A.CODTAR=@CODTAR)
     AND (@CODBND IS NULL OR A.CODBND=@CODBND)
     AND (@TIPLIN IS NULL OR A.TIPLIN=@TIPLIN)
     AND (@MODCRT IS NULL OR A.MODCRT=@MODCRT)
    

GO

IF OBJECT_ID ( 'dbo.PRCADTARCALV2', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADTARCALV2;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Efetua o Cálculo de uma tarifa com base nos parâmetros fornecidos
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADTARCALV2
(
    @USUPRO Integer,
    @CODUSU Integer,
    @CODTAR smallint,
    @VLRBAS decimal,
    @UPDUSU Integer,
    @NUMPCL Tinyint = 1,
    @CURPCL Tinyint = 1,
    @DATVL1 DateTime=NULL,
    @DATVL2 DateTime=NULL,
    @FINLIV Bit = false,
    @CODBND smallint = 0,
    @TIPLIN Tinyint = 0,
    @MODCRT Tinyint = 0,
    @TARNID Integer = 0
)
AS
    SET NOCOUNT ON
    DECLARE @VLRTAR FLOAT
        DECLARE @PCTTAR FLOAT
        DECLARE @FMTCOB TINYINT
        DECLARE @TARMAX FLOAT
        DECLARE @TARBAS FLOAT
    
        /* VARIAVEIS INTERNAS */
        DECLARE @EXPTAR FLOAT = 0
        DECLARE @NIDTAR INT =0
        DECLARE @EXTVLR FLOAT = 0
        DECLARE @NUMDYS INT =0
    
        IF(@TARNID=0)
            EXEC @NIDTAR = PRCADTARFNDTAR @USUPRO, @CODUSU, @CODTAR,@CODBND,@TIPLIN,@MODCRT
        ELSE
            SET @NIDTAR = @TARNID
        IF(@NIDTAR>0)
            BEGIN
            	SELECT @FMTCOB = FMTCOB, @VLRTAR = VLRTAR, @PCTTAR = PCTTAR, @TARBAS = TARBAS, @TARMAX = TARMAX
           			FROM TBCADTAR (nolock)
           		 WHERE NIDTAR = @NIDTAR
    
        		  /* VERIFICA SE EXISTE EXPANSAO DA TARIFA E TRAZ A NOVA TARIFA */
        		  EXEC @EXPTAR = PRCADTARGETEXPTAR @NIDTAR, @VLRBAS, @UPDUSU
    
        		  IF(@EXPTAR>0)
        			  BEGIN
        				  SELECT @VLRTAR = VLRTAR FROM TBCALTAR WHERE NIDCAL=@EXPTAR AND VLRTAR<>0
        			  END
    
           		/* TARIFA POR VALOR */
        		  IF( @FMTCOB = 1 )
        			  BEGIN
        				  SET @EXTVLR = @VLRTAR
        			  END
    
        		  /* TARIFA POR PERCENTUAL */
        		  IF( @FMTCOB = 2 )
        			  BEGIN
        				  SET @EXTVLR = Round(@VLRBAS * ( @VLRTAR / 100 ), 2)
        			END
    
        		  /* ANTECIPACAO */
        		  IF ( @FMTCOB = 3 )
        			  BEGIN
        				  SET @EXTVLR = Round(@VLRBAS * ( @VLRTAR / 100 ), 2)
        			  END
    
        		  /* PARCELAMENTO */
        		  IF ( @FMTCOB = 4 )
        			  BEGIN
        				  SET @NUMDYS = Datediff(dd, @DATVL1, @DATVL2);
        				  SET @EXTVLR = Round(@VLRBAS * ( ( @VLRTAR / 100 ) / 30 ) * @NUMDYS, 2);
        			  END
    
        		  /* RAV */
        		  IF ( @FMTCOB = 5 )
        			  BEGIN
        				  SET @EXTVLR = Round(@VLRBAS * ( ( @VLRTAR * @CURPCL ) / 100 ), 2);
        			  END
    
        		  /* % + VALOR */
        		  IF ( @FMTCOB = 6 )
        			  BEGIN
        				  SET @EXTVLR = Round(@VLRBAS * ( @PCTTAR / 100 ), 2);
        				  SET @EXTVLR = @EXTVLR + @TARBAS;
        			  END
    
    
        			IF(@FMTCOB IN (2, 3))
        			  BEGIN
        				  IF ( @TARMAX > 0 )
        					  BEGIN
        						  IF ( @EXTVLR > @TARMAX )
        							  BEGIN
        								  SET @EXTVLR = @TARMAX;
        							  END
        					  END
    
        				  IF ( @TARBAS > 0 )
        					  BEGIN
        						  IF ( @EXTVLR < @TARBAS )
        							  BEGIN
        								  SET @EXTVLR = @TARBAS;
        							  END
         					  END
        			END
        	END
    
    
        	IF(@EXTVLR<>0)
        		BEGIN
        			UPDATE TBCALTAR
        				 SET EXTVLR = @EXTVLR, NIDTAR = @NIDTAR, NUMDYS = @NUMDYS, DATVL1 = @DATVL1, DATVL2 = @DATVL2
           		 WHERE NIDCAL = @EXPTAR
        		END
        RETURN @EXPTAR

    

GO

IF OBJECT_ID ( 'dbo.PRREGMENCRTMENCRT', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGMENCRTMENCRT;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Verifica se o cartão já foi alocado para um portador
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGMENCRTMENCRT
(
    @CODCRT Integer,
    @UPDUSU Integer
)
AS
    SET NOCOUNT ON
    DECLARE @USUPRO INT
    DECLARE @ASSUSU INT
    DECLARE @USUMEN INT
    DECLARE @DATATV DATETIME =NULL
    DECLARE @DATVCT DATETIME =NULL
    DECLARE @VALCRT VARCHAR(10)
    DECLARE @APLMEN BIT = 0
    DECLARE @VLRMEN MONEY =0
    DECLARE @CODUSU INT = 0;
    DECLARE @USRREF INT = 0;
    DECLARE @APLM1 BIT = 0
    DECLARE @VLRM1 MONEY =0
    DECLARE @APLM2 BIT = 0
    DECLARE @VLRM2 MONEY =0
    DECLARE @RETURN_VALUE INT = 0  /* NUMERO DE REGISTROS ADICIONADOS */
    
    
       	SELECT @USUPRO = USUPRO
    	      ,@CODUSU = CASE WHEN (USUMEN=0) THEN ASSUSU ELSE USUMEN END
       		  ,@USRREF = ASSUSU
    		  ,@VALCRT = VALCRT
    		  ,@DATVCT = dbo.ValidadeCartao(@VALCRT)
    		  ,@DATATV = DATATV
       	  FROM TBREGCRT (NOLOCK)
         WHERE CODCRT = @CODCRT AND STACRT=109 AND CALMEN=1
    
        IF(@@ROWCOUNT =0 )
    		SET @RETURN_VALUE=-2;
    
    	IF(@DATATV IS NOT NULL AND @RETURN_VALUE =0)
    		BEGIN
    
    			/* Valor da Mensalidade Gestor */
       			SELECT @APLM1 = ISNULL(APLMEN,CAST(0 AS BIT)), @VLRM1 =ISNULL(VLRMEN,0) FROM TBUSUPRO (NOLOCK) WHERE USUPRO=@USUPRO
    			/* Valor da Mensalidade Usuário */
       			SELECT @APLM2 = ISNULL(APLMEN,CAST(0 AS BIT)), @VLRM2 =ISNULL(VLRMEN,0) FROM TBUSUCFG (NOLOCK) WHERE USUCFG=@USRREF AND NIVCFG=2
    
    
    			SET @APLMEN = @APLM2;
       			SET @VLRMEN = @VLRM2
       		    IF(@APLMEN=0)
       				  BEGIN
       					  SET @APLMEN = @APLM1;
       					  SET @VLRMEN = @VLRM1
       				  END
       			IF(@APLMEN=1 AND @VLRMEN>0)
       				BEGIN
    				    DECLARE @LOTINS INT = 0
    					EXEC @LOTINS = PRGETNXTNUM 13
       					INSERT INTO
    					  TBREGMEN (
    					            STAMEN,
    								NUMMES,
    								NUMANO,
    								CODUSU,
    								USRREF,
    								DATMEN,
    								DATVCT,
    								TIPMEN,
    								NUMPCL,
    								SIGOPE,
    								USUPRO,
    								VLRCOB,
    								CODREF,
    								LOTINS
       					)
       					SELECT 261
       						  ,MONTH(VALDAT)
       						  ,YEAR(VALDAT)
       						  ,@CODUSU
       						  ,@USRREF
       						  ,VALDAT
       						  ,VALDAT
       						  ,1
       						  ,id
    						  ,1
       						  ,@USUPRO
       						  ,@VLRMEN
      						  ,@CODCRT
    						  ,@LOTINS
       	 				  FROM dbo.ExpandPeriod(@DATATV, @DATVCT)
       					  WHERE  '01' + RIGHT('00' + CAST(MONTH(VALDAT) AS VARCHAR),2)+
       							 RIGHT('0000' + CAST(YEAR(VALDAT) AS VARCHAR),4) +
    							 RIGHT('00000000' + CAST(@CODCRT AS VARCHAR),2) + '1'  NOT IN (SELECT
    							 RIGHT('00' + CAST(TIPMEN AS VARCHAR),2)+
    							 RIGHT('00' + CAST(NUMMES AS VARCHAR),2)+
    							 RIGHT('0000' + CAST(NUMANO AS VARCHAR),4) +
    							 RIGHT('00000000' + CAST(CODREF AS VARCHAR),2)+
       							 CAST(MODREG AS VARCHAR(1))  FROM TBREGMEN)
    					  SET @RETURN_VALUE = @@ROWCOUNT
    
       					  IF(@RETURN_VALUE>0)
       						BEGIN
       							UPDATE TBREGCRT SET CALMEN= 2, DATUPD=GETDATE(), UPDUSU=@UPDUSU WHERE CODCRT=@CODCRT
    						    DECLARE @DSCOCO VARCHAR(100)
    							SET @DSCOCO = N'[' + CONVERT(VARCHAR,@RETURN_VALUE) + '] MENSALIDADES DE ' + FORMAT(@DATATV, 'dd/MM/yyyy') + ' ATE ' + FORMAT(@DATVCT, 'dd/MM/yyyy')
    							INSERT INTO TBAUDDAT ( UPDUSU,    AUDDAT, AUDKEY, AUDIDR,   AUDIMS,                AUDOBJ, AUDSRC,  AUDCHG)
                                              VALUES (@UPDUSU, GETDATE(),     14, @CODCRT,       0, OBJECT_NAME(@@PROCID),     '', @DSCOCO);
    
       						END
       				END
    			ELSE
    			    SET @RETURN_VALUE=-3;
    		END
    	ELSE
    	    BEGIN
    				IF(@RETURN_VALUE=0)
    					SET @RETURN_VALUE=-1;
    		END
    RETURN @RETURN_VALUE

    

GO

IF OBJECT_ID ( 'dbo.PRSYSGETUSRACE', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRSYSGETUSRACE;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Verificar se existe o acesso de um usuário para uma rotina específica
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRSYSGETUSRACE
(
    @SYSTBL smallint,
    @SYSROL smallint,
    @CODUSU Integer
)
AS
    SET NOCOUNT ON
    DECLARE @RETURN_VALUE INT
    SELECT @RETURN_VALUE = COUNT(*) FROM TBSYSUXG (NOLOCK) A
    INNER JOIN TBSYSGRP (NOLOCK) B ON (B.SYSGRP =  A.SYSGRP)
    INNER JOIN TBSYSFXG (NOLOCK) C ON (C.SYSGRP =  B.SYSGRP)
    INNER JOIN TBSYSFUN (NOLOCK) D ON (D.SYSFUN = C.SYSFUN)
    WHERE A.STAREC=1 AND B.STAREC IN(1,2) AND C.STAREC=1 AND D.STAREC=1
    AND A.CODUSU=@CODUSU
    AND D.SYSTBL=@SYSTBL
    AND D.SYSROL=@SYSROL
    IF(@RETURN_VALUE IS NULL)
       SET @RETURN_VALUE = 0
    RETURN @RETURN_VALUE

    

GO

IF OBJECT_ID ( 'dbo.PRGETNXTNUM', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRGETNXTNUM;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm o proximo numero de uma transacao especificada pelo codigo de tabelamento interno
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRGETNXTNUM
(
    @CODTBI Integer
)
AS
    SET NOCOUNT ON
    DECLARE @RETURN_VALUE INT =0
    IF( NOT EXISTS(SELECT 1 FROM TBNXTNUM WHERE CODTBI = @CODTBI))
    		BEGIN
    			INSERT INTO TBNXTNUM (CODTBI, NUMNXT) VALUES(@CODTBI, 1)
    			SET @RETURN_VALUE=1;
    		END
    ELSE
    	    SELECT @RETURN_VALUE = ISNULL(NUMNXT,0) FROM TBNXTNUM (NOLOCK)  WHERE CODTBI = @CODTBI
    
    	UPDATE TBNXTNUM WITH (UPDLOCK) SET NUMNXT = (@RETURN_VALUE+1) WHERE CODTBI = @CODTBI
    	RETURN @RETURN_VALUE;

    

GO

IF OBJECT_ID ( 'dbo.PRREGCCRSLDACT', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGCCRSLDACT;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm o saldo da conta conforme o indicador informado
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGCCRSLDACT
(
    @INDLCT smallint,
    @NIDCTA Integer
)
AS
    SET NOCOUNT ON
    DECLARE @RETURN_VALUE MONEY=0
    SELECT @RETURN_VALUE = COALESCE(SUM((VLRMOV*SIGOPE)),0)
      FROM TBREGCCR A (NOLOCK)
     WHERE (A.STAREC IN (1,2))
       AND (A.TIPVAL = 1)
       AND (A.REGHAB = 1)
       AND (A.INDLCT = @INDLCT)
       AND (A.NIDCTA = @NIDCTA)
     GROUP BY A.NIDCTA
    RETURN ISNULL(@RETURN_VALUE,0);

    

GO

IF OBJECT_ID ( 'dbo.PRGETCTAVRT', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRGETCTAVRT;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm o ID da Conta Virtual
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRGETCTAVRT
(
    @CODUSU Integer,
    @TIPCTA Tinyint = 1,
    @ORGCTA Tinyint = 1,
    @STACTA smallint = 250
)
AS
    SET NOCOUNT ON
    DECLARE @RETURN_VALUE INT = 0
    
        IF(EXISTS (SELECT 1 FROM TBCADCTA (NOLOCK)
                    WHERE CODUSU = @CODUSU
                      AND STAREC = 1
                      AND ORGCTA = @ORGCTA
                      AND TIPCTA = @TIPCTA
                      AND STACTA = @STACTA))
            BEGIN
                SELECT @RETURN_VALUE = NIDCTA  FROM TBCADCTA (NOLOCK)
                 WHERE CODUSU = @CODUSU
                   AND STAREC = 1
                   AND ORGCTA = @ORGCTA
                   AND TIPCTA = @TIPCTA
                   AND STACTA = @STACTA
            END
        ELSE
            BEGIN
              SELECT @RETURN_VALUE = NIDCTA  FROM TBCADCTA (NOLOCK)
               WHERE CODUSU = @CODUSU
                 AND STAREC = 1
                 AND ORGCTA = 1
                 AND TIPCTA = @TIPCTA
                 AND STACTA = @STACTA
            END
        RETURN ISNULL(@RETURN_VALUE,0)

    

GO

IF OBJECT_ID ( 'dbo.PRGETINFCRT', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRGETINFCRT;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Obtêm informações específicas do cartão com status 103, 109
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRGETINFCRT
(
    @CODCRT Integer,
    @GETMOD Tinyint
)
AS
    SET NOCOUNT ON
    DECLARE @RETURN_VALUE INT = 0
    
        IF(@GETMOD=1)
           BEGIN
        			SELECT @RETURN_VALUE = ASSUSU  FROM TBREGCRT (NOLOCK)
        	 		 WHERE CODCRT = @CODCRT
        	           AND STAREC = 1
        	           AND STACRT IN (103,109)
        	  END
    
        IF(@GETMOD=2)
         	  BEGIN
        			SELECT @RETURN_VALUE = NIDCTA FROM TBREGCRT (NOLOCK)
        	 		 WHERE CODCRT = @CODCRT
        	           AND STAREC = 1
        	           AND STACRT IN (103,109)
        	  END
    
        IF(@GETMOD=3)
         	  BEGIN
        			SELECT @RETURN_VALUE = USUPRO FROM TBREGCRT (NOLOCK)
        	 		 WHERE CODCRT = @CODCRT
        	           AND STAREC = 1
        	           AND STACRT IN (103,109)
        	  END
        RETURN ISNULL(@RETURN_VALUE,0)

    

GO

IF OBJECT_ID ( 'dbo.PRREGCCRMAKTRF', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGCCRMAKTRF;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:15
 Objetivo : Executa a gravação do Movimento de Transferencia em conta corrente  e retorna o número do lote gerado
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGCCRMAKTRF
(
    @NIDTRF Integer,
    @UPDUSU Integer
)
AS
    SET NOCOUNT ON
    DECLARE @RETURN_VALUE INT = 0
    DECLARE @LOTFIN INT = 0
    DECLARE @NXTNUM INT=0
    DECLARE @STAREC TINYINT = 0
    DECLARE @CODTRM TINYINT = 0
    DECLARE @CODTAR SMALLINT
    DECLARE @TIPLCT SMALLINT
    DECLARE @INDDEB SMALLINT
    DECLARE @NIDCAL INT
    DECLARE @USUPRO INT
    DECLARE @CODUSU INT
    DECLARE @CTADEB INT
    DECLARE @VLRTAR MONEY=0
    DECLARE @VLRMOV MONEY=0
    DECLARE @OMTSLD CHAR(1) = 'S'
    DECLARE @OMTTAR CHAR(1) = 'S'
    DECLARE @NIDTRA VARCHAR(13)= N''
    
    IF(NOT EXISTS(SELECT 1 FROM TBREGMOV (NOLOCK) WHERE NIDTRF=@NIDTRF AND STAREC=1))
        SET @RETURN_VALUE=-1
    
    IF(@RETURN_VALUE=0)
       BEGIN
          SELECT @OMTTAR = OMTTAR
                ,@OMTSLD = OMTSLD
                ,@STAREC = STAREC
                ,@VLRTAR = VLRTAR
                ,@VLRMOV = VLRMOV
                ,@TIPLCT = TIPLCT
                ,@USUPRO = USUPRO
                ,@INDDEB = INDDEB
                ,@CODUSU = CODUSU
                ,@CTADEB = CTADEB
                ,@CODTRM = CODTRM
            FROM TBREGMOV (NOLOCK)
           WHERE NIDTRF=@NIDTRF
       END
    
    IF(@RETURN_VALUE=0 AND @STAREC=1 AND @OMTTAR='N' AND @CODTRM=1)
       BEGIN
          SELECT @CODTAR = ISNULL(CODTAR,0) FROM TBTIPLCT (NOLOCK) WHERE TIPLCT =@TIPLCT AND STAREC=1
          IF(@CODTAR > 0)
               BEGIN
                  EXEC @NIDCAL = PRCADTARCALV2 @USUPRO, @CODUSU, @CODTAR,@VLRMOV, @UPDUSU,1,1, NULL,NULL,1,0,0,0,0
                  IF(@NIDCAL>0)
                      BEGIN
                         SELECT @VLRTAR = EXTVLR FROM TBCALTAR (NOLOCK) WHERE NIDCAL = @NIDCAL
                         UPDATE TBREGMOV SET VLRTAR =@VLRTAR, NIDCAL=@NIDCAL WHERE NIDTRF=@NIDTRF
                      END
               END
       END
    IF(@RETURN_VALUE = 0 AND @STAREC=1 AND @OMTSLD='N' AND @CODTRM=1)
       BEGIN
          DECLARE @SLDDAT DATETIME
          SET @SLDDAT = GETDATE()
          DECLARE @SLDVAL MONEY=0
          EXEC @SLDVAL = PRREGCCRSLDACT @INDDEB, @CTADEB
          IF(@SLDVAL <0)
             BEGIN
                UPDATE TBREGMOV
                   SET DATUPD= GETDATE()
                      ,DSCERR = DSCERR + CHAR(13) +  ' SALDO NEGATIVO EM ' + CONVERT(VARCHAR(25), getdate())
                      ,SLDDAT = @SLDDAT
                      ,SLDVAL = @SLDVAL
                WHERE NIDTRF = @NIDTRF
                SET @RETURN_VALUE=-2
             END
          ELSE
             IF((@SLDVAL - (@VLRMOV + @VLRTAR)) < 0)
                 BEGIN
                    UPDATE TBREGMOV
                       SET DATUPD= GETDATE()
                          ,DSCERR = DSCERR + CHAR(13) +  ' SALDO INSUFICIENTE PARA DÉBITO DE ' + CONVERT(VARCHAR,(@VLRMOV + @VLRTAR))
                          ,SLDDAT = @SLDDAT
                          ,SLDVAL = @SLDVAL
                     WHERE NIDTRF = @NIDTRF
                    SET @RETURN_VALUE=-3
                 END
       END
    
    
    IF(@RETURN_VALUE = 0 AND @NIDTRF >0 AND @STAREC=1)
        BEGIN
           EXEC @NXTNUM = PRGETNXTNUM 6
           SET @NIDTRA = RIGHT('000' + CONVERT(VARCHAR(3),22), 3) + RIGHT('00000000' + CONVERT(VARCHAR(8),@NXTNUM), 8)
           EXEC @LOTFIN = PRGETNXTNUM 12
    
          INSERT INTO TBREGCCR(
    	         CODTRM
              ,SUBSYS
              ,NIDREF
              ,TIPLCT
              ,INDLCT
              ,CODMOE
              ,CODMOV
              ,USUPRO
              ,NIDCTA
              ,REFCTA
              ,NIDTRA
    	        ,CANTRA
              ,DATMOV
              ,CODUSU
              ,REGHAB
              ,LOTFIN
              ,CODAPR
              ,ORGLCT
              ,CODCRT
              ,SIGOPE
              ,TIPLIN
              ,IDEPRE
              ,CNDBLO
              ,NUMPCL
              ,TIPVAL
              ,VLRMOV
              ,DSCMOV
              ,STAREG
              ,STAREC
              ,UPDUSU)
    
         SELECT
            CODTRM
            ,SUBSYS
            ,NIDREF
            ,TIPLCT
            ,INDLCT
            ,CODMOE
            ,CODMOV
            ,USUPRO
            ,NIDCTA
            ,REFCTA
            ,NIDTRA
    	      ,CANTRA
            ,DATMOV
            ,CODUSU
            ,REGHAB
            ,LOTFIN
            ,CODAPR
            ,ORGLCT
            ,CODCRT
            ,SIGOPE
            ,TIPLIN
            ,IDEPRE
            ,CNDBLO
            ,NUMPCL
            ,TIPVAL
            ,VLRMOV
            ,DSCMOV
            ,STAREG
            ,STAREC
            ,UPDUSU
        FROM (SELECT REGLIN=1
                  ,CODTRM = A.CODTRM
                  ,SUBSYS = A.SUBSYS
                  ,NIDREF = A.NIDTRF
                  ,TIPLCT = A.TIPLCT
                  ,INDLCT = B.INDDEB
                  ,CODMOE = A.CODMOE
                  ,CODMOV = B.MOVDEB
                  ,USUPRO = A.USUPRO
                  ,NIDCTA = A.CTADEB
                  ,REFCTA = A.CTACRD
                  ,NIDTRA = @NIDTRA
    	            ,CANTRA = A.CANTRA
                  ,DATMOV = A.DATMOV
                  ,CODUSU = A.CODUSU
                  ,REGHAB = 1
                  ,LOTFIN = @LOTFIN
                  ,CODAPR= 0
                  ,ORGLCT =1
                  ,CODCRT= A.CODCRT
                  ,SIGOPE = -1
                  ,TIPLIN =0
                  ,IDEPRE = B.IDEPRE
                  ,CNDBLO =0
                  ,NUMPCL=1
                  ,TIPVAL=1
                  ,VLRMOV= CASE
    				            WHEN B.LINLCT = 1 THEN A.VLRMOV
    				            WHEN B.LINLCT = 2 THEN A.VLRTAR
    			              ELSE 0
    			              END
                  ,DSCMOV = C.DSCMOV
                  ,STAREG = B.STAREG
                  ,STAREC = 1
                  ,UPDUSU = @UPDUSU
            FROM TBREGMOV (NOLOCK) A
            INNER JOIN TBCADLCT (NOLOCK) B ON (B.TIPLCT = A.TIPLCT AND B.CODTRM = A.CODTRM)
            INNER JOIN TBTIPMOV (NOLOCK) C ON (C.CODMOV = B.MOVDEB)
            WHERE A.NIDTRF=@NIDTRF AND A.STAREC= 1 AND B.INDDEB>0 AND B.MOVDEB>0
            UNION
              SELECT REGLIN=2
                   ,CODTRM = A.CODTRM
                   ,SUBSYS = A.SUBSYS
                  ,NIDREF = A.NIDTRF
                  ,TIPLCT = A.TIPLCT
                  ,INDLCT = B.INDCRD
                  ,CODMOE = A.CODMOE
                  ,CODMOV = B.MOVCRD
                  ,USUPRO = A.USUPRO
                  ,NIDCTA = A.CTACRD
                  ,REFCTA = A.CTADEB
                  ,NIDTRA = @NIDTRA
    	            ,CANTRA = A.CANTRA
                  ,DATMOV = A.DATMOV
                  ,CODUSU = A.CODUSU
                  ,REGHAB = 1
                  ,LOTFIN = @LOTFIN
                  ,CODAPR= 0
                  ,ORGLCT =1
                  ,CODCRT= A.CODCRT
                  ,SIGOPE = 1
                  ,TIPLIN =0
                  ,IDEPRE = B.IDEPRE
                  ,CNDBLO =0
                  ,NUMPCL=1
                  ,TIPVAL=1
                  ,VLRMOV= CASE
    				          WHEN B.LINLCT = 1 THEN A.VLRMOV
    				          WHEN B.LINLCT = 2 THEN A.VLRTAR
    			           ELSE 0
    			           END
                  ,DSCMOV = C.DSCMOV
                  ,STAREG = B.STAREG
                  ,STAREC = 1
                  ,UPDUSU = @UPDUSU
              FROM TBREGMOV (NOLOCK) A
              INNER JOIN TBCADLCT (NOLOCK) B ON (B.TIPLCT = A.TIPLCT AND B.CODTRM = A.CODTRM)
              INNER JOIN TBTIPMOV (NOLOCK) C ON (C.CODMOV = B.MOVCRD)
              WHERE A.NIDTRF=@NIDTRF AND A.STAREC=1 AND B.INDCRD>0 AND B.MOVCRD>0) A
    
              WHERE A.VLRMOV > 0
    
              IF(@@ROWCOUNT>0)
                 BEGIN
                     UPDATE TBREGMOV
                        SET CANTRA=NIDTRA
                           ,NIDTRA = @NIDTRA
                           ,DATUPD = GETDATE()
                           ,UPDUSU = @UPDUSU
                           ,STAREC = 2
                      WHERE NIDTRF=@NIDTRF;
                      SET @RETURN_VALUE= @LOTFIN
                 END
        END
    RETURN @RETURN_VALUE

    

GO

