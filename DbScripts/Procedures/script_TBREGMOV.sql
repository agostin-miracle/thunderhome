IF OBJECT_ID ( 'dbo.PRREGMOVINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGMOVINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/06/2022 16:18:06
 Objetivo : Inserção de Registros na Tabela TBREGMOV
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGMOVINS
        (        @SUBSYS tinyint, 
        @TIPLCT smallint = 0, 
        @STAMOV smallint = 0, 
        @DATMOV datetime, 
        @CODTRM tinyint = 1, 
        @CODMOE int = 1, 
        @USUPRO int = 0, 
        @CODUSU int = 0, 
        @CODCRT int = 0, 
        @CODRSP char(2) = '00', 
        @USRDEB int = 0, 
        @USRCRD int = 0, 
        @USUDEB smallint, 
        @INDDEB smallint, 
        @ORGDEB tinyint = 0, 
        @CTADEB int, 
        @USUCRD int, 
        @INDCRD smallint, 
        @ORGCRD tinyint = 0, 
        @CTACRD int, 
        @VLRMOV money = 0, 
        @OMTSLD char(1) = 'S', 
        @OMTTAR char(1) = 'S', 
        @NIDCAL int, 
        @VLRTAR money = 0, 
        @DSCERR varchar(150) = '', 
        @SLDDAT datetime = null, 
        @SLDVAL money = 0, 
        @NIDTRA varchar(13) = '', 
        @CANTRA varchar(13) = '', 
        @LOTFIN int = 0, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    DECLARE @ORGCTA TINYINT=0
    DECLARE @GETVRT INT =0
    DECLARE @TIPCTA TINYINT=1		      -- TIPO DE CONTA
    DECLARE @STACTA SMALLINT =250
    DECLARE @CODTAR SMALLINT =0    
    DECLARE @SLDCTA MONEY =0                
    
    /* 
        11 - TRANSFERENCIA PARA OUTROS BANCOS - 
        O CAMPO @USRCRD CORRESPONDE AO ID DA CONTA VIRTUAL EXTERNA
    */
    IF(@ORGDEB=1 AND @ORGCRD=3)
      	BEGIN
       		 IF(@CODTRM=1)
           		BEGIN
    				     SET @USUDEB = @USRDEB
            		 EXEC @CTADEB = PRGETCTAVRT  @USRDEB, @TIPCTA, @ORGCTA, @STACTA
                 SELECT @GETVRT = CODUSU FROM TBCADCTA (NOLOCK) WHERE NIDCTA = @USRCRD AND STAREC=1 AND STACTA=250             
            		 SET @USUCRD = @CODUSU          
             		 SET @CTACRD = @USRCRD;
              END
    			 ELSE
              BEGIN
    				     SET @CTADEB = @USRCRD;
                 SELECT @GETVRT = CODUSU FROM TBCADCTA (NOLOCK) WHERE NIDCTA = @USRCRD AND STAREC=1 AND STACTA=250                          
                 SET @USUDEB = @GETVRT
                 EXEC @CTACRD = PRGETCTAVRT  @USRDEB, @TIPCTA, @ORGCTA, @STACTA
                 SELECT @GETVRT = CODUSU FROM TBCADCTA (NOLOCK) WHERE NIDCTA = @CTACRD AND STAREC=1 AND STACTA=250
                 IF (@GETVRT>0)
                     BEGIN
                        SET @USUDEB = @GETVRT;
                     END
       		    END
        END
                
    /*
        12 - RECARGA DE CARTAO
        21 - TRANSFERENCIA CCV PARA CARTAO
       112 - TIPO LANCAMENTO RECARGA EM LOTE 
         O CAMPO @USRCRD corresponde ao CODIGO DO CARTAO
    */
                
                
    IF(@ORGDEB=1 AND @ORGCRD=2)
       BEGIN
          IF(@CODTRM = 1)
             BEGIN
                SET @USUDEB = @USRDEB
                EXEC @CTADEB = PRGETCTAVRT  @USRDEB, @TIPCTA, @ORGCTA, @STACTA
                EXEC @GETVRT = PRGETINFCRT @USRCRD, 2;
                IF(@GETVRT=0)
    			         EXEC @CTACRD = PRGETCTAVRT @USUCRD, @TIPCTA, @ORGCTA, @STACTA
     			      ELSE
    			         SET @CTACRD = @GETVRT
                EXEC @USUPRO = PRGETINFCRT @USRCRD, 3;
                SET @CODUSU = @USRDEB
             END
          ELSE
             BEGIN
       	        EXEC @GETVRT = PRGETINFCRT @USRDEB, 1;
                SET @USUDEB = @GETVRT;
                EXEC @GETVRT = PRGETINFCRT @USRCRD, 2;
                IF(@GETVRT=0)
                    EXEC @CTADEB = PRGETCTAVRT  @USUDEB, @TIPCTA, @ORGCTA, @STACTA
                ELSE
                    SET @CTADEB = @GETVRT
                EXEC @USUPRO = PRGETINFCRT @USRCRD, 3;
                SET @USUCRD = @USRCRD;
                EXEC @CTACRD = PRGETCTAVRT @USUCRD, @TIPCTA, @ORGCTA, @STACTA
             END
      END
                
                
                /*
                   REVERSAO TRANSFERENCIA PARA OUTROS BANCOS
                    O CAMPO @USRCRD CORRESPONDE AO ID DA CONTA VIRTUAL
                 */
                
                --if (@TIPLCT = 79)
                --    BEGIN
                --		IF (@CODTRM = -1)
                --			BEGIN
                --				SELECT @USUDEB = ISNULL(CODUSU,0) FROM TBCADCTA (NOLOCK) WHERE NIDCTA = @USRCRD AND STAREC=1 AND STACTA=@STACTA;
                --		        SET @CTADEB = @USRCRD;
                --		        SET @USUCRD = @USRDEB;
               	--			    EXEC @CTACRD = PRGETCTAVRT @USUCRD, @TIPCTA, @ORGCTA, @STACTA
                --		        SET @USURET = @USUCRD;
                --		    END
                --    END
                
                
        /*
                	   PAY SMART
                		O Campo pUSRDEB, pUSRCRD corresponde ao ID do Usuário
                   22 - TRANSFERENCIA CCV PARA CCV
                  120 - CREDITO DE REFEICAO
                  121 - CREDITO DE ALIMENTACAO
                  122 - CREDITO DE COMBUSTIVEL
                  123 - CREDITO DE SALARIO
                  124 - CREDITO DE COMBO MULTI
                  125 - CREDITO DE PREMIACAO
                  126 - CREDITO DE AJUDA DE CUSTO
                  127 - CREDITO INCENTIVO
                  128 - CREDITO REMUNERACAO
                  130 - COMBO MOBILIDADE                  (16/03/2021)
                    34 - CREDITO AVULSO
                    44 - LANCAMENTO DE COMPRAS
                    55 - LANCAMENTO DE REFEICAO
                    56 - LANCAMENTO DE ALIMENTACAO
                    57 - LANCAMENTO DE COMBUSTIVEL
                    58 - LANCAMENTO DE CHARGEBACK COMPRAS
                    59 - LANCAMENTO DE CHARGEBACK REFEICAO
                    60 - LANCAMENTO DE CHARGEBACK ALIMENTACAO
                    61 - LANCAMENTO DE CHARGEBACK COMBUSTIVEL
                    63 - LANCAMENTO DE COMBO MULTI
                    64 - LANCAMENTO DE CHARGEBACK COMBO MULTI
                    78 - RECARGA DE ORIGEM TED
                   139 - COMBO MOBILIDADE
                    65 - REGISTRO DE COMBO MOBILIDADE                      (16/03/2021)
                    66 - REVERSAO DE REGISTRO DE COMBO MOBILIDADE          (16/03/2021)
    
                
                */
                
    IF (@ORGDEB=1 AND @ORGCRD=1)
        BEGIN
           if (@CODTRM = 1)
      	        BEGIN
    				      SET @USUDEB=@USRDEB
          		    EXEC @CTADEB = PRGETCTAVRT  @USRDEB, @TIPCTA, @ORGCTA, @STACTA
    				      SET @USUCRD=@USRCRD
          		    EXEC @CTACRD = PRGETCTAVRT  @USRCRD, @TIPCTA, @ORGCTA, @STACTA
      	        END
    			ELSE
              	BEGIN
           		     EXEC @CTADEB = PRGETCTAVRT  @USRCRD, @TIPCTA, @ORGCTA, @STACTA
           		     EXEC @CTACRD = PRGETCTAVRT  @USRDEB, @TIPCTA, @ORGCTA, @STACTA
              	END
          END
                
         /*
            *  TRANSFERENCIA CARTAO PARA CCV
              	14/11/2020
               	   O CAMPO @USRDEB CORRESPONDE AO CODIGO DO CARTAO
               	   O CAMPO @USRCRD CORRESPONDE AO CODIGO DO USUARIO
         */
         
    IF(@ORGDEB=2 AND @ORGCRD=1)     
       BEGIN
          IF (@CODTRM = 1)
              BEGIN
                  EXEC @USUDEB = PRGETINFCRT @USRDEB, 1;
     			        EXEC @GETVRT = PRGETINFCRT @USRDEB, 2;
     				      EXEC @USUPRO = PRGETINFCRT @USRDEB, 3;
     	            IF(@GETVRT=0)
     					       EXEC @CTADEB = PRGETCTAVRT  @USUDEB, @TIPCTA, @ORGCTA, @STACTA
     				      ELSE
     				         SET @CTADEB = @GETVRT
       				       EXEC @CTACRD = PRGETCTAVRT  @USRCRD, @TIPCTA, @ORGCTA, @STACTA
           				   SET @USUCRD = @USRCRD;
              END
    	    ELSE
              BEGIN
      			     EXEC @CTADEB = PRGETCTAVRT  @USRCRD, @TIPCTA, @ORGCTA, @STACTA
    	           SET @USUDEB = @USRCRD
        		     EXEC @USUCRD = PRGETINFCRT @USRDEB, 1;
        		     EXEC @GETVRT = PRGETINFCRT @USRDEB, 2;
        		     EXEC @USUPRO = PRGETINFCRT @USRDEB, 3;
                 IF(@GETVRT=0)
        			       EXEC @CTACRD = PRGETCTAVRT  @USUCRD, @TIPCTA, @ORGCTA, @STACTA
        		     ELSE
        		         SET @CTACRD = @GETVRT
              END
      END                
    
                
                /*
                 * TIPO_LANCAMENTO_TRANSFERENCIA_CARTAO_CARTAO
                 */
    IF(@ORGDEB=2 AND @ORGCRD=2)            
        BEGIN
           if (@CODTRM = 1)
             	 BEGIN
                 	EXEC @USUDEB = PRGETINFCRT @USRDEB, 1;
            		  EXEC @GETVRT = PRGETINFCRT @USRDEB, 2;
            		  EXEC @USUPRO = PRGETINFCRT @USRDEB, 3;
                 	IF(@GETVRT=0)
            		     EXEC @CTADEB = PRGETCTAVRT  @USUDEB, @TIPCTA, @ORGCTA, @STACTA
            		  ELSE
            		     SET @CTADEB = @GETVRT
                 	EXEC @USUCRD = PRGETINFCRT @USRCRD, 1;
            		  EXEC @GETVRT = PRGETINFCRT @USRCRD, 2;
                 	IF(@GETVRT=0)
            		     EXEC @CTACRD = PRGETCTAVRT  @USUCRD, @TIPCTA, @ORGCTA, @STACTA
            		  ELSE
            		     SET @CTACRD = @GETVRT
            	 END
            ELSE
             	 BEGIN
                 	EXEC @USUDEB = PRGETINFCRT @USRCRD, 1;
            		  EXEC @GETVRT = PRGETINFCRT @USRCRD, 2;
            		  EXEC @USUPRO = PRGETINFCRT @USRCRD, 3;
                 	IF(@GETVRT=0)
            		     EXEC @CTADEB = PRGETCTAVRT  @USUDEB, @TIPCTA, @ORGCTA, @STACTA
            		  ELSE
            		     SET @CTADEB = @GETVRT
                 	EXEC @USUCRD = PRGETINFCRT @USRDEB, 1;
            		  EXEC @GETVRT = PRGETINFCRT @USRDEB, 2;
                 	IF(@GETVRT=0)
            		     EXEC @CTACRD = PRGETCTAVRT  @USUCRD, @TIPCTA, @ORGCTA, @STACTA
            		  ELSE
            		     SET @CTACRD = @GETVRT
            	 END
         END
                
                
                /*
                 *  TRANSFERENCIA CARTAO PARA CCV EXTERNA - TIPO_LANCAMENTO_TRANSFERENCIA_CARTAO_CCVEXTERNA
                 	14/11/2020
                 	   O CAMPO @USRDEB CORRESPONDE AO CODIGO DO CARTAO
                 	   O CAMPO @USRCRD CORRESPONDE AO ID DA CONTA VIRTUAL EXTERNA
                 */
    IF(@ORGDEB=2 AND @ORGCRD=3)
      BEGIN
         if (@CODTRM = 1)
            BEGIN
               EXEC @USUDEB = PRGETINFCRT @USRDEB, 1;
             	 EXEC @GETVRT = PRGETINFCRT @USRDEB, 2;
             	 EXEC @USUPRO = PRGETINFCRT @USRDEB, 3;
               IF(@GETVRT=0)
             		  EXEC @CTADEB = PRGETCTAVRT  @USUDEB, @TIPCTA, @ORGCTA, @STACTA
             	 ELSE
             		  SET @CTADEB = @GETVRT
      				 SET @CTACRD = @USRCRD  
             	 SELECT @GETVRT = CODUSU FROM TBCADCTA (NOLOCK) WHERE NIDCTA = @USRCRD AND STAREC=1 AND STACTA=250
             	 IF (@GETVRT>0)
      			       BEGIN
            			    SET @USUCRD = @GETVRT;
             			 END
            END
      	 ELSE
           	BEGIN
               SELECT @GETVRT = CODUSU FROM TBCADCTA (NOLOCK) WHERE NIDCTA = @USRCRD AND STAREC=1 AND STACTA=250
               IF (@GETVRT>0)
             		   BEGIN
             		      SET @USUDEB = @GETVRT;
             		      SET @CTADEB = @USRCRD
     			         END
                 	 EXEC @USUCRD = PRGETINFCRT @USRDEB, 1;
             			 EXEC @GETVRT = PRGETINFCRT @USRCRD, 2;
             			 EXEC @USUPRO = PRGETINFCRT @USRCRD, 3;
                   IF(@GETVRT=0)
             			     EXEC @CTACRD = PRGETCTAVRT  @USUCRD, @TIPCTA, @ORGCTA, @STACTA
             			 ELSE
             			     SET @CTACRD = @GETVRT
            END
      END
      		
    
                
    IF(@USUDEB=0 OR @USUCRD=0 OR @CTADEB=0 OR @CTACRD=0)
       BEGIN
          SET @CODRSP='57';
          SET @STAREC= 13;
          IF(@USUDEB=0)
             SET @DSCERR = 'USUARIO DE DEBITO INVALIDO'
          IF(@USUCRD=0)
            SET @DSCERR = 'USUARIO DE CREDITO INVALIDO'
          IF(@CTADEB=0)
          	 SET @DSCERR = 'CONTA DE DEBITO INVALIDA'
          IF(@CTACRD=0)
          	 SET @DSCERR = 'CONTA DE CREDITO INVALIDA'
       END

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBREGMOV(SUBSYS, TIPLCT, STAMOV, DATMOV, CODTRM, CODMOE, USUPRO, CODUSU, CODCRT, CODRSP, USRDEB, USRCRD, USUDEB, INDDEB, ORGDEB, CTADEB, USUCRD, INDCRD, ORGCRD, CTACRD, VLRMOV, OMTSLD, OMTTAR, NIDCAL, VLRTAR, DSCERR, SLDDAT, SLDVAL, NIDTRA, CANTRA, LOTFIN, STAREC, UPDUSU)
                        VALUES (@SUBSYS, @TIPLCT, @STAMOV, @DATMOV, @CODTRM, @CODMOE, @USUPRO, @CODUSU, @CODCRT, @CODRSP, @USRDEB, @USRCRD, @USUDEB, @INDDEB, @ORGDEB, @CTADEB, @USUCRD, @INDCRD, @ORGCRD, @CTACRD, @VLRMOV, @OMTSLD, @OMTTAR, @NIDCAL, @VLRTAR, @DSCERR, @SLDDAT, @SLDVAL, @NIDTRA, @CANTRA, @LOTFIN, @STAREC, @UPDUSU);
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
