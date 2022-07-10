
SELECT * FROM TBCADCTA
DECLARE @RV INT
EXEC dbo.PRREGMOVINS 1,22,1,'20220413',1,1,1,6,0,'00',6,7,0,2,0,0,2,0,1000,'N','N',0,0,'',NULL,0,1,0, @RV OUTPUT
PRINT @RV

SELECT * FROM TBREGMOV
UPDATE TBREGMOV SET VLRTAR=2.21 WHERE NIDTRF=2
UPDATE TBCADLCT SET INDCRD=6, MOVDEB=2 WHERE NIDLCT=12
SELECT * FROM TBTIPMOV
SELECT * FROM TBCADLCT WHERE TIPLCT = 22 AND CODTRM=1

DECLARE @LOTFIN INT = 0
DECLARE @NIDTRA VARCHAR(13) = N''
DECLARE @NXTNUM INT=0
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
    ,UPDUSU = A.UPDUSU
FROM TBREGMOV (NOLOCK) A
INNER JOIN TBCADLCT (NOLOCK) B ON (B.TIPLCT = A.TIPLCT AND B.CODTRM = A.CODTRM)
INNER JOIN TBTIPMOV (NOLOCK) C ON (C.CODMOV = B.MOVDEB)
WHERE A.NIDTRF=2 AND B.INDDEB>0 AND B.MOVDEB>0
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
    ,UPDUSU = A.UPDUSU
FROM TBREGMOV (NOLOCK) A
INNER JOIN TBCADLCT (NOLOCK) B ON (B.TIPLCT = A.TIPLCT AND B.CODTRM = A.CODTRM)
INNER JOIN TBTIPMOV (NOLOCK) C ON (C.CODMOV = B.MOVCRD)
WHERE A.NIDTRF=2 AND B.INDCRD>0 AND B.MOVCRD>0) A


SELECT * FROM TBREGCCR



        --(        @SUBSYS tinyint, 
        --@TIPLCT smallint = 0, 
        --@STAMOV smallint = 0, 
        --@DATMOV datetime, 
        --@CODTRM tinyint = 1, 
        --@CODMOE int = 1, 
        --@USUPRO int = 0, 
        --@CODUSU int = 0, 
        --@CODCRT int = 0, 
        --@CODRSP char(2) = '00', 
        --@USRDEB int = 0, 
        --@USRCRD int = 0, 
        --@USUDEB smallint, 
        --@INDDEB smallint, 
        --@CTADEB int, 
        --@USUCRD int, 
        --@INDCRD smallint, 
        --@CTACRD int, 
        --@VLRMOV money = 0, 
        --@OMTSLD char(1) = 'N', 
        --@OMTTAR char(1) = 'N', 
        --@NIDCAL int, 
        --@VLRTAR money = 0, 
        --@DSCERR varchar(150) = '', 
        --@SLDDAT datetime, 
        --@SLDVAL money = 0, 
        --@STAREC tinyint = 1, 
        --@UPDUSU int = 0,
        --@RETURN_VALUE int OUTPUT)



