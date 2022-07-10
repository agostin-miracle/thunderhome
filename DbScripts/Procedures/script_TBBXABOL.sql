IF OBJECT_ID ( 'dbo.PRBXABOLINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRBXABOLINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 10/04/2022 11:51:46
 Objetivo : Inserção de Registros na Tabela TBBXABOL
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRBXABOLINS
        (        @NIDRBB int = 0, 
        @NIDBOL int = 0, 
        @IDXSEQ smallint, 
        @TIPBXA tinyint = 0, 
        @DATPGT date, 
        @NUMPCL smallint = 1, 
        @VLRPAG money = 0, 
        @VLRMOR money, 
        @VLRJUR money = 0, 
        @VLRDES money = 0, 
        @VLRLIQ money = 0, 
        @VLRTEX money, 
        @DSCOBS varchar(255) = '', 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    DECLARE @RSTLIQ MONEY =0    
        DECLARE @CTLOPE TINYINT =0    
        IF( NOT EXISTS(SELECT 1 FROM TBREGBOL (NOLOCK) WHERE NIDBOL=@NIDBOL))
           BEGIN
              SET @STAREC=13
              SET @DSCOBS = 'BOLETO NÃO EXISTE'
              SET @RETURN_VALUE=-2
           END
        
        IF(@STAREC<>13)
           BEGIN
              IF( NOT EXISTS(SELECT 1 FROM TBREGBOL (NOLOCK) WHERE NIDBOL=@NIDBOL AND STABOL = 1))
                  BEGIN
                    SET @STAREC=13
                    SET @DSCOBS = 'BOLETO NÃO ESTÁ EM ABERTO'
                    SET @RETURN_VALUE=-3                
                  END
           END
        
        IF(@STAREC<>13)
           BEGIN
              IF(EXISTS(SELECT 1 FROM TBBXABOL (NOLOCK) WHERE NIDBOL=@NIDBOL AND STAREC=9))
    		     BEGIN
    				SET @STAREC = 13
    				SET @DSCOBS = 'REGISTRO DE BAIXA DE BOLETO JA EXISTE'
              SET @RETURN_VALUE=-4        
    		     END
           END          
        
        SELECT @IDXSEQ= (ISNULL(MAX(IDXSEQ),0) + 1) FROM TBBXABOL (NOLOCK) WHERE NIDBOL = @NIDBOL
        
        IF(@STAREC<>13)   
           BEGIN
              SELECT @RSTLIQ = ISNULL(SUM(VLROPE*SIGOPE),0) FROM TBREGOPE (NOLOCK) WHERE SUBSYS =2 AND STAREC IN (1,2) AND NIDREF=@NIDBOL GROUP BY NIDREF
              IF((@RSTLIQ-@VLRLIQ)!=0)
                BEGIN
                    SET @STAREC=13
                    SET @DSCOBS = 'VALOR DO RECEBIMENTO DIFERE DO SALDO DO BOLETO'
                    SET @RETURN_VALUE=-5
                END   
           END
           
        IF(@STAREC<>13)
           SET @CTLOPE=1 -- INDICA QUE O REGISTRO PODE SER PROCESSADO

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBBXABOL(NIDRBB, NIDBOL, IDXSEQ, TIPBXA, DATPGT, NUMPCL, VLRPAG, VLRMOR, VLRJUR, VLRDES, VLRLIQ, VLRTEX, DSCOBS, STAREC, UPDUSU)
                        VALUES (@NIDRBB, @NIDBOL, @IDXSEQ, @TIPBXA, @DATPGT, @NUMPCL, @VLRPAG, @VLRMOR, @VLRJUR, @VLRDES, @VLRLIQ, @VLRTEX, @DSCOBS, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = @@IDENTITY
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
    IF(@RETURN_VALUE>0 AND @CTLOPE=1)
    BEGIN
      DECLARE @IDPROC INT
      DECLARE @REGBXO INT      
			EXEC @IDPROC = PRGETNXTNUM 100      
       
			IF( NOT EXISTS (SELECT 1 
				               FROM TBREGOPE (NOLOCK)
                              WHERE SUBSYS=2
                                AND NIDREF= @NIDBOL
                                AND CODMOV = 540) AND @VLRLIQ > 0 )
			  BEGIN
					  INSERT INTO TBREGOPE (SUBSYS, NIDREF, DATOPE,CODMOV,  VLRBAS,VLRPCT,SIGOPE,VLRPCP,  VLROPE, STAREC, UPDUSU,USELCT,IDPROC)
						              VALUES (     2,@NIDBOL,@DATPGT,   540, @VLRLIQ,     0,    -1,     0, @VLRLIQ,      2,@UPDUSU,     1,@IDPROC)
			  END
        
  	  IF( NOT EXISTS (SELECT 1 
						            FROM TBREGOPE (NOLOCK)
							          WHERE SUBSYS=2
							            AND NIDREF= @NIDBOL
							            AND CODMOV = 522) AND @VLRMOR > 0 )
        BEGIN
				    INSERT INTO TBREGOPE (SUBSYS, NIDREF, DATOPE,CODMOV,  VLRBAS,VLRPCT,SIGOPE,VLRPCP,  VLROPE, STAREC, UPDUSU,IDPROC)
								         VALUES  (     2,@NIDBOL,@DATPGT,   522, @VLRMOR,     0,    1,     0, @VLRMOR,      2,@UPDUSU,@IDPROC)
			  END
      
		  IF( NOT EXISTS (SELECT 1 
								        FROM TBREGOPE (NOLOCK)
								       WHERE SUBSYS=2
								         AND NIDREF= @NIDBOL
								         AND CODMOV = 521) AND @VLRJUR > 0 )
		    BEGIN
				    INSERT INTO TBREGOPE (SUBSYS, NIDREF, DATOPE,CODMOV,  VLRBAS,VLRPCT,SIGOPE,VLRPCP,  VLROPE, STAREC, UPDUSU,IDPROC)
									        VALUES (     2,@NIDBOL,@DATPGT,   521, @VLRJUR,     0,    1,     0, @VLRJUR,      2,@UPDUSU,@IDPROC)
		    END
        
		  IF( NOT EXISTS (SELECT 1 
								    FROM TBREGOPE (NOLOCK)
								   WHERE SUBSYS=2
								     AND NIDREF= @NIDBOL
								     AND CODMOV = 523) AND @VLRDES > 0 )
		    BEGIN
					    INSERT INTO TBREGOPE (SUBSYS, NIDREF, DATOPE,CODMOV,  VLRBAS,VLRPCT,SIGOPE,VLRPCP,  VLROPE, STAREC, UPDUSU,IDPROC)
									  VALUES (     2,@NIDBOL,@DATPGT,   523, @VLRDES,     0,    -1,     0, @VLRDES,      2,@UPDUSU,@IDPROC)
		    END
		  IF( NOT EXISTS (SELECT 1 
								    FROM TBREGOPE (NOLOCK)
								   WHERE SUBSYS=2
								     AND NIDREF= @NIDBOL
								     AND CODMOV = 524) AND @VLRTEX > 0 )
		    BEGIN
					    INSERT INTO TBREGOPE (SUBSYS, NIDREF, DATOPE,CODMOV,  VLRBAS,VLRPCT,SIGOPE,VLRPCP,  VLROPE, STAREC, UPDUSU,IDPROC)
									  VALUES (     2,@NIDBOL,@DATPGT,   524, @VLRTEX,     0,     0,     0, @VLRTEX,      2,@UPDUSU,@IDPROC)
		    END


        EXEC @REGBXO = PRREGBOLCLOTKT @NIDBOL, 9, @TIPBXA, 2, @DATPGT,@UPDUSU,@VLRLIQ
           
        IF(@REGBXO>0)
            BEGIN
                    -- CONTABILIZA PARA O CONTA CORRENTE
                    
                    -- FECHA O REGISTRO COM STATUS DE PROCESSADO
                    UPDATE TBBXABOL SET STAREC=9 WHERE NIDRBD=@RETURN_VALUE                    
            END
        ELSE
            BEGIN
                 DELETE FROM TBREGOPE WHERE IDPROC=@IDPROC
                 UPDATE TBBXABOL SET STAREC=15 WHERE NIDRBD=@RETURN_VALUE
                 SET @RETURN_VALUE=-6
            END
    END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRBXABOLSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRBXABOLSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 10/04/2022 11:51:46
 Objetivo : Obtêm o Registro de Baixa de acordo com o id
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRBXABOLSEL
(
    @NIDRBD Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBBXABOL (NOLOCK) A

    WHERE     (A.NIDRBD=@NIDRBD)

GO

