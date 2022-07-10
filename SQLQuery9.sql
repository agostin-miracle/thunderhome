--DECLARE @DATVCT DATETIME = dbo.ValidadeCartao('08/24')
--SELECT * FROM dbo.ExpandPeriod(GETDATE(), @DATVCT)

--SELECT DATATV,VALCRT, CODCRT, CALMEN  FROM TBREGCRT
-- UPDATE TBREGCRT SET CALMEN=1 WHERE CODCRT=2
-- SELECT * FROM TBREGMEN


/* ===================================================================================================
   Author : Agostin
     Date : 08/03/2022 15:41:57
 Objetivo : Verifica se o cartão já foi alocado para um portador
 ==================================================================================================== */
    DECLARE @CODCRT INTEGER =2
    DECLARE @UPDUSU INTEGER=1



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
PRINT @return_value