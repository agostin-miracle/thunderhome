IF OBJECT_ID ( 'dbo.PRCADTARINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADTARINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 30/03/2022 13:56:18
 Objetivo : Inserção de Registros na Tabela TBCADTAR
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADTARINS
        (        @NIVCFG tinyint = 2, 
        @USUCFG int, 
        @CODTAR tinyint, 
        @CODBND smallint = 0, 
        @TIPLIN tinyint = 0, 
        @MODCRT smallint = 0, 
        @CODEXP smallint = 0, 
        @CODMOE smallint = 1, 
        @FMTCOB tinyint = 1, 
        @RSPTAR tinyint = 2, 
        @TARBAS money = 0, 
        @TARMAX money = 0, 
        @PCTTAR money = 0, 
        @VLRTAR money = 0, 
        @VLRINF money = 0, 
        @VLRMAX money = 0, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    IF(EXISTS (SELECT 1 FROM TBCADTAR (NOLOCK) 
                WHERE NIVCFG =@NIVCFG
                  AND USUCFG =@USUCFG
                  AND CODTAR = @CODTAR
                  AND CODBND =@CODBND
                  AND TIPLIN = @TIPLIN
                  AND MODCRT =@MODCRT))
        SET @RETURN_VALUE=-2

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBCADTAR(NIVCFG, USUCFG, CODTAR, CODBND, TIPLIN, MODCRT, CODEXP, CODMOE, FMTCOB, RSPTAR, TARBAS, TARMAX, PCTTAR, VLRTAR, VLRINF, VLRMAX, STAREC, UPDUSU)
                        VALUES (@NIVCFG, @USUCFG, @CODTAR, @CODBND, @TIPLIN, @MODCRT, @CODEXP, @CODMOE, @FMTCOB, @RSPTAR, @TARBAS, @TARMAX, @PCTTAR, @VLRTAR, @VLRINF, @VLRMAX, @STAREC, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PRCADTARSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADTARSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 30/03/2022 13:56:18
 Objetivo : Obtêm o Id de Tarifação Tarifação de Acordo com o ID de Registro de Tarifação fornecido
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADTARSEL
(
    @NIDTAR Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBCADTAR (NOLOCK) A INNER JOIN TBMODTAR (NOLOCK) B ON (A.MODCRT = B.MODCRT)

    WHERE     (A.NIDTAR=@NIDTAR)

GO

IF OBJECT_ID ( 'dbo.PRCADTARGETARBOL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADTARGETARBOL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 30/03/2022 13:56:18
 Objetivo : Efetua o Cálculo de uma Tarifa de Boleto
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADTARGETARBOL
(
    @USUPRO Integer,
    @USUCED Integer,
    @TIPBOL Tinyint,
    @VLRBOL float
)
AS
    SET NOCOUNT ON
    DECLARE @NIDCBL INT = 0;
    DECLARE @CODTAR INT = 0;
    DECLARE @GSTPAD INT = 0;
    /* GESTOR PADRAO */
    SELECT @GSTPAD = ISNULL(GSTPAD,0) FROM TBSYSCFG (NOLOCK)
    /* LOCALIZA A CONFIGURACAO DO BOLETO */
    EXEC @NIDCBL = PRCFGBOLOCREC @USUPRO, @USUCED, @TIPBOL
    IF(@NIDCBL>0)
        BEGIN
           SELECT @CODTAR FROM TBCFGBOL WHERE NIDCBL=@NIDCBL
        END
    EXEC PRCADTARCALV2 @USUPRO, @USUCED, @CODTAR,@VLRBOL,1,1,NULL,NULL,@GSTPAD,0,0,0,0


GO

IF OBJECT_ID ( 'dbo.PRCADTARCALV2', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADTARCALV2;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 30/03/2022 13:56:18
 Objetivo : Efetua o Cálculo de uma tarifa com base nos parâmetros fornecidos
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADTARCALV2
(
    @USUPRO Integer,
    @CODUSU Integer,
    @CODTAR smallint,
    @VLRBAS decimal,
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
            EXEC PRCADTARFNDTAR @USUPRO, @CODUSU, @CODTAR,@FINLIV,@CODBND,@TIPLIN,@MODCRT, @NIDTAR OUTPUT
        ELSE
            SET @NIDTAR = @TARNID
        IF(@NIDTAR>0)
            BEGIN
            	SELECT @FMTCOB = FMTCOB, @VLRTAR = VLRTAR, @PCTTAR = PCTTAR, @TARBAS = TARBAS, @TARMAX = TARMAX
           			FROM TBCADTAR (nolock)
           		 WHERE NIDTAR = @NIDTAR
    
        		  /* VERIFICA SE EXISTE EXPANSAO DA TARIFA E TRAZ A NOVA TARIFA */
        		  EXEC PREXPTARGETTAR @NIDTAR, @VLRBAS, @EXPTAR OUTPUT
    
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
    
        SELECT A.NIDTAR,A.NIVCFG,A.USUCFG,A.codtar
        		  ,a.starec,A.codmoe,a.datcad,a.updusu
        		  ,A.fmtcob,A.pcttar,A.vlrtar,A.tarbas
        		  ,A.tarmax,A.rsptar,A.VLRINF,A.VLRMAX
        		  ,A.codbnd,A.tiplin,A.MODCRT,PARINI  = isnull(b.parini,0)
              ,PARFIN  = isnull(b.parfin,0)
              ,EXTVBS = 0
              ,EXTVLR = @EXTVLR
              ,NIDCAL = ISNULL(c.NIDCAL,0)
          FROM TBCADTAR (nolock) a
          LEFT JOIN TBMODTAR (nolock) b on (a.modcrt = b.modcrt)
          LEFT JOIN tbcaltar (nolock) c on (c.nidcal = @EXPTAR)
         WHERE A.NIDTAR = @NIDTAR


GO

IF OBJECT_ID ( 'dbo.PRCADTARUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADTARUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 30/03/2022 13:56:18
 Objetivo : Altera um registro da tabela TBCADTAR (Tariffs)  de acordo com a chave identity
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADTARUPD
        (@NIDTAR int, 
        @NIVCFG tinyint = 2, 
        @USUCFG int, 
        @CODTAR tinyint, 
        @CODBND smallint = 0, 
        @TIPLIN tinyint = 0, 
        @MODCRT smallint = 0, 
        @CODEXP smallint = 0, 
        @CODMOE smallint = 1, 
        @FMTCOB tinyint = 1, 
        @RSPTAR tinyint = 2, 
        @TARBAS money = 0, 
        @TARMAX money = 0, 
        @PCTTAR money = 0, 
        @VLRTAR money = 0, 
        @VLRINF money = 0, 
        @VLRMAX money = 0, 
        @STAREC tinyint = 1, 
        @DATUPD date, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD=GETDATE()

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBCADTAR
               SET CODEXP=@CODEXP
                  ,CODMOE=@CODMOE
                  ,FMTCOB=@FMTCOB
                  ,RSPTAR=@RSPTAR
                  ,TARBAS=@TARBAS
                  ,TARMAX=@TARMAX
                  ,PCTTAR=@PCTTAR
                  ,VLRTAR=@VLRTAR
                  ,VLRINF=@VLRINF
                  ,VLRMAX=@VLRMAX
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (NIDTAR=@NIDTAR)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @NIDTAR
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRCADTARSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADTARSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 30/03/2022 13:56:18
 Objetivo : Seleciona todos os registros com base nos parâmetros fornecidos
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADTARSELALL
(
    @USUCFG Integer,
    @NIVCFG Tinyint
)
AS
    SET NOCOUNT ON
    SELECT * FROM VWCADTAR A
     WHERE       (A.USUCFG=@USUCFG)
       AND (A.NIVCFG=@NIVCFG)
     ORDER BY A.CODTAR, A.CODBND, A.TIPLIN, A.MODCRT, A.CODEXP



GO

IF OBJECT_ID ( 'dbo.PRCADTARFND', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADTARFND;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 30/03/2022 13:56:18
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
    SELECT ISNULL(NIDTAR ,0)
      FROM TBCADTAR (NOLOCK) 
     WHERE USUCFG =@USUCFG 
       AND CODTAR =@CODTAR 
       AND NIVCFG =@NIVCFG        
       AND CODBND =@CODBND
       AND TIPLIN =@TIPLIN
       AND MODCRT =@MODCRT
    SELECT COALESCE(@RETURN_VALUE,0) AS RETURN_VALUE
    RETURN @RETURN_VALUE;

GO

IF OBJECT_ID ( 'dbo.PRCADTARGETEXPTAR', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADTARGETEXPTAR;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 30/03/2022 13:56:18
 Objetivo : Obtêm a tarifa expandida de registro de tarifação
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADTARGETEXPTAR
(
    @NIDTAR Integer,
    @VLRTRA float = 0
    ,@RETURN_VALUE INT OUTPUT
)
AS
    SET NOCOUNT ON

    DECLARE @NUMANO INT = DATEPART(year,GETDATE())
        DECLARE @NUMMES INT = DATEPART(month,GETDATE())
        DECLARE @QTDREG MONEY =0
        DECLARE @CODTAR SMALLINT    
        DECLARE @CODUSU INT =0
        DECLARE @NIVTAR TINYINT=0
        DECLARE @LVLREG TINYINT=0    
        DECLARE @CODEXP TINYINT=0    
        DECLARE @CODMOV SMALLINT = 0
        DECLARE @LVLAPL TINYINT = 0
        DECLARE @VLRTAR MONEY = 0
        SET @RETURN_VALUE=-1
                    
        SELECT @CODUSU = USUCFG,
               @NIVTAR = NIVCFG,
       		   @CODEXP = CODEXP,
       		   @CODTAR = CODTAR  
          FROM TBCADTAR (NOLOCK)  
         WHERE NIDTAR = @NIDTAR
               
        /*
         * Tipo de Expansao [1,2] - Diversos, [3,4] - Boletos, [5] - Base direta na Faixa de Valor da tabela de tarifas
         */
        IF (@CODEXP >0 )
            SELECT @LVLREG = ISNULL(LVLREG,0) FROM TBTIPEXP (NOLOCK) WHERE TIPEXP = @CODEXP AND STAREC=1
               
         /* BOLETOS */
            
        IF(@LVLREG IN (3,4) AND (@CODUSU>0 AND @NIVTAR IN (1,2)) ) 
           	BEGIN
           		SELECT @QTDREG = ISNULL(SUM(CASE WHEN @LVLREG=3 THEN ISNULL(QTDBOL,0) ELSE ISNULL(SUMBOL,0) END),0)
           		  FROM VWSUMBOL
           		 WHERE CODUSU = @CODUSU
           		   AND NIVSUM = @NIVTAR
           		   AND NUMANO = @NUMANO
           		   AND NUMMES = @NUMMES
                      
           		IF(@QTDREG>0)
     			    BEGIN 
       				  	IF(@LVLREG=3)
     					  	SET @QTDREG=@QTDREG+1
    			        IF(@LVLREG=4)
    		 		        SET @QTDREG= @QTDREG + @VLRTRA
    				    SELECT @VLRTAR = VLRTAR, @LVLAPL=LVLAPL 
    			   	      FROM TBEXPTAR
                       	 WHERE STAREC=1 AND TIPEXP = @CODEXP AND @QTDREG BETWEEN VLRMIN AND VLRMAX
                    END
        	END
        
            
          /* Outras tarifas */
        	/*
        	   1 - por quantidade
        	   2 - por valor
        	*/
                
            IF(@LVLREG IN (1,2) AND (@CODUSU > 0)) 
               BEGIN
          	        SET @QTDREG=0				--NAO EXISTE MOVIMENTACAO
        		        SELECT @QTDREG = ISNULL(SUM(CASE WHEN @LVLREG=1 THEN ISNULL(QTDMOV,0) ELSE ISNULL(VLRMOV,0) END),0)
               			  FROM VWREGCCRTAR
        			       WHERE CODUSU = @CODUSU
                       AND NUMANO = @NUMANO
                       AND NUMMES = @NUMMES
                       AND CODMOV IN ( SELECT CODMOV 
                                         FROM TBTARMOV (NOLOCK) 
                                        WHERE CODTAR = @CODTAR)
        
                    IF(@QTDREG>=0)
        						    BEGIN 
        						      IF(@LVLREG=1)
        								      SET @QTDREG= @QTDREG + 1
        						      IF(@LVLREG=2)
        								      SET @QTDREG= @QTDREG + @VLRTRA
        							    SELECT @VLRTAR = VLRTAR, @LVLAPL=LVLAPL  
         						        FROM TBEXPTAR (NOLOCK)
                           WHERE STAREC=1 AND TIPEXP = @CODEXP AND @QTDREG BETWEEN VLRMIN AND VLRMAX
        						    END
        				END
        
            
        /* O Valor da tarifa é calculada com base na faixa de valor informado */
        IF(@LVLREG=5 AND @VLRTRA<>0) 
            BEGIN
           			SELECT @VLRTAR = VLRTAR, @LVLAPL=LVLAPL  FROM TBEXPTAR (NOLOCK) WHERE STAREC=1 AND TIPEXP = @CODEXP AND @VLRTRA BETWEEN VLRMIN AND VLRMAX
            END      
        
        INSERT INTO TBCALTAR (USUCFG, NIVCFG,  CODTAR, CODEXP, LVLAPL, VLRBAS, VLRTAR)
              VALUES         (@CODUSU,@NIVTAR, @CODTAR,@CODEXP,@LVLAPL,@VLRTRA,@VLRTAR)
            IF(@@ERROR=0)
        	     SET @RETURN_VALUE = @@IDENTITY
        RETURN @RETURN_VALUE

GO

