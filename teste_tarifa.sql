
EXEC PRCADTARCALV2 3,4,1,1000,1,1,NULL,NULL,0,0,0,0,0
DECLARE @NUMERO INT
EXEC @NUMERO = PRCADTARCALV2 3,4,1,1000,1
PRINT @NUMERO 
EXEC @NUMERO = PRCADTARCALV2 3,4,22,1000,1
PRINT @NUMERO

SELECT * FROM TBCALTAR
SELECT * FROM TBCADTAR
DECLARE @NIDTAR INT=0
EXEC PRCADTARFNDTAR 3, 4, 1, 0, 0, 0, @NIDTAR OUTPUT
PRINT 'ID DA TARIFA ' + CONVERT(VARCHAR, @NIDTAR)

SELECT * FROM TBCALTAR WHERE NIDCAL IN (6,7)




GO



alter PROCEDURE dbo.PRCADTARCALV2
(
    @USUPRO Integer,
    @CODUSU Integer,
    @CODTAR smallint,
    @VLRBAS decimal,
	@UPDUSU INT, 
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

    --SELECT A.NIDTAR,A.NIVCFG,A.USUCFG,A.codtar
    --		  ,a.starec,A.codmoe,a.datcad,a.updusu
    --		  ,A.fmtcob,A.pcttar,A.vlrtar,A.tarbas
    --		  ,A.tarmax,A.rsptar,A.VLRINF,A.VLRMAX
    --		  ,A.codbnd,A.tiplin,A.MODCRT,PARINI  = isnull(b.parini,0)
    --      ,PARFIN  = isnull(b.parfin,0)
    --      ,EXTVBS = 0
    --      ,EXTVLR = @EXTVLR
    --      ,NIDCAL = ISNULL(c.NIDCAL,0)
    --  FROM TBCADTAR (NOLOCK) a
    --  LEFT JOIN TBMODTAR (NOLOCK) b on (a.modcrt = b.modcrt)
    --  LEFT JOIN tbcaltar (NOLOCK) c on (c.nidcal = @EXPTAR)
    -- WHERE A.NIDTAR = @NIDTAR
GO

ALTER PROCEDURE dbo.PRCADTARGETEXPTAR
(
    @NIDTAR Integer,
    @VLRTRA float = 0,
	@UPDUSU INT
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
    DECLARE @RETURN_VALUE INT =-1
                
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
    
    INSERT INTO TBCALTAR (USUCFG, NIVCFG,  CODTAR, CODEXP, LVLAPL, VLRBAS, VLRTAR,UPDUSU)
          VALUES         (@CODUSU,@NIVTAR, @CODTAR,@CODEXP,@LVLAPL,@VLRTRA,@VLRTAR,@UPDUSU)
        IF(@@ERROR=0)
    	     SET @RETURN_VALUE = @@IDENTITY
    RETURN @RETURN_VALUE
GO



/* ===================================================================================================
   Author : Agostin
     Date : 13/03/2022 14:31:33
 Objetivo : Localiza um registro de tarifação com base nos parâmetros fornecidos
 ==================================================================================================== */
ALTER PROCEDURE dbo.PRCADTARFNDTAR
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
     		IF(@RETURN_VALUE=0)
     			BEGIN
				    /* PADRAO*/
					SELECT @GSTPAD = ISNULL(GSTPAD,0) FROM TBSYSCFG (NOLOCK);
     				IF(@GSTPAD>0)
     				   BEGIN
   							EXEC @RETURN_VALUE = PRCADTARFND @GSTPAD, 1, @CODTAR, @CODBND, @TIPLIN, @MODCRT   				   	   
     				   END
     			END
      END          
    RETURN @RETURN_VALUE
GO


ALTER PROCEDURE dbo.PRCADTARFND
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
   RETURN @RETURN_VALUE

GO