IF OBJECT_ID ( 'dbo.PRREGMOVINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGMOVINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 13/04/2022 17:12:20
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
        @CTADEB int, 
        @USUCRD int, 
        @INDCRD smallint, 
        @CTACRD int, 
        @VLRMOV money = 0, 
        @OMTSLD char(1) = 'N', 
        @OMTTAR char(1) = 'N', 
        @NIDCAL int, 
        @VLRTAR money = 0, 
        @DSCERR varchar(150) = '', 
        @SLDDAT datetime, 
        @SLDVAL money = 0, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

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
         IF(@TIPLCT = 11)
             	BEGIN
             		IF(@CODTRM=1)
               			BEGIN
    						       SET @USUDEB = @USRDEB
               				 EXEC @CTADEB = PRGETCTAVRT  @USRDEB, @TIPCTA, @ORGCTA, @STACTA
               				 EXEC @USUCRD = PRGETINFCRT  @USRCRD, 1          
                   		 SET @CTACRD = @USRCRD;
            			  END
    			    ELSE
             		    BEGIN
    						      SET @CTADEB = @USRCRD;
    						      EXEC @CTACRD = PRGETCTAVRT  @USRDEB, @TIPCTA, @ORGCTA, @STACTA
    
                      SELECT @GETVRT = CODUSU FROM TBCADCTA (NOLOCK) WHERE NIDCTA = @USRCRD AND STAREC=1 AND STACTA=250
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
                
                
            IF(@TIPLCT IN (12,21,112))
                 BEGIN
                    IF(@CODTRM = 1)
                         BEGIN
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
                   65 - REGISTRO DE COMBO MOBILIDADE                      (16/03/2021)
                   66 - REVERSAO DE REGISTRO DE COMBO MOBILIDADE          (16/03/2021)
                   78 - RECARGA DE ORIGEM TED
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
                  139 - COMBO MOBILIDADE
                */
                
    	IF (@TIPLCT IN (34,44,55,56,57,58,59,60,61,63,64,78,65,66,22, 120, 121, 122, 123, 124, 125, 126, 127,128,130))
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
        if (@TIPLCT IN (23,27))
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
                
           IF (@TIPLCT = 24)
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
      	IF (@TIPLCT = 25)
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
             				      IF(@GETVRT>0)
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
      		
    
                
                
          /*
            29 - TRANSFERENCIA PARA CONTA ESPELHO (SALDO CREDOR)
          */
          if (@TIPLCT IN (29, 129))
              BEGIN
                  SET @USUDEB = @USRDEB;
                  SET @USUCRD = @USRCRD;
                  if (@USUDEB > 0)
          			EXEC @CTADEB = PRGETCTAVRT  @USUDEB, @TIPCTA, @ORGCTA, @STACTA
                  if (@USUCRD > 0)
          			EXEC @CTACRD = PRGETCTAVRT  @USUDEB, @TIPCTA, @ORGCTA, @STACTA
              END
          
          /*
           *  ADIANTAMENTO DE BONUS / PAGAMENTO DE BONUS
           */
          if (@TIPLCT IN (30,31))
              BEGIN
                  SET @USUDEB = @USRDEB;
                  SET @USUCRD = @USRCRD;
                  if (@USUDEB > 0)
          			EXEC @CTADEB = PRGETCTAVRT  @USUDEB, @TIPCTA, @ORGCTA, @STACTA
                  if (@USUCRD > 0)
          			EXEC @CTACRD = PRGETCTAVRT  @USUDEB, @TIPCTA, @ORGCTA, @STACTA
              END
          
          /*
          	TRANSFERENCIA PARA CONTA ESPELHO SALDO DEVEDOR
          */
          if (@TIPLCT = 33)
              BEGIN
                  SET @USUDEB = @USRDEB;
                  SET @USUCRD = @USRCRD;
                  if (@USUDEB > 0)
          			EXEC @CTADEB = PRGETCTAVRT  @USUDEB, @TIPCTA, @ORGCTA, @STACTA
                  if (@USUCRD > 0)
          			EXEC @CTACRD = PRGETCTAVRT  @USUDEB, @TIPCTA, @ORGCTA, @STACTA
              END
          
          /*
          	SAQUE - TIPO_LANCAMENTO_REGISTRO_SAQUE
          */
          
          if (@TIPLCT = 71)
              BEGIN
                  if (@CODTRM = 1)
                      BEGIN
                          SET @USUDEB = @USRDEB;
                          SET @USUCRD = @USRCRD;
                      END
                  ELSE
                      BEGIN
                          SET @USUDEB = @USRCRD;
                          SET @USUCRD = @USRDEB;
    
                      END
                  if (@USUDEB > 0)
          			        EXEC @CTADEB = PRGETCTAVRT  @USUDEB, @TIPCTA, @ORGCTA, @STACTA
                  if (@USUCRD > 0)
          			        EXEC @CTACRD = PRGETCTAVRT  @USUDEB, @TIPCTA, @ORGCTA, @STACTA
          
              END
                
                /*
                	TED - TIPO_LANCAMENTO_REGISTRO_TED
                */
                
                if (@TIPLCT = 80)
                    BEGIN
                        if (@CODTRM = 1)
                            BEGIN
                                SET @USUDEB = @USRDEB;
                                SET @USUCRD = @USRCRD;
                            END
                        if (@CODTRM = -1)
                            BEGIN
                                SET @USUDEB = @USRCRD;
                                SET @USUCRD = @USRDEB;
    
                            END
                        if (@USUDEB > 0)
                			        EXEC @CTADEB = PRGETCTAVRT  @USUDEB, @TIPCTA, @ORGCTA, @STACTA
                        if (@USUCRD > 0)
                			        EXEC @CTACRD = PRGETCTAVRT  @USUDEB, @TIPCTA, @ORGCTA, @STACTA
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
                    
            
            IF(@RETURN_VALUE = 0 AND @OMTTAR='N')
               BEGIN
                    SELECT @CODTAR = ISNULL(CODTAR,0) FROM TBTIPLCT (NOLOCK) WHERE TIPLCT =@TIPLCT AND STAREC=1
                    IF(@CODTAR > 0)
                       BEGIN
                           EXEC @NIDCAL = PRCADTARCALV2 @USUPRO, @CODUSU, @CODTAR,@VLRMOV, @UPDUSU,1,1, NULL,NULL,1,0,0,0,0
                           IF(@NIDCAL>0)
                              SELECT @VLRTAR = EXTVLR FROM TBCALTAR (NOLOCK) WHERE NIDCAL = @NIDCAL
                       END
               END
                
            IF(@RETURN_VALUE = 0 AND @OMTSLD='N')
               BEGIN
                  SET @SLDDAT = GETDATE()   
                  EXEC @SLDVAL = PRREGCCRSLDACT @INDDEB, @CTADEB
                  IF(@SLDVAL <0)
                     BEGIN
                        SET @STAREC=13
                        SET @DSCERR = @DSCERR + ' SALDO NEGATIVO' + CHAR(13);
                     END
                  ELSE
                     IF( (@SLDCTA - (@VLRMOV + @VLRTAR)) < 0)
                        BEGIN
                          SET @STAREC=13            
                          SET @DSCERR = @DSCERR + 'SALDO INSUFICIENTE PARA DÉBITO DE ' + CONVERT(VARCHAR,(@VLRMOV + @VLRTAR)) + CHAR(13);            
                        END
                END

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBREGMOV(SUBSYS, TIPLCT, STAMOV, DATMOV, CODTRM, CODMOE, USUPRO, CODUSU, CODCRT, CODRSP, USRDEB, USRCRD, USUDEB, INDDEB, CTADEB, USUCRD, INDCRD, CTACRD, VLRMOV, OMTSLD, OMTTAR, NIDCAL, VLRTAR, DSCERR, SLDDAT, SLDVAL, STAREC, UPDUSU)
                        VALUES (@SUBSYS, @TIPLCT, @STAMOV, @DATMOV, @CODTRM, @CODMOE, @USUPRO, @CODUSU, @CODCRT, @CODRSP, @USRDEB, @USRCRD, @USUDEB, @INDDEB, @CTADEB, @USUCRD, @INDCRD, @CTACRD, @VLRMOV, @OMTSLD, @OMTTAR, @NIDCAL, @VLRTAR, @DSCERR, @SLDDAT, @SLDVAL, @STAREC, @UPDUSU);
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
